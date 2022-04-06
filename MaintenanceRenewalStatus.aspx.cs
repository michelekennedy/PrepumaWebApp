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
    public partial class MaintenanceRenewalStatus : System.Web.UI.Page
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
        private void getStatuses()
        {
            List<ClsRenewalStatus> listStatuses = ClsRenewalStatus.GetRenewalStatusListWInactive();
            rgGrid.DataSource = listStatuses;

        }

        protected void rgGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            getStatuses();
            
        }

        protected void rgGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
        }

        protected void rgGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                rgGrid.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Contract Renewal Status Information";
                rgGrid.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            else
            {
                rgGrid.MasterTableView.EditFormSettings.CaptionFormatString = "Add Contract Renewal Status Information";
                rgGrid.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                rgGrid.ExportSettings.FileName = "RenewalStatus";
                rgGrid.AllowFilteringByColumn = false;
            }
        }

        protected void rgGrid_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

                  ClsRenewalStatus oStatus = new ClsRenewalStatus();
                  string insertMsg = "";
                  if (IsValid)
                  {

                      oStatus.ContractRenewalStatus = (userControl.FindControl("txtRenewalStatus") as RadTextBox).Text;
                      oStatus.CreatedBy = (string)(Session["userName"]);
                      oStatus.UpdatedBy = (string)(Session["userName"]);
                      oStatus.ActiveFlag = true;
                      oStatus.OrderNumber = (float)(userControl.FindControl("txtOrderNumber") as RadNumericTextBox).Value;


                      if (oStatus != null)
                      {
                          insertMsg = oStatus.InsertRenewalStatus(oStatus);
                          if (insertMsg == "")
                          {
                              windowManager.RadAlert("Successfully Added New Contract Renewal Status Type " + "'" + oStatus.ContractRenewalStatus + "'", 250, 250, "Success", "callBackFn", "");
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

                ClsRenewalStatus oStatus = new ClsRenewalStatus();
                string insertMsg = "";
                if (IsValid)
                {
                    oStatus.idContractRenewalStatus = Convert.ToInt16((userControl.FindControl("lblRenewalStatusID") as Label).Text);
                    oStatus.ContractRenewalStatus = (userControl.FindControl("txtRenewalStatus") as RadTextBox).Text;
                    oStatus.CreatedBy = (string)(Session["userName"]);
                    oStatus.UpdatedBy = (string)(Session["userName"]);
                    oStatus.ActiveFlag = (userControl.FindControl("ActiveFlag") as RadButton).Checked;
                    oStatus.OrderNumber = (float)(userControl.FindControl("txtOrderNumber") as RadNumericTextBox).Value;

                    if (oStatus != null)
                    {
                        insertMsg = oStatus.UpdateRenewalStatus(oStatus);
                        if (insertMsg == "")
                        {
                            windowManager.RadAlert("Successfully Updated New Contract Renewal Status Type " + "'" + oStatus.ContractRenewalStatus + "'", 250, 250, "Success", "callBackFn", "");
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