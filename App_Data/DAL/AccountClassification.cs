using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class AccountClassification
    {
        public string ContractNumber { get; set; }
        public string Acctnbr { get; set; }
        public string StrategicSRID { get; set; }
        public string LocalSRID { get; set; }
        public string BusinessType { get; set; }
        public string ProductType { get; set; }
        public string Region { get; set; }
        public string ShippingLocation { get; set; }
        public Single? SharePercent { get; set; }
        public string Updatedby { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public double? gatewayID { get; set; }
        public string RTNSID { get; set; }
        public string MATHANCPPID { get; set; }
        public string CDCPLBID { get; set; }

        public string Createdby { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Boolean? ActiveFlag { get; set; }
       

        public static AccountClassification GetData(string sDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            AccountClassification oContract = (from cac in prepumaContext.GetTable<tblContractAccountClassification>()
                                                where cac.Acctnbr == sDatabaseKey

                                               select new AccountClassification
                                                {
                                                    ContractNumber = cac.ContractNumber,
                                                    Acctnbr = cac.Acctnbr,
                                                    StrategicSRID = cac.StrategicSRID,
                                                    LocalSRID = cac.LocalSRID,
                                                    BusinessType = cac.BusinessType,
                                                    ProductType = cac.ProductType,
                                                    Region = cac.Region,
                                                    ShippingLocation = cac.ShippingLocation,
                                                    SharePercent = cac.SharePercent,
                                                    gatewayID = cac.gatewayID,
                                                    RTNSID = cac.RTNSID,
                                                    MATHANCPPID = cac.MATHANDCPPID,
                                                    CDCPLBID = cac.CDCPLBID,
                                                    Updatedby = cac.Updatedby, 
                                                    UpdatedOn = cac.UpdatedOn,
                                                    Createdby = cac.Createdby,
                                                    CreatedOn = cac.CreatedOn,
                                                    ActiveFlag = cac.ActiveFlag
                                                }).SingleOrDefault<AccountClassification>();



            return oContract;
        }
    }
}