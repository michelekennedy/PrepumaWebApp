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
    public partial class MaintenanceRenewalType : System.Web.UI.Page
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
        private void getTypes()
        {
            List<ClsRenewalType> listTypes = ClsRenewalType.GetRenewalTypeListWInactive();
            rgGrid.DataSource = listTypes;

        }

        protected void rgGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            getTypes();

        }

        protected void rgGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
        }

        protected void rgGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                rgGrid.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Contract Renewal Type Information";
                rgGrid.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            else
            {
                rgGrid.MasterTableView.EditFormSettings.CaptionFormatString = "Add Contract Renewal Type Information";
                rgGrid.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                rgGrid.ExportSettings.FileName = "RenewalType";
                rgGrid.AllowFilteringByColumn = false;
            }
        }

        protected void rgGrid_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

                ClsRenewalType oType = new ClsRenewalType();
                string insertMsg = "";
                if (IsValid)
                {

                    oType.ContractRenewalType = (userControl.FindControl("txtRenewalType") as RadTextBox).Text;
                    oType.CreatedBy = (string)(Session["userName"]);
                    oType.UpdatedBy = (string)(Session["userName"]);
                    oType.ActiveFlag = true;


                    if (oType != null)
                    {
                        insertMsg = oType.InsertRenewalType(oType);
                        if (insertMsg == "")
                        {
                            windowManager.RadAlert("Successfully Added New Contract Renewal Type Type " + "'" + oType.ContractRenewalType + "'", 250, 250, "Success", "callBackFn", "");
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

                ClsRenewalType oType = new ClsRenewalType();
                string insertMsg = "";
                if (IsValid)
                {
                    oType.idContractRenewalType = Convert.ToInt16((userControl.FindControl("lblRenewalTypeID") as Label).Text);
                    oType.ContractRenewalType = (userControl.FindControl("txtRenewalType") as RadTextBox).Text;
                    oType.CreatedBy = (string)(Session["userName"]);
                    oType.UpdatedBy = (string)(Session["userName"]);
                    oType.ActiveFlag = (userControl.FindControl("ActiveFlag") as RadButton).Checked;


                    if (oType != null)
                    {
                        insertMsg = oType.UpdateRenewalType(oType);
                        if (insertMsg == "")
                        {
                            windowManager.RadAlert("Successfully Updated New Contract Renewal Type Type " + "'" + oType.ContractRenewalType + "'", 250, 250, "Success", "callBackFn", "");
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