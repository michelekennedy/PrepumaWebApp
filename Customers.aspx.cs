using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Web.Services;
using System.Security.Principal;
using System.ServiceModel;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Drawing;
using Telerik.Web.UI.GridExcelBuilder;
using PrepumaWebApp.App_Data.DAL;

namespace PrepumaWebApp
{
    public partial class Customers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] == null || Session["appName"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            
            //Do not allow Add New Record for User Role Audit
            if (Session["userRole"].ToString().ToLower() == "audit")
            {
                rgCustomer.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

        protected void rgCustomer_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClsCustomer cust = new ClsCustomer();
            List<ClsCustomer> customerlist = cust.GetCustomers();
            this.rgCustomer.DataSource = customerlist;

        }

        protected void rgCustomer_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {

                rgCustomer.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Customer";
            }
            else
            {
                rgCustomer.MasterTableView.EditFormSettings.CaptionFormatString = "Add Customer";
            }

            e.Item.OwnerTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
        }
        protected void rgCustomer_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                ClsCustomer objCustomer = new ClsCustomer();
                objCustomer.idCustomer = Convert.ToInt32((userControl.FindControl("hdCustomerID") as HiddenField).Value);
                objCustomer.RelationshipName = (userControl.FindControl("txtCustomerName") as RadTextBox).Text;
                objCustomer.MCNumber = (userControl.FindControl("txtMCNumber") as RadTextBox).Text;
                objCustomer.UpdatedBy = (string)(Session["userName"]);
                objCustomer.UpdatedOn = DateTime.Now;
                objCustomer.ActiveFlag = (bool)(userControl.FindControl("cbxActive") as RadButton).Checked;

                string msg = ClsCustomer.UpdateCustomer(objCustomer);
                if (msg != "")
                {
                    lblDanger.Text = msg;
                    pnlDanger.Visible = true;
                }
                else
                {
                    pnlsuccess.Visible = true;
                    lblSuccess.Text = "New Customer Successfully Updated for Relationship Name " + "'" + objCustomer.RelationshipName + "'";
                }

            }
            catch (Exception ex)
            {
                lblDanger.Text = ex.Message;
                pnlDanger.Visible = true;
            }
        }

        protected void rgCustomer_InsertCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                string DefaultValue = "14";
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                ClsCustomer objCustomer = new ClsCustomer();
                objCustomer.RelationshipName = (userControl.FindControl("txtCustomerName") as RadTextBox).Text;
                objCustomer.MCNumber = (userControl.FindControl("txtMCNumber") as RadTextBox).Text;
                objCustomer.UpdatedBy = (string)(Session["userName"]);
                objCustomer.UpdatedOn = DateTime.Now;
                objCustomer.CreatedBy = (string)(Session["userName"]);
                objCustomer.CreatedOn = DateTime.Now;
                objCustomer.ActiveFlag = true;
                objCustomer.PaymentTerms = DefaultValue;                

                try
                {
                    string msg = ClsCustomer.InsertCustomer(objCustomer);
                    if (msg != "")
                    {
                        lblDanger.Text = msg;
                        pnlDanger.Visible = true;
                    }
                    else
                    {
                        pnlsuccess.Visible = true;
                        lblSuccess.Text = "New Customer Successfully Added with Relationship Name as " + "'" + objCustomer.RelationshipName + "'";
                    }
                   
                }
                catch (Exception ex)
                {
                    lblDanger.Text = ex.Message;
                    pnlDanger.Visible = true;

                }
            }
            catch (Exception ex)
            {
                lblDanger.Text = ex.Message;
                pnlDanger.Visible = true;
            }

        }
    }
}