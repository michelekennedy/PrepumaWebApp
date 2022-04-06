using PrepumaWebApp.App_Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace PrepumaWebApp
{
    public partial class View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ClientInfoContext ctxt = new ClientInfoContext();
            string ContractNumber = Request.QueryString[1];
            string AccountNumber = Request.QueryString[2];
            if (Request.QueryString["ClientID"] != null)
            {
                string clientId = Request.QueryString["ClientID"].Trim();
                List<ClientInfo> clientData = ctxt.GetData();
                List<ClientInfo> clientlist = clientData.FindAll(x => x.ClientName == clientId);

                try
                {
                    if (clientlist != null && ContractNumber != null)
                    {
                        List<ClientInfo> clientconlist = clientData.FindAll(x => x.ContractNumber == ContractNumber);
                        if (clientconlist != null && AccountNumber != null && AccountNumber != "&nbsp;")
                        {
                            List<ClientInfo> clientacclist = clientData.FindAll(x => x.Acctnbr == AccountNumber);

                            fillInData(clientacclist);
                            //foreach (ClientInfo client in clientacclist)
                            //{
                            //    lblClientName.Text = client.ClientName;
                            //    lblContractName.Text = client.ContractName;
                            //    lblContractNumber.Text = client.ContractNumber;
                            //    lblAccountNumber.Text = client.Acctnbr;
                            //    lblStrategicSRID.Text = client.StrategicSRID;
                            //    lblLocalSRID.Text = client.LocalSRID;
                            //    lblBusinessType.Text = client.BusinessType;
                            //    lblProductType.Text = client.ProductType;
                            //    lblCCID.Text = Convert.ToString(client.CCID);
                            //    lblExpr1.Text = client.Expr1;
                            //    lblTerritory.Text = client.Territory;
                            //    lblSalesType.Text = client.SalesType;
                            //    ClsGATEWAY gatewayInfo = new ClsGATEWAY().GetGatewayInfo().FirstOrDefault(x => x.gatewayID == client.gatewayID);
                            //    lblGatewayID.Text = gatewayInfo != null ? gatewayInfo.gateway : string.Empty;
                            //    ClsMATHANCPP mathancppInfo = new ClsMATHANCPP().GetMathandcppInfo().FirstOrDefault(x => x.CPPMATHANID == client.MATHANCPPID);
                            //    lblMATHANCPPID.Text = mathancppInfo != null ? mathancppInfo.mathancppId : string.Empty; 
                            //    ClsRTNSCPP rtnsidInfo = new ClsRTNSCPP().GetRtnsIdInfo().FirstOrDefault(x => x.RTNSID == client.RTNSID);
                            //    lblRTNSID.Text = rtnsidInfo != null? rtnsidInfo.rtnsIdInfo:string.Empty;
                            //    ClsCrossDockCPLB cdcplbInfo = new ClsCrossDockCPLB().GetcdcplbIdInfo().FirstOrDefault(x => x.CDCPLBID == client.CDCPLBID);
                            //    lblCDCPLBID.Text = cdcplbInfo != null ? Convert.ToString(cdcplbInfo.CDCPLB) : string.Empty;

                            //    lblISP.Text = client.ISP;
                            //    lblRelationshipName.Text = client.RelationshipName;
                            //    if (client.ISPpaid == false)
                            //    {
                            //        lblISPpaid.Text = "No";
                            //    }
                            //    else
                            //    {
                            //        lblISPpaid.Text = "Yes";
                            //    }
                            //    if (client.LostBiz == false)
                            //    {
                            //        lblLostBiz.Text = "No";
                            //    }
                            //    else
                            //    {
                            //        lblLostBiz.Text = "Yes";
                            //    }
                            //    lblRegion.Text = client.Region;
                            //    lblShippingLocation.Text = client.ShippingLocation;
                            //    lblEstAnnualRev.Text = "$ " + Convert.ToString(client.EstAnnualRev);
                            //    lblEstMarginPct.Text = Convert.ToString(client.EstMarginPct) + " %";
                            //    if (client.SharePercent <= 1)
                            //    {
                            //        lblSharePercent.Text = Convert.ToString(client.SharePercent * 100) + " %"; 
                            //    }
                            //    else { lblSharePercent.Text = Convert.ToString(client.SharePercent) + " %"; }

                            //    lblStartDate.Text = formatDate(client.StartDate);
                            //    lblEndDate.Text = formatDate(client.EndDate); 
                            //    lblRolloffQtr.Text = formatDate(client.rolloff_quarter); 
                               
                            //    lblUpdatedBy.Text = client.AcctUpdatedby;
                            //    lblUpdatedOn.Text = Convert.ToString(client.AcctUpdatedOn);
                            //}
                        }
                        else
                        {
                            fillInData(clientconlist);
                            //foreach (ClientInfo client in clientconlist)
                            //{
                            //    lblClientName.Text = client.ClientName;
                            //    lblContractName.Text = client.ContractName;
                            //    lblContractNumber.Text = client.ContractNumber;
                            //    lblAccountNumber.Text = client.Acctnbr;
                            //    lblStrategicSRID.Text = client.StrategicSRID;
                            //    lblLocalSRID.Text = client.LocalSRID;
                            //    lblBusinessType.Text = client.BusinessType;
                            //    lblProductType.Text = client.ProductType;
                            //    lblCCID.Text = Convert.ToString(client.CCID);
                            //    lblExpr1.Text = client.Expr1;
                            //    lblTerritory.Text = client.Territory;
                            //    lblSalesType.Text = client.SalesType;
                            //    ClsGATEWAY gatewayInfo = new ClsGATEWAY().GetGatewayInfo().FirstOrDefault(x => x.gatewayID == client.gatewayID);
                            //    lblGatewayID.Text = gatewayInfo != null ? gatewayInfo.gateway : string.Empty;
                            //    ClsMATHANCPP mathancppInfo = new ClsMATHANCPP().GetMathandcppInfo().FirstOrDefault(x => x.CPPMATHANID == client.MATHANCPPID);
                            //    lblMATHANCPPID.Text = mathancppInfo != null ? mathancppInfo.mathancppId : string.Empty;
                            //    ClsRTNSCPP rtnsidInfo = new ClsRTNSCPP().GetRtnsIdInfo().FirstOrDefault(x => x.RTNSID == client.RTNSID);
                            //    lblRTNSID.Text = rtnsidInfo != null ? rtnsidInfo.rtnsIdInfo : string.Empty;
                            //    ClsCrossDockCPLB cdcplbInfo = new ClsCrossDockCPLB().GetcdcplbIdInfo().FirstOrDefault(x => x.CDCPLBID == client.CDCPLBID);
                            //    lblCDCPLBID.Text = cdcplbInfo != null ? Convert.ToString(cdcplbInfo.CDCPLB) : string.Empty;

                            //    lblISP.Text = client.ISP;
                            //    lblRelationshipName.Text = client.RelationshipName;
                            //    if (client.ISPpaid == false)
                            //    {
                            //        lblISPpaid.Text = "No";
                            //    }
                            //    else
                            //    {
                            //        lblISPpaid.Text = "Yes";
                            //    }
                            //    if (client.LostBiz == false)
                            //    {
                            //        lblLostBiz.Text = "No";
                            //    }
                            //    else
                            //    {
                            //        lblLostBiz.Text = "Yes";
                            //    }
                            //    lblRegion.Text = client.Region;
                            //    lblShippingLocation.Text = client.ShippingLocation;
                            //    lblEstAnnualRev.Text = "$ " + Convert.ToString(client.EstAnnualRev);
                            //    lblEstMarginPct.Text = Convert.ToString(client.EstMarginPct) + " %";
                            //    if (client.SharePercent == 1)
                            //    {
                            //        lblSharePercent.Text = 100 + " %";
                            //    }
                            //    else { lblSharePercent.Text = Convert.ToString(client.SharePercent*100) + " %"; }
                            //    lblStartDate.Text = Convert.ToString(client.StartDate);
                            //    lblEndDate.Text = Convert.ToString(client.EndDate);
                            //    lblRolloffQtr.Text = Convert.ToString(client.rolloff_quarter);
                            //    lblUpdatedBy.Text = client.AcctUpdatedby;
                            //    lblUpdatedOn.Text = Convert.ToString(client.AcctUpdatedOn);
                            //}
                        }
                    }

                }
                catch (Exception ex)
                {
                    pnlDanger.Visible = true;
                    lblDanger.Text = ex.Message.ToString();
                }

            }
        }

        protected void fillInData(List<ClientInfo> clientconlist)
        {
            foreach (ClientInfo client in clientconlist)
            {
                lblClientName.Text = client.ClientName;
                lblContractName.Text = client.ContractName;
                lblContractNumber.Text = client.ContractNumber;
                lblAccountNumber.Text = client.Acctnbr;
                lblStrategicSRID.Text = client.StrategicSRID;
                lblLocalSRID.Text = client.LocalSRID;
                lblBusinessType.Text = client.BusinessType;
                lblProductType.Text = client.ProductType;
                //lblCCID.Text = Convert.ToString(client.CCID);
                lblExpr1.Text = client.Expr1;
                lblTerritory.Text = client.Territory;
                lblSalesType.Text = client.SalesType;
                ClsGATEWAY gatewayInfo = new ClsGATEWAY().GetGatewayInfo().FirstOrDefault(x => x.gatewayID == client.gatewayID);
                lblGatewayID.Text = gatewayInfo != null ? gatewayInfo.gateway : string.Empty;
                ClsMATHANCPP mathancppInfo = new ClsMATHANCPP().GetMathandcppInfo().FirstOrDefault(x => x.CPPMATHANID == client.MATHANCPPID);
                lblMATHANCPPID.Text = mathancppInfo != null ? mathancppInfo.mathancppId : string.Empty;
                ClsRTNSCPP rtnsidInfo = new ClsRTNSCPP().GetRtnsIdInfo().FirstOrDefault(x => x.RTNSID == client.RTNSID);
                lblRTNSID.Text = rtnsidInfo != null ? rtnsidInfo.rtnsIdInfo : string.Empty;
                ClsCrossDockCPLB cdcplbInfo = new ClsCrossDockCPLB().GetcdcplbIdInfo().FirstOrDefault(x => x.CDCPLBID == client.CDCPLBID);
                lblCDCPLBID.Text = cdcplbInfo != null ? Convert.ToString(cdcplbInfo.CDCPLB) : string.Empty;

                lblISP.Text = client.ISP;
                lblRelationshipName.Text = client.RelationshipName;
                if (client.ISPpaid == false)
                {
                    lblISPpaid.Text = "No";
                }
                else
                {
                    lblISPpaid.Text = "Yes";
                }
                if (client.LostBiz == false)
                {
                    lblLostBiz.Text = "No";
                }
                else
                {
                    lblLostBiz.Text = "Yes";
                }
                lblRegion.Text = client.Region;
                lblShippingLocation.Text = client.ShippingLocation;
                lblEstAnnualRev.Text = "$ " + Convert.ToString(client.EstAnnualRev);
                lblEstMarginPct.Text = Convert.ToString(client.EstMarginPct) + " %";
                if (client.SharePercent <= 1)
                {
                    lblSharePercent.Text = Convert.ToString(client.SharePercent * 100) + " %";
                }
                else { lblSharePercent.Text = Convert.ToString(client.SharePercent) + " %"; }
                lblStartDate.Text = formatDate(client.StartDate);
                lblEndDate.Text = formatDate(client.EndDate); 
                lblRolloffQtr.Text = formatDate(client.rolloff_quarter); 
                lblUpdatedBy.Text = client.InfoUpdatedby;
                lblUpdatedOn.Text = Convert.ToString(client.InfoUpdatedOn);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (lblEstAnnualRev.Text == "$ 0")
            {
                lblEstAnnualRev.Text = string.Empty;
            }
            if (lblEstMarginPct.Text == "0 %")
            {
                lblEstMarginPct.Text = string.Empty;
            }
            ClientScript.RegisterClientScriptBlock(this.GetType(), "key", "window.print()", true);
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "key", "window.close()", true);
        }

        protected string formatDate(DateTime? thisdate) 
        {
            string sdt = "";
            if(!String.IsNullOrEmpty(thisdate.ToString()))
            {
              DateTime dt = Convert.ToDateTime(thisdate);
              sdt = dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
            return sdt;
        }
             
        
    }
}