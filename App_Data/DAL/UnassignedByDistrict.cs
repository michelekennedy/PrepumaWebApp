using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class UnassignedByDistrict
    {
        public string district { get; set; }
        public int? unAssignedCount { get; set; }

        public List<UnassignedByDistrict> getUnassignedByDistrict()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            List<UnassignedByDistrict> getData = (from data in prepumaContext.vw_unassignedByDistricts
                                                  select new UnassignedByDistrict
                                                 {
                                                     district = data.District,
                                                     unAssignedCount = (int)data.Unassigned,

                                                 }).ToList<UnassignedByDistrict>();

            return getData;
        }

        
    }
}