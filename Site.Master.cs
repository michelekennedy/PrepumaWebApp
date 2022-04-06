using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Configuration;

namespace PrepumaWebApp
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //Development Server Branding
            string develProdImagePath = ConfigurationManager.AppSettings["develProdImage"].ToString();
            Image Image2 = new Image();
            Image2.ImageUrl = develProdImagePath;

            PlaceHolder1.Controls.Add(Image2);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] == null || Session["appName"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            if (((string)Session["userRole"]).ToLower() == "admin" || ((string)Session["userRole"]).ToLower() == "itadmin")
            {
                //add User Login Menu for admin  users
                RadMenuItem usermaint = new RadMenuItem();
                usermaint.Text = "Edit User Logins";
                usermaint.NavigateUrl = "~/UserSecurity.aspx";
                RadMenuItem maint = (RadMenuItem)RadMenu1.FindItemByText("Data Maintenance");
                maint.Items.Add(usermaint);
                
                //Account Import menu
                RadMenuItem sep = new RadMenuItem();
                sep.IsSeparator=true;
                RadMenu1.Items.Add(sep);
                RadMenu1.Items.Add(new RadMenuItem("Account Utilities"));
                RadMenuItem item = (RadMenuItem)RadMenu1.FindItemByText("Account Utilities");
                RadMenuItem logistics = new RadMenuItem("Logistics");
                logistics.Text = "Logistics Import";
                logistics.NavigateUrl = "~/LogisticsImport.aspx";
                item.Items.Add(logistics);                
                RadMenuItem ac = new RadMenuItem();
                ac.Text = "Account Check";
                ac.NavigateUrl = "~/AccountCheck.aspx";
                //RadMenuItem acct = (RadMenuItem)RadMenu1.FindItemByText("Account Utilities");
                item.Items.Add(ac);
                RadMenuItem contractsearch = new RadMenuItem();
                contractsearch.Text = "Contract Search";
                contractsearch.NavigateUrl = "~/ContractSearch.aspx";
                item.Items.Add(contractsearch);
                RadMenuItem qe = new RadMenuItem();
                qe.Text = "Quarter End Alignment Save";
                qe.NavigateUrl = "~/QuarterEndAcctSave";
                item.Items.Add(sep);
                item.Items.Add(qe);
                RadMenuItem ar = new RadMenuItem();
                ar.Text = "Account Realignment Utlity";
                ar.NavigateUrl = "~/AccountRealignment";
                item.Items.Add(ar);

                //Account Revenue Check
                RadMenuItem arc = new RadMenuItem();
                arc.Text = "Account Revenue Check";
                arc.NavigateUrl = "~/AccountRevenueCheck";
                item.Items.Add(arc);

               
                //report menu
                RadMenuItem sep2 = new RadMenuItem();
                sep2.IsSeparator = true;
                RadMenu1.Items.Add(sep2);
                RadMenu1.Items.Add(new RadMenuItem("Reports"));
                RadMenuItem rpts = (RadMenuItem)RadMenu1.FindItemByText("Reports");
                RadMenuItem margin = new RadMenuItem("Margin");
                margin.Text = "Margin Report";
                margin.NavigateUrl = "~/MarginReport.aspx";
                rpts.Items.Add(margin);




            }

            


            //Maintenance
            if (((string)Session["userRole"]).ToLower() != "audit")
            {              
                RadMenuItem reg = new RadMenuItem();
                reg.Text = "Regions";
                reg.NavigateUrl = "~/MaintenanceRegions.aspx";
                RadMenuItem srid = new RadMenuItem();
                srid.Text = "Salesperson IDs";
                srid.NavigateUrl = "~/MaintenanceSalesReps.aspx";
                RadMenuItem srtitle = new RadMenuItem();
                srtitle.Text = "Salesperson Titles";
                srtitle.NavigateUrl = "~/MaintenanceSalesRepTitles.aspx";


                RadMenuItem maint = (RadMenuItem)RadMenu1.FindItemByText("Data Maintenance");

                maint.Items.Add(srid);
                maint.Items.Add(srtitle);
                maint.Items.Add(reg);

                RadMenuItem amaint = (RadMenuItem)RadMenu1.FindItemByText("Account Maintenance");
                RadMenuItem una = new RadMenuItem();
                una.Text = "Unassigned Accounts";
                una.NavigateUrl = "~/UnassignedAccounts.aspx";
                amaint.Items.Add(una);
            }

            //remove menus for Contracts and Pricing, and just add the Client Log Menus
            if (((string)Session["userRole"]).ToLower() == "pricing" || ((string)Session["userRole"]).ToLower() == "contracts" || ((string)Session["userRole"]).ToLower() == "clientlogapproval")
            {
                RadMenuItem maint = (RadMenuItem)RadMenu1.FindItemByText("Data Maintenance");
                RadMenuItem item = (RadMenuItem)RadMenu1.FindItemByText("Account Utilities");
                RadMenu1.Items.Clear();
                
            }


            //Client Logs Maintenance
            //if (((string)Session["userRole"]).ToLower() == "pricing" || ((string)Session["userRole"]).ToLower() == "contracts")
            if (((string)Session["userRole"]).ToLower() == "contracts")
            {
                RadMenuItem sep = new RadMenuItem();
                sep.IsSeparator = true;
                RadMenu1.Items.Add(sep);
                RadMenu1.Items.Add(new RadMenuItem("Client Log Maintenance"));
                RadMenuItem item = (RadMenuItem)RadMenu1.FindItemByText("Client Log Maintenance");

                RadMenuItem crr = new RadMenuItem();
                crr.Text = "Contract Renewal Emails";
                crr.NavigateUrl = "~/MaintenanceRenewalRouting.aspx";
                item.Items.Add(crr);

                RadMenuItem cc = new RadMenuItem();
                cc.Text = "Contract Renewal Emails CC";
                cc.NavigateUrl = "~/MaintenanceRenewalRoutingCC.aspx";
                item.Items.Add(cc);


                RadMenuItem crt = new RadMenuItem();
                crt.Text = "Contract Renewal Type";
                crt.NavigateUrl = "~/MaintenanceRenewalType.aspx";
                RadMenuItem maint = (RadMenuItem)RadMenu1.FindItemByText("Data Maintenance");

                item.Items.Add(crt);
                if (((string)Session["userRole"]).ToLower() == "contracts")
                {
                    RadMenuItem cru = new RadMenuItem();
                    cru.Text = "SAP Accessorial Import";
                    cru.NavigateUrl = "~/ImportAccessorials.aspx";
                    item.Items.Add(sep);
                    item.Items.Add(cru);
                }
            }

            //Client Logs Search
            if (((string)Session["userRole"]).ToLower() == "pricing" || ((string)Session["userRole"]).ToLower() == "contracts" || ((string)Session["userRole"]).ToLower() == "clientlogapproval")
            {
                RadMenuItem sep = new RadMenuItem();
                sep.IsSeparator = true;
                RadMenu1.Items.Add(sep);
                RadMenu1.Items.Add(new RadMenuItem("Client Logs"));
                RadMenuItem item = (RadMenuItem)RadMenu1.FindItemByText("Client Logs");

                RadMenuItem searchMy = new RadMenuItem();
                searchMy.Text = "My Client Logs";
                searchMy.NavigateUrl = "~/SearchContractRenewal.aspx?filter=true";
                item.Items.Add(sep);
                item.Items.Add(searchMy);

                RadMenuItem searchAll = new RadMenuItem();
                searchAll.Text = "Search All Client Logs";
                searchAll.NavigateUrl = "~/SearchContractRenewal.aspx";
                item.Items.Add(sep);
                item.Items.Add(searchAll);
            }

            //Client Logs Reporting
            if (((string)Session["userRole"]).ToLower() == "pricing" || ((string)Session["userRole"]).ToLower() == "contracts")
            {
                RadMenuItem sep2 = new RadMenuItem();
                sep2.IsSeparator = true;
                RadMenu1.Items.Add(sep2);
                RadMenu1.Items.Add(new RadMenuItem("Client Log Reports"));
                RadMenuItem rpts = (RadMenuItem)RadMenu1.FindItemByText("Client Log Reports");
                RadMenuItem det = new RadMenuItem();
                det.Text = "Client Log Detail";
                det.NavigateUrl = "~/ClientLogDetail.aspx";
                rpts.Items.Add(det);
                //Renewal Sheet
                if (((string)Session["userRole"]).ToLower() == "contracts")
                {
                    RadMenuItem renewal = new RadMenuItem();
                    renewal.Text = "Renewal Sheet";
                    renewal.NavigateUrl = "~/RenewalSheet.aspx";
                    rpts.Items.Add(renewal);
                }
            }

         


            string menuName = Request.Url.PathAndQuery;
            menuName = menuName + ".aspx";
            RadMenuItem currentItem = RadMenu1.FindItemByUrl(menuName);

            if (currentItem != null)
            {
                //Select the current item and his parents
                currentItem.HighlightPath();
            }
            else
                RadMenu1.Items[0].HighlightPath();
            
        }

       

        
        
    }

}