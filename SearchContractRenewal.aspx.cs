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
using System.Net.Mail;
using System.Net;

namespace PrepumaWebApp
{
    public partial class SearchContractRenewal : System.Web.UI.Page
    {
        ClsContractRenewal objContractRenewal = new ClsContractRenewal();
        //ClsContractRenewalDetail objContractRenewalDetail = nweClsContractRenewalDetail();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                   
                    if (Session["userName"] != null && Session["appName"] != null)
                    {                       
                            GetContractInfo();
                            //SET PERMISSIONS

                            //Contracts User Role can Delete Contracts (flag as inactive)
                            if (Session["userRole"].ToString().ToLower() != "contracts")
                            {
                                rgContracts.MasterTableView.GetColumn("DeleteLink").Visible = false;
                                rgContracts.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
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

        protected void rgContracts_PreRender(object sender, EventArgs e)
        {
            
        }

       

        private void GetContractInfo()
        {
            try
            {

                 string filterName = Session["userName"].ToString();
                 string filter =  Request.QueryString["filter"];
                       

                if (filter == "true")
                {
                    rgContracts.DataSource = ClsContractRenewal.GetContractRenewalList(filterName);
                    lblFilterOn.Text = "Client Logs Filtered for " + filterName;
                    lblFilterOn.ForeColor = System.Drawing.Color.DarkBlue;
                }
                else
                {
                    rgContracts.DataSource = ClsContractRenewal.GetContractRenewalList();
                    lblFilterOn.Text = "Viewing All Client Logs";
                    lblFilterOn.ForeColor = System.Drawing.Color.DarkBlue;
                }
               
            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }
        }

        protected void rgContracts_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GetContractInfo();
        }

        protected void rgContracts_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                rgContracts.ExportSettings.FileName = "ContractRenewalInfo";
               
                rgContracts.ExportSettings.IgnorePaging = true;
                rgContracts.ExportSettings.ExportOnlyData = true;
                rgContracts.ExportSettings.OpenInNewWindow = true;
                rgContracts.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;


            }

            if (e.CommandName == "Edit")
            {
                rgContracts.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Contract Information";
                rgContracts.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }
            else
            {
                rgContracts.MasterTableView.EditFormSettings.CaptionFormatString = "Add Contract Information";
                rgContracts.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
            }


        }
        //If the page size changed rgClients_PageSizeChanged will be fired and binds the data according to the page size.
        protected void rgContracts_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            rgContracts.CurrentPageIndex = e.NewPageSize;
            rgContracts.CurrentPageIndex = 0;
            GetContractInfo();
        }

 
        protected void rgContracts_ItemDataBound(object sender, GridItemEventArgs e)
        {
           

            if (e.Item is GridDataItem)
            {


                GridDataItem item = (GridDataItem)e.Item;
                HyperLink hLink = (HyperLink)item["Relationship"].Controls[0];
                hLink.ForeColor = System.Drawing.Color.Blue;
                ClsContractRenewal row = (ClsContractRenewal)item.DataItem;
                hLink.Attributes["onclick"] = "OpenWin('" + row.idContractRenewal + "');";
            }
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {

                try
                {
                    
                    UserControl userControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;

                    //DEFINE ALL THE FORM ELEMENTS
                    string hiddenRname = (userControl.FindControl("hdRelationship") as HiddenField).Value;
                    RadComboBox rnameCBX = userControl.FindControl("cbxRelationshipName") as RadComboBox;
                    RadComboBox typeCBX = userControl.FindControl("cbxRenewalTypes") as RadComboBox;
                    Label lblContractDetails = userControl.FindControl("lblContractDetails") as Label;
                    RadDatePicker rdpEffectiveDate = userControl.FindControl("rdpEffectiveDate") as RadDatePicker;
                    RadDatePicker rdpExpiryDate = userControl.FindControl("rdpExpiryDate") as RadDatePicker;
                    TextBox txtModReason = userControl.FindControl("txtModReason") as TextBox;
                    RadNumericTextBox txtpctCourierN = userControl.FindControl("txtpctCourierN") as RadNumericTextBox;
                    RadNumericTextBox txtpctFreightN = userControl.FindControl("txtpctFFN") as RadNumericTextBox;
                    RadNumericTextBox txtpctLTLN = userControl.FindControl("txtpctLTLN") as RadNumericTextBox;
                    RadNumericTextBox txtpctPPSTN = userControl.FindControl("txtpctPPSTN") as RadNumericTextBox;                    
                    RadNumericTextBox txtpctCPCN = userControl.FindControl("txtpctCPCN") as RadNumericTextBox;                    
                    RadNumericTextBox txtpctOtherN = userControl.FindControl("txtpctOtherN") as RadNumericTextBox; 
                    RadNumericTextBox txtcurMarginN = userControl.FindControl("txtcurMarginN") as RadNumericTextBox;                    
                    RadNumericTextBox txtsowMarginN = userControl.FindControl("txtsowMarginN") as RadNumericTextBox;                    
                    RadNumericTextBox txtrocMarginN = userControl.FindControl("txtrocMarginN") as RadNumericTextBox;                    
                    RadNumericTextBox txtexpMarginN = userControl.FindControl("txtexpMarginN") as RadNumericTextBox;                    
                    RadNumericTextBox txtnewMarginN = userControl.FindControl("txtnewMarginN") as RadNumericTextBox;
                    RadNumericTextBox txtcurCourierN = userControl.FindControl("txtcurCourierN") as RadNumericTextBox;                    
                    RadNumericTextBox txtsowCourierN = userControl.FindControl("txtsowCourierN") as RadNumericTextBox;                   
                    RadNumericTextBox txtrocCourierN = userControl.FindControl("txtrocCourierN") as RadNumericTextBox;                    
                    RadNumericTextBox txtexpCourierN = userControl.FindControl("txtexpCourierN") as RadNumericTextBox;   
                    RadNumericTextBox txtcurFFN = userControl.FindControl("txtcurFFN") as RadNumericTextBox;                   
                    RadNumericTextBox txtsowFFN = userControl.FindControl("txtsowFFN") as RadNumericTextBox;                    
                    RadNumericTextBox txtrocFFN = userControl.FindControl("txtrocFFN") as RadNumericTextBox;                    
                    RadNumericTextBox txtexpFFN = userControl.FindControl("txtexpFFN") as RadNumericTextBox;  
                    RadNumericTextBox txtcurLTLN = userControl.FindControl("txtcurLTLN") as RadNumericTextBox;                   
                    RadNumericTextBox txtsowLTLN = userControl.FindControl("txtsowLTLN") as RadNumericTextBox;                    
                    RadNumericTextBox txtrocLTLN = userControl.FindControl("txtrocLTLN") as RadNumericTextBox;                    
                    RadNumericTextBox txtexpLTLN = userControl.FindControl("txtexpLTLN") as RadNumericTextBox; 
                    RadNumericTextBox txtcurPPSTN = userControl.FindControl("txtcurPPSTN") as RadNumericTextBox;                    
                    RadNumericTextBox txtsowPPSTN = userControl.FindControl("txtsowPPSTN") as RadNumericTextBox;                   
                    RadNumericTextBox txtrocPPSTN = userControl.FindControl("txtrocPPSTN") as RadNumericTextBox;                    
                    RadNumericTextBox txtexpPPSTN = userControl.FindControl("txtexpPPSTN") as RadNumericTextBox;
                    RadNumericTextBox txtcurCPCN = userControl.FindControl("txtcurCPCN") as RadNumericTextBox;                   
                    RadNumericTextBox txtsowCPCN = userControl.FindControl("txtsowCPCN") as RadNumericTextBox;                    
                    RadNumericTextBox txtrocCPCN = userControl.FindControl("txtrocCPCN") as RadNumericTextBox;                    
                    RadNumericTextBox txtexpCPCN = userControl.FindControl("txtexpCPCN") as RadNumericTextBox;
                    RadNumericTextBox txtcurOtherN = userControl.FindControl("txtcurOtherN") as RadNumericTextBox;                    
                    RadNumericTextBox txtsowOtherN = userControl.FindControl("txtsowOtherN") as RadNumericTextBox;                   
                    RadNumericTextBox txtrocOtherN = userControl.FindControl("txtrocOtherN") as RadNumericTextBox;                    
                    RadNumericTextBox txtexpOtherN = userControl.FindControl("txtexpOtherN") as RadNumericTextBox;                    
                    RadNumericTextBox txtcurTargetGrMarginPctN = userControl.FindControl("txtcurTargetGrMarginPctN") as RadNumericTextBox;                    
                    RadNumericTextBox txtsowTargetGrMarginPctN = userControl.FindControl("txtsowTargetGrMarginPctN") as RadNumericTextBox;                    
                    RadNumericTextBox txtrocTargetGrMarginPctN = userControl.FindControl("txtrocTargetGrMarginPctN") as RadNumericTextBox;                    
                    RadNumericTextBox txtexpTargetGrMarginPctN = userControl.FindControl("txtexpTargetGrMarginPctN") as RadNumericTextBox;                    
                    RadNumericTextBox txtnewTargetGrMarginPctN = userControl.FindControl("txtnewTargetGrMarginPctN") as RadNumericTextBox;     
                    RadNumericTextBox txtVDARevenue = userControl.FindControl("txtVDACommitment") as RadNumericTextBox;
                    RadTextBox txtOtherDesc = userControl.FindControl("txtOtherDesc") as RadTextBox;
                    RadButton sowFlag = userControl.FindControl("sowFlag") as RadButton;
                    RadButton rocFlag = userControl.FindControl("rocFlag") as RadButton;
                    Button addOther = userControl.FindControl("AddOther") as Button;
                    RadButton getRev = userControl.FindControl("btnGetRevenue") as RadButton;
                    RadNumericTextBox txtcurDimsAir = userControl.FindControl("txtcurDimsAir") as RadNumericTextBox;
                    RadNumericTextBox txtexpDimsAir = userControl.FindControl("txtexpDimsAir") as RadNumericTextBox;
                    RadNumericTextBox txtcurDimsGround = userControl.FindControl("txtcurDimsGround") as RadNumericTextBox;
                    RadNumericTextBox txtexpDimsGround = userControl.FindControl("txtexpDimsGround") as RadNumericTextBox;
                    RadNumericTextBox txtcurResiFees = userControl.FindControl("txtcurResiFees") as RadNumericTextBox;
                    RadNumericTextBox txtexpResiFees = userControl.FindControl("txtexpResiFees") as RadNumericTextBox;                    
                    RadTextBox txtcurFuelPI = userControl.FindControl("txtcurFuelPIS") as RadTextBox;                    
                    RadTextBox txtexpFuelPI = userControl.FindControl("txtexpFuelPIS") as RadTextBox;                    
                    RadTextBox txtcurFuelPuro = userControl.FindControl("txtcurFuelPuroS") as RadTextBox;                    
                    RadTextBox txtexpFuelPuro = userControl.FindControl("txtexpFuelPuroS") as RadTextBox;                 
                    RadNumericTextBox txtcurDGFR = userControl.FindControl("txtcurdgFR") as RadNumericTextBox;
                    RadNumericTextBox txtexpDGFR = userControl.FindControl("txtexpdgFR") as RadNumericTextBox;
                    RadNumericTextBox txtcurDGUN3373 = userControl.FindControl("txtcurdgUN3373") as RadNumericTextBox;
                    RadNumericTextBox txtexpDGUN3373 = userControl.FindControl("txtexpdgUN3373") as RadNumericTextBox;
                    RadNumericTextBox txtcurDGUN1845 = userControl.FindControl("txtcurdgUN1845") as RadNumericTextBox;
                    RadNumericTextBox txtexpDGUN1845 = userControl.FindControl("txtexpdgUN1845") as RadNumericTextBox;
                    RadNumericTextBox txtcurDGLT500kg = userControl.FindControl("txtcurdgLT500kg") as RadNumericTextBox;
                    RadNumericTextBox txtexpDGLT500kg = userControl.FindControl("txtexpdgLT500kg") as RadNumericTextBox;
                    RadNumericTextBox txtcurDGLQ = userControl.FindControl("txtcurdgLQ") as RadNumericTextBox;
                    RadNumericTextBox txtexpDGLQ = userControl.FindControl("txtexpdgLQ") as RadNumericTextBox;
                    RadNumericTextBox txtcurSHFP = userControl.FindControl("txtcurshFP") as RadNumericTextBox;
                    RadNumericTextBox txtexpSHFP = userControl.FindControl("txtexpshFP") as RadNumericTextBox;
                    RadNumericTextBox txtcurSHAH = userControl.FindControl("txtcurshAH") as RadNumericTextBox;
                    RadNumericTextBox txtexpSHAH = userControl.FindControl("txtexpshAH") as RadNumericTextBox;
                    RadNumericTextBox txtcurSHLP = userControl.FindControl("txtcurshLP") as RadNumericTextBox;
                    RadNumericTextBox txtexpSHLP = userControl.FindControl("txtexpshLP") as RadNumericTextBox;
                    RadNumericTextBox txtcurSHOML = userControl.FindControl("txtcurshOML") as RadNumericTextBox;
                    RadNumericTextBox txtexpSHOML = userControl.FindControl("txtexpshOML") as RadNumericTextBox;
                    RadNumericTextBox txtcurSHO = userControl.FindControl("txtcurshO") as RadNumericTextBox;
                    RadNumericTextBox txtexpSHO = userControl.FindControl("txtexpshO") as RadNumericTextBox;
                    RadNumericTextBox txtcurSHRAH = userControl.FindControl("txtcurshRAH") as RadNumericTextBox;
                    RadNumericTextBox txtexpSHRAH = userControl.FindControl("txtexpshRAH") as RadNumericTextBox;     
                    TextBox txtAccComment = userControl.FindControl("txtAccessorialComment") as TextBox; 
                    RadComboBox contractCBX = userControl.FindControl("cbxContracts") as RadComboBox;
                    RadioButtonList radioCurrFuel = userControl.FindControl("radioCurrFuel") as RadioButtonList;
                    RadioButtonList radioExpFuel = userControl.FindControl("radioExpFuel") as RadioButtonList;
                    RadioButtonList radioBeyondOrigin = userControl.FindControl("radioBeyondOrigin") as RadioButtonList;
                    RadioButtonList radioBeyondDest = userControl.FindControl("radioBeyondDest") as RadioButtonList;
                    RadComboBox cbxperc1 = userControl.FindControl("cbxperc1") as RadComboBox;
                    RadComboBox cbxperc2 = userControl.FindControl("cbxperc2") as RadComboBox;
                    RadComboBox cbxperc3 = userControl.FindControl("cbxperc3") as RadComboBox;
                    RadComboBox cbxperc4 = userControl.FindControl("cbxperc4") as RadComboBox;
                    RadComboBox cbxperc5 = userControl.FindControl("cbxperc5") as RadComboBox;
                    RadComboBox cbxpere1 = userControl.FindControl("cbxpere1") as RadComboBox;
                    RadComboBox cbxpere2 = userControl.FindControl("cbxpere2") as RadComboBox;
                    RadComboBox cbxpere3 = userControl.FindControl("cbxpere3") as RadComboBox;
                    RadComboBox cbxpere4 = userControl.FindControl("cbxpere4") as RadComboBox;
                    RadComboBox cbxpere5 = userControl.FindControl("cbxpere5") as RadComboBox;
                    //END DEFINITIONS

                    //SET PERMISSIONS
                    //User Role Pricing can edit Numbers, all others can view only
                    if (Session["userRole"].ToString().ToLower() != "pricing")
                    {
                        getRev.Visible = false;
                        Label revlbl1 = userControl.FindControl("lblGetRevenue") as Label;
                        Label revlbl2 = userControl.FindControl("lbldaterange") as Label;
                        revlbl1.Visible = false;
                        revlbl2.Visible = false;
                        addOther.Visible = false;
                        RadButton getAcc = userControl.FindControl("btnGetAccessorials") as RadButton;
                        getAcc.Visible = false;
                        Label acclbl1 = userControl.FindControl("lblAccessorialdt") as Label;
                        Label acclbl2 = userControl.FindControl("lblAccessorialDates") as Label;
                        acclbl1.Visible = false;
                        acclbl2.Visible = false;
                        sowFlag.Enabled = false;
                        rocFlag.Enabled = false;
                        txtpctCourierN.Enabled = false;
                        txtpctFreightN.Enabled = false;
                        txtpctLTLN.Enabled = false;
                        txtpctPPSTN.Enabled = false;
                        txtpctCPCN.Enabled = false;
                        txtpctOtherN.Enabled = false;

                        txtcurMarginN.Enabled = false;
                        txtsowMarginN.Enabled = false;
                        txtrocMarginN.Enabled = false;
                        txtexpMarginN.Enabled = false;
                        txtnewMarginN.Enabled = false;

                        txtcurCourierN.Enabled = false;
                        txtsowCourierN.Enabled = false;
                        txtrocCourierN.Enabled = false;
                        txtexpCourierN.Enabled = false;

                        txtcurFFN.Enabled = false;
                        txtsowFFN.Enabled = false;
                        txtrocFFN.Enabled = false;
                        txtexpFFN.Enabled = false;

                        txtcurLTLN.Enabled = false;
                        txtsowLTLN.Enabled = false;
                        txtrocLTLN.Enabled = false;
                        txtexpLTLN.Enabled = false;

                        txtcurPPSTN.Enabled = false;
                        txtsowPPSTN.Enabled = false;
                        txtrocPPSTN.Enabled = false;
                        txtexpPPSTN.Enabled = false;

                        txtcurCPCN.Enabled = false;
                        txtsowCPCN.Enabled = false;
                        txtrocCPCN.Enabled = false;
                        txtexpCPCN.Enabled = false;

                        txtcurOtherN.Enabled = false;                                
                        txtsowOtherN.Enabled = false;
                        txtrocOtherN.Enabled = false;
                        txtexpOtherN.Enabled = false;

                        txtcurTargetGrMarginPctN.Enabled = false;
                        txtsowTargetGrMarginPctN.Enabled = false;
                        txtrocTargetGrMarginPctN.Enabled = false;
                        txtexpTargetGrMarginPctN.Enabled = false;
                        txtnewTargetGrMarginPctN.Enabled = false;

                        txtVDARevenue.Enabled = false;
                        txtOtherDesc.Enabled = false;                               

                        //ACCESORIALS
                        txtcurDimsAir.Enabled = false;
                        txtexpDimsAir.Enabled = false;
                        txtcurDimsGround.Enabled = false;
                        txtexpDimsGround.Enabled = false;
                        txtcurResiFees.Enabled = false;
                        txtexpResiFees.Enabled = false;
                        txtcurFuelPI.Enabled = false;
                        txtexpFuelPI.Enabled = false;
                        txtcurFuelPuro.Enabled = false;
                        txtexpFuelPuro.Enabled = false;

                        //Dangerous Goods                                
                        txtcurDGFR.Enabled = false;
                        txtexpDGFR.Enabled = false;
                        txtcurDGUN3373.Enabled = false;
                        txtexpDGUN3373.Enabled = false;
                        txtcurDGUN1845.Enabled = false;
                        txtexpDGUN1845.Enabled = false;
                        txtcurDGLT500kg.Enabled = false;
                        txtexpDGLT500kg.Enabled = false;
                        txtcurDGLQ.Enabled = false;
                        txtexpDGLQ.Enabled = false;

                        //Special Handling                                
                        txtcurSHFP.Enabled = false;
                        txtexpSHFP.Enabled = false;
                        txtcurSHAH.Enabled = false;
                        txtexpSHAH.Enabled = false;
                        txtcurSHLP.Enabled = false;
                        txtexpSHLP.Enabled = false;
                        txtcurSHOML.Enabled = false;
                        txtexpSHOML.Enabled = false;
                        txtcurSHO.Enabled = false;
                        txtexpSHO.Enabled = false;
                        txtcurSHRAH.Enabled = false;
                        txtexpSHRAH.Enabled = false;
                        txtAccComment.Enabled = false;

                        cbxperc1.Enabled = false;
                        cbxperc2.Enabled = false;
                        cbxperc3.Enabled = false;
                        cbxperc4.Enabled = false;
                        cbxperc5.Enabled = false;
                        cbxpere1.Enabled = false;
                        cbxpere2.Enabled = false;
                        cbxpere3.Enabled = false;
                        cbxpere4.Enabled = false;
                        cbxpere5.Enabled = false;

                        radioCurrFuel.Enabled = false;
                        radioExpFuel.Enabled = false;
                        radioBeyondOrigin.Enabled = false;
                        radioBeyondDest.Enabled = false;
                    }
                    //User Role Contracts can edit Relationship, Contract and Dates
                    if (Session["userRole"].ToString().ToLower() != "contracts")
                    {
                        rnameCBX.Enabled = false;
                        rdpEffectiveDate.Enabled = false;
                        rdpExpiryDate.Enabled = false;
                        contractCBX.Enabled = false;
                        typeCBX.Enabled = false;
                        txtModReason.Enabled = false;
                    }

                    //Load RelationshipName List                   
                    List<ContractClassification> rnlist = ContractClassification.GetRelationshipList();
                    rnameCBX.DataSource = rnlist;
                    rnameCBX.DataBind();

                    //Load Renewal Types                    
                    typeCBX.Items.Clear();
                    List<ClsRenewalType> lstTypes;
                    lstTypes = ClsRenewalType.GetRenewalTypeList();
                    typeCBX.DataSource = lstTypes;
                    typeCBX.DataTextField = "ContractRenewalType";
                    typeCBX.DataValueField = "idContractRenewalType";
                    typeCBX.DataBind();

                    HiddenField hdnID = userControl.FindControl("ContractRenewalID") as HiddenField;
                    string hiddenID = hdnID.Value;
                    int ContractRenewalID = 0;
                    if (hiddenID != "")
                         ContractRenewalID = Convert.ToInt16(hiddenID);

                    if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                    {
                        //leave blank so it will be filled in when client is selected
                        rnameCBX.Enabled = true;
                    }
                    else
                    {
                        //disable for Edit mode                                     
                        rnameCBX.SelectedValue = hiddenRname;
                        rnameCBX.Enabled = false;
                    }

                    

                    if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
                    {
                        // insert item
                    }
                    else
                    {
                        RadTabStrip rts1 = userControl.FindControl("RadTabStrip1") as RadTabStrip;
                        //rts1.Tabs[3].Visible = false;
                        //RadButton btnSaveUpload = userControl.FindControl("btnSaveUpload") as RadButton;
                        //btnSaveUpload.Visible = false;

                        if (ContractRenewalID != 0)
                        {

                            //EXISTING CONTRACT

                            Label lblSaveFirst = userControl.FindControl("lblSaveFirst") as Label;
                            lblSaveFirst.Visible = false;
                            //SHOW Uploads if existing
                            rts1.Tabs[0].Enabled = true;
                            rts1.Tabs[1].Enabled = true;
                            rts1.Tabs[2].Enabled = true;
                            rts1.Tabs[3].Enabled = true;
                            //btnSaveUpload.Visible = true;

                            //Get CONTRACT FROM Database
                            ClsContractRenewal thisContract = ClsContractRenewal.GetContractRenewal(ContractRenewalID);                            
                            lblContractDetails.Text = thisContract.Relationship;

                            //Contract Dates                            
                            rdpEffectiveDate.SelectedDate = thisContract.effectiveDate;
                            rdpExpiryDate.SelectedDate = thisContract.expiryDate;

                            //Modification Reason
                            txtModReason.Text = thisContract.modificationReason;
                           
                            //Change label for New Contract
                            Label lblExpected = userControl.FindControl("lblExpected") as Label;                           
                            int newtypeid = Convert.ToInt16(ConfigurationManager.AppSettings["NewCustomerTypeID"]);
                            if (thisContract.idContractRenewalType == newtypeid)
                            {
                                lblExpected.Text = "Expected";
                            }
                            else
                            {
                                lblExpected.Text = "Increase";
                            }
                           
                            //Fill in all values                            
                            txtpctCourierN.Value = thisContract.pctIncreaseCourier;                            
                            txtpctFreightN.Value = thisContract.pctIncreaseFreight;                            
                            txtpctLTLN.Value = thisContract.pctIncreaseLTL;                            
                            txtpctPPSTN.Value = thisContract.pctIncreasePPST;                            
                            txtpctCPCN.Value = thisContract.pctIncreaseCPC;                            
                            txtpctOtherN.Value = thisContract.pctIncreaseOther;
                            //txtOtherDesc.Text = thisContract.otherDescription;
                           
                            txtcurMarginN.Value = thisContract.currMarginPct;                            
                            txtsowMarginN.Value = thisContract.sowMarginPct;                            
                            txtrocMarginN.Value = thisContract.rocMarginPct;                            
                            txtexpMarginN.Value = thisContract.expMarginPct;                            
                            txtnewMarginN.Value = thisContract.newMarginPct;

                            Label lblcurRevenue = userControl.FindControl("lblcurRevenue") as Label;
                            lblcurRevenue.Text = String.Format("{0:C0}", thisContract.currRevenue);
                            Label lblsowRevenue = userControl.FindControl("lblsowRevenue") as Label;
                            lblsowRevenue.Text = String.Format("{0:C0}", thisContract.sowRevenue);
                            Label lblrocRevenue = userControl.FindControl("lblrocRevenue") as Label;
                            lblrocRevenue.Text = String.Format("{0:C0}", thisContract.rocRevenue);
                            Label lblexpRevenue = userControl.FindControl("lblexpRevenue") as Label;
                            lblexpRevenue.Text = String.Format("{0:C0}", thisContract.expRevenue);
                            Label lblnewRevenue = userControl.FindControl("lblnewRevenue") as Label;
                            lblnewRevenue.Text = String.Format("{0:C0}", thisContract.newRevenue);
                                                       
                            txtcurCourierN.Value = (double)thisContract.currCourier;                           
                            txtsowCourierN.Value = (double)thisContract.sowCourier;                            
                            txtrocCourierN.Value = (double)thisContract.rocCourier;                            
                            txtexpCourierN.Value = (double)thisContract.expCourier;
                            Label lblnewCourier = userControl.FindControl("lblnewCourier") as Label;
                            lblnewCourier.Text = String.Format("{0:C0}", thisContract.newCourier);
                            
                            txtcurFFN.Value = (double)thisContract.currFFWD;                            
                            txtsowFFN.Value = (double)thisContract.sowFFWD;                            
                            txtrocFFN.Value = (double)thisContract.rocFFWD;                            
                            txtexpFFN.Value = (double)thisContract.expFFWD;
                            Label lblnewFF = userControl.FindControl("lblnewFF") as Label;
                            lblnewFF.Text = String.Format("{0:C0}", thisContract.newFFWD);
                            
                            txtcurLTLN.Value = (double)thisContract.currLTL;                            
                            txtsowLTLN.Value = (double)thisContract.sowLTL;                            
                            txtrocLTLN.Value = (double)thisContract.rocLTL;                            
                            txtexpLTLN.Value = (double)thisContract.expLTL;
                            Label lblnewLTL = userControl.FindControl("lblnewLTL") as Label;
                            lblnewLTL.Text = String.Format("{0:C0}", thisContract.newLTL);
                                                        
                            txtcurPPSTN.Value = (double)thisContract.currPPST;                            
                            txtsowPPSTN.Value = (double)thisContract.sowPPST;                            
                            txtrocPPSTN.Value = (double)thisContract.rocPPST;                            
                            txtexpPPSTN.Value = (double)thisContract.expPPST;
                            Label lblnewPPST = userControl.FindControl("lblnewPPST") as Label;
                            lblnewPPST.Text = String.Format("{0:C0}", thisContract.newPPST);
                            
                            txtcurCPCN.Value = (double)thisContract.currCPC;                            
                            txtsowCPCN.Value = (double)thisContract.sowCPC;                            
                            txtrocCPCN.Value = (double)thisContract.rocCPC;                            
                            txtexpCPCN.Value = (double)thisContract.expCPC;
                            Label lblnewCPC = userControl.FindControl("lblnewCPC") as Label;
                            lblnewCPC.Text = String.Format("{0:C0}", thisContract.newCPC);
                            
                            txtcurOtherN.Value = (double)thisContract.currOther;                           
                            txtsowOtherN.Value = (double)thisContract.sowOther;                            
                            txtrocOtherN.Value = (double)thisContract.rocOther;                            
                            txtexpOtherN.Value = (double)thisContract.expOther;
                            Label lblnewOther = userControl.FindControl("lblnewOther") as Label;
                            lblnewOther.Text = String.Format("{0:C0}", thisContract.newOther);
                            
                            txtcurTargetGrMarginPctN.Value = (double)thisContract.currTargetGrMarginPct;                            
                            txtsowTargetGrMarginPctN.Value = (double)thisContract.sowTargetGrMarginPct;                            
                            txtrocTargetGrMarginPctN.Value = (double)thisContract.rocTargetGrMarginPct;                            
                            txtexpTargetGrMarginPctN.Value = (double)thisContract.expTargetGrMarginPct;                            
                            txtnewTargetGrMarginPctN.Value = (double)thisContract.newTargetGrMarginPct;
                           
                            txtVDARevenue.Value = (double)thisContract.VDACommitment;                            
                            txtOtherDesc.Text = thisContract.otherDescription;  

                            //ACCESORIALS                                                   
                            txtcurDimsAir.Value = (double)thisContract.currDimsAir;

                            txtexpDimsAir.Value = (double)thisContract.expDimsAir;
                            txtcurDimsGround.Value = (double)thisContract.currDimsGround;
                            txtexpDimsGround.Value = (double)thisContract.expDimsGround;
                            txtcurResiFees.Value = (double)thisContract.currResiFees;
                            txtexpResiFees.Value = (double)thisContract.expResiFees;
                            txtcurFuelPI.Text = thisContract.currFuelPI.ToString();
                            txtexpFuelPI.Text = thisContract.expFuelPI.ToString();
                            txtcurFuelPuro.Text = thisContract.currFuelPuro.ToString();
                            txtexpFuelPuro.Text = thisContract.expFuelPuro.ToString();

                            //Dangerous Goods                           
                            txtcurDGFR.Value = (double)thisContract.currDGFR;
                            cbxperc1.SelectedValue = thisContract.currDGFRtype;
                            txtexpDGFR.Value = (double)thisContract.expDGFR;
                            cbxpere1.SelectedValue = thisContract.expDGFRtype;
                            txtcurDGUN3373.Value = (double)thisContract.currDGUN3373;
                            cbxperc2.SelectedValue = thisContract.currDGUN3373type;
                            txtexpDGUN3373.Value = (double)thisContract.expDGUN3373;
                            cbxpere2.SelectedValue = thisContract.expDGUN3373type;
                            txtcurDGUN1845.Value = (double)thisContract.currDGUN1845;
                            cbxperc3.SelectedValue = thisContract.currDGUN1845type;
                            txtexpDGUN1845.Value = (double)thisContract.expDGUN1845;
                            cbxpere3.SelectedValue = thisContract.expDGUN1845type;
                            txtcurDGLT500kg.Value = (double)thisContract.currDGLT500kg;
                            cbxperc4.SelectedValue = thisContract.currDGLT500kgtype;
                            txtexpDGLT500kg.Value = (double)thisContract.expDGLT500kg;
                            cbxpere4.SelectedValue = thisContract.expDGLT500kgtype;
                            txtcurDGLQ.Value = (double)thisContract.currDGLQ;
                            cbxperc5.SelectedValue = thisContract.currDGLQtype;
                            txtexpDGLQ.Value = (double)thisContract.expDGLQ;
                            cbxpere5.SelectedValue = thisContract.expDGLQtype;

                            //Special Handling                            
                            txtcurSHFP.Value = (double)thisContract.currSHFP;
                            txtexpSHFP.Value = (double)thisContract.expSHFP;
                            txtcurSHAH.Value = (double)thisContract.currSHAH;
                            txtexpSHAH.Value = (double)thisContract.expSHAH;
                            txtcurSHLP.Value = (double)thisContract.currSHLP;
                            txtexpSHLP.Value = (double)thisContract.expSHLP;
                            txtcurSHOML.Value = (double)thisContract.currSHOML;
                            txtexpSHOML.Value = (double)thisContract.expSHOML;
                            txtcurSHO.Value = (double)thisContract.currSHO;
                            txtexpSHO.Value = (double)thisContract.expSHO;
                            txtcurSHRAH.Value = (double)thisContract.currSHRAH;
                            txtexpSHRAH.Value = (double)thisContract.expSHRAH;

                            //Fuel
                            if (thisContract.currAlignPuroInc == true)
                                radioCurrFuel.SelectedValue = "fuelpi";
                            else
                                radioCurrFuel.SelectedValue = "fuelpuroinc";
                            if (thisContract.expAlignPuroInc == true)
                                radioExpFuel.SelectedValue = "fuelpi";
                            else
                                radioExpFuel.SelectedValue = "fuelpuroinc";

                            //Beyond Charges
                            if (thisContract.currBeyondOriginDisc == true)
                               radioBeyondOrigin.SelectedValue = "yes";
                            else
                                radioBeyondOrigin.SelectedValue = "no";
                            if (thisContract.expBeyondDestDisc == true)
                                radioBeyondDest.SelectedValue = "yes";
                            else
                                radioBeyondDest.SelectedValue = "no";
                            
                            txtAccComment.Text = thisContract.accessorialComment;

                            RadButton addButton = userControl.FindControl("btnAddNote") as RadButton;
                            addButton.Visible = true;
                            
                            //ContractNumbers
                            contractCBX.Items.Clear();
                            List<ClsContractRelationship> lstContracts;
                            ClsContractRelationship cr = new ClsContractRelationship();
                            lstContracts = cr.GetContractsByRelationship(hiddenRname);
                            contractCBX.DataSource = lstContracts;
                            contractCBX.DataTextField = "ContractNumber";
                            contractCBX.DataValueField = "ContractNumber";
                            contractCBX.DataBind();
                            //Select The Right Ones

                            List<string> lstSelectedContracts = ClsContractRenewal.GetContractNumbers(ContractRenewalID);
                            foreach (RadComboBoxItem rcbItem in contractCBX.Items)
                            {
                                //if this item is in the selected list, select the checkbox
                                string checkedvalue = lstSelectedContracts.FirstOrDefault(x => x == rcbItem.Value);
                                if (checkedvalue != null)
                                    rcbItem.Checked = true;
                            }
                            //Get First Ship Date, if available in PI Sales Incentives table
                            try
                            {
                                Label lblFSD  = userControl.FindControl("lblFSD") as Label;
                                lblFSD.Text = "unavailable";
                               
                                ClsContractRelationship ccr = new ClsContractRelationship();
                                DateTime? fsd = ccr.GetFirstShipDate(hiddenRname);
                                if (fsd == null)
                                {
                                    lblFSD.Text = "unavailable";
                                }
                                else
                                {
                                    lblFSD.Text = Convert.ToDateTime(fsd).ToString("MM/dd/yyyy");
                                }

                            }
                            catch (Exception ex)
                            {

                            }
                            //Get SRID and Branch
                            Label lblBranch = userControl.FindControl("lblBranch") as Label;
                            Label lblSRID = userControl.FindControl("lblSRID") as Label;
                            string srids = "";
                            List<string> sridList = new List<string>();
                            string branches = "";
                            List<string> branchList = new List<string>();
                            bool sridfound;
                            bool branchfound;
                            foreach (ClsContractRelationship critem in lstContracts)
                            {
                                string checkedvalue = lstSelectedContracts.FirstOrDefault(x => x == critem.ContractNumber);
                                if (checkedvalue != null)
                                {

                                    //Check SRID
                                    sridfound = false;
                                    foreach (string sriditem in sridList)
                                    {
                                        if (critem.Territory == sriditem)
                                            sridfound = true;
                                    }
                                    if (sridfound == false)
                                    {
                                        sridList.Add(critem.Territory);
                                        if (srids != "")
                                            srids = srids + ", ";
                                        srids = srids + critem.Territory;
                                    }

                                    //Check Branch
                                    branchfound = false;
                                    foreach (string branchitem in branchList)
                                    {
                                        if (critem.Region == branchitem)
                                            branchfound = true;
                                    }
                                    if (branchfound == false)
                                    {
                                        branchList.Add(critem.Region);
                                        if (branches != "")
                                            branches = branches + ", ";
                                        branches = branches + critem.Region;
                                    }
                                    lblSRID.Text = srids;
                                    lblSRID.ForeColor = System.Drawing.Color.Blue;
                                    lblBranch.Text = branches;
                                    lblBranch.ForeColor = System.Drawing.Color.Blue;

                                    //Get Sls Name, if there is a single srid
                                    if (srids.IndexOf(',') == -1)
                                    {
                                        ClsSalesReps rep = ClsSalesReps.GetSalesReps(srids);
                                        Label lblslsName = userControl.FindControl("lblslsName") as Label;
                                        if (rep != null)
                                            lblslsName.Text = rep.SalesRep;
                                    }
                                   
                                }

                            }
                            
                            //OTHER
                            bool otherFlag = false;
                            if (thisContract.currOther != 0 && thisContract.currOther != null)
                                otherFlag = true;
                            if (thisContract.sowOther != 0 && thisContract.sowOther != null)
                                otherFlag = true;
                            if (thisContract.rocOther != 0 && thisContract.rocOther != null)
                                otherFlag = true;
                            if (thisContract.expOther != 0 && thisContract.expOther != null)
                                otherFlag = true;
                            if (thisContract.newOther != 0 && thisContract.newOther != null)
                                otherFlag = true;
                            if (otherFlag == true)
                            {
                                Label lblOtherDesc = userControl.FindControl("lblOtherDesc") as Label;
                                Label lblAnOther = userControl.FindControl("lblAnOther") as Label;
                                lblOtherDesc.Visible = true;
                                txtOtherDesc.Visible = true;
                                lblnewOther.Visible = true;
                                lblAnOther.Visible = true;
                                txtcurOtherN.Visible = true;
                                txtexpOtherN.Visible = true;
                                if (thisContract.sowFlag == true)
                                    txtsowOtherN.Visible = true;
                                if (thisContract.rateOfChangeFlag == true)
                                    txtrocOtherN.Visible = true;
                            }

                          

                            //Hide fields if New Contract
                            //if (thisContract.idContractRenewalType == 5)
                            int newcusttypeid = Convert.ToInt16(ConfigurationManager.AppSettings["NewCustomerTypeID"]);
                            if (thisContract.idContractRenewalType == newcusttypeid)
                            {
                                Label lblVDA = userControl.FindControl("lblNewContract") as Label;
                                lblVDA.Visible = true;
                                Label lblVDA2 = userControl.FindControl("lblNewContract1") as Label;
                                lblVDA2.Visible = true;
                                txtVDARevenue.Visible = true;
                                //Get Revenue
                                RadButton btnGetRevenue = userControl.FindControl("btnGetRevenue") as RadButton;
                                btnGetRevenue.Visible = false;
                                Label lblGetRevenue = userControl.FindControl("lblGetRevenue") as Label;
                                lblGetRevenue.Visible = false;
                                Label lblDateRange= userControl.FindControl("lblDateRange") as Label;
                                lblDateRange.Visible = false;

                                //hide labels
                                Label lblpctIncreases = userControl.FindControl("lblpctIncreases") as Label;
                                lblpctIncreases.Visible = false;
                                Label lblPctIncCourier = userControl.FindControl("lblPctIncCourier") as Label;
                                lblPctIncCourier.Visible = false;
                                Label lblPctIncFreight = userControl.FindControl("lblPctIncFF") as Label;
                                lblPctIncFreight.Visible = false;
                                Label lblPctIncLTL = userControl.FindControl("lblPctIncLTL") as Label;
                                lblPctIncLTL.Visible = false;
                                Label lblPctIncPPST = userControl.FindControl("lblPctIncPPST") as Label;
                                lblPctIncPPST.Visible = false;
                                Label lblPctIncCPC = userControl.FindControl("lblPctIncCPC") as Label;
                                lblPctIncCPC.Visible = false;
                                Label lblPctIncOther = userControl.FindControl("lblPctIncOther") as Label;
                                lblPctIncOther.Visible = false;

                                Label lblCurrent = userControl.FindControl("lblCurrent") as Label;
                                lblCurrent.Visible = false;
                                Label lblNewProfile = userControl.FindControl("lblNewProfile") as Label;
                                lblNewProfile.Visible = false;                               

                                //hide numeric text boxes fields
                                txtpctCourierN.Visible = false;                                
                                txtpctFreightN.Visible = false;
                                txtpctLTLN.Visible = false;
                                txtpctPPSTN.Visible = false;
                                txtpctCPCN.Visible = false;
                                txtpctOtherN.Visible = false;

                                txtcurMarginN.Visible = false;
                                txtnewMarginN.Visible = false;
                                lblcurRevenue.Visible = false;
                                lblnewRevenue.Visible = false;
                                txtcurCourierN.Visible = false;
                                lblnewCourier.Visible = false;
                                txtcurFFN.Visible = false;
                                lblnewFF.Visible = false;
                                txtcurLTLN.Visible = false;
                                lblnewLTL.Visible = false;
                                txtcurPPSTN.Visible = false;
                                lblnewPPST.Visible = false;
                                txtcurCPCN.Visible = false;
                                lblnewCPC.Visible = false;
                                txtcurOtherN.Visible = false;
                                lblnewOther.Visible = false;
                                txtcurTargetGrMarginPctN.Visible = false;
                                txtnewTargetGrMarginPctN.Visible = false;

                                //hide flags for New Contracts
                                Label lblsowFlag = userControl.FindControl("lblsowFlag") as Label;
                                lblsowFlag.Visible = false;
                                sowFlag.Visible = false;
                                showHideSOW(userControl,false); 
                                
                                Label lblrocFlag = userControl.FindControl("lblrocFlag") as Label;
                                lblrocFlag.Visible = false;
                                rocFlag.Visible = false;
                                showHideROC(userControl, false);
                            }
                            //Show or hide columns based on selection
                            //SOW Flag
                            bool sowVisible = (bool)thisContract.sowFlag;
                            showHideSOW(userControl,sowVisible);

                            //Rate Of Change Flag
                            bool rocVisible = (bool)thisContract.rateOfChangeFlag;
                            showHideROC(userControl, rocVisible);



                            //Choose Renewal Type
                            HiddenField hdnTypeID = userControl.FindControl("hdnRewewalType") as HiddenField;
                            string hiddenTypeID = hdnTypeID.Value;
                            int ContractRenewalTypeID = 0;
                            if (hiddenTypeID != "")
                            {
                                ContractRenewalTypeID = Convert.ToInt16(hiddenTypeID);
                                typeCBX.SelectedValue = ContractRenewalTypeID.ToString();
                            }

                            
                            
                        }                     
                       
                    }

                    //Keep the binding of notesGrid here, not in the if stmt for existing contract id otherwise form stops posting back
                    //NOTES
                    List<ClsRenewalNotes> notesList = ClsRenewalNotes.GetContractRenewalNotes(ContractRenewalID);
                    RadGrid notesGrid = userControl.FindControl("rgNotesGrid") as RadGrid;
                    notesGrid.DataSource = notesList;
                    notesGrid.DataBind();

                    //FILE UPLOADS
                    ClsFileUpload fup = new ClsFileUpload();
                    List<ClsFileUpload> alluploads = fup.GetFileList(ContractRenewalID);
                    RadGrid rgUpload = userControl.FindControl("rgUpload") as RadGrid;
                    rgUpload.DataSource = alluploads;
                    rgUpload.DataBind();

                    //Routing
                    try
                     {
                         RadComboBox routeToCBX = userControl.FindControl("cbxRouteTo") as RadComboBox;
                         Label AssignedTo = userControl.FindControl("lblAssignedTo") as Label;
                         AssignedTo.Visible = true;
                         routeToCBX.Items.Clear();
                         List<ClsRenewalRouting> lstRoute;
                         lstRoute = ClsRenewalRouting.GetRenewalRouteList();
                         routeToCBX.DataSource = lstRoute;
                         routeToCBX.DataTextField = "RoutingName";
                         routeToCBX.DataValueField = "idContractRenewalRouting";
                         routeToCBX.DataBind();
                         HiddenField hdnRouteTo = userControl.FindControl("hdnRouteTo") as HiddenField;
                         string hiddenRouteTo = hdnRouteTo.Value;
                         int ContractRenewalRouteID = 0;
                         if (hiddenRouteTo != "" && hiddenRouteTo != "0")
                         {
                             Label lblcurrentroute = userControl.FindControl("lblcurrentroute") as Label;
                             lblcurrentroute.Visible = true;
                             ContractRenewalRouteID = Convert.ToInt16(hiddenRouteTo);
                             routeToCBX.SelectedValue = ContractRenewalRouteID.ToString();
                             if (routeToCBX != null)
                                  AssignedTo.Text = routeToCBX.SelectedItem.Text;
                         }

                     }

                                          
                     catch (Exception ex)
                     {
                     }

                  
                   
                }


                catch (Exception ex)
                {
                    string msg = ex.Message;
                    lblWarning.Text = msg;
                    pnlwarning.Visible = true;

                }

            }
            
            
        }

       

        protected void showHideSOW(UserControl userControl, bool visibleFlag)
        {

            Label lblSOW = userControl.FindControl("lblSOW") as Label;
            RadNumericTextBox txtsowMarginN = userControl.FindControl("txtsowMarginN") as RadNumericTextBox;
            Label lblsowRevenue = userControl.FindControl("lblsowRevenue") as Label;
            RadNumericTextBox txtsowCourierN = userControl.FindControl("txtsowCourierN") as RadNumericTextBox;
            RadNumericTextBox txtsowFFN = userControl.FindControl("txtsowFFN") as RadNumericTextBox;
            RadNumericTextBox txtsowLTLN = userControl.FindControl("txtsowLTLN") as RadNumericTextBox;
            RadNumericTextBox txtsowPPSTN = userControl.FindControl("txtsowPPSTN") as RadNumericTextBox;
            RadNumericTextBox txtsowCPCN = userControl.FindControl("txtsowCPCN") as RadNumericTextBox;
            RadNumericTextBox txtsowTargetGrMarginPctN = userControl.FindControl("txtsowTargetGrMarginPctN") as RadNumericTextBox;

            lblSOW.Visible = visibleFlag;
            //txtsowMarginN.Visible = visibleFlag;
            lblsowRevenue.Visible = visibleFlag;
            txtsowCourierN.Visible = visibleFlag;
            txtsowFFN.Visible = visibleFlag;
            txtsowLTLN.Visible = visibleFlag;
            txtsowPPSTN.Visible = visibleFlag;
            txtsowCPCN.Visible = visibleFlag;
            txtsowTargetGrMarginPctN.Visible = visibleFlag;           
        }

        protected void showHideROC(UserControl userControl,bool visibleFlag)
        {

            Label lblROC = userControl.FindControl("lblROC") as Label;
            RadNumericTextBox txtrocMarginN = userControl.FindControl("txtrocMarginN") as RadNumericTextBox;
            Label lblrocRevenue = userControl.FindControl("lblrocRevenue") as Label;
            RadNumericTextBox txtrocCourierN = userControl.FindControl("txtrocCourierN") as RadNumericTextBox;
            RadNumericTextBox txtrocFFN = userControl.FindControl("txtrocFFN") as RadNumericTextBox;
            RadNumericTextBox txtrocLTLN = userControl.FindControl("txtrocLTLN") as RadNumericTextBox;
            RadNumericTextBox txtrocPPSTN = userControl.FindControl("txtrocPPSTN") as RadNumericTextBox;
            RadNumericTextBox txtrocCPCN = userControl.FindControl("txtrocCPCN") as RadNumericTextBox;
            RadNumericTextBox txtrocTargetGrMarginPctN = userControl.FindControl("txtrocTargetGrMarginPctN") as RadNumericTextBox;

            lblROC.Visible = visibleFlag;
            //txtrocMarginN.Visible = visibleFlag;
            lblrocRevenue.Visible = visibleFlag;
            txtrocCourierN.Visible = visibleFlag;
            txtrocFFN.Visible = visibleFlag;
            txtrocLTLN.Visible = visibleFlag;
            txtrocPPSTN.Visible = visibleFlag;
            txtrocCPCN.Visible = visibleFlag;
            txtrocTargetGrMarginPctN.Visible = visibleFlag;
        }

         protected void rgContracts_CancelCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            
        }

         protected void rgContracts_DeleteCommand(object sender, GridCommandEventArgs e)
         {
             try
             {

                 GridDataItem item = (GridDataItem)e.Item;
                 string ContractRenewalID = item.OwnerTableView.DataKeyValues[item.ItemIndex]["idContractRenewal"].ToString();

                 Int16 idContractRewewal = Convert.ToInt16(ContractRenewalID);
                 //Get Row and Set ActiveFlag to false
                 if (idContractRewewal != 0)
                 {
                     //Get CONTRACT FROM Database
                     string userID = Session["userName"].ToString();
                     ClsContractRenewal.DeactivateContract(idContractRewewal, userID);


                 }


                 GetContractInfo();
                 rgContracts.DataBind();
                
             }
             catch (Exception ex)
             {
                 pnlDanger.Visible = true;
                 lblDanger.Text = ex.Message.ToString();
                 e.Canceled = true;
             }
         }


         protected void rgContracts_UpdateCommand(object source, GridCommandEventArgs e)
         {
             try
             {
                
                 {
                     if (Page.IsValid)
                     {
                         if (Session["userName"] == null)
                             Response.Redirect("Default.aspx");

                         //Get existing ID
                         UserControl userControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                         HiddenField hdnID = userControl.FindControl("ContractRenewalID") as HiddenField;
                         string hiddenID = hdnID.Value;
                         int ContractRenewalID = 0;
                         if (hiddenID != "")
                             ContractRenewalID = Convert.ToInt16(hiddenID);

                         doSubmit(false, ContractRenewalID, userControl);

                        
                     }

                 }
             }
             catch (Exception ex)
             {
                 lblWarning.Text = ex.Message;
                 lblWarning.Visible = true;
                 pnlwarning.Visible = true;
             }

         }

      
         protected void rgContracts_InsertCommand(object source, GridCommandEventArgs e)
         {
             try
             {
                     if (Page.IsValid)
                     {
                         if (Session["userName"] == null)
                             Response.Redirect("Default.aspx");

                         UserControl userControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;

                         doSubmit(true, 0, userControl);
                     }                       

                
             }
             catch (Exception ex)
             {
                 lblWarning.Text = ex.Message;
                 lblWarning.Visible = true;
                 pnlwarning.Visible = true;
             }
         }

        private void doSubmit(bool newFlag, int idContractRenewal, UserControl userControl)
        {
            //Populate the object with the current values
            //Header
            objContractRenewal = populateContractRenewalObj(newFlag, idContractRenewal, userControl);

            //Detail
            List<string> contractlist = populateContractRenewalDetObj(userControl);            

            //Check for Unsaved Notes
            ClsRenewalNotes noteToSave = checkforNotes(userControl);

            //Do DB Update
            ClsContractRenewal.UpdateContractRenewal(objContractRenewal,contractlist,noteToSave);

            //Check for Unsaved File Upload

            //Send Email
            bool emailFlag = (userControl.FindControl("sendEmailFlag") as RadButton).Checked;
            if (emailFlag == true)
            {
                sendEmail(objContractRenewal);
            }
        }


        private ClsContractRenewal populateContractRenewalObj(bool newFlag, int idContractRenewal, UserControl userControl)
         {
            try
            {
                if (newFlag == true)
                {
                    objContractRenewal.CreatedBy = (string)Session["userName"];
                    objContractRenewal.CreatedOn = Convert.ToDateTime(DateTime.Now);
                    objContractRenewal.ActiveFlag = true;
                }
                else
                {
                    //getExisting ContractRenewal record
                    ClsContractRenewal editRow = new ClsContractRenewal();
                    editRow = ClsContractRenewal.GetContractRenewal(idContractRenewal);
                    //maintain original CREATED BY values
                    objContractRenewal.idContractRenewal = editRow.idContractRenewal;
                    objContractRenewal.CreatedBy = editRow.CreatedBy;
                    objContractRenewal.CreatedOn = editRow.CreatedOn;
                }

                //Populate all fields
                string RelationshipName = (userControl.FindControl("cbxRelationshipName") as RadComboBox).SelectedValue;
                objContractRenewal.Relationship = RelationshipName;

                RadComboBox cbxRenewalTypes = userControl.FindControl("cbxRenewalTypes") as RadComboBox;
                Int16 idContractRenewalType = Convert.ToInt16(cbxRenewalTypes.SelectedValue);
                objContractRenewal.idContractRenewalType = idContractRenewalType;
                string renewaltype = cbxRenewalTypes.SelectedItem.Text;
                objContractRenewal.RenewalType = renewaltype;
                DateTime rdpEffectiveDate = (DateTime)(userControl.FindControl("rdpEffectiveDate") as RadDatePicker).SelectedDate;
                objContractRenewal.effectiveDate = rdpEffectiveDate;
                DateTime rdpExpiryDate = (DateTime)(userControl.FindControl("rdpExpiryDate") as RadDatePicker).SelectedDate;
                objContractRenewal.expiryDate = rdpExpiryDate;
                String txtModReason = (userControl.FindControl("txtModReason") as TextBox).Text;
                objContractRenewal.modificationReason = txtModReason;

                RadComboBox cbxRouteTo = userControl.FindControl("cbxRouteTo") as RadComboBox;
                string routeTo = cbxRouteTo.Text;
                if (routeTo == null)
                    routeTo = "";
                objContractRenewal.currentlyRoutedTo = routeTo;
                int routeToID = 0;
                int routeToIDi = 0;
                if (Int32.TryParse(cbxRouteTo.SelectedValue, out routeToIDi))
                    routeToID = routeToIDi;
                objContractRenewal.idContractRenewalRouting = routeToID;
                               
                //All these fields are optional - fill in with zero if no value
                objContractRenewal.pctIncreaseCourier = getDoubleValue(userControl, "txtpctCourierN");                
                objContractRenewal.pctIncreaseFreight = getDoubleValue(userControl, "txtpctFFN");
                objContractRenewal.pctIncreaseLTL = getDoubleValue(userControl, "txtpctLTLN");
                objContractRenewal.pctIncreasePPST = getDoubleValue(userControl, "txtpctPPSTN");
                objContractRenewal.pctIncreaseCPC = getDoubleValue(userControl, "txtpctCPCN");
                objContractRenewal.pctIncreaseOther = getDoubleValue(userControl, "txtpctOtherN");

                objContractRenewal.currMarginPct = getDoubleValue(userControl, "txtcurMarginN");
                objContractRenewal.sowMarginPct = getDoubleValue(userControl, "txtsowMarginN");
                objContractRenewal.rocMarginPct = getDoubleValue(userControl, "txtrocMarginN");
                objContractRenewal.expMarginPct = getDoubleValue(userControl, "txtexpMarginN");
                objContractRenewal.newMarginPct = getDoubleValue(userControl, "txtnewMarginN");

                objContractRenewal.currRevenue = getlblDecimalValue(userControl, "lblcurRevenue");
                objContractRenewal.sowRevenue = getlblDecimalValue(userControl, "lblsowRevenue");
                objContractRenewal.rocRevenue = getlblDecimalValue(userControl, "lblrocRevenue");
                objContractRenewal.expRevenue = getlblDecimalValue(userControl, "lblexpRevenue");
                objContractRenewal.newRevenue = getlblDecimalValue(userControl, "lblnewRevenue");

                objContractRenewal.currCourier = getDecimalValue(userControl, "txtcurCourierN");
                objContractRenewal.sowCourier = getDecimalValue(userControl, "txtsowCourierN");
                objContractRenewal.rocCourier = getDecimalValue(userControl, "txtrocCourierN");
                objContractRenewal.expCourier = getDecimalValue(userControl, "txtexpCourierN");
                objContractRenewal.newCourier = getlblDecimalValue(userControl, "lblnewCourier");

                objContractRenewal.currFFWD = getDecimalValue(userControl, "txtcurFFN");
                objContractRenewal.sowFFWD = getDecimalValue(userControl, "txtsowFFN");
                objContractRenewal.rocFFWD = getDecimalValue(userControl, "txtrocFFN");
                objContractRenewal.expFFWD = getDecimalValue(userControl, "txtexpFFN");
                objContractRenewal.newFFWD = getlblDecimalValue(userControl, "lblnewFF");

                objContractRenewal.currLTL = getDecimalValue(userControl, "txtcurLTLN");
                objContractRenewal.sowLTL = getDecimalValue(userControl, "txtsowLTLN");
                objContractRenewal.rocLTL = getDecimalValue(userControl, "txtrocLTLN");
                objContractRenewal.expLTL = getDecimalValue(userControl, "txtexpLTLN");
                objContractRenewal.newLTL = getlblDecimalValue(userControl, "lblnewLTL");

                objContractRenewal.currPPST = getDecimalValue(userControl, "txtcurPPSTN");
                objContractRenewal.sowPPST = getDecimalValue(userControl, "txtsowPPSTN");
                objContractRenewal.rocPPST = getDecimalValue(userControl, "txtrocPPSTN");
                objContractRenewal.expPPST = getDecimalValue(userControl, "txtexpPPSTN");
                objContractRenewal.newPPST = getlblDecimalValue(userControl, "lblnewPPST");

                objContractRenewal.currCPC = getDecimalValue(userControl, "txtcurCPCN");
                objContractRenewal.sowCPC = getDecimalValue(userControl, "txtsowCPCN");
                objContractRenewal.rocCPC = getDecimalValue(userControl, "txtrocCPCN");
                objContractRenewal.expCPC = getDecimalValue(userControl, "txtexpCPCN");
                objContractRenewal.newCPC = getlblDecimalValue(userControl, "lblnewCPC");

                objContractRenewal.currOther = getDecimalValue(userControl, "txtcurOtherN");
                objContractRenewal.sowOther = getDecimalValue(userControl, "txtsowOtherN");
                objContractRenewal.rocOther = getDecimalValue(userControl, "txtrocOtherN");
                objContractRenewal.expOther = getDecimalValue(userControl, "txtexpOtherN");
                objContractRenewal.newOther = getlblDecimalValue(userControl, "lblnewOther");

                objContractRenewal.currTargetGrMarginPct = getDoubleValue(userControl, "txtcurTargetGrMarginPctN");
                objContractRenewal.sowTargetGrMarginPct = getDoubleValue(userControl, "txtsowTargetGrMarginPctN");
                objContractRenewal.rocTargetGrMarginPct = getDoubleValue(userControl, "txtrocTargetGrMarginPctN");
                objContractRenewal.expTargetGrMarginPct = getDoubleValue(userControl, "txtexpTargetGrMarginPctN");
                objContractRenewal.newTargetGrMarginPct = getDoubleValue(userControl, "txtnewTargetGrMarginPctN");


                //ACCESSORIALS
                objContractRenewal.currDimsAir = getDecimalValue(userControl, "txtcurDimsAir");
                objContractRenewal.expDimsAir = getDecimalValue(userControl, "txtexpDimsAir");
                objContractRenewal.currDimsGround = getDecimalValue(userControl, "txtcurDimsGround");
                objContractRenewal.expDimsGround = getDecimalValue(userControl, "txtexpDimsGround");
                objContractRenewal.currResiFees = getDecimalValue(userControl, "txtcurResiFees");
                objContractRenewal.expResiFees = getDecimalValue(userControl, "txtexpResiFees");
                objContractRenewal.currFuelPI = getStringValue(userControl, "txtcurFuelPIS");
                objContractRenewal.expFuelPI = getStringValue(userControl, "txtexpFuelPIS");
                objContractRenewal.currFuelPuro = getStringValue(userControl, "txtcurFuelPuroS");
                objContractRenewal.expFuelPuro = getStringValue(userControl, "txtexpFuelPuroS");

                objContractRenewal.currDGFR = getDecimalValue(userControl, "txtcurDGFR");
                objContractRenewal.expDGFR = getDecimalValue(userControl, "txtexpDGFR");
                objContractRenewal.currDGUN3373 = getDecimalValue(userControl, "txtcurDGUN3373");
                objContractRenewal.expDGUN3373 = getDecimalValue(userControl, "txtexpDGUN3373");
                objContractRenewal.currDGUN1845 = getDecimalValue(userControl, "txtcurDGUN1845");
                objContractRenewal.expDGUN1845 = getDecimalValue(userControl, "txtexpDGUN1845");
                objContractRenewal.currDGUN1845 = getDecimalValue(userControl, "txtcurDGUN1845");
                objContractRenewal.currDGLT500kg = getDecimalValue(userControl, "txtcurDGLT500kg");
                objContractRenewal.expDGLT500kg = getDecimalValue(userControl, "txtexpDGLT500kg");
                objContractRenewal.currDGLQ = getDecimalValue(userControl, "txtcurDGLQ");
                objContractRenewal.expDGLQ = getDecimalValue(userControl, "txtexpDGLQ");

                objContractRenewal.currSHFP = getDecimalValue(userControl, "txtcurSHFP");
                objContractRenewal.expSHFP = getDecimalValue(userControl, "txtexpSHFP");
                objContractRenewal.currSHAH = getDecimalValue(userControl, "txtcurSHAH");
                objContractRenewal.expSHAH = getDecimalValue(userControl, "txtexpSHAH");
                objContractRenewal.currSHLP = getDecimalValue(userControl, "txtcurSHLP");
                objContractRenewal.expSHLP = getDecimalValue(userControl, "txtexpSHLP");
                objContractRenewal.currSHOML = getDecimalValue(userControl, "txtcurSHOML");
                objContractRenewal.expSHOML = getDecimalValue(userControl, "txtexpSHOML");
                objContractRenewal.currSHO = getDecimalValue(userControl, "txtcurSHO");
                objContractRenewal.expSHO = getDecimalValue(userControl, "txtexpSHO");
                objContractRenewal.currSHRAH = getDecimalValue(userControl, "txtcurSHRAH");
                objContractRenewal.expSHRAH = getDecimalValue(userControl, "txtexpSHRAH");

                objContractRenewal.currDGFRtype = getDropDownValue(userControl, "cbxperc1");
                objContractRenewal.expDGFRtype = getDropDownValue(userControl, "cbxpere1");
                objContractRenewal.currDGUN3373type = getDropDownValue(userControl, "cbxperc2");
                objContractRenewal.expDGUN3373type = getDropDownValue(userControl, "cbxpere2");
                objContractRenewal.currDGUN1845type = getDropDownValue(userControl, "cbxperc3");
                objContractRenewal.expDGUN1845type = getDropDownValue(userControl, "cbxpere3");
                objContractRenewal.currDGLT500kgtype = getDropDownValue(userControl, "cbxperc4");
                objContractRenewal.expDGLT500kgtype = getDropDownValue(userControl, "cbxpere4");
                objContractRenewal.currDGLQtype = getDropDownValue(userControl, "cbxperc5");
                objContractRenewal.expDGLQtype = getDropDownValue(userControl, "cbxpere5");

                string curFuelAlign = getRadioValue(userControl, "radioCurrFuel");
                if (curFuelAlign == "fuelpi")
                    objContractRenewal.currAlignPuroInc = true;
                else
                    objContractRenewal.currAlignPuroInc = false;
                string expFuelAlign = getRadioValue(userControl, "radioExpFuel");
                if (expFuelAlign == "fuelpi")
                    objContractRenewal.expAlignPuroInc = true;
                else
                    objContractRenewal.expAlignPuroInc = false;

                string beyondOrigin = getRadioValue(userControl, "radioBeyondOrigin");
                if (beyondOrigin == "yes")
                    objContractRenewal.currBeyondOriginDisc = true;
                else
                    objContractRenewal.currBeyondOriginDisc = false;
                string beyondDest = getRadioValue(userControl, "radioBeyondDest");
                if (beyondDest == "yes")
                    objContractRenewal.currBeyondDestDisc = true;
                else
                    objContractRenewal.currBeyondDestDisc = false;

                String txtOtherDesc = (userControl.FindControl("txtOtherDesc") as RadTextBox).Text;
                objContractRenewal.otherDescription = txtOtherDesc;

                objContractRenewal.VDACommitment = getDecimalValue(userControl, "txtVDACommitment");

                String txtAccCommentVal = (userControl.FindControl("txtAccessorialComment") as TextBox).Text;
                objContractRenewal.accessorialComment = txtAccCommentVal;



                bool sowFlag = (bool)(userControl.FindControl("sowFlag") as RadButton).Checked;
                objContractRenewal.sowFlag = sowFlag;
                bool rocFlag = (bool)(userControl.FindControl("rocFlag") as RadButton).Checked;
                objContractRenewal.rateOfChangeFlag = rocFlag;

                objContractRenewal.UpdatedBy =  (string)(Session["userName"]);
                objContractRenewal.UpdatedOn = DateTime.Now;        
                

               
                
            }
            catch (Exception ex)
            {
                lblWarning.Text = ex.Message;
                lblWarning.Visible = true;
                pnlwarning.Visible = true;
             }

             return objContractRenewal;

         }

        private List<string> populateContractRenewalDetObj(UserControl userControl)
        {
            List<string> contractlist = new List<string>();
            try
            {
                //List of ContractNumbers
                RadComboBox contractCBX = userControl.FindControl("cbxContracts") as RadComboBox;                  
               
                foreach (RadComboBoxItem rcbItem in contractCBX.Items)
                {
                    
                    if (rcbItem.Checked == true)
                    {
                        contractlist.Add(rcbItem.Text);
                    }
                }         

            }
            catch (Exception ex)
            {
                lblWarning.Text = ex.Message;
                lblWarning.Visible = true;
                pnlwarning.Visible = true;
            }

            return contractlist;
        }


        private double getDoubleValue(UserControl userControl, String radTextBoxName)
        {
            double returnval = 0;

            string txtpctCourierN =(userControl.FindControl(radTextBoxName) as RadNumericTextBox).Text;
            if (txtpctCourierN != "")
            {
                returnval = Convert.ToDouble(txtpctCourierN);
            }
            

            return returnval;

        }

        private decimal getDecimalValue(UserControl userControl, String radTextBoxName)
        {
            decimal returnval = 0;

            string txtNumericValue = (userControl.FindControl(radTextBoxName) as RadNumericTextBox).Text;
            if (txtNumericValue != "")
            {
                returnval = Convert.ToDecimal(txtNumericValue);
            }


            return returnval;
        }


        private string getStringValue(UserControl userControl, String radTextBoxName)
        {
            
            string txtValue = (userControl.FindControl(radTextBoxName) as RadTextBox).Text;
            return txtValue;

        }

        private string getDropDownValue(UserControl userControl, String radComboBoxName)
        {
            
            string txtValue = (userControl.FindControl(radComboBoxName) as RadComboBox).SelectedValue;
            return txtValue;

        }

        private string getRadioValue(UserControl userControl, String radioButtonName)
        {

            string txtValue = (userControl.FindControl(radioButtonName) as RadioButtonList).SelectedValue;
            return txtValue;

        }


        private decimal getlblDecimalValue(UserControl userControl, String labelName)
        {
            decimal returnval = 0;

            string txtNumericValue = (userControl.FindControl(labelName) as Label).Text;
            if (txtNumericValue != "")
            {
                txtNumericValue=txtNumericValue.Replace("$","");
                txtNumericValue = txtNumericValue.Replace(",", "");
                if (txtNumericValue == "")
                    txtNumericValue = "0";
                returnval = Convert.ToDecimal(txtNumericValue);
            }


            return returnval;
        }

        protected ClsRenewalNotes checkforNotes(UserControl userControl)
        {
            //If Notes have been entered, return true so notes can be saved
            ClsRenewalNotes note = new ClsRenewalNotes();
            TextBox txtNotes = userControl.FindControl("txtNotes") as TextBox;
            note.Note = txtNotes.Text;
            //RadButton cbxIncludeNote = userControl.FindControl("cbxIncludeNote") as RadButton;
            //note.includeFlag = cbxIncludeNote.Checked;
            RadioButtonList rblist1 = userControl.FindControl("rblist1") as RadioButtonList;
            note.NoteType = rblist1.SelectedValue.ToLower();
            note.CreatedBy = (string)Session["userName"];
            note.CreatedOn = Convert.ToDateTime(DateTime.Now);
            note.UpdatedBy = (string)Session["userName"];
            note.UpdatedOn = Convert.ToDateTime(DateTime.Now);
            note.ActiveFlag = true;
            return note;
        }

        

        protected void sendEmail(ClsContractRenewal objContractRenewal)
        {
            try
            {
                string subject = "Client Log Notification";
                string msgBody = composeEmail(objContractRenewal);

                ClsKeyValuePair kvp = new ClsKeyValuePair();
                string ccEmail = "Client Log Notification for " + kvp.GetKeyValue("ClientLogsCC").ToString();

                string host = ConfigurationManager.AppSettings["host"].ToString();
                int port = int.Parse(ConfigurationManager.AppSettings["port"]);
                string userName = ConfigurationManager.AppSettings["userName"];
                string password = ConfigurationManager.AppSettings["password"];

                string fromEmail = ConfigurationManager.AppSettings["fromEmail"];

                ClsRenewalRouting routeto = ClsRenewalRouting.GetRenewalRoute(objContractRenewal.currentlyRoutedTo);
                string toEmail = routeto.RoutingEmail;
                
                SmtpClient client = new SmtpClient(host, port);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(userName, password);

                string errorMsg = "Error Sending Email";
                MailMessage message = new MailMessage(fromEmail, toEmail, subject, errorMsg);
                              

                message.Body = msgBody;

                client.Send(message);

                //Send Second email to the CC
                if (ccEmail != "")
                {

                    subject = "CC: " + subject;
                    MailMessage message2 = new MailMessage(fromEmail, ccEmail, subject, errorMsg);
                    
                    message2.Body = msgBody;

                    client.Send(message2);
                }
            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }

        }

        protected string composeEmail(ClsContractRenewal objContractRenewal)
        {
            string msgBody = "CLIENT LOG NOTIFICATION for ";

       

            //Customer info
             msgBody = msgBody + objContractRenewal.Relationship + " - " + objContractRenewal.RenewalType;
             if (objContractRenewal.modificationReason != null && objContractRenewal.modificationReason != "")
             {
                 msgBody = msgBody + "\nReason: " + objContractRenewal.modificationReason;
             }

             msgBody = msgBody + "\n\nCurrently Routed to: " + objContractRenewal.currentlyRoutedTo;

             msgBody = msgBody + "\n\nLast Updated By: " + objContractRenewal.UpdatedBy;
             msgBody = msgBody + "\nLast Updated On: " + objContractRenewal.UpdatedOn.ToString();
             

             msgBody = msgBody + "\n\nEffective Date: " + Convert.ToDateTime(objContractRenewal.effectiveDate).ToString("MM/dd/yyyy");
             msgBody = msgBody + "\nExpiry Date: " + Convert.ToDateTime(objContractRenewal.expiryDate).ToString("MM/dd/yyyy");
             List<ClsRenewalNotes> notesList = ClsRenewalNotes.GetContractRenewalNotes(objContractRenewal.idContractRenewal);
             if (notesList.Count > 0)
             {
                 string noteString = "";
                 noteString = "\n\nNotes:";
                 string notedate;
                 string notevalue = "";

                 foreach (ClsRenewalNotes note in notesList)
                 {
                     notedate = Convert.ToDateTime(note.CreatedOn).ToString("MM/dd/yyyy");
                     notevalue = note.Note;                     
                     noteString = noteString + "\n" + notedate + " - " + note.CreatedBy + " - " + notevalue;
                 }
                 
                 msgBody = msgBody + noteString;
             }

            return msgBody;
        }
    }
}