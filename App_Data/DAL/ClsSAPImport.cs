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
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Web;
using Telerik.Web.UI;
using Telerik.Web.UI.Upload;

namespace PrepumaWebApp
{
    public class ClsSAPImport
    {

        public string ContractNumber { get; set; }
        public string Customer { get; set; }
        public double SAPAirDim { get; set; }
        public double SAPGroundDim { get; set; }
        public string FreightFSCPct { get; set; }
        public bool PuroIncMirror { get; set; }
        public string CourierFSCDisc { get; set; }
        public double ResiFee { get; set; }
        public double DG01 { get; set; }
        public string DG01type { get; set; }
        public double DG02 { get; set; }
        public string DG02type { get; set; }
        public double DG03 { get; set; }
        public string DG03type { get; set; }
        public double DG04 { get; set; }
        public string DG04type { get; set; }
        public double DG05 { get; set; }
        public string DG05type { get; set; }
        public double SH05 { get; set; }
        public double SH06 { get; set; }
        public double SH07 { get; set; }
        public double SH08 { get; set; }
        public double SH09 { get; set; }
        public double SH10 { get; set; }
        public bool BeyondOriginDisc { get; set; }
        public bool BeyondDestDisc { get; set; }

        String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
        int MaxErrors = 10;

         public ClsSAPImport()
        {
            listSAPImport = new List<ClsSAPImport>();
        }

         private List<ClsSAPImport> listSAPImport;
         public List<ClsSAPImport> ListSAPImport
        {
            get { return listSAPImport; }
            set { listSAPImport = value; }
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

                     ClsSAPImport importRecord;
                     int ivar;
                     int cCnt=1;
                     int rCnt=1;

                                             
                     DataRow row;
                     //skip first row which contains headers
                     for (rCnt = 1; rCnt < dtSheet.Rows.Count; rCnt++)
                     {

                        row=dtSheet.Rows[rCnt];
                        //put values into a new Import object
                        importRecord = new ClsSAPImport();
                        string importdata;
                        bool isNumeric;
                        double finput;
                        int errorCount = 0;
                        bool boolvalue = false;
                        for (cCnt = 0; cCnt < dtSheet.Columns.Count; cCnt++)
                        {
                            importdata = (string)row[cCnt].ToString();
                            switch (cCnt)
                            {
                                case 0:
                                    importRecord.ContractNumber = importdata;
                                    break;
                                case 1:
                                    importRecord.Customer = importdata;
                                    break;
                                case 2:
                                    if (importdata != "")
                                     {
                                         importdata = importdata.Replace("$","");
                                         isNumeric = double.TryParse(importdata, out finput);
                                         if (!isNumeric)
                                         {
                                             error = error + "<br>Non-numeric SAPAirDim Value found: " + importdata;
                                             errorCount = errorCount + 1;
                                         }
                                         else
                                         {
                                             importRecord.SAPAirDim = (double)(finput);
                                         }
                                     }                                    
                                     break;
                                case 3:
                                     if (importdata != "")
                                     {
                                         importdata = importdata.Replace("$", "");
                                         isNumeric = double.TryParse(importdata, out finput);
                                         if (!isNumeric)
                                         {
                                             error = error + "<br>Non-numeric SAPGroundDim Value found: " + importdata;
                                             errorCount = errorCount + 1;
                                         }
                                         else
                                         {
                                             importRecord.SAPGroundDim = (double)(finput);
                                         }
                                     }                                    
                                     break;
                                case 4:
                                    importRecord.FreightFSCPct = importdata;
                                    break;
                                case 5:
                                    if (importdata == "No")
                                        boolvalue = false;
                                    else
                                        boolvalue = true;
                                    importRecord.PuroIncMirror = boolvalue;
                                    break;
                                case 6:
                                    importRecord.CourierFSCDisc = importdata;
                                    break;
                                case 7:
                                     if (importdata != "")
                                     {
                                         importdata = importdata.Replace("$", "");
                                         isNumeric = double.TryParse(importdata, out finput);
                                         if (!isNumeric)
                                         {
                                             error = error + "<br>Non-numeric ResiFee Value found: " + importdata;
                                             errorCount = errorCount + 1;
                                         }
                                         else
                                         {
                                             importRecord.ResiFee = (double)(finput);
                                         }
                                     }                                    
                                     break;
                                case 8:
                                   if (importdata != "")
                                     {
                                         importdata = importdata.Replace("$", "");
                                         isNumeric = double.TryParse(importdata, out finput);
                                         if (!isNumeric)
                                         {
                                             error = error + "<br>Non-numeric DG01 Value found: " + importdata;
                                             errorCount = errorCount + 1;
                                         }
                                         else
                                         {
                                             importRecord.DG01 = (double)(finput);
                                         }
                                     }                                    
                                     break;
                                case 9:
                                    importRecord.DG01type = importdata;
                                    break;
                                case 10:
                                    if (importdata != "")
                                     {
                                         importdata = importdata.Replace("$", "");
                                         isNumeric = double.TryParse(importdata, out finput);
                                         if (!isNumeric)
                                         {
                                             error = error + "<br>Non-numeric DG02 Value found: " + importdata;
                                             errorCount = errorCount + 1;
                                         }
                                         else
                                         {
                                             importRecord.DG02 = (double)(finput);
                                         }
                                     }                                    
                                     break;
                                case 11:
                                    importRecord.DG02type = importdata;
                                    break;
                                case 12:
                                    if (importdata != "")
                                     {
                                         importdata = importdata.Replace("$", "");
                                         isNumeric = double.TryParse(importdata, out finput);
                                         if (!isNumeric)
                                         {
                                             error = error + "<br>Non-numeric DG03 Value found: " + importdata;
                                             errorCount = errorCount + 1;
                                         }
                                         else
                                         {
                                             importRecord.DG03 = (double)(finput);
                                         }
                                     }                                    
                                     break;
                                case 13:
                                     importRecord.DG03type = importdata;
                                     break;
                                case 14:
                                     if (importdata != "")
                                     {
                                         importdata = importdata.Replace("$", "");
                                         isNumeric = double.TryParse(importdata, out finput);
                                         if (!isNumeric)
                                         {
                                             error = error + "<br>Non-numeric DG04 Value found: " + importdata;
                                             errorCount = errorCount + 1;
                                         }
                                         else
                                         {
                                             importRecord.DG04 = (double)(finput);
                                         }
                                     }                                    
                                     break;
                                case 15:
                                    importRecord.DG04type = importdata;
                                    break;
                                case 16:
                                      if (importdata != "")
                                     {
                                         importdata = importdata.Replace("$", "");
                                         isNumeric = double.TryParse(importdata, out finput);
                                         if (!isNumeric)
                                         {
                                             error = error + "<br>Non-numeric DG05 Value found: " + importdata;
                                             errorCount = errorCount + 1;
                                         }
                                         else
                                         {
                                             importRecord.DG05 = (double)(finput);
                                         }
                                     }                                    
                                     break;
                                case 17:
                                    importRecord.DG05type = importdata;
                                    break;
                                case 18:
                                     if (importdata != "")
                                     {
                                         importdata = importdata.Replace("$", "");
                                         isNumeric = double.TryParse(importdata, out finput);
                                         if (!isNumeric)
                                         {
                                             error = error + "<br>Non-numeric SH05 Value found: " + importdata;
                                             errorCount = errorCount + 1;
                                         }
                                         else
                                         {
                                             importRecord.SH05 = (double)(finput);
                                         }
                                     }                                    
                                     break;
                                case 19:
                                     if (importdata != "")
                                     {
                                         importdata = importdata.Replace("$", "");
                                         isNumeric = double.TryParse(importdata, out finput);
                                         if (!isNumeric)
                                         {
                                             error = error + "<br>Non-numeric SH06 Value found: " + importdata;
                                             errorCount = errorCount + 1;
                                         }
                                         else
                                         {
                                             importRecord.SH06 = (double)(finput);
                                         }
                                     }
                                     break;
                                case 20:
                                     if (importdata != "")
                                     {
                                         importdata = importdata.Replace("$", "");
                                         isNumeric = double.TryParse(importdata, out finput);
                                         if (!isNumeric)
                                         {
                                             error = error + "<br>Non-numeric SH07 Value found: " + importdata;
                                             errorCount = errorCount + 1;
                                         }
                                         else
                                         {
                                             importRecord.SH07 = (double)(finput);
                                         }
                                     }
                                     break;
                                case 21:
                                     if (importdata != "")
                                     {
                                         importdata = importdata.Replace("$", "");
                                         isNumeric = double.TryParse(importdata, out finput);
                                         if (!isNumeric)
                                         {
                                             error = error + "<br>Non-numeric SH08 Value found: " + importdata;
                                             errorCount = errorCount + 1;
                                         }
                                         else
                                         {
                                             importRecord.SH08 = (double)(finput);
                                         }
                                     }
                                     break;
                                case 22:
                                     if (importdata != "")
                                     {
                                         importdata = importdata.Replace("$", "");
                                         isNumeric = double.TryParse(importdata, out finput);
                                         if (!isNumeric)
                                         {
                                             error = error + "<br>Non-numeric SH09 Value found: " + importdata;
                                             errorCount = errorCount + 1;
                                         }
                                         else
                                         {
                                             importRecord.SH09 = (double)(finput);
                                         }
                                     }
                                     break;
                                case 23:
                                     if (importdata != "")
                                     {
                                         importdata = importdata.Replace("$", "");
                                         isNumeric = double.TryParse(importdata, out finput);
                                         if (!isNumeric)
                                         {
                                             error = error + "<br>Non-numeric SH10 Value found: " + importdata;
                                             errorCount = errorCount + 1;
                                         }
                                         else
                                         {
                                             importRecord.SH10 = (double)(finput);
                                         }
                                     }
                                     break;
                                case 24:
                                    if (importdata == "Yes")
                                        boolvalue = true;
                                    else
                                        boolvalue = false;
                                    importRecord.BeyondOriginDisc = boolvalue;
                                    break;
                                case 25:
                                     if (importdata == "Yes")
                                        boolvalue = true;
                                    else
                                        boolvalue = false;
                                    importRecord.BeyondDestDisc = boolvalue;
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

                        //if errorCount is too high, stop processing
                        if (errorCount >= MaxErrors)
                        {
                            error = error + "<br>--Max Error Count of " + MaxErrors.ToString() + " Reached--";
                            rCnt = dtSheet.Rows.Count;
                        }
                        
                        listSAPImport.Add(importRecord);                                               
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


        public void UploadRows(string createdBy, DateTime createdOn)
        {



            foreach (var Item in listSAPImport)
            {
                //do insert
                if (!DoSQLInsert(Item, createdBy, createdOn))
                {

                }

            }



            



        }

        public Boolean DoSQLInsert(ClsSAPImport Item,string createdBy, DateTime createdOn)
        {

            bool validResult = true;

            try
            {
                

                SqlConnection cnn;
                SqlCommand cmd;
                cnn = new SqlConnection(strConnString);

                cnn.Open();
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_InsertIntoSAPImportTable";

                cmd.Parameters.Add("@ContractNumber", SqlDbType.VarChar).Value = Item.ContractNumber;
                cmd.Parameters.Add("@Customer", SqlDbType.VarChar).Value = Item.Customer;
                cmd.Parameters.Add("@SAPAirDim", SqlDbType.Money).Value = Item.SAPAirDim;
                cmd.Parameters.Add("@SAPGroundDim", SqlDbType.Money).Value = Item.SAPGroundDim;
                cmd.Parameters.Add("@FreightFSCPct", SqlDbType.VarChar).Value = Item.FreightFSCPct;               
                cmd.Parameters.Add("@PuroIncMirror", SqlDbType.Bit).Value = Item.PuroIncMirror;
                cmd.Parameters.Add("@CourierFSCDisc", SqlDbType.VarChar).Value = Item.CourierFSCDisc;
                cmd.Parameters.Add("@ResiFee", SqlDbType.Money).Value = Item.ResiFee;
                cmd.Parameters.Add("@DG01", SqlDbType.Money).Value = Item.DG01;
                cmd.Parameters.Add("@DG01type", SqlDbType.VarChar).Value = Item.DG01type;
                cmd.Parameters.Add("@DG02", SqlDbType.Money).Value = Item.DG02;
                cmd.Parameters.Add("@DG02type", SqlDbType.VarChar).Value = Item.DG02type;
                cmd.Parameters.Add("@DG03", SqlDbType.Money).Value = Item.DG03;
                cmd.Parameters.Add("@DG03type", SqlDbType.VarChar).Value = Item.DG03type;
                cmd.Parameters.Add("@DG04", SqlDbType.Money).Value = Item.DG04;
                cmd.Parameters.Add("@DG04type", SqlDbType.VarChar).Value = Item.DG04type;
                cmd.Parameters.Add("@DG05", SqlDbType.Money).Value = Item.DG05;
                cmd.Parameters.Add("@DG05type", SqlDbType.VarChar).Value = Item.DG05type;
                cmd.Parameters.Add("@SH05", SqlDbType.Money).Value = Item.SH05;
                cmd.Parameters.Add("@SH06", SqlDbType.Money).Value = Item.SH06;
                cmd.Parameters.Add("@SH07", SqlDbType.Money).Value = Item.SH07;
                cmd.Parameters.Add("@SH08", SqlDbType.Money).Value = Item.SH08;
                cmd.Parameters.Add("@SH09", SqlDbType.Money).Value = Item.SH09;
                cmd.Parameters.Add("@SH10", SqlDbType.Money).Value = Item.SH10;
                cmd.Parameters.Add("@BeyondOriginDisc", SqlDbType.Bit).Value = Item.BeyondOriginDisc;
                cmd.Parameters.Add("@BeyondDestDisc", SqlDbType.Bit).Value = Item.BeyondDestDisc;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = createdBy;
                cmd.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = createdOn;
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
                   
                    //string validFormat = validateFormat(dtSheet);
                    //if (validFormat != "")
                    //{

                    //    error = "Error Found on File Format: " + validFormat;
                    //}


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
                    str = str.Replace(".", "");
                    str = str.Replace("#", "");
                    str = str.ToLower();
                   switch (cCnt)
                   {
                       case 1:
                           if (!String.Equals(str,"contractinsap"))
                           {
                               invalidColumn = invalidColumn + " Contract # in SAP";
                           }
                           break;
                       case 2:
                           if (!String.Equals(str, "customer"))
                           {
                               invalidColumn = invalidColumn + " customer";
                           }
                           break;
                       case 3:
                           if (!String.Equals(str, "sapairdim"))
                           {
                               invalidColumn = invalidColumn + " SAP Air Dim";
                           }
                           break;
                       case 4:
                           if (!String.Equals(str, "sapgrounddim"))
                           {
                               invalidColumn = invalidColumn + " SAP Ground Dim";
                           }
                           break;
                       //case 5:
                       //    if (!String.Equals(str, "fueldiscfreight"))
                       //    {
                       //        invalidColumn = invalidColumn + " Fuel Disc. Freight";
                       //    }
                       //    break;
                       //case 6:
                       //    if (!String.Equals(str, "fueldisccourier"))
                       //    {
                       //        invalidColumn = invalidColumn + " Fuel Disc. Courier";
                       //    }
                       //    break;
                       case 5:
                           if (!String.Equals(str, "resifee"))
                           {
                               invalidColumn = invalidColumn + " Resi Fee";
                           }
                           break;
                       case 6:
                           if (!String.Equals(str, "dg01"))
                           {
                               invalidColumn = invalidColumn + " DG01";
                           }
                           break;
                       case 7:
                           if (!String.Equals(str, "dg02"))
                           {
                               invalidColumn = invalidColumn + " DG02";
                           }
                           break;
                       case 8:
                           if (!String.Equals(str, "dg03"))
                           {
                               invalidColumn = invalidColumn + " DG03";
                           }
                           break;                      
                       case 9:
                           if (!String.Equals(str, "dg04"))
                           {
                               invalidColumn = invalidColumn + " DG04";
                           }
                           break;
                       case 10:
                           if (!String.Equals(str, "dg05"))
                           {
                               invalidColumn = invalidColumn + " DG05";
                           }
                           break;
                       case 11:
                           if (!String.Equals(str, "sh05"))
                           {
                               invalidColumn = invalidColumn + " SH05";
                           }
                           break;
                       case 12:
                           if (!String.Equals(str, "sh06"))
                           {
                               invalidColumn = invalidColumn + " SH06";
                           }
                           break;                      
                       case 13:
                           if (!String.Equals(str, "sh07"))
                           {
                               invalidColumn = invalidColumn + " SH07";
                           }
                           break;
                       case 14:
                           if (!String.Equals(str, "sh08"))
                           {
                               invalidColumn = invalidColumn + " SH08";
                           }
                           break;
                       case 15:
                           if (!String.Equals(str, "sh09"))
                           {
                               invalidColumn = invalidColumn + " SH09";
                           }
                           break;
                       case 16:
                           if (!String.Equals(str, "sh10"))
                           {
                               invalidColumn = invalidColumn + " SH10";
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
                     cmd.CommandText = "sp_ClearSAPImportTable";
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