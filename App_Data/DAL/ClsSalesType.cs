using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsSalesType
    {
        public string SalesType { get; set; }
        public string SalesDescription { get; set; }
        public string Updatedby { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Boolean? ActiveFlag { get; set; }

        public List<ClsSalesType> GetSalesType()
        {
            PumaSQLDataContext pumaContext = new PumaSQLDataContext();
            List<ClsSalesType> oSalesType = (from data in pumaContext.GetTable<tblSalesType>()
                                          select new ClsSalesType
                                          {
                                              SalesType = data.SalesType,
                                              SalesDescription = data.SalesDescription,
                                              Updatedby = data.Updatedby,
                                              UpdatedOn = data.UpdatedOn,
                                              Createdby = data.Createdby,
                                              CreatedOn = data.CreatedOn,
                                              ActiveFlag = (bool)data.ActiveFlag

                                          }).ToList<ClsSalesType>();
            return oSalesType;
        }

        public static ClsSalesType GetSalesType(string sDatabaseKey)
        {
            PumaSQLDataContext pumaContext = new PumaSQLDataContext();

            ClsSalesType oSalesType = (from data in pumaContext.GetTable<tblSalesType>()
                                      where data.SalesType == sDatabaseKey

                                      select new ClsSalesType
                                      {
                                          SalesType = data.SalesType,
                                          SalesDescription = data.SalesDescription,
                                          Updatedby = data.Updatedby,
                                          UpdatedOn = data.UpdatedOn,
                                          Createdby = data.Createdby,
                                          CreatedOn = data.CreatedOn,
                                          ActiveFlag = (bool)data.ActiveFlag

                                      }).SingleOrDefault<ClsSalesType>();

            return oSalesType;
        }

        public static string InsertSalesType(ClsSalesType oNewData)
        {
            string errMsg = "";
            PumaSQLDataContext pumaContext = new PumaSQLDataContext();
            try
            {
                ClsSalesType osalesType = ClsSalesType.GetSalesType(oNewData.SalesType);
                if (osalesType == null)
                {
                    tblSalesType oNewRow = new tblSalesType()
                    {
                        SalesType = oNewData.SalesType,
                        SalesDescription = oNewData.SalesDescription,
                        Updatedby = oNewData.Updatedby,
                        Createdby = oNewData.Createdby,
                        ActiveFlag = oNewData.ActiveFlag
                        
                    };

                    // Add the new object to the sales Rep collection.
                    pumaContext.GetTable<tblSalesType>().InsertOnSubmit(oNewRow);
                    // Submit the changes to the database. 
                    pumaContext.SubmitChanges();
                }
                else
                {
                    errMsg = "Already Exists Sales Type with " + "'" + oNewData.SalesType + "'";
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