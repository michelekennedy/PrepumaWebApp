using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrepumaWebApp
{
    public partial class EditUser : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == true)
            {
                lblUserPwd.Visible = true;
                txtPWD.Visible = true;
                lblUserPwd2.Visible = true;
                txtPWD2.Visible = true;
                lblpwdReq.Visible = true;
                lblpwdReq2.Visible = true;
                lblNoExisting.Visible = true;
            }
            else
            {
                lblUserPwd.Visible = false;
                txtPWD.Visible = false;
                lblUserPwd2.Visible = false;
                txtPWD2.Visible = false;
                lblpwdReq.Visible = false;
                lblpwdReq2.Visible = false;
                lblNoExisting.Visible = true;
            }

        }
    }
}