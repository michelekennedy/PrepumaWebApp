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
    public partial class MaintenanceSalesType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] == null || Session["appName"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            if (Session["userRole"].ToString().ToLower() == "audit")
            {
                Response.Redirect("Default.aspx");
            } 
            getSalesType();
        }

        private void getSalesType()
        {
            ClsSalesType oSalesType = new ClsSalesType();
            List<ClsSalesType> salesTypeList = oSalesType.GetSalesType();
            rgSalesType.DataSource = salesTypeList;
            //rgSalesType.DataBind();
        }

        protected void rgSalesType_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            getSalesType();
        }

        protected void rgSalesType_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                rgSalesType.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Sales Type Information";
                rgSalesType.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            else
            {
                rgSalesType.MasterTableView.EditFormSettings.CaptionFormatString = "Add Sales Type Information";
                rgSalesType.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                rgSalesType.ExportSettings.FileName = "SalesType";
                rgSalesType.AllowFilteringByColumn = false;
            }
        }

        protected void rgSalesType_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

                ClsSalesType oSalesType = new ClsSalesType();
                string insertMsg = "";
                if (IsValid)
                {
                    oSalesType.SalesType = (userControl.FindControl("txtSalesType") as RadTextBox).Text;
                    oSalesType.SalesDescription = (userControl.FindControl("txtSalesDescription") as RadTextBox).Text;
                    oSalesType.Createdby = (string)(Session["userName"]);
                    oSalesType.Updatedby = (string)(Session["userName"]);
                    oSalesType.ActiveFlag = true;


                    if (oSalesType != null)
                    {
                        insertMsg = ClsSalesType.InsertSalesType(oSalesType);
                        if (insertMsg == "")
                        {
                            windowManager.RadAlert("Successfully Added New SalesType with Sales Type as " + oSalesType.SalesType, 300, 250, "Success", "callBackFn", "");
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

        protected void rgSalesType_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            
        }
    }
}