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
    public partial class MaintenanceCrossDock : System.Web.UI.Page
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
                getCrossDocking();
            }
        }

        private void getCrossDocking()
        {
            ClsCrossDockCPLB oCrossDck = new ClsCrossDockCPLB();
            List<ClsCrossDockCPLB> crsDckList = oCrossDck.GetcdcplbIdInfo();
            rgCrossDock.DataSource = crsDckList;
        }

        protected void rgCrossDock_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            getCrossDocking();
        }

        protected void rgCrossDock_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                rgCrossDock.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Cross Docking Information";
                rgCrossDock.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            else
            {
                rgCrossDock.MasterTableView.EditFormSettings.CaptionFormatString = "Add Cross Docking Information";
                rgCrossDock.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                rgCrossDock.ExportSettings.FileName = "CrossDocking";
                rgCrossDock.AllowFilteringByColumn = false;
                rgCrossDock.MasterTableView.GetColumn("Edit").Visible = false;
            }
        }

        protected void rgCrossDock_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

            try
            {

                ClsCrossDockCPLB oCrsDckng = new ClsCrossDockCPLB();
                string insertMsg = "";
                if (IsValid)
                {
                    oCrsDckng.CDCPLBID = (userControl.FindControl("txtCdId") as RadTextBox).Text;
                    oCrsDckng.CDCPLB = Convert.ToDecimal((userControl.FindControl("txtCrossDckFeeN") as RadNumericTextBox).Text);
                    oCrsDckng.DESC = (userControl.FindControl("txtDesc") as RadTextBox).Text == string.Empty ? string.Empty : (userControl.FindControl("txtDesc") as RadTextBox).Text;
                    oCrsDckng.Createdby = (string)(Session["userName"]);
                    oCrsDckng.Updatedby = (string)(Session["userName"]);
                    oCrsDckng.ActiveFlag = true;


                    if (oCrsDckng != null)
                    {
                        insertMsg = ClsCrossDockCPLB.InsertCrossDocking(oCrsDckng);
                        if (insertMsg == "")
                        {
                            windowManager.RadAlert("Successfully Added New CrossDocking with CrossDocking ID as " + oCrsDckng.CDCPLBID, 300, 250, "Success", "callBackFn", "");
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

        protected void rgCrossDock_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

            try
            {

                ClsCrossDockCPLB oCrsDckng = new ClsCrossDockCPLB();
                string updateMsg = "";
                if (IsValid)
                {
                    oCrsDckng.CDCPLBID = (userControl.FindControl("txtCdId") as RadTextBox).Text;
                    oCrsDckng.CDCPLB = Convert.ToDecimal((userControl.FindControl("txtCrossDckFeeN") as RadNumericTextBox).Text);
                    oCrsDckng.DESC = (userControl.FindControl("txtDesc") as RadTextBox).Text == string.Empty ? string.Empty : (userControl.FindControl("txtDesc") as RadTextBox).Text;
                    oCrsDckng.Updatedby = (string)(Session["userName"]);
                    //later make this optional
                    oCrsDckng.ActiveFlag = true;

                    if (oCrsDckng != null)
                    {
                        updateMsg = ClsCrossDockCPLB.UpdateCrossDocking(oCrsDckng);
                        if (updateMsg == "")
                        {
                            windowManager.RadAlert("Successfully Updated CrossDocking with CrossDocking ID as " + oCrsDckng.CDCPLBID, 300, 250, "Success", "callBackFn", "");
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

        protected void rgCrossDock_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
            try
            {
                //Need to select drop downs after data binding
                 if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                 {
                     UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                     RadTextBox DescTXT = userControl.FindControl("txtDesc") as RadTextBox;
                     RadTextBox CdIdTXT = userControl.FindControl("txtCdId") as RadTextBox;
                     if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                     {

                     }
                     else
                     {
                         //disable for Edit mode
                         CdIdTXT.Enabled = false;
                         DescTXT.Enabled = false;
                     }
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