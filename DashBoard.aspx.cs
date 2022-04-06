using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PrepumaWebApp.App_Data.DAL;

namespace PrepumaWebApp
{
    public partial class DashBoard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userName"] != null && Session["appName"] != null)
                {
                    getTotalContracts();
                    getTotalAccounts();
                    //getTotalUnasignedContracts();
                    getTotalUnasignedAccounts();
                    getPiechartData();
                    getColumnchartData();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void getTotalContracts()
        {
            int totalContracts = ClsContract.countContracts();
            lblTotalContracts.Text = totalContracts.ToString();
        }

        private void getTotalAccounts()
        {
            int totalAccounts = ClsAccount.countAccounts();
            lblTotalAccounts.Text = totalAccounts.ToString();
        }

        private void getTotalUnasignedContracts()
        {
            int totalUnassignedCon = ClsContract.numberUnAssignedContracts();
            //lblTotalUnassignedContracts.Text = totalUnassignedCon.ToString();
        }

        private void getTotalUnasignedAccounts()
        {
            int totalUnassignedCon = ClsAccount.numberUnAssignedAccounts();
            lblTotalUnassignedAccounts.Text = totalUnassignedCon.ToString();
        }

       
        private void getPiechartData()
        {
            SalesRepsByDistrict srbd = new SalesRepsByDistrict();
            List<SalesRepsByDistrict> series = srbd.getSalesRepsByDistrict();
            decimal total = series.Sum(x => x.salesRepsCount);
            foreach (SalesRepsByDistrict item in series)
            {
                item.salesRepsCount = Math.Round(((item.salesRepsCount / total) * 100), 2);              
            }

            PieChart1.DataSource = series;
            PieChart1.DataBind();
        }

        private void getColumnchartData()
        {
            UnassignedByDistrict unAssigned = new UnassignedByDistrict();
            List<UnassignedByDistrict> series = unAssigned.getUnassignedByDistrict();
            ColumnChart.DataSource = series;
            ColumnChart.DataBind();
        }
        
    }


}
