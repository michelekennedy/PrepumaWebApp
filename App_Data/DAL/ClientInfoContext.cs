using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PrepumaWebApp.App_Data.DAL
{
    public class ClientInfoContext
    {
        public List<ClientInfo> clientData { get; set; }

        public ClientInfoContext()
        {
            this.GetData();
        }

        public List<ClientInfo> GetClientInfo()
        {
            String CS = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
            List<ClientInfo> clientInfolist = new List<ClientInfo>();
            SqlDataReader rd = null;
            SqlConnection con = new SqlConnection(CS);
            string query = "sp_vw_PrePumaData ";
            try
            {
                SqlCommand sqlcmd = new SqlCommand(query, con);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                rd = sqlcmd.ExecuteReader();

                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        ClientInfo ci = new ClientInfo();
                        
                        ci.ClientID = (Double)rd["ClientID"];
                        ci.ClientName = rd["ClientName"].ToString();
                        ci.ContractID = (Double)rd["ContractID"];
                        ci.ContractNumber = rd["ContractNumber"].ToString();
                        ci.ContractName = rd["ContractName"].ToString();
                        ci.AcctID = (Double)rd["AcctID"];
                        ci.Acctnbr = rd["Acctnbr"].ToString();
                        ci.BusinessType = rd["BusinessType"].ToString();
                        ci.LocalSRID = rd["LocalSRID"].ToString();
                        ci.ProductType = rd["ProductType"].ToString();
                        ci.StrategicSRID = rd["StrategicSRID"].ToString();
                        //ci.Currency = rd["Currency"].ToString();
                        //ci.Region = rd["Region"].ToString();
                        //ci.ShippingLocation = rd["ShippingLocation"].ToString();
                        //ci.SharePercent = rd["SharePercent"].ToString().Length > 0 ? (Single)rd["SharePercent"] : 0;
                        clientInfolist.Add(ci);
                    }
                }

            }
            catch (Exception ex)
            {
                clientInfolist = null;
                throw ex;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return clientInfolist;
        }
        public List<ClientInfo> GetData()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            clientData = (from data in prepumaContext.vw_PrePumaDatas
                               select new ClientInfo
                               {
                                   ClientID = (Double)data.ClientID,
                                   ClientName = data.ClientName,
                                   ContractID = (Double)data.ContractID,
                                   ContractName = data.ContractName,
                                   ContractNumber = data.ContractNumber,
                                   AcctID = (Double)data.AcctID,
                                   Acctnbr = data.Acctnbr,
                                   StrategicSRID = data.StrategicSRID,
                                   LocalSRID = data.LocalSRID,
                                   BusinessType = data.BusinessType,
                                   ProductType = data.ProductType,
                                   Territory = data.Territory,
                                   SalesType = data.SalesType,
                                   gatewayID = data.gatewayID,
                                   gateway = data.gateway,
                                   MATHANCPPID = data.MATHANDCPPID,
                                   MaterialHandling = data.MaterialHandling,
                                   RTNSID = data.RTNSID,
                                   Returns = data.Returns,
                                   CDCPLBID = data.CDCPLBID,
                                   CDCPLB = (Double)data.CDCPLB,
                                   CCID = data.CCID,
                                   Region=data.Region,
                                   ISP=data.ISP,
                                   Expr1 = data.Expr1,
                                   ShippingLocation = data.ShippingLocation,
                                   RelationshipName=data.RelationshipName,
                                   ISPpaid=data.ISPpaid,
                                   LostBiz=data.LostBiz,
                                   EstAnnualRev=data.EstAnnualRev,
                                   EstMarginPct =data.EstMarginPct,
                                   SecondaryNAICS=data.SecondaryNAICS,
                                   Source=data.Source,
                                   SharePercent=data.SharePercent,
                                   Updatedby=data.Updatedby,
                                   AcctUpdatedby=data.AcctUpdatedBy,
                                   AcctUpdatedOn=Convert.ToDateTime(data.AcctUpdatedOn),
                                   InfoUpdatedby = data.InfoUpdatedby,
                                   InfoUpdatedOn = Convert.ToDateTime(data.InfoUpdatedOn),
                                   StartDate = Convert.ToDateTime(data.StartDate),
                                   EndDate = Convert.ToDateTime(data.EndDate),
                                   rolloff_quarter = Convert.ToDateTime(data.rolloff_quarter),
                                   //Currency = data.Currency,
                                   LocalRepEmployeeID = data.LocalRepEmployeeID,
                                   StrategicRepEmployeeID = data.StrategicRepEmployeeID,
                                   MCNumber = data.MCNumber,
                                   MissionCriticalSRID = data.MissionCriticalSRID
                               }).ToList<ClientInfo>();

            return clientData;
        }


        public List<ClientInfo> GetClientNames()
        {
            //List<string> names = new List<string>();

            //names = clientData.Select(x => x.ClientName).ToList();

            //return names;
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            clientData = (from data in prepumaContext.vw_PrePumaDatas
                          orderby data.ClientID
                          select new ClientInfo
                          {
                              ClientID = (Double)data.ClientID,
                              ClientName = data.ClientName,
                              
                          }).Distinct().ToList<ClientInfo>();

            return clientData;
        }

        public List<ClsClient> GetClNames()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            List<ClsClient> oClient = (from data in prepumaContext.GetTable<tblclient>()
                                       select new ClsClient
                                       {
                                           ClientID = (Double)data.ClientID,
                                           ClientName = data.ClientName,
                                       }).ToList<ClsClient>();
            return oClient;
        }

        public List<ClientInfo> getUnAssignedContracts(List<string> listContractNumbers)
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            ClientInfoContext ctxt = new ClientInfoContext();
            List<ClientInfo> getUnAssignedContracts = ctxt.GetData();
            
            //foreach (string conNum in listContractNumbers)
            //{
            //    if (conNum != null)
            //    {
            //        getUnAssignedContracts = getUnAssignedContracts.FindAll(x => x.ContractNumber == conNum);
            //    }
                
            //}
            return getUnAssignedContracts;
        }

        public List<ClientInfo> GetUnassignedData()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            clientData = (from data in prepumaContext.vw_PrePumaDatas
                         where !(from cac in prepumaContext.GetTable<tblContractAccountClassification>() select cac.Acctnbr).Contains(data.Acctnbr) && data.Acctnbr != ""
                          orderby data.ClientName,data.ContractNumber,data.Acctnbr
                          select new ClientInfo
                          {
                              ClientID = (Double)data.ClientID,
                              ClientName = data.ClientName,
                              ContractID = (Double)data.ContractID,
                              ContractName = data.ContractName,
                              ContractNumber = data.ContractNumber,
                              AcctID = (Double)data.AcctID,
                              Acctnbr = data.Acctnbr,
                              StrategicSRID = data.StrategicSRID,
                              LocalSRID = data.LocalSRID,
                              BusinessType = data.BusinessType,
                              ProductType = data.ProductType,
                              Territory = data.Territory,
                              SalesType = data.SalesType,
                              CCID = data.CCID,
                              Region = data.Region,
                              ISP = data.ISP,
                              Expr1 = data.Expr1,
                              ShippingLocation = data.ShippingLocation,
                              RelationshipName = data.RelationshipName,
                              ISPpaid = data.ISPpaid,
                              LostBiz = data.LostBiz,
                              EstAnnualRev = data.EstAnnualRev,
                              EstMarginPct = data.EstMarginPct,
                              SecondaryNAICS = data.SecondaryNAICS,
                              Source = data.Source,
                              SharePercent = data.SharePercent,
                              Updatedby = data.Updatedby,
                              StartDate = Convert.ToDateTime(data.StartDate),
                              EndDate = Convert.ToDateTime(data.EndDate),
                              rolloff_quarter = Convert.ToDateTime(data.rolloff_quarter)

                          }).ToList<ClientInfo>();

            return clientData;
        }

        public static int numberMissingRegion()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            int totalMissingAccount = (from data in prepumaContext.vw_PrePumaDatas
                                       where data.Region != null && data.Expr1 == null
                                       orderby data.ClientName, data.ContractNumber
                                       select new ClientInfo
                                       {

                                           ContractName = data.ContractName,

                                       }).ToList<ClientInfo>().Count();
            return totalMissingAccount;
        }

        public List<ClientInfo> GetDataMissingRegion()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            clientData = (from data in prepumaContext.vw_PrePumaDatas
                          where data.Region != null && data.Expr1 == null
                          orderby data.ClientName, data.ContractNumber, data.Acctnbr
                          select new ClientInfo
                          {
                              ClientID = (Double)data.ClientID,
                              ClientName = data.ClientName,
                              ContractID = (Double)data.ContractID,
                              ContractName = data.ContractName,
                              ContractNumber = data.ContractNumber,
                              AcctID = (Double)data.AcctID,
                              Acctnbr = data.Acctnbr,
                              StrategicSRID = data.StrategicSRID,
                              LocalSRID = data.LocalSRID,
                              BusinessType = data.BusinessType,
                              ProductType = data.ProductType,
                              Territory = data.Territory,
                              SalesType = data.SalesType,
                              CCID = data.CCID,
                              Region = data.Region,
                              ISP = data.ISP,
                              Expr1 = data.Expr1,
                              ShippingLocation = data.ShippingLocation,
                              RelationshipName = data.RelationshipName,
                              ISPpaid = data.ISPpaid,
                              LostBiz = data.LostBiz,
                              EstAnnualRev = data.EstAnnualRev,
                              EstMarginPct = data.EstMarginPct,
                              SecondaryNAICS = data.SecondaryNAICS,
                              Source = data.Source,
                              SharePercent = data.SharePercent,
                              Updatedby = data.Updatedby,
                              //Currency = data.Currency

                          }).ToList<ClientInfo>();

            return clientData;
        }

       

        public static int numberMissingAccount()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();
            int totalMissingAccount = (from data in prepumaContext.vw_PrePumaDatas
                                       where data.Acctnbr == null
                                       orderby data.ClientName, data.ContractNumber
                                       select new ClientInfo
                                       {
                                          
                                           ContractName = data.ContractName,
                                           
                                       }).ToList<ClientInfo>().Count();
            return totalMissingAccount;
        }


        public List<ClientInfo> GetDataMissingAccount()
        {
            PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

            clientData = (from data in prepumaContext.vw_PrePumaDatas
                          where data.Acctnbr == null
                          orderby data.ClientName, data.ContractNumber
                          select new ClientInfo
                          {
                              ClientID = (Double)data.ClientID,
                              ClientName = data.ClientName,
                              ContractID = (Double)data.ContractID,
                              ContractName = data.ContractName,
                              ContractNumber = data.ContractNumber,
                              AcctID = (Double)data.AcctID,
                              Acctnbr = data.Acctnbr,
                              StrategicSRID = data.StrategicSRID,
                              LocalSRID = data.LocalSRID,
                              BusinessType = data.BusinessType,
                              ProductType = data.ProductType,
                              Territory = data.Territory,
                              SalesType = data.SalesType,
                              CCID = data.CCID,
                              Region = data.Region,
                              ISP = data.ISP,
                              Expr1 = data.Expr1,
                              ShippingLocation = data.ShippingLocation,
                              RelationshipName = data.RelationshipName,
                              ISPpaid = data.ISPpaid,
                              LostBiz = data.LostBiz,
                              EstAnnualRev = data.EstAnnualRev,
                              EstMarginPct = data.EstMarginPct,
                              SecondaryNAICS = data.SecondaryNAICS,
                              Source = data.Source,
                              SharePercent = data.SharePercent,
                              Updatedby = data.Updatedby,
                              //Currency = data.Currency

                          }).ToList<ClientInfo>();

            return clientData;
        }

     }
}