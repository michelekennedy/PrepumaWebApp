using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using PrepumaWebApp.App_Data.DAL;
using Telerik.Web.UI;
using System.Configuration;
using System.Collections.Specialized;
namespace PrepumaWebApp
{
    public partial class EditAccount : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

         protected void cbxClientName_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                //bind Contract List according to selected Client
                double? selClient = Convert.ToDouble(cbxClientName.SelectedValue);
                ClsContract contract = new ClsContract();
                List<ClsContract> contractlist = contract.GetContractInfo();
                contractlist = contractlist.FindAll(x => x.ClientID == selClient);
                cbxContractNumber.DataSource = contractlist;
                cbxContractNumber.DataBind();
                cbxContractNumber.SelectedIndex=0;
               
            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
       }

         protected void cbxContractNumber_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
         {
             try
             {
                 string contractNumber = cbxContractNumber.SelectedItem.Text;
                 ClsClient thisclient = PrepumaWebApp.App_Data.DAL.ClsClient.GetClientbyContractNumber(contractNumber);
                  if (thisclient != null)
                  {
                      string lastClientID = thisclient.ClientID.ToString();
                      cbxClientName.SelectedValue = lastClientID;
                  }

             }
             catch (Exception ex)
             {
                 lblErrorMessage.Visible = true;
                 lblErrorMessage.Text = ex.Message.ToString();
             }
         }
    }
}