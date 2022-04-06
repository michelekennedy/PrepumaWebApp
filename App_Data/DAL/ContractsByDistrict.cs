using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ContractsByDistrict
    {
        public string district { get; set; }
        public int contractCount { get; set; }
        public int accountCount { get; set; }

        public List<ContractsByDistrict> getContractsByDistrict()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            List<ContractsByDistrict> getData =(from data in prepumaContext.vw_contractsByDistricts
                                                 select new ContractsByDistrict
                                                 {
                                                     district = data.District,
                                                     contractCount = (int)data.NumberContracts,
                                                     accountCount = (int)data.NumberAccounts

                                                 }).ToList<ContractsByDistrict>();
            
            return getData;
        }
    }
}