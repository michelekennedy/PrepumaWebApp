using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
   

    public class ClsRenewalType
    {
        public int idContractRenewalType { get; set; }
        public string ContractRenewalType { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? ActiveFlag { get; set; }

        public static List<ClsRenewalType> GetRenewalTypeList()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            List<ClsRenewalType> oTypeList = (from data in prepumaContext.GetTable<tblContractRenewalType>()
                                                  where data.ActiveFlag == true || data.ActiveFlag == null
                                                  orderby data.ContractRenewalType

                                                  select new ClsRenewalType
                                                  {
                                                      idContractRenewalType = data.idContractRenewalType,
                                                      ContractRenewalType = data.ContractRenewalType,
                                                      UpdatedBy = data.UpdatedBy,
                                                      UpdatedOn = data.UpdatedOn,
                                                      CreatedBy = data.CreatedBy,
                                                      CreatedOn = data.CreatedOn,
                                                      ActiveFlag = (bool)data.ActiveFlag

                                                  }).ToList<ClsRenewalType>();

            return oTypeList;
        }

        public static List<ClsRenewalType> GetRenewalTypeListWInactive()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            List<ClsRenewalType> oTypeList = (from data in prepumaContext.GetTable<tblContractRenewalType>()
                                              orderby data.ContractRenewalType

                                              select new ClsRenewalType
                                              {
                                                  idContractRenewalType = data.idContractRenewalType,
                                                  ContractRenewalType = data.ContractRenewalType,
                                                  UpdatedBy = data.UpdatedBy,
                                                  UpdatedOn = data.UpdatedOn,
                                                  CreatedBy = data.CreatedBy,
                                                  CreatedOn = data.CreatedOn,
                                                  ActiveFlag = (bool)data.ActiveFlag

                                              }).ToList<ClsRenewalType>();

            return oTypeList;
        }


        public string InsertRenewalType(ClsRenewalType data)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            try
            {

                tblContractRenewalType oNewRow = new tblContractRenewalType()
                {
                    idContractRenewalType = (Int32)data.idContractRenewalType,
                    ContractRenewalType = data.ContractRenewalType,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = (DateTime?)data.CreatedOn,
                    //UpdatedBy = data.UpdatedBy,
                    //UpdatedOn = (DateTime?)data.UpdatedOn,
                    ActiveFlag = data.ActiveFlag
                };



                prepumaContext.GetTable<tblContractRenewalType>().InsertOnSubmit(oNewRow);
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

        public string UpdateRenewalType(ClsRenewalType data)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            try
            {

                if (data.idContractRenewalType > 0)
                {
                    // Query the database for the row to be updated. 
                    var query =
                        from qdata in prepumaContext.GetTable<tblContractRenewalType>()
                        where qdata.idContractRenewalType == data.idContractRenewalType
                        select qdata;

                    // Execute the query, and change the column values 
                    // you want to change. 
                    foreach (tblContractRenewalType updRow in query)
                    {

                        updRow.ContractRenewalType = data.ContractRenewalType;
                        updRow.ActiveFlag = data.ActiveFlag;
                        updRow.idContractRenewalType = data.idContractRenewalType;
                        updRow.UpdatedBy = data.UpdatedBy;
                        updRow.UpdatedOn = data.UpdatedOn;

                    }

                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();


                }
                else
                {
                    errMsg = "There is No Contract Renwewal Type with ID = " + "'" + data.idContractRenewalType + "'";
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