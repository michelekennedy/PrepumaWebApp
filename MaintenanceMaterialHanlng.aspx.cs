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
    public partial class MaintenanceMaterialHanlng : System.Web.UI.Page
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
            getMaterialHnlg();
        }

        private void getMaterialHnlg()
        {
            ClsMATHANCPP oMatrlHnlg = new ClsMATHANCPP();
            List<ClsMATHANCPP> mtrlHnlgList = oMatrlHnlg.GetMathandcppInfo();
            rgMaterialHnlg.DataSource = mtrlHnlgList;
        }

        protected void rgMaterialHnlg_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            getMaterialHnlg();
        }

        protected void rgMaterialHnlg_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                rgMaterialHnlg.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Material Handling Information";
                rgMaterialHnlg.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            else
            {
                rgMaterialHnlg.MasterTableView.EditFormSettings.CaptionFormatString = "Add Material Handling Information";
                rgMaterialHnlg.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                rgMaterialHnlg.ExportSettings.FileName = "MaterialHnlg";
                rgMaterialHnlg.AllowFilteringByColumn = false;
                rgMaterialHnlg.MasterTableView.GetColumn("Edit").Visible = false;
            }
        }

        protected void rgMaterialHnlg_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

            try
            {

                ClsMATHANCPP oMtrlHnlg = new ClsMATHANCPP();
                string insertMsg = "";
                if (IsValid)
                {
                    oMtrlHnlg.CPPMATHANID = (userControl.FindControl("txtCPPMATHANID") as RadTextBox).Text;
                    oMtrlHnlg.CPPMATHAN = Convert.ToDouble((userControl.FindControl("rtxtCppmathanN") as RadNumericTextBox).Text);
                    oMtrlHnlg.DESC = (userControl.FindControl("txtDesc") as RadTextBox).Text;
                    oMtrlHnlg.Createdby = (string)(Session["userName"]);
                    oMtrlHnlg.Updatedby = (string)(Session["userName"]);
                    oMtrlHnlg.ActiveFlag = true;
  
                    if (oMtrlHnlg != null)
                    {
                        insertMsg = ClsMATHANCPP.InsertMaterialHandling(oMtrlHnlg);
                        if (insertMsg == "")
                        {
                            windowManager.RadAlert("Successfully Added New Material Handling  with Material Handling ID as " + oMtrlHnlg.CPPMATHANID, 300, 250, "Success", "callBackFn", "");
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

        protected void rgMaterialHnlg_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");

            try
            {

                ClsMATHANCPP oMtrlHnlg = new ClsMATHANCPP();
                string updateMsg = "";
                if (IsValid)
                {
                    oMtrlHnlg.CPPMATHANID = (userControl.FindControl("txtCPPMATHANID") as RadTextBox).Text;
                    oMtrlHnlg.CPPMATHAN = Convert.ToDouble((userControl.FindControl("rtxtCppmathanN") as RadNumericTextBox).Text);
                    oMtrlHnlg.DESC = (userControl.FindControl("txtDesc") as RadTextBox).Text;
                    oMtrlHnlg.Updatedby = (string)(Session["userName"]);
                    //later make this optional
                    oMtrlHnlg.ActiveFlag = true;


                    if (oMtrlHnlg != null)
                    {
                        updateMsg = ClsMATHANCPP.UpdateMaterialHandling(oMtrlHnlg);
                        if (updateMsg == "")
                        {
                            windowManager.RadAlert("Successfully Updated Material Handling with Material Handling  ID as " + oMtrlHnlg.CPPMATHANID, 300, 250, "Success", "callBackFn", "");
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

        protected void rgMaterialHnlg_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                //Need to select drop downs after data binding
                if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                    RadTextBox MaterialHnlgIdTXT = userControl.FindControl("txtCPPMATHANID") as RadTextBox;
                    if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                    {

                    }
                    else
                    {
                        //disable for Edit mode
                        MaterialHnlgIdTXT.Enabled = false;
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