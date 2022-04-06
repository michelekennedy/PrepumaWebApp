<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClientInfoDetails.ascx.cs" Inherits="PrepumaWebApp.ClientInfoDetails" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
    <telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" RelativeTo="Element"
                               Position="MiddleRight" AutoTooltipify="true" ContentScrolling="Default" Width="150"
                               Height="10">
    </telerik:RadToolTipManager>


    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="cbxClientName">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="cbxContractName"  LoadingPanelID="RadAjaxLoadingPanel1e" />
                <telerik:AjaxUpdatedControl ControlID="cbxAccountNumber"  LoadingPanelID="RadAjaxLoadingPanel1e" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cbxContractName">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="cbxAccountNumber"  LoadingPanelID="RadAjaxLoadingPanel1e" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="StartDate">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="StartDate"  LoadingPanelID="RadAjaxLoadingPanel1e" />
                <telerik:AjaxUpdatedControl ControlID="EndDate"  LoadingPanelID="RadAjaxLoadingPanel1e" />
                <telerik:AjaxUpdatedControl ControlID="RolloffQuarter"  LoadingPanelID="RadAjaxLoadingPanel1e" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1e" runat="server" />
  <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="40" >
    <asp:Image ID="imgLoading" Style="margin-top: 90px" runat="server" ImageUrl="~/Images/Loading640.gif" height="250px"    
        BorderWidth="0px" AlternateText="Loading" />     
    </telerik:RadAjaxLoadingPanel>

<asp:Panel ID="Panel1" runat="server" GroupingText=''>
   
    <%--<span style="padding-left: 420px;">
        <asp:ImageButton id="btnImgReload" runat="server"
           AlternateText="Click here to Reload edit from When you Add new Item to the any Drop down list" Width="22px" Height="22px"
           ImageUrl="~/Images/reload.png" OnClick="btnImgReload_Click" />
    </span>--%>
 <table style="padding-top:2px;" border="0">
      <tr><td></td>
          <td style="color:blue;"><p></p><b>Relationship Name:</b><p></p></td>
          <td style="color:blue;" colspan="2"> 
               <asp:Label ID="lblRelNameHdr" runat="server" Text='<%# Bind("RelationshipName") %>'></asp:Label>
          </td>
          <td colspan="3"></td></tr>
       
         <tr>
            <td style="width: 40px; color:red;text-align:right">*</td>
            <td><asp:Label ID="labelContractNumber" runat="server" CssClass="rdfLabel" Text="Contract Number:" /></td>
            
            <td>

                <asp:HiddenField ID="hdContract" runat="server" Value='<%# Bind("ContractID") %>' />
                <telerik:RadComboBox ID="cbxContractName" runat="server"  DataTextField="contractInfo" DataValueField="ContractID" Filter="StartsWith" EmptyMessage="Select Contract"
                    AutoPostBack="true" OnSelectedIndexChanged="cbxContractName_SelectedIndexChanged" >
                </telerik:RadComboBox>
                 </td><td>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="salesInfo" runat="server" ControlToValidate="cbxContractName" ErrorMessage="*Required" style="color: red" ></asp:RequiredFieldValidator>
                  </td>
                  <td style="color:blue;"><b><i>Last UpDated On:</i></b>
                  </td>
                 
                     <td>
                         <asp:HiddenField ID="hdAcctUpdatedOn" runat="server" Value='<%# Bind("InfoUpdatedOn") %>' />                           
                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("InfoUpdatedOn") %>'></asp:Label>
                     </td>
                     <td>
                         <asp:ImageButton ID="ImageButton2" runat="server"
                             AlternateText="Click here to Reload edit from When you Add new Item to the any Drop down list" Width="22px" Height="22px"
                             ImageUrl="~/Images/reload.png" OnClick="btnImgReload_Click" />
                     </td>

        </tr>
        <tr>
             <td style="width: 40px; color:red;text-align:right">*</td>
            <td> <asp:Label ID="labelAccountNumber" runat="server" CssClass="rdfLabel" Text="Account Number:"></asp:Label></td>
             <td>
                <asp:HiddenField ID="hdAccount" runat="server" Value='<%# Bind("AcctID") %>' />
                 <telerik:RadComboBox ID="cbxAccountNumber" runat="server" DataTextField="Acctnbr" DataValueField="AcctID" Filter="StartsWith" EmptyMessage="Select Account">
                 </telerik:RadComboBox>
                 </td><td>
  
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="salesInfo" runat="server" ControlToValidate="cbxAccountNumber" ErrorMessage="*Required" style="color: red" ></asp:RequiredFieldValidator>
   
           </td>
             <td style="color:blue;"><b><i>Last UpDated By:</i></b></td>
           <td>
                       <asp:HiddenField ID="hdAcctUpdatedby" runat="server" Value='<%# Bind("InfoUpdatedby") %>' />
                      <asp:Label ID="Label2" runat="server" Text='<%# Bind("InfoUpdatedby") %>'></asp:Label></td>
            
                               <td> </td>
        </tr>
      <tr>
            <td style="width: 40px; color:red;text-align:right">*</td>
            <td style="width: 170px"><asp:Label ID="labelClientName" runat="server" CssClass="rdfLabel" Text="Account Name:" />
            </td>
            <td>
                <asp:HiddenField ID="hdClient" runat="server" Value='<%# Bind("ClientID") %>' />
                <telerik:RadComboBox ID="cbxClientName" runat="server"  DataTextField="ClientName" DataValueField="ClientID" Filter="StartsWith" EmptyMessage="Select Client" 
                    AutoPostBack="true"  OnSelectedIndexChanged="cbxClientName_SelectedIndexChanged" >
                   
                </telerik:RadComboBox>
                </td>
             <td style="width: 25px">
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="salesInfo" runat= "server" ControlToValidate="cbxClientName" ErrorMessage="*Required" style="color: red" ></asp:RequiredFieldValidator>
             </td>
            
        </tr>
     </table><p></p>
    <table>
        <tr><td></td><td colspan="3"><p></p><b><u>Contract Classification Data</u></b><p /></td><td colspan="3"></td></tr>
                   
            <tr>
              <td style="width: 40px; color:red;text-align:right">*</td>
                <td><asp:Label ID="labelContractRegion" runat="server" CssClass="rdfLabel" Text="Control Branch:"></asp:Label></td>
                        <td style="width:160px">
                            <asp:HiddenField ID="hdRegion" runat="server" Value='<%# Bind("Expr1") %>' />
                            <telerik:RadDropDownList ID="rddlConRegionEdit" runat="server" DataTextField="Airport" DataValueField="RegionID" DefaultMessage="Select Region" ToolTip="This REGION pertains to the <i>contract</i>"></telerik:RadDropDownList>
                </td>
                <td>
                    <asp:ImageButton ID="imgbtnConRegion" runat="server" AlternateText="Addnew Region" ImageUrl="~/Images/plus-icon.png" Height="20px" Width="20px"  OnClientClick="window.open('MaintenanceRegions.aspx');return false;" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="salesInfo" runat="server" ControlToValidate="rddlConRegionEdit" ErrorMessage="*Required" style="color: red" ></asp:RequiredFieldValidator>
               </td>    
                  <td >Lost Business?</td>
                        <td colspan="2" style="text-align:left">
                           <telerik:RadButton ID="lostbizCheckBox" runat="server" ToggleType="CheckBox" ButtonType="StandardButton" AutoPostBack="false" ToolTip= "Lost Business flag, check for true, uncheck for false" Checked='<%# Eval("LostBiz") == DBNull.Value ? false : Convert.ToBoolean(Eval("LostBiz")) %>'>
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="True" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                    <telerik:RadButtonToggleState Text="False" PrimaryIconCssClass="rbToggleCheckbox" />
                                 </ToggleStates>
                            </telerik:RadButton>
                  
                        </td>

            </tr>
            <tr>
                <td style="width: 40px; color:red;text-align:right">*</td>
                <td><asp:Label ID="labelTerritory" runat="server" CssClass="rdfLabel" Text="Lead Sales Rep:"></asp:Label></td>
                        <td>
                            <asp:HiddenField ID="hdTerritory" runat="server" Value='<%# Bind("Territory") %>' />
                            <telerik:RadDropDownList ID="rddlTerritoryEdit" runat="server" DefaultMessage="Select Territory" DataValueField="SalesRepID" DataTextField="salesrep_id" ToolTip ="Enter the TERRITORY for the contract"></telerik:RadDropDownList>
                        </td>
                 <td>
                     <asp:ImageButton ID="imgbtnTerritory" runat="server" AlternateText="Addnew Territory" ImageUrl="~/Images/plus-icon.png" Height="20px" Width="20px" OnClientClick="window.open('MaintenanceSalesReps.aspx');return false;" />
                    
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="salesInfo" runat="server" ControlToValidate="rddlTerritoryEdit" ErrorMessage="*Required" style="color: red" ></asp:RequiredFieldValidator> 
                </td>        
                 <td>ISP Paid?</td>
                        <td colspan="2" style="text-align:left">
                            <telerik:RadButton ID="ispCheckBox" runat="server" ToggleType="CheckBox" ButtonType="StandardButton" AutoPostBack="false" ToolTip= "ISP Paid flag, check for true, uncheck for false" Checked='<%# Eval("ISPpaid") == DBNull.Value ? false : Convert.ToBoolean(Eval("ISPpaid")) %>'>
                                   <ToggleStates>
                                    <telerik:RadButtonToggleState Text="True" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                    <telerik:RadButtonToggleState Text="False" PrimaryIconCssClass="rbToggleCheckbox" />
                                 </ToggleStates>
                            </telerik:RadButton>
                        </td>

            </tr>
            <tr>
                <td style="width: 40px; color:red;text-align:right">*</td>
                <td><asp:Label ID="labelSalesType" runat="server" CssClass="rdfLabel" Text="Sales Type:"></asp:Label></td>
                        <td>
                            <asp:HiddenField ID="hdSalesType" runat="server" Value='<%# Bind("SalesType") %>' />
                            <telerik:RadDropDownList ID="rddlSalesTypeEdit" runat="server" DataValueField="SalesType" DataTextField="SalesType" DefaultMessage="Select SalesType" ToolTip="Enter the Sales Type for the contract"></telerik:RadDropDownList>
                       </td>
                 <td >
                     <asp:ImageButton ID="imgbtnSalesType" runat="server" AlternateText="addnew" ImageUrl="~/Images/plus-icon.png" Height="20px" Width="20px" OnClientClick="window.open('MaintenanceSalesType.aspx');return false;" />
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="salesInfo" runat="server" ControlToValidate="rddlSalesTypeEdit" ErrorMessage="*Required" style="color: red" ></asp:RequiredFieldValidator>
                 </td>
                 <td ><asp:Label ID="lblISP" runat="server" CssClass="rdfLabel" Text="ISP Rep:" /></td>
                 <td colspan="2" style="text-align:left">
                      <asp:HiddenField ID="hdISP" runat="server" Value='<%# Bind("ISP") %>' />
                       <telerik:RadDropDownList ID="rddISP" runat="server" DefaultMessage="Select ISP" DataValueField="SalesRepID" DataTextField="salesrep_id" ToolTip="Select the ISP"></telerik:RadDropDownList>
                 </td>
            </tr>
            

           <tr style="height:23px;">
                <td style="width: 40px; color:red;text-align:right">*</td>
                <td><asp:Label ID="lblRelationshipName" runat="server" CssClass="rdfLabel" Text="Relationship Name:" /></td>
                        <td>
                            <telerik:RadDropDownList ID="rddlRelationship" runat="server" DataValueField="RelationshipName" DataTextField="RelationshipName" DefaultMessage="Select Relationship" ToolTip="Enter the Relationship for the contract"></telerik:RadDropDownList>
                            <asp:HiddenField ID="hdRelationship" runat="server" Value='<%# Bind("RelationshipName") %>' />
                            <%--<telerik:RadTextBox ID="RelationshipTextBox" runat="server" MaxLength="50" Text='<%# Bind("RelationshipName") %>'  Width="160px" ToolTip="Enter the Relationship Name"/>--%>
                        </td>

                <td style="text-align:right">
                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="salesInfo" runat="server" ControlToValidate="RelationshipTextBox" ErrorMessage="*Required" style="color: red" ></asp:RequiredFieldValidator>--%>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="salesInfo" runat="server" ControlToValidate="rddlRelationship" ErrorMessage="*Required" style="color: red" ></asp:RequiredFieldValidator>
                </td>
               <td><asp:Label ID="lblSnaics" runat="server" CssClass="rdfLabel" MaxLength="15" Text="Industry Code: " /></td>
                <td>
                             <telerik:RadTextBox ID="snaicsTextBox" runat="server" Text='<%# Bind("SecondaryNAICS") %>'  Width="160px" ToolTip="Enter the Secondary NAICS"/>
                 </td>
            </tr>
               
          
           <tr>
                <td></td>
                <td>Estimated Annual Revenue:</td>
                        <td>
                             <telerik:RadNumericTextBox ID="EstAnnRevTextBoxN" runat="server"  DisplayText='<%# Bind("EstAnnualRev") %>'  Width="160px" ToolTip="Enter the Estimated Annual Rev"/>
                        </td>
                <td></td>
               <td ></td>
               <td><telerik:RadNumericTextBox ID="CCIDTextBoxN" runat ="server" DisplayText='<%# Bind("CCID") %>' NumberFormat-DecimalDigits="0" Width=" 160px" ToolTip="Enter the CCID" Visible="false" />
                    <%--<asp:HiddenField ID="hdCurrency" runat="server" Value='<%# Bind("Currency") %>' />--%>
                   <%--<telerik:RadComboBox ID="rddlCurrency" runat="server" EmptyMessage="Select Currency" ToolTip="Select Currency">
                        <Items>
                             <telerik:RadComboBoxItem runat="server" Text="USD" Value="USD" /> 
                             <telerik:RadComboBoxItem runat="server" Text="CAD" Value="CAD" /> 
                         </Items>
                   </telerik:RadComboBox>--%>
               </td>
            </tr>
            <tr>
                <td></td>
                <td>Estimated Margin Percent:</td>
                        <td>
                             <telerik:RadNumericTextBox ID="EstMarPctTextBoxN" runat="server" Text='<%# Bind("EstMarginPct") %>'  Width="160px" ToolTip="Enter the Estimated Margin %"/>
                        </td>
             <td></td>
            <td colspan="3"></td>
            </tr>

   


        <tr><td></td><td colspan="3"><p></p><b><u>Contract Account Classification Data</u></b><p></p></td><td colspan="3"></td></tr>
               
            <tr>
                <td style="width: 40px; color:red;text-align:right">*</td>
                <td><asp:Label ID="labelBusinessType" runat="server" CssClass="rdfLabel" Text="Business Type:"></asp:Label></td>
                        <td>
                            <asp:HiddenField ID="hdBusinessType" runat="server" Value='<%# Bind("BusinessType") %>' />
                            <telerik:RadDropDownList ID="rddlBusinesstypeEdit" runat="server" DefaultMessage="Select BusinessType" DataValueField="BusinessType" DataTextField="BusinessType" 
                                ToolTip="Enter the Business Type for this account" ></telerik:RadDropDownList>
                        </td>
                 <td>
                    <asp:ImageButton ID="imgbtnBizType" runat="server" AlternateText="addnew" ImageUrl="~/Images/plus-icon.png" Height="20px" Width="20px" OnClientClick="window.open('MaintenanceBizType.aspx');return false;" />
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="salesInfo" runat="server" ControlToValidate="rddlBusinesstypeEdit" ErrorMessage="*Required" style="color: red" ></asp:RequiredFieldValidator>
                 
                 </td>
                <td >
                    Start Date:
                </td>
                 <td >
                     <asp:HiddenField ID="hdStartDate" runat="server" Value='<%# Bind("StartDate") %>' />
                   <telerik:RadDatePicker RenderMode="Lightweight" ID="StartDate" runat="server" OnSelectedDateChanged="StartDate_SelectedDateChanged" AutoPostBack="true" Enabled="true">
                       
                   </telerik:RadDatePicker>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>Field Rep:</td>
                        <td>
                            <asp:HiddenField ID="hdLocalSRID" runat="server" Value='<%# Bind("LocalSRID") %>' />
                            <telerik:RadDropDownList ID="rddlLocalSRIDEdit" runat="server" DefaultMessage="Select Local SRID" DataValueField="SalesRepID" DataTextField="salesrep_id" ToolTip="Enter the Local SRID for this account"></telerik:RadDropDownList>
                        </td>
                 <td>
                     <asp:ImageButton ID="imgbtnLocalSrid" runat="server" AlternateText="addnew" ImageUrl="~/Images/plus-icon.png" Height="20px" Width="20px" OnClientClick="window.open('MaintenanceSalesReps.aspx');return false;" />
                 </td>

                 <td>End Date: </td>
                <td>
                        <asp:HiddenField ID="hdEndDate" runat="server" Value='<%# Bind("EndDate") %>' />
                        <telerik:RadDatePicker RenderMode="Lightweight" ID="EndDate" runat="server" Enabled="false">                                
                        </telerik:RadDatePicker>
                </td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td>Strategic Rep:</td>
                        <td>
                            <asp:HiddenField ID="hdStrategicSRID" runat="server" Value='<%# Bind("StrategicSRID") %>' />
                            <telerik:RadDropDownList ID="rddlStrategicSRIDEdit" runat="server" DefaultMessage="Select StrategicSRID" DataValueField="SalesRepID" DataTextField="salesrep_id" ToolTip="Enter the Strategic SRID for this account"></telerik:RadDropDownList>
                        </td>
                <td>
                    <asp:ImageButton ID="imgbtnStratSrid" runat="server" AlternateText="addnew" ImageUrl="~/Images/plus-icon.png" Height="20px" Width="20px" OnClientClick="window.open('MaintenanceSalesReps.aspx');return false;"  />
                    </td>
                <td>Rolloff Date:</td>
                <td>
                      <asp:HiddenField ID="hdRolloffQtr" runat="server" Value='<%# Bind("rolloff_quarter") %>' />
                      <telerik:RadDatePicker RenderMode="Lightweight" ID="RolloffQuarter" runat="server" Enabled="false" >
                       </telerik:RadDatePicker>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                        <td>
                              </td>
                 <td>
                          </td>
                 <td></td>
                <td colspan="3"></td>
            </tr>
            <tr>
                <td style="width: 40px; color:red;text-align:right">*</td>
                <td><asp:Label ID="labelAcctRegion" runat="server" CssClass="rdfLabel" Text="Branch:"></asp:Label></td>
                        <td>
                            <asp:HiddenField ID="hdAccRegion" runat="server" Value='<%# Bind("Region") %>' />
                            <telerik:RadDropDownList ID="rddlAccRegionEdit" runat="server" DefaultMessage="Select Region" DataValueField="Airport" DataTextField="Airport" ToolTip="This REGION pertains to the <i>account</i>"></telerik:RadDropDownList>
                        </td>
                <td >
                    <asp:ImageButton ID="imgbtnAccRegion" runat="server" AlternateText="addnew" ImageUrl="~/Images/plus-icon.png" Height="20px" Width="20px" OnClientClick="window.open('MaintenanceRegions.aspx');return false;" />
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="salesInfo" runat="server" ControlToValidate="rddlAccRegionEdit" ErrorMessage="*Required" style="color: red" ></asp:RequiredFieldValidator>
                </td>
                 <td >Product Type:</td>  
                <td>
                     <asp:HiddenField ID="hdProductType" runat="server" Value='<%# Bind("ProductType") %>' />
                            <telerik:RadDropDownList ID="rddlProductTypeEdit" runat="server" DefaultMessage="Select Product Type" DataValueField="ProductType" DataTextField="ProductType" ToolTip="Enter the Product Type for this account"></telerik:RadDropDownList>
                       
                </td>
                <td>
                     <asp:ImageButton ID="imgbtnProdType" runat="server" AlternateText="addnew" ImageUrl="~/Images/plus-icon.png" Height="20px" Width="20px" OnClientClick="window.open('MaintenanceProductType.aspx');return false;"  />
          
                </td>
            </tr>
            <tr>
                <td></td>
                <td>Shipping Location:</td>
                        <td>
                            <asp:HiddenField ID="hdShippingLocation" runat="server" Value='<%# Bind("ShippingLocation") %>' />
                            <telerik:RadDropDownList ID="rddlShippingLocationEdit" runat="server" DefaultMessage="Select ShippingLocation" DataValueField="Airport" DataTextField="Airport" ToolTip="Enter the Shipping Location for this <i>account</i>"></telerik:RadDropDownList>
                        </td>
                <td>
                    <asp:ImageButton ID="imgbtnShippingLoc" runat="server" AlternateText="addnew" ImageUrl="~/Images/plus-icon.png" Height="20px" Width="20px" OnClientClick="window.open('MaintenanceRegions.aspx');return false;"  />
                </td>

                <td><asp:Label ID="labelMATHANCPPID" runat="server" CssClass="rdfLabel" Text="Material Handling: "></asp:Label></td>
                <td>  <asp:HiddenField ID="hdMATHANDCPPID" runat="server" Value='<%# Bind("MATHANCPPID") %>' />
                            <telerik:RadDropDownList ID="rddlMATHANCPPIDEdit" runat="server" DataValueField="CPPMATHANID" DataTextField="mathancppId" DefaultMessage="Select Material Handling" ToolTip="Enter the Material Handling for the contract"></telerik:RadDropDownList>
                      </td>
                <td><asp:ImageButton ID="imgbtnMaterialhandling" runat="server" AlternateText="addnew" ImageUrl="~/Images/plus-icon.png" Height="20px" Width="20px" OnClientClick="window.open('MaintenanceMaterialHanlng.aspx');return false;"  />
      </td>
            </tr>
        
        <tr>
                <%--Adding 4 new fields to the Caontract Classification table  --%>
                <td></td>
                <td>
                    <asp:Label ID="labelGatewayID" runat="server" Text="Gateway"></asp:Label>
                </td>
                <td>
                            <asp:HiddenField ID="hdGatewayID" runat="server" Value='<%# Bind("gatewayID") %>' />
                            <telerik:RadDropDownList ID="rddlGatewayIDEdit" runat="server" DataValueField="gatewayID" DataTextField="id_gateway" DefaultMessage="Select gateway" ToolTip="Enter the Gateway for the contract"></telerik:RadDropDownList>
                        </td>
                <td>
                    <asp:ImageButton ID="imgbtnGateway" runat="server" AlternateText="addnew" ImageUrl="~/Images/plus-icon.png" Height="20px" Width="20px" OnClientClick="window.open('MaintenanceGateway.aspx');return false;" />
                </td>
                <td> <asp:Label ID="labelRTNSID" runat="server" Text="Returns"></asp:Label></td>
            <td><asp:HiddenField ID="hdRTNSID" runat="server" Value='<%# Bind("RTNSID") %>' />
                            <telerik:RadDropDownList ID="rddlRTNSIDEdit" runat="server"  DataTextField="rtnsIdInfo" DataValueField="RTNSID" DefaultMessage="Select Returns" ToolTip="Enter the Returns for the contract"></telerik:RadDropDownList>
            </td>
            <td > <asp:ImageButton ID="imgbtnReturns" runat="server" AlternateText="addnew" ImageUrl="~/Images/plus-icon.png" Height="20px" Width="20px" OnClientClick="window.open('MaintenanceReturns.aspx');return false;"  />
</td>
            </tr>
           
         
            <tr>
            <td></td>
            <td><asp:Label ID="labelCDCPLB" runat="server" CssClass="rdfLabel" Text="Cross Docking Fees"></asp:Label></td>
            <td>
                            <asp:HiddenField ID="hdCDCPLB" runat="server" Value='<%# Bind("CDCPLBID") %>' />
                            <telerik:RadDropDownList ID="rddlCDCPLBEdit" runat="server" DataValueField="CDCPLBID" DataTextField="id_crsdockfee" DefaultMessage="Select Cross Docking Fees" ToolTip="Enter the Cross Docking Fees for the contract"></telerik:RadDropDownList>
                        </td>
                <td>
                  <asp:ImageButton ID="imgbtnCrossDocking" runat="server" AlternateText="addnew" ImageUrl="~/Images/plus-icon.png" Height="20px" Width="20px"  OnClientClick="window.open('MaintenanceCrossDock.aspx');return false;"  />
              </td>
                  <td>Share Percent:</td>
                <td><telerik:RadNumericTextBox ID="SharePctTextBoxN" runat="server" Text='<%# Bind("SharePercent") %>'  Width="160px" ToolTip="Enter the Share Percent"/></td>
                <td ></td>
        </tr>
         <tr>
                <td></td>
                <td></td>
                        <td>
                             
                            
                        </td>
              <td></td>
             <td colspan="3"></td>
            </tr>
      
           

       
        <tr>
                <td></td>
                <td></td>
                <td>
                    <p></p>
                    <asp:Button ID="btnUpdate" causesValidation ="true" ValidationGroup="salesInfo" CssClass="btn btn-primary" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>' />
                    &nbsp;
                    <asp:Button ID="btnCancel" CssClass="btn btn-primary" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" /><p></p>
                </td>
                 <td></td>
                 <td colspan="3"></td>
            </tr>
       
        
     </table>
     <div style="padding-left:10px">
        <%--<p style="color:blue"><asp:Label ID="lblLastUpdatedby" runat="server" Font-Italic="true" ForeColor="gray"></asp:Label></p>--%>
                <p style="color:red"><i>*Required Fields</i></p>
                <asp:Label ID="lblErrorMessage" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblSuccessMessage" runat="server" Visible="false" Font-Bold="true" ForeColor="Green"></asp:Label>
    </div>
<style>
    div.right {
            float:right;
            height: 60px;
            width: 260px;
            font-style:italic;
            font-size:small;
            color:blue;
        }
</style>
  
</asp:Panel>
