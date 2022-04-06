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
    public partial class ClientInfoDetails : System.Web.UI.UserControl
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
        protected void cbxClientName_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                ClsContract contract = new ClsContract();
                List<ClsContract> contractinfo = contract.GetContractInfo();
                double selClientId = Convert.ToDouble(cbxClientName.SelectedItem.Value);
                cbxContractName.DataSource = contractinfo.FindAll(x => x.ClientID == selClientId);
                cbxContractName.DataBind();
                //select first contract and first account
                cbxContractName.SelectedIndex = 0;
                selectFirstAccount();
            }
            catch (Exception ex)
            {
                //added by Jyothi to catch exception
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();
            }
            
        }
        
        protected void selectFirstAccount()
        {
            try
            {
                ClsAccount account = new ClsAccount();
                List<ClsAccount> accountInfo = account.GetAccountInfo();
                if (cbxContractName.SelectedItem != null)
                {
                    double selContractId = Convert.ToDouble(cbxContractName.SelectedItem.Value);
                    cbxAccountNumber.DataSource = accountInfo.FindAll(x => x.ContractID == selContractId);
                    cbxAccountNumber.DataBind();
                    cbxAccountNumber.SelectedIndex = 0;

                }
            }
            catch (Exception ex)
            {
                //added by Jyothi to catch exception
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = ex.Message.ToString();     
            }
                      

        }

        protected void cbxContractName_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            selectFirstAccount();
        }

        private void refreshDropDowns()
        {
            RefreshTerritory();
            RefreshSRIDs();
            RefreshSalesType();
            RefreshBizType();
            RefreshProductType();
            RefreshConRegion();
            RefreshAccRegion();
            RefreshShippingLoc();
            RefreshGateway();
            RefreshMaterialHanlng();
            RefreshReturns();
            RefreshCrossDocking();
        }

        protected void btnImgReload_Click(object sender, ImageClickEventArgs e)
        {
            string msg = "";
            try
            {
                refreshDropDowns();
            }
            catch (Exception ex)
            {
                 msg = ex.Message;
                 lblErrorMessage.Text = msg;
            }
        }

        private void RefreshTerritory()
        {
            string hiddenTerritory = (this.FindControl("hdTerritory") as HiddenField).Value;
            RadDropDownList territoryDDL = this.FindControl("rddlTerritoryEdit") as RadDropDownList;
            ClsSalesReps territory = new ClsSalesReps();
            List<ClsSalesReps> territorylist = territory.GetTerritory();
            territoryDDL.DataSource = territorylist;
            territoryDDL.DataBind();
            territoryDDL.SelectedValue = hiddenTerritory;
        }
        private void RefreshSRIDs()
        {
            string hiddenlsrid = (this.FindControl("hdLocalSRID") as HiddenField).Value;
            string hiddenssrid = (this.FindControl("hdStrategicSRID") as HiddenField).Value;
            string hiddenisp = (this.FindControl("hdISP") as HiddenField).Value;
            ClsSalesReps srid = new ClsSalesReps();
            List<ClsSalesReps> sridlist = srid.GetTerritory();

            RadDropDownList lsridDDL = this.FindControl("rddlLocalSRIDEdit") as RadDropDownList;
            lsridDDL.DataSource = sridlist;
            lsridDDL.DataBind();
            //lsridDDL.Items.Add("N/A");
            lsridDDL.SelectedValue = hiddenlsrid;

            RadDropDownList ssridDDL = this.FindControl("rddlStrategicSRIDEdit") as RadDropDownList;
            ssridDDL.DataSource = sridlist;
            ssridDDL.DataBind();
            //ssridDDL.Items.Add("N/A");
            ssridDDL.SelectedValue = hiddenssrid;

            RadDropDownList ispDDL = this.FindControl("rddlISP") as RadDropDownList;
            ispDDL.DataSource = sridlist;
            ispDDL.DataBind();
            //ispDDL.Items.Add("N/A");
            ispDDL.SelectedValue = hiddenisp;
        }

        private void RefreshSalesType()
        {
            string hiddenSalesType = (this.FindControl("hdSalesType") as HiddenField).Value;
            RadDropDownList salesTypeDDL = this.FindControl("rddlSalesTypeEdit") as RadDropDownList;
            ClsSalesType salesType = new ClsSalesType();
            List<ClsSalesType> salesTypelist = salesType.GetSalesType();
            salesTypeDDL.DataSource = salesTypelist;
            salesTypeDDL.DataBind();
            salesTypeDDL.SelectedText = hiddenSalesType;
        }

        private void RefreshBizType()
        {
            string hiddenBusType = (this.FindControl("hdBusinessType") as HiddenField).Value;
            RadDropDownList busTypeDDL = this.FindControl("rddlBusinesstypeEdit") as RadDropDownList;
            ClsBusinessType businessType = new ClsBusinessType();
            List<ClsBusinessType> businessTypelist = businessType.GetBizType();
            busTypeDDL.DataSource = businessTypelist;
            busTypeDDL.DataBind();
            busTypeDDL.SelectedText = hiddenBusType;
        }

        private void RefreshProductType()
        {
            string hiddenProdType = (this.FindControl("hdProductType") as HiddenField).Value;
            RadDropDownList prodTypeDDL = this.FindControl("rddlProductTypeEdit") as RadDropDownList;
            ClsProductType productType = new ClsProductType();
            List<ClsProductType> productTypelist = productType.GetProductType();
            prodTypeDDL.DataSource = productTypelist;
            prodTypeDDL.DataBind();
            prodTypeDDL.SelectedText = hiddenProdType;
        }

        private void RefreshConRegion()
        {
            string hiddenReg = (this.FindControl("hdRegion") as HiddenField).Value;
            RadDropDownList regionDDL = this.FindControl("rddlConRegionEdit") as RadDropDownList;
            ClsRegion region = new ClsRegion();
            List<ClsRegion> regionlist = region.GetRegions();
            regionDDL.DataSource = regionlist;
            regionDDL.DataBind();
            regionDDL.SelectedText = hiddenReg;
        }

        private void RefreshAccRegion()
        {
            string hiddenAccRegion = (this.FindControl("hdAccRegion") as HiddenField).Value;
            RadDropDownList accRegionDDL = this.FindControl("rddlAccRegionEdit") as RadDropDownList;
            ClsRegion region = new ClsRegion();
            List<ClsRegion> regionlist = region.GetRegions();
            accRegionDDL.DataSource = regionlist;
            accRegionDDL.DataBind();
            accRegionDDL.SelectedText = hiddenAccRegion;
        }

        private void RefreshShippingLoc()
        {
            string hiddenShipLoc = (this.FindControl("hdShippingLocation") as HiddenField).Value;
            RadDropDownList shippingLocDDL = this.FindControl("rddlShippingLocationEdit") as RadDropDownList;
            ClsRegion region = new ClsRegion();
            List<ClsRegion> regionlist = region.GetRegions();
            shippingLocDDL.DataSource = regionlist;
            shippingLocDDL.DataBind();
            shippingLocDDL.SelectedText = hiddenShipLoc;
        }
        private void RefreshGateway()
        {
            string hiddenGatewayID = (this.FindControl("hdGatewayID") as HiddenField).Value;
            RadDropDownList gatewayDDL = this.FindControl("rddlGatewayIDEdit") as RadDropDownList;
            ClsGATEWAY gateway = new ClsGATEWAY();
            List<ClsGATEWAY> gatewayList = gateway.GetGatewayInfo();
            gatewayDDL.DataSource = gatewayList;
            gatewayDDL.DataBind();
            gatewayDDL.SelectedValue = hiddenGatewayID;
        }

        private void RefreshMaterialHanlng()
        {
            string hiddenMATHANDCPPID = (this.FindControl("hdMATHANDCPPID") as HiddenField).Value;
            RadDropDownList mathandcppidDDL = this.FindControl("rddlMATHANCPPIDEdit") as RadDropDownList;
            ClsMATHANCPP mathandcppid = new ClsMATHANCPP();
            List<ClsMATHANCPP> mathandcppidList = mathandcppid.GetMathandcppInfo();
            mathandcppidDDL.DataSource = mathandcppidList;
            mathandcppidDDL.DataBind();
            mathandcppidDDL.SelectedValue = hiddenMATHANDCPPID;
        }

        private void RefreshReturns()
        {
            string hiddenRTNSID = (this.FindControl("hdRTNSID") as HiddenField).Value;
            RadDropDownList rtnsIdDDL = this.FindControl("rddlRTNSIDEdit") as RadDropDownList;
            ClsRTNSCPP rtnsId = new ClsRTNSCPP();
            List<ClsRTNSCPP> rtnsIdList = rtnsId.GetRtnsIdInfo();
            rtnsIdDDL.DataSource = rtnsIdList;
            rtnsIdDDL.DataBind();
            rtnsIdDDL.SelectedValue = hiddenRTNSID;
        }
        private void RefreshCrossDocking()
        {
            string hiddenCDCPLB = (this.FindControl("hdCDCPLB") as HiddenField).Value;
            RadDropDownList cdcplbIdDDL = this.FindControl("rddlCDCPLBEdit") as RadDropDownList;
            ClsCrossDockCPLB cdcplbId = new ClsCrossDockCPLB();
            List<ClsCrossDockCPLB> cdcplbIdList = cdcplbId.GetcdcplbIdInfo();
            cdcplbIdDDL.DataSource = cdcplbIdList;
            cdcplbIdDDL.DataBind();
            cdcplbIdDDL.SelectedValue = hiddenCDCPLB;
        }

        private void OpenAddPopup(string Url,int Height, int Width)
        {
            RadWindowManager windowManager = new RadWindowManager();
            RadWindow window1 = new RadWindow();
            // Set the window properties   
            window1.NavigateUrl = Url;
            window1.ID = "RadWindow1";
            window1.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
            windowManager.Windows.Add(window1);
            this.Controls.Add(window1);
            window1.Behaviors = WindowBehaviors.Move | WindowBehaviors.Close;
            window1.Width = Height;
            window1.Height = Width;
        }

        protected void rddlAccRegionEdit_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            string msg = string.Empty;
            RadDropDownList acctRegionDDL = this.FindControl("rddlAccRegionEdit") as RadDropDownList;
            RadDropDownList cdcplbIdDDL = this.FindControl("rddlCDCPLBEdit") as RadDropDownList;
            RadDropDownList mathandcppidDDL = this.FindControl("rddlMATHANCPPIDEdit") as RadDropDownList;
            string sRegion = acctRegionDDL.SelectedValue;
            try
            {
                string sCrdkByRegion = ClsRelCrossDocMATRegion.getCrossDockByRegion(sRegion);
                ClsCrossDockCPLB cdcplbId = new ClsCrossDockCPLB();
                List<ClsCrossDockCPLB> cdcplbIdList = cdcplbId.GetcdcplbIdInfo();
                //List<ClsCrossDockCPLB> cdcplbIdList = ClsCrossDockCPLB.GetCrossDckgById(sCrdkByRegion);
                cdcplbIdDDL.DataSource = cdcplbIdList;
                cdcplbIdDDL.DataBind();
                cdcplbIdDDL.SelectedValue = sCrdkByRegion;
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
                cdcplbIdDDL.SelectedIndex = -1;
            }

            try
            {
                string sMatHnlgByRegion = ClsRelCrossDocMATRegion.getMatHnlgByRegion(sRegion);
                ClsMATHANCPP mathandcppid = new ClsMATHANCPP();
                List<ClsMATHANCPP> mathandcppidList = mathandcppid.GetMathandcppInfo();
                mathandcppidDDL.DataSource = mathandcppidList;
                mathandcppidDDL.DataBind();
                mathandcppidDDL.SelectedValue = sMatHnlgByRegion;
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
                mathandcppidDDL.SelectedIndex = -1;
            }
                
            
        }

        protected void StartDate_SelectedDateChanged(object sender, EventArgs e)
        {
            string msg = string.Empty;
            try
            {
                DateTime startdate = Convert.ToDateTime(StartDate.SelectedDate);
                DateTime enddate = startdate.AddMonths(12);
                DateTime rolloffqtr = startdate;
                EndDate.SelectedDate = enddate;
                RolloffQuarter.SelectedDate = LastDayOfQuarter(enddate);
                EndDate.Enabled = false;
                RolloffQuarter.Enabled = false;

            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();                
            }
            

        }

        DateTime LastDayOfQuarter(DateTime thedate)
        {
            int quarter = ((thedate.Month - 1) / 3) + 1;
            int lastMonthInQuarter = quarter * 3;
            int lastDayInMonth = DateTime.DaysInMonth(thedate.Year, lastMonthInQuarter);
            return new DateTime(thedate.Year, lastMonthInQuarter, lastDayInMonth);
        }

    }
}