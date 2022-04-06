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
    public partial class MaintenanceProductType : System.Web.UI.Page
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
                getProductType();
            }
        }

        private void getProductType()
        {
            ClsProductType oProdctType = new ClsProductType();
            List<ClsProductType> prodctList = oProdctType.GetProductType();
            rgProductType.DataSource = prodctList;
        }

        protected void rgProductType_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            getProductType();
        }

        protected void rgProductType_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                rgProductType.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Product Type Information";
                rgProductType.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            else
            {
                rgProductType.MasterTableView.EditFormSettings.CaptionFormatString = "Add Product Type Information";
                rgProductType.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                rgProductType.ExportSettings.FileName = "ProductType";
                rgProductType.AllowFilteringByColumn = false;
            }
        }

        protected void rgProductType_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

                ClsProductType oPrdctType = new ClsProductType();
                string insertMsg = "";
                if (IsValid)
                {
                    oPrdctType.ProductType = (userControl.FindControl("txtProductType") as RadTextBox).Text;
                    oPrdctType.ProductDesc = (userControl.FindControl("txtProductDesc") as RadTextBox).Text;
                    oPrdctType.Createdby = (string)(Session["userName"]);
                    oPrdctType.Updatedby = (string)(Session["userName"]);
                    oPrdctType.ActiveFlag = true;

                    if (oPrdctType != null)
                    {
                        insertMsg = ClsProductType.InsertProductType(oPrdctType);
                        if (insertMsg == "")
                        {
                            windowManager.RadAlert("Successfully Added New Business Type with Business Type as " + "'"+ oPrdctType.ProductType+ "'", 250, 250, "Success", "callBackFn", "");
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