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
    public partial class Accounts : System.Web.UI.Page
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
                rgAccount.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }

        protected void rgAccount_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClsAccount account = new ClsAccount();
            List<ClsAccount> accountlist = account.GetAccountInfo();
            this.rgAccount.DataSource = accountlist;

        }

        protected void rgAccount_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {

                rgAccount.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Account Information";
            }
            else
            {
                rgAccount.MasterTableView.EditFormSettings.CaptionFormatString = "Add Account Information";
            }
        }  

        protected void rgAccount_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {


                string addFlag = Request.QueryString["addmode"];
                if (addFlag == "true")
                {
                    rgAccount.MasterTableView.IsItemInserted = true;
                    rgAccount.Rebind();
                }
                else
                {
                    Session["lastContractAdded"] = null;
                }


            }
        }
        

        protected void rgAccount_InsertCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                bool isNumeric;
                Int32 ivar;

                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                ClsAccount objAccount = new ClsAccount();
                objAccount.AcctID = -1;
                objAccount.Acctnbr = (userControl.FindControl("AccountTextBox") as RadTextBox).Text;
                objAccount.Acctnbr = objAccount.Acctnbr.ToUpper();
                objAccount.ContractNumber = (userControl.FindControl("cbxContractNumber") as RadComboBox).SelectedItem.Text;
                objAccount.ClientName = (userControl.FindControl("cbxClientName") as RadComboBox).SelectedItem.Text;
                objAccount.Updatedby = (string)(Session["userName"]);
                objAccount.Createdby = (string)(Session["userName"]);
                objAccount.CreatedOn = DateTime.Today;
                objAccount.UpdatedOn = DateTime.Today;
                objAccount.ActiveFlag = true;

                string selContract = (userControl.FindControl("cbxContractNumber") as RadComboBox).SelectedValue;
                //below line added by Jyothi to show user for which contract number added new account
                string selContractNumber = (userControl.FindControl("cbxContractNumber") as RadComboBox).SelectedItem.Text;
                isNumeric = Int32.TryParse(selContract, out ivar);
                if (isNumeric)
                {
                    objAccount.ContractID = ivar;
                }
                else
                {
                    objAccount.ContractID = 0;
                }

                string selClient = (userControl.FindControl("cbxClientName") as RadComboBox).SelectedValue;
                isNumeric = Int32.TryParse(selClient, out ivar);
                if (isNumeric)
                {
                    objAccount.ClientID = ivar;
                }
                else
                {
                    objAccount.ClientID = 0;
                }
                


                bool doUpdate = checkFields(objAccount, userControl);

                if (doUpdate)
                {
                    ClsAccount.UpdateAccount(objAccount);
                    //added by Jyothi
                    pnlsuccess.Visible = true;
                    lblSuccess.Text = "New Account Successfully added to the Contract Number  " + "'" + selContractNumber + "'";
                    Session["lastAccountAdded"] = objAccount.Acctnbr;
                    e.Canceled = true;
                    windowManager.RadConfirm("Would you like to set up a new Sales Info for this Account?", "confirmCallBackFn", 350, 200, null, "Please confirm");
                    //go to page of added record
                    Int32 position = getRecordPosition(objAccount.ClientName);
                    if (position>0) {
                
                        int locatedPage = position / rgAccount.PageSize;
                        if (rgAccount.CurrentPageIndex != locatedPage)
                        {
                            rgAccount.CurrentPageIndex = locatedPage;
                            rgAccount.Rebind();
                        }
                  }
                }
                else
                {
                    Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                    //errorMsg.Text = "Please Enter Required Fields";
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

        protected void rgAccount_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                bool isNumeric;
                Int32 ivar;

                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                ClsAccount objAccount = new ClsAccount();
                string acctid = (userControl.FindControl("hdAcctID") as HiddenField).Value;
                objAccount.AcctID = Convert.ToInt32(acctid);
                objAccount.Acctnbr = (userControl.FindControl("AccountTextBox") as RadTextBox).Text;
                objAccount.Acctnbr = objAccount.Acctnbr.ToUpper();
                objAccount.ContractNumber = (userControl.FindControl("cbxContractNumber") as RadComboBox).SelectedItem.Text;
                
                objAccount.Updatedby = (string)(Session["userName"]);
                objAccount.Createdby = (string)(Session["userName"]);
                objAccount.UpdatedOn = DateTime.Today;
                objAccount.ActiveFlag = true;

                string selContract = (userControl.FindControl("cbxContractNumber") as RadComboBox).SelectedValue;
              
                isNumeric = Int32.TryParse(selContract, out ivar);
                if (isNumeric)
                {
                    objAccount.ContractID = ivar;
                }
                else
                {
                    objAccount.ContractID = 0;
                }

 
                ClsAccount.UpdateAccount(objAccount);
                pnlsuccess.Visible = true;
                lblSuccess.Text = "Account Successfully Updated to the Contract Number  " + "'" + objAccount.ContractNumber + "'";                    

            }
            catch (Exception ex)
            {
                lblDanger.Text = ex.Message;
                pnlDanger.Visible = true;
            }

        }

        protected Int32 getRecordPosition(string ClientName) 
        {
            //call sp_GetAccountRowPositionForGrid to get identify row number of newly added row
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
                cmd.CommandText = "sp_GetAccountRowPositionForGrid";
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

        
        protected void rgAccount_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                GridEditFormItem editform = (GridEditFormItem)e.Item;
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                RadComboBox cbxClient = userControl.FindControl("cbxClientName") as RadComboBox;

                try
                {
                    string hiddenClientID = (userControl.FindControl("hdClient") as HiddenField).Value;

                    ClsClient client = new ClsClient();
                    List<ClsClient> clientlist = client.GetClientInfo();
                    cbxClient.DataSource = clientlist;
                    cbxClient.DataBind();
                    cbxClient.SelectedValue = hiddenClientID;
                    if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                    {
                        //add mode
                        cbxClient.Enabled = true;
                        //if addmode, fill in lastclient and lastcontract if there are not null;
                        string lastContractNumber = (string)Session["lastContractAdded"];
                        if (lastContractNumber != null)
                        {
                            //get ClientID
                            ClsContract thiscontract = PrepumaWebApp.App_Data.DAL.ClsContract.GetContract(lastContractNumber);
                            ClsClient thisclient = PrepumaWebApp.App_Data.DAL.ClsClient.GetClient(thiscontract.ClientName);
                            if (thisclient != null)
                            {
                                string lastClientID = thisclient.ClientID.ToString();
                                cbxClient.SelectedValue = lastClientID;
                                //blank out Session variable after it's been used

                                //fill in last contract
                                RadComboBox cbxCcontract = userControl.FindControl("cbxContractNumber") as RadComboBox;
                                if (lastContractNumber != null)
                                {
                                    string lastContractID = thiscontract.ContractID.ToString();
                                    ClsContract contract = new ClsContract();
                                    List<ClsContract> contractlist = contract.GetContractInfo();
                                    double? selClient = Convert.ToDouble(lastClientID);
                                    contractlist = contractlist.FindAll(x => x.ClientID == selClient);
                                    cbxCcontract.DataSource = contractlist;
                                    cbxCcontract.DataBind();
                                    cbxCcontract.SelectedValue = lastContractID;
                                    cbxCcontract.Enabled = true;
                                    //blank out Session variable after it's been used
                                    Session["lastContractAdded"] = null;
                                }
                                
                            }
                        }
                        else
                        {
                            //select all contracts
                            RadComboBox cbxCcontract = userControl.FindControl("cbxContractNumber") as RadComboBox;
                            ClsContract contract = new ClsContract();
                            List<ClsContract> allContracts = contract.GetContractInfo();
                            cbxCcontract.DataSource = allContracts;
                            cbxCcontract.DataBind();
                        }
                        //Load Available Accounts
                        //RadComboBox cbxAvailAccounts = userControl.FindControl("cbAvailAccounts") as RadComboBox;
                        //ClsAccount account = new ClsAccount();
                        //List<ClsAccount> accountlist = account.GetAccountInfo();
                        ////contractlist = contractlist.FindAll(x => x.ClientID == selClient);
                        //cbxAvailAccounts.DataSource = accountlist;
                        //cbxAvailAccounts.DataBind();
                        
                    }
                    else
                    {
                        //in edit mode
                        cbxClient.Enabled = false;
                        //load contract, select the right client and disable
                        RadComboBox cbxCcontract = userControl.FindControl("cbxContractNumber") as RadComboBox;
                        string hiddenContractID = (userControl.FindControl("hdContractNumber") as HiddenField).Value;
                        ClsContract contract = new ClsContract();
                        List<ClsContract> contractlist = contract.GetContractInfo();
                        cbxCcontract.DataSource = contractlist;
                        cbxCcontract.DataBind();
                        cbxCcontract.SelectedValue = hiddenContractID;
                        cbxCcontract.Enabled = true;
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

        private bool checkFields(ClsAccount objAccount, UserControl userControl)
        {
            bool okFlag = true;

            //check the length
            if (objAccount.Acctnbr.Length > 20)
            {
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                errorMsg.Text = "Max Length of Account Number is 20";
                okFlag = false;
            }

            //check for non-alphanumeric
            if (!objAccount.Acctnbr.All(char.IsLetterOrDigit))
     
            {
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                errorMsg.Text = "Account Number must be AlphaNumeric";
                okFlag = false;
            }
           
            //check for duplicate
            ClsAccount account = PrepumaWebApp.App_Data.DAL.ClsAccount.GetAccount(objAccount.Acctnbr);
            if (account != null)
            {
                //lblDanger.Text = "Account Number " + objAccount.Acctnbr + " already exists";
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                errorMsg.Text = "Account Number " + objAccount.Acctnbr + " already exists";
 
                okFlag = false;
            }

            return okFlag;
        }


        //protected void btnAddAcct_Click(object sender, EventArgs e)
        //{
            
        //    //Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
        //    //RadTextBox txtAcct = (RadTextBox)userControl.FindControl("AccountTextBox");
        //    //RadComboBox cbAcct = (RadComboBox)userControl.FindControl("cbAvailAccounts");
        //    //txtAcct.Visible = true;
        //    //cbAcct.Visible = false;
            

        //}
    }
}