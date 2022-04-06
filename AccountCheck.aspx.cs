using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using PI_BusinessClasses;
using PrepumaWebApp.App_Data.DAL;
using System.Text;
using PI_Application;
using Telerik.Web.UI;
using Telerik.Web.UI.Upload;
using System.Diagnostics;
using System.Configuration;


namespace PrepumaWebApp
{

    public partial class AccountCheck : System.Web.UI.Page
    {

        AccountImportCollection temp;
        static clsScheduledProcessLog scheduledProcessLog;
        static StringBuilder processLog;
        Telerik.Web.UI.RadProgressBar progressBar = new Telerik.Web.UI.RadProgressBar();

        //String tempPath = ConfigurationManager.AppSettings["tempDir"];
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                //added by Jyothi for user authentication.
                if (Session["userName"] != null && Session["appName"] != null && (Session["userRole"].Equals("Admin") || Session["userRole"].Equals("ITAdmin")))
                {
                    //Do not display SelectedFilesCount progress indicator.
                    RadProgressArea1.ProgressIndicators &= ~ProgressIndicators.SelectedFilesCount;

                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }

            RadProgressArea1.Localization.Uploaded = "Total Progress";
            RadProgressArea1.Localization.UploadedFiles = "Progress";
            RadProgressArea1.Localization.CurrentFileName = "Custom progress in action: ";
           
          
        }

        protected void btnUpload_Click(object sender, System.EventArgs e)
        {

            pnlWarning.Visible = false;
            pnlSuccess.Visible = false;
            pnlDanger.Visible = false;

            if (ExcelFile.HasFile)
            {
                string filePath;
                filePath = createTempFile(ExcelFile);

                temp = new AccountImportCollection();

                if (scheduledProcessLog == null)
                    scheduledProcessLog = CreateProcessingLog();

                if (processLog == null)
                    processLog = new StringBuilder();

                if (filePath == "")
                {
                    pnlDanger.Visible = true;
                    lblDanger.Text = "Cannot Upload File. ";
                    processLog.Append(DateTime.Now.ToShortTimeString() + ":  Cannot Upload File.");
                    processLog.Append(Environment.NewLine);
                    AbortProcessingLog();
                }
                else
                {
                    //Initialize Progress Bar
                    const int totalSteps = 4; //number of steps for progress bar update
                    int stepNum = 1;
                    RadProgressContext progress = RadProgressContext.Current;


                    progress.Speed = "N/A";
                    updateProgressBar(progress, totalSteps, stepNum);
                    stepNum = stepNum + 1;


                    processLog.Append(DateTime.Now.ToShortTimeString() + ":  File " + ExcelFile.FileName + " Selected.");
                    processLog.Append(Environment.NewLine);
                    //STEP 1 Validate
                    Boolean doImport = ValidateFileType(ExcelFile.FileName);
                    updateProgressBar(progress, totalSteps, stepNum);
                    stepNum = stepNum + 1;

                    if (doImport)
                    {



                        pnlInfo.Visible = true;
                        pnlWarning.Visible = false;
                        btnUpload.Visible = false;
                        lblInfo.Text = "<i>Validating Import File</i>";
                        if (temp.ValidateAccountExcelFile(filePath, ExcelFile.FileName))
                        {

                            processLog.Append(DateTime.Now.ToShortTimeString() + ":  Import File Validated.");
                            processLog.Append(Environment.NewLine);
                            updateProgressBar(progress, totalSteps, stepNum);
                            stepNum = stepNum + 1;
 
                                //STEP 3 Parse ExelFile
                                List<String> listInvalidAccounts = (temp.ParseAccountsFromExcel(filePath, ExcelFile.FileName, progress));
                              
                                processLog.Append(DateTime.Now.ToShortTimeString() + ":  Excel File Parsed.");
                                processLog.Append(Environment.NewLine);
                                updateProgressBar(progress, totalSteps, stepNum);
                                stepNum = stepNum + 1;

                               //Print out new Account numbers
                                    
                               ShowInvalidAccounts(listInvalidAccounts, ExcelFile.FileName);
                               ExcelFile.Visible = false;
                               btnUpload.Visible = false;
                               lblFileType.Visible = false;

                               grid.DataSource = listInvalidAccounts;
                               grid.Rebind();
 
                        }
                        else
                        {
                            pnlInfo.Visible = false;
                            pnlDanger.Visible = true;
                            lblDanger.Text = "File Does not have the proper column format - Cannot find Account Column.";
                            processLog.Append(DateTime.Now.ToShortTimeString() + ":  File Does not have the proper column format.");
                            processLog.Append(Environment.NewLine);
                            AbortProcessingLog();
                        }

                    }
                    else
                    {
                        pnlWarning.Visible = false;
                        pnlDanger.Visible = true;
                        lblDanger.Text = "Select an <i>Excel File</i> to Upload";
                        AbortProcessingLog();
                    }

                }

                //Clean Up
                clsScheduledProcessLogFile scheduledProcessLogFile = new clsScheduledProcessLogFile(scheduledProcessLog.ScheduledProcessLogId, ExcelFile.FileName, "xls");
                clsScheduledProcessLogFile.UpdateScheduledProcessLogFile(scheduledProcessLogFile);
                scheduledProcessLog = null;

                if (processLog != null)
                {
                    writeLogToText();
                    processLog.Clear();
                }
            }
            else
            {
                pnlWarning.Visible = true;
                lblWarning.Text = "Select File to Upload";
            }



        }

        private string createTempFile(FileUpload excelFile)
        {
            String tempPath = Server.MapPath("~/temp/");
            try
            {
               
                //first check if temp dir exists
                if (!Directory.Exists(tempPath))
                {
                    System.IO.Directory.CreateDirectory(tempPath);
                }

                string tempFilePath = tempPath + excelFile.FileName;
                //string tempFilePath = Path.Combine(Server.MapPath(tempPath), excelFile.FileName);
                excelFile.PostedFile.SaveAs(tempFilePath);
                return tempFilePath;
            }
            catch (Exception ex)
            {
                pnlWarning.Visible = true;
                lblWarning.Text = "Cannot create temp file in " + tempPath;
                return "";
            }
        }

        private Boolean ValidateFileType(string filename)
        //Make sure it's an Excel File
        {
            FileInfo fi = new FileInfo(filename);
            string ext = fi.Extension;
            if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                return true;
            else
            {
                return false;
            }
        }

        private void ShowInvalidAccounts(List<String> invalidAccounts, string filename)
        {
   

            //Check that Accounts are in the tblAccounts table
            string msg = "File: " + filename + "<br>";
            int numInvalidAccts = invalidAccounts.Count;
            if (numInvalidAccts > 0)
            {
                //Display invalid accounts
                msg = msg + Convert.ToString(invalidAccounts.Count) + "  New Accounts Found.";
                //msg = msg + "The following Accounts are not in the Accounts Table<ul>";
                //msg = msg + listInvalidAccounts(invalidAccounts);
                processLog.Append(DateTime.Now.ToShortTimeString() + ": " + Convert.ToString(invalidAccounts.Count) + "  New Accounts Found.");
                processLog.Append(Environment.NewLine);
                pnlWarning.Visible = true;
                lblWarning.Text = msg;
                pnlInfo.Visible = false;

            }
            else
            {
                msg = msg + "No New Accounts Found.";
                processLog.Append(DateTime.Now.ToShortTimeString() + ": " + Convert.ToString(invalidAccounts.Count) + "  No new Accounts Found.");
                processLog.Append(Environment.NewLine);
                pnlWarning.Visible = true;
                lblWarning.Text = msg;
                pnlInfo.Visible = false;
            }
           
        }

        private string listInvalidAccounts(List<string> invalidAccts)
        {
            //display invalid accounts as a list
            string msg = "";

            foreach (string item in invalidAccts)
            {
                msg = msg + "<li>" + item;
            }


            msg = msg + "</ul>";


            return msg;
        }



        static clsScheduledProcessLog CreateProcessingLog()
        {
            clsScheduledProcess scheduledProcess = clsScheduledProcess.GetScheduledProcess("Logistics Account Import");
            clsScheduledProcessLog scheduledProcessLog = scheduledProcess.CreateLogEntry("Logistics_Account_Import_Log.txt");

            return scheduledProcessLog;
        }

        static void AbortProcessingLog()
        {
            scheduledProcessLog.Status = clsScheduledProcessLog.STATUS_ABORTED;
            clsScheduledProcessLog.UpdateScheduledProcessLog(scheduledProcessLog);
            processLog.Append(DateTime.Now.ToShortTimeString() + ":  Processing Aborted.");
            processLog.Append(Environment.NewLine);
        }

        private void writeLogToText()
        {
            //string logFile = tempPath + LogisticsFile.FileName + scheduledProcessLog.ScheduledProcessLogId;
            //** skanike string logFile = tempPath + ExcelFile.FileName + "_importLog";
            
            String tempPath = Server.MapPath("~/temp/");

            //string logFile = Path.Combine(Server.MapPath(tempPath), ExcelFile.FileName) + "_importLog";
            string logFile = tempPath + ExcelFile.FileName + "_importLog";

            logFile = logFile.Replace(".", "");
            logFile = logFile + ".txt";

            System.IO.File.WriteAllText(@logFile, processLog.ToString());
        }

        private void updateProgressBar(RadProgressContext progress, int Total, int stepNum)
        {
            //base it on 100 - 4 steps 
            double dTotal = Total * 25;
            double dStepNum = stepNum * 25;
            

            progress.PrimaryTotal = Total;
            progress.PrimaryValue = stepNum;
            progress.PrimaryPercent = dStepNum;

            progress.SecondaryTotal = 100;
            progress.SecondaryValue = 1;
            progress.SecondaryPercent = 1;

            progress.CurrentOperationText = "Step " + stepNum.ToString();



            if (!Response.IsClientConnected)
            {
                //Cancel button was clicked or the browser was closed, so stop processing
                //break;

            }

            progress.TimeEstimated = (dTotal - dStepNum) * 100;
            //Stall the current thread for 0.1 seconds
            System.Threading.Thread.Sleep(100);




        }
    }
}