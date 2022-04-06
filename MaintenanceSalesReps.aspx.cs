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
    public partial class MaintenanceSalesReps : System.Web.UI.Page
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
                getSalesReps();
            }
        }

        protected void rgSalesRep_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            getSalesReps();
        }
        
        private void getSalesReps()
        {
            ClsSalesReps oSalesRep = new ClsSalesReps();
            List<ClsSalesReps> lstSalesReps = oSalesRep.GetTerritory();
            rgSalesRep.DataSource = lstSalesReps;
            //rgSalesRep.DataBind();
        }

        protected void rgSalesRep_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            try
            {
                 var msg = "";
                //Need to select drop downs after data binding
                 if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                 {
                     //************First calling dropdown list values selected in pop up edit form**************/ 
                     UserControl userControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                     GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;

                     
                     //SALES REP TITLES
                     try
                     {
                         string hiddenSlsRpTitles = (userControl.FindControl("hdSalesRpTitles") as HiddenField).Value;
                         RadDropDownList SalesRepTitlesDDL = userControl.FindControl("ddlSlsRepTitles") as RadDropDownList;
                         ClsSalesRepTitle oSalesRep = new ClsSalesRepTitle();
                         List<ClsSalesRepTitle> lstSalesRepTitles = oSalesRep.GetTitles();
                         

                         SalesRepTitlesDDL.DataSource = lstSalesRepTitles;
                         SalesRepTitlesDDL.DataTextField = "SalesRepTitle";
                         SalesRepTitlesDDL.DataValueField = "idSalesRepTitle";
                         SalesRepTitlesDDL.DataBind();
                         SalesRepTitlesDDL.SelectedText = hiddenSlsRpTitles;

                         RadButton newBizCHK = userControl.FindControl("newBizChk") as RadButton;
                         RadNumericTextBox thresholdTXT = userControl.FindControl("txtThresholdN") as RadNumericTextBox;
                         if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                         {

                         }
                         else
                         {
                             //disable for Edit mode
                             //newBizCHK.Enabled = false;
                             //thresholdTXT.Enabled = false;
                         }
                     

                     }
                     catch (Exception ex)
                     {
                         msg = ex.Message;
                     }

                     //NSMID
                     try
                     {
                         string hiddenNsmid = (userControl.FindControl("hdNsmid") as HiddenField).Value;
                         RadDropDownList NsmidDDL = userControl.FindControl("ddlNSMID") as RadDropDownList;
                         ClsSalesReps oSalesRep = new ClsSalesReps();
                         List<ClsSalesReps> lstSalesReps = oSalesRep.GetTerritory();
                         List<String> lstNSMID = lstSalesReps.Select(x => x.SalesRepID).Distinct().ToList();
                         NsmidDDL.DataSource = lstNSMID;
                         NsmidDDL.DataBind();
                         NsmidDDL.SelectedText = hiddenNsmid;

                     }
                     catch (Exception ex)
                     {
                         msg = ex.Message;
                     }
                     //DSM
                     try
                     {
                         string hiddenDsm = (userControl.FindControl("hdDsm") as HiddenField).Value;
                         RadDropDownList NsmidDDL = userControl.FindControl("ddlDSM") as RadDropDownList;
                         ClsSalesReps oSalesRep = new ClsSalesReps();
                         List<ClsSalesReps> lstSalesReps = oSalesRep.GetTerritory();
                         List<String> lstNSMID = lstSalesReps.Select(x => x.SalesRepID).Distinct().ToList();
                         NsmidDDL.DataSource = lstNSMID;
                         NsmidDDL.DataBind();
                         NsmidDDL.SelectedText = hiddenDsm;

                     }
                     catch (Exception ex)
                     {
                         msg = ex.Message;
                     }

                     //BUSINESS TYPE
                     try
                     {
                         string hiddenBizType = (userControl.FindControl("hdBizType") as HiddenField).Value;
                         RadDropDownList BizTypeDDL = userControl.FindControl("ddlBusinessType") as RadDropDownList;
                         ClsBusinessType businessType = new ClsBusinessType();
                         List<ClsBusinessType> bizTypelist = businessType.GetBizType();
                         BizTypeDDL.DataSource = bizTypelist;
                         BizTypeDDL.DataBind();
                         BizTypeDDL.SelectedValue = hiddenBizType;

                     }
                     catch (Exception ex)
                     {
                         msg = ex.Message;
                     }

                     ////STACK REPORT
                     //try
                     //{
                     //    RadButton StackReportCHK = userControl.FindControl("stackReportChk") as RadButton;

                     //}
                     //catch (Exception ex)
                     //{
                     //    msg = ex.Message;
                     //}

                 }
                
            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }
        }

        protected void rgSalesRep_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                rgSalesRep.MasterTableView.EditFormSettings.CaptionFormatString = "Edit SalesRep Information";
                rgSalesRep.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            else
            {
                rgSalesRep.MasterTableView.EditFormSettings.CaptionFormatString = "Add SalesRep Information";
                rgSalesRep.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                rgSalesRep.ExportSettings.FileName = "SalesReps";
                rgSalesRep.AllowFilteringByColumn = false;
                rgSalesRep.MasterTableView.GetColumn("Edit").Visible = false;
                rgSalesRep.ExportSettings.IgnorePaging = true;
                rgSalesRep.ExportSettings.ExportOnlyData = true;
                rgSalesRep.ExportSettings.OpenInNewWindow = true;
                rgSalesRep.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            }
        }

        protected void rgSalesRep_InsertCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

                ClsSalesReps oSalesRep = new ClsSalesReps();
                string insertMsg = "";
                if (IsValid)
                {
                    oSalesRep.SalesRep = (userControl.FindControl("txtSalesRepName") as RadTextBox).Text;
                    oSalesRep.SalesRepID = (userControl.FindControl("txtSalesRepId") as RadTextBox).Text;
                    oSalesRep.Threshold = (userControl.FindControl("txtThresholdN") as RadNumericTextBox).Text == string.Empty ? 0: Convert.ToInt32((userControl.FindControl("txtThresholdN") as RadNumericTextBox).Text);
                    if ((userControl.FindControl("ddlSlsRepTitles") as RadDropDownList).Visible)
                    {
                        oSalesRep.SalesRepTitle = (userControl.FindControl("ddlSlsRepTitles") as RadDropDownList).SelectedText;
                    }
                    else
                    {
                        oSalesRep.SalesRepTitle = (userControl.FindControl("txtSlsRepTitles") as RadTextBox).Text;
                    }
                    oSalesRep.nSMID = (userControl.FindControl("ddlNSMID") as RadDropDownList).SelectedText;
                    //oSalesRep.BusinesType = (userControl.FindControl("ddlBusinessType") as RadDropDownList).SelectedValue;
                    if ((userControl.FindControl("ddlBusinessType") as RadDropDownList).Visible)
                    {
                        oSalesRep.BusinesType = (userControl.FindControl("ddlBusinessType") as RadDropDownList).SelectedValue;
                    }
                    else
                    {
                        oSalesRep.BusinesType = (userControl.FindControl("txtBusinessType") as RadTextBox).Text;
                    }
                    oSalesRep.dSM = (userControl.FindControl("ddlDSM") as RadDropDownList).SelectedText;
                    oSalesRep.newBizline = (userControl.FindControl("newBizChk") as RadButton).Checked;
                    oSalesRep.Createdby = (string)(Session["userName"]);
                    oSalesRep.Updatedby = (string)(Session["userName"]);
                    oSalesRep.ActiveFlag = true;
                    oSalesRep.StackReport = (bool)(userControl.FindControl("stackReportChk") as RadButton).Checked;
                    oSalesRep.EmployeeID = (userControl.FindControl("txtEmployeeID") as RadTextBox).Text;

                    if (oSalesRep != null)
                    {
                        insertMsg = ClsSalesReps.InsertSalesRep(oSalesRep);
                        if (insertMsg == "")
                        {
                            windowManager.RadAlert("Successfully Added New SalesRep with SalesRep ID as " + oSalesRep.SalesRepID, 300, 250, "Success", "callBackFn", "");
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

                ClsSalesReps oSalesRep = new ClsSalesReps();
                string updateMsg = "";
                if (IsValid)
                {
                    oSalesRep.SalesRep = (userControl.FindControl("txtSalesRepName") as RadTextBox).Text;
                    oSalesRep.SalesRepID = (userControl.FindControl("txtSalesRepId") as RadTextBox).Text;
                    oSalesRep.Threshold = (userControl.FindControl("txtThresholdN") as RadNumericTextBox).Text == string.Empty ? 0 : Convert.ToInt32((userControl.FindControl("txtThresholdN") as RadNumericTextBox).Text);
                    if ((userControl.FindControl("ddlSlsRepTitles") as RadDropDownList).Visible)
                    {
                        oSalesRep.SalesRepTitle = (userControl.FindControl("ddlSlsRepTitles") as RadDropDownList).SelectedText;
                    }
                    else
                    {
                        oSalesRep.SalesRepTitle = (userControl.FindControl("txtSlsRepTitles") as RadTextBox).Text;
                    }

                    oSalesRep.nSMID = (userControl.FindControl("ddlNSMID") as RadDropDownList).SelectedText;
                    //oSalesRep.BusinesType = (userControl.FindControl("ddlBusinessType") as RadDropDownList).SelectedValue;
                    if ((userControl.FindControl("ddlBusinessType") as RadDropDownList).Visible)
                    {
                        oSalesRep.BusinesType = (userControl.FindControl("ddlBusinessType") as RadDropDownList).SelectedValue;
                    }
                    else
                    {
                        oSalesRep.BusinesType = (userControl.FindControl("txtBusinessType") as RadTextBox).Text;
                    }
                    oSalesRep.dSM = (userControl.FindControl("ddlDSM") as RadDropDownList).SelectedText;
                    oSalesRep.newBizline = (userControl.FindControl("newBizChk") as RadButton).Checked;
                    oSalesRep.Updatedby = (string)(Session["userName"]);
                    //later make this optional
                    oSalesRep.ActiveFlag = true;
                    oSalesRep.StackReport = (bool)(userControl.FindControl("stackReportChk") as RadButton).Checked;
                    oSalesRep.EmployeeID = (userControl.FindControl("txtEmployeeID") as RadTextBox).Text;

                    if (oSalesRep != null)
                    {
                        updateMsg = ClsSalesReps.UpdateSalesRep(oSalesRep);
                        if (updateMsg == "")
                        {
                            windowManager.RadAlert("Successfully Updated SalesRep with SalesRep ID as" + oSalesRep.SalesRepID, 300, 250, "Success", "callBackFn", "");
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