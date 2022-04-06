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
    public partial class Clients : System.Web.UI.Page
    {
        string lastClient = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] == null || Session["appName"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            if (Session["userName"] == null || Session["appName"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            //Do not allow Add New Record for User Role Audit
            if (Session["userRole"].ToString().ToLower() == "audit")
            {
                rgClient.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

       
        protected void rgClient_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClsClient client = new ClsClient();
            List<ClsClient> clientlist = client.GetClientInfo();
            this.rgClient.DataSource = clientlist;
            
        }

        protected void rgClient_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                
                rgClient.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Client Information";
            }
            else
            {
                rgClient.MasterTableView.EditFormSettings.CaptionFormatString = "Add Client Information";
            }
            //rgClient.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            //gClient.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Underline = true;
            e.Item.OwnerTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
        }  


        protected void rgClient_PreRender(object source, EventArgs e)
        {
            try
            {
                if (rgClient.MasterTableView.IsItemInserted)
                {
                   
                    // Compare the CategoryID of the each item with the newly inserted record CategoryID    
                    // and select the last inserted row   
                    //only works on current page:
                    //foreach (GridDataItem item in rgClient.Items)
                    //{
                    //    string thisClient = item["ClientName"].Text;
                    //    if (thisClient == lastClient)
                    //    {
                    //        item.Selected = true;
                    //    }
                    //}
                    //string filterExp = "[ClientName] LIKE '%" + lastClient + "%'";
                    //rgClient.MasterTableView.FilterExpression = filterExp;
                    //rgClient.Rebind();
                }   


               

            }
            catch (Exception ex)
            {
                lblDanger.Text = ex.Message;
                pnlDanger.Visible = true;
            }
        }

        protected void rgClient_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {


                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                ClsClient objClient = new ClsClient();
                objClient.ClientID = Convert.ToInt32((userControl.FindControl("lblClientId") as Label).Text);
                objClient.ClientName = (userControl.FindControl("ClientNameTextbox") as RadTextBox).Text;
                objClient.Updatedby = (string)(Session["userName"]);
                objClient.ActiveFlag = true;

                ClsClient.UpdateClient(objClient);
                lastClient = objClient.ClientName;

            }
            catch (Exception ex)
            {
                lblDanger.Text = ex.Message;
                pnlDanger.Visible = true;
            }
        }

        protected void rgClient_InsertCommand(object source, GridCommandEventArgs e)
        {
            try
            {


                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                ClsClient objClient = new ClsClient();
                objClient.ClientName = (userControl.FindControl("ClientNameTextbox") as RadTextBox).Text;
                objClient.ClientName = objClient.ClientName.ToUpper();
                objClient.Updatedby = (string)(Session["userName"]);
                objClient.Createdby = (string)(Session["userName"]);
                objClient.ActiveFlag = true;

                //check for duplicate
                bool dup = false;
                try
                {
                    ClsClient client = PrepumaWebApp.App_Data.DAL.ClsClient.GetClient(objClient.ClientName);
                    if (client != null)
                    {
                        dup = true;
                    }
                if (dup == true)
                {
                    Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                    errorMsg.Text = "Client Name " + objClient.ClientName + " already exists.";
                    errorMsg.Visible = true;
                    e.Canceled = true;
                }
                else
                {
                    ClsClient.UpdateClient(objClient);
                       
                        
                        //go to page of added record
                    Int32 position = getRecordPosition(objClient.ClientName);
                    if (position > 0)
                    {

                        int locatedPage = position / rgClient.PageSize;
                        if (rgClient.CurrentPageIndex != locatedPage)
                        {
                            rgClient.CurrentPageIndex = locatedPage;
                            rgClient.Rebind();
                        }
                    }

                        //added by Jyothi
                        pnlsuccess.Visible = true;
                        lblSuccess.Text = "New Client Successfully Added with Client Name as " + "'" + objClient.ClientName + "'";
                        Session["lastClientAdded"] = objClient.ClientName;
                        e.Canceled = true;
                        windowManager.RadConfirm("Would you like to set up a new Contract for " + objClient.ClientName + "?", "confirmCallBackFn", 350, 200, null, "Please confrim");
                    }
                }
                catch (Exception ex)
                {
                    lblDanger.Text = ex.Message;
                    pnlDanger.Visible = true;
                    //dup=true;
                }
            }
            catch (Exception ex)
            {
                lblDanger.Text = ex.Message;
                pnlDanger.Visible = true;
            }

        }
        protected Int32 getRecordPosition(string ClientName)
        {
            //call sp_GetClientRowPositionForGrid to get identify row number of newly added row
            Int32 pos = 0;
            try
            {

                SqlConnection cnn;
                SqlCommand cmd;
                String strConnString = ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString;
                cnn = new SqlConnection(strConnString);

                cnn.Open();
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_GetClientRowPositionForGrid";
                cmd.Parameters.Add("@ClientName", SqlDbType.VarChar).Value = ClientName;
                cmd.Connection = cnn;

                SqlDataReader rdr;

                try
                {

                    rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            pos = (Int32)rdr["rowNum"];
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    cnn.Close();
                }



            }
            catch (Exception ex)
            {
                //validResult = false;
            }
            finally
            {

            }
            return pos;

        }
    }
}