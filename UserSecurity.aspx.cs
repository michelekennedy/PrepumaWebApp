using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using PrepumaWebApp.App_Data.DAL;
using Telerik.Web.UI;
using PI_Application;
using System.Data.SqlClient;
using System.Data;

namespace PrepumaWebApp
{
    public partial class UserSecurity : System.Web.UI.Page
    {
        String strConnString = ConfigurationManager.ConnectionStrings["PurolatorReportingConnectionString"].ConnectionString;
        Int16 idPI_Application = Convert.ToInt16(ConfigurationManager.AppSettings["AppID"]);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["userName"] != null && Session["appName"] != null && ((string)Session["userRole"] == "Admin" || (string)Session["userRole"] == "ITAdmin"))
                {

                    loadUsers();
                    rgUsers.Rebind();

                }
                else
                {
                    Response.Redirect("NoAccess.aspx");

                }
            }
            else
            {

            }
        }


        protected void rgUsers_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            loadUsers();
        }


        protected void loadUsers()
        {

            ClsAppUsers au = new ClsAppUsers();
            List<ClsAppUsers> appusers = au.GetListClsAppUsers(idPI_Application);
            rgUsers.DataSource = appusers;

        }

        protected void rgUsers_InsertCommand(object sender, GridCommandEventArgs e)
        {
            //hardcoding application for now - should do lookup backed on appNameContracts

            Int32 idEmployee;
            Int32 idPI_ApplicationRole;
            Int32 idPI_ApplicationUser;

            try
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                string msg = "";

                //Check that passwords match
                string pwd1 = (userControl.FindControl("txtPWD") as RadTextBox).Text;
                string pwd2 = (userControl.FindControl("txtPWD2") as RadTextBox).Text;
                if (pwd1 == "")
                {
                    msg = "Password is Required";
                }
                if (pwd1 != pwd2)
                {
                    msg = "Passwords do not match";
                }
                if (msg != "")
                {
                    pnlDanger.Visible = true;
                    lblDanger.Text = msg;
                }
                else
                {

                    //SET UP USER
                    RadComboBox cbxUsers = userControl.FindControl("cbxUsers") as RadComboBox;
                    idEmployee = Convert.ToInt16(cbxUsers.SelectedValue);


                    ClsEncryptDecrypt decrypt = new ClsEncryptDecrypt();
                    string encryptedPwd = decrypt.EncryptString(pwd1);


                    //Do Insert into tblPI_ApplicationUser, then use the id generated (@idPI_ApplicaitonUSer) and insert into tblPI_ApplicationUserRole

                    //PROCEDURE [dbo].[spPI_ApplicationUser_Insert]
                    //@idPI_Application int,
                    //@idEmployee int,
                    //@idPI_ApplicationUser int output

                    //MK - change to add password

                    SqlConnection cnn;
                    SqlCommand cmd;
                    cnn = new SqlConnection(strConnString);

                    cnn.Open();
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spPI_ApplicationUser_Insert";

                    cmd.Parameters.Add("@idPI_Application", SqlDbType.Int).Value = idPI_Application;
                    cmd.Parameters.Add("@idEmployee", SqlDbType.Int).Value = idEmployee;
                    cmd.Parameters.Add("@encryptedPassword", SqlDbType.VarChar).Value = encryptedPwd;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = pwd1;
                    var idPI_ApplicationUserOut = new SqlParameter
                    {
                        ParameterName = "@idPI_ApplicationUser",
                        Direction = ParameterDirection.Output,
                        SqlDbType = System.Data.SqlDbType.Int

                    };

                    cmd.Parameters.Add(idPI_ApplicationUserOut);

                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                    //assign output paramter so we can use it in next stp
                    idPI_ApplicationUser = (Int32)cmd.Parameters["@idPI_ApplicationUser"].Value;

                    cmd.Dispose();
                    cnn.Close();

                    //SET UP USER ROLE
                    RadComboBox cbxUserRoles = userControl.FindControl("cbxUserRole") as RadComboBox;
                    idPI_ApplicationRole = Convert.ToInt16(cbxUserRoles.SelectedValue);

                    //PROCEDURE [dbo].[spPI_ApplicationUserRole_Insert]
                    //@idPI_ApplicationUser int,
                    //@idPI_ApplicationRole int,
                    //@idPI_ApplicationUserRole int output    
                    //SqlConnection cnn;
                    SqlCommand cmd2;
                    cnn = new SqlConnection(strConnString);

                    cnn.Open();
                    cmd2 = new SqlCommand();
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = "spPI_ApplicationUserRole_Insert";

                    cmd2.Parameters.Add("@idPI_ApplicationUser", SqlDbType.Int).Value = idPI_ApplicationUser;
                    cmd2.Parameters.Add("@idPI_ApplicationRole", SqlDbType.Int).Value = idPI_ApplicationRole;

                    var idPI_ApplicationUserRoleOut = new SqlParameter
                    {
                        ParameterName = "@idPI_ApplicationUserRole",
                        Direction = ParameterDirection.Output,
                        SqlDbType = System.Data.SqlDbType.Int
                    };
                    cmd2.Parameters.Add(idPI_ApplicationUserRoleOut);


                    cmd2.Connection = cnn;
                    cmd2.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }

        }

        protected void rgUsers_UpdateCommand(object sender, GridCommandEventArgs e)
        {

            Int16 idPI_ApplicationUser;
            Int16 idPI_ApplicationRole;

            try
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                //If Passwords were changed, Check That Passwords Match
                //
                bool pwdChanged = (userControl.FindControl("CheckBox1") as CheckBox).Checked;
                string pwd1 = (userControl.FindControl("txtPWD") as RadTextBox).Text;
                bool allowUpdate = true;
                if (pwdChanged)
                {
                    string msg = "";

                    //Check that passwords match
                    string pwd2 = (userControl.FindControl("txtPWD2") as RadTextBox).Text;
                    if (pwd1 == "")
                    {
                        msg = "Password is Required";
                    }
                    if (pwd1 != pwd2)
                    {
                        msg = "Passwords do not match";
                    }
                    if (msg != "")
                    {
                        pnlDanger.Visible = true;
                        lblDanger.Text = msg;
                        allowUpdate = false;
                    }
                }
                //

                if (allowUpdate)
                {
                    HiddenField hdApplicationUser = userControl.FindControl("hdnApplicationUser") as HiddenField;
                    idPI_ApplicationUser = Convert.ToInt16(hdApplicationUser.Value);
                    RadComboBox cbxUserRoles = userControl.FindControl("cbxUserRole") as RadComboBox;
                    idPI_ApplicationRole = Convert.ToInt16(cbxUserRoles.SelectedValue);

                    //Do update of tblPI_ApplicationUserRole, set idPI_ApplicationRole for idPI_ApplicationUser
                    //stp: PROCEDURE [dbo].[spPI_ApplicationUserRole_Update]
                    //@idPI_ApplicationUser int,
                    //@idPI_ApplicationRole int

                    SqlConnection cnn;
                    SqlCommand cmd;
                    cnn = new SqlConnection(strConnString);

                    cnn.Open();
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spPI_ApplicationUserRole_Update";

                    cmd.Parameters.Add("@idPI_ApplicationUser", SqlDbType.Int).Value = idPI_ApplicationUser;
                    cmd.Parameters.Add("@idPI_ApplicationRole", SqlDbType.Int).Value = idPI_ApplicationRole;

                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();

                    //Check for Password Update
                    if (pwdChanged)
                    {
                        //stp: PROCEDURE [dbo].[spPI_ApplicationUser_Update]
                        //@idPI_Application int,
                        //@idEmployee int,
                        //@encryptedPassword nvarchar(50),
                        //@password nvarchar(50)

                        RadComboBox cbxUsers = userControl.FindControl("cbxUsers") as RadComboBox;
                        Int32 idEmployee = Convert.ToInt16(cbxUsers.SelectedValue);
                        ClsEncryptDecrypt decrypt = new ClsEncryptDecrypt();
                        string encryptedPwd = decrypt.EncryptString(pwd1);

                        cnn = new SqlConnection(strConnString);

                        cnn.Open();
                        cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spPI_ApplicationUser_Update";

                        cmd.Parameters.Add("@idPI_Application", SqlDbType.Int).Value = idPI_Application;
                        cmd.Parameters.Add("@idEmployee", SqlDbType.Int).Value = idEmployee;
                        cmd.Parameters.Add("@encryptedPassword", SqlDbType.VarChar).Value = encryptedPwd;
                        cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = pwd1;

                        cmd.Connection = cnn;
                        cmd.ExecuteNonQuery();

                        pnlDanger.Visible = false;

                    }
                }

            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }
        }

        protected void rgUsers_DeleteCommand(object source, GridCommandEventArgs e)
        {

            Int32 idPI_ApplicationUser = 0;
            int? idPI_ApplicationUserRole = 0;

            try
            {
                //MK - get the idPI_ApplicationUser and idPI_ApplicationUserRole from the grid item
                string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["idPI_ApplicationUser"].ToString();
                idPI_ApplicationUser = Convert.ToInt32(ID);
                //get idPI_ApplicationUserRole
                ClsAppUsers appuser = new ClsAppUsers();
                ClsAppUsers thisuser = appuser.GetAppUser(idPI_ApplicationUser);
                idPI_ApplicationUserRole = thisuser.idPI_ApplicationUserRole;



                //Delete from tblPI_ApplicationUserRole and tblPI_ApplicationUser
                //stp: [dbo].[spPI_ApplicationUserRole_Delete]
                //@idPI_ApplicationUserRole int
                SqlConnection cnn;
                SqlCommand cmd;
                cnn = new SqlConnection(strConnString);

                cnn.Open();
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spPI_ApplicationUserRole_Delete";

                cmd.Parameters.Add("@idPI_ApplicationUserRole", SqlDbType.Int).Value = idPI_ApplicationUserRole;

                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();


                //stp: PROCEDURE [dbo].[spPI_ApplicationUser_Delete]
                //@idPI_ApplicationUser int
                SqlCommand cmd2;
                cnn = new SqlConnection(strConnString);

                cnn.Open();
                cmd2 = new SqlCommand();
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "spPI_ApplicationUser_Delete";

                cmd2.Parameters.Add("@idPI_ApplicationUser", SqlDbType.Int).Value = idPI_ApplicationUser;

                cmd2.Connection = cnn;
                cmd2.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }


        }

        protected void rgUsers_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                var msg = "";

                if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                {
                    UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                    //Users
                    RadComboBox cbxUsers = userControl.FindControl("cbxUsers") as RadComboBox;
                    ClsEmployee emp = new ClsEmployee();
                    List<ClsEmployee> listUsers = emp.GetListClsEmployees();
                    cbxUsers.DataTextField = "UserName";
                    cbxUsers.DataValueField = "idEmployee";
                    cbxUsers.DataSource = listUsers;
                    cbxUsers.DataBind();

                    //Role
                    string UserRoleHidden = (userControl.FindControl("hdnUserRole") as HiddenField).Value;
                    RadComboBox cbxUserRoles = userControl.FindControl("cbxUserRole") as RadComboBox;
                    List<clsPI_ApplicationRole> listRoles = clsPI_ApplicationRole.GetApplicationRoles(idPI_Application);
                    cbxUserRoles.DataTextField = "RoleName";
                    cbxUserRoles.DataValueField = "ApplicationRoleId";
                    cbxUserRoles.DataSource = listRoles;
                    cbxUserRoles.DataBind();

                    //Show Pwd boxes and hide CheckBox   
                    RadTextBox userPwd = userControl.FindControl("txtPWD") as RadTextBox;
                    userPwd.Visible = true;
                    RadTextBox userPwd2 = userControl.FindControl("txtPWD2") as RadTextBox;
                    userPwd2.Visible = true;
                    Label lblUserPwd = userControl.FindControl("lblUserPwd") as Label;
                    lblUserPwd.Visible = true;
                    Label lblUserPwd2 = userControl.FindControl("lblUserPwd2") as Label;
                    lblUserPwd2.Visible = true;
                    Label lblpwdReq = userControl.FindControl("lblpwdReq") as Label;
                    lblpwdReq.Visible = true;
                    Label lblpwdReq2 = userControl.FindControl("lblpwdReq2") as Label;
                    lblpwdReq2.Visible = true;
                    CheckBox checkbox1 = userControl.FindControl("CheckBox1") as CheckBox;
                    checkbox1.Visible = false;
                    Label lblnoexist = userControl.FindControl("lblNoExisting") as Label;
                    lblnoexist.Visible = false;
                }
                else
                {

                    if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                    {
                        //************First calling dropdown list values selected in pop up edit form**************/ 
                        UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                        string UserHidden = (userControl.FindControl("hdnUser") as HiddenField).Value;

                        //USER NAME
                        try
                        {


                            RadComboBox cbxUsers = userControl.FindControl("cbxUsers") as RadComboBox;
                            ClsEmployee emp = new ClsEmployee();
                            List<ClsEmployee> listUsers = emp.GetListClsEmployees();

                            cbxUsers.DataTextField = "UserName";
                            cbxUsers.DataValueField = "idEmployee";
                            cbxUsers.DataSource = listUsers;
                            cbxUsers.DataBind();
                            cbxUsers.SelectedValue = UserHidden;

                            //if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                            //{

                            //}
                            //else
                            //{
                            cbxUsers.Enabled = false;
                            //}
                        }
                        catch (Exception ex)
                        {
                            msg = ex.Message;
                        }

                        //PASSWORD CHANGE
                        try
                        {
                            //Hide Pwd boxes and show CheckBox                    
                            RadTextBox userPwd = userControl.FindControl("txtPWD") as RadTextBox;
                            userPwd.Visible = false;
                            RadTextBox userPwd2 = userControl.FindControl("txtPWD2") as RadTextBox;
                            userPwd2.Visible = false;
                            Label lblUserPwd = userControl.FindControl("lblUserPwd") as Label;
                            lblUserPwd.Visible = false;
                            Label lblUserPwd2 = userControl.FindControl("lblUserPwd2") as Label;
                            lblUserPwd2.Visible = false;
                            Label lblpwdReq = userControl.FindControl("lblpwdReq") as Label;
                            lblpwdReq.Visible = false;
                            Label lblpwdReq2 = userControl.FindControl("lblpwdReq2") as Label;
                            lblpwdReq2.Visible = false;
                            CheckBox checkbox1 = userControl.FindControl("CheckBox1") as CheckBox;
                            checkbox1.Visible = true;
                            Label lblnoexist = userControl.FindControl("lblNoExisting") as Label;
                            lblnoexist.Visible = true;

                        }
                        catch (Exception ex)
                        {
                            msg = ex.Message;
                        }


                        //USER ROLE
                        try
                        {

                            string UserRoleHidden = (userControl.FindControl("hdnUserRole") as HiddenField).Value;
                            RadComboBox cbxUserRoles = userControl.FindControl("cbxUserRole") as RadComboBox;


                            List<clsPI_ApplicationRole> listRoles = clsPI_ApplicationRole.GetApplicationRoles(idPI_Application);

                            cbxUserRoles.DataTextField = "RoleName";
                            cbxUserRoles.DataValueField = "ApplicationRoleId";
                            cbxUserRoles.DataSource = listRoles;
                            cbxUserRoles.DataBind();
                            cbxUserRoles.SelectedValue = UserRoleHidden;

                            //if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                            //{

                            //}
                            //else
                            //{
                            //    //disable service code text box for Edit mode

                            //}

                        }
                        catch (Exception ex)
                        {
                            msg = ex.Message;
                        }
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