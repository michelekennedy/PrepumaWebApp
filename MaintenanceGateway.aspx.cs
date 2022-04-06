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
    public partial class MaintenanceGateway : System.Web.UI.Page
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
                getGateway();
            }
        }

        private void getGateway()
        {
            ClsGATEWAY oGateway = new ClsGATEWAY();
            List<ClsGATEWAY> gatewayList = oGateway.GetGatewayInfo();
            rgGateway.DataSource = gatewayList;
        }

        protected void rgGateway_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            getGateway();
        }

        protected void rgGateway_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                rgGateway.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Gateway Information";
            }
            else
            {
                rgGateway.MasterTableView.EditFormSettings.CaptionFormatString = "Add Gateway Information";
            }
            e.Item.OwnerTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;

            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                rgGateway.ExportSettings.FileName = "Gateway";
                rgGateway.AllowFilteringByColumn = false; 
            }
        }

        protected void rgGateway_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

            try
            {
                
                ClsGATEWAY oGateway = new ClsGATEWAY();
                string insertMsg = "";
                if (IsValid)
                {
                    oGateway.gateway = (userControl.FindControl("cbxGateway") as RadComboBox).Text;
                    oGateway.Createdby = (string)(Session["userName"]);
                    oGateway.Updatedby = (string)(Session["userName"]);
                    oGateway.ActiveFlag = true;

                    if (oGateway != null)
                    {
                        insertMsg = ClsGATEWAY.InsertGateway(oGateway);
                        if (insertMsg == "")
                        {
                            windowManager.RadAlert("Successfully Added New Gateway with Gateway as " + oGateway.gateway , 300, 250, "Success", "callBackFn", "");
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

        

        protected void rgGateway_ItemDataBound(object sender, GridItemEventArgs e)
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
                     
                     //GATEWAY
                     try
                     {

                         string hiddenGateWay = (userControl.FindControl("hdGateway") as HiddenField).Value;
                         RadComboBox gatewayCBX = userControl.FindControl("cbxGateway") as RadComboBox;
                         ClsRegion region = new ClsRegion();
                         List<ClsRegion> airportlst = region.GetRegions();
                         gatewayCBX.DataSource = airportlst;
                         gatewayCBX.DataBind();
                         gatewayCBX.SelectedValue = hiddenGateWay;
                     }
                     catch (Exception ex)
                     {
                         msg = ex.Message;
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