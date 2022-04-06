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
    public partial class MaintenanceSalesRepTitles : System.Web.UI.Page
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
                getSalesRepTitles();
            }
        }


     protected void rgSalesRep_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            getSalesRepTitles();
        }
        
        private void getSalesRepTitles()
        {
            ClsSalesRepTitle oSalesRep = new ClsSalesRepTitle();
            List<ClsSalesRepTitle> lstSalesReps = oSalesRep.GetTitles();
            rgSalesRep.DataSource = lstSalesReps;

        }

        protected void rgSalesRep_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                //************First calling dropdown list values selected in pop up edit form**************/ 
                UserControl userControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;

                //Title
                try
                {

                    RadTextBox txtTitle = userControl.FindControl("txtTitle") as RadTextBox;
                    

                    if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                    {
                        txtTitle.Enabled = true;
                    }
                    else
                    {
                        //enable for Add mode
                        txtTitle.Enabled = false;
                    }

                }
                catch (Exception ex)
                {
                    
                    pnlDanger.Visible = true;
                    lblDanger.Text = ex.Message.ToString();
                }
            }
        }

        protected void rgSalesRep_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edit")
                {
                    rgSalesRep.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Threshold Information";
                    rgSalesRep.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
                }
                else
                {
                    rgSalesRep.MasterTableView.EditFormSettings.CaptionFormatString = "Add New Title";
                    rgSalesRep.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
                }
                if (e.CommandName == RadGrid.ExportToExcelCommandName)
                {
                    rgSalesRep.ExportSettings.FileName = "Titles";

                    rgSalesRep.ExportSettings.IgnorePaging = true;
                    rgSalesRep.ExportSettings.ExportOnlyData = true;
                    rgSalesRep.ExportSettings.OpenInNewWindow = true;
                    rgSalesRep.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
                }

            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }
        }

        protected void rgSalesRep_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

                ClsSalesRepTitle oSalesRep = new ClsSalesRepTitle();
                string insertMsg = "";
                if (IsValid)
                {

                      oSalesRep.SalesRepTitle = (userControl.FindControl("txtTitle") as RadTextBox).Text;
                      oSalesRep.Threshold = (userControl.FindControl("txtThresholdN") as RadNumericTextBox).Text == string.Empty ? 0: Convert.ToInt32((userControl.FindControl("txtThresholdN") as RadNumericTextBox).Text);
                      oSalesRep.ActiveFlag = (bool)(userControl.FindControl("activeChk") as RadButton).Checked;

                      if (oSalesRep != null)
                      {
                          insertMsg = ClsSalesRepTitle.InsertSalesRepTitle(oSalesRep);
                          if (insertMsg == "")
                          {
                              windowManager.RadAlert("Successfully Added New SalesRep Title " + oSalesRep.SalesRepTitle, 300, 250, "Success", "callBackFn", "");
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

        protected void rgSalesRep_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

                ClsSalesRepTitle oSalesRep = new ClsSalesRepTitle();
                string updateMsg = "";
                if (IsValid)
                {
                    oSalesRep.idSalesRepTitle = Convert.ToInt32((userControl.FindControl("hdidSalesRepTitle") as HiddenField).Value);
                    oSalesRep.SalesRepTitle = (userControl.FindControl("txtTitle") as RadTextBox).Text;
                    oSalesRep.Threshold = (userControl.FindControl("txtThresholdN") as RadNumericTextBox).Text == string.Empty ? 0 : Convert.ToInt32((userControl.FindControl("txtThresholdN") as RadNumericTextBox).Text);
                    oSalesRep.ActiveFlag = (bool)(userControl.FindControl("activeChk") as RadButton).Checked;


                    if (oSalesRep != null)
                    {
                        updateMsg = ClsSalesRepTitle.UpdateSalesRepTitle(oSalesRep);
                        if (updateMsg == "")
                        {
                            windowManager.RadAlert("Successfully Updated SalesRep Title " + oSalesRep.SalesRepTitle, 300, 250, "Success", "callBackFn", "");
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