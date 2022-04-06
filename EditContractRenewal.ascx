<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditContractRenewal.ascx.cs" Inherits="PrepumaWebApp.EditContractRenewal" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
       <script type="text/javascript">
           function submitCallBackFn(arg) {
               window.location.href = "Home.aspx";
           }
           function profileCallBackFn(arg) {
           }
           function notesCallBackFn(arg) {
           }
           function onRequestStart(sender, args) {
               if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                       args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                       args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {
                   args.set_enableAjax(false);
               }
           }
           function OpenWin(path) {
               //window.open(path, 'null', 'scrollbars =1,width=900,height=800,top=200,left=600');
               window.open(path);
               return false;
           }
           var $ = $telerik.$;

           function onClientFileUploaded(radAsyncUpload, args) {
               var $row = $(args.get_row());
               var inputName = radAsyncUpload.getAdditionalFieldID("TextBox");
               var inputType = "text";
               var inputID = inputName;
               var input = createInput(inputType, inputID, inputName);
               var label = createLabel(inputID);
               $row.append("<br/>");
               $row.append(label);
               $row.append(input);
           }

           function createInput(inputType, inputID, inputName) {
               var input = '<input type="' + inputType + '" id="' + inputID + '" name="' + inputName + '" />';
               return input;
           }

           function createLabel(forArrt) {
               var label = '<label for=' + forArrt + '>Enter Description: </label>';
               return label;
           }
        </script>
      </telerik:RadCodeBlock>
<telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" RelativeTo="Element"
                               Position="MiddleRight" AutoTooltipify="true" ContentScrolling="Default" Width="150"
                               Height="10">
    </telerik:RadToolTipManager>
<telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="cbxRenewalStatus">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="cbxRenewalStatus" />
            </UpdatedControls>
        </telerik:AjaxSetting>     
              
        <telerik:AjaxSetting AjaxControlID="rdpEffectiveDate">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rdpExpiryDate"  />
            </UpdatedControls>
        </telerik:AjaxSetting>

        <telerik:AjaxSetting AjaxControlID="btnSaveUpload">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="btnSaveUpload"  />
            </UpdatedControls>
        </telerik:AjaxSetting>
        
         <telerik:AjaxSetting AjaxControlID="cbxperc1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="cbxperc1"  />
            </UpdatedControls>
        </telerik:AjaxSetting>

       <%-- <telerik:AjaxSetting AjaxControlID="radioFuel">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtcurFuelPIS"  />
                 <telerik:AjaxUpdatedControl ControlID="txtexpFuelPIS"  />
                 <telerik:AjaxUpdatedControl ControlID="txtcurFuelPuroS"  />
                 <telerik:AjaxUpdatedControl ControlID="txtexpFuelPuroS"  />
            </UpdatedControls>
        </telerik:AjaxSetting>--%>
       
    </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
  <telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
<asp:Panel ID="Panel1" runat="server" GroupingText=''>
   
   <table border="0" >
         <tr>
             <td style="height: 10px" colspan="5"></td>
         </tr>
            <tr>
                <td style="width: 10px;"></td>
                <td style="width: 500px; color:blue">
                    <b><asp:Label ID="lblContractDetails" runat="server" Text='Contract Details'  Enabled="false" Visible="true"></asp:Label> </b>
                    <asp:HiddenField ID="ContractRenewalID" runat="server" Value='<%# Bind("idContractRenewal") %>' />
                </td>
                <td style="text-align:right; color:blue; font-style:italic">
                     <asp:Label ID="lblLastUpdated" runat="server" Text='Last Updated By '  Enabled="false" Visible="true"></asp:Label>
                </td>
                <td style="width: 10px;"></td>
               <td style="text-align:right; color:blue; font-style:italic">
                      <asp:Label ID="LastUpdatedBy" CssClass="alert-link" runat="server" Text='<%# Bind("UpdatedBy") %>' Visible="true"></asp:Label>

                </td>
            </tr>
         <tr>
             <td></td>
             <td style="color:forestgreen;"> <b> <asp:Label ID="lblcurrentroute" CssClass="alert-link" runat="server" Text='Currently Routed To' Visible="false" />&nbsp;<asp:Label ID="lblAssignedTo" CssClass="alert-link" runat="server" Text='' Visible="false"></asp:Label></b></td>
             <td style="text-align:right; color:blue; font-style:italic">
                 <asp:Label ID="lblLastUpdatedOn" runat="server" Text='Last Updated On '  Enabled="false" Visible="true"></asp:Label>
             </td>
             <td></td>
             <td style="text-align:right; color:blue; font-style:italic">
                    
                 <asp:Label ID="LastUpdatedOn" CssClass="alert-link" runat="server" Text='<%# Bind("UpdatedOn") %>' Visible="true"></asp:Label>

              </td>
         </tr>
      
        </table>
    <hr />
    <table border="0">
        
        <tr>
            <td style="width: 30px; color:red; text-align:right " >*</td>
             <td style="width: 100px;text-align:left"><asp:Label ID="lblRenewalRelationship" runat="server" CssClass="rdfLabel" Text="Relationship" /></td>
            <td>
                  <telerik:RadComboBox ID="cbxRelationshipName" runat="server" EmptyMessage="Select Relationship" Width="350px" AllowCustomText="true" AutoPostBack="true" DataTextField="RelationshipName" DataValueField="RelationshipName" Filter="StartsWith" OnSelectedIndexChanged="cbxRelationshipName_SelectedIndexChanged" >
                </telerik:RadComboBox>
                 <asp:HiddenField ID="hdRelationship" runat="server" Value='<%# Bind("Relationship") %>' /> 
                 <asp:RequiredFieldValidator runat="server" ValidationGroup="Submitgroup" ControlToValidate="cbxRelationshipName" ErrorMessage=" *Required" style="color: red"></asp:RequiredFieldValidator>
                <%--<telerik:RadComboBox ID="cbxRenewalTypes" runat="server" ToolTip="Select Renewal Type" Width="200px" EmptyMessage="Select Renewal Type" AutoPostBack="true" OnSelectedIndexChanged="cbxRenewalTypes_SelectedIndexChanged"></telerik:RadComboBox>
                <asp:HiddenField ID="hdnRewewalType" runat="server" Value='<%# Bind("idContractRenewalType") %>' />
                 <asp:RequiredFieldValidator runat="server" ValidationGroup="Submitgroup" ControlToValidate="cbxRenewalTypes" ErrorMessage=" *Required" style="color: red"></asp:RequiredFieldValidator>--%>
            </td>
            <td style="width: 30px; color:red; text-align:right " >*</td>
            <td style="width: 80px"><b>Effective Date</b></td>
            <td style="width: 80px"><b>Expiry Date</b></td>
        </tr>

        <tr>
             <td style="width: 30px; color:red; text-align:right " >*</td>
           <td ><%--<asp:Label ID="lblRenewalRelationship" runat="server" CssClass="rdfLabel" Text="Relationship" />--%>Contract#</td>
             <td>
                  <telerik:RadComboBox ID="cbxContracts" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" CheckedItemsTexts="DisplayAllInInput" runat="server" ToolTip="Select Contracts " Width="350px" EmptyMessage="Select Contract" AutoPostBack="true" OnSelectedIndexChanged="cbxContracts_SelectedIndexChanged"></telerik:RadComboBox>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Submitgroup" ControlToValidate="cbxContracts" ErrorMessage=" *Required" style="color: red"></asp:RequiredFieldValidator>
              <%-- <telerik:RadComboBox ID="cbxRelationshipName" runat="server" EmptyMessage="Select Relationship" Width="350px" AllowCustomText="true" AutoPostBack="true" DataTextField="RelationshipName" DataValueField="RelationshipName" Filter="StartsWith" OnSelectedIndexChanged="cbxRelationshipName_SelectedIndexChanged" >
                </telerik:RadComboBox>
                 <asp:HiddenField ID="hdRelationship" runat="server" Value='<%# Bind("Relationship") %>' /> 
                 <asp:RequiredFieldValidator runat="server" ValidationGroup="Submitgroup" ControlToValidate="cbxRelationshipName" ErrorMessage=" *Required" style="color: red"></asp:RequiredFieldValidator>--%>
            </td>
            <td></td>
            <td><telerik:RadDatePicker ID="rdpEffectiveDate" runat="server" OnSelectedDateChanged="date1_Changed" AutoPostBack="true"> </telerik:RadDatePicker>
               
            </td>
            <td><telerik:RadDatePicker ID="rdpExpiryDate" runat="server" > </telerik:RadDatePicker>
               
            </td>
        </tr>


        
          <tr>
               <td style="width: 30px; color:red; text-align:right " ></td>
                 <td></td>
                <td>
                    Branch: <asp:Label ID="lblBranch" runat="server"   ></asp:Label> 
                    SRID: <asp:Label ID="lblSRID" runat="server"  ></asp:Label> &nbsp; <asp:Label ID="lblslsName" runat="server"  ></asp:Label>
                     <%--<telerik:RadComboBox ID="cbxContracts" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" CheckedItemsTexts="DisplayAllInInput" runat="server" ToolTip="Select Contracts " Width="350px" EmptyMessage="Select Contract" AutoPostBack="true" OnSelectedIndexChanged="cbxContracts_SelectedIndexChanged"></telerik:RadComboBox>
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="Submitgroup" ControlToValidate="cbxContracts" ErrorMessage=" *Required" style="color: red"></asp:RequiredFieldValidator>--%>
                </td>
              <td></td>
             
                <td >
                  <asp:RequiredFieldValidator runat="server" ValidationGroup="Submitgroup" ControlToValidate="rdpEffectiveDate" ErrorMessage="Effective Date required" style="color: red"></asp:RequiredFieldValidator>
                 </td><td>
                  <asp:RequiredFieldValidator runat="server" ValidationGroup="Submitgroup" ControlToValidate="rdpExpiryDate" ErrorMessage="Expiry Date required" style="color: red"></asp:RequiredFieldValidator>
             </td>
          </tr>

     
         <tr>
            <td style="width: 30px; color:red; text-align:right " >*</td>
            <td>Contract Type</td>
            <td style="text-align:left">
                <telerik:RadComboBox ID="cbxRenewalTypes" runat="server" ToolTip="Select Renewal Type" Width="200px" EmptyMessage="Select Renewal Type" AutoPostBack="true" OnSelectedIndexChanged="cbxRenewalTypes_SelectedIndexChanged"></telerik:RadComboBox>
                <asp:HiddenField ID="hdnRewewalType" runat="server" Value='<%# Bind("idContractRenewalType") %>' />
                 <asp:RequiredFieldValidator runat="server" ValidationGroup="Submitgroup" ControlToValidate="cbxRenewalTypes" ErrorMessage=" *Required" style="color: red"></asp:RequiredFieldValidator>
               <%-- Branch: <asp:Label ID="lblBranch" runat="server"   ></asp:Label> 
                SRID: <asp:Label ID="lblSRID" runat="server"  ></asp:Label> &nbsp; <asp:Label ID="lblslsName" runat="server"  ></asp:Label>--%>
            </td>
            <td></td>
             <td colspan="2">
                    <asp:Label ID="lblFSDl" runat="server" Text="<b>First Ship Date: </b>" /><asp:Label ID="lblFSD" runat="server" CssClass="rdfLabel" Text="" />
               </td>

        </tr>
       
       
         <tr>
            <td></td>
            <td><asp:Label ID="lblReason" runat="server" Text="Reason" Visible="true"/></td>
            <td colspan="4"> <asp:TextBox id="txtModReason" TextMode="multiline" Columns="120" Rows="1" runat="server"  Text='<%# Bind("modificationReason") %>' Visible="true" />
            </td>
        </tr> 
      

        
        </table>
    <p></p>

    <!-- Tabs -->
      <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="RadTabStrip1"  MultiPageID="RadMultiPage1" SelectedIndex="0" Visible="true">
            <Tabs>
                <telerik:RadTab runat="server" Text="Pricing Information" Width="190px" PageViewID="pricing" Enabled="false"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="Accessorials" Width="190px" PageViewID="accessorials" Enabled="false"></telerik:RadTab> 
                <telerik:RadTab runat="server" Text="Notes/Approvals" Width="190px" PageViewID="notes" Enabled="false"></telerik:RadTab>  
                <telerik:RadTab runat="server" Text="File Attachments" Width="190px" PageViewID="uploads" Enabled="false"></telerik:RadTab>             
            </Tabs>
      </telerik:RadTabStrip>

    <!-- TAB ONE - PRICING -->
   
        
     <telerik:RadMultiPage runat="server" ID="RadMultiPage1"  SelectedIndex="0">
         <telerik:RadPageView runat="server" ID="pricing">
 <asp:Panel ID="Panel2" runat="server" DefaultButton = "button1">
    <table border ="0">
         <tr>
            
             <td colspan="11"><hr /></td>
            

         </tr>
        <tr>
            <td colspan="6"></td>
            <td style="text-align:center">
               <%-- <telerik:RadButton ID="btnDefaultr" runat="server" OnClick="btnDefault_Click" Text="Update"  Visible="false" Enabled="true" /> --%>
                <telerik:RadButton ID="btnGetRevenue" runat="server" OnClick="btnGetRevenue_Click" Text="Get Revenue"  Visible="true"/>
            </td>
            <td colspan="4"><i><asp:Label ID="lblGetRevenue" runat="server" Text="Revenue from Margin " Visible="true" /></i>&nbsp;<asp:Label ID="lbldaterange" runat="server" Text="" Visible="true" /></td>
        </tr>
        <tr>
            <td style="width: 50px; " ></td>
            <td colspan="2" style="text-align:right">
               <asp:Label ID="lblsowFlag" runat="server" Text="Share of Wallet Gains" Visible="true" />

                <telerik:RadButton ID="sowFlag" runat="server" ToggleType="CheckBox" ButtonType="LinkButton" Checked='<%# Eval("sowFlag") == DBNull.Value ? false : Convert.ToBoolean(Eval("sowFlag")) %>' AutoPostBack="true" OnCheckedChanged="sow_check_changed">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Yes" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox"/>

                    </ToggleStates>
                </telerik:RadButton>
            </td>
            <td style="width: 50px; "></td>
            <td style="width: 80px"><b>Annualized:</b></td>
            <td></td>
            <td style="width:<%=tdcurwidth%>; text-align:center; color:blue;"><asp:Label ID="lblCurrent" runat="server" Text="Current" Visible="true" /></td>
            <td style="width:<%=tdsowwidth%>; text-align:center; color:blue;"><asp:Label ID="lblSOW" runat="server" Visible="false" Text="Share of Wallet" /></td>
            <td style="width:<%=tdrocwidth%>; text-align:center; color:blue;"><asp:Label ID="lblROC" runat="server" Visible="false" Text="Rate of Change" /></td>
            <td style="width:<%=tdexpwidth%>; text-align:center;color:blue;"><asp:Label ID="lblExpected" runat="server" Text="Increase" Visible="true" /></td>
            <td style="width:<%=tdnewwidth%>; text-align:center;color:blue;"><asp:Label ID="lblNewProfile" runat="server" Text="New Profile" Visible="true" /></td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2" style="text-align:right">
                <asp:Label ID="lblrocFlag" runat="server" Text=" Impact Rate of Change" Visible="true" />
               <telerik:RadButton ID="rocFlag" runat="server" ToggleType="CheckBox" ButtonType="LinkButton" Checked='<%# Eval("rateOfChangeFlag") == DBNull.Value ? false : Convert.ToBoolean(Eval("rateOfChangeFlag")) %>' OnCheckedChanged="roc_check_changed" AutoPostBack="true">
                    <ToggleStates>
                       <telerik:RadButtonToggleState Text="Yes" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox" />

                    </ToggleStates>
                </telerik:RadButton>
            </td>
           <td></td>
             <td>Margin%</td>
                        <td></td>
                        <td style="text-align:left">
                            <telerik:RadNumericTextBox ID="txtcurMarginN" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Type="Percent" NumberFormat-DecimalDigits="1" Width="100px" ToolTip="Enter the Current Margin Percent">
                              <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
                     
                        <td>
                              <telerik:RadNumericTextBox ID="txtsowMarginN" runat="server"  Visible="false" autopostback="true" RenderMode="Lightweight" MaxLength="50" Type="Percent" NumberFormat-DecimalDigits="1" Width="100px" ToolTip="Enter the Share of Wallet Margin Percent">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                              <telerik:RadNumericTextBox ID="txtrocMarginN" runat="server" Visible="false" autopostback="true" RenderMode="Lightweight" MaxLength="50" Type="Percent" NumberFormat-DecimalDigits="1" Width="100px" ToolTip="Enter the Rate of CHange Margin Percent">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>

                         <td style="text-align:right">
                            <telerik:RadNumericTextBox ID="txtexpMarginN" runat="server" Visible="false" RenderMode="Lightweight" autopostback="true" MaxLength="50" Type="Percent" NumberFormat-DecimalDigits="1" Width="100px" ToolTip="Enter the Expected Margin Percent">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>

                        <td style="text-align:right">
                            <telerik:RadNumericTextBox ID="txtnewMarginN" runat="server" RenderMode="Lightweight" autopostback="true" MaxLength="50" Type="Percent" NumberFormat-DecimalDigits="1" Width="100px" ToolTip="Enter the New Margin Percent">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
        </tr>
        <tr>
            <td></td>
            <td><b><asp:Label ID="lblPctIncreases" runat="server" Text="% Increases:" Visible="true" /></b>
                 <asp:Label ID="lblNewContract1" runat="server" Text="Annualized Yearly" Visible="false" /> 
            </td>
            <td>               
            </td>
            <td></td>        
             <td>Revenue</td>
                        <td></td>
                        <td style="text-align:right; color:blue;">
                            <%--<telerik:RadNumericTextBox ID="txtcurRevenueN" runat="server" autopostback="true" RenderMode="Lightweight" Type="Currency" NumberFormat-DecimalDigits="0" Width="100px" >
                            <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>--%>
                            <asp:Label ID="lblcurRevenue" runat="server" Text="" Visible="true" />
                        </td>
                        <td style="text-align:right; color:blue;">
                            <%--<telerik:RadNumericTextBox ID="txtsowRevenueN" runat="server" autopostback="true" Visible="false" RenderMode="Lightweight" Type="Currency" NumberFormat-DecimalDigits="0" Width="100px" >
                            <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>--%>
                             <asp:Label ID="lblsowRevenue" runat="server" Text="" Visible="true" />
                        </td>
                       <td style="text-align:right; color:blue;">
                           <%--<telerik:RadNumericTextBox ID="txtrocRevenueN" runat="server"  autopostback="true" Visible="false" RenderMode="Lightweight" Type="Currency" NumberFormat-DecimalDigits="0" Width="100px" >
                            <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>--%>
                            <asp:Label ID="lblrocRevenue" runat="server" Text="" Visible="true" />
                       </td>
                        <td style="text-align:right; color:blue;">
                           <%-- <telerik:RadNumericTextBox ID="txtexpRevenueN" runat="server"  autopostback="true" RenderMode="Lightweight" Type="Currency" NumberFormat-DecimalDigits="0"  MaxLength="50" Width="100px" ToolTip="Enter the Expected Revenue">
                                 <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>--%>
                           <asp:Label ID="lblexpRevenue" runat="server" Text="" Visible="true" />
                        </td>

                       <td style="text-align:right; color:blue;">
                           <%-- <telerik:RadNumericTextBox ID="txtnewRevenueN" runat="server"  autopostback="true" RenderMode="Lightweight" Type="Currency" NumberFormat-DecimalDigits="0"  MaxLength="50" Width="100px" ToolTip="Enter the New Revenue">
                                 <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>--%>
                            <asp:Label ID="lblnewRevenue" runat="server" Text="" Visible="true" />
                        </td>
        </tr>
        <tr>
           
             <td></td>
            <td>
                <asp:Label ID="lblpctIncCourier" runat="server" Text="Courier" Visible="true" />
                <asp:Label ID="lblNewContract" runat="server" Text="Commitment Per VDA" Visible="false" />
            </td>
            <td  style="text-align:right">
                <telerik:RadNumericTextBox ID="txtpctCourierN" runat="server" OnTextChanged="courierpct_TextChanged" autopostback="true" RenderMode="Lightweight" MaxLength="25" Type="Percent" NumberFormat-DecimalDigits="1" Width="50px" ToolTip="Enter the Courier Percent Increase">
                    <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
              
            </td>
            <td></td>

             <td>Courier</td>
                        <td></td>
                        <td style="text-align:left">
                            <telerik:RadNumericTextBox ID="txtcurCourierN" runat="server" OnTextChanged="courierpct_TextChanged"  autopostback="true" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Current Courier">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtsowCourierN" runat="server" OnTextChanged="btnDefault_Click" autopostback="true" Visible="false" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Share of Wallet Courier">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                             <telerik:RadNumericTextBox ID="txtrocCourierN" runat="server" OnTextChanged="btnDefault_Click"  autopostback="true" Visible="false" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Rate of Change Courier">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style="text-align:right">
                            <telerik:RadNumericTextBox ID="txtexpCourierN" runat="server" OnTextChanged="btnDefault_Click" autopostback="true" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Expected Courier">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>

                        <td style="text-align:right">
                            <%--<telerik:RadNumericTextBox ID="txtnewCourierN" runat="server" OnTextChanged="btnDefault_Click"  autopostback="true" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the New Courier">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>--%>
                            <asp:Label ID="lblnewCourier" runat="server" Text="" Visible="true" />
                        </td>
        </tr>
       <tr>
           <td></td>
            <td >
                <asp:Label ID="lblPctIncFF" runat="server" Text="Freight Fwd" Visible="true" />   
                  <telerik:RadNumericTextBox ID="txtVDACommitment" runat="server" RenderMode="Lightweight"  autopostback="true" MaxLength="25" Type="Currency" NumberFormat-DecimalDigits="0" Width="120px" ToolTip="Enter the Yearly Revenue Commitment" Visible="false">
                    <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>             
            </td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtpctFFN" runat="server" OnTextChanged="ffpct_TextChanged" autopostback="true" RenderMode="Lightweight" MaxLength="25" Type="Percent" NumberFormat-DecimalDigits="1" Width="50px" ToolTip="Enter the Freight Forward Percent Increase">
                    <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>               
            </td>
            <td></td>
           
              <td>Freight Fwd</td>
                        <td></td>
                       <td style="text-align:left">
                           <telerik:RadNumericTextBox ID="txtcurFFN" runat="server" OnTextChanged="ffpct_TextChanged"  autopostback="true" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Current Freight Fwd">
                               <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                           </telerik:RadNumericTextBox>
                       </td>
                        <td>
                             <telerik:RadNumericTextBox ID="txtsowFFN" runat="server" OnTextChanged="btnDefault_Click"  autopostback="true" Visible="false" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Share of Wallet Freight Fwd">
                               <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                           </telerik:RadNumericTextBox>
                        </td>
                        <td>
                             <telerik:RadNumericTextBox ID="txtrocFFN" runat="server" OnTextChanged="btnDefault_Click"  autopostback="true" Visible="false" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Rate of Change Freight Fwd">
                               <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                           </telerik:RadNumericTextBox>
                        </td>
                        <td style="text-align:right">
                            <telerik:RadNumericTextBox ID="txtexpFFN" runat="server" OnTextChanged="btnDefault_Click"  autopostback="true" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Expected Freight Fwd">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
                        
                        <td style="text-align:right">
                            <%--<telerik:RadNumericTextBox ID="txtnewFFN" runat="server" OnTextChanged="btnDefault_Click"  autopostback="true" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the New Freight Fwd">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>--%>
                            <asp:Label ID="lblnewFF" runat="server" Text="" Visible="true" />
                        </td>
        </tr>
        <tr>
             <td></td>
             <td >
                <asp:Label ID="lblpctIncLTL" runat="server" Text="LTL" Visible="true" />                
            </td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtpctLTLN" runat="server" OnTextChanged="ltlpct_TextChanged" autopostback="true" RenderMode="Lightweight" MaxLength="25" Type="Percent" NumberFormat-DecimalDigits="1" Width="50px" ToolTip="Enter the LTL Percent Increase">
                    <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>               
            </td>
            <td></td>
             <td>LTL</td>
                        <td></td>
                         <td style="text-align:left">
                             <telerik:RadNumericTextBox ID="txtcurLTLN" RenderMode="Lightweight" runat="server" OnTextChanged="ltlpct_TextChanged" autopostback="true" MaxLength="50" Width="100px"  Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Current LTL">
                                 <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                             </telerik:RadNumericTextBox>
                         </td>
                        <td>
                             <telerik:RadNumericTextBox ID="txtsowLTLN" RenderMode="Lightweight" runat="server" OnTextChanged="btnDefault_Click" autopostback="true" Visible="false" MaxLength="50" Width="100px"  Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Share of Wallet LTL">
                                 <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                             </telerik:RadNumericTextBox>
                        </td>
                        <td>
                             <telerik:RadNumericTextBox ID="txtrocLTLN" RenderMode="Lightweight" runat="server" OnTextChanged="btnDefault_Click" autopostback="true" Visible="false" MaxLength="50" Width="100px"  Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Rate of Change LTL">
                                 <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                             </telerik:RadNumericTextBox>
                        </td>
                        <td style="text-align:right">
                            <telerik:RadNumericTextBox ID="txtexpLTLN" RenderMode="Lightweight" runat="server" OnTextChanged="btnDefault_Click" autopostback="true" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Expected LTL">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
                        
                        <td style="text-align:right">
                            <%--<telerik:RadNumericTextBox ID="txtnewLTLN" RenderMode="Lightweight" runat="server" OnTextChanged="btnDefault_Click" autopostback="true" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the New LTL">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>--%>
                            <asp:Label ID="lblnewLTL" runat="server" Text="" Visible="true" />
                        </td>
        </tr>

            <tr>
              <td></td>
           <td >
                <asp:Label ID="lblpctIncPPST" runat="server" Text="PPST" Visible="true" />                
            </td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtpctPPSTN" runat="server" OnTextChanged="ppstpct_TextChanged" autopostback="true" RenderMode="Lightweight" MaxLength="25" Type="Percent" NumberFormat-DecimalDigits="1" Width="50px" ToolTip="Enter the PPST Percent Increase">
                    <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>               
            </td>
            <td></td>
           
              <td>PPST</td>
                        <td></td>
                       <td style="text-align:left">
                           <telerik:RadNumericTextBox ID="txtcurPPSTN" runat="server" OnTextChanged="ppstpct_TextChanged" AutoPostBack="true" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Current PuroPost">
                               <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                           </telerik:RadNumericTextBox>
                       </td>
                        <td>
                             <telerik:RadNumericTextBox ID="txtsowPPSTN" runat="server" OnTextChanged="btnDefault_Click"  autopostback="true" Visible="false" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Share of Wallet PuroPost">
                               <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                           </telerik:RadNumericTextBox>
                        </td>
                        <td>
                             <telerik:RadNumericTextBox ID="txtrocPPSTN" runat="server" OnTextChanged="btnDefault_Click"  autopostback="true" Visible="false" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Rate of Change PuroPost">
                               <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                           </telerik:RadNumericTextBox>
                        </td>
                        <td style="text-align:right">
                            <telerik:RadNumericTextBox ID="txtexpPPSTN" runat="server"  OnTextChanged="btnDefault_Click" autopostback="true" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Expected PuroPost">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style="text-align:right">
                            <%--<telerik:RadNumericTextBox ID="txtnewPPSTN" runat="server"  OnTextChanged="btnDefault_Click" autopostback="true" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the New PuroPost">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>--%>
                            <asp:Label ID="lblnewPPST" runat="server" Text="" Visible="true" />
                        </td>
        </tr>

        <tr>
             <td></td>
            <td><asp:Label ID="lblPctIncCPC" runat="server" Text="CPC" Visible="true" /></td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtpctCPCN" runat="server" OnTextChanged="cpcpct_TextChanged" autopostback="true" RenderMode="Lightweight" MaxLength="25" Type="Percent" NumberFormat-DecimalDigits="1" Width="50px" ToolTip="Enter the CPC Percent Increase">
                         <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" />                 
                </telerik:RadNumericTextBox>
            </td>
            <td></td>
             <td>CPC</td>
                        <td></td>
                        <td style="text-align:left">
                            <telerik:RadNumericTextBox ID="txtcurCPCN" RenderMode="Lightweight" runat="server" OnTextChanged="cpcpct_TextChanged" autopostback="true" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0"  ToolTip="Enter the Current CPC">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
                         <td>
                              <telerik:RadNumericTextBox ID="txtsowCPCN" RenderMode="Lightweight" runat="server" OnTextChanged="btnDefault_Click"  autopostback="true" Visible="false" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0"  ToolTip="Enter the Shaore of Wallet CPC">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                         </td>
                        <td>
                             <telerik:RadNumericTextBox ID="txtrocCPCN" RenderMode="Lightweight" runat="server" OnTextChanged="btnDefault_Click" autopostback="true" Visible="false" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0"  ToolTip="Enter the Rate of Change CPC">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style="text-align:right">
                            <telerik:RadNumericTextBox ID="txtexpCPCN" OnTextChanged="btnDefault_Click" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0"  ToolTip="Enter the Expected CPC">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
                       
                        <td style="text-align:right">
                           <%-- <telerik:RadNumericTextBox ID="txtnewCPCN" RenderMode="Lightweight" runat="server" OnTextChanged="btnDefault_Click" autopostback="true" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0"  ToolTip="Enter the New CPC">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>--%>
                            <asp:Label ID="lblnewCPC" runat="server" Text="" Visible="true" />
                        </td>
        </tr>
     
              <tr>
              <td></td>
            <td >
                
                <asp:Button ID="AddOther" runat="server" OnClick="btnAddOther_Click" Text="Add Other"  Visible="true"/>
                <asp:Label ID="lblpctIncOther" runat="server" Text="Other" Visible="false" />                
            </td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtpctOtherN" runat="server" autopostback="true" RenderMode="Lightweight" MaxLength="25" Type="Percent" NumberFormat-DecimalDigits="1" Width="50px"  Visible="false" ToolTip="Enter the Other Percent Increase">
                    <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>               
            </td>
            <td></td>
           
              <td><asp:Label ID="lblAnOther" runat="server" Text="Other" Visible="false" /></td>
                        <td></td>
                       <td style="text-align:left">
                           <telerik:RadNumericTextBox ID="txtcurOtherN"  Visible="false" runat="server" OnTextChanged="otherpct_TextChanged" autopostback="true" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Current Value for Other">
                               <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                           </telerik:RadNumericTextBox>
                       </td>
                        <td>
                             <telerik:RadNumericTextBox ID="txtsowOtherN"  Visible="false" runat="server" OnTextChanged="btnDefault_Click" autopostback="true" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Share of Wallet Other">
                               <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                           </telerik:RadNumericTextBox>
                        </td>
                         <td>
                              <telerik:RadNumericTextBox ID="txtrocOtherN"  Visible="false" runat="server" OnTextChanged="btnDefault_Click" autopostback="true" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0" ToolTip="Enter the Rate of Change Other">
                               <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                           </telerik:RadNumericTextBox>
                         </td>
                        <td style="text-align:right">
                            <telerik:RadNumericTextBox ID="txtexpOtherN"  Visible="false" runat="server" OnTextChanged="btnDefault_Click" autopostback="true" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0"  ToolTip="Enter the Expected Other">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
                       
                        <td style="text-align:right">
                            <%--<telerik:RadNumericTextBox ID="txtnewOtherN"  Visible="false" runat="server" OnTextChanged="btnDefault_Click"  autopostback="true" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Currency" NumberFormat-DecimalDigits="0"  ToolTip="Enter the New Other">
                                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>--%>
                            <asp:Label ID="lblnewOther" runat="server" Text="" Visible="false" />
                        </td>
        </tr>
    
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>

            <td style="text-align:left">
                <asp:Label ID="lblOtherDesc" runat="server" Visible="false" Text="Other Desc" />
            </td>
            <td></td>
            <td colspan="5">
                <telerik:RadTextBox ID="txtOtherDesc" Width="325px" runat="server" autopostback="true" Visible= "false" ToolTip="Enter the Description for Other"></telerik:RadTextBox>
            </td>

        </tr>
        <tr>
            <td colspan="5" style="text-align:right">
                <b>Target Gross Margin%&nbsp;&nbsp;</b>
            </td>
            <td></td>
                        <td style="text-align:left">
                            <telerik:RadNumericTextBox ID="txtcurTargetGrMarginPctN" runat="server" OnTextChanged="btnDefault_Click" autopostback="true" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Percent" NumberFormat-DecimalDigits="1" ToolTip="Enter the Current Target Grorss Margin %">
                            <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtsowTargetGrMarginPctN" runat="server" OnTextChanged="btnDefault_Click" autopostback="true" Visible="false" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Percent" NumberFormat-DecimalDigits="1" ToolTip="Enter the Share of Wallet Target Grorss Margin %">
                            <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                             <telerik:RadNumericTextBox ID="txtrocTargetGrMarginPctN" runat="server" OnTextChanged="btnDefault_Click" autopostback="true" Visible="false" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Percent" NumberFormat-DecimalDigits="1" ToolTip="Enter the Rate of Change Target Grorss Margin %">
                            <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
                        <td style="text-align:right">
                            <telerik:RadNumericTextBox ID="txtexpTargetGrMarginPctN" runat="server" OnTextChanged="btnDefault_Click" autopostback="true" Visible="false" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Percent" NumberFormat-DecimalDigits="1" ToolTip="Enter the Expected Grorss Margin %">
                            <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>

                        <td style="text-align:right">
                            <telerik:RadNumericTextBox ID="txtnewTargetGrMarginPctN" runat="server" OnTextChanged="btnDefault_Click" autopostback="true" RenderMode="Lightweight" MaxLength="50" Width="100px" Type="Percent" NumberFormat-DecimalDigits="1" ToolTip="Enter the New Grorss Margin %">
                            <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                            </telerik:RadNumericTextBox>
                        </td>
        </tr>
    
        <tr>
            <td colspan="11"><hr /></td>
        </tr>    
        </table>
     </asp:Panel>
              </telerik:RadPageView>


     <%--  TAB 2 - Accessorials  --%>
    <telerik:RadPageView runat="server" ID="accessorials">
        <asp:Panel ID="Panel3" runat="server" DefaultButton = "btnDefaultAcc">

        <table border="0">
            <tr>
                <td colspan="13"><hr /></td>
            </tr>
         
        <tr>
            <td></td>
            <td colspan="3" style="color:red;vertical-align:top;">
                <asp:Label ID="lblNoAccessorialInfo" runat="server" Text="Accessorial Information Not Available" Visible="false" />
            </td>
            <td style="text-align:right">
                <telerik:RadButton ID="btnDefaultAcc" runat="server" OnClick="btnDefault_Click" Text="Update"  Visible="false" Enabled="true" />                 
                
            </td>
            <td><telerik:RadButton ID="btnGetAccessorials" runat="server" OnClick="btnGetAccessorials_Click" Text="Get Accessorials"  Visible="true"/></td>
            <td colspan="7"><i><asp:Label ID="lblAccessorialdt" runat="server" Text="Accessorials from SAP as of " Visible="true" /></i>&nbsp;<asp:Label ID="lblAccessorialDates" runat="server" Text="" Visible="true" /></td>
        </tr>
        <tr>
            <td style="width:20px;"></td>
            <td style="width:150px;"></td>
            <td style="width:40px; text-align:center; color:blue;"><asp:Label ID="Label1" runat="server" Text="Current" Visible="true" /></td>
            <td style="width:40px; text-align:center; color:blue;"><asp:Label ID="Label2" runat="server" Visible="true" Text="New" /></td>
            <td style="width:20px; text-align:center; color:blue;"><asp:Label ID="Label3" runat="server" Visible="true" Text="" /></td>
            <td style="width:120px; text-align:center;color:blue;"><asp:Label ID="Label6" runat="server" Text="" Visible="true" /></td>
            <td style="width:40px; text-align:center;color:blue;"><asp:Label ID="Label4" runat="server" Text="Current" Visible="true" /></td>

            <td style="width:10px;"></td>
            <td style="width:40px;text-align:center; color:blue;"><asp:Label ID="Label5" runat="server" Text="New" Visible="true" /></td>
            <td style="width:10px;"></td>
            <td style="width:40px; text-align:center;color:blue;"></td>
            <td style="width:10px;"></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>Resi Fees Per Shipment</td>
           <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtcurResiFees" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Current Resi Fees">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
           <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtexpResiFees" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Expected Resi Fees">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
           <td></td>      
             <td>Dims Ground</td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtcurDimsGround" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Current Dims Ground">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
            <td></td>
            <td style="text-align:left">
                 <telerik:RadNumericTextBox ID="txtexpDimsGround" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Expected Dims Ground">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
            </td>
            <td></td>
            <td style="text-align:right">
               
           </td>
            <td></td>
            <td></td>

        </tr>
        <tr>
            <td></td>
            <td><b>Special Hndlng Per Piece:</b></td>
            <td></td>
            <td></td>
            <td></td>
            <td>Dims Air</td>
            <td style="text-align:right">
                 <telerik:RadNumericTextBox ID="txtcurDimsAir" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Current Dims Air">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
            </td>
            <td></td>
            <td style="text-align:left">
                 <telerik:RadNumericTextBox ID="txtexpDimsAir" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Expected Dims Air">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
            </td>
            <td></td>
            <td style="text-align:right">
                
            </td>
            <td></td>
            <td></td>

        </tr>
           
        <tr>
            <td></td>
            <td>Flat Pkg</td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtcurshFP" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Current Special Handling Flat Pkg">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtexpshFP" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Expected Special Handling Flat Pkg">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
           <td></td>      
           <td><b>Dangerous Goods:</b></td>
            <td style="text-align:center; color:blue;">
               <asp:Label ID="Label7" runat="server" Text="Current" Visible="true" />
           </td>
            <td></td>
            <td>Per:</td>
            <td></td>
            <td style="text-align:center; color:blue;">
               <asp:Label ID="Label8" runat="server" Text="New" Visible="true" />
           </td>
            <td></td>
            <td>Per:</td>

        </tr>
       <tr>
            <td></td>
            <td>Addl Handling</td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtcurshAH" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Current Special Handling Addl Handling">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtexpshAH" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Expected Special Handling Addl Handling">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
           <td></td>      
            <td>Fully Regulated</td>
           <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtcurdgFR" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Current Dangerous Goods Fully Regulated">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
           <td></td>
            <td>
                <telerik:RadComboBox ID="cbxperc1" runat="server" Width="75px" >
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Piece" Value="piece" Selected="true"/>
                        <telerik:RadComboBoxItem runat="server" Text="Shipment" Value="shipment" />
                    </Items>
                  </telerik:RadComboBox>                 
            </td>
            <td></td>
           <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtexpdgFR" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Expected Dangerous Goods Fully Regulated">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
           <td></td>
            <td>
                <telerik:RadComboBox ID="cbxpere1" runat="server" Width="75px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Piece" Value="piece" Selected="true"/>
                        <telerik:RadComboBoxItem runat="server" Text="Shipment" Value="shipment" />
                    </Items>
                  </telerik:RadComboBox>
            </td>

       </tr>
        <tr>
            <td></td>
            <td>Large Pkg</td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtcurshLP" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Current Special Handling Large Pkg">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtexpshLP" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Expected Special Handling Large Pkg">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
           <td></td>      
            <td>UN3373</td>
           <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtcurdgUN3373" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Current Dangerous Goods UN3373">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
            <td></td>
            <td>
                <telerik:RadComboBox ID="cbxperc2" runat="server" Width="75px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Piece" Value="piece" Selected="true"/>
                        <telerik:RadComboBoxItem runat="server" Text="Shipment" Value="shipment" />
                    </Items>
                  </telerik:RadComboBox>
            </td>
            <td></td>
           <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtexpdgUN3373" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Expected Dangerous Goods UN3373">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
            <td></td>
            <td>
                <telerik:RadComboBox ID="cbxpere2" runat="server" Width="75px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Piece" Value="piece" Selected="true"/>
                        <telerik:RadComboBoxItem runat="server" Text="Shipment" Value="shipment" />
                    </Items>
                  </telerik:RadComboBox>
            </td>

        </tr>
        <tr>
            <td></td>
            <td>Over Max Limit</td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtcurshOML" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Current Special Handling Over Max Limit">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtexpshOML" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Expected Special Handling Over Max Limit">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
           <td></td>      
            <td>UN1845</td>
           <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtcurdgUN1845" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Current Dangerous Goods UN1845">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
            <td></td>
            <td>
                <telerik:RadComboBox ID="cbxperc3" runat="server" Width="75px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Piece" Value="piece" Selected="true"/>
                        <telerik:RadComboBoxItem runat="server" Text="Shipment" Value="shipment" />
                    </Items>
                  </telerik:RadComboBox>
            </td>
            <td></td>
           <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtexpdgUN1845" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Expected Dangerous Goods UN1845">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
            <td></td>
            <td>
                <telerik:RadComboBox ID="cbxpere3" runat="server" Width="75px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Piece" Value="piece" Selected="true"/>
                        <telerik:RadComboBoxItem runat="server" Text="Shipment" Value="shipment" />
                    </Items>
                  </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Oversized</td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtcurshO" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Current Special Handling Oversized">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtexpshO" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Expected Special Handling Oversized">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
           <td></td>      
            <td>Less Than 500kg</td>
           <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtcurdgLT500kg" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Current Dangerous Goods < 500kg">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
            <td></td>
            <td>
                <telerik:RadComboBox ID="cbxperc4" runat="server" Width="75px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Piece" Value="piece" Selected="true"/>
                        <telerik:RadComboBoxItem runat="server" Text="Shipment" Value="shipment" />
                    </Items>
                  </telerik:RadComboBox>
            </td>
            <td></td>
           <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtexpdgLT500kg" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Expected Dangerous Goods < 500 kg">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
            <td></td>
            <td>
                <telerik:RadComboBox ID="cbxpere4" runat="server" Width="75px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Piece" Value="piece" Selected="true"/>
                        <telerik:RadComboBoxItem runat="server" Text="Shipment" Value="shipment" />
                    </Items>
                  </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>Res Area HvyWt</td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtcurshRAH" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Current Special Handling Res Area HvyWt">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
            <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtexpshRAH" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Expected Special Handling Res Area HvyWt">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
           <td></td>      
            <td>Limited qty</td>
           <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtcurdgLQ" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Current Dangerous Goods Limited qty">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
            <td></td>
            <td>
                <telerik:RadComboBox ID="cbxperc5" runat="server" Width="75px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Piece" Value="piece" Selected="true"/>
                        <telerik:RadComboBoxItem runat="server" Text="Shipment" Value="shipment" />
                    </Items>
                  </telerik:RadComboBox>
            </td>
            <td></td>
           <td style="text-align:right">
                <telerik:RadNumericTextBox ID="txtexpdgLQ" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Expected Dangerous Goods Limited qty">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadNumericTextBox>
           </td>
            <td></td>
            <td>
                <telerik:RadComboBox ID="cbxpere5" runat="server" Width="75px">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" Text="Piece" Value="piece" Selected="true"/>
                        <telerik:RadComboBoxItem runat="server" Text="Shipment" Value="shipment" />
                    </Items>
                  </telerik:RadComboBox>
            </td>
       </tr>
             <tr>
                <td colspan="13"><p></td>
       </tr> 
            <tr>
                <td></td>
                <td colspan="12">
                    Comments&nbsp;&nbsp;<asp:TextBox id="txtAccessorialComment" TextMode="multiline" Columns="120" Rows="1" runat="server"  Text='<%# Bind("modificationReason") %>' Visible="true" /><p></p>
                </td>
            </tr>
        
      <%-- <tr>
                <td colspan="13"><hr /></td>
       </tr> --%>
            </table>
            <p></p>

           <%-- FUEL--%>
            <table border="0">
               
        <tr>

              <td style="width:85px"></td>
            <td style="color:blue;">Courier Fuel Alignment</td>
             <td style="width:10px"></td>
               <td style="color:blue;text-align:center;">Current Fuel Discount</td>
             <td style="width:10px"></td>
             <td style="color:blue;text-align:center;">New Fuel Alignment</td>
             <td style="width:10px"></td>
             <td style="color:blue;text-align:center;">New Fuel Discount</td>
            <td style="width:50px"></td>
            <td style="color:blue;text-align:center;">Origin Beyond Disc?</td>
            <td style="width:10px"></td>
            <td style="color:blue;text-align:center;">Dest Beyond Disc?</td>
        </tr> 
        <tr>
                <td></td>
                 <td rowspan="2">
                 <asp:RadioButtonList ID="radioCurrFuel" runat="server">
                      <asp:ListItem Text ="Fuel PI" Value="fuelpi" Selected="True"/> 
                      <asp:ListItem Text ="Fuel Puro Inc." Value="fuelpuroinc" />                      
                </asp:RadioButtonList>
            </td>
            <td></td>
           <td style="text-align:left">
                <telerik:RadTextBox ID="txtcurFuelPIS" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Current Fuel PI">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadTextBox>
           </td>
           <td></td>
            <td rowspan="2">
                 <asp:RadioButtonList ID="radioExpFuel" runat="server">
                      <asp:ListItem Text ="Fuel PI" Value="fuelpi" Selected="True"/> 
                      <asp:ListItem Text ="Fuel Puro Inc." Value="fuelpuroinc" />                      
                </asp:RadioButtonList>
            </td>
             <td></td>

           <td style="text-align:left">
                <telerik:RadTextBox ID="txtexpFuelPIS" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Expected Fuel PI">
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadTextBox>
           </td>     
            <td></td> 
              <td rowspan="2">
                 <asp:RadioButtonList ID="radioBeyondOrigin" runat="server">
                      <asp:ListItem Text ="Yes" Value="yes" Selected="True"/> 
                      <asp:ListItem Text ="No" Value="no" />                      
                </asp:RadioButtonList>
            </td>    
             <td></td> 
              <td rowspan="2">
                 <asp:RadioButtonList ID="radioBeyondDest" runat="server">
                      <asp:ListItem Text ="Yes" Value="yes" Selected="True"/> 
                      <asp:ListItem Text ="No" Value="no" />                      
                </asp:RadioButtonList>
            </td>              
          </tr>  
       
          <tr>
              <td></td>
             <td></td>
           <td style="text-align:left">
                <telerik:RadTextBox ID="txtcurFuelPuroS" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Current Fuel Puro Inc" >
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadTextBox>
           </td>
             <td></td>
             <td></td>   
           <td style="text-align:left">
                <telerik:RadTextBox ID="txtexpFuelPuroS" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="50" Width="70px" ToolTip="Enter the Expected Fuel Puro Inc" >
                <EnabledStyle Font-Names="Verdana" HorizontalAlign="Right" /> 
                </telerik:RadTextBox>
           </td>      
     
          </tr>   
       </table>
            <hr />
                    </asp:Panel>
    </telerik:RadPageView>


      <%--  TAB 3 - NOTES  --%>

    <telerik:RadPageView runat="server" ID="notes">


             <table border="0">
               <tr>
             <td colspan="5"><p /></td>
         </tr>
        <tr>
            <td style="width: 50px;"></td>
            <td style="width: 100px;">Note Type</td>
            <td>Notes</td>
            <td style="width: 50px;"></td>
           <td></td>
        </tr>

        <tr>

            <td></td>
             <td>

                  <asp:RadioButtonList ID="rblist1" runat="server">
                      <asp:ListItem Text ="approval" Value="Approval" Selected="True"/> 
                      <asp:ListItem Text ="internal" Value="Internal" />                      
                </asp:RadioButtonList>
                </td>
            <td><asp:TextBox id="txtNotes" TextMode="multiline" Columns="90" Rows="3" runat="server"  />
               
            <td>
               

            </td>
             <td><telerik:RadButton ID="btnAddNote" runat="server" OnClick="btnAddNote_Click" Text="Add"  Visible="true"/></td>
        </tr> 

           
        </table>
        <table>
        <tr>
            <td style="width: 50px;"></td>
            <td><p/></td>
        </tr>
        <tr>
            <td style="width: 50px;"></td>
            <td style="text-align:left;width: 800px;">

                <telerik:RadGrid ID="rgNotesGrid" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" AllowFilteringByColumn="false" PageSize="5"
              OnDeleteCommand="rgNotesGrid_DeleteCommand" OnUpdateCommand="rgNotesGrid_UpdateCommand" AllowPaging="True" OnPageIndexChanged="rgNotesGrid_PageIndexChanged" OnItemDataBound="rgNotesGrid_ItemDataBound">
            
            <MasterTableView AutoGenerateColumns="False" AllowPaging="True" CommandItemDisplay="Top" EditMode="InPlace" DataKeyNames="idContractRenewalNotes">
                <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False" ShowExportToWordButton="false" ShowExportToExcelButton="false"></CommandItemSettings>
                 
                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="EditLink" Visible="false"><HeaderStyle Width="36px"></HeaderStyle>
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="idContractRenewalNotes" FilterControlAltText="Filter idContractRenewalNotes column" HeaderText="idContractRenewalNotes" SortExpression="idContractRenewalNotes" UniqueName="idContractRenewalNotes" Visible="true" Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CreatedBy" AllowFiltering="true" FilterControlAltText="Filter Createdby column" HeaderText="Entered By" SortExpression="Createdby" UniqueName="Createdby" HeaderStyle-Width="10%">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CreatedOn"  DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" FilterControlAltText="Filter noteDate column" HeaderText="Note Date" SortExpression="noteDate" UniqueName="noteDate" HeaderStyle-Width="10%">
                    </telerik:GridBoundColumn>                  
                    
                    <telerik:GridBoundColumn DataField="Note"  AllowFiltering="true" FilterControlAltText="Filter Note column" HeaderText="Note" SortExpression="Note" UniqueName="Note">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="NoteType"  AllowFiltering="true" ItemStyle-ForeColor="Green" FilterControlAltText="Filter NoteType column" HeaderText="Note Type" SortExpression="NoteType" UniqueName="NoteType" HeaderStyle-Width="10%">
                    </telerik:GridBoundColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete"  HeaderText="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false" ConfirmDialogType="RadWindow" ConfirmText="Delete Note?" >
                    <HeaderStyle Width="36px"></HeaderStyle></telerik:GridButtonColumn>   
                </Columns>
                <PagerStyle AlwaysVisible="true"></PagerStyle>
            </MasterTableView>
        </telerik:RadGrid>

            </td>
        </tr>
        
       

        <tr>
            <td colspan="2"><hr /></td>
        </tr>        
                 </table>

       </telerik:RadPageView>
    
         
     <!-- TAB 4 File UPlaods -->
     <telerik:RadPageView runat="server" ID="uploads">
          <hr />  
         <table border="0">
             <tr>
                 <td style="width: 50px;"></td>
                 <td colspan="2">
                      Upload a Document of type: Gif, JPeg, Png, Doc, Docx, Xls, Xlsx, PDF, Txt.  
                  </td>
             </tr>
             <tr>
                 <td style="width: 50px;"></td>
                 <td style="text-align:left">
                        <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="RadAsyncUpload2" HideFileInput="true" 
                         AllowedFileExtensions=".jpeg,.jpg,.png,.doc,.docx,.xls,.xlsx,.pdf,.txt,.gif,.csv" OnFileUploaded="RadAsyncUpload1_FileUploaded" targetfolder="~/FileUploads" localization-select="Browse"
                         />
                     <%--<asp:RequiredFieldValidator ID="rfvfile" runat="server" ValidationGroup="Submitfile" ControlToValidate="RadAsyncUpload2" ErrorMessage=" *Required" style="color: red"></asp:RequiredFieldValidator>--%>
                 </td>
                 <td>
                                        
                 </td>
                 </tr>
          
             <tr>
                 <td></td>
                 
                 <td colspan="2">Enter Description:&nbsp;<telerik:RadTextBox ID="txtFileDescription" RenderMode="Lightweight" runat="server" autopostback="true" MaxLength="150" Width="200px" ToolTip="Enter the File Description" />
                      
                      <%--<asp:RequiredFieldValidator ID="rfvdfesc" runat= "server" ValidationGroup="Submitfile" ControlToValidate="txtFileDescription" ErrorMessage=" *Description Required" style="color: red"></asp:RequiredFieldValidator>--%>
                 </td>
             </tr>
             <tr>
                 <td></td>
                 <td>                      
                      <telerik:RadButton ID="btnSaveUpload" Text="Save File" runat="server" OnClick="btnSaveUpload_Click" AutoPostBack="false" Visible="true" Enabled="true"></telerik:RadButton>                      
                 </td>
                 <td></td>

             </tr>
             <tr>
                 <td></td>
                 <td colspan="2">
             <telerik:RadGrid ID="rgUpload" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" AllowFilteringByColumn="true" PageSize="5" 
              OnDeleteCommand="rgUpload_DeleteCommand" OnInsertCommand="rgUpload_InsertCommand" OnUpdateCommand="rgUpload_UpdateCommand" OnItemDataBound="rgUpload_ItemDataBound" AllowPaging="True" OnPageIndexChanged="rgUpload_PageIndexChanged">
            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
            </ExportSettings>
            <MasterTableView AutoGenerateColumns="False" AllowPaging="True" CommandItemDisplay="Top" EditMode="PopUp" DataKeyNames="idContractRenewalUpload">
                <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False" ShowExportToWordButton="False" ShowExportToExcelButton="False"></CommandItemSettings>
                 <EditFormSettings UserControlName="EditFileUpload.ascx" EditFormType="WebUserControl"  PopUpSettings-Height="250px" PopUpSettings-Width="550px">
                    <FormStyle Height="500px"></FormStyle>
                    <PopUpSettings Modal="true" />
                </EditFormSettings>
                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="EditLink"  Visible="false"><HeaderStyle Width="36px"></HeaderStyle>
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="idContractRenewalUpload" FilterControlAltText="Filter idContractRenewalUpload column" HeaderText="idContractRenewalUpload" SortExpression="idContractRenewalUpload" UniqueName="idContractRenewalUpload" Visible="true" Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="idContractRenewal" FilterControlAltText="Filter idContractRenewal column" HeaderText="idContractRenewal" SortExpression="idContractRenewal" UniqueName="idContractRenewal" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="UploadDate"  DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" FilterControlAltText="Filter UploadDate column" HeaderText="Upload Date" SortExpression="UploadDate" UniqueName="UploadDate" HeaderStyle-Width="10%">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Createdby" AllowFiltering="true" FilterControlAltText="Filter Createdby column" HeaderText="Uploaded By" SortExpression="Createdby" UniqueName="Createdby" HeaderStyle-Width="10%">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Description"  AllowFiltering="true" FilterControlAltText="Filter Description column" HeaderText="Description" SortExpression="Description" UniqueName="Description">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ActiveFlag"  AllowFiltering="true" FilterControlAltText="Filter ActiveFlag column" HeaderText="ActiveFlag" SortExpression="ActiveFlag" UniqueName="ActiveFlag" Visible="false">
                    </telerik:GridBoundColumn>
                     <telerik:GridHyperLinkColumn DataTextField="FilePath" Target="_parent" NavigateUrl="javascript:void(0);"
                         AllowFiltering="true" FilterControlAltText="Filter FilePath column" HeaderText="File Path" SortExpression="FilePath" UniqueName="FilePath" ItemStyle-ForeColor="Blue">
                    </telerik:GridHyperLinkColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete"  HeaderText="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false" ConfirmDialogType="RadWindow" ConfirmText="Delete?" Visible="true">
                    <HeaderStyle Width="36px"></HeaderStyle></telerik:GridButtonColumn>   
                </Columns>
                <PagerStyle AlwaysVisible="True"></PagerStyle>
            </MasterTableView>
        </telerik:RadGrid>
                 </td>
                  
             </tr>
             <tr><td colspan="3">
                      <hr />
                   </td></tr>
          </table>
     </telerik:RadPageView>

   </telerik:RadMultiPage>




      <table border="0">
        
     
          <tr>
            <td style="width: 30px;" ></td>
            <td colspan="4"> <p style="color:red"><i >*Required Fields</i></p></td>
               <td style="text-align:left">Route To:</td>  
              <td></td>   
              <td >Send Email?</td>              
          </tr>
          <tr>
             <td style="width: 30px;" ></td>
             <td style="text-align:right;">
                 <asp:Button CssClass="btn btn-primary" ValidationGroup="Submitgroup" ID="Button1" Text="Save"
                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>
              </td>
              <td style="width: 10px;" ></td>
               <td>
                 <asp:Button ID="CancelButton" Text="Cancel" runat="server" CausesValidation="False"  CssClass="btn btn-primary" CommandName="Cancel"></asp:Button>  

               </td>
              <td style="text-align:left; width: 10px;" ></td>   
              <td>
                  <telerik:RadComboBox ID="cbxRouteTo" runat="server" ToolTip="Route To" Width="150px" EmptyMessage="Route To" AutoPostBack="true" OnSelectedIndexChanged="cbxRouteTo_SelectedIndexChanged"></telerik:RadComboBox>
                  <asp:HiddenField ID="hdnRouteTo" runat="server" Value='<%# Bind("idContractRenewalRouting") %>' />
              </td>          
              <td style="width: 10px;" ></td>   
              <td>
                   <telerik:RadButton ID="sendEmailFlag" runat="server" ToggleType="CheckBox" ButtonType="LinkButton" Checked='false' AutoPostBack="false">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Yes" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox" />
                    </ToggleStates>
                  </telerik:RadButton>
              </td>
              <td>
                  <asp:Label ID="lblErrorMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblSuccessMessage" runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>
              </td>
          </tr>
         <tr>
            <td style="width: 30px;" ></td>
            <td colspan="7"> <p /><i ><asp:Label ID="lblSaveFirst" runat="server" ForeColor="Blue" Text="Save Contract Before Adding Notes or File Attachments"></asp:Label></i></td>
                       
          </tr>
         
    </table>
    
  
   
</asp:Panel>