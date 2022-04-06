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
    public partial class MaintenanceRegions : System.Web.UI.Page
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
                getRegions();
            }
        }

        protected void rgRegions_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            getRegions();
        }
        private void getRegions()
        {
            ClsRegion region = new ClsRegion();
            List<ClsRegion> regionlst = region.GetRegions();
            rgRegions.DataSource = regionlst;
            //rgRegions.DataBind();
        }

        protected void rgRegions_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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

                     //AIRPORT
                     try
                     {
                         
                         RadTextBox airportTXT = userControl.FindControl("txtAirport") as RadTextBox;
                         RadTextBox BranchManagerTXT = userControl.FindControl("txtBranchManager") as RadTextBox;
                         RadButton ReportedCHK = userControl.FindControl("ReportedChk") as RadButton;
                         RadNumericTextBox CostCenterTXT = userControl.FindControl("txtCostCenter") as RadNumericTextBox;

                         if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                         {
                             airportTXT.Enabled = true;
                         }
                         else
                         {
                             //enable for Add mode
                             airportTXT.Enabled = false;
                         }

                     }
                     catch (Exception ex)
                     {
                         msg = ex.Message;
                     }
                     //JURISDICTION
                     try
                     {
                         string hiddenJurdiction = (userControl.FindControl("hdJusrisdiction") as HiddenField).Value;
                         RadComboBox JusrisdictionCBX = userControl.FindControl("cbxEditJusrisdiction") as RadComboBox;
                         List<ClsCity> citylst = ClsCity.GetCities();
                         List<string> stateslst = citylst.Select(x => x.state).Distinct().ToList();
                         JusrisdictionCBX.DataSource = stateslst;
                         JusrisdictionCBX.DataBind();
                         JusrisdictionCBX.SelectedValue = hiddenJurdiction;
                         //if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                         //{
                         //    JusrisdictionCBX.Enabled = true;

                         //}
                         //else
                         //{
                         //    //disable for Edit mode
                         //    JusrisdictionCBX.Enabled = false;
                         //}
                     }
                     catch (Exception ex)
                     {
                         msg = ex.Message;
                     }
                     //COST CENTER LOCATION
                     try
                     {
                         string hiddenCCloc = (userControl.FindControl("hdCCLocation") as HiddenField).Value;
                         RadComboBox CCLocationCBX = userControl.FindControl("cbxCCLocation") as RadComboBox;
                         List<ClsCity> citylst = ClsCity.GetCities();
                         CCLocationCBX.DataSource = citylst;
                         CCLocationCBX.DataBind();
                         CCLocationCBX.SelectedValue = hiddenCCloc;
                         //if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                         //{
                         //    CCLocationCBX.Enabled = true;

                         //}
                         //else
                         //{
                         //    //disable for Edit mode
                         //    CCLocationCBX.Enabled = false;
                         //}
                     }
                     catch (Exception ex)
                     {
                         msg = ex.Message;
                     }
                     //DISTRICT
                     try
                     {
                         string hiddenDistrict = (userControl.FindControl("hdDistrict") as HiddenField).Value;
                         RadComboBox DistrictCBX = userControl.FindControl("cbxDistrict") as RadComboBox;
                         ClsRegion region = new ClsRegion();
                         List<ClsRegion> regionlst = region.GetRegions();
                         List<string> districts = regionlst.Select(x => x.District).Distinct().ToList();
                         DistrictCBX.DataSource = districts;
                         DistrictCBX.DataBind();
                         DistrictCBX.SelectedValue = hiddenDistrict;
                         //if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                         //{
                         //    DistrictCBX.Enabled = true;

                         //}
                         //else
                         //{
                         //    //disable for Edit mode
                         //    DistrictCBX.Enabled = false;
                         //}
                     }
                     catch (Exception ex)
                     {
                         msg = ex.Message;
                     }
                 }
            }
            catch(Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }
           
        }

        protected void rgRegions_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                rgRegions.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Region Information";
                rgRegions.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            else
            {
                rgRegions.MasterTableView.EditFormSettings.CaptionFormatString = "Add Region Information";
                rgRegions.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                rgRegions.ExportSettings.FileName = "Regions";
                //rgRegions.AllowFilteringByColumn = false;
                //rgRegions.MasterTableView.GetColumn("Edit").Visible = false;
                rgRegions.ExportSettings.IgnorePaging = true;
                rgRegions.ExportSettings.ExportOnlyData = true;
                rgRegions.ExportSettings.OpenInNewWindow = true;
                rgRegions.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            }
        }

        protected void rgRegions_InsertCommand(object sender, GridCommandEventArgs e)
        {
            
                try
                {
                    UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                    Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
               
                    ClsRegion oRegion = new ClsRegion();
                    string insertMsg = "";
                   
                    if (IsValid)
                    {
                            oRegion.Airport = (userControl.FindControl("txtAirport") as RadTextBox).Text;
                            oRegion.BranchManager = (userControl.FindControl("txtBranchManager") as RadTextBox).Text;
                            oRegion.CostCenter = (userControl.FindControl("txtCostCenter") as RadNumericTextBox).Text;
                            oRegion.Jusrisdiction = (userControl.FindControl("cbxEditJusrisdiction") as RadComboBox).SelectedValue;
                            oRegion.CostCenterLocation = (userControl.FindControl("cbxCCLocation") as RadComboBox).SelectedValue;
                            //oRegion.District = (userControl.FindControl("cbxDistrict") as RadComboBox).SelectedValue;                          
                            if ((userControl.FindControl("cbxDistrict") as RadComboBox).Visible)
                            {
                                oRegion.District = (userControl.FindControl("cbxDistrict") as RadComboBox).SelectedValue;
                            }
                            else
                            {
                                oRegion.District = (userControl.FindControl("txtDistrict") as RadTextBox).Text;
                            }

                            oRegion.DistrictManager = (userControl.FindControl("txtDistManager") as RadTextBox).Text;
                            oRegion.Reported = (userControl.FindControl("ReportedChk") as RadButton).Checked;
                            oRegion.StackReport = (userControl.FindControl("StackRptChk") as RadButton).Checked;
                            oRegion.Createdby = (string)(Session["userName"]);
                            oRegion.Updatedby = (string)(Session["userName"]);
                            oRegion.ActiveFlag = true;

                            if (oRegion != null)
                            {

                                bool validCostCenter = checkValidCostCenter(oRegion);
                                if (validCostCenter == false)
                                {
                                    errorMsg.Visible = true;
                                    errorMsg.Text = "Cost Center Required";
                                    e.Canceled = true;
                                }
                                else 
                                {
                                    insertMsg = ClsRegion.InsertRegion(oRegion);
                                    if (insertMsg == "")
                                    {
                                        windowManager.RadAlert("Successfully Added New Region with Airport as " + oRegion.Airport, 300, 250, "Success", "callBackFn", "");
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
                            e.Canceled = true;
                        }
                    }
                                        

            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
                e.Canceled = true;
            }                                                   
               
            
        }

        protected void rgRegions_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                ClsRegion oRegion = new ClsRegion();
                string updateMsg = "";                

               
                if (IsValid)
                    {
                        oRegion.Airport = (userControl.FindControl("txtAirport") as RadTextBox).Text;
                        oRegion.BranchManager = (userControl.FindControl("txtBranchManager") as RadTextBox).Text;
                        oRegion.CostCenter = (userControl.FindControl("txtCostCenter") as RadNumericTextBox).Text;
                        oRegion.Jusrisdiction = (userControl.FindControl("cbxEditJusrisdiction") as RadComboBox).SelectedValue;
                        oRegion.CostCenterLocation = (userControl.FindControl("cbxCCLocation") as RadComboBox).SelectedValue;
                        //oRegion.District = (userControl.FindControl("cbxDistrict") as RadComboBox).SelectedValue;
                        if ((userControl.FindControl("cbxDistrict") as RadComboBox).Visible)
                        {
                            oRegion.District = (userControl.FindControl("cbxDistrict") as RadComboBox).SelectedValue;
                        }
                        else
                        {
                            oRegion.District = (userControl.FindControl("txtDistrict") as RadTextBox).Text;
                        }
                        oRegion.DistrictManager = (userControl.FindControl("txtDistManager") as RadTextBox).Text;
                        oRegion.Reported = (userControl.FindControl("ReportedChk") as RadButton).Checked;
                        oRegion.StackReport = (userControl.FindControl("StackRptChk") as RadButton).Checked;
                        oRegion.Updatedby = (string)(Session["userName"]);
                        //later make this optional
                        oRegion.ActiveFlag = true;

                        bool validCostCenter = checkValidCostCenter(oRegion);
                        if (validCostCenter == false)
                        {
                             errorMsg.Visible = true;
                             errorMsg.Text = "Cost Center Required";
                             e.Canceled = true;
                        }
                        else
                        {
                             updateMsg = ClsRegion.UpdateRegion(oRegion);
                             if (updateMsg == "")
                             {
                                  windowManager.RadAlert("Successfully updated Region with Airport as" + oRegion.Airport, 300, 250, "Success", "callBackFn", "");
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

        protected bool checkValidCostCenter(ClsRegion oRegion)
        {
            bool validCostCenter = true;
            try
            {
                string region = oRegion.Airport;
                bool costCenterException = ClsRegion.CheckException(region);
                if (costCenterException == false && String.IsNullOrEmpty(oRegion.CostCenter))
                {
                    validCostCenter = false;
                }

            }
            catch (Exception ex)
            {
                //pnlDanger.Visible = true;
                //lblDanger.Text = ex.Message.ToString();
                return false;
            }
            return validCostCenter;
        }
    }
}