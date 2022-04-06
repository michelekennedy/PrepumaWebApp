using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class SalesRepsByDistrict
    {
        public string district { get; set; }
        public decimal salesRepsCount { get; set; }
        public bool IsExploded { get; set; }

        public List<SalesRepsByDistrict> getSalesRepsByDistrict()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            List<SalesRepsByDistrict> getData = (from data in prepumaContext.vw_salesRepByDistricts
                                                 select new SalesRepsByDistrict
                                                 {
                                                     district = data.District,
                                                     salesRepsCount= (int)data.NumberSRID

                                                 }).ToList<SalesRepsByDistrict>();
            return getData;
        }
    }
} 