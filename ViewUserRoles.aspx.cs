using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using PI_BusinessClasses;
using PrepumaWebApp.App_Data.DAL;
using System.Text;
using PI_Application;
using Telerik.Web.UI;
using Telerik.Web.UI.Upload;
using System.Diagnostics;
using System.Configuration;


namespace PrepumaWebApp
{


    public partial class ViewUserRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



        }
        protected void rgUsers_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            loadUsers();


        }

        protected void loadUsers()
        {
            ClsUserRoles users = new ClsUserRoles();
            List<ClsUserRoles> appusers = users.GetListClsAppUsers((string)Session["appName"]);
            rgUsers.DataSource = appusers;


        }
    }
}