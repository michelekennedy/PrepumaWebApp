using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsCity
    {
        public int Id { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string stateName { get; set; }
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
        public string upDatedBy { get; set; }
        public DateTime? upDatedOn { get; set; }

        public static List<ClsCity> GetCities()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsCity> oCity = (from data in prepumaContext.GetTable<tblCity>()
                                       orderby data.State
                                       select new ClsCity
                                       {
                                           city = data.City,
                                           state = data.State,
                                           stateName = data.State_name,
                                           Id = data.ID

                                       }).ToList<ClsCity>();
            return oCity;
        }

        public static List<ClsCity> GetCitiesInState(string vState)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsCity> oCity = (from data in prepumaContext.GetTable<tblCity>()
                                   where data.State == vState
                                   select new ClsCity
                                   {
                                       city = data.City,
                                       state = data.State,
                                       stateName = data.State_name,
                                       Id = data.ID

                                   }).ToList<ClsCity>();
            return oCity;
        }
    }
}