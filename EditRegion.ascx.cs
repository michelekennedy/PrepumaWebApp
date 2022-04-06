using PrepumaWebApp.App_Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PrepumaWebApp
{
    public partial class EditRegion : System.Web.UI.UserControl
    {
        private object _dataItem = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }

        }

        protected void imgAddAccount_Click(object sender, ImageClickEventArgs e)
        {
            if (cbxDistrict.Visible)
            {
                windowManager.RadAlert("Do you want to add new District? ", 220, 200, "Please Confirm", "callBackFn", "");
                txtDistrict.Visible = true;
                cbxDistrict.Visible = false;
                imgAddAccount.Visible = false;
                ibAccountminus.Visible = true;
                rfacctypeminus.Display = ValidatorDisplay.Static;
                rfacctypeplus.Display = ValidatorDisplay.None;
                rfacctypeminus.Enabled = true;
                rfacctypeplus.Enabled = false;
            }

        }
        protected void ibAccountminus_Click(object sender, ImageClickEventArgs e)
        {
            cbxDistrict.Visible = true;
            txtDistrict.Visible = false;
            rfacctypeplus.Display = ValidatorDisplay.Static;
            rfacctypeminus.Display = ValidatorDisplay.None;
            imgAddAccount.Visible = true;
            ibAccountminus.Visible = false;
            rfacctypeminus.Enabled = false;
            rfacctypeplus.Enabled = true;
        }

        protected void cbxEditJusrisdiction_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            string state = cbxEditJusrisdiction.SelectedValue;
            List<ClsCity> citylst = ClsCity.GetCities();
            cbxCCLocation.Items.Clear();
            cbxCCLocation.DataSource = citylst.FindAll(x => x.state == state).OrderBy(x => x.city);
            cbxCCLocation.DataBind();
        }
    }
}