<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditContract.ascx.cs" Inherits="PrepumaWebApp.EditContract" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

 <telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" RelativeTo="Element"
                               Position="MiddleRight" AutoTooltipify="true" ContentScrolling="Default" Width="150"
                               Height="10">
    </telerik:RadToolTipManager>
    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="cbClientName">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="ContractNameTextBox"  LoadingPanelID="RadAjaxLoadingPanel1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
    </telerik:RadAjaxManagerProxy>

<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />


<asp:Panel ID="Panel1" runat="server" GroupingText=''>

    <table>
        <tr><td colspan="4" style="height: 25px"></td></tr>
        <tr>
            <td style="width: 50px; color:red; text-align:right " >*</td>
            <td style="width: 75px"><asp:Label ID="labelClient" runat="server" CssClass="rdfLabel" Text="Client:" />
            </td>
            <td>
                 <asp:HiddenField ID="hdClientID" runat="server" Value='<%# Bind("ClientID") %>' />
                 <telerik:RadComboBox ID="cbClientName" runat="server" EmptyMessage="Select ClientName"  Width="200px" AllowCustomText="true" AutoPostBack="true" DataTextField="ClientName" DataValueField="ClientID" Filter="StartsWith" OnSelectedIndexChanged="cbClientName_SelectedIndexChanged"></telerik:RadComboBox>
            </td>
        </tr>
         <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="cbClientName" ValidationGroup="ClientInfo" ErrorMessage="Client Name is required." style="color: red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
        
        <tr>
            <td style="width: 50px; color:red; text-align:right ">*</td>
            <td style="width: 120px">
                <asp:Label ID="labelContractNumber" runat="server" CssClass="rdfLabel" Text="Contract Number:" />
            </td>
            <td style="width: 200px">
                <asp:Label ID="lblContractID" runat="server" Text='<%# Eval("ContractID") %>' Visible="false" />
                <telerik:RadTextBox ID="ContractTextBox" runat="server" MaxLength="10" Text='<%# Bind("ContractNumber") %>' ToolTip="Enter the Contract Number" Width="200px">
                </telerik:RadTextBox>
            </td>
        <tr>
            <td></td>
            <td></td>
            <td>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ContractTextBox" ErrorMessage="AlphaNumeric Contract Number required." style="color: red" ValidationExpression="^[a-zA-Z0-9]{0,10}$" ValidationGroup="ClientInfo"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 50px; color:red; text-align:right ">*</td>
            <td style="width: 75px">
                <asp:Label ID="labelContractName" runat="server" CssClass="rdfLabel" Text="Contract Name:" />
            </td>
            <td style="width: 200px">
                <telerik:RadTextBox ID="ContractNameTextBox" runat="server" Enabled="true" Text='<%# Bind("ContractName") %>' ToolTip="Enter the Contract Name" Width="200px" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ContractNameTextBox" ErrorMessage="Contract Name is required." style="color: red" ValidationGroup="ClientInfo"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>
                <p>
                </p>
                <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' CssClass="btn btn-primary" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>' ValidationGroup="ClientInfo" />
                &nbsp;
                <asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel" CssClass="btn btn-primary" Text="Cancel" />
                <asp:Button ID="btnAddClient" runat="server" CausesValidation="False" CssClass="btn btn-primary" Text="Add Client" visible="false" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td colspan="2">
                <p style="color:red">
                    <i>*Required Fields</i></p>
                <asp:Label ID="lblErrorMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblSuccessMessage" runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 50px"></td>
            <td colspan="2" style="color: red">
                <asp:ValidationSummary runat="server" HeaderText="Please Enter Required Fields:" />
            </td>
        </tr>
    </table>
</asp:Panel>
