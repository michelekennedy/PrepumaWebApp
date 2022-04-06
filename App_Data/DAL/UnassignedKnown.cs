using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class UnassignedKnown
    {
        public string acctnbr;

        public string contractNumber;

        public string contract_Name;

        public string region;

        public string salesType;

        public string strategicSRID;

        public string localSRID;

        public string district { get; set; }

        public int getUnassignedKnownCount()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            int unAssignedKnownCount = (from data in prepumaContext.vw_unassignedKnowns
                                        select new UnassignedKnown
                                        {
                                            district = data.District,
                                            acctnbr = data.Acctnbr,
                                            contractNumber = data.ContractNumber,
                                            contract_Name = data.Contract_Name,
                                            region = data.Region,
                                            salesType = data.SalesType,
                                            strategicSRID = data.StrategicSRID,
                                            localSRID = data.StrategicSRID,

                                        }).ToList<UnassignedKnown>().Count();

            return unAssignedKnownCount;
        }
    }
}