using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsRelCrossDocMATRegion
    {
        public int id { get; set; }
        public string cdcplbId { get; set; }
        public string region { get; set; }
        public string mathancppId { get; set; }

        public static string getCrossDockByRegion(string sRegion)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            string regionCrossDockId = string.Empty;
            ClsRelCrossDocMATRegion ocrdkRegion = (from data in prepumaContext.GetTable<tblRelCrossDocMATRegion>()
                                                   where data.REGION == sRegion
                                                   select new ClsRelCrossDocMATRegion
                                               {
                                                   cdcplbId = data.CDCPLBID

                                               }).SingleOrDefault<ClsRelCrossDocMATRegion>();

            return regionCrossDockId = ocrdkRegion.cdcplbId;
        }

        public static string getMatHnlgByRegion(string sRegion)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            string regionMathanCppId = string.Empty;
            ClsRelCrossDocMATRegion ocrdkRegion = (from data in prepumaContext.GetTable<tblRelCrossDocMATRegion>()
                                                   where data.REGION == sRegion
                                                   select new ClsRelCrossDocMATRegion
                                                   {
                                                       mathancppId = data.MATHANDCPPID

                                                   }).SingleOrDefault<ClsRelCrossDocMATRegion>();

            return regionMathanCppId = ocrdkRegion.mathancppId;
        }
    }
}