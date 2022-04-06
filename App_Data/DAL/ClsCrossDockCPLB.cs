using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsCrossDockCPLB
    {
        public string CDCPLBID { get; set; }
        public Decimal? CDCPLB { get; set; }
        public string id_crsdockfee { get; set; }
        public string DESC { get; set; }
        public string Updatedby { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Boolean? ActiveFlag { get; set; }
        public string region { get; set; }

        public List<ClsCrossDockCPLB> GetcdcplbIdInfo()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsCrossDockCPLB> oCrossDockcplb = (from data in prepumaContext.GetTable<tblCrossDockCPLB>()
                                                     select new ClsCrossDockCPLB
                                              {
                                                  CDCPLBID = data.CDID,
                                                  CDCPLB = data.CDCPLB,
                                                  id_crsdockfee = string.Format("{0}-{1}", data.CDID, data.CDCPLB),
                                                  DESC = data.DESC,
                                                  Updatedby = data.Updatedby,
                                                  UpdatedOn = data.UpdatedOn,
                                                  Createdby = data.Createdby,
                                                  CreatedOn = data.CreatedOn,
                                                  ActiveFlag = (bool)data.ActiveFlag

                                              }).ToList<ClsCrossDockCPLB>();
            return oCrossDockcplb;
        }

        public static ClsCrossDockCPLB GetCrossDockingId(string sDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsCrossDockCPLB oCrossDockcplb = (from data in prepumaContext.GetTable<tblCrossDockCPLB>()
                                   where data.CDID == sDatabaseKey

                                         select new ClsCrossDockCPLB
                                   {
                                       CDCPLB = data.CDCPLB,
                                       CDCPLBID = data.CDID,
                                       id_crsdockfee = string.Format("{0}-{1}", data.CDID, data.CDCPLB),
                                       DESC = data.DESC,
                                       Updatedby = data.Updatedby,
                                       UpdatedOn = data.UpdatedOn,
                                       Createdby = data.Createdby,
                                       CreatedOn = data.CreatedOn,
                                       ActiveFlag = (bool)data.ActiveFlag

                                   }).SingleOrDefault<ClsCrossDockCPLB>();

            return oCrossDockcplb;
        }

        public static List<ClsCrossDockCPLB> GetCrossDckgById(string sDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsCrossDockCPLB> oCrossDockcplb = (from data in prepumaContext.GetTable<tblCrossDockCPLB>()
                                                     where data.CDID == sDatabaseKey
                                                     select new ClsCrossDockCPLB
                                                     {
                                                         CDCPLBID = data.CDID,
                                                         CDCPLB = data.CDCPLB,
                                                         id_crsdockfee = string.Format("{0}-{1}", data.CDID, data.CDCPLB),
                                                         DESC = data.DESC,
                                                         Updatedby = data.Updatedby,
                                                         UpdatedOn = data.UpdatedOn,
                                                         Createdby = data.Createdby,
                                                         CreatedOn = data.CreatedOn,
                                                         ActiveFlag = (bool)data.ActiveFlag

                                                     }).ToList<ClsCrossDockCPLB>();
            return oCrossDockcplb;
        }

        public static string InsertCrossDocking(ClsCrossDockCPLB oNewData)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            try
            {
                ClsCrossDockCPLB oRtncpp = ClsCrossDockCPLB.GetCrossDockingId(oNewData.CDCPLBID);

                if (oRtncpp == null)
                {
                    tblCrossDockCPLB oNewRow = new tblCrossDockCPLB()
                    {
                        CDID = oNewData.CDCPLBID,
                        CDCPLB = oNewData.CDCPLB,
                        DESC = oNewData.DESC,
                        Updatedby = oNewData.Updatedby,
                        Createdby = oNewData.Createdby,
                        ActiveFlag = oNewData.ActiveFlag
                    };

                    // Add the new object to the sales Rep collection.
                    prepumaContext.GetTable<tblCrossDockCPLB>().InsertOnSubmit(oNewRow);
                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();
                }
                else
                {
                    errMsg = "Already Exists Cross Dcoking with Cross Docking Id as" + "'" + oNewData.CDCPLBID + "'";
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }

        public static string UpdateCrossDocking(ClsCrossDockCPLB oNewData)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            try
            {
                ClsCrossDockCPLB oExisting = ClsCrossDockCPLB.GetCrossDockingId(oNewData.CDCPLBID);

                if (oExisting != null)
                {
                    // Query the database for the row to be updated. 
                    var query =
                        from qdata in prepumaContext.GetTable<tblCrossDockCPLB>()
                        where qdata.CDID == oExisting.CDCPLBID
                        select qdata;

                    // Execute the query, and change the column values 
                    // you want to change. 
                    foreach (tblCrossDockCPLB updRow in query)
                    {
                        updRow.CDCPLB = oNewData.CDCPLB;
                        updRow.Updatedby = oNewData.Updatedby;
                        updRow.ActiveFlag = oNewData.ActiveFlag;
                    }

                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();

                    oNewData.CDCPLBID = oExisting.CDCPLBID;
                }
                else
                {
                    errMsg = "There is No CrossDocking Exists with CrossDockig ID as" + "'" + oNewData.CDCPLBID + "'";
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