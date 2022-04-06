//using Prepuma.App_Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using Telerik.Web.UI;
using Telerik.Web.UI.Upload;
using System.Configuration;
using System.Globalization;
using PI_Application;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrepumaWebApp
{
    public class ClsAlignmentImport
    {

        public string oldRelationshipName { get; set; }
        public string oldContractNumber { get; set; }
        public string oldContractName { get; set; }
        public string oldAcctnbr { get; set; }
        public string oldDistrict { get; set; }
        public string oldBranch { get; set; }
        public string oldLocalSRID { get; set; }
        public string oldLocalRepName { get; set; }
        public string oldStrategicSRID { get; set; }
        public string oldStrRepName { get; set; }
        public string oldSalesType { get; set; }
        public string oldBusinessType { get; set; }
        public string newContractNumber { get; set; }
        public string newContractName { get; set; }
        public string newAcctnbr { get; set; }
        public string newDistrict { get; set; }
        public string newBranch { get; set; }
        public string newLocalSRID { get; set; }
        public string newLocalRepName { get; set; }
        public string newStrategicSRID { get; set; }
        public string newStrRepName { get; set; }
        public string newSalesType { get; set; }
        public string newBusinessType { get; set; }

        public string newControlBranch { get; set; }
        public string newLeadSalesSRID { get; set; }
        public string newProductType { get; set; }

        //String strConnString = ConfigurationManager.ConnectionStrings["RevenueSQLConnectionString"].ConnectionString;
        String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
        int MaxErrors = 10;

      
        private List<ClsAlignmentImport> listAlignmentImport;

        public List<ClsAlignmentImport> ListAlignmentImport
        {
            get { return listAlignmentImport; }
            set { listAlignmentImport = value; }
        }

        public ClsAlignmentImport()
        {
            listAlignmentImport = new List<ClsAlignmentImport>();
        }


        public string ParseExcel(string filePath, string fileName, RadProgressContext progress)
        {
            string error = "";
            int errorCount = 0;

            try
            {
                string sSheetName = null;
                string sConnection = null;
                System.Data.DataTable dtTablesList = default(System.Data.DataTable);
                OleDbCommand oleExcelCommand = default(OleDbCommand);
                OleDbDataReader oleExcelReader = default(OleDbDataReader);
                OleDbConnection oleExcelConnection = default(OleDbConnection);
                System.Data.DataTable table = new System.Data.DataTable();


                sConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";

                oleExcelConnection = new OleDbConnection(sConnection);
                oleExcelConnection.Open();

                dtTablesList = oleExcelConnection.GetSchema("Tables");

                if (dtTablesList.Rows.Count > 0)
                {
                    sSheetName = dtTablesList.Rows[0]["TABLE_NAME"].ToString();
                }

                dtTablesList.Clear();
                dtTablesList.Dispose();


                if (!string.IsNullOrEmpty(sSheetName))
                {
                    oleExcelCommand = oleExcelConnection.CreateCommand();
                    oleExcelCommand.CommandText = "Select * From [" + sSheetName + "]";
                    oleExcelCommand.CommandType = CommandType.Text;
                    oleExcelReader = oleExcelCommand.ExecuteReader();

                    System.Data.DataTable dtSheet = new System.Data.DataTable();
                    dtSheet.Load(oleExcelReader);
                    ClsAlignmentImport importRecord;
                    int cCnt = 1;
                    int rCnt = 1;
                    string inputString;
                    DataRow row;
                    //Int32 inputVal;
                    //float finput;
                    //DateTime dateValue;
                    //skip first row containing header columns
                    for (rCnt = 1; rCnt < dtSheet.Rows.Count; rCnt++)
                    {
                        //put values into a new ManualImportCollection object
                        row = dtSheet.Rows[rCnt];


                        //rows.count is not always accurate, need to check if end of data
                        if (endOfRows(row, dtSheet.Columns.Count))
                        {
                            rCnt = dtSheet.Rows.Count;
                        }
                        else
                        {
                            importRecord = new ClsAlignmentImport();
                            //bool isNumeric;
                            int numColumns = 15;
                            for (cCnt = 0; cCnt < numColumns; cCnt++)
                            {
                                inputString = row[cCnt].ToString();
                                inputString = inputString.Trim();
                                switch (cCnt)
                                {
                                   
                                    case 0:
                                        //Old Contract #
                                        importRecord.oldContractNumber = inputString;
                                        if (String.IsNullOrEmpty(inputString))
                                        {
                                            error = error + "<br>Old Contract Number is Empty";
                                            errorCount = errorCount + 1;
                                        }
                                        break;
                                   case 1:
                                        //Old Acct Number
                                        importRecord.oldAcctnbr = inputString;
                                        if (String.IsNullOrEmpty(inputString))
                                        {
                                            error = error + "<br>Old Account Number is Empty";
                                            errorCount = errorCount + 1;
                                        }
                                        break;                                  
                                   case 2:
                                        //Old Local SRID
                                        importRecord.oldLocalSRID = inputString;
                                        if (String.IsNullOrEmpty(inputString))
                                        {
                                            error = error + "<br>Old Local SRID is Empty";
                                            errorCount = errorCount + 1;
                                        }
                                        break;
                                   case 3:
                                        //Old Strategic SRID
                                        importRecord.oldStrategicSRID = inputString;
                                        if (String.IsNullOrEmpty(inputString))
                                        {
                                            error = error + "<br>Old Strategic SRID is Empty";
                                            errorCount = errorCount + 1;
                                        }
                                        break;

                                    //New Values
                                    case 4:
                                        //New Contract #
                                        importRecord.newContractNumber = inputString;
                                        if (String.IsNullOrEmpty(inputString))
                                        {
                                            error = error + "<br>New Contract Number is Empty";
                                            errorCount = errorCount + 1;
                                        }
                                        break;
                                    case 5:
                                        //New Contract Name   
                                        importRecord.newContractName = inputString;
                                        if (String.IsNullOrEmpty(inputString))
                                        {
                                            error = error + "<br>New Contract Name is Empty";
                                            errorCount = errorCount + 1;
                                        }
                                        break;
                                    case 6:
                                        //New Acct Number
                                        importRecord.newAcctnbr = inputString;
                                        if (String.IsNullOrEmpty(inputString))
                                        {
                                            error = error + "<br>New Account Number is Empty";
                                            errorCount = errorCount + 1;
                                        }
                                        break;
                                     case 7:
                                        //New Branch
                                        importRecord.newBranch = inputString;
                                        if (String.IsNullOrEmpty(inputString))
                                        {
                                            error = error + "<br>New Branch is Empty";
                                            errorCount = errorCount + 1;
                                        }
                                        break;
                                    case 8:
                                        //New Local SRID
                                        importRecord.newLocalSRID = inputString;
                                        if (String.IsNullOrEmpty(inputString))
                                        {
                                            error = error + "<br>New Local SRID is Empty";
                                            errorCount = errorCount + 1;
                                        }
                                        break;
                                    case 9:
                                        //New Strategic SRID
                                        importRecord.newStrategicSRID = inputString;
                                        if (String.IsNullOrEmpty(inputString))
                                        {
                                            error = error + "<br>New Strategic SRID is Empty";
                                            errorCount = errorCount + 1;
                                        }
                                        break;
                                    case 10:
                                        //New Sales Type
                                        importRecord.newSalesType = inputString;
                                        if (String.IsNullOrEmpty(inputString))
                                        {
                                            error = error + "<br>New Sales Type is Empty";
                                            errorCount = errorCount + 1;
                                        }
                                        break;
                                    case 11:
                                        //New Business Type
                                        importRecord.newBusinessType = inputString;
                                        if (String.IsNullOrEmpty(inputString))
                                        {
                                            error = error + "<br>New Business Type is Empty";
                                            errorCount = errorCount + 1;
                                        }
                                        break;
                                    case 12:
                                        //New Control Branch
                                        importRecord.newControlBranch = inputString;
                                        if (String.IsNullOrEmpty(inputString))
                                        {
                                            error = error + "<br>New Control Branch is Empty";
                                            errorCount = errorCount + 1;
                                        }
                                        break;
                                    case 13:
                                        //New Lead Sales SRID
                                        importRecord.newLeadSalesSRID = inputString;
                                        if (String.IsNullOrEmpty(inputString))
                                        {
                                            error = error + "<br>New Lead Sales SRID is Empty";
                                            errorCount = errorCount + 1;
                                        }
                                        break;
                                    case 14:
                                        //Product Type                                       
                                        if (String.IsNullOrEmpty(inputString))
                                        {
                                            inputString = "";
                                        }
                                        importRecord.newProductType = inputString;
                                        break;
                                }

                            }
                            //Update secondard progress bar
                            int rnd = (int)(dtSheet.Rows.Count / 100);
                            if (rnd <= 0)
                                rnd = 1;
                            int mod = rCnt % rnd;
                            if (mod == 0)
                            {
                                updateProgressBar(progress, dtSheet.Rows.Count, rCnt);
                            }
                            //if errorCount is too high, stop processing
                            if (errorCount >= MaxErrors)
                            {
                                error = error + "<br>--Max Error Count of " + MaxErrors.ToString() + " Reached--";
                                rCnt = dtSheet.Rows.Count;
                            }
                            listAlignmentImport.Add(importRecord);
                        }
                    }

                    oleExcelReader.Close();
                }
                oleExcelConnection.Close();

            }

            catch (Exception ex)
            {
                error = error + " " + ex.Message;
                //containError = true;
                Console.WriteLine(ex.Message);
            }

            finally
            {
                //clsScheduledProcessLogFile scheduledProcessLogFile = new clsScheduledProcessLogFile(scheduledProcessLogId, fileName, "xls");
                //clsScheduledProcessLogFile.UpdateScheduledProcessLogFile(scheduledProcessLogFile);
            }

            return error;
        }

        private bool endOfRows(DataRow row, int numColumns)
        {
            //if all columns are blank, then this is the last row
            bool endFlag = true;
            for (int cCnt = 0; cCnt < numColumns; cCnt++)
            {
                if (row[cCnt].ToString().Trim() != "")
                    endFlag = false;
            }
            return endFlag;
        }


        public Boolean ClearImportSQLTable()
        {
            //Clear the existing rows from the import table
            bool validResult = true;
            try
            {

                SqlConnection cnn;
                SqlCommand cmd;
                cnn = new SqlConnection(strConnString);

                cnn.Open();
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ClearRealignmentImportTable";
                cmd.Connection = cnn;
                cmd.CommandTimeout = 10800;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cnn.Close();


            }
            catch (Exception ex)
            {
                validResult = false;
            }

            return validResult;
        }

        public string UploadRealignImport(string userName)
        {

            string errmsg = "";

            //get all items in listClsInvoices and import into SQL table 
            foreach (var item in listAlignmentImport)
            {
                //do insert
                errmsg = DoSQLInsert(item, userName);
                if (errmsg != "")
                {
                    return errmsg;
                }

            }

            return "";

        }



        public string DoSQLInsert(ClsAlignmentImport item, string userName)
        {

            // bool validResult = true;

            try
            {

                SqlConnection cnn;
                SqlCommand cmd;
                cnn = new SqlConnection(strConnString);

                cnn.Open();
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_InsertIntoAlignmentImportTable";

                cmd.Parameters.Add("@oldContractNumber", SqlDbType.VarChar).Value = item.oldContractNumber;
                cmd.Parameters.Add("@oldAcctnbr", SqlDbType.VarChar).Value = item.oldAcctnbr;
                cmd.Parameters.Add("@oldLocalSRID", SqlDbType.VarChar).Value = item.oldLocalSRID;
                cmd.Parameters.Add("@oldStrategicSRID", SqlDbType.VarChar).Value = item.oldStrategicSRID;
                
                cmd.Parameters.Add("@newContractNumber", SqlDbType.VarChar).Value = item.newContractNumber;
                cmd.Parameters.Add("@newContractName", SqlDbType.VarChar).Value = item.newContractName;
                cmd.Parameters.Add("@newAcctnbr", SqlDbType.VarChar).Value = item.newAcctnbr;
                cmd.Parameters.Add("@newBranch", SqlDbType.VarChar).Value = item.newBranch;
                cmd.Parameters.Add("@newLocalSRID", SqlDbType.VarChar).Value = item.newLocalSRID;
                cmd.Parameters.Add("@newStrategicSRID", SqlDbType.VarChar).Value = item.newStrategicSRID;
                cmd.Parameters.Add("@newSalesType", SqlDbType.VarChar).Value = item.newSalesType;
                cmd.Parameters.Add("@newBusinessType", SqlDbType.VarChar).Value = item.newBusinessType;
                cmd.Parameters.Add("@newControlBranch", SqlDbType.VarChar).Value = item.newControlBranch;
                cmd.Parameters.Add("@newLeadSalesSRID", SqlDbType.VarChar).Value = item.newLeadSalesSRID;
                cmd.Parameters.Add("@newProductType", SqlDbType.VarChar).Value = item.newProductType;

                cmd.CommandTimeout = 10800;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                cnn.Close();

            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            finally
            {

            }
            return "";
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




            progress.TimeEstimated = (dTotal - dStepNum) * 100;

        }

    }
}