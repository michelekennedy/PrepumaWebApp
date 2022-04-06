using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClientInfo
    {
        public Double? ClientID { get; set; }
        public string ClientName { get; set; }
        public Double? ContractID { get; set; }
        public string ContractName { get; set; }
        public string ContractNumber { get; set; }
        public Double? AcctID { get; set; }
        public string Acctnbr { get; set; }
        public string StrategicSRID { get; set; }
        public string LocalSRID { get; set; }
        public string BusinessType { get; set; }
        public string ProductType { get; set; }
        public Double? CCID { get; set; }
        public string Expr1 { get; set; }
        public string Territory { get; set; }
        public string SalesType { get; set; }
        public string ISP { get; set; }
        public string RelationshipName { get; set; }
        public Boolean? ISPpaid { get; set; }
        public Boolean? LostBiz { get; set; }
        public string Region { get; set; }
        public string ShippingLocation { get; set; }
        public Single? EstAnnualRev { get; set; }
        public Single? EstMarginPct { get; set; }
        public string SecondaryNAICS { get; set; }
        public string Source { get; set; }
        public Single? SharePercent { get; set; }
        public Boolean? CCFlag { get; set; }
        public double? gatewayID { get; set; }
        public string RTNSID { get; set; }
        public string MATHANCPPID {get; set;}
        public string CDCPLBID { get; set; }
        public string Updatedby { get; set; }
        public string AcctUpdatedby { get; set; }
        public DateTime? AcctUpdatedOn { get; set; }
        public Boolean? ActiveFlag { get; set; }
        public string gateway { get; set; }
        public string Returns { get; set; }
        public string MaterialHandling { get; set; }
        public double? CDCPLB { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? rolloff_quarter { get; set; }
        public string InfoUpdatedby { get; set; }
        public DateTime? InfoUpdatedOn { get; set; }
        //public string Currency { get; set; }
        public string LocalRepEmployeeID { get; set; }
        public string StrategicRepEmployeeID { get; set; }
        public string MCNumber { get; set; }

        public string MissionCriticalSRID { get; set; }


        public string doSQLUpdate(ClientInfo objClient, string username)
        {

            string errorMsg = "";
            try
            {
                String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_UpdateContractInfo";

                cmd.Parameters.Add("@ContractNumber", SqlDbType.NVarChar).Value = objClient.ContractNumber;
                cmd.Parameters.Add("@ContractName", SqlDbType.NVarChar).Value = objClient.ContractName;
                cmd.Parameters.Add("@ContractRegion", SqlDbType.NVarChar).Value = objClient.Expr1;
                cmd.Parameters.Add("@Territory", SqlDbType.NVarChar).Value = objClient.Territory;
                cmd.Parameters.Add("@SalesType", SqlDbType.NVarChar).Value = objClient.SalesType;
                cmd.Parameters.Add("@ISP", SqlDbType.NVarChar).Value = objClient.ISP;
                //cmd.Parameters.Add("@Currency", SqlDbType.NVarChar).Value = objClient.Currency;
                cmd.Parameters.Add("@RelationshipName", SqlDbType.NVarChar).Value = objClient.RelationshipName;
                cmd.Parameters.Add("@CCID", SqlDbType.Float).Value = objClient.CCID;
                cmd.Parameters.Add("@ISPpaid", SqlDbType.Bit).Value = objClient.ISPpaid;
                cmd.Parameters.Add("@LostBiz", SqlDbType.Bit).Value = objClient.LostBiz;
                cmd.Parameters.Add("@EstAnnualRev", SqlDbType.Real).Value = objClient.EstAnnualRev;
                cmd.Parameters.Add("@EstMarginPct", SqlDbType.Real).Value = objClient.EstMarginPct;
                cmd.Parameters.Add("@SecondaryNAICS", SqlDbType.NVarChar).Value = objClient.SecondaryNAICS;
                cmd.Parameters.Add("@Source", SqlDbType.NVarChar).Value = objClient.Source;
                cmd.Parameters.Add("@Acctnbr", SqlDbType.NVarChar).Value = objClient.Acctnbr;
                cmd.Parameters.Add("@StrategicSRID", SqlDbType.NVarChar).Value = objClient.StrategicSRID;
                cmd.Parameters.Add("@LocalSRID", SqlDbType.NVarChar).Value = objClient.LocalSRID;
                cmd.Parameters.Add("@BusinessType", SqlDbType.NVarChar).Value = objClient.BusinessType;
                cmd.Parameters.Add("@ProductType", SqlDbType.NVarChar).Value = objClient.ProductType;
                cmd.Parameters.Add("@AccountRegion", SqlDbType.NVarChar).Value = objClient.Region;
                cmd.Parameters.Add("@ShippingLocation", SqlDbType.NVarChar).Value = objClient.ShippingLocation;
                cmd.Parameters.Add("@SharePercent", SqlDbType.Real).Value = objClient.SharePercent;
                cmd.Parameters.Add("@gatewayID", SqlDbType.Int).Value = objClient.gatewayID;
                cmd.Parameters.Add("@MATHANDCPPID", SqlDbType.NVarChar).Value = objClient.MATHANCPPID;
                cmd.Parameters.Add("@RTNSID", SqlDbType.NVarChar).Value = objClient.RTNSID;
                cmd.Parameters.Add("@CDCPLBID", SqlDbType.NVarChar).Value = objClient.CDCPLBID;

                cmd.Parameters.Add("@Updatedby", SqlDbType.NVarChar).Value = username;
                cmd.Parameters.Add("@ActiveFlag", SqlDbType.Bit).Value = objClient.ActiveFlag;
                if (objClient.StartDate.HasValue)
                {
                    cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = objClient.StartDate;
                }
                else
                {
                    cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                if (objClient.EndDate.HasValue)
                {
                    cmd.Parameters.Add("@enddate", SqlDbType.NVarChar).Value = objClient.EndDate;
                }
                else
                {
                    cmd.Parameters.Add("@enddate", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                if (objClient.rolloff_quarter.HasValue)
                {
                    cmd.Parameters.Add("@rolloffqtr", SqlDbType.NVarChar).Value = objClient.rolloff_quarter;
                }
                else
                {
                    cmd.Parameters.Add("@rolloffqtr", SqlDbType.NVarChar).Value = DBNull.Value;
                }


                var returnParam2 = new SqlParameter
                {
                    ParameterName = "@Error",
                    Direction = ParameterDirection.Output,
                    Size = 1000
                };
                cmd.Parameters.Add(returnParam2);

                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                errorMsg = (string)cmd.Parameters["@Error"].Value;


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return errorMsg;
        }

        public string doSQLInsert(ClientInfo objClient, string username)
        {

            string errorMsg = "";
            try
            {
                String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_InsertContractInfo";

                cmd.Parameters.Add("@ContractNumber", SqlDbType.NVarChar).Value = objClient.ContractNumber;
                cmd.Parameters.Add("@ContractName", SqlDbType.NVarChar).Value = objClient.ContractName;
                cmd.Parameters.Add("@ContractRegion", SqlDbType.NVarChar).Value = objClient.Expr1;
                cmd.Parameters.Add("@Territory", SqlDbType.NVarChar).Value = objClient.Territory;
                cmd.Parameters.Add("@SalesType", SqlDbType.NVarChar).Value = objClient.SalesType;
                cmd.Parameters.Add("@gatewayID", SqlDbType.Int).Value = objClient.gatewayID;
                cmd.Parameters.Add("@MATHANDCPPID", SqlDbType.NVarChar).Value = objClient.MATHANCPPID;
                cmd.Parameters.Add("@RTNSID", SqlDbType.NVarChar).Value = objClient.RTNSID;
                cmd.Parameters.Add("@CDCPLBID", SqlDbType.NVarChar).Value = objClient.CDCPLBID;

                cmd.Parameters.Add("@ISP", SqlDbType.NVarChar).Value = objClient.ISP;
                //cmd.Parameters.Add("@Currency", SqlDbType.NVarChar).Value = objClient.Currency;
                cmd.Parameters.Add("@RelationshipName", SqlDbType.NVarChar).Value = objClient.RelationshipName;
                cmd.Parameters.Add("@CCID", SqlDbType.Float).Value = objClient.CCID;
                cmd.Parameters.Add("@ISPpaid", SqlDbType.Bit).Value = objClient.ISPpaid;
                cmd.Parameters.Add("@LostBiz", SqlDbType.Bit).Value = objClient.LostBiz;
                cmd.Parameters.Add("@EstAnnualRev", SqlDbType.Real).Value = objClient.EstAnnualRev;
                cmd.Parameters.Add("@EstMarginPct", SqlDbType.Real).Value = objClient.EstMarginPct;
                cmd.Parameters.Add("@SecondaryNAICS", SqlDbType.NVarChar).Value = objClient.SecondaryNAICS;
                cmd.Parameters.Add("@Source", SqlDbType.NVarChar).Value = objClient.Source;
                cmd.Parameters.Add("@Acctnbr", SqlDbType.NVarChar).Value = objClient.Acctnbr;
                cmd.Parameters.Add("@StrategicSRID", SqlDbType.NVarChar).Value = objClient.StrategicSRID;
                cmd.Parameters.Add("@LocalSRID", SqlDbType.NVarChar).Value = objClient.LocalSRID;
                cmd.Parameters.Add("@BusinessType", SqlDbType.NVarChar).Value = objClient.BusinessType;
                cmd.Parameters.Add("@ProductType", SqlDbType.NVarChar).Value = objClient.ProductType;
                cmd.Parameters.Add("@AccountRegion", SqlDbType.NVarChar).Value = objClient.Region;
                cmd.Parameters.Add("@ShippingLocation", SqlDbType.NVarChar).Value = objClient.ShippingLocation;
                cmd.Parameters.Add("@SharePercent", SqlDbType.Real).Value = objClient.SharePercent;
                cmd.Parameters.Add("@Updatedby", SqlDbType.NVarChar).Value = username;
                cmd.Parameters.Add("@ActiveFlag", SqlDbType.Bit).Value = objClient.ActiveFlag;
                if (objClient.StartDate.HasValue)
                {
                    cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = objClient.StartDate;
                }
                else
                {
                    cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                if (objClient.EndDate.HasValue)
                {
                    cmd.Parameters.Add("@enddate", SqlDbType.NVarChar).Value = objClient.EndDate;
                }
                else
                {
                    cmd.Parameters.Add("@enddate", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                if (objClient.rolloff_quarter.HasValue)
                {
                    cmd.Parameters.Add("@rolloffqtr", SqlDbType.NVarChar).Value = objClient.rolloff_quarter;
                }
                else
                {
                    cmd.Parameters.Add("@rolloffqtr", SqlDbType.NVarChar).Value = DBNull.Value;
                }

                var returnParam2 = new SqlParameter
                {
                    ParameterName = "@Error",
                    Direction = ParameterDirection.Output,
                    Size = 1000
                };
                cmd.Parameters.Add(returnParam2);

                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                errorMsg = (string)cmd.Parameters["@Error"].Value;


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return errorMsg;
        }

        public string doSQLUpdateCCInsertCAC(ClientInfo objClient, string username)
        {
            string errorMsg = "";
            try
            {
                String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_UpdateCCInsertCAC";

                cmd.Parameters.Add("@ContractNumber", SqlDbType.NVarChar).Value = objClient.ContractNumber;
                cmd.Parameters.Add("@ContractName", SqlDbType.NVarChar).Value = objClient.ContractName;
                cmd.Parameters.Add("@ContractRegion", SqlDbType.NVarChar).Value = objClient.Expr1;
                cmd.Parameters.Add("@Territory", SqlDbType.NVarChar).Value = objClient.Territory;
                cmd.Parameters.Add("@SalesType", SqlDbType.NVarChar).Value = objClient.SalesType;

                cmd.Parameters.Add("@ISP", SqlDbType.NVarChar).Value = objClient.ISP;
                //cmd.Parameters.Add("@Currency", SqlDbType.NVarChar).Value = objClient.Currency;
                cmd.Parameters.Add("@RelationshipName", SqlDbType.NVarChar).Value = objClient.RelationshipName;
                cmd.Parameters.Add("@CCID", SqlDbType.Float).Value = objClient.CCID;
                cmd.Parameters.Add("@ISPpaid", SqlDbType.Bit).Value = objClient.ISPpaid;
                cmd.Parameters.Add("@LostBiz", SqlDbType.Bit).Value = objClient.LostBiz;
                cmd.Parameters.Add("@EstAnnualRev", SqlDbType.Real).Value = objClient.EstAnnualRev;
                cmd.Parameters.Add("@EstMarginPct", SqlDbType.Real).Value = objClient.EstMarginPct;
                cmd.Parameters.Add("@SecondaryNAICS", SqlDbType.NVarChar).Value = objClient.SecondaryNAICS;
                cmd.Parameters.Add("@Source", SqlDbType.NVarChar).Value = objClient.Source;
                cmd.Parameters.Add("@Acctnbr", SqlDbType.NVarChar).Value = objClient.Acctnbr;
                cmd.Parameters.Add("@StrategicSRID", SqlDbType.NVarChar).Value = objClient.StrategicSRID;
                cmd.Parameters.Add("@LocalSRID", SqlDbType.NVarChar).Value = objClient.LocalSRID;
                cmd.Parameters.Add("@BusinessType", SqlDbType.NVarChar).Value = objClient.BusinessType;
                cmd.Parameters.Add("@ProductType", SqlDbType.NVarChar).Value = objClient.ProductType;
                cmd.Parameters.Add("@AccountRegion", SqlDbType.NVarChar).Value = objClient.Region;
                cmd.Parameters.Add("@ShippingLocation", SqlDbType.NVarChar).Value = objClient.ShippingLocation;
                cmd.Parameters.Add("@SharePercent", SqlDbType.Real).Value = objClient.SharePercent;
                cmd.Parameters.Add("@gatewayID", SqlDbType.Int).Value = objClient.gatewayID;
                cmd.Parameters.Add("@MATHANDCPPID", SqlDbType.NVarChar).Value = objClient.MATHANCPPID;
                cmd.Parameters.Add("@RTNSID", SqlDbType.NVarChar).Value = objClient.RTNSID;
                cmd.Parameters.Add("@CDCPLBID", SqlDbType.NVarChar).Value = objClient.CDCPLBID;

                cmd.Parameters.Add("@Updatedby", SqlDbType.NVarChar).Value = username;
                cmd.Parameters.Add("@ActiveFlag", SqlDbType.Bit).Value = objClient.ActiveFlag;
                if (objClient.StartDate.HasValue)
                {
                    cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = objClient.StartDate;
                }
                else
                {
                    cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                if (objClient.EndDate.HasValue)
                {
                    cmd.Parameters.Add("@enddate", SqlDbType.NVarChar).Value = objClient.EndDate;
                }
                else
                {
                    cmd.Parameters.Add("@enddate", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                if (objClient.rolloff_quarter.HasValue)
                {
                    cmd.Parameters.Add("@rolloffqtr", SqlDbType.NVarChar).Value = objClient.rolloff_quarter;
                }
                else
                {
                    cmd.Parameters.Add("@rolloffqtr", SqlDbType.NVarChar).Value = DBNull.Value;
                }

                var returnParam2 = new SqlParameter
                {
                    ParameterName = "@Error",
                    Direction = ParameterDirection.Output,
                    Size = 1000
                };
                cmd.Parameters.Add(returnParam2);

                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                errorMsg = (string)cmd.Parameters["@Error"].Value;


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return errorMsg;
        }

        public string doSQLInsertCCUpdateCAC(ClientInfo objClient, string username)
        {
            string errorMsg = "";
            try
            {
                String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
                SqlConnection conn = new SqlConnection(strConnString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_InsertCCUpdateCAC";

                cmd.Parameters.Add("@ContractNumber", SqlDbType.NVarChar).Value = objClient.ContractNumber;
                cmd.Parameters.Add("@ContractName", SqlDbType.NVarChar).Value = objClient.ContractName;
                cmd.Parameters.Add("@ContractRegion", SqlDbType.NVarChar).Value = objClient.Expr1;
                cmd.Parameters.Add("@Territory", SqlDbType.NVarChar).Value = objClient.Territory;
                cmd.Parameters.Add("@SalesType", SqlDbType.NVarChar).Value = objClient.SalesType;

                cmd.Parameters.Add("@ISP", SqlDbType.NVarChar).Value = objClient.ISP;
                //cmd.Parameters.Add("@Currency", SqlDbType.NVarChar).Value = objClient.Currency;
                cmd.Parameters.Add("@RelationshipName", SqlDbType.NVarChar).Value = objClient.RelationshipName;
                cmd.Parameters.Add("@CCID", SqlDbType.Float).Value = objClient.CCID;
                cmd.Parameters.Add("@ISPpaid", SqlDbType.Bit).Value = objClient.ISPpaid;
                cmd.Parameters.Add("@LostBiz", SqlDbType.Bit).Value = objClient.LostBiz;
                cmd.Parameters.Add("@EstAnnualRev", SqlDbType.Real).Value = objClient.EstAnnualRev;
                cmd.Parameters.Add("@EstMarginPct", SqlDbType.Real).Value = objClient.EstMarginPct;
                cmd.Parameters.Add("@SecondaryNAICS", SqlDbType.NVarChar).Value = objClient.SecondaryNAICS;
                cmd.Parameters.Add("@Source", SqlDbType.NVarChar).Value = objClient.Source;
                cmd.Parameters.Add("@Acctnbr", SqlDbType.NVarChar).Value = objClient.Acctnbr;
                cmd.Parameters.Add("@StrategicSRID", SqlDbType.NVarChar).Value = objClient.StrategicSRID;
                cmd.Parameters.Add("@LocalSRID", SqlDbType.NVarChar).Value = objClient.LocalSRID;
                cmd.Parameters.Add("@BusinessType", SqlDbType.NVarChar).Value = objClient.BusinessType;
                cmd.Parameters.Add("@ProductType", SqlDbType.NVarChar).Value = objClient.ProductType;
                cmd.Parameters.Add("@AccountRegion", SqlDbType.NVarChar).Value = objClient.Region;
                cmd.Parameters.Add("@ShippingLocation", SqlDbType.NVarChar).Value = objClient.ShippingLocation;
                cmd.Parameters.Add("@SharePercent", SqlDbType.Real).Value = objClient.SharePercent;
                cmd.Parameters.Add("@gatewayID", SqlDbType.Int).Value = objClient.gatewayID;
                cmd.Parameters.Add("@MATHANDCPPID", SqlDbType.NVarChar).Value = objClient.MATHANCPPID;
                cmd.Parameters.Add("@RTNSID", SqlDbType.NVarChar).Value = objClient.RTNSID;
                cmd.Parameters.Add("@CDCPLBID", SqlDbType.NVarChar).Value = objClient.CDCPLBID;

                cmd.Parameters.Add("@Updatedby", SqlDbType.NVarChar).Value = username;
                cmd.Parameters.Add("@ActiveFlag", SqlDbType.Bit).Value = objClient.ActiveFlag;
                if (objClient.StartDate.HasValue)
                {
                    cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = objClient.StartDate;
                }
                else
                {
                    cmd.Parameters.Add("@startdate", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                if (objClient.EndDate.HasValue)
                {
                    cmd.Parameters.Add("@enddate", SqlDbType.NVarChar).Value = objClient.EndDate;
                }
                else
                {
                    cmd.Parameters.Add("@enddate", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                if (objClient.rolloff_quarter.HasValue)
                {
                    cmd.Parameters.Add("@rolloffqtr", SqlDbType.NVarChar).Value = objClient.rolloff_quarter;
                }
                else
                {
                    cmd.Parameters.Add("@rolloffqtr", SqlDbType.NVarChar).Value = DBNull.Value;
                }

                var returnParam2 = new SqlParameter
                {
                    ParameterName = "@Error",
                    Direction = ParameterDirection.Output,
                    Size = 1000
                };
                cmd.Parameters.Add(returnParam2);

                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                errorMsg = (string)cmd.Parameters["@Error"].Value;


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return errorMsg;
        }


    }


}