using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsSalesRepTitle
    {
        public int idSalesRepTitle { get; set; }
        public string SalesRepTitle { get; set; }
        public int? Threshold { get; set; }
        public Boolean? ActiveFlag { get; set; }


        public List<ClsSalesRepTitle> GetTitles()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsSalesRepTitle> oTitles = (from data in prepumaContext.GetTable<tblSalesRepTitle>()
                                              orderby data.SalesRepTitle
                                                 select new ClsSalesRepTitle
                                          {
                                              idSalesRepTitle = data.idSalesRepTitle,
                                              SalesRepTitle = data.SalesRepTitle,
                                              Threshold = Convert.ToInt32(data.Threshold),                                              
                                              ActiveFlag = (bool)data.ActiveFlag,
                                          }).ToList<ClsSalesRepTitle>();
            return oTitles;

        }

        public int? GetThresholdbyTitle(string title)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            ClsSalesRepTitle oTitle = (from data in prepumaContext.GetTable<tblSalesRepTitle>()
                                       where data.SalesRepTitle == title
                                       select new ClsSalesRepTitle
                                       {
                                           idSalesRepTitle = data.idSalesRepTitle,
                                           SalesRepTitle = data.SalesRepTitle,
                                           Threshold = Convert.ToInt32(data.Threshold),
                                           ActiveFlag = (bool)data.ActiveFlag,
                                       }).FirstOrDefault();
            return oTitle.Threshold;

        }

         public static string InsertSalesRepTitle(ClsSalesRepTitle oNewData)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            try
            {
                //ClsSalesRepTitle osalesRep = ClsSalesRepTitle.GetSalesReps(oNewData.SalesRepID);
                //if (osalesRep == null)
                //{
                    tblSalesRepTitle oNewRow = new tblSalesRepTitle()
                    {
                        SalesRepTitle = oNewData.SalesRepTitle,
                        Threshold = oNewData.Threshold,
                        ActiveFlag = oNewData.ActiveFlag
                    };

                    // Add the new object to the sales Rep collection.
                    prepumaContext.GetTable<tblSalesRepTitle>().InsertOnSubmit(oNewRow);
                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();
                //}
                //else
                //{
                //    errMsg = "Already Exists SalesRepID with " + "'" +oNewData.SalesRepID + "'";
                //} 
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }

        public static string UpdateSalesRepTitle(ClsSalesRepTitle oNewData)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            try
            {
                             
                    var query =
                        from qdata in prepumaContext.GetTable<tblSalesRepTitle>()
                        where qdata.idSalesRepTitle == oNewData.idSalesRepTitle
                        select qdata;

                    // Execute the query, and change the column values 
                    // you want to change. 
                    foreach (tblSalesRepTitle updRow in query)
                    {
               
                        updRow.Threshold = oNewData.Threshold;
                        updRow.ActiveFlag = oNewData.ActiveFlag;
                    }

                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();


            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }
    }

    }


