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
    public partial class UnassignedAccounts : System.Web.UI.Page
    {

        ClientInfoContext ctxt = new ClientInfoContext();
        public List<ClientInfo> clientData = null;
        ClientInfo objClient = new ClientInfo();
        public List<ClsRegion> regionlist;
        string[] reqLabels = new string[] { "labelContractRegion", "labelTerritory", "lblRelationshipName", "labelAcctRegion" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userName"] == null || Session["appName"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            if (Session["userRole"].ToString().ToLower() == "audit")
            {
                Response.Redirect("Default.aspx");
            } 
            if (!IsPostBack)
            {
                populateTypeDropDown();
            }
        }

        private void populateTypeDropDown()
        {
            ClsExceptionReport exrep = new ClsExceptionReport();
            List<ClsExceptionReport> exreplist = exrep.GetData();
            RadDropDownType.DataSource = exreplist;
            RadDropDownType.SelectedIndex = 0;
            RadDropDownType.DataBind();
            
        }
        //GetClientInfo method binds rgClients grid on page initial load
        private void GetClientInfo()
        {
            
            string sSelectedType = RadDropDownType.SelectedValue;
            Int32 SelectedType;
            bool isNumeric = Int32.TryParse(sSelectedType, out SelectedType);
            if (!isNumeric)
            {
                SelectedType = 1;
            }

            String stpName = "";
            String RptName = "";

            ClsExceptionReport expRpt = ClsExceptionReport.GetReport(SelectedType);

            stpName = expRpt.STP_Name;
            RptName = expRpt.ExceptionName;

            lblType.Text = RptName;


            try
            {

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["prepumaSQLConnectionString"].ConnectionString))
                using (var cmd = new SqlCommand(stpName, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(table);
                }
                //edit doesn't work when the grid is populated by table
                List<ClientInfo> listName = table.AsEnumerable().Select(m => new ClientInfo()
                {
                    ClientID = m.Field<Double?>("ClientID"),
                    ClientName = m.Field<string>("ClientName"),
                    ContractID = m.Field<Double?>("ContractID"),
                    ContractName = m.Field<string>("ContractName"),
                    ContractNumber = m.Field<string>("ContractNumber"),
                    AcctID = m.Field<Double?>("AcctID"),
                    Acctnbr = m.Field<string>("Acctnbr"),
                    StrategicSRID = m.Field<string>("StrategicSRID"),
                    LocalSRID = m.Field<string>("LocalSRID"),
                    BusinessType = m.Field<string>("BusinessType"),
                    ProductType = m.Field<string>("ProductType"),
                    Territory = m.Field<string>("Territory"),
                    SalesType = m.Field<string>("SalesType"),
                    CCID = m.Field<Double?>("CCID"),
                    Region = m.Field<string>("Region"),
                    ISP = m.Field<string>("ISP"),
                    Expr1 = m.Field<string>("Expr1"),
                    ShippingLocation = m.Field<string>("ShippingLocation"),
                    RelationshipName = m.Field<string>("RelationshipName"),
                    ISPpaid = m.Field<bool?>("ISPpaid"),
                    LostBiz = m.Field<bool?>("LostBiz"),
                    EstAnnualRev = m.Field<float?>("EstAnnualRev"),
                    EstMarginPct = m.Field<float?>("EstMarginPct"),
                    SecondaryNAICS = m.Field<string>("SecondaryNAICS"),
                    Source = m.Field<string>("Source"),
                    SharePercent = m.Field<float?>("SharePercent"),
                    Updatedby = m.Field<string>("Updatedby"),
                    StartDate = m.Field<DateTime?>("StartDate"),
                    EndDate = m.Field<DateTime?>("EndDate"),
                    rolloff_quarter = m.Field<DateTime?>("rolloff_quarter")
                }).ToList();

                rgAccount.DataSource = listName;
                   
            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }
        }



        protected void rgAccount_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            GetClientInfo();
        }

        protected void RadDropDownType_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {

            GetClientInfo();
            rgAccount.Rebind();

        }

        protected void rgAccount_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {

                rgAccount.MasterTableView.EditFormSettings.CaptionFormatString = "Update Sales Information";
            }
            else
            {
                rgAccount.MasterTableView.EditFormSettings.CaptionFormatString = "Insert Sales Information";
            }

            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                rgAccount.ExportSettings.FileName = "UnAssignedAccounts";
                //rgAccount.MasterTableView.GetColumn("Edit").Visible = false;
                rgAccount.ExportSettings.IgnorePaging = true;
                rgAccount.ExportSettings.ExportOnlyData = true;
                rgAccount.ExportSettings.OpenInNewWindow = true;
                rgAccount.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            }

            if (e.CommandName == RadGrid.EditCommandName)
            {

                
            }

        }  


        private void getTotalUnasignedAccounts()
        {
            int totalUnassignedCon = ClsAccount.numberUnAssignedAccounts();

        }



        protected void rgAccount_ItemDataBound(object sender, GridItemEventArgs e)
        {

           
            try
            {
                var msg = "";
                //Need to select drop downs after data binding


                if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    //************First calling dropdown list values selected in pop up edit form**************/ 
                    UserControl userControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                    GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;

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
                        clientCBX.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    //CONTRACT NAME
                    try
                    {
                        RadComboBox contractCBX = userControl.FindControl("cbxContractName") as RadComboBox;
                        string hiddenContract = (userControl.FindControl("hdContract") as HiddenField).Value;
                        ClsContract contract = new ClsContract();
                        List<ClsContract> contractlist = contract.GetContractInfo();
                        contractCBX.DataSource = contractlist;
                        contractCBX.DataBind();
                        contractCBX.SelectedValue = hiddenContract;
                        contractCBX.Enabled = false;

                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }

                    //CONTRACT NUMBER
                    try
                    {
                        RadComboBox contractCBX = userControl.FindControl("cbxContractNumber") as RadComboBox;
                        string hiddenContract = (userControl.FindControl("hdContractNumber") as HiddenField).Value;
                        ClsContract contract = new ClsContract();
                        List<ClsContract> contractlist = contract.GetContractInfo();
                        contractCBX.DataSource = contractlist;
                        contractCBX.DataBind();
                        contractCBX.SelectedValue = hiddenContract;
                        contractCBX.Enabled = false;

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

                    //ACCOUNT
                    try
                    {
                        RadComboBox acctCBX = userControl.FindControl("cbxAccountNumber") as RadComboBox;
                        string hiddenAcct = (userControl.FindControl("hdAccount") as HiddenField).Value;
                        ClsAccount account = new ClsAccount();
                        List<ClsAccount> accountlist = account.GetAccountInfo();
                        acctCBX.DataSource = accountlist;
                        acctCBX.DataBind();
                        acctCBX.SelectedValue = hiddenAcct;
                        acctCBX.Enabled = false;

                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
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
                        territoryDDL.SelectedText = hiddenTerritory;
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

                        RadDropDownList lsridDDL = userControl.FindControl("rddlLocalSRIDEdit") as RadDropDownList;
                        lsridDDL.DataSource = sridlist;
                        lsridDDL.DataBind();
                        lsridDDL.Items.Add("N/A"); //added by Jyothi
                        lsridDDL.SelectedText = hiddenlsrid;

                        RadDropDownList ssridDDL = userControl.FindControl("rddlStrategicSRIDEdit") as RadDropDownList;
                        ssridDDL.DataSource = sridlist;
                        ssridDDL.DataBind();
                        ssridDDL.Items.Add("N/A");//added by Jyothi
                        ssridDDL.SelectedText = hiddenssrid;

                        RadDropDownList ispDDL = userControl.FindControl("rddISP") as RadDropDownList;
                        ispDDL.DataSource = sridlist;
                        ispDDL.DataBind();
                        ispDDL.Items.Add("N/A");//added by Jyothi
                        ispDDL.SelectedText = hiddenssrid;
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

                    //SHARE PERCENT
                    try
                    {
                        String sharePct = (userControl.FindControl("SharePctTextBoxN") as RadNumericTextBox).Text;
                        //if (sharePct == "")
                        //{
                        //    (userControl.FindControl("SharePctTextBoxN") as RadNumericTextBox).Text = "100";
                        //}
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
                        (userControl.FindControl("SharePctTextBoxN") as RadNumericTextBox).Text = fSharePct.ToString();
                       
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
                }
            }
            catch (Exception ex)
            {

                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }
        }

        protected void rgAccount_UpdateCommand(object source, GridCommandEventArgs e)
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
                            SQLerrorMsg = client.doSQLInsert(objClient,(string)(Session["userName"]));
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
                        windowManager.RadAlert("Sales Info Successfully Updated", 350, 200, "Success", "alertCallBackFn", "SalesInfo");
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

        private ClientInfo populateClientObj(UserControl userControl)
        {
            try
            {
                //string tempvar;
                //bool isNumeric;
                //Int32 ivar;
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
                objClient.StrategicSRID = (userControl.FindControl("rddlStrategicSRIDEdit") as RadDropDownList).SelectedText;
                objClient.LocalSRID = (userControl.FindControl("rddlLocalSRIDEdit") as RadDropDownList).SelectedText;
                objClient.BusinessType = (userControl.FindControl("rddlBusinesstypeEdit") as RadDropDownList).SelectedText;
                objClient.ProductType = (userControl.FindControl("rddlProductTypeEdit") as RadDropDownList).SelectedText;

                objClient.CCID = (userControl.FindControl("CCIDTextBoxN") as RadNumericTextBox).Text.Length > 0 ? Convert.ToDouble((userControl.FindControl("CCIDTextBoxN") as RadNumericTextBox).Text) : 0;
                //Expr1 is Contract Classification Region
                objClient.Expr1 = (userControl.FindControl("rddlConRegionEdit") as RadDropDownList).SelectedText;
                objClient.Territory = (userControl.FindControl("rddlTerritoryEdit") as RadDropDownList).SelectedText;
                objClient.SalesType = (userControl.FindControl("rddlSalesTypeEdit") as RadDropDownList).SelectedValue;
                //adding 4 new fields to the Contrct Classifiction table
                objClient.gatewayID = ((userControl.FindControl("rddlGatewayIDEdit")) as RadDropDownList).SelectedValue.Length > 0 ? Convert.ToInt32(((userControl.FindControl("rddlGatewayIDEdit")) as RadDropDownList).SelectedValue) : 0;
                objClient.MATHANCPPID = (userControl.FindControl("rddlMATHANCPPIDEdit") as RadDropDownList).SelectedValue;
                objClient.RTNSID = (userControl.FindControl("rddlRTNSIDEdit") as RadDropDownList).SelectedValue;
                objClient.CDCPLBID = (userControl.FindControl("rddlCDCPLBEdit") as RadDropDownList).SelectedValue;
                //ISP is null in db
                //objClient.ISP = (userControl.FindControl("ISPTextBox") as RadTextBox).Text;
                objClient.ISP = (userControl.FindControl("rddISP") as RadDropDownList).SelectedText;
                //objClient.RelationshipName = (userControl.FindControl("RelationshipTextBox") as RadTextBox).Text;
                objClient.RelationshipName = (userControl.FindControl("rddlRelationship") as RadDropDownList).SelectedText;
                objClient.ISPpaid = (userControl.FindControl("ispCheckBox") as RadButton).Checked;
                objClient.LostBiz = (userControl.FindControl("lostbizCheckBox") as RadButton).Checked;
                objClient.Region = (userControl.FindControl("rddlAccRegionEdit") as RadDropDownList).SelectedText;
                objClient.ShippingLocation = (userControl.FindControl("rddlShippingLocationEdit") as RadDropDownList).SelectedText;
                objClient.EstAnnualRev = (userControl.FindControl("EstAnnRevTextBoxN") as RadNumericTextBox).Text.Length > 0 ? Convert.ToSingle((userControl.FindControl("EstAnnRevTextBoxN") as RadNumericTextBox).Text) : 0;
                objClient.EstMarginPct = (userControl.FindControl("EstMarPctTextBoxN") as RadNumericTextBox).Text.Length > 0 ? Convert.ToSingle((userControl.FindControl("EstMarPctTextBoxN") as RadNumericTextBox).Text) : 0;
                objClient.SecondaryNAICS = (userControl.FindControl("snaicsTextBox") as RadTextBox).Text;
                string sharePct = (userControl.FindControl("SharePctTextBoxN") as RadNumericTextBox).Text;
                float fSharePct;
                bool isNumeric = float.TryParse(sharePct, out fSharePct);
                             
                if (!isNumeric)
                {
                    fSharePct = 1;
                }
                else
                {
                    //Share Pct stored as 1 for 100 or .5 for 50
                    fSharePct = fSharePct / 100;
                }
                //objClient.SharePercent = (userControl.FindControl("SharePctTextBoxN") as RadNumericTextBox).Text.Length > 0 ? Convert.ToSingle((userControl.FindControl("SharePctTextBoxN") as RadNumericTextBox).Text) : 1;
                objClient.SharePercent = fSharePct;
                objClient.Source = "PrePuma Web";
                objClient.Updatedby = Convert.ToString(Session["userName"]);
                objClient.ActiveFlag = true;

                objClient.StartDate = (userControl.FindControl("StartDate") as RadDatePicker).SelectedDate;
                objClient.EndDate = (userControl.FindControl("EndDate") as RadDatePicker).SelectedDate;
                objClient.rolloff_quarter = (userControl.FindControl("RolloffQuarter") as RadDatePicker).SelectedDate;



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
                if (val != null && val.Length > 50)
                {
                    Label lblrn = (Label)userControl.FindControl("lblRelationshipName");
                    lblrn.ForeColor = System.Drawing.Color.Red;
                    Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
                    errorMsg.Text = errorMsg.Text + " Relationship Name cannot be more than 50 characters";
                    doUpdate = false;
                }
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

        private string GetCCFlag(string region)
        {
            if (region != "")
            {
                return "true";
            }
                else
            {
                return "false";
            } 

        }

    }
}