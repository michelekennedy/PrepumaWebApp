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
    public partial class EditSalesRep : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ibSlsRepTitlesPlus_Click(object sender, ImageClickEventArgs e)
        {

            if (ddlSlsRepTitles.Visible)
            {
                windowManager.RadAlert("Do you want to add a new Sales Title? ", 200, 200, "Please Confirm", "callBackFn", "");
                ddlSlsRepTitles.Visible = false;
                rfSlsRepTitles1.Display = ValidatorDisplay.None;
                rfSlsRepTitles2.Display = ValidatorDisplay.Static;
                rfSlsRepTitles1.Enabled = false;
                rfSlsRepTitles2.Enabled = true;
                txtSlsRepTitles.Visible = true;
                //ibSlsRepTitlesMinus.Visible = true;
                //ibSlsRepTitlesPlus.Visible = false;
            }

        }
       

        protected void ibSlsRepTitlesMinus_Click(object sender, ImageClickEventArgs e)
        {
            ddlSlsRepTitles.Visible = true;
            txtSlsRepTitles.Visible = false;
            //ibSlsRepTitlesMinus.Visible = false;
            //ibSlsRepTitlesPlus.Visible = true;                
        }

        protected void ddlTitle_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            string title = ddlSlsRepTitles.SelectedItem.Text;
            int? thresholdvalue = 0;
            ClsSalesRepTitle ct = new ClsSalesRepTitle();
            thresholdvalue = ct.GetThresholdbyTitle(title);           
            txtThresholdN.Text = thresholdvalue.ToString();
            ddlSlsRepTitles.Enabled = true;
        }


        
        
    }
}