using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsExceptionReport
    {
        public Int32 ExceptionID { get; set; }
        public string ExceptionName { get; set; }
        public string STP_Name { get; set; }
        public Boolean Default_Report { get; set; }

     public List<ClsExceptionReport> GetData()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsExceptionReport> oRecord = (from data in prepumaContext.GetTable<tblDataExceptionReports>()
                                                orderby data.Default_Report descending
                                       select new ClsExceptionReport
                                       {
                                           ExceptionID = (Int32)data.ExceptionID,
                                           ExceptionName = data.ExceptionName,
                                           STP_Name = data.STP_Name,
                                           Default_Report = (Boolean)data.Default_Report
                                       }).ToList<ClsExceptionReport>();
            return oRecord;
        }


     public static ClsExceptionReport GetReport(Int32 iDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsExceptionReport oRecord = (from data in prepumaContext.GetTable<tblDataExceptionReports>()
                                 where data.ExceptionID == iDatabaseKey

                                 select new ClsExceptionReport
                                 {
                                     ExceptionID = (Int32)data.ExceptionID,
                                     ExceptionName = data.ExceptionName,
                                     STP_Name = data.STP_Name,
                                     Default_Report =  (Boolean)data.Default_Report
                                  }).SingleOrDefault<ClsExceptionReport>();

          
            return oRecord;
        }
    }
}