using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace PrepumaWebApp.App_Data.DAL
{
    
    public class ClsFileUpload
    {
        public int idContractRenewalUpload { get; set; }
        public int? idContractRenewal { get; set; }
        public DateTime? UploadDate { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool ActiveFlag { get; set; }

        public ClsFileUpload GetFileUpload(int ID)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsFileUpload oNote = (from data in prepumaContext.GetTable<tblContractRenewalUpload>()
                                   where data.idContractRenewal == ID
                                   where data.ActiveFlag != false
                                   select new ClsFileUpload
                                   {
                                       idContractRenewalUpload = data.idContractRenewalUpload,
                                       idContractRenewal = data.idContractRenewal,
                                       Description = data.Description,
                                       UploadDate = data.UploadDate,
                                       FilePath = data.FilePath,
                                       CreatedBy = data.CreatedBy,
                                       UpdatedBy = data.UpdatedBy,
                                       UpdatedOn = (DateTime?)data.UpdatedOn,
                                       ActiveFlag = (bool)data.ActiveFlag
                                   }).FirstOrDefault();
            return oNote;
        }



        public string InsertFileUpload(ClsFileUpload data, out Int32 newID)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            newID = -1;

            try
            {

                tblContractRenewalUpload oNewRow = new tblContractRenewalUpload()
                {
                    //idFileUpload = (Int32)data.idFileUpload,
                    idContractRenewal = (Int32)data.idContractRenewal,
                    UploadDate = data.UploadDate,
                    Description = data.Description,
                    FilePath = data.FilePath,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = (DateTime?)data.CreatedOn,

                    ActiveFlag = data.ActiveFlag
                };



                prepumaContext.GetTable<tblContractRenewalUpload>().InsertOnSubmit(oNewRow);
                // Submit the changes to the database. 
                prepumaContext.SubmitChanges();
                newID = oNewRow.idContractRenewalUpload;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }

        public string UpdateFileUpload(ClsFileUpload data)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            try
            {

                if (data.idContractRenewalUpload > 0)
                {
                    // Query the database for the row to be updated. 
                    var query =
                        from qdata in prepumaContext.GetTable<tblContractRenewalUpload>()
                        where qdata.idContractRenewalUpload == data.idContractRenewalUpload
                        select qdata;

                    // Execute the query, and change the column values 
                    // you want to change. 
                    foreach (tblContractRenewalUpload updRow in query)
                    {

                        updRow.UploadDate = data.UploadDate;
                        updRow.Description = data.Description;
                        updRow.FilePath = data.FilePath;
                        updRow.UpdatedBy = data.UpdatedBy;
                        updRow.UpdatedOn = data.UpdatedOn;
                        updRow.ActiveFlag = data.ActiveFlag;
                    }

                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();


                }
                else
                {
                    errMsg = "There is No File Upload with ID = " + "'" + data.idContractRenewalUpload + "'";
                }


            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }

        //DELETE  (set ActiveFlag to false)
        public string deActivateFileUpload(Int32 ID)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            try
            {

                if (ID > 0)
                {
                    // Query the database for the row to be updated. 
                    var query =
                        from qdata in prepumaContext.GetTable<tblContractRenewalUpload>()
                        where qdata.idContractRenewalUpload == ID
                        select qdata;

                    // Execute the query, and change the column values 
                    // you want to change. 
                    foreach (tblContractRenewalUpload updRow in query)
                    {

                        updRow.ActiveFlag = false;

                    }

                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();


                }
                else
                {
                    errMsg = "There is No UploadFile File Upload with id = " + "'" + ID + "'";
                }


            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }


     public List<ClsFileUpload> GetFileList(int DRid)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsFileUpload> oFiles = (from data in prepumaContext.GetTable<tblContractRenewalUpload>()
                                          where data.idContractRenewal == DRid
                                    where data.ActiveFlag != false
                                    orderby data.UploadDate descending
                                          orderby data.idContractRenewal descending
                                    select new ClsFileUpload
                                    {
                                        idContractRenewalUpload = data.idContractRenewalUpload,
                                        idContractRenewal = data.idContractRenewal,
                                        UploadDate = data.UploadDate,
                                        Description = data.Description,
                                        FilePath = data.FilePath,
                                        CreatedBy = data.CreatedBy,
                                        CreatedOn = (DateTime)data.CreatedOn,
                                        UpdatedBy = data.UpdatedBy,
                                        UpdatedOn = data.UpdatedOn,
                                        ActiveFlag = (bool)data.ActiveFlag

                                    }).ToList<ClsFileUpload>();
            return oFiles;
        }
    }
}