using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClsCustomer
    {
        public Int32 idCustomer { get; set; }
        public string RelationshipName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Boolean? ActiveFlag { get; set; }
        public decimal? CreditLimit { get; set; }
        public string PaymentTerms { get; set; }

        public string MCNumber { get; set; }
        //public string BusinessConsumerFlag { get; set; }
        //public string IndustryCode { get; set; }


        public List<ClsCustomer> GetCustomers()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
           
            List<ClsCustomer> oCustomers = (from cust in prepumaContext.GetTable<tblCustomer>()                                       
                                             orderby cust.RelationshipName
                                            select new ClsCustomer
                                        {
                                            idCustomer = cust.idCustomer,
                                            RelationshipName = cust.RelationshipName,
                                            CreatedBy = cust.CreatedBy,
                                            CreatedOn = Convert.ToDateTime(cust.CreatedOn),
                                            UpdatedBy = cust.UpdatedBy,
                                            UpdatedOn = Convert.ToDateTime(cust.UpdatedOn),
                                            ActiveFlag = (bool)cust.ActiveFlag,
                                            CreditLimit = Convert.ToDecimal(cust.CreditLimit),
                                            PaymentTerms = cust.PaymentTerms ,
                                            MCNumber = cust.MCNumber
                                        }).ToList<ClsCustomer>();
            return oCustomers;
        }

        public List<ClsCustomer> GetActiveCustomers()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            List<ClsCustomer> oCustomers = (from cust in prepumaContext.GetTable<tblCustomer>()
                                            where cust.ActiveFlag != false
                                            orderby cust.RelationshipName
                                           
                                            select new ClsCustomer
                                            {
                                                idCustomer = cust.idCustomer,
                                                RelationshipName = cust.RelationshipName,
                                                CreatedBy = cust.CreatedBy,
                                                CreatedOn = Convert.ToDateTime(cust.CreatedOn),
                                                UpdatedBy = cust.UpdatedBy,
                                                UpdatedOn = Convert.ToDateTime(cust.UpdatedOn),
                                                ActiveFlag = (bool)cust.ActiveFlag,
                                                CreditLimit = Convert.ToDecimal(cust.CreditLimit),
                                                PaymentTerms = cust.PaymentTerms,
                                                MCNumber = cust.MCNumber
                                            }).ToList<ClsCustomer>();
            return oCustomers;
        }

        public static ClsCustomer GetCustomer(Double? iDatabaseKey)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsCustomer oCustomer = (from data in prepumaContext.GetTable<tblCustomer>()
                                     where data.idCustomer == iDatabaseKey

                                     select new ClsCustomer
                                   {
                                       idCustomer = data.idCustomer,
                                       RelationshipName = data.RelationshipName,
                                       CreditLimit = Convert.ToDecimal(data.CreditLimit),
                                       PaymentTerms = data.PaymentTerms,
                                       UpdatedBy = data.UpdatedBy,
                                       UpdatedOn = Convert.ToDateTime(data.UpdatedOn),
                                       CreatedBy = data.CreatedBy,
                                       CreatedOn = Convert.ToDateTime(data.CreatedOn),
                                       ActiveFlag = (bool)data.ActiveFlag,
                                       MCNumber = data.MCNumber
                                     }).SingleOrDefault<ClsCustomer>();



            return oCustomer;
        }

        public static ClsCustomer GetCustomer(string relationshipname)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            ClsCustomer oCustomer = (from data in prepumaContext.GetTable<tblCustomer>()
                                     where data.RelationshipName == relationshipname

                                    select new ClsCustomer
                                   {
                                       idCustomer = data.idCustomer,
                                       RelationshipName = data.RelationshipName,
                                       CreditLimit = Convert.ToDecimal(data.CreditLimit),
                                       PaymentTerms = data.PaymentTerms,
                                       UpdatedBy = data.UpdatedBy,
                                       UpdatedOn = Convert.ToDateTime(data.UpdatedOn),
                                       CreatedBy = data.CreatedBy,
                                       CreatedOn = Convert.ToDateTime(data.CreatedOn),
                                       ActiveFlag = (bool)data.ActiveFlag,
                                       MCNumber = data.MCNumber
                                    }).SingleOrDefault<ClsCustomer>();



            return oCustomer;
        }

        public static string UpdateCustomer(ClsCustomer oNewData)
        {
            string errMsg = "";
            try
            {
                PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
                ClsCustomer oExisting = null;

                if (oNewData.idCustomer > 0)
                    oExisting = GetCustomer(oNewData.idCustomer);

                if (oExisting != null)
                {
                 // Query the database for the row to be updated. 
                    var query =
                    from qdata in prepumaContext.GetTable<tblCustomer>()
                    where qdata.idCustomer == oExisting.idCustomer
                    select qdata;

                    // Execute the query, and change the column values 
                    // you want to change. 
                    foreach (tblCustomer updRow in query)
                    {
                        updRow.RelationshipName = oNewData.RelationshipName;                       
                        updRow.UpdatedBy = oNewData.UpdatedBy;
                        updRow.UpdatedOn = oNewData.UpdatedOn;
                        updRow.ActiveFlag = Convert.ToBoolean(oNewData.ActiveFlag);
                        updRow.MCNumber = oNewData.MCNumber;
                    }
                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();

                    //Check for Relationship Name Change until we get to a point where we keep it only in Customer table
                    if (oExisting.RelationshipName != oNewData.RelationshipName)
                    {
                        //here we have to update tblContractClassification because too many reports still use that value instead of tblCustomer
                        UpdateRelationshipName(oExisting.RelationshipName, oNewData.RelationshipName);
                    }
                }
                else
                {
                    errMsg = "Could not find Customer with id " + oNewData.idCustomer;
                }
            }
            catch (Exception ex)
            {
                  errMsg = ex.Message.ToString();
            }
            return errMsg;
        }

        public static string InsertCustomer(ClsCustomer oNewData)
        {
           
            string errMsg = "";
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            try
            {
                ClsCustomer cust = new ClsCustomer();
                ClsCustomer oCustomer = ClsCustomer.GetCustomer(oNewData.RelationshipName);
                if (oCustomer == null)
                {
                    //List<ClsCustomer> cust = gateWay.GetGatewayInfo();
                    //int val = Convert.ToInt16(gateWayInfo.Max(x => x.gatewayID));

                    tblCustomer oNewRow = new tblCustomer()
                    {                        
                        RelationshipName = oNewData.RelationshipName,
                        UpdatedBy = oNewData.UpdatedBy,
                        UpdatedOn = oNewData.UpdatedOn,
                        CreatedBy = oNewData.CreatedBy,
                        CreatedOn = oNewData.CreatedOn,
                        CreditLimit = oNewData.CreditLimit,
                        PaymentTerms = oNewData.PaymentTerms,
                        ActiveFlag = oNewData.ActiveFlag,
                       MCNumber = oNewData.MCNumber
                };
                    prepumaContext.GetTable<tblCustomer>().InsertOnSubmit(oNewRow);
                    // Submit the changes to the database. 
                    prepumaContext.SubmitChanges();
                }
                else
                {
                    errMsg = "Customer Already Exists with RelationshipName " + "'" + oNewData.RelationshipName + "'";
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }

        public static void UpdateRelationshipName(string oldname, string newname)
        {
            try
            {
                String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;

                SqlConnection cnn;
                SqlCommand cmd;
                cnn = new SqlConnection(strConnString);

                cnn.Open();
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_RelationshipNameChange";
                cmd.Parameters.Add("@oldname", SqlDbType.VarChar).Value = oldname;
                cmd.Parameters.Add("@newname", SqlDbType.VarChar).Value = newname;
               
                cmd.CommandTimeout = 10800;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();
                

                cmd.Dispose();
                cnn.Close();

            }
            catch (Exception ex)
            {
                //ErrorMsg = ex.ToString();
            }
        }
    }
}