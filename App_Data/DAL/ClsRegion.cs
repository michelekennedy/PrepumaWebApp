using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsRegion
    {
        public int RegionID { get; set; }
        public string Airport { get; set; }
        public string BranchManager { get; set; }
        public string CostCenter { get; set; }
        public string CostCenterLocation { get; set; }
        public string Jusrisdiction { get; set; }
        public string District { get; set; }
        public string DistrictManager { get; set; }
        public bool? Reported { get; set; }
        public string Updatedby { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Boolean? ActiveFlag { get; set; }
        public Boolean StackReport { get; set; }


        public List<ClsRegion> GetRegions()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsRegion> oRegion = (from data in prepumaContext.GetTable<tblRegion>() orderby data.Airport
                                       select new ClsRegion
                                       {
                                           RegionID = data.RegionID,
                                           Airport = data.Airport,
                                           BranchManager = data.BranchManager,
                                           CostCenter = data.CostCenter,
                                           CostCenterLocation = data.CostCenterLocation,
                                           Jusrisdiction = data.Jurisdiction,
                                           District = data.District,
                                           DistrictManager = data.DistrictManager,
                                           Reported = data.Reported,
                                           Updatedby = data.Updatedby,
                                           UpdatedOn = data.UpdatedOn,
                                           Createdby = data.Createdby,
                                           CreatedOn = data.CreatedOn,
                                           ActiveFlag = (bool)data.ActiveFlag,
                                           StackReport = (bool)data.StackReport

                                       }).ToList<ClsRegion>();
            return oRegion;
        }

        public static ClsRegion CompareAirport(string sDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsRegion oRegion = (from data in prepumaContext.GetTable<tblRegion>()
                                   where data.Airport == sDatabaseKey

                                  select new ClsRegion
                                   {
                                       RegionID = data.RegionID,
                                       Airport = data.Airport,
                                       BranchManager = data.BranchManager,
                                       CostCenter = data.CostCenter,
                                       CostCenterLocation = data.CostCenterLocation,
                                       Jusrisdiction = data.Jurisdiction,
                                       District = data.District,
                                       DistrictManager = data.DistrictManager,
                                       Reported = data.Reported,
                                       StackReport = data.StackReport

                                   }).SingleOrDefault<ClsRegion>();

            return oRegion;
        }

        public static ClsRegion CompareCostCenter(string sDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsRegion oRegion = (from data in prepumaContext.GetTable<tblRegion>()
                                 where data.CostCenter == sDatabaseKey

                                 select new ClsRegion
                                 {
                                     RegionID = data.RegionID,
                                     Airport = data.Airport,
                                     BranchManager = data.BranchManager,
                                     CostCenter = data.CostCenter,
                                     CostCenterLocation = data.CostCenterLocation,
                                     Jusrisdiction = data.Jurisdiction,
                                     District = data.District,
                                     DistrictManager = data.DistrictManager,
                                     Reported = data.Reported,
                                     StackReport = data.StackReport

                                 }).SingleOrDefault<ClsRegion>();

            return oRegion;
        }

        public static string InsertRegion(ClsRegion oNewData)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            try
            {
                ClsRegion oAirport = ClsRegion.CompareAirport(oNewData.Airport);
                ClsRegion oCostCenter = ClsRegion.CompareCostCenter(oNewData.CostCenter);
                if (oAirport == null && oCostCenter == null)
                {
                    tblRegion oNewRow = new tblRegion()
                    {
                        Airport = oNewData.Airport,
                        BranchManager = oNewData.BranchManager,
                        CostCenter = oNewData.CostCenter,
                        CostCenterLocation = oNewData.CostCenterLocation,
                        Jurisdiction = oNewData.Jusrisdiction,
                        District = oNewData.District,
                        DistrictManager = oNewData.DistrictManager,
                        Reported = oNewData.Reported,
                        Updatedby = oNewData.Updatedby,
                        Createdby = oNewData.Createdby,
                        ActiveFlag = oNewData.ActiveFlag,
                        StackReport = oNewData.StackReport
                    };

                    // Add the new object to the sales Rep collection.
                    prepumaContext.GetTable<tblRegion>().InsertOnSubmit(oNewRow);
                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();
                }
                else
                {
                    string displayProp = "";
                    if (oAirport == null)
                    {
                        displayProp = oNewData.CostCenter;
                    }
                    else
                    {
                        displayProp = oNewData.Airport;
                    }
                    errMsg = "Already Exists Region with Airport as" + "'" + displayProp + "'";
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }

        public static string UpdateRegion(ClsRegion oNewData)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            try
            {
                ClsRegion oExisting = ClsRegion.CompareAirport(oNewData.Airport);

                if (oExisting != null)
                {
                    // Query the database for the row to be updated. 
                    var query =
                        from qdata in prepumaContext.GetTable<tblRegion>()
                        where qdata.Airport == oExisting.Airport
                        select qdata;

                    // Execute the query, and change the column values 
                    // you want to change. 
                    foreach (tblRegion updRow in query)
                    {
                        updRow.BranchManager = oNewData.BranchManager;
                        updRow.CostCenter = oNewData.CostCenter;
                        updRow.CostCenterLocation = oNewData.CostCenterLocation;
                        updRow.Jurisdiction = oNewData.Jusrisdiction;
                        updRow.District = oNewData.District;
                        updRow.DistrictManager = oNewData.DistrictManager;
                        updRow.Updatedby = oNewData.Updatedby;
                        updRow.ActiveFlag = oNewData.ActiveFlag;
                        updRow.StackReport = oNewData.StackReport;
                    }

                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();

                    oNewData.Airport = oExisting.Airport;
                }
                else
                {
                    errMsg = "There is No Region Exists with Airport as" +"'"+ oNewData.Airport +"'";
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }

        public static bool CheckException(string sDatabaseKey)
        {
            bool exemptFlag = false;
            try
            {
                PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
               
                ClsRegion oRegion = (from data in prepumaContext.GetTable<tblCostCenterException>()
                                     where data.Region == sDatabaseKey

                                     select new ClsRegion
                                     {
                                         Airport = data.Region

                                     }).SingleOrDefault<ClsRegion>();

                if (oRegion != null)
                {
                    exemptFlag = true;
                }
            }
            catch (Exception ex)
            {
                exemptFlag=false;
            }
           
            return exemptFlag;
        }

    }
}