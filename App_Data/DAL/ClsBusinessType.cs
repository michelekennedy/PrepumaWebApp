using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsBusinessType
    {
        public string BusinessType { get; set; }
        public string BusinessDesc { get; set; }
        public string Updatedby { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Boolean? ActiveFlag { get; set; }

        public List<ClsBusinessType> GetBizType()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsBusinessType> oBusinessType = (from data in prepumaContext.GetTable<tblBusinessType>()
                                                 select new ClsBusinessType
                                                 {
                                                     
                                                     BusinessType = data.BusinessType,
                                                     BusinessDesc = data.BusinessDesc,
                                                     Updatedby = data.Updatedby,
                                                     UpdatedOn = data.UpdatedOn,
                                                     Createdby = data.Createdby,
                                                     CreatedOn = data.CreatedOn,
                                                     ActiveFlag = (bool)data.ActiveFlag

                                                 }).ToList<ClsBusinessType>();
            return oBusinessType;
        }

        public static ClsBusinessType GetBizType(string sDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsBusinessType oBizType = (from data in prepumaContext.GetTable<tblBusinessType>()
                                       where data.BusinessType == sDatabaseKey

                                          select new ClsBusinessType
                                       {
                                           BusinessType = data.BusinessType,
                                           BusinessDesc = data.BusinessDesc,
                                           Updatedby = data.Updatedby,
                                           UpdatedOn = data.UpdatedOn,
                                           Createdby = data.Createdby,
                                           CreatedOn = data.CreatedOn,
                                           ActiveFlag = (bool)data.ActiveFlag
                                           
                                       }).SingleOrDefault<ClsBusinessType>();

            return oBizType;
        }

        public static string InsertBizType(ClsBusinessType oNewData)
        {
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            try
            {
                ClsBusinessType obizType = ClsBusinessType.GetBizType(oNewData.BusinessType);
                if (obizType == null)
                {
                    tblBusinessType oNewRow = new tblBusinessType()
                    {
                        BusinessType = oNewData.BusinessType,
                        BusinessDesc = oNewData.BusinessDesc,
                        Updatedby = oNewData.Updatedby,
                        Createdby = oNewData.Createdby,
                        ActiveFlag = oNewData.ActiveFlag
                    };

                    // Add the new object to the sales Rep collection.
                    prepumaContext.GetTable<tblBusinessType>().InsertOnSubmit(oNewRow);
                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();
                }
                else
                {
                    errMsg = "Already Exists Business Type with " + "'" + oNewData.BusinessType + "'";
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