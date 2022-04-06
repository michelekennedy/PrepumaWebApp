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
    public partial class LogisticsImport : System.Web.UI.Page
    {
        
        AccountImportCollection temp;
        static clsScheduledProcessLog scheduledProcessLog;
        static StringBuilder processLog;
        Telerik.Web.UI.RadProgressBar progressBar = new Telerik.Web.UI.RadProgressBar();

        //String tempPath = ConfigurationManager.AppSettings["tempDir"];

        //static List<AccountImport> transactions;
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
            
            if (LogisticsFile.HasFile) { 
            string filePath;
            filePath = createTempFile(LogisticsFile);

            temp = new AccountImportCollection();

            if (scheduledProcessLog == null)
                scheduledProcessLog = CreateProcessingLog();

            if (processLog == null)
                processLog = new StringBuilder();

            if (filePath == "")
            {
                pnlDanger.Visible = true;
                lblDanger.Text = "Cannot create Temp File.";
                processLog.Append(DateTime.Now.ToShortTimeString() + ":  Cannot create Temp File.");
                processLog.Append(Environment.NewLine);
                AbortProcessingLog();
            }
            else
            {

                //
                //STEP 1 Initialize Progress Bar
                //
                const int totalSteps = 8; //number of steps for progress bar update
                int stepNum = 1;
                RadProgressContext progress = RadProgressContext.Current;

                
                progress.Speed = "N/A";
                updateProgressBar(progress, totalSteps, stepNum);
                stepNum = stepNum + 1;


                processLog.Append(DateTime.Now.ToShortTimeString() + ":  File " + LogisticsFile.FileName + " Selected.");
                processLog.Append(Environment.NewLine);

 
                    pnlInfo.Visible = true;
                    lblInfo.Text = "<i>Validating Import File</i>";

                    string validExcelFile = temp.ValidateExcelFile(filePath, LogisticsFile.FileName);
                    if (validExcelFile == "")
                    {

                        processLog.Append(DateTime.Now.ToShortTimeString() + ":  Import File Validated.");
                        processLog.Append(Environment.NewLine);
                        updateProgressBar(progress, totalSteps, stepNum);
                        stepNum = stepNum + 1;

                        //
                        //STEP 2 Clear SQL Import Table
                        //
                        string clearTableResults = temp.ClearImportSQLTable();
                        if (clearTableResults == "")
                        {

                            processLog.Append(DateTime.Now.ToShortTimeString() + ":  Account Import SQL File Cleared.");
                            processLog.Append(Environment.NewLine);
                            updateProgressBar(progress, totalSteps, stepNum);
                            stepNum = stepNum + 1;

                            //
                            //STEP 3 Parse ExelFile
                            //
                            string parseError = temp.ParseExcel(filePath, LogisticsFile.FileName, progress);
                            if (parseError == "")
              
                            {
                                //temp has the spreadsheet converted to a collection
                               
                                processLog.Append(DateTime.Now.ToShortTimeString() + ":  Excel File Parsed.");
                                processLog.Append(Environment.NewLine);
                                updateProgressBar(progress, totalSteps, stepNum);
                                stepNum=stepNum+1;

                                //
                                //STEP 4 Upload to SQL Import Table
                                //
                                string accountWithProblem = temp.UploadAccounts();
                                if (accountWithProblem == "")
                                {
                                    processLog.Append(DateTime.Now.ToShortTimeString() + ":  Data Imported into tblAccountImport SQL Table.");
                                    processLog.Append(Environment.NewLine);
                                    updateProgressBar(progress, totalSteps, stepNum);
                                    stepNum = stepNum + 1;

                                    //
                                    //STEP 5 Verify the Accounts 
                                    //
                                    bool validateOK = validateAccounts();
                                    updateProgressBar(progress, totalSteps, stepNum);
                                    stepNum = stepNum + 1;
                                    if (validateOK)
                                    {

                                        //
                                        //STEP 6 Update the Contract Classifaction Table
                                        //
                                        int classRows = temp.UpdateClassification((string)(Session["userName"]));
                                        updateProgressBar(progress, totalSteps, stepNum);
                                        stepNum = stepNum + 1;

                                        //
                                        //STEP 7 Update the 2 Contract Account Classifaction Table
                                        //
                                        int acctClassRows = temp.UpdateClassificationAccounts((string)(Session["userName"]));
                                        updateProgressBar(progress, totalSteps, stepNum);
                                        stepNum = stepNum + 1;
                                        string msg = "";
                                        if (classRows < 0 || acctClassRows < 0)
                                        {
                                            msg = "<ul>";
                                            if (classRows < 0)
                                            {
                                               msg = msg + "<li>Error Encountered During <i>Contract Classification</i> Update.";
                                               processLog.Append(DateTime.Now.ToShortTimeString() + ":  Error Encountered During Contract Classification Import.");
                                               processLog.Append(Environment.NewLine);
                                            }
                                            if (acctClassRows < 0)
                                            {
                                               msg = msg + "<li>Error Encountered During <i>Contract Account Classification</i> Update.";
                                               processLog.Append(DateTime.Now.ToShortTimeString() + ":  Error Encountered During Contract Account Classification Import.");
                                               processLog.Append(Environment.NewLine);
                                            }
                                            msg = msg + "</ul>";
                                            pnlInfo.Visible = false;
                                            pnlDanger.Visible = true;
                                            lblDanger.Text = msg;
                                            AbortProcessingLog();
                                        } 
                                        else 
                                        {
                                            pnlSuccess.Visible = true;
                                            msg = LogisticsFile.FileName + ": <b>Import Successfully Completed</b> <ul><li>" + Convert.ToString(classRows) + " Rows Inserted into <i>tblContractClassification</i>";
                                            msg = msg + "<li> " + Convert.ToString(acctClassRows) + " Rows Inserted into <i>tblContractAccountClassification</i><ul>";
                                            lblSuccess.Text = msg;
                                            pnlInfo.Visible = false;
                                            processLog.Append(DateTime.Now.ToShortTimeString() + ":  Data Successfully Imported to the Contract Tables.");
                                            processLog.Append(Environment.NewLine);
                                            processLog.Append(DateTime.Now.ToShortTimeString() + ":  " + Convert.ToString(classRows) +  "  Rows Inserted into tblContractClassification.");
                                            processLog.Append(Environment.NewLine);
                                            processLog.Append(DateTime.Now.ToShortTimeString() + ":  " + Convert.ToString(acctClassRows) + "  Rows Inserted into tblContractAccountClassification.");
                                            processLog.Append(Environment.NewLine);
                                            processLog.Append(DateTime.Now.ToShortTimeString() + ":  Logistics Accounts Import Process Completed!.");
                                            processLog.Append(Environment.NewLine);
                                            scheduledProcessLog.Status = clsScheduledProcessLog.STATUS_COMPLETED;
                                            clsScheduledProcessLog.UpdateScheduledProcessLog(scheduledProcessLog);
                                            updateProgressBar(progress, totalSteps, stepNum);
                                            stepNum = stepNum + 1;
                                        }
                                        
                                    }
                                    else
                                    {
                                        //failed the validation, Duplicates and/or Missing Accounts are already displayed
                                        pnlInfo.Visible = true;
                                        lblInfo.Text = LogisticsFile.FileName + ": No Rows Were Imported";
                                    }
                                }
                                else
                                {
                                    pnlInfo.Visible = false;
                                    pnlDanger.Visible = true;
                                    lblDanger.Text = "Error Encountered During SQL Import with Account: " + accountWithProblem;
                                    processLog.Append(DateTime.Now.ToShortTimeString() + ":  Error Encountered During SQL Import with Account: " + accountWithProblem);
                                    processLog.Append(Environment.NewLine);
                                    AbortProcessingLog();
                                }

                               
                             

                            }
                            else
                            {
                                pnlInfo.Visible = false;
                                pnlDanger.Visible = true;
                                lblDanger.Text = "Error Encountered during Excel File Parse: " + parseError;
                                processLog.Append(DateTime.Now.ToShortTimeString() + ":  Error Encountered during Excel File Parse: " + parseError);
                                processLog.Append(Environment.NewLine);
                                AbortProcessingLog();
                            }

                            
                        }
                        else
                        {
                            pnlInfo.Visible = false;
                            pnlDanger.Visible = true;
                            lblDanger.Text = "Error Encountered Clearing Import Table: " + clearTableResults;
                            processLog.Append(DateTime.Now.ToShortTimeString() + ":  Error Encountered Clearing Import Table: " + clearTableResults);
                            processLog.Append(Environment.NewLine);
                            AbortProcessingLog();
                        }
                       
                    }
                    else
                    {
                        pnlInfo.Visible = false;
                        pnlDanger.Visible = true;
                        lblDanger.Text = validExcelFile;
                        processLog.Append(DateTime.Now.ToShortTimeString() + ":  Error Encountered on File Parse: " + validExcelFile);
                        processLog.Append(Environment.NewLine);
                        AbortProcessingLog();
                    }
                   

              }

                //
                //Clean Up
                //
                clsScheduledProcessLogFile scheduledProcessLogFile = new clsScheduledProcessLogFile(scheduledProcessLog.ScheduledProcessLogId, LogisticsFile.FileName, "xls");
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

        private string createTempFile(FileUpload LogisticsFile)
        {
            String tempPath = Server.MapPath("~/temp/");
            try 
            {
                

                //first check if temp dir exists
                if (!Directory.Exists(tempPath))
                {
                    System.IO.Directory.CreateDirectory(tempPath);
                }
                string tempFilePath = tempPath + LogisticsFile.FileName;

                LogisticsFile.PostedFile.SaveAs(tempFilePath);
                return tempFilePath;
            }
            catch (Exception ex)
            {
                pnlWarning.Visible = true;
                //lblWarning.Text = "Cannot create temp file in " + tempPath;
                lblWarning.Text = ex.ToString();
                return "";
            }
        }

       
        private Boolean validateAccounts()
        {
            bool isValid=true;

            
            //Check that Accounts are in the tblAccounts table
            string msg;
            List<string> invalidAccts = new List<string>();
            invalidAccts = temp.ValidateAccounts();
            int numInvalidAccts = invalidAccts.Count;
            if (numInvalidAccts > 0)
            {
                //Display invalid accounts
                msg = "The following Accounts are not in the Accounts Table<ul>";
                msg = msg + listInvalidAccounts(invalidAccts);
                processLog.Append(DateTime.Now.ToShortTimeString() + ": " + Convert.ToString(invalidAccts.Count) + "  Invalid Accounts Found.");
                processLog.Append(Environment.NewLine);
                pnlWarning.Visible = true;
                lblWarning.Text = msg;
                pnlInfo.Visible = false;
                isValid = false;
            }

            //Check that Accounts are NOT the tblContractAccountClassification table
            
            List<string> invalidCAccts = new List<string>();
            invalidCAccts = temp.ValidateClassAccounts();
            int numInvalidCAccts = invalidCAccts.Count;
            if (numInvalidCAccts > 0)
            {
                //Display invalid accounts
                msg = "The following Accounts are Already in the Contract Account Classification Table<ul>";
                msg = msg + listInvalidAccounts(invalidCAccts);
                processLog.Append(DateTime.Now.ToShortTimeString() + ": " + Convert.ToString(invalidCAccts.Count) + "  Accounts Found That are Already in ContractAccountClassification.");
                processLog.Append(Environment.NewLine);
                pnlDanger.Visible = true;
                lblDanger.Text = msg;
                pnlInfo.Visible = false;
                isValid = false;
            }


             return isValid;

        }

        private string listInvalidAccounts(List<string> invalidAccts)
        {
            //display invalid accounts as a list
            string msg="";

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

        static void AbortProcessingLog() {
            scheduledProcessLog.Status = clsScheduledProcessLog.STATUS_ABORTED;
            clsScheduledProcessLog.UpdateScheduledProcessLog(scheduledProcessLog);
            processLog.Append(DateTime.Now.ToShortTimeString() + ":  Processing Aborted.");
            processLog.Append(Environment.NewLine);
        }

        private void writeLogToText()
        {
            //string logFile = tempPath + LogisticsFile.FileName + scheduledProcessLog.ScheduledProcessLogId;
            //**Skanike string logFile = tempPath + LogisticsFile.FileName + "_importLog";

            String tempPath = Server.MapPath("~/temp/");

            //string logFile = Path.Combine(Server.MapPath(tempPath), LogisticsFile.FileName) +"_importLog";
            string logFile = tempPath + LogisticsFile.FileName + "_importLog";

            logFile = logFile.Replace(".","");
            logFile = logFile + ".txt";

            System.IO.File.WriteAllText(@logFile, processLog.ToString());
        }

        private void updateProgressBar(RadProgressContext progress, int Total, int stepNum)
        {
            
            double dTotal = Total * 12.5;
            double dStepNum = stepNum * 12.5;


            progress.PrimaryTotal = Total;
            progress.PrimaryValue = stepNum;
            progress.PrimaryPercent = dStepNum;

            progress.SecondaryTotal = 100;
            progress.SecondaryValue = 1;
            progress.SecondaryPercent = 1;

         
            progress.CurrentOperationText = "Step# " + stepNum.ToString() + " of " + Total.ToString();



            if (!Response.IsClientConnected)
            {
                //Cancel button was clicked or the browser was closed, so stop processing
                //break;

            }

            progress.TimeEstimated = (dTotal - dStepNum) * 100;
           
        }
        
    }
}