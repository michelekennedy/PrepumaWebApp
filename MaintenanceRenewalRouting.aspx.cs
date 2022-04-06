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
    public partial class MaintenanceRenewalRouting : System.Web.UI.Page
    {
        PrepumaDataDataContext prepumaContext = new PrepumaDataDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                if (Session["userName"] != null && Session["appName"] != null)
                {

                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void getEmails()
        {
            List<ClsRenewalRouting> listEmails = ClsRenewalRouting.GetRenewalRouteListWInactive();
            rgGrid.DataSource = listEmails;

        }

        protected void rgGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            getEmails();

        }

        protected void rgGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
        }

        protected void rgGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                rgGrid.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Contract Renewal Routing Email Information";
                rgGrid.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            else
            {
                rgGrid.MasterTableView.EditFormSettings.CaptionFormatString = "Add Contract Renewal Routing Email Information";
                rgGrid.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                rgGrid.ExportSettings.FileName = "RenewalRouting";
                rgGrid.AllowFilteringByColumn = false;
            }
        }

        protected void rgGrid_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

                ClsRenewalRouting oRoute = new ClsRenewalRouting();
                string insertMsg = "";
                if (IsValid)
                {

                    oRoute.RoutingName = (userControl.FindControl("txtRoutingName") as RadTextBox).Text;
                    oRoute.RoutingEmail = (userControl.FindControl("txtRoutingEmail") as RadTextBox).Text;
                    oRoute.LoginID = (userControl.FindControl("txtLoginID") as RadTextBox).Text;
                    oRoute.CreatedBy = (string)(Session["userName"]);
                    oRoute.UpdatedBy = (string)(Session["userName"]);
                    oRoute.ActiveFlag = true;


                    if (oRoute != null)
                    {
                        insertMsg = oRoute.InsertRenewalRoute(oRoute);
                        if (insertMsg == "")
                        {
                            windowManager.RadAlert("Successfully Added New Contract Renewal Routing Email for " + "'" + oRoute.RoutingName + "'", 250, 250, "Success", "callBackFn", "");
                        }
                        else
                        {

                            errorMsg.Visible = true;
                            errorMsg.Text = insertMsg;
                            e.Canceled = true;
                        }

                    }
                }
                else
                {
                    // display error
                    errorMsg.Visible = true;
                    errorMsg.Text = "Please enter Required fields";
                }

            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }
        }

        protected void rgGrid_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

                ClsRenewalRouting oRoute = new ClsRenewalRouting();
                string insertMsg = "";
                if (IsValid)
                {
                    oRoute.idContractRenewalRouting = Convert.ToInt16((userControl.FindControl("lblRenewalRouteID") as Label).Text);
                    oRoute.RoutingName = (userControl.FindControl("txtRoutingName") as RadTextBox).Text;
                    oRoute.RoutingEmail = (userControl.FindControl("txtRoutingEmail") as RadTextBox).Text;
                    oRoute.LoginID = (userControl.FindControl("txtLoginID") as RadTextBox).Text;
                    oRoute.CreatedBy = (string)(Session["userName"]);
                    oRoute.UpdatedBy = (string)(Session["userName"]);
                    oRoute.ActiveFlag = (userControl.FindControl("ActiveFlag") as RadButton).Checked;


                    if (oRoute != null)
                    {
                        insertMsg = oRoute.UpdateRenewalRoute(oRoute);
                        if (insertMsg == "")
                        {
                            windowManager.RadAlert("Successfully Updated New Contract Renewal Routing Info for " + "'" + oRoute.RoutingName + "'", 250, 250, "Success", "callBackFn", "");
                        }
                        else
                        {

                            errorMsg.Visible = true;
                            errorMsg.Text = insertMsg;
                            e.Canceled = true;
                        }

                    }
                }
                else
                {
                    // display error
                    errorMsg.Visible = true;
                    errorMsg.Text = "Please enter Required fields";
                }

            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }
        }
    }
}