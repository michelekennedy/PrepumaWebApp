using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsMATHANCPP
    {
        public string CPPMATHANID { get; set; }
        public double? CPPMATHAN { get; set; }
        public string DESC { get; set; }
        public string mathancppId { get; set; }
        public string Updatedby { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Boolean? ActiveFlag { get; set; }

        public List<ClsMATHANCPP> GetMathandcppInfo()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsMATHANCPP> oMATHANCPP = (from data in prepumaContext.GetTable<tblMATHANCPP>()
                                           select new ClsMATHANCPP
                                         {
                                             CPPMATHAN = data.CPPMATHAN,
                                             CPPMATHANID = data.CPPMATHANID,
                                             DESC = data.DESC,
                                             mathancppId = string.Format("{0}-{1}", data.CPPMATHAN, data.DESC),
                                             Updatedby = data.Updatedby,
                                             UpdatedOn = data.UpdatedOn,
                                             Createdby = data.Createdby,
                                             CreatedOn = data.CreatedOn,
                                             ActiveFlag = (bool)data.ActiveFlag

                                         }).ToList<ClsMATHANCPP>();
            return oMATHANCPP;
        }

        public static ClsMATHANCPP GetMaterialHanlgID(string sDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsMATHANCPP oGateway = (from data in prepumaContext.GetTable<tblMATHANCPP>()
                                   where data.CPPMATHANID == sDatabaseKey

                                     select new ClsMATHANCPP
                                   {
                                       CPPMATHANID = data.CPPMATHANID,
                                       CPPMATHAN = data.CPPMATHAN,
                                       DESC = data.DESC,
                                       Updatedby = data.Updatedby,
                                       UpdatedOn = data.UpdatedOn,
                                       Createdby = data.Createdby,
                                       CreatedOn = data.CreatedOn,
                                       ActiveFlag = (bool)data.ActiveFlag
                                       
                                       
                                   }).SingleOrDefault<ClsMATHANCPP>();

            return oGateway;
        }

        public static string InsertMaterialHandling(ClsMATHANCPP oNewData)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            try
            {
                ClsMATHANCPP oGateway = ClsMATHANCPP.GetMaterialHanlgID(oNewData.CPPMATHANID);
                if (oGateway == null)
                {
                    tblMATHANCPP oNewRow = new tblMATHANCPP()
                    {
                        CPPMATHAN = oNewData.CPPMATHAN,
                        CPPMATHANID = oNewData.CPPMATHANID,
                        DESC = oNewData.DESC,
                        Updatedby = oNewData.Updatedby,
                        Createdby = oNewData.Createdby,
                        ActiveFlag = oNewData.ActiveFlag

                    };

                    // Add the new object to the sales Rep collection.
                    prepumaContext.GetTable<tblMATHANCPP>().InsertOnSubmit(oNewRow);
                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();
                }
                else
                {
                    errMsg = "Already Exists Material Handling ID with " + "'" + oNewData.CPPMATHANID + "'";
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }

        public static string UpdateMaterialHandling(ClsMATHANCPP oNewData)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            try
            {
                ClsMATHANCPP oExisting = ClsMATHANCPP.GetMaterialHanlgID(oNewData.CPPMATHANID);

                if (oExisting != null)
                {
                    // Query the database for the row to be updated. 
                    var query =
                        from qdata in prepumaContext.GetTable<tblMATHANCPP>()
                        where qdata.CPPMATHANID == oExisting.CPPMATHANID
                        select qdata;

                    // Execute the query, and change the column values 
                    // you want to change. 
                    foreach (tblMATHANCPP updRow in query)
                    {
                        updRow.CPPMATHAN = oNewData.CPPMATHAN;
                        updRow.DESC = oNewData.DESC;
                        updRow.Updatedby = oNewData.Updatedby;
                        updRow.ActiveFlag = oNewData.ActiveFlag;
                    }

                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();

                    oNewData.CPPMATHANID = oExisting.CPPMATHANID;
                }
                else
                {
                    errMsg = "There is No MaterialHandling Exists with MaterialHandling ID as" + "'" + oNewData.CPPMATHANID + "'";
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