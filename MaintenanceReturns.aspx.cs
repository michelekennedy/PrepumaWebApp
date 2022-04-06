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
    public partial class MaintenanceReturns : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userName"] == null || Session["appName"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                if (Session["userRole"].ToString().ToLower() == "audit")
                {
                    Response.Redirect("Default.aspx");
                } 
                getReturns();
            }
        }

        private void getReturns()
        {
            ClsRTNSCPP oReturns = new ClsRTNSCPP();
            List<ClsRTNSCPP> rtnslist = oReturns.GetRtnsIdInfo();
            rgReturns.DataSource = rtnslist;
        }

        protected void rgReturns_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            getReturns();
        }

        protected void rgReturns_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                rgReturns.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Returns Information";
            }
            else
            {
                rgReturns.MasterTableView.EditFormSettings.CaptionFormatString = "Add Returns Information";
            }
            e.Item.OwnerTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;

            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                rgReturns.ExportSettings.FileName = "Returns";
                rgReturns.AllowFilteringByColumn = false;
                rgReturns.MasterTableView.GetColumn("Edit").Visible = false;
            }
        }

        protected void rgReturns_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                //Need to select drop downs after data binding
                if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                    RadTextBox RtnsIdTXT = userControl.FindControl("txtRtnsId") as RadTextBox;
                    if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                    {

                    }
                    else
                    {
                        //disable for Edit mode
                        RtnsIdTXT.Enabled = false;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }
        }

        protected void rgReturns_InsertCommand(object sender, GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

            try
            {

                ClsRTNSCPP oRtns = new ClsRTNSCPP();
                string insertMsg = "";
                if (IsValid)
                {
                    oRtns.RTNSID = (userControl.FindControl("txtRtnsId") as RadTextBox).Text;
                    oRtns.RTNCPP = Convert.ToDouble((userControl.FindControl("txtRtnsCppN") as RadNumericTextBox).Text);
                    oRtns.DESC = (userControl.FindControl("txtDesc") as RadTextBox).Text == string.Empty ? string.Empty : (userControl.FindControl("txtDesc") as RadTextBox).Text;
                    oRtns.Createdby = (string)(Session["userName"]);
                    oRtns.Updatedby = (string)(Session["userName"]);
                    oRtns.ActiveFlag = true;

                    if (oRtns != null)
                    {
                        insertMsg = ClsRTNSCPP.InsertReturns(oRtns);
                        if (insertMsg == "")
                        {
                            windowManager.RadAlert("Successfully Added New Returns with Returns ID as " + oRtns.RTNSID, 300, 250, "Success", "callBackFn", "");
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
                    e.Canceled = true;
                    errorMsg.Visible = true;
                    errorMsg.Text = "Please enter Required fields";
                }

            }
            catch (Exception ex)
            {
                e.Canceled = true;
                errorMsg.Visible = true;
                errorMsg.Text = ex.Message.ToString();
            }
        }

        protected void rgReturns_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

            try
            {

                ClsRTNSCPP oRtns = new ClsRTNSCPP();
                string updateMsg = "";
                if (IsValid)
                {
                    oRtns.RTNSID = (userControl.FindControl("txtRtnsId") as RadTextBox).Text;
                    oRtns.RTNCPP = Convert.ToDouble((userControl.FindControl("txtRtnsCppN") as RadNumericTextBox).Text);
                    oRtns.DESC = (userControl.FindControl("txtDesc") as RadTextBox).Text == string.Empty ? string.Empty : (userControl.FindControl("txtDesc") as RadTextBox).Text;
                    oRtns.Updatedby = (string)(Session["userName"]);
                    //later make this optional
                    oRtns.ActiveFlag = true;

                    if (oRtns != null)
                    {
                        updateMsg = ClsRTNSCPP.UpdateReturns(oRtns);
                        if (updateMsg == "")
                        {
                            windowManager.RadAlert("Successfully Updated Returns with Returns ID as " + oRtns.RTNSID, 300, 250, "Success", "callBackFn", "");
                        }
                        else
                        {

                            errorMsg.Visible = true;
                            errorMsg.Text = updateMsg;
                            e.Canceled = true;
                        }
                    }
                }
                else
                {
                    // display error
                    e.Canceled = true;
                    errorMsg.Visible = true;
                    errorMsg.Text = "Please enter Required fields";
                }

            }
            catch (Exception ex)
            {
                e.Canceled = true;
                errorMsg.Visible = true;
                errorMsg.Text = ex.Message.ToString();
            }
        }



    }
}