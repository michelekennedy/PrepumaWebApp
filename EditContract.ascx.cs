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
    public partial class EditContract : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

                        
        }
        protected void cbClientName_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                string clientName = cbClientName.SelectedItem.Text;
                ContractNameTextBox.Text = clientName;
            }
            catch (Exception ex)
            {

            }

        }
    }
}