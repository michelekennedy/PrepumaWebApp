using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Globalization;
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
    public partial class AccountRealignment : System.Web.UI.Page
    {
        ClsAlignmentImport temp = new ClsAlignmentImport();
        String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
        static StringBuilder processLog;
        Telerik.Web.UI.RadProgressBar progressBar = new Telerik.Web.UI.RadProgressBar();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userName"] != null && Session["appName"] != null)
                {
                    getYears();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void getYears()
        {
            Int32 curryear = DateTime.Now.Year;
            Int32 lastyear = DateTime.Now.Year - 1;

            cbxYear.Items.Insert(0, lastyear.ToString());
            cbxYear.Items.Insert(0, curryear.ToString());
            cbxYear.SelectedValue = curryear.ToString();

            RadComboBoxItem item1 = new RadComboBoxItem();
            RadComboBoxItem item2 = new RadComboBoxItem();
            RadComboBoxItem item3 = new RadComboBoxItem();
            RadComboBoxItem item4 = new RadComboBoxItem();
            item1.Text = "Qtr1";
            item1.Value = "1";
            cbxQtr.Items.Add(item1);
            item2.Text = "Qtr2";
            item2.Value = "2";
            cbxQtr.Items.Add(item2);
            item3.Text = "Qtr3";
            item3.Value = "3";
            cbxQtr.Items.Add(item3);
            item4.Text = "Qtr4";
            item4.Value = "4";
            cbxQtr.Items.Add(item4);

            cbxQtr.SelectedIndex = 0;


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                int qtr = Convert.ToInt16(cbxQtr.SelectedItem.Value);
                int yr = Convert.ToInt16(cbxYear.Text);


                int yrToCheck = yr;
                int qtrToCheck = qtr - 1;
                if (qtrToCheck <= 0)
                {
                    qtrToCheck = 4;
                    yrToCheck = yrToCheck - 1;
                }

                bool backupDone = CheckPrior(qtrToCheck, yrToCheck);
                if (backupDone == true)
                {
                    DoImport();

                }



            }
            catch (Exception ex)
            {
                lblDanger.Text = ex.Message;
                pnlDanger.Visible = true;
            }

        }

        protected void DoImport()
        {

            try
            {
                pnlWarning.Visible = false;
                pnlSuccess.Visible = false;
                pnlDanger.Visible = false;


                startProcessLog();

                ////INITIALIZE PROGRESS BAR
                const int totalSteps = 4; //number of steps for progress bar update

                int stepNum = 1;
                RadProgressContext progress = RadProgressContext.Current;

                progress.Speed = "N/A";
                updateProgressBar(progress, totalSteps, stepNum);
                stepNum = stepNum + 1;


                processLog.Append(DateTime.Now.ToShortTimeString() + ":  Realignment Import File:  " + ImportFile.FileName);
                processLog.Append(Environment.NewLine);
                processLog.Append(DateTime.Now.ToShortTimeString() + ":  --- UPLOAD FILES ---");
                processLog.Append(Environment.NewLine);

                bool doImport = true;

                //create temp file to get it on the server
                string filePath = "";
                string alldataErrors = "";
                if (ImportFile.HasFile)
                {
                    filePath = createTempFile(ImportFile);
                    //lblFilePath.Text = "File Selected: " + ImportFile.FileName;
                    temp = new ClsAlignmentImport();
                    processLog.Append(DateTime.Now.ToShortTimeString() + ":  Uploaded Relalignment File:  " + filePath);
                    processLog.Append(Environment.NewLine);
                    updateProgressBar(progress, totalSteps, stepNum);
                    stepNum = stepNum + 1;
                }
                else
                {
                    alldataErrors = "Error Uploading Realignment File";
                    doImport = false;
                }

                if (doImport == false)
                {
                    abortLogFile(alldataErrors);
                }

                //make sure file paths are there
                if (doImport)
                {
                    // Check that Temp File Paths have Value
                    if (filePath == "")
                    {
                        abortLogFile("Cannot upload Files.");
                        doImport = false;
                    }
                }


                //VALIDATE DATA TYPES
                if (doImport)
                {
                    alldataErrors = "";
                    processLog.Append(DateTime.Now.ToShortTimeString() + ":  --- VALIDATE DATA TYPES ---");
                    processLog.Append(Environment.NewLine);
                    //Parse Data, make sure data types are ok
                    string dataErrors = "";
                    dataErrors = temp.ParseExcel(filePath, ImportFile.FileName, progress);
                    if (dataErrors != "")
                    {

                        alldataErrors = "Data Errors Found:<br>" + dataErrors;
                        doImport = false;
                    }
                    else
                    {
                        updateLogFile("Data File Data Validated", progress, totalSteps, stepNum);
                    }


                    if (doImport == false)
                    {
                        abortLogFile(alldataErrors);
                    }
                    updateProgressBar(progress, totalSteps, stepNum, "Clearing Import Table");
                    stepNum = stepNum + 1;
                }

                //DATA TYPES VALIDATED, NOW LOAD INTO TEMP SQL FILES FOR FINAL VALIDATION BEFORE IMPORT

                // Clear SQL Import Tables
                if (doImport)
                {


                    alldataErrors = "";
                    processLog.Append(DateTime.Now.ToShortTimeString() + ":  --- CLEAR IMPORT TABLE ---");
                    processLog.Append(Environment.NewLine);
                    bool tablesCleared = true;
                    tablesCleared = temp.ClearImportSQLTable();
                    if (tablesCleared == false)
                    {

                        alldataErrors = "Error Encountered Clearing Realignment Import SQL Table";
                        doImport = false;
                    }
                    else
                    {
                        updateLogFile("Realignment Import SQL Table Cleared", progress, totalSteps, stepNum);
                    }

                    if (doImport == false)
                    {

                        abortLogFile(alldataErrors);
                    }
                    updateProgressBar(progress, totalSteps, stepNum, "Importing Excel Data To SQL");
                    stepNum = stepNum + 1;
                }

                //Import into SQL Import tables
                if (doImport)
                {
                    alldataErrors = "";
                    processLog.Append(DateTime.Now.ToShortTimeString() + ":  --- BEGIN DATA IMPORT ---");
                    processLog.Append(Environment.NewLine);

                    string uploaderrmsg = "";
                    uploaderrmsg = temp.UploadRealignImport(Session["userName"].ToString());
                    if (uploaderrmsg != "")
                    {
                        alldataErrors = "Error Encountered Uploading to Realignment Import SQL Tables: " + uploaderrmsg;
                        doImport = false;
                    }
                    else
                    {
                        updateLogFile("Data Uploaded into Realignment Import SQL Table", progress, totalSteps, stepNum);
                    }

                    if (doImport == false)
                    {
                        abortLogFile(alldataErrors);
                    }
                    updateProgressBar(progress, totalSteps, stepNum, "Validating Data");
                    stepNum = stepNum + 1;
                }



                if (doImport == true)
                {
                    string ErrorMsg = "";

                    try
                    {

                        SqlConnection cnn;
                        SqlCommand cmd;
                        cnn = new SqlConnection(strConnString);

                        cnn.Open();
                        cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_DoRealigmentVerify";
                        
                        var returnParam2 = new SqlParameter
                        {
                            ParameterName = "@returnValue",
                            Direction = ParameterDirection.Output,
                            Size = 1000
                        };
                        cmd.CommandTimeout = 10800;
                        cmd.Parameters.Add(returnParam2);
                        cmd.Connection = cnn;

                        cmd.ExecuteNonQuery();
                        ErrorMsg = (string)cmd.Parameters["@returnValue"].Value;


                        cmd.Dispose();
                        cnn.Close();

                    }
                    catch (Exception ex)
                    {
                        ErrorMsg = ex.ToString();
                    }
                    finally
                    {

                    }
                    if (ErrorMsg != "")
                    {
                        doImport = false;
                        abortLogFile(ErrorMsg);
                    }
                }

                if (doImport == true)
                {
                    //execute the stp sp_DoAccountRealignments
                    string ErrorMsg = "";
                    DateTime EffectiveStartDate;
                    //GET DATE for EffectiveStartDate
                    EffectiveStartDate = getQtrBeginDate();
                    try
                    {

                        SqlConnection cnn;
                        SqlCommand cmd;
                        cnn = new SqlConnection(strConnString);

                        cnn.Open();
                        cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_DoAccountRealigments";
                        cmd.Parameters.Add("@EffectiveStartDate", SqlDbType.DateTime).Value = EffectiveStartDate;
                        cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = Session["userName"].ToString();
                        var returnParam2 = new SqlParameter
                        {
                            ParameterName = "@Error",
                            Direction = ParameterDirection.Output,
                            Size = 1000
                        };
                        cmd.CommandTimeout = 10800;
                        cmd.Parameters.Add(returnParam2);
                        cmd.Connection = cnn;

                        cmd.ExecuteNonQuery();
                        ErrorMsg = (string)cmd.Parameters["@Error"].Value;


                        cmd.Dispose();
                        cnn.Close();

                    }
                    catch (Exception ex)
                    {
                        ErrorMsg = ex.ToString();
                    }
                    finally
                    {

                    }
                    if (ErrorMsg != "")
                    {
                        doImport = false;
                        abortLogFile(ErrorMsg);
                    }
                }


                if (doImport)
                {
                    processLog.Append(DateTime.Now.ToShortTimeString() + ":  Data Successfully Imported to the Temp Tables.");
                    processLog.Append(Environment.NewLine);
                    processLog.Append(DateTime.Now.ToShortTimeString() + ":  Realignment Data Import Process Completed!.");
                    processLog.Append(Environment.NewLine);
                    //scheduledProcessLog.Status = clsScheduledProcessLog.STATUS_COMPLETED;
                    //clsScheduledProcessLog.UpdateScheduledProcessLog(scheduledProcessLog);
                    updateProgressBar(progress, totalSteps, stepNum, "Process Completed");
                    stepNum = stepNum + 1;

                    //Ready for step 2 - import after data review
                    //Response.Redirect("ReviewImport.aspx?revtype=" + revenuetype);
                    String msg = "Account Realignment Import Successfully Completed";
                    lblSuccess.Text = msg;
                    pnlSuccess.Visible = true;

                }

                completeProcessLog("AccountRealignmentImport", ImportFile.FileName);

                btnSubmit.Visible = false;
               

            }
            catch (Exception ex)
            {
                lblDanger.Text = ex.Message;
                pnlDanger.Visible = true;

            }

        }

        protected DateTime getQtrBeginDate()
        {

            string selYear = cbxYear.SelectedItem.Text;
            string selQtr = cbxQtr.SelectedItem.Text;
            int iyear = Convert.ToInt16(selYear);
            int iqtr = Convert.ToInt16(selQtr.Substring(selQtr.Length - 1));
            SqlConnection cnn;
            //String strConnString = ConfigurationManager.ConnectionStrings["PISalesIncentivesSQLConnectionString"].ConnectionString;
            cnn = new SqlConnection(strConnString);

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            DateTime qtrBeginDate = DateTime.Now;
            DateTime qtrEndDate = DateTime.Now;
            try
            {
                cmd = new SqlCommand("sp_GetQtrDates", cnn);
                cmd.Parameters.Add("@qtrNumber", SqlDbType.Float).Value = iqtr;
                cmd.Parameters.Add("@year", SqlDbType.Int).Value = iyear;
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);


            }
            catch (Exception ex)
            {
                string errMsg = ex.Message.ToString();
            }
            finally
            {
                cnn.Close();
            }


            qtrBeginDate = Convert.ToDateTime(dt.Rows[0]["qtrBeginDate"]);
            return qtrBeginDate;

        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {

            try
            {
                
                
            }
            catch (Exception ex)
            {
                lblDanger.Text = ex.Message;
                pnlDanger.Visible = true;

            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected bool CheckPrior(int asofQtr, int asofYear)
        {
            int numRows = 0;
            bool hasPrior = true;
            SqlConnection cnn;
            SqlCommand cmd;
            cnn = new SqlConnection(strConnString);

            cnn.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_getCACbyQTRCounts";
            cmd.Connection = cnn;
            cmd.Parameters.Add("@asofQtr", SqlDbType.Int).Value = asofQtr;
            cmd.Parameters.Add("@asofYear", SqlDbType.Int).Value = asofYear;

            SqlDataReader rdr;
            try
            {

                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        numRows = (Int32)rdr["RecCount"];
                    }
                    rdr.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();

                //cnn.Close();
            }
            if (numRows == 0)
            {
                lblDanger.Text = "No backup found for QTR" + asofQtr.ToString() + " Year " + asofYear.ToString() + ". Qtr End Backup must be completed first.";
                lblDanger.Visible = true;
                pnlDanger.Visible = true;
                hasPrior = false;
                btnSubmit.Visible = false;
                //btnCancel.Visible = true;
                //btnContinue.Visible = false;
            }

            return hasPrior;
        }


     protected void startProcessLog()
    {
        //START PROCESSING LOG
        //if (scheduledProcessLog == null)
        //    scheduledProcessLog = CreateProcessingLog();

        if (processLog == null)
            processLog = new StringBuilder();
    }

    private void abortLogFile(string updateText)
    {

        //saveError(updateText, "", scheduledProcessLog.ScheduledProcessLogId);
        doAbortLogFile(updateText);
        //btnCancel.Visible = true;
        btnSubmit.Visible = false;

    }

    private void doAbortLogFile(string updateText)
    {
        pnlDanger.Visible = true;
        lblDanger.Text = "--No Realignment Data Imported--<br><p/>" + updateText + "<p/>Please Fix Errors and Restart the Import.";
        processLog.Append(DateTime.Now.ToShortTimeString() + ": " + updateText);
        processLog.Append(Environment.NewLine);
        //AbortProcessingLog();
    }

    private void updateLogFile(string updateText, RadProgressContext progress, int totalSteps, int stepNum)
    {

        processLog.Append(DateTime.Now.ToShortTimeString() + ":  " + updateText);
        processLog.Append(Environment.NewLine);
        updateProgressBar(progress, totalSteps, stepNum);
        stepNum = stepNum + 1;
    }

    private void updateProgressBar(RadProgressContext progress, int Total, int stepNum, string desc)
    {

        double dTotal = Total * 5;
        double dStepNum = stepNum * 5;

        progress.PrimaryTotal = Total;
        progress.PrimaryValue = stepNum;
        progress.PrimaryPercent = dStepNum;

        //fill in values during the data parsing
        progress.SecondaryTotal = 100;
        progress.SecondaryValue = 1;
        progress.SecondaryPercent = 1;

        progress.CurrentOperationText = "Step " + stepNum.ToString();

    }

    protected void saveError(string ErrorMsg, string fileName, int scheduledProcessLogId)
    {
        clsPI_ApplicationException appException = new clsPI_ApplicationException();

        appException.ApplicationId = clsPI_Application.GetApplication(ConfigurationManager.AppSettings["appName"]).ApplicationId;
        appException.ExceptionCode = "";
        appException.ExceptionMessage = ErrorMsg;
        appException.FileName = fileName;
        appException.ErrorRow = 0;
        appException.ErrorColumn = 0;
        appException.ScheduledProcessLogId = scheduledProcessLogId;
        appException.UpdatedBy = Environment.UserName;
        appException.UpdatedOn = DateTime.Now;

        clsPI_ApplicationException.InsertException(appException);
    }

    protected void completeProcessLog(string logname, string filename)
    {
        
        if (processLog != null)
        {
            writeLogToText(logname, filename);
            processLog.Clear();
        }
    }

    private void writeLogToText(string type, string filename)
    {
        try
        {
            String tempPath = Server.MapPath("~/temp/");
            string logFile = tempPath + type + filename + "_importLog";
            logFile = logFile.Replace(".", "");
            logFile = logFile + ".txt";

            //get rid of HTML coding for the log
            string logString = processLog.ToString();
            logString = logString.Replace("<br>", " ");
            logString = logString.Replace("<p/>", " - ");
            System.IO.File.WriteAllText(@logFile, logString);
        }
        catch (Exception ex)
        {


        }

    }
    private string createTempFile(FileUpload ManualFile)
    {
        String tempPath = Server.MapPath("~/temp/");
        try
        {


            //first check if temp dir exists
            if (!Directory.Exists(tempPath))
            {
                System.IO.Directory.CreateDirectory(tempPath);
            }
            string tempFilePath = tempPath + ManualFile.FileName;

            ManualFile.PostedFile.SaveAs(tempFilePath);
            return tempFilePath;
        }
        catch (Exception ex)
        {
            pnlWarning.Visible = true;
            lblWarning.Text = "Cannot create temp file in " + tempPath;
            return "";
        }
    }

    
    private void writeLogToText()
    {

        String tempPath = Server.MapPath("~/temp/");

        string logFile = tempPath + ImportFile.FileName + "_importLog";

        logFile = logFile.Replace(".", "");
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


    protected void btnCancelImport_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void btnRestartImport_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("AccountRealignment.aspx");

    }

    protected void btnImport_Click(object sender, System.EventArgs e)
    {
        doImporttoProd();
    }


    protected void btnView_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("Default.aspx");

    }

    protected void showImport()
    {
        launchWindow();
        btnImport.Visible = true;
        btnSubmit.Visible = false;

    }

    protected void launchWindow()
    {
        String script = "<script type=\"text/javascript\">window.open('ViewAlignmentData');</script>";
        ClientScript.RegisterStartupScript(GetType(), "openWindow", script);
    }

    protected void doImporttoProd()
    {

        //pnlWarning.Visible = false;
        //pnlSuccess.Visible = false;
        //pnlDanger.Visible = false;

        //temp = new ClsMarginTrend();
        //string ErrorMsg = temp.ImportMarginTrend();

        //if (ErrorMsg != "")
        //{
        //    lblDanger.Text = ErrorMsg;
        //    pnlDanger.Visible = true;
        //    pnlInfo.Visible = false;

        //}
        //else
        //{
        //    string msg;
        //    msg = "<b>Import Successfully Completed</b>";
        //    lblSuccess.Text = msg;
        //    pnlSuccess.Visible = true;
        //    pnlInfo.Visible = false;
        //    btnImport.Visible = false;
        //    btnView.Visible = true;
        //    btnCancelImport.Visible = false;
        //}

    }

    }
}