using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Configuration;

namespace PrepumaWebApp
{
    public partial class SiteWide : System.Web.UI.MasterPage
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

        }
    }
}