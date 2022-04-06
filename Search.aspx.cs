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
using xi = Telerik.Web.UI.ExportInfrastructure;
using Telerik.Web.UI.GridExcelBuilder;


namespace PrepumaWebApp
{
    public partial class Contracts : System.Web.UI.Page
    {
        ClientInfoContext ctxt = new ClientInfoContext();
        public List<ClientInfo> clientData = null;
        ClientInfo objClient = new ClientInfo();
        public List<ClsRegion> regionlist;
        string[] reqLabels = new string[] { "labelClientName", "labelContractNumber", "labelAccountNumber", "labelSalesType", "labelContractRegion", "labelTerritory", "lblRelationshipName", "labelAcctRegion" };

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                try
                {               
                    if (Session["userName"] != null && Session["appName"] != null)
                    {
                        GetClientInfo();
                        //Do not allow edit for User Role Audit
                        if (Session["userRole"].ToString().ToLower() == "audit")
                        {
                            rgClients.MasterTableView.GetColumn("Edit").Display = false; 
                        }
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("Default.aspx");
                }

            }
        }

        protected void rgClients_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {


                string addFlag = Request.QueryString["addmode"];
                if (addFlag == "true")
                {
                    rgClients.MasterTableView.IsItemInserted = true;
                    rgClients.Rebind();
                }
                else
                {
                    Session["lastAccountAdded"] = null;
                }


            }
        }

        //GetClientInfo method binds rgClients grid on page initial load
        private void GetClientInfo()
        {
            try
            {
                rgClients.DataSource = ctxt.GetData();
            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }
        }

        protected void rgClients_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GetClientInfo();
        }

        protected void rgClients_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                rgClients.ExportSettings.FileName = "ClientInfo";                
                //rgClients.MasterTableView.GetColumn("Edit").Visible = false;

                rgClients.ExportSettings.IgnorePaging = true;
                rgClients.ExportSettings.ExportOnlyData = true;
                rgClients.ExportSettings.OpenInNewWindow = true;
                rgClients.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;


            }

            if (e.CommandName == "Edit")
            {
                rgClients.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Sales Information";
                rgClients.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            else
            {
                rgClients.MasterTableView.EditFormSettings.CaptionFormatString = "Add Sales Information";
                rgClients.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            
           
        }
        //If the page size changed rgClients_PageSizeChanged will be fired and binds the data according to the page size.
        protected void rgClients_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            rgClients.CurrentPageIndex = e.NewPageSize;
            rgClients.CurrentPageIndex = 0;
            GetClientInfo();
        }

        //MK - ADDED BELOW
        protected void rgClients_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //Jyothi - added below
            if (e.Item is GridDataItem)
            {
                //adding all the links to the hyperlink column, to navigate when it is clicked. 
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["Acctnbr"].Text;
                HyperLink hLink = (HyperLink)item["Acctnbr"].Controls[0];
                hLink.ForeColor = System.Drawing.Color.Blue;
                ClientInfo row = (ClientInfo)item.DataItem;
                
                hLink.Attributes["onclick"] ="OpenWin('" + row.ClientName + "','" +row.ContractNumber +"','"+row.Acctnbr +"');";
                Session["viewClientInfo"] = row;
                // Jyothi added end 
            }
            try
            {
                var msg = "";
                //Need to select drop downs after data binding
                if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    //************First calling dropdown list values selected in pop up edit form**************/ 
                    UserControl userControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                    GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;
                    //SHARE PERCENT
                    try
                    {
                        RadNumericTextBox sharePercentTXT = userControl.FindControl("SharePctTextBoxN") as RadNumericTextBox;
                        sharePercentTXT.Enabled = false;
                        
                        //if (sharePercentTXT.Text == "1")
                        //{
                        //    sharePercentTXT.Text = "100";
                        //}
                        //else
                        //{
                        //    sharePercentTXT.Text = "0";
                        //}//sharePercentTXT.[DisplayFormat(DataFormatString = @"{0:#\%}")]
                        string sharePct = sharePercentTXT.Text;
                        float fSharePct;
                        bool isNumeric = float.TryParse(sharePct, out fSharePct);
                        if (!isNumeric)
                        {
                            fSharePct = 100;
                        }
                        if (fSharePct <= 1)
                        {
                            //Share Pct stored as 1 for 100 or .5 for 50 - display as percentage
                            fSharePct = fSharePct * 100;
                        }
                        sharePercentTXT.Text = fSharePct.ToString();
                           
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }
                    //CLIENT
                    try
                    {
                        string hiddenClient = (userControl.FindControl("hdClient") as HiddenField).Value;
                        RadComboBox clientCBX = userControl.FindControl("cbxClientName") as RadComboBox;
                        ClsClient client = new ClsClient();
                        List<ClsClient> clientlist = client.GetClientInfo();
                        clientCBX.DataSource = clientlist;
                        clientCBX.DataBind();
                        clientCBX.SelectedValue = hiddenClient;
                        if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                        {
                            clientCBX.Enabled = true;
                            
                        }
                        else
                        {
                            //disable for Edit mode
                            clientCBX.Enabled = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    //CONTRACT
                    try
                    {
                        RadComboBox contractCBX = userControl.FindControl("cbxContractName") as RadComboBox;
                        if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                        {
                            //leave blank so it will be filled in when client is selected
                            contractCBX.Enabled = true;
                        }
                        else
                        {
                            //disable for Edit mode
                            string hiddenContract = (userControl.FindControl("hdContract") as HiddenField).Value;
                            ClsContract contract = new ClsContract();
                            List<ClsContract> contractlist = contract.GetContractInfo();
                            contractCBX.DataSource = contractlist;
                            contractCBX.DataBind();
                            contractCBX.SelectedValue = hiddenContract;
                            contractCBX.Enabled = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    //ACCOUNT
                    try
                    {
                        RadComboBox acctCBX = userControl.FindControl("cbxAccountNumber") as RadComboBox;
                        
                        if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                        {
                            acctCBX.Enabled = true;
                        }
                        else
                        {
                            
                            //disable for Edit mode
                            string hiddenAcct = (userControl.FindControl("hdAccount") as HiddenField).Value;
                            //If there is no account in edit mode, give alert message
                            if (String.IsNullOrEmpty(hiddenAcct))
                            {
                                windowManager.RadAlert("No Account Associated - Add Account in Account Screen first.", 350, 200, "Alert", "alertCallBackFn", "SalesInfo");
                            }
                            acctCBX.SelectedValue = hiddenAcct;
                            ClsAccount account = new ClsAccount();
                            List<ClsAccount> accountlist = account.GetAccountInfo();
                            acctCBX.DataSource = accountlist;
                            acctCBX.DataBind();
                            acctCBX.Enabled = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }


                    //RELATIONSHIP
                    try
                    {
                        RadDropDownList rddlRelationship = userControl.FindControl("rddlRelationship") as RadDropDownList;
                        string hiddenRelationship = (userControl.FindControl("hdRelationship") as HiddenField).Value;
                        hiddenRelationship = hiddenRelationship.Trim();
                        ClsCustomer cust = new ClsCustomer();
                        List<ClsCustomer> customerlist = cust.GetActiveCustomers();
                        rddlRelationship.DataSource = customerlist;
                        rddlRelationship.DataBind();
                        rddlRelationship.SelectedValue = hiddenRelationship;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }


                    //LOGIC FOR AUTOMATIC ADD - link from add Account Add
                    //Use last account number added from session to get and fill in Client, Contract and Account
                    string lastAccountNumber = (string)Session["lastAccountAdded"];
                    if (lastAccountNumber != null)
                    {
                      

                        //get IDs
                        ClsAccount thisaccount = PrepumaWebApp.App_Data.DAL.ClsAccount.GetAccount(lastAccountNumber);
                        ClsContract thiscontract = PrepumaWebApp.App_Data.DAL.ClsContract.GetContract(thisaccount.ContractID);
                        ClsClient thisclient = PrepumaWebApp.App_Data.DAL.ClsClient.GetClient(thiscontract.ClientName);

                      

                        if (thisclient != null)
                        {
                            //Client
                            string lastClientID = thisclient.ClientID.ToString();
                            RadComboBox clientCBX = userControl.FindControl("cbxClientName") as RadComboBox;
                            clientCBX.SelectedValue = lastClientID;
                        }

                        if (thiscontract != null)
                        { 
                            //Contract
                            string lastContractID = thiscontract.ContractID.ToString();
                            RadComboBox cbxContractName = (userControl.FindControl("cbxContractName") as RadComboBox);
                            ClsContract contract = new ClsContract();
                            List<ClsContract> contractinfo = contract.GetContractInfo();
                            double selClientId = Convert.ToDouble(thisclient.ClientID);
                            cbxContractName.DataSource = contractinfo.FindAll(x => x.ClientID == selClientId);
                            cbxContractName.DataBind();
                            cbxContractName.SelectedValue = lastContractID;
                            //If pre-existing contract, fill in ContractClassification values
                            if (contractClassificationFlag(thiscontract.ContractNumber) == true)
                                fillInContractValues(userControl, thiscontract.ContractNumber);
                                                        
                        }

                        if (thisaccount != null)
                        {
                            //Account
                            string lastAccountID = thisaccount.Acctnbr.ToString();
                            RadComboBox cbxAccountNumber = (userControl.FindControl("cbxAccountNumber") as RadComboBox);
                            cbxAccountNumber.Text = lastAccountID;

                            ClsAccount account = new ClsAccount();
                            List<ClsAccount> accountInfo = account.GetAccountInfo();
                            double selContractId = Convert.ToDouble(thiscontract.ContractID);
                            cbxAccountNumber.DataSource = accountInfo.FindAll(x => x.ContractID == selContractId);
                            cbxAccountNumber.DataBind();
                            cbxAccountNumber.SelectedValue = lastAccountID;

                          
                        }
                                              
                            
                        //blank out Session variable after it's been used
                        Session["lastAccountAdded"] = null;
                    }


                    //REGION
                    try
                    {
                        string hiddenReg = (userControl.FindControl("hdRegion") as HiddenField).Value;
                        RadDropDownList regionDDL = userControl.FindControl("rddlConRegionEdit") as RadDropDownList;
                        ClsRegion region = new ClsRegion();
                        regionlist = region.GetRegions();
                        regionDDL.DataSource = regionlist;
                        regionDDL.DataBind();
                        regionDDL.SelectedText = hiddenReg;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    //TERRITORY
                    try
                    {
                        string hiddenTerritory = (userControl.FindControl("hdTerritory") as HiddenField).Value;
                        RadDropDownList territoryDDL = userControl.FindControl("rddlTerritoryEdit") as RadDropDownList;
                        ClsSalesReps territory = new ClsSalesReps();
                        List<ClsSalesReps> territorylist = territory.GetTerritory();
                        territoryDDL.DataSource = territorylist;
                        territoryDDL.DataBind();
                        territoryDDL.SelectedValue = hiddenTerritory;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }
                    //SALES TYPE
                    try
                    {
                        string hiddenSalesType = (userControl.FindControl("hdSalesType") as HiddenField).Value;
                        RadDropDownList salesTypeDDL = userControl.FindControl("rddlSalesTypeEdit") as RadDropDownList;
                        ClsSalesType salesType = new ClsSalesType();
                        List<ClsSalesType> salesTypelist = salesType.GetSalesType();
                        salesTypeDDL.DataSource = salesTypelist;
                        salesTypeDDL.DataBind();
                        salesTypeDDL.SelectedText = hiddenSalesType;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    //Added 4 new fields to the Contract Classification table
                    //GateWayID
                    try
                    {
                        string hiddenGatewayID = (userControl.FindControl("hdGatewayID") as HiddenField).Value;
                        RadDropDownList gatewayDDL = userControl.FindControl("rddlGatewayIDEdit") as RadDropDownList;
                        ClsGATEWAY gateway = new ClsGATEWAY();
                        List<ClsGATEWAY> gatewayList = gateway.GetGatewayInfo();
                        gatewayDDL.DataSource = gatewayList;
                        gatewayDDL.DataBind();
                        gatewayDDL.SelectedValue = hiddenGatewayID;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    //MATHANDCPPID
                    try
                    {
                        string hiddenMATHANDCPPID = (userControl.FindControl("hdMATHANDCPPID") as HiddenField).Value;
                        RadDropDownList mathandcppidDDL = userControl.FindControl("rddlMATHANCPPIDEdit") as RadDropDownList;
                        ClsMATHANCPP mathandcppid = new ClsMATHANCPP();
                        List<ClsMATHANCPP> mathandcppidList = mathandcppid.GetMathandcppInfo();
                        mathandcppidDDL.DataSource = mathandcppidList;
                        mathandcppidDDL.DataBind();
                        mathandcppidDDL.SelectedValue = hiddenMATHANDCPPID;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    //RTNSID
                    try
                    {
                        string hiddenRTNSID = (userControl.FindControl("hdRTNSID") as HiddenField).Value;
                        RadDropDownList rtnsIdDDL = userControl.FindControl("rddlRTNSIDEdit") as RadDropDownList;
                        ClsRTNSCPP rtnsId = new ClsRTNSCPP();
                        List<ClsRTNSCPP> rtnsIdList = rtnsId.GetRtnsIdInfo();
                        rtnsIdDDL.DataSource = rtnsIdList;
                        rtnsIdDDL.DataBind();
                        rtnsIdDDL.SelectedValue = hiddenRTNSID;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    //CDCPLBID
                    try
                    {
                        string hiddenCDCPLB = (userControl.FindControl("hdCDCPLB") as HiddenField).Value;
                        RadDropDownList cdcplbIdDDL = userControl.FindControl("rddlCDCPLBEdit") as RadDropDownList;
                        ClsCrossDockCPLB cdcplbId = new ClsCrossDockCPLB();
                        List<ClsCrossDockCPLB> cdcplbIdList = cdcplbId.GetcdcplbIdInfo();
                        //List<ClsCrossDockCPLB> cdcplbIdList = ClsCrossDockCPLB.getCrossDockWithRegion();
                        cdcplbIdDDL.DataSource = cdcplbIdList;
                        cdcplbIdDDL.DataBind();
                        cdcplbIdDDL.SelectedValue = hiddenCDCPLB;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    //BUSINESS TYPE
                    try
                    {
                        string hiddenBusType = (userControl.FindControl("hdBusinessType") as HiddenField).Value;
                        RadDropDownList busTypeDDL = userControl.FindControl("rddlBusinesstypeEdit") as RadDropDownList;
                        ClsBusinessType businessType = new ClsBusinessType();
                        List<ClsBusinessType> businessTypelist = businessType.GetBizType();
                        busTypeDDL.DataSource = businessTypelist;
                        busTypeDDL.DataBind();
                        busTypeDDL.SelectedText = hiddenBusType;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }
                    //LOCAL and STRATEGIC SRID and ISP
                    try
                    {
                        string hiddenlsrid = (userControl.FindControl("hdLocalSRID") as HiddenField).Value;
                        string hiddenssrid = (userControl.FindControl("hdStrategicSRID") as HiddenField).Value;
                        string hiddenisp = (userControl.FindControl("hdISP") as HiddenField).Value;
                        ClsSalesReps srid = new ClsSalesReps();
                        List<ClsSalesReps> sridlist = srid.GetTerritory();

                        ClsSalesReps naItem = new ClsSalesReps();
                        naItem.salesrep_id="N/A";
                        naItem.SalesRepID="N/A";
                        sridlist.Insert(0, naItem);

                        RadDropDownList lsridDDL = userControl.FindControl("rddlLocalSRIDEdit") as RadDropDownList;
                        lsridDDL.DataSource = sridlist;
                        lsridDDL.DataBind();
                        lsridDDL.SelectedValue = hiddenlsrid;
                       

                        RadDropDownList ssridDDL = userControl.FindControl("rddlStrategicSRIDEdit") as RadDropDownList;
                        ssridDDL.DataSource = sridlist;
                        ssridDDL.DataBind();
                        ssridDDL.SelectedValue = hiddenssrid;

                        RadDropDownList ispDDL = userControl.FindControl("rddISP") as RadDropDownList;
                        ispDDL.DataSource = sridlist;
                        ispDDL.DataBind();
                        ispDDL.SelectedValue = hiddenisp;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    //PRODUCT TYPE
                    try
                    {
                        string hiddenProdType = (userControl.FindControl("hdProductType") as HiddenField).Value;
                        RadDropDownList prodTypeDDL = userControl.FindControl("rddlProductTypeEdit") as RadDropDownList;
                        ClsProductType productType = new ClsProductType();
                        List<ClsProductType> productTypelist = productType.GetProductType();
                        prodTypeDDL.DataSource = productTypelist;
                        prodTypeDDL.DataBind();
                        prodTypeDDL.SelectedText = hiddenProdType;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    //REGION ACCT CLASSIFICATION
                    try
                    {
                        string hiddenAccRegion = (userControl.FindControl("hdAccRegion") as HiddenField).Value;
                        RadDropDownList accRegionDDL = userControl.FindControl("rddlAccRegionEdit") as RadDropDownList;
                        ClsRegion region = new ClsRegion();
                        List<ClsRegion> regionlist = region.GetRegions();
                        accRegionDDL.DataSource = regionlist;
                        accRegionDDL.DataBind();
                        accRegionDDL.SelectedText = hiddenAccRegion;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    //SHIPPING LOCATION
                    try
                    {
                        string hiddenShipLoc = (userControl.FindControl("hdShippingLocation") as HiddenField).Value;
                        RadDropDownList shippingLocDDL = userControl.FindControl("rddlShippingLocationEdit") as RadDropDownList;
                        ClsRegion region = new ClsRegion();
                        List<ClsRegion> regionlist = region.GetRegions();
                        shippingLocDDL.DataSource = regionlist;
                        shippingLocDDL.DataBind();
                        shippingLocDDL.SelectedText = hiddenShipLoc;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    //LAST UPDATED BY
                    try
                    {
                        string lastUpdatedText = "";
                        string hiddenUpdatedby = (userControl.FindControl("hdAcctUpdatedby") as HiddenField).Value;
                        string hiddenUpdatedon = (userControl.FindControl("hdAcctUpdatedOn") as HiddenField).Value;
                        Label lblUpdatedby = userControl.FindControl("lblLastUpdatedby") as Label;
                        if (hiddenUpdatedby != "")
                        {
                          lastUpdatedText= "Account Classification Data last updated by " + hiddenUpdatedby;

                          if (hiddenUpdatedon != "")
                              lastUpdatedText = lastUpdatedText + " on " + hiddenUpdatedon;
                          lblUpdatedby.Text = lastUpdatedText;
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    //SHARE PERCENT
                    try
                    {
                        String sharePct = (userControl.FindControl("SharePctTextBoxN") as RadNumericTextBox).Text;
                        if (sharePct == "")
                        {
                            (userControl.FindControl("SharePctTextBoxN") as RadNumericTextBox).Text = "100";
                        }

                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }
                    
                    //STARTDATE
                    string hiddenStartDate = (userControl.FindControl("hdStartDate") as HiddenField).Value;
                    if (hiddenStartDate != "")
                    {
                        try
                        {
                            DateTime startdate = Convert.ToDateTime(hiddenStartDate);
                            (userControl.FindControl("StartDate") as RadDatePicker).SelectedDate = startdate;
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    //ENDDATE
                    string hiddenEndDate = (userControl.FindControl("hdEndDate") as HiddenField).Value;
                    if (hiddenEndDate != "")
                    {
                        try
                        {
                            DateTime enddate = Convert.ToDateTime(hiddenEndDate);
                            (userControl.FindControl("EndDate") as RadDatePicker).SelectedDate = enddate;
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    //ROLLOFF QUARTER
                    string hiddenRolloffQtr = (userControl.FindControl("hdRolloffQtr") as HiddenField).Value;
                    if (hiddenRolloffQtr != "")
                    {
                        try
                        {
                            DateTime rolloffqtr = Convert.ToDateTime(hiddenRolloffQtr);
                            (userControl.FindControl("RolloffQuarter") as RadDatePicker).SelectedDate = rolloffqtr;
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    //CURRENCY
                    //string hiddenCurrency = (userControl.FindControl("hdCurrency") as HiddenField).Value;
                    //if (hiddenCurrency != "")
                    //{
                    //    try
                    //    {
                    //        //String currency = ;
                    //        (userControl.FindControl("rddlCurrency") as RadComboBox).SelectedValue = hiddenCurrency;

                         

                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }
                    //}
                    //else
                    //{

                    //}

                }
            }
            catch (Exception ex)
            {

                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }
        }

        

        protected void rgClients_CancelCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //lblWarning.Text = "";
            //pnlwarning.Visible = false;
        }


        protected void rgClient_UpdateCommand(object source, GridCommandEventArgs e)
        {

            GridEditableItem editedItem = e.Item as GridEditableItem;
            bool doUpdate = true;

            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            ClientInfo client = new ClientInfo();

            try
            {

                //initialize error to ""
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                errorMsg.Text = "";

                //Reset Required Labels back to Black
                resetRequired(reqLabels, userControl);

                //POPULATE the objClient
                objClient = populateClientObj(userControl);


                //CHECK for errors
                doUpdate = checkItemValues(objClient, userControl);


                if (doUpdate)
                {
                    bool updateDone = false;
                    bool existingContract = contractClassificationFlag(objClient.ContractNumber);
                     
                    bool existingAccount = contractAccountClassificationFlag(objClient.ContractNumber, objClient.Acctnbr);
                    //if ContractClassificationExists, do update else do insert
                    string SQLerrorMsg = "";
                    if (existingContract)
                    {

                        if (existingAccount)
                        {
                            //Both Exist, update together in a transaction
                            SQLerrorMsg = client.doSQLUpdate(objClient, (string)(Session["userName"]));
                        }
                        else
                        {
                            //ContractClassification gets updated, ContractAccountClassification gets inserted
                            SQLerrorMsg = client.doSQLUpdateCCInsertCAC(objClient, (string)(Session["userName"]));
                            
                        }
                        if (SQLerrorMsg != "")
                        {
                            errorMsg.Text = SQLerrorMsg;
                            errorMsg.Visible = true;
                            e.Canceled = true;
                        }
                        else
                        {
                            //rgClients.DataBind();
                            GetClientInfo();
                            updateDone = true;
                            //SearchBindings();
                        }
                    }
                    else
                    {

                        if (existingAccount)
                        {
                            //Insert CC, update CAC - update both together in a transaction
                            SQLerrorMsg = client.doSQLInsertCCUpdateCAC(objClient, (string)(Session["userName"]));
                            if (SQLerrorMsg != "")
                            {
                                errorMsg.Text = SQLerrorMsg;
                                errorMsg.Visible = true;
                                e.Canceled = true;
                            }
                            else
                            {
                                pnlsuccess.Visible = true;
                                lblSuccess.Text = "New SalesInfo Successfully added to the Contract Name " + "'" + objClient.ContractName + "'";
                                updateDone = true;
                                GetClientInfo();
                            }

                        }
                        else
                        {
                            //Both are Updated update both together in a transaction
                            SQLerrorMsg = client.doSQLInsert(objClient, (string)(Session["userName"]));
                            if (SQLerrorMsg != "")
                            {
                                errorMsg.Text = SQLerrorMsg;
                                errorMsg.Visible = true;
                                e.Canceled = true;
                            }
                            else
                            {
                                pnlsuccess.Visible = true;
                                lblSuccess.Text = "New SalesInfo Successfully added to the Contract Name " + "'" + objClient.ContractName + "'";
                                updateDone = true;
                                GetClientInfo();
                            }
                        }
                    }
                    if (updateDone == true)
                    {
                        windowManager.RadAlert("Sales Info Successfully Updated",350, 200, "Success","alertCallBackFn", "SalesInfo");
                    }
                }
                else
                {
                    e.Canceled = true;
                    errorMsg.Visible = true;

                }

            }
            catch (Exception ex)
            {
                e.Canceled = true;
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                errorMsg.Text = ex.Message;
                errorMsg.Visible = true;
            }
        }

        protected void rgClient_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            bool doUpdate = true;

            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            ClientInfo client = new ClientInfo();

            try
            {

                //initialize error to ""
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                errorMsg.Text = "";

                //Reset Required Labels back to Black
                resetRequired(reqLabels, userControl);

                //POPULATE the objClient
                objClient = populateClientObj(userControl);

                //CHECK for errors
                doUpdate = checkItemValues(objClient, userControl);

                //CHECK for existing before doing insert
                bool existingAccount = contractAccountClassificationFlag(objClient.ContractNumber, objClient.Acctnbr);
                if (existingAccount)
                {
                    e.Canceled = true;
                    errorMsg.Text = "Account " + objClient.Acctnbr + " already exists, use Edit Function to modify";
                    errorMsg.Visible = true;
                }
                else
                {

                    string SQLerrorMsg;
                    if (doUpdate)
                    {
                        //Get ContractClassificationExists flag
                        bool existingContract = contractClassificationFlag(objClient.ContractNumber);
                        bool inserteDone = false;
                        //if ContractClassificationExists, do update else do insert
                        if (existingContract)
                        {
                            //ContractClassification gets updated, ContractAccountClassification gets inserted
                            SQLerrorMsg = client.doSQLUpdateCCInsertCAC(objClient, (string)(Session["userName"]));
                            if (SQLerrorMsg != "")
                            {
                                errorMsg.Text = SQLerrorMsg;
                                errorMsg.Visible = true;
                                e.Canceled = true;
                            }
                            else
                            {
                                pnlsuccess.Visible = true;
                                lblSuccess.Text = "New SalesInfo Successfully added to the Contract Name " + "'" + objClient.ContractName + "'";
                                inserteDone = true;
                            }

                        }
                        else
                        {
                            if (existingAccount)
                            {
                                //update both together in a transaction
                                SQLerrorMsg = client.doSQLInsertCCUpdateCAC(objClient, (string)(Session["userName"]));
                                if (SQLerrorMsg != "")
                                {
                                    errorMsg.Text = SQLerrorMsg;
                                    errorMsg.Visible = true;
                                    e.Canceled = true;
                                }
                                else
                                {
                                    pnlsuccess.Visible = true;
                                    lblSuccess.Text = "New SalesInfo Successfully added to the Contract Name " + "'" + objClient.ContractName + "'";
                                    GetClientInfo();
                                    inserteDone = true;
                                }

                            }
                            else
                            {
                                //update both together in a transaction
                                SQLerrorMsg = client.doSQLInsert(objClient, (string)(Session["userName"]));
                                if (SQLerrorMsg != "")
                                {
                                    errorMsg.Text = SQLerrorMsg;
                                    errorMsg.Visible = true;
                                    e.Canceled = true;
                                }
                                else
                                {
                                    pnlsuccess.Visible = true;
                                    lblSuccess.Text = "New SalesInfo Successfully added to the Contract Name " + "'" + objClient.ContractName + "'";
                                    GetClientInfo();
                                    inserteDone = true;
                                }
                            }
                            if (inserteDone == true)
                            {
                                windowManager.RadAlert("Sales Info Successfully Inserted!", 350, 200, "Success", "alertCallBackFn", "SalesInfo");
                            }

                        }
                    }
                    else
                    {
                        e.Canceled = true;
                        errorMsg.Text = errorMsg.Text + "<br>Please Select Required Fields";
                        errorMsg.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                e.Canceled = true;
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                errorMsg.Text = ex.Message;
                errorMsg.Visible = true;
            }

        }

        private ClientInfo populateClientObj(UserControl userControl)
        {
            try
            {
                objClient.ClientName = (userControl.FindControl("cbxClientName") as RadComboBox).Text;
                RadComboBox contractBox = userControl.FindControl("cbxContractName") as RadComboBox;
                if (contractBox.SelectedIndex >= 0)
                {
                    string selContract = contractBox.SelectedItem.Value;
                    objClient.ContractID = Convert.ToDouble(selContract);
                    objClient.ContractNumber = contractBox.Text.Split('-')[0];
                    objClient.ContractName = contractBox.Text.Split('-')[1];
                }
                objClient.Acctnbr = (userControl.FindControl("cbxAccountNumber") as RadComboBox).Text;
                objClient.StrategicSRID = (userControl.FindControl("rddlStrategicSRIDEdit") as RadDropDownList).SelectedValue;
                objClient.LocalSRID = (userControl.FindControl("rddlLocalSRIDEdit") as RadDropDownList).SelectedValue;
                objClient.BusinessType = (userControl.FindControl("rddlBusinesstypeEdit") as RadDropDownList).SelectedText;
                objClient.ProductType = (userControl.FindControl("rddlProductTypeEdit") as RadDropDownList).SelectedText;
               
                objClient.CCID = (userControl.FindControl("CCIDTextBoxN") as RadNumericTextBox).Text.Length > 0 ? Convert.ToDouble((userControl.FindControl("CCIDTextBoxN") as RadNumericTextBox).Text) : 0;
                //Expr1 is Contract Classification Region
                objClient.Expr1 = (userControl.FindControl("rddlConRegionEdit") as RadDropDownList).SelectedText;
                objClient.Territory = (userControl.FindControl("rddlTerritoryEdit") as RadDropDownList).SelectedValue;
                objClient.SalesType = (userControl.FindControl("rddlSalesTypeEdit") as RadDropDownList).SelectedText;
                //adding 4 new fields to the Contrct Classifiction table
                objClient.gatewayID = ((userControl.FindControl("rddlGatewayIDEdit")) as RadDropDownList).SelectedValue.Length > 0 ? Convert.ToInt32(((userControl.FindControl("rddlGatewayIDEdit")) as RadDropDownList).SelectedValue) : 0;
                objClient.MATHANCPPID = (userControl.FindControl("rddlMATHANCPPIDEdit") as RadDropDownList).SelectedValue;
                objClient.RTNSID = (userControl.FindControl("rddlRTNSIDEdit") as RadDropDownList).SelectedValue;
                objClient.CDCPLBID = (userControl.FindControl("rddlCDCPLBEdit") as RadDropDownList).SelectedValue;
                //ISP is null in db
                objClient.ISP = (userControl.FindControl("rddISP") as RadDropDownList).SelectedValue;
                //objClient.Currency = (userControl.FindControl("rddlCurrency") as RadComboBox).SelectedValue;
                //objClient.RelationshipName = (userControl.FindControl("RelationshipTextBox") as RadTextBox).Text;
                objClient.RelationshipName = (userControl.FindControl("rddlRelationship") as RadDropDownList).SelectedText;
                objClient.ISPpaid = (userControl.FindControl("ispCheckBox") as RadButton).Checked;
                objClient.LostBiz = (userControl.FindControl("lostbizCheckBox") as RadButton).Checked;
                objClient.Region = (userControl.FindControl("rddlAccRegionEdit") as RadDropDownList).SelectedText;
                objClient.ShippingLocation = (userControl.FindControl("rddlShippingLocationEdit") as RadDropDownList).SelectedText;
                objClient.EstAnnualRev = (userControl.FindControl("EstAnnRevTextBoxN") as RadNumericTextBox).Text.Length > 0 ? Convert.ToSingle((userControl.FindControl("EstAnnRevTextBoxN") as RadNumericTextBox).Text) : 0;
                objClient.EstMarginPct = (userControl.FindControl("EstMarPctTextBoxN") as RadNumericTextBox).Text.Length > 0 ? Convert.ToSingle((userControl.FindControl("EstMarPctTextBoxN") as RadNumericTextBox).Text) : 0;
                objClient.SecondaryNAICS = (userControl.FindControl("snaicsTextBox") as RadTextBox).Text;

                objClient.StartDate = (userControl.FindControl("StartDate") as RadDatePicker).SelectedDate;
                objClient.EndDate = (userControl.FindControl("EndDate") as RadDatePicker).SelectedDate;
                objClient.rolloff_quarter = (userControl.FindControl("RolloffQuarter") as RadDatePicker).SelectedDate;
                    

                string sharePct = (userControl.FindControl("SharePctTextBoxN") as RadNumericTextBox).Text;
                float fSharePct;
                bool isNumeric =  float.TryParse(sharePct, out fSharePct);
                if (!isNumeric)
                {
                    fSharePct=1;
                }
                else
                {
                    //Share Pct stored as 1 for 100 or .5 for 50
                    fSharePct = fSharePct / 100;
                }
                //objClient.SharePercent = (userControl.FindControl("SharePctTextBoxN") as RadNumericTextBox).Text.Length > 0 ? Convert.ToSingle((userControl.FindControl("SharePctTextBoxN") as RadNumericTextBox).Text) : 1;
                objClient.SharePercent = fSharePct;

                //Assign Prepuma Web  to Source
                objClient.Source = "Prepuma Web";
                objClient.Updatedby = Convert.ToString(Session["userName"]);
                objClient.ActiveFlag = true;
                
            }
            catch (Exception ex)
            {
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                errorMsg.Text = ex.Message;
                errorMsg.Visible = true;
            }

            return objClient;
        }

        private bool checkItemValues(ClientInfo objClient, UserControl userControl)
        {
            try
            {

                string val;
                bool doUpdate = true;
                //CLIENT NAME
                val = objClient.ClientName;
                if ((string.IsNullOrEmpty(val)))
                {
                    Label lblrn = (Label)userControl.FindControl("labelClientName");
                    lblrn.ForeColor = System.Drawing.Color.Red;
                    doUpdate = false;
                }
                //CONTRACT NUMBER
                val = objClient.ContractNumber;
                if ((string.IsNullOrEmpty(val)))
                {
                    Label lblrn = (Label)userControl.FindControl("labelContractNumber");
                    lblrn.ForeColor = System.Drawing.Color.Red;
                    doUpdate = false;
                }

                //ACCOUNT NUMBER
                val = objClient.Acctnbr;
                if ((string.IsNullOrEmpty(val)))
                {
                    Label lblrn = (Label)userControl.FindControl("labelAccountNumber");
                    lblrn.ForeColor = System.Drawing.Color.Red;
                    doUpdate = false;
                }

                //CONTRACT REGION
                val = objClient.Expr1;
                if ((string.IsNullOrEmpty(val)))
                {
                    Label lblrn = (Label)userControl.FindControl("labelContractRegion");
                    lblrn.ForeColor = System.Drawing.Color.Red;
                    doUpdate = false;
                }
                //TERRITORY
                val = objClient.Territory;
                if ((string.IsNullOrEmpty(val)))
                {
                    Label lblrn = (Label)userControl.FindControl("labelTerritory");
                    lblrn.ForeColor = System.Drawing.Color.Red;
                    doUpdate = false;
                }
                //SALES TYPE
                val = objClient.SalesType;
                if ((string.IsNullOrEmpty(val)))
                {
                    Label lblrn = (Label)userControl.FindControl("labelSalesType");
                    lblrn.ForeColor = System.Drawing.Color.Red;
                    doUpdate = false;
                }
                //BUSINESS TYPE
                val = objClient.BusinessType;
                if ((string.IsNullOrEmpty(val)))
                {
                    Label lblrn = (Label)userControl.FindControl("labelBusinessType");
                    lblrn.ForeColor = System.Drawing.Color.Red;
                    doUpdate = false;
                }
                //ACCOUNT REGION
                val = objClient.Region;
                if ((string.IsNullOrEmpty(val)))
                {
                    Label lblrn = (Label)userControl.FindControl("labelAcctRegion");
                    lblrn.ForeColor = System.Drawing.Color.Red;
                    doUpdate = false;
                }
                //RELATIONSHIP NAME
                val = objClient.RelationshipName;
                if ((string.IsNullOrEmpty(val)))
                {
                    Label lblrn = (Label)userControl.FindControl("lblRelationshipName");
                    lblrn.ForeColor = System.Drawing.Color.Red;
                    doUpdate = false;
                }
                if (val != null && val.Length > 50)
                {

                    Label lblrn = (Label)userControl.FindControl("lblRelationshipName");
                    lblrn.ForeColor = System.Drawing.Color.Red;
                    Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                    errorMsg.Text = errorMsg.Text + " Relationship Name cannot be more than 50 characters";
                    doUpdate = false;
                }
                //PRODUCT TYPE
                val = objClient.Region;
             
                //SECONDARY NAICS
                val = objClient.SecondaryNAICS;
                if (val != null && val.Length > 15)
                {
                    Label lblrn = (Label)userControl.FindControl("lblSnaics");
                    lblrn.ForeColor = System.Drawing.Color.Red;
                    Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                    errorMsg.Text = errorMsg.Text + "<br> Secondary Naics cannot be more than 15 characters";
                    doUpdate = false;
                }
                //ISP
                val = objClient.ISP;
                if (val != null && val.Length > 10)
                {
                    Label lblrn = (Label)userControl.FindControl("lblISP");
                    lblrn.ForeColor = System.Drawing.Color.Red;
                    Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                    errorMsg.Text = errorMsg.Text + "<br> ISP cannot be more than 10 characters";
                    doUpdate = false;
                }
             

                return doUpdate;

            }
            catch (Exception ex)
            {
                Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                errorMsg.Text = ex.Message;
                errorMsg.Visible = true;
                return false;
            }

        }

        private void resetRequired(string[] reqLabels, UserControl userControl)
        {
            for (int i = 0; i < reqLabels.Length; i++)
            {
                Label lblobj = (Label)userControl.FindControl((string)reqLabels[i]);
                lblobj.ForeColor = System.Drawing.Color.Black;
            }

        }

       

                


        private bool contractClassificationFlag(string contractNumber)
        {
            bool existingContract = false;
            try
            {

                ContractClassification cc = PrepumaWebApp.App_Data.DAL.ContractClassification.GetData(contractNumber);
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

        

        private bool contractAccountClassificationFlag(string contractNumber, string accountNumber)
        {
            bool existingAccount = false;
           
            try
            {

                AccountClassification cac = PrepumaWebApp.App_Data.DAL.AccountClassification.GetData(accountNumber);
                if (cac != null)
                {
                    existingAccount = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return existingAccount;
        }

        private void fillInContractValues(UserControl userControl, string ContractNumber)
        {
            //fill the hiiden fields and text boxes just as if it were bound, so user doesn't have re-select when they already exist
            try
            {

                ContractClassification cc = PrepumaWebApp.App_Data.DAL.ContractClassification.GetData(ContractNumber);
                if (cc != null)
                {
                    HiddenField hdRegion = userControl.FindControl("hdRegion") as HiddenField;
                    hdRegion.Value = cc.Region;
                    HiddenField hdTerritory = userControl.FindControl("hdTerritory") as HiddenField;
                    hdTerritory.Value = cc.Territory;
                    HiddenField hdSalesType = userControl.FindControl("hdSalesType") as HiddenField;
                    hdSalesType.Value = cc.SalesType;
                    //RadTextBox RelationshipTextBox = userControl.FindControl("RelationshipTextBox") as RadTextBox;
                    //RelationshipTextBox.Text = cc.RelationshipName;
                    HiddenField hdRelationship = userControl.FindControl("hdRelationship") as HiddenField;
                    hdRelationship.Value = cc.RelationshipName;
                    //MK added for change to drop down
                    RadDropDownList rddlRelationship = userControl.FindControl("rddlRelationship") as RadDropDownList;
                    rddlRelationship.SelectedValue = hdRelationship.Value;
                    HiddenField hdISP = userControl.FindControl("hdISP") as HiddenField;
                    hdISP.Value = cc.ISP;
                    RadDropDownList isp = userControl.FindControl("rddISP") as RadDropDownList;

                    //HiddenField hdCurrency = userControl.FindControl("hdCurrency") as HiddenField;
                    //hdCurrency.Value = cc.Currency;
                    
                    RadTextBox snaics = userControl.FindControl("snaicsTextBox") as RadTextBox;
                    snaics.Text = cc.SecondaryNAICS;
                    RadNumericTextBox ccid = userControl.FindControl("CCIDTextBoxN") as RadNumericTextBox;
                    ccid.Text = cc.CCID.ToString();
                    RadNumericTextBox rev = userControl.FindControl("EstAnnRevTextBoxN") as RadNumericTextBox;
                    rev.Text = cc.EstAnnualRev.ToString();
                    RadNumericTextBox pct = userControl.FindControl("EstMarPctTextBoxN") as RadNumericTextBox;
                    pct.Text = cc.EstMarginPct.ToString();
                    RadTextBox source = userControl.FindControl("SourceTextBox") as RadTextBox;
                    source.Text = cc.Source;
                    RadButton lostbiz = userControl.FindControl("lostbizCheckBox") as RadButton;
                    lostbiz.Checked = cc.LostBiz;
                    RadButton isppaid = userControl.FindControl("ispCheckBox") as RadButton;
                    isppaid.Checked = cc.ISPpaid;
                }

            }
            catch (Exception ex)
            {

            }
           
        }
        //END MK ADDED
              
        
        
    }
}