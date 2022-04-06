using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ContractClassification
    {
        public Double? CCID { get; set; }
        public string ContractNumber { get; set; }
        public string ContractName { get; set; }
        public string Region { get; set; }
        public string Territory { get; set; }
        public string SalesType { get; set; }
        public string ISP { get; set; }
        public string RelationshipName { get; set; }
        public Boolean ISPpaid { get; set; }
        public Boolean LostBiz { get; set; }
        public Single? EstAnnualRev { get; set; }
        public Single? EstMarginPct { get; set; }
        public string SecondaryNAICS { get; set; }
        public string Source { get; set; }
        public string Updatedby { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Boolean? ActiveFlag { get; set; }
        //public string Currency { get; set; }
        

        public static ContractClassification GetData(string sDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ContractClassification oContract = (from cc in prepumaContext.GetTable<tblContractClassification>()
                                                where cc.ContractNumber == sDatabaseKey

                                                select new ContractClassification
                                                {
                                                    CCID = Convert.ToDouble(cc.CCID),
                                                    ContractNumber = cc.ContractNumber,
                                                    ContractName = cc.Contract_Name,
                                                    Region = cc.Region,
                                                    Territory = cc.Territory,
                                                    SalesType = cc.SalesType,
                                                    ISP = cc.ISP,
                                                    RelationshipName = cc.RelationshipName,
                                                    ISPpaid = Convert.ToBoolean(cc.ISPpaid),
                                                    LostBiz = Convert.ToBoolean(cc.LostBiz),
                                                    EstAnnualRev = cc.EstAnnualRev,
                                                    EstMarginPct = cc.EstMarginPct,
                                                    SecondaryNAICS = cc.SecondaryNAICS,
                                                    Source = cc.Source,
                                                    
                                                    Updatedby = cc.Updatedby,
                                                    UpdatedOn = cc.UpdatedOn,
                                                    Createdby = cc.Createdby,
                                                    CreatedOn = cc.CreatedOn,
                                                    ActiveFlag = cc.ActiveFlag
                                                    //Currency = cc.Currency
                                                }).SingleOrDefault<ContractClassification>();



            return oContract;
        }

        public static List<ContractClassification> GetRelationshipList()
        {

            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ContractClassification> rnames = (from data in prepumaContext.GetTable<tblContractClassification>()
                          orderby data.RelationshipName
                      select new ContractClassification
                          {
                              
                              RelationshipName = data.RelationshipName,

                          }).Distinct().ToList<ContractClassification>();

            return rnames;
           
        }

       
    }

    
}