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
    public partial class MaintenanceBizType : System.Web.UI.Page
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
             getBizType();
        }

        private void getBizType()
        {
            ClsBusinessType oBizType = new ClsBusinessType();
            List<ClsBusinessType> bizTypelist = oBizType.GetBizType();
            rgBizType.DataSource = bizTypelist;
        }

        protected void rgBizType_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            getBizType();
        }

        protected void rgBizType_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                rgBizType.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Business Type Information";
                rgBizType.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            else
            {
                rgBizType.MasterTableView.EditFormSettings.CaptionFormatString = "Add Business Type Information";
                rgBizType.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                rgBizType.ExportSettings.FileName = "BusinessType";
                rgBizType.AllowFilteringByColumn = false;
            }
        }

        protected void rgBizType_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

                ClsBusinessType oBizType = new ClsBusinessType();
                string insertMsg = "";
                if (IsValid)
                {
                    oBizType.BusinessType = (userControl.FindControl("txtBizType") as RadTextBox).Text;
                    oBizType.BusinessDesc = (userControl.FindControl("txtbizDesc") as RadTextBox).Text;
                    oBizType.Createdby = (string)(Session["userName"]);
                    oBizType.Updatedby = (string)(Session["userName"]);
                    oBizType.ActiveFlag = true;


                    if (oBizType != null)
                    {
                        insertMsg = ClsBusinessType.InsertBizType(oBizType);
                        if (insertMsg == "")
                        {
                            windowManager.RadAlert("Successfully Added New Business Type with Business Type as " + "'" + oBizType.BusinessType + "'", 250, 250, "Success", "callBackFn", "");
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

        protected void rgBizType_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            
        }

    }
}