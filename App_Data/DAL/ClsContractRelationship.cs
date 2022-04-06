using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsContractRelationship
    {

        public string RelationshipName { get; set; }
        public float ContractID { get; set; }
        public string ContractName { get; set; }
        public string ContractNumber { get; set; }
        public float ClientID { get; set; }
        public string ContractNameNumber { get; set; }
        public string Territory { get; set; }
        public string Region { get; set; }
        public DateTime? FirstShipDate { get; set; }

        public List<ClsContractRelationship> GetContractsByRelationship(string Relationship)
        {

            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();


            List<ClsContractRelationship> listObjects = (from data in prepumaContext.GetTable<vw_ContractRelationship>()
                                                         where data.RelationshipName == Relationship

                                                         select new ClsContractRelationship
                                                         {
                                                             RelationshipName = data.RelationshipName,
                                                             ContractID = Convert.ToSingle(data.ContractID),
                                                             ContractName = data.ContractName,
                                                             ContractNumber = data.ContractNumber,
                                                             ClientID = Convert.ToSingle(data.ClientID),
                                                             ContractNameNumber = data.ContractNumber + " - " + data.ContractName,
                                                             Territory = data.Territory,
                                                             Region = data.Region

                                                         }).ToList();
            return listObjects;
        }

        public DateTime? GetFirstShipDate(string Relationship)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsContractRelationship fsd = (from dt in prepumaContext.GetTable<vw_FirstShipDate>()
                           where dt.RelationshipName == Relationship
                           select new ClsContractRelationship
                                     { FirstShipDate = dt.FirstShipDate }).FirstOrDefault();

                   
            return fsd.FirstShipDate;
        }
    }
}