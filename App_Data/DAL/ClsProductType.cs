using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsProductType
    {
        public string ProductType { get; set; }
        public string ProductDesc { get; set; }
        public string Updatedby { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Boolean? ActiveFlag { get; set; }

        public List<ClsProductType> GetProductType()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsProductType> oProductType = (from data in prepumaContext.GetTable<tblProductType>()
                                       select new ClsProductType
                                       {
                                           ProductType =data.ProductType,
                                           ProductDesc = data.ProductDesc,
                                           Updatedby = data.Updatedby,
                                           UpdatedOn = data.UpdatedOn,
                                           Createdby = data.Createdby,
                                           CreatedOn = data.CreatedOn,
                                           ActiveFlag = (bool)data.ActiveFlag

                                       }).ToList<ClsProductType>();
            return oProductType;
        }

        public static ClsProductType GetProductType(string sDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsProductType oProductType = (from data in prepumaContext.GetTable<tblProductType>()
                                        where data.ProductType == sDatabaseKey

                                       select new ClsProductType
                                        {
                                            ProductType = data.ProductType,
                                            ProductDesc = data.ProductDesc,
                                            Updatedby = data.Updatedby,
                                            UpdatedOn = data.UpdatedOn,
                                            Createdby = data.Createdby,
                                            CreatedOn = data.CreatedOn,
                                            ActiveFlag = (bool)data.ActiveFlag

                                        }).SingleOrDefault<ClsProductType>();

            return oProductType;
        }

        public static string InsertProductType(ClsProductType oNewData)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            try
            {
                ClsProductType obizType = ClsProductType.GetProductType(oNewData.ProductType);
                if (obizType == null)
                {
                    tblProductType oNewRow = new tblProductType()
                    {
                        ProductType = oNewData.ProductType,
                        ProductDesc = oNewData.ProductDesc,
                        Updatedby = oNewData.Updatedby,
                        Createdby = oNewData.Createdby,
                        ActiveFlag = oNewData.ActiveFlag
                    };

                    // Add the new object to the sales Rep collection.
                    prepumaContext.GetTable<tblProductType>().InsertOnSubmit(oNewRow);
                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();
                }
                else
                {
                    errMsg = "Already Exists Product Type with " + "'" + oNewData.ProductType + "'";
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