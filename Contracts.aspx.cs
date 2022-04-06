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
    public partial class Contracts1 : System.Web.UI.Page
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
                rgContract.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }           

        }

        protected void rgContract_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClsContract contract = new ClsContract();
            List<ClsContract> contractlist = contract.GetContractInfo();
            this.rgContract.DataSource = contractlist;

        }

        protected void rgContract_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {

                rgContract.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Contract Information";

            }
            else
            {
                rgContract.MasterTableView.EditFormSettings.CaptionFormatString = "Add Contract Information";

            }
        }  

        protected void rgContract_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {


                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                ClsContract objContract = new ClsContract();
                objContract.ContractID = Convert.ToDouble((userControl.FindControl("lblContractID") as Label).Text);
                objContract.ContractNumber = (userControl.FindControl("ContractTextBox") as RadTextBox).Text;
                objContract.ContractName = (userControl.FindControl("ContractNameTextBox") as RadTextBox).Text;
                objContract.ContractName = objContract.ContractName.ToUpper();
                objContract.ClientID = Convert.ToDouble((userControl.FindControl("cbClientName") as RadComboBox).SelectedValue);
                objContract.Updatedby = (string)(Session["userName"]);
                objContract.ActiveFlag = true;

                //Reset Required Labels back to Black
                //resetRequiredFields(userControl);

                bool doUpdate = checkFields(objContract, userControl, false);

                if (doUpdate)
                {
                    ClsContract.UpdateContract(objContract);
                }
                else
                {
                    Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                    errorMsg.Text = "Please Enter Required Fields";
                    errorMsg.Visible = true;
                    e.Canceled = true;
                }
              

            }
            catch (Exception ex)
            {
                lblDanger.Text = ex.Message;
                pnlDanger.Visible = true;
            }
        }

        protected void rgContract_InsertCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                //this.Validate();
                //if (this.IsValid)
                if (IsValid)
                {


                    bool isNumeric;
                    Int32 ivar;


                    ClsContract objContract = new ClsContract();
                    objContract.ContractID = -1;
                    objContract.ContractNumber = (userControl.FindControl("ContractTextBox") as RadTextBox).Text;
                    objContract.ContractName = (userControl.FindControl("ContractNameTextBox") as RadTextBox).Text;
                    objContract.ClientName = (userControl.FindControl("cbClientName") as RadComboBox).SelectedItem.Text;
                    objContract.Updatedby = (string)(Session["userName"]);
                    objContract.Createdby = (string)(Session["userName"]);
                    objContract.ActiveFlag = true;

                    string selClient = (userControl.FindControl("cbClientName") as RadComboBox).SelectedValue;
                    isNumeric = Int32.TryParse(selClient, out ivar);
                    if (isNumeric)
                    {
                        objContract.ClientID = ivar;
                    }
                    else
                    {
                        objContract.ClientID = 0;
                    }

                    //Reset Required Labels back to Black
                    //resetRequiredFields(userControl);

                    bool doUpdate = checkFields(objContract, userControl, true);


                    if (doUpdate)
                    {

                        string msg = "";
                        //check if there is a row in ContractClassification and notify
                        bool existingContract = contractClassificationFlag(objContract.ContractNumber);
                        if (existingContract)
                        {
                            msg = "Note: Contract Already Exists in Contract Classification.<br>";

                        }


                        ClsContract.UpdateContract(objContract);
                        //added by Jyothi
                        pnlsuccess.Visible = true;
                        msg = msg + "New Contract Successfully added to the Contract Name " + "'" + objContract.ContractName + "'";
                        lblSuccess.Text = msg;
                        Session["lastContractAdded"] = objContract.ContractNumber;
                        e.Canceled = true;
                        if (existingContract)
                            windowManagerContract.RadConfirm("Note: Contract Exists in Contract Classification.  Would you like to set up a new Account for this Contract?", "confirmCallBackFn", 350, 200, null, "Please confirm");
                        else
                            windowManagerContract.RadConfirm("Would you like to set up a new Account for this Contract?", "confirmCallBackFn", 350, 200, null, "Please confirm");




                        //go to page of added record
                        Int32 position = getRecordPosition(objContract.ClientName);
                        if (position > 0)
                        {

                            int locatedPage = position / rgContract.PageSize;
                            if (rgContract.CurrentPageIndex != locatedPage)
                            {
                                rgContract.CurrentPageIndex = locatedPage;
                                rgContract.Rebind();
                            }
                        }
                    }
                    else
                    {

                        Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                        errorMsg.Visible = true;
                        e.Canceled = true;
                    }
                }
                else
                {
                    e.Canceled = true;

                    string msg = "";
                    // Loop through all validation controls to see which
                    // generated the errors.
                    foreach (IValidator aValidator in this.Validators)
                    {
                        if (!aValidator.IsValid)
                        {
                            msg += "<br />" + aValidator.ErrorMessage;
                        }
                    }
                    Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                    errorMsg.Visible = true;
                    errorMsg.Text = msg;

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
            //call sp_GetContractRowPositionForGrid to get identify row number of newly added row
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
                cmd.CommandText = "sp_GetContractRowPositionForGrid";
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

        protected void rgContract_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {


                string addFlag = Request.QueryString["addmode"];
                if (addFlag == "true")
                {
                    rgContract.MasterTableView.IsItemInserted = true;
                    rgContract.Rebind();
                }
                    else
                {
                    Session["lastClientAdded"] = null;
                }

               
            }
        }

         protected void rgContract_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                GridEditFormItem editform = (GridEditFormItem)e.Item;
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                RadComboBox cbxClient = userControl.FindControl("cbClientName") as RadComboBox;

                try
                {
                    string hiddenClientID = (userControl.FindControl("hdClientID") as HiddenField).Value;

                    ClsClient client = new ClsClient();
                    //List <ClsClient> clientlist = client.GetClientInfo();
                    List<ClsClient> clientlist = client.GetClientList();
                    cbxClient.DataSource = clientlist;
                    cbxClient.SelectedValue = hiddenClientID;
                    cbxClient.DataBind();
                   
                    //cbxClient.SelectedValue = hiddenClientID;
                    if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                    {
                        //add mode
                        cbxClient.Enabled = true;
                        string lastClientName = (string)Session["lastClientAdded"];
                        if (lastClientName != null)
                        {
                            //get ID
                            ClsClient thisclient = PrepumaWebApp.App_Data.DAL.ClsClient.GetClient(lastClientName);
                            if (thisclient != null)
                            {
                                string lastClientID = thisclient.ClientID.ToString();
                                cbxClient.SelectedValue = lastClientID;
                                RadTextBox tbxContractName = (userControl.FindControl("ContractNameTextBox") as RadTextBox);
                                tbxContractName.Text = lastClientName;
                                //blank out Session variable after it's been used
                                Session["lastClientAdded"] = null;
                            }
                         }
                    }
                    else
                    {
                        //in edit mode, do not allow change of client or contract
                        cbxClient.Enabled = false;
                        RadTextBox rtbContract = userControl.FindControl("ContractTextBox") as RadTextBox;
                        if (Session["userRole"].ToString().ToLower() != "itadmin")
                        {
                            rtbContract.Enabled = false;
                        }
                        
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
                //lblDanger.Text = ex.Message;
                //pnlDanger.Visible = true;
            }

        }

         private bool checkFields(ClsContract objContract, UserControl userControl, bool newflag)
         {
             bool okFlag = true;

             //check the length
             if (objContract.ContractNumber.Length > 10)
             {
                 Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                 errorMsg.Text = "Max Length of Contract Number is 10";
                 okFlag = false;
             }

             //check for non-alphanumeric
             if (!objContract.ContractNumber.All(char.IsLetterOrDigit))
             {
                 Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                 errorMsg.Text = "Contract Number must be AlphaNumeric with no spaces";
                 okFlag = false;
             }
            
             if (newflag == true)
             {            
                //check for duplicate
                 ClsContract contract = PrepumaWebApp.App_Data.DAL.ClsContract.GetContract(objContract.ContractNumber);
                if (contract != null)
                {
                    Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                    errorMsg.Text = "Contract Number " + objContract.ContractNumber + " already exists for " + contract.ClientName;
                    okFlag = false;
                }
             }

             return okFlag;
         }

         private bool contractClassificationFlag(string contractNumber)
         {
             bool existingContract = false;
             try
             {

                 ContractClassification cc = ContractClassification.GetData(contractNumber);
                 if (cc != null)
                 {
                     existingContract = true;
                 }


             }
             catch (Exception ex)
             {
                 throw ex;
             }

             return existingContract;
         }

    }
}