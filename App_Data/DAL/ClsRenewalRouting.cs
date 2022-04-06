using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsRenewalRouting
    {

        public int idContractRenewalRouting { get; set; }
        public string RoutingName { get; set; }
        public string RoutingEmail { get; set; }
        public string LoginID { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? ActiveFlag { get; set; }

        public static List<ClsRenewalRouting> GetRenewalRouteList()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            List<ClsRenewalRouting> oRouteList = (from data in prepumaContext.GetTable<tblContractRenewalRouting>()
                                              where data.ActiveFlag == true || data.ActiveFlag == null
                                              orderby data.RoutingName

                                                 select new ClsRenewalRouting
                                              {
                                                  idContractRenewalRouting = data.idContractRenewalRouting,
                                                  RoutingName = data.RoutingName,
                                                  RoutingEmail = data.RoutingEmail,
                                                  LoginID = data.LoginID,
                                                  UpdatedBy = data.UpdatedBy,
                                                  UpdatedOn = data.UpdatedOn,
                                                  CreatedBy = data.CreatedBy,
                                                  CreatedOn = data.CreatedOn,
                                                  ActiveFlag = (bool)data.ActiveFlag

                                              }).ToList<ClsRenewalRouting>();

            return oRouteList;
        }

        public static List<ClsRenewalRouting> GetRenewalRouteListWInactive()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            List<ClsRenewalRouting> oRouteList = (from data in prepumaContext.GetTable<tblContractRenewalRouting>()
                                              orderby data.RoutingName

                                                 select new ClsRenewalRouting
                                              {
                                                  idContractRenewalRouting = data.idContractRenewalRouting,
                                                  RoutingName = data.RoutingName,
                                                  RoutingEmail = data.RoutingEmail,
                                                  LoginID = data.LoginID,
                                                  UpdatedBy = data.UpdatedBy,
                                                  UpdatedOn = data.UpdatedOn,
                                                  CreatedBy = data.CreatedBy,
                                                  CreatedOn = data.CreatedOn,
                                                  ActiveFlag = (bool)data.ActiveFlag

                                              }).ToList<ClsRenewalRouting>();

            return oRouteList;
        }

        public static ClsRenewalRouting GetRenewalRoute(string routingName)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsRenewalRouting oRouteList = (from data in prepumaContext.GetTable<tblContractRenewalRouting>()
                                                  where data.RoutingName == routingName
     
                                                  select new ClsRenewalRouting
                                                  {
                                                      idContractRenewalRouting = data.idContractRenewalRouting,
                                                      RoutingName = data.RoutingName,
                                                      RoutingEmail = data.RoutingEmail,
                                                      LoginID = data.LoginID,
                                                      UpdatedBy = data.UpdatedBy,
                                                      UpdatedOn = data.UpdatedOn,
                                                      CreatedBy = data.CreatedBy,
                                                      CreatedOn = data.CreatedOn,
                                                      ActiveFlag = (bool)data.ActiveFlag

                                                  }).SingleOrDefault();

            return oRouteList;
        }


        public string InsertRenewalRoute(ClsRenewalRouting data)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            try
            {

                tblContractRenewalRouting oNewRow = new tblContractRenewalRouting()
                {
                    idContractRenewalRouting = (Int32)data.idContractRenewalRouting,
                    RoutingName = data.RoutingName,
                    RoutingEmail = data.RoutingEmail,
                    LoginID = data.LoginID,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = (DateTime?)data.CreatedOn,
                    //UpdatedBy = data.UpdatedBy,
                    //UpdatedOn = (DateTime?)data.UpdatedOn,
                    ActiveFlag = data.ActiveFlag
                };



                prepumaContext.GetTable<tblContractRenewalRouting>().InsertOnSubmit(oNewRow);
                // Submit the changes to the database. 
                prepumaContext.SubmitChanges();


            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }

        public string UpdateRenewalRoute(ClsRenewalRouting data)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            try
            {

                if (data.idContractRenewalRouting > 0)
                {
                    // Query the database for the row to be updated. 
                    var query =
                        from qdata in prepumaContext.GetTable<tblContractRenewalRouting>()
                        where qdata.idContractRenewalRouting == data.idContractRenewalRouting
                        select qdata;

                    // Execute the query, and change the column values 
                    // you want to change. 
                    foreach (tblContractRenewalRouting updRow in query)
                    {

                        updRow.RoutingName = data.RoutingName;
                        updRow.RoutingEmail = data.RoutingEmail;
                        updRow.LoginID = data.LoginID;
                        updRow.ActiveFlag = data.ActiveFlag;
                        updRow.idContractRenewalRouting = data.idContractRenewalRouting;
                        updRow.UpdatedBy = data.UpdatedBy;
                        updRow.UpdatedOn = data.UpdatedOn;

                    }

                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();


                }
                else
                {
                    errMsg = "There is No Contract Renwewal Routing Email with ID = " + "'" + data.idContractRenewalRouting + "'";
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