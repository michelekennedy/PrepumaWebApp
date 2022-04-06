using PrepumaWebApp.App_Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


namespace PrepumaWebApp
{
    public partial class MaintenanceRenewalRoutingCC : System.Web.UI.Page
    {
        PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Session["userName"] != null && Session["appName"] != null)
                {
                    getEmails();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void getEmails()
        {
            ClsKeyValuePair kvp = new ClsKeyValuePair();
            string ccEmail = kvp.GetKeyValue("ClientLogsCC").ToString();
            txtRoutingCCemail.Text = ccEmail;
        }

        protected void updateEmails(object sender, System.EventArgs e)
        {
            ClsKeyValuePair kvp = new ClsKeyValuePair();
            kvp.sKey = "ClientLogsCC";
            kvp.sValue = txtRoutingCCemail.Text;
            string retval = ClsKeyValuePair.UpdateKeyValue(kvp);
            if (retval != "")
            {
                pnlDanger.Visible = true;
                lblDanger.Text = retval;
            }
            else
            {
                Response.Redirect("SearchContractRenewal.aspx?filter=true");
            }
        }

        protected void cancelcode(object sender, System.EventArgs e)
        {
            Response.Redirect("SearchContractRenewal.aspx?filter=true");
        }
    }
}