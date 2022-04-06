using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsGATEWAY
    {
        public double gatewayID { get; set; }
        public string gateway { get; set; }
        public string id_gateway { get; set; }
        public string Updatedby { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Boolean? ActiveFlag { get; set; }

        public List<ClsGATEWAY> GetGatewayInfo()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsGATEWAY> oGateway= (from data in prepumaContext.GetTable<tblGATEWAY>()
                                         select new ClsGATEWAY
                                         {
                                             gateway = data.gateway,
                                             gatewayID = data.gatewayID,
                                             id_gateway = string.Format("{0}-{1}", data.gatewayID, data.gateway),
                                             Updatedby = data.Updatedby,
                                             UpdatedOn = data.UpdatedOn,
                                             Createdby = data.Createdby,
                                             CreatedOn = data.CreatedOn,
                                             ActiveFlag = (bool)data.ActiveFlag

                                         }).ToList<ClsGATEWAY>();
            return oGateway;
        }

        public static ClsGATEWAY GetGatewayID(double sDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsGATEWAY oGateway = (from data in prepumaContext.GetTable<tblGATEWAY>()
                                        where data.gatewayID == sDatabaseKey

                                   select new ClsGATEWAY
                                        {
                                            gatewayID = data.gatewayID,
                                            gateway = data.gateway,
                                            Updatedby = data.Updatedby,
                                            UpdatedOn = data.UpdatedOn,
                                            Createdby = data.Createdby,
                                            CreatedOn = data.CreatedOn,
                                            ActiveFlag = (bool)data.ActiveFlag
                                            
                                        }).SingleOrDefault<ClsGATEWAY>();

            return oGateway;
        }

        public static ClsGATEWAY GetGateway(string sDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsGATEWAY oGateway = (from data in prepumaContext.GetTable<tblGATEWAY>()
                                   where data.gateway == sDatabaseKey

                                   select new ClsGATEWAY
                                   {
                                       gatewayID = data.gatewayID,
                                       gateway = data.gateway,
                                       Updatedby = data.Updatedby,
                                       UpdatedOn = data.UpdatedOn,
                                       Createdby = data.Createdby,
                                       CreatedOn = data.CreatedOn,
                                       ActiveFlag = (bool)data.ActiveFlag

                                   }).SingleOrDefault<ClsGATEWAY>();

            return oGateway;
        }

        public static string InsertGateway(ClsGATEWAY oNewData)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            try
            {
                ClsGATEWAY gateWay= new ClsGATEWAY();
                ClsGATEWAY oGateway = ClsGATEWAY.GetGateway(oNewData.gateway);
                if (oGateway == null)
                {
                    List<ClsGATEWAY> gateWayInfo = gateWay.GetGatewayInfo();
                    int val = Convert.ToInt16(gateWayInfo.Max(x => x.gatewayID));

                    tblGATEWAY oNewRow = new tblGATEWAY()
                    {
                        gatewayID = val + 1,
                        gateway = oNewData.gateway,
                        Updatedby = oNewData.Updatedby,
                        Createdby = oNewData.Createdby,
                        ActiveFlag = oNewData.ActiveFlag
                    };

                    // Add the new object to the sales Rep collection.
                    prepumaContext.GetTable<tblGATEWAY>().InsertOnSubmit(oNewRow);
                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();
                }
                else
                {
                    errMsg = "Already Exists Business Type with " + "'" + oNewData.gateway + "'";
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