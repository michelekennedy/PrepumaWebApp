using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsSalesReps
    {
        public string SalesRepID { get; set; }
        public string SalesRep { get; set; }
        public string salesrep_id { get; set; }
        public string SalesRepTitle { get; set; }
        public int? Threshold { get; set; }
        public string BusinesType { get; set; }
        public string nSMID { get; set; }
        public string dSM { get; set; }
        public bool? newBizline { get; set; }
        public int id { get; set; }
        public string Updatedby { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Boolean? ActiveFlag { get; set; }
        public Boolean? StackReport { get; set; }
        public string EmployeeID { get; set; }

        public List<ClsSalesReps> GetTerritory()
        {
            PumaSQLDataContext pumaContext = new PumaSQLDataContext();
            List<ClsSalesReps> oTerritory = (from data in pumaContext.GetTable<tblSalesRep>()
                                          select new ClsSalesReps
                                       {
                                           SalesRepID = data.SalesRepID,
                                           SalesRep = data.SalesRep,
                                           salesrep_id = string.Format("{0}-{1}",data.SalesRepID,data.SalesRep),
                                           SalesRepTitle = data.SalesRepTitle,
                                           Threshold = data.Threshold,
                                           BusinesType = data.BusinessType,
                                           nSMID = data.NSMID,
                                           dSM = data.DSM,
                                           newBizline = data.NewBizLine,
                                           id= data.id,
                                           Updatedby = data.Updatedby,
                                           UpdatedOn = Convert.ToDateTime(data.UpdatedOn),
                                           Createdby = data.Createdby,
                                           CreatedOn = Convert.ToDateTime(data.CreatedOn),
                                           ActiveFlag = (bool)data.ActiveFlag,
                                           StackReport = (bool)data.StackReport,
                                           EmployeeID = data.EmployeeID
                                          }).ToList<ClsSalesReps>();
            return oTerritory;
            
        }

        public static ClsSalesReps GetSalesReps(string sDatabaseKey)
        {
            PumaSQLDataContext pumaContext = new PumaSQLDataContext();

            ClsSalesReps oSalesRep = (from data in pumaContext.GetTable<tblSalesRep>()
                                 where data.SalesRepID == sDatabaseKey

                                      select new ClsSalesReps
                                 {
                                     SalesRep = data.SalesRep,
                                     SalesRepTitle = data.SalesRepTitle,
                                     Threshold = data.Threshold,
                                     BusinesType = data.BusinessType,
                                     nSMID = data.NSMID,
                                     dSM = data.DSM,
                                     newBizline = data.NewBizLine,
                                     id= data.id,
                                     Updatedby = data.Updatedby,
                                     UpdatedOn = data.UpdatedOn,
                                     Createdby = data.Createdby,
                                     CreatedOn = data.CreatedOn,
                                     ActiveFlag = (bool)data.ActiveFlag,
                                     StackReport = (bool)data.StackReport,
                                     EmployeeID = data.EmployeeID
                                      }).SingleOrDefault<ClsSalesReps>();
            
            return oSalesRep;
        }

        public int GetSalesRepKey(string salesRepID)
        {
            int salesRepKey = 0;
           PumaSQLDataContext pumaContext = new PumaSQLDataContext();

           var slsrepId = pumaContext.tblSalesReps
                        .Where(x => x.id == id)
                        .FirstOrDefault();

           salesRepKey = slsrepId.id;
           return salesRepKey;
        }

        //********************************************************************
        /// <summary> Function to update or insert a sales Rep </summary>
        /// <param name="oNewData"> sales Rep object</param>

        public List<ClsSalesReps> GetDisticntSalesRepTitle()
        {
            PumaSQLDataContext pumaContext = new PumaSQLDataContext();

            List<ClsSalesReps> uniqueSlsRepTitle = (from data in pumaContext.GetTable<tblSalesRep>()
                                                   
                                                    select new ClsSalesReps
                                                    {
                                            

                                                     }).ToList<ClsSalesReps>();

            return uniqueSlsRepTitle;
        }

        public static string InsertSalesRep(ClsSalesReps oNewData)
        {
            string errMsg = "";
            PumaSQLDataContext pumaContext = new PumaSQLDataContext();
            try
            {
                ClsSalesReps osalesRep = ClsSalesReps.GetSalesReps(oNewData.SalesRepID);
                if (osalesRep == null)
                {
                    tblSalesRep oNewRow = new tblSalesRep()
                    {
                        //id = oNewData.id,
                        SalesRepTitle = oNewData.SalesRepTitle,
                        SalesRep = oNewData.SalesRep,
                        SalesRepID = oNewData.SalesRepID,
                        Threshold = oNewData.Threshold,
                        BusinessType = oNewData.BusinesType,
                        NSMID = oNewData.nSMID,
                        DSM = oNewData.dSM,
                        NewBizLine = oNewData.newBizline,
                        Updatedby = oNewData.Updatedby,
                        Createdby = oNewData.Createdby,
                        ActiveFlag = oNewData.ActiveFlag,
                        StackReport = oNewData.StackReport,
                        EmployeeID = oNewData.EmployeeID
                    };

                    // Add the new object to the sales Rep collection.
                    pumaContext.GetTable<tblSalesRep>().InsertOnSubmit(oNewRow);
                    // Submit the changes to the database. 
                    pumaContext.SubmitChanges();
                }
                else
                {
                    errMsg = "Already Exists SalesRepID with " + "'" +oNewData.SalesRepID + "'";
                } 
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }

        public static string UpdateSalesRep(ClsSalesReps oNewData)
        {
            string errMsg = "";
            PumaSQLDataContext pumaContext = new PumaSQLDataContext();
            try
            {
                ClsSalesReps oExisting = ClsSalesReps.GetSalesReps(oNewData.SalesRepID);

                if (oExisting != null)
                {
                    // Query the database for the row to be updated. 
                    var query =
                        from qdata in pumaContext.GetTable<tblSalesRep>()
                        where qdata.id== oExisting.id
                        select qdata;

                    // Execute the query, and change the column values 
                    // you want to change. 
                    foreach (tblSalesRep updRow in query)
                    {
                        updRow.SalesRep = oNewData.SalesRep;
                        updRow.SalesRepTitle = oNewData.SalesRepTitle;
                        updRow.Threshold = oNewData.Threshold;
                        updRow.BusinessType = oNewData.BusinesType;
                        updRow.NSMID = oNewData.nSMID;
                        updRow.DSM = oNewData.dSM;
                        updRow.Updatedby = oNewData.Updatedby;
                        updRow.ActiveFlag = oNewData.ActiveFlag;
                        updRow.StackReport = oNewData.StackReport;
                        updRow.EmployeeID = oNewData.EmployeeID;
                    }

                    // Submit the changes to the database. 
                    pumaContext.SubmitChanges();

                    oNewData.id = oExisting.id;
                    
                }
                else
                {
                    errMsg = "There is No SalesRep Exists with SalesRep ID as" + "'" + oNewData.SalesRepID + "'";
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }
    }

    public class ClsThreshold
    {
        public string idThreshold { get; set; }
        public string BusinessType { get; set; }
        public double Threshold { get; set; }


        public double GetThreshold(string bustype)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            double thisThreshold = 0;
            bustype = bustype.ToUpper();

            ClsThreshold threshold = (from data in prepumaContext.GetTable<tblThreshold>()
                                      where data.BusinessType == bustype
                                      select new ClsThreshold
                                      {
                                          Threshold = Convert.ToDouble(data.Threshold)
                                      }).SingleOrDefault<ClsThreshold>();

            if (threshold == null)
            {
                ClsThreshold thresholdDefault = (from data in prepumaContext.GetTable<tblThreshold>()
                                                 where data.BusinessType == "DEFAULT"
                                                 select new ClsThreshold
                                                 {
                                                     Threshold = Convert.ToDouble(data.Threshold)
                                                 }).SingleOrDefault<ClsThreshold>();
                thisThreshold = thresholdDefault.Threshold;
            }
            else
            {
                thisThreshold = threshold.Threshold;
            }


            return thisThreshold;

        }
       
    }
}