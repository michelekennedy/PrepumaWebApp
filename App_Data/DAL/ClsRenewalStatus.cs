using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;


namespace PrepumaWebApp.App_Data.DAL
{

    public class ClsRenewalStatus
    {
        public int idContractRenewalStatus { get; set; }
        public string ContractRenewalStatus { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? ActiveFlag { get; set; }
        public float OrderNumber { get; set; }

        public static List<ClsRenewalStatus> GetRenewalStatusList()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            List<ClsRenewalStatus> oStatusList = (from data in prepumaContext.GetTable<tblContractRenewalStatus>()
                                                  where data.ActiveFlag == true || data.ActiveFlag == null
                                                  orderby data.OrderNumber

                                         select new ClsRenewalStatus
                                        {
                                            idContractRenewalStatus = data.idContractRenewalStatus,
                                            ContractRenewalStatus = data.ContractRenewalStatus,
                                            UpdatedBy = data.UpdatedBy,
                                            UpdatedOn = data.UpdatedOn,
                                            CreatedBy = data.CreatedBy,
                                            CreatedOn = data.CreatedOn,
                                            ActiveFlag = (bool)data.ActiveFlag,
                                            OrderNumber = (float)data.OrderNumber

                                        }).ToList<ClsRenewalStatus>();

            return oStatusList;
        }

        public static List<ClsRenewalStatus> GetRenewalStatusListWInactive()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            List<ClsRenewalStatus> oStatusList = (from data in prepumaContext.GetTable<tblContractRenewalStatus>()
                                                  orderby data.OrderNumber

                                                  select new ClsRenewalStatus
                                                  {
                                                      idContractRenewalStatus = data.idContractRenewalStatus,
                                                      ContractRenewalStatus = data.ContractRenewalStatus,
                                                      UpdatedBy = data.UpdatedBy,
                                                      UpdatedOn = data.UpdatedOn,
                                                      CreatedBy = data.CreatedBy,
                                                      CreatedOn = data.CreatedOn,
                                                      ActiveFlag = (bool)data.ActiveFlag,
                                                      OrderNumber = (float)data.OrderNumber

                                                  }).ToList<ClsRenewalStatus>();

            return oStatusList;
        }



        public string InsertRenewalStatus(ClsRenewalStatus data)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            try
            {

                tblContractRenewalStatus oNewRow = new tblContractRenewalStatus()
                {
                    idContractRenewalStatus = (Int32)data.idContractRenewalStatus,
                    ContractRenewalStatus = data.ContractRenewalStatus,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = (DateTime?)data.CreatedOn,
                    //UpdatedBy = data.UpdatedBy,
                    //UpdatedOn = (DateTime?)data.UpdatedOn,
                    ActiveFlag = data.ActiveFlag,
                    OrderNumber = (float)data.OrderNumber
                };



                prepumaContext.GetTable<tblContractRenewalStatus>().InsertOnSubmit(oNewRow);
                // Submit the changes to the database. 
                prepumaContext.SubmitChanges();
                //newID = oNewRow.idTaskType;

            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }

        public string UpdateRenewalStatus(ClsRenewalStatus data)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            try
            {

                if (data.idContractRenewalStatus > 0)
                {
                    // Query the database for the row to be updated. 
                    var query =
                        from qdata in prepumaContext.GetTable<tblContractRenewalStatus>()
                        where qdata.idContractRenewalStatus == data.idContractRenewalStatus
                        select qdata;

                    // Execute the query, and change the column values 
                    // you want to change. 
                    foreach (tblContractRenewalStatus updRow in query)
                    {

                        updRow.ContractRenewalStatus = data.ContractRenewalStatus;
                        updRow.ActiveFlag = data.ActiveFlag;
                        updRow.idContractRenewalStatus = data.idContractRenewalStatus;
                        updRow.UpdatedBy = data.UpdatedBy;
                        updRow.UpdatedOn = data.UpdatedOn;
                        updRow.OrderNumber = data.OrderNumber;

                    }

                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();


                }
                else
                {
                    errMsg = "There is No Contract Renwewal Status with ID = " + "'" + data.idContractRenewalStatus + "'";
                }


            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }
    }
}
