using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using PI_Application;
using PI_BusinessClasses;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Web;
using Telerik.Web.UI;
using Telerik.Web.UI.Upload;

namespace PrepumaWebApp.App_Data.DAL
{
     class AccountImportCollection  

     {

         String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
        

         public AccountImportCollection()
        {
            listAccountImport = new List<AccountImport>();
        }

         private List<AccountImport> listAccountImport;
         public List<AccountImport> ListAccountImport
        {
            get { return listAccountImport; }
            set { listAccountImport = value; }
        }

         private int contractRowsAdded;
         public int ContractRowsAdded
         {
             get { return contractRowsAdded;  }
             set { contractRowsAdded = value;  }
         }

         public string ParseExcel(string filePath, string fileName, RadProgressContext progress)
         {
             string error = "";
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

                     AccountImport importRecord;
                     int ivar;
                     int cCnt=1;
                     int rCnt=1;

                                             
                     DataRow row;
                     //skip first row which contains headers
                     for (rCnt = 1; rCnt < dtSheet.Rows.Count; rCnt++)
                     {

                        row=dtSheet.Rows[rCnt];
                        //put values into a new AccountImport object
                        importRecord = new AccountImport();
                        string importdata;
                        bool isNumeric;
                        for (cCnt = 0; cCnt < dtSheet.Columns.Count; cCnt++)
                        {
                            importdata = (string)row[cCnt].ToString();
                            switch (cCnt)
                            {
                                case 0:
                                    importRecord.Account = importdata;
                                    break;
                                case 1:
                                    importRecord.Customer = importdata;
                                    break;
                                case 2:
                                    importRecord.Branch = importdata;
                                    break;
                                case 3:
                                    isNumeric = Int32.TryParse(importdata, out ivar);
                                    if (!isNumeric)
                                    {
                                        //need to reject if Cost Center is not numeric
                                        return "Error Encountered on Cost Center " + importdata + " not numeric";
                                    }
                                    importRecord.CostCenter = ivar;
                                    break;
                                case 4:
                                    importRecord.BusinessType = importdata;
                                    break;
                                case 5:
                                    importRecord.StrategicSRID = importdata;
                                    break;
                                case 6:
                                    importRecord.LocalSRID = importdata;
                                    break;
                            }


                        }
                        //Update secondard progress bar
                        int rnd = (int)(dtSheet.Rows.Count / 100);
                        if (rnd<=0)
                            rnd=1;
                        int mod = rCnt % rnd;
                        if (mod == 0)
                        {
                            updateProgressBar(progress, dtSheet.Rows.Count, rCnt);
                        }

                        listAccountImport.Add(importRecord);                                               
                    }                   
           
                     oleExcelReader.Close();
                 }
                 oleExcelConnection.Close();

             }
             catch (Exception ex)
             {
                 error = ex.Message;
                
             }
            
             return error;
         }

         
       

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;

            }
            finally
            {
                GC.Collect();
            }
        }


        public string UploadAccounts()
        {


            //get all items in listAccountImport and import into SQL table 
            string accountsWithProblems = "";
            foreach (var acctItem in listAccountImport) {
                //do insert
                if (!DoSQLInsert(acctItem)) {
                    //return acctItem.Account;
                    accountsWithProblems = accountsWithProblems + " " + acctItem.Account;
                }
               
            }

            return accountsWithProblems;

        }

        public Boolean DoSQLInsert(AccountImport acctItem)
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
                cmd.CommandText = "sp_InsertIntoAccountImportTable";
                
                cmd.Parameters.Add("@Account",SqlDbType.VarChar).Value = acctItem.Account;
                cmd.Parameters.Add("@Customer", SqlDbType.VarChar).Value = acctItem.Customer;
                cmd.Parameters.Add("@Branch", SqlDbType.VarChar).Value = acctItem.Branch;
                cmd.Parameters.Add("@CostCenter", SqlDbType.Int).Value = acctItem.CostCenter;
                cmd.Parameters.Add("@BusinessType", SqlDbType.VarChar).Value = acctItem.BusinessType;
                cmd.Parameters.Add("@StrategicSRID", SqlDbType.VarChar).Value = acctItem.StrategicSRID;
                cmd.Parameters.Add("@LocalSRID", SqlDbType.VarChar).Value = acctItem.LocalSRID;
                
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                cnn.Close();
                              
            }
            catch (Exception ex)
            {
                validResult = false;
            }
            finally
            {

            }
            return validResult;
        }
         
         public List<string> ValidateAccounts()
        {
             //Return accts that do not have a correstponding row in the Accounts table
             List<string> invalidAccts;
             invalidAccts = new List<string>();

            try
            {

                SqlConnection cnn;
                SqlCommand cmd;
                cnn = new SqlConnection(strConnString);

                cnn.Open();
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetMissingAccountImportAccts";
                cmd.Connection = cnn;
                
                //process results
                 SqlDataReader rdr;

                    try
                    {
                        
                        rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                invalidAccts.Add((string)rdr["Account"]);
                            }
                        }
                                                
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        cmd.Dispose();
                        cnn.Close();
                    }
                          
                

            }
            catch (Exception ex)
            {
                //validResult = false;
            }
            finally
            {

            }
            return invalidAccts;

        }

         public List<string> ValidateClassAccounts()
         {
             //Return accounts that are already inthe Contract Account Classification table
             List<string> invalidAccts;
             invalidAccts = new List<string>();

             try
             {

                 SqlConnection cnn;
                 SqlCommand cmd;
                 cnn = new SqlConnection(strConnString);

                 cnn.Open();
                 cmd = new SqlCommand();
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandText = "sp_GetExistingAccountImportAccts";
                 cmd.Connection = cnn;

                 //process results
                 SqlDataReader rdr;

                 try
                 {

                     rdr = cmd.ExecuteReader();
                     if (rdr.HasRows)
                     {
                         while (rdr.Read())
                         {
                             invalidAccts.Add((string)rdr["Account"]);
                         }
                     }

                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 finally
                 {
                     cmd.Dispose();
                     cnn.Close();
                 }



             }
             catch (Exception ex)
             {
                 //validResult = false;
             }
             finally
             {

             }
             return invalidAccts;

         }

        
         public int UpdateClassification(string Updatedby)
         {
             
             int rowsAffected=0;

             //Run stored prodedure to update ContractClassifications 
             try
             {

                 SqlConnection cnn;
                 SqlCommand cmd;
                 cnn = new SqlConnection(strConnString);

                 cnn.Open();
                 cmd = new SqlCommand();
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandText = "sp_ContractClassificationImportAccounts";
                 cmd.Parameters.Add("@Updatedby", SqlDbType.NVarChar).Value = Updatedby;
                 cmd.Connection = cnn;
                 //cmd.ExecuteNonQuery();

                 //
                 //process results
                 SqlDataReader rdr;

                 try
                 {

                     rdr = cmd.ExecuteReader();

                     if (rdr.HasRows)
                     {
                         while (rdr.Read())
                         {

                             rowsAffected=(int)rdr[0];
                         }
                     }

                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 finally
                 {
                     cmd.Dispose();
                     cnn.Close();
                 }
                 //

                 cmd.Dispose();
                 cnn.Close();

             }
             catch (Exception ex)
             {
                 rowsAffected = -1;
             }
             finally
             {

             }


             return rowsAffected;

         }

         public int UpdateClassificationAccounts(string Updatedby)
         {
            
             int rowsAffected = 0;

             //Run stored prodedure to update ContractClassificationAccounts
             try
             {

                 SqlConnection cnn;
                 SqlCommand cmd;
                 cnn = new SqlConnection(strConnString);

                 cnn.Open();
                 cmd = new SqlCommand();
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandText = "sp_ContractAccountClassificationImportAccounts";
                 cmd.Parameters.Add("@Updatedby", SqlDbType.NVarChar).Value = Updatedby;
                 cmd.Connection = cnn;
                 //cmd.ExecuteNonQuery();

                 SqlDataReader rdr;
                 try
                 {

                     rdr = cmd.ExecuteReader();

                     if (rdr.HasRows)
                     {
                         while (rdr.Read())
                         {

                             rowsAffected = (int)rdr[0];
                         }
                     }

                 }
                 catch (Exception ex)
                 {
                     throw ex;
                 }
                 finally
                 {
                     cmd.Dispose();
                     cnn.Close();
                 }

                 cmd.Dispose();
                 cnn.Close();

             }
             catch (Exception ex)
             {
                 rowsAffected=-1;
             }
             finally
             {

             }

             //return isValid;
             return rowsAffected;

         }
         

        
        public string ValidateExcelFile(string filePath, string fileName)
        {
            string error = "";
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
                   
                    string validFormat = validateFormat(dtSheet);
                    if (validFormat != "")
                    {

                        error = "Error Found on File Format: " + validFormat;
                    }


                    oleExcelReader.Close();
                }
                oleExcelConnection.Close();

            }
            catch (Exception ex)
            {
                error = ex.Message;

            }

            return error;
        }

        private string validateFormat(System.Data.DataTable dtSheet)
        {
            //Check that the file has the right header columns
            string str;
            int cCnt = 1;
            string invalidColumn = "";

            int requiredNumColumns = dtSheet.Columns.Count;

            DataRow row = dtSheet.Rows[0];
            foreach (DataColumn col in dtSheet.Columns)
            {
             
                    str = row[col].ToString();
                    str=str.ToLower();
                    str = str.Replace(" ", "");
                   switch (cCnt)
                   {
                       case 1:
                           if (!String.Equals(str,"account"))
                           {
                               invalidColumn = invalidColumn + " account";
                           }
                           break;
                       case 2:
                           if (!String.Equals(str, "customer"))
                           {
                               invalidColumn = invalidColumn + " customer";
                           }
                           break;
                       case 3:
                           if (!String.Equals(str, "branch"))
                           {
                               invalidColumn = invalidColumn + " branch";
                           }
                           break;
                       case 4:
                           if (!String.Equals(str, "costcenter"))
                           {
                               invalidColumn = invalidColumn + " cost center";
                           }
                           break;
                       case 5:
                           if (!String.Equals(str, "businesstype"))
                           {
                               invalidColumn = invalidColumn + " business type";
                           }
                           break;
                       case 6:
                           if (!String.Equals(str, "strategicsrid"))
                           {
                               invalidColumn = invalidColumn + " strategic srid";
                           }
                           break;
                       case 7:
                           if (!String.Equals(str, "localsrid"))
                           {
                               invalidColumn = invalidColumn + " local srid";
                           }
                           break;
                   }
                   cCnt = cCnt + 1;
                    
                }

               return invalidColumn;
        }

        

        public string ClearImportSQLTable()
        {
            //Clear the existing rows from the import table
            //bool validResult = true;
           
             try
             {
                 
                 SqlConnection cnn;
                 SqlCommand cmd;
                 cnn = new SqlConnection(strConnString);
                
                     cnn.Open();
                     cmd = new SqlCommand();
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.CommandText = "sp_ClearAccountImportTable";
                     cmd.Connection = cnn;
                     cmd.ExecuteNonQuery();
                     cmd.Dispose();
                     cnn.Close();
                     
                                 
             }
             catch (Exception ex)
             {
                 return ex.ToString();
             }

             return "";
        }

        public bool ValidateAccountExcelFile(string filePath, string fileName)
        {
            string error = "";
            bool validFile = true;


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

                    if (!validateAccountFormat(dtSheet))
                    {
                        validFile = false;
                    }
                 

                    oleExcelReader.Close();
                }
                oleExcelConnection.Close();



            }

            catch (Exception ex)
            {
                error = ex.Message;
            }

            finally
            {
                //clsScheduledProcessLogFile scheduledProcessLogFile = new clsScheduledProcessLogFile(scheduledProcessLogId, fileName, "xls");
                //clsScheduledProcessLogFile.UpdateScheduledProcessLogFile(scheduledProcessLogFile);
            }

            return validFile;
        }


        

        private Boolean validateAccountFormat(System.Data.DataTable dtSheet)
        {
            //Check that the file has the right header columns
            string str;
            bool validFlag = false;

            
            DataRow row = dtSheet.Rows[0];
            foreach (DataColumn col in dtSheet.Columns)
            {
                 str = row[col].ToString();
                str = str.ToLower();
                if (str == "account" | str == "accounts")
                {
                    validFlag = true;
                }

            }

            return validFlag;
        }


        public List<string> ParseAccountsFromExcel(string filePath, string fileName, RadProgressContext progress)
        {
            string error = "";
            List<string> invalidAccounts = new List<string>();
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

                    //AccountImport importRecord;
                    //int ivar;
                    int cCnt = 1;
                    int rCnt = 1;

                    //Get Column Number of Account Column
                    int accountColumn = 0;
                    string str;
                    DataRow row = dtSheet.Rows[0];
                    for (cCnt = 0; cCnt < dtSheet.Columns.Count; cCnt++)
                    {                        
                        str = row[cCnt].ToString();
                        str = str.ToLower();

                        if (str == "account" | str == "accounts")
                        {
                            accountColumn = cCnt;
                        }

                       
                    }


                  //skip first row containing header columns
                  string acctstr;
                  for (rCnt = 1; rCnt < dtSheet.Rows.Count; rCnt++)
                  {

                     row = dtSheet.Rows[rCnt];
                     acctstr = row[accountColumn].ToString();

                      ClsAccount account = PrepumaWebApp.App_Data.DAL.ClsAccount.GetAccount(acctstr);
                      if (account == null)
                      {
                          //new account
                          bool alreadyThere = false;
                          foreach (string existingAcct in invalidAccounts)
                          {
                              if (existingAcct == acctstr)
                                  alreadyThere = true;
                          }
                          if (!alreadyThere)
                          {
                              invalidAccounts.Add(acctstr);
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
                 
                    }

                    oleExcelReader.Close();
                }
                oleExcelConnection.Close();


            }

            catch (Exception ex)
            {
                error = ex.Message;

            }


            return invalidAccounts;
        }

        private void updateProgressBar(RadProgressContext progress, int Total, int recNum)
        {
            //base it on 100 
            double dTotal = 100;
            double dRecNum = recNum * 100 / Total;



            progress.SecondaryTotal = dTotal;
            progress.SecondaryValue = recNum;
            progress.SecondaryPercent = dRecNum;

            progress.CurrentOperationText = "Record# " + recNum.ToString() + " out of " + Total.ToString();


            progress.TimeEstimated = (dTotal - dRecNum) * 100;

        }
            
                
    }
}
