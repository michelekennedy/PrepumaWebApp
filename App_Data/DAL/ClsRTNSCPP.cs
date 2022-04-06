using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsRTNSCPP
    {
        public string RTNSID { get; set; }
        public double? RTNCPP { get; set; }
		public string DESC { get; set; }
        public string rtnsIdInfo { get; set; }
        public string Updatedby { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Boolean? ActiveFlag { get; set; }

        public List<ClsRTNSCPP> GetRtnsIdInfo()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsRTNSCPP> oRTNSCPP = (from data in prepumaContext.GetTable<tblRTNSCPP>()
                                                    select new ClsRTNSCPP
                                                     {
                                                         RTNSID = data.RTNSID,
                                                         RTNCPP = data.RTNCPP,
                                                         DESC = data.DESC,
                                                         rtnsIdInfo = string.Format("{0}-{1}", data.RTNCPP, data.DESC),
                                                         Updatedby = data.Updatedby,
                                                         UpdatedOn = data.UpdatedOn,
                                                         Createdby = data.Createdby,
                                                         CreatedOn = data.CreatedOn,
                                                         ActiveFlag = (bool)data.ActiveFlag

                                                     }).ToList<ClsRTNSCPP>();
            return oRTNSCPP;
        }

        public static ClsRTNSCPP CompareRtnsId(string sDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsRTNSCPP oRTNSCPP = (from data in prepumaContext.GetTable<tblRTNSCPP>()
                                 where data.RTNSID == sDatabaseKey

                                  select new ClsRTNSCPP
                                 {
                                   RTNCPP = data.RTNCPP,
                                   RTNSID = data.RTNSID,
                                   DESC = data.DESC

                                 }).SingleOrDefault<ClsRTNSCPP>();

            return oRTNSCPP;
        }

        public static string InsertReturns(ClsRTNSCPP oNewData)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            try
            {
                ClsRTNSCPP oRtncpp = ClsRTNSCPP.CompareRtnsId(oNewData.RTNSID);

                if (oRtncpp == null)
                {
                    tblRTNSCPP oNewRow = new tblRTNSCPP()
                    {
                        RTNSID = oNewData.RTNSID,
                        RTNCPP = oNewData.RTNCPP,
                        DESC = oNewData.DESC,
                        Updatedby = oNewData.Updatedby,
                        Createdby = oNewData.Createdby,
                        ActiveFlag = oNewData.ActiveFlag
                    };

                    // Add the new object to the sales Rep collection.
                    prepumaContext.GetTable<tblRTNSCPP>().InsertOnSubmit(oNewRow);
                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();
                }
                else
                {
                    errMsg = "Already Exists Returns with Returns Id as" + "'" + oNewData.RTNSID + "'";
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }

        public static string UpdateReturns(ClsRTNSCPP oNewData)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            try
            {
                ClsRTNSCPP oExisting = ClsRTNSCPP.CompareRtnsId(oNewData.RTNSID);

                if (oExisting != null)
                {
                    // Query the database for the row to be updated. 
                    var query =
                        from qdata in prepumaContext.GetTable<tblRTNSCPP>()
                        where qdata.RTNSID == oExisting.RTNSID
                        select qdata;

                    // Execute the query, and change the column values 
                    // you want to change. 
                    foreach (tblRTNSCPP updRow in query)
                    {
                        updRow.RTNCPP = oNewData.RTNCPP;
                        updRow.DESC = oNewData.DESC;
                        updRow.Updatedby = oNewData.Updatedby;
                        updRow.ActiveFlag = oNewData.ActiveFlag;
                    }

                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();

                    oNewData.RTNSID = oExisting.RTNSID;
                }
                else
                {
                    errMsg = "There is No Returns Exists with Returns ID as" + "'" + oNewData.RTNSID + "'";
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }
    }
}