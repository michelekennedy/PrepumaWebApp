<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditSalesRep.ascx.cs" Inherits="PrepumaWebApp.EditSalesRep" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>

    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="ddlBusinessType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtThresholdN"  LoadingPanelID="RadAjaxLoadingPanel1e" />
                
            </UpdatedControls>
        </telerik:AjaxSetting>
           </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1e" runat="server" />

<div style="margin: 20px 30px 60px 10px;  padding-left:20px">
      <table>
        <tr>
            <td colspan="4" style="height: 20px"></td>
        </tr>
    <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="lblSalesRepId" runat="server" CssClass="rdfLabel" Text="SalesRep ID:" /></td>
                        <td>
                             <telerik:RadTextBox ID="txtSalesRepId" runat="server" MaxLength="3"  Width="160px" ToolTip="Enter the  SalesRep Id" CssClass="upper" Text='<%# Bind("SalesRepID") %>'>
                                 <ClientEvents OnValueChanging="MyValueChanging" />
                             </telerik:RadTextBox>
                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="salesRepVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtSalesRepId"></asp:RequiredFieldValidator>
                </td>
            </tr>
          <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="lblSalesRepName" runat="server" CssClass="rdfLabel" Text="SalesRep Name:" /></td>
                        <td>
                             <telerik:RadTextBox ID="txtSalesRepName" runat="server" MaxLength="25" Width="160px" ToolTip="Enter the  SalesRep Name" Text='<%# Bind("SalesRep") %>'/>
                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="salesRepVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtSalesRepName"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td style="color:red; text-align:right">*</td>
                <td>
                    <asp:Label ID="lblSalesTitle" runat="server" CssClass="rdfLabel" Text="SalesRep Title" />
                </td>
                        <td>
                            <asp:HiddenField ID="hdSalesRpTitles" runat="server" Value='<%# Bind("SalesRepTitle") %>' />
                             <telerik:RadDropDownList ID="ddlSlsRepTitles" runat="server" DefaultMessage="select Sales Rep title" Visible="true" OnSelectedIndexChanged="ddlTitle_SelectedIndexChanged" autopostback="true"></telerik:RadDropDownList>
                            <telerik:RadTextBox ID="txtSlsRepTitles" runat="server"  Width="150px" MaxLength="50" Visible="false" ToolTip="Enter New Title "/>   
                        </td>               
                <td>
                     <asp:RequiredFieldValidator ID="rfSlsRepTitles1" runat="server" ValidationGroup="salesRepVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="ddlSlsRepTitles"></asp:RequiredFieldValidator>
                     <asp:RequiredFieldValidator runat="server" ID="rfSlsRepTitles2" ControlToValidate="txtSlsRepTitles" ErrorMessage="Required" ValidationGroup="salesRepVG"  ForeColor="Red" Enabled="false"></asp:RequiredFieldValidator>
                </td>
            </tr>

           
          
           <tr>
                <td></td>
                <td><asp:Label ID="lblThreshold" runat="server" CssClass="rdfLabel" Text="Threshold:" /></td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtThresholdN" runat="server" Width="160px" ToolTip="Enter the  Threshold" Text='<%# Bind("Threshold") %>' >
                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true"   KeepNotRoundedValue="false"  />
                            </telerik:RadNumericTextBox>
                        </td>
                <td></td>
            </tr>

            <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="lblBusinessType" runat="server" CssClass="rdfLabel" Text="Business Type" /></td>
                        <td>
                            <asp:HiddenField ID="hdBizType" runat="server" Value='<%# Bind("BusinesType") %>' />                          
                           <telerik:RadDropDownList ID="ddlBusinessType" runat="server" DefaultMessage="select BusinessType" DataTextField="BusinessType" DataValueField="BusinessType" ToolTip="Enter the Business Type" 
                                      autopostback="false"></telerik:RadDropDownList>   
                        </td>
                <td>
                     <asp:ImageButton ID="imgbtnBizType" runat="server" AlternateText="addnew" ImageUrl="~/Images/plus-icon.png" Height="20px" Width="20px" OnClientClick="window.open('MaintenanceBizType.aspx');return false;" />
               
                     <asp:RequiredFieldValidator ID="rfBusinessType1" runat="server" ValidationGroup="salesRepVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="ddlBusinessType"></asp:RequiredFieldValidator>

                  </td>
            </tr>
          <tr>
                <td></td>
                <td><asp:Label ID="lblEmployeeID" runat="server" CssClass="rdfLabel" Text="EmployeeID:" /></td>
                        <td>
                            <telerik:RadTextBox ID="txtEmployeeID" runat="server" MaxLength="25" Width="160px" ToolTip="Enter the  SalesRep Name" Text='<%# Bind("EmployeeID") %>'/>                
                        </td>
                <td></td>
            </tr>

            <tr>
                <td></td>
                <td><asp:Label ID="lblNSMID" runat="server" CssClass="rdfLabel" Text="NSMID" /></td>
                        <td>
                            <asp:HiddenField ID="hdNsmid" runat="server" Value='<%# Bind("nSMID") %>' />
                            <telerik:RadDropDownList ID="ddlNSMID" runat="server" DefaultMessage="select NSMID" ToolTip="Select the NSMID" ></telerik:RadDropDownList>
                        </td>
                <td></td>
            </tr>
          <tr>
                <td></td>
                <td><asp:Label ID="lblDSM" runat="server" CssClass="rdfLabel" Text="DSM" /></td>
                        <td>
                            <asp:HiddenField ID="hdDsm" runat="server" Value='<%# Bind("dSM") %>' />
                             <telerik:RadDropDownList ID="ddlDSM" runat="server" DefaultMessage="select NSMID" ToolTip="Select the DSM" ></telerik:RadDropDownList>
                        </td>
                <td></td>
            </tr>
          
        <tr>
                <td></td>
            <td> <asp:Label ID="lblReported" runat="server" CssClass="rdfLabel" Text="New Business Line?"  /></td>
            <td>
                <telerik:RadButton ID="newBizChk" runat="server" ToggleType="CheckBox" ButtonType="LinkButton" AutoPostBack="false" Checked='<%# Eval("newBizline") == DBNull.Value ? false : Convert.ToBoolean(Eval("newBizline")) %>'>
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Yes" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox" />
                    </ToggleStates>
                </telerik:RadButton>

            </td>
               <td></td>
            </tr>
           <tr>
                <td></td>
            <td> <asp:Label ID="lblStackReport" runat="server" CssClass="rdfLabel" Text="Include in Stack Report?"  /></td>
            <td>
                <telerik:RadButton ID="stackReportChk" runat="server" ToggleType="CheckBox" ButtonType="LinkButton" AutoPostBack="false" Checked='<%# Eval("StackReport") == DBNull.Value ? false : Convert.ToBoolean(Eval("StackReport")) %>'>
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Yes" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox" />
                    </ToggleStates>
                </telerik:RadButton>

            </td>
               <td></td>
            </tr>
          <tr><td></td></tr>
          <tr>
                <td></td>
                <td></td>
                <td>

                    <asp:Button ID="Button1" causesValidation ="true" ValidationGroup="salesRepVG" CssClass="btn btn-primary" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' 
                        Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>' />
                    &nbsp;
                    <asp:Button ID="btnCancel" CssClass="btn btn-primary" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" /><p></p>
                </td>
                 <td></td>

            </tr>
          <tr>
            <td></td>
            <td colspan="2" >
                <p style="color:red"><i>*Required Fields</i></p>
                <asp:Label ID="lblErrorMessage" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblSuccessMessage" runat="server" Visible="false" Font-Bold="true" ForeColor="Green"></asp:Label>
                      </td>
             <td></td>
        </tr>
          
          
    </table>
    <style>
        .upper
            {
                text-transform:uppercase;
            }
    </style>
        
    </div>
