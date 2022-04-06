<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditAccount.ascx.cs" Inherits="PrepumaWebApp.EditAccount" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

 <telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" RelativeTo="Element"
                               Position="MiddleRight" AutoTooltipify="true" ContentScrolling="Default" Width="150"
                               Height="10">
    </telerik:RadToolTipManager>
<telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="cbxClientName">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="cbxContractNumber"  LoadingPanelID="RadAjaxLoadingPanel1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cbxContractNumber">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="cbxClientName"  LoadingPanelID="RadAjaxLoadingPanel1" />
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
                 <asp:HiddenField ID="hdClient" runat="server" Value='<%# Bind("ClientID") %>' />
                 <telerik:RadComboBox ID="cbxClientName" runat="server" EmptyMessage="Select ClientName"  Width="200px" AllowCustomText="true" AutoPostBack="true"  DataTextField="ClientName" DataValueField="ClientID" Filter="StartsWith" OnSelectedIndexChanged="cbxClientName_SelectedIndexChanged"></telerik:RadComboBox>
            </td>
        </tr>
                 <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="cbxClientName" ValidationGroup="AccountInfo" ErrorMessage="Client Name is required." style="color: red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
         <tr>
            <td style="width: 50px; color:red; text-align:right " >*</td>
            <td style="width: 100px"><asp:Label ID="labelContractNumber" runat="server" CssClass="rdfLabel" Text="Contract Number:" />
            </td>
            <td style="width: 200px">
                <asp:HiddenField ID="hdContractNumber" runat="server" Value='<%# Bind("ContractID") %>' />
                <telerik:RadComboBox ID="cbxContractNumber" runat="server" EmptyMessage="Select Contract" Width="200px" AllowCustomText="true" AutoPostBack="true" DataTextField="ContractNumber" DataValueField="ContractID" Filter="StartsWith" OnSelectedIndexChanged="cbxContractNumber_SelectedIndexChanged" >
                </telerik:RadComboBox>
   
            </td>
        </tr>
         <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="cbxContractNumber" ValidationGroup="AccountInfo"  ErrorMessage="Contract Number is required." style="color: red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
        <tr>
            <td style="width: 50px; color:red; text-align:right " >*</td>
            <asp:HiddenField ID="hdAcctID" runat="server" Value='<%# Bind("AcctID") %>' />
            <td style="width: 75px"><asp:Label ID="labelAccountNumber" runat="server" CssClass="rdfLabel" Text="Account:" />
            </td>
            <td style="width: 200px">

                
                <telerik:RadTextBox  ID="AccountTextBox" MaxLength="20" runat="server" Text='<%# Bind("Acctnbr") %>'  Width="150px" ToolTip="Enter the Account Number" Visible="true">
                </telerik:RadTextBox>
                
            </td>
        </tr>
         <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="AccountTextBox" ValidationGroup="AccountInfo" ValidationExpression = "^[a-zA-Z0-9]{0,5}$" ErrorMessage="Account Number is required." style="color: red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
        
        <tr>
            <td> </td>
            <td> </td>
            <td>
                 <asp:Button CssClass="btn btn-primary" ValidationGroup="AccountInfo" ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                runat="server" CausesValidation="True" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                 <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False"  CssClass="btn btn-primary" CommandName="Cancel"></asp:Button>  
                                <asp:Button ID="btnAddClient" Text="Add Client" runat="server" CausesValidation="False"  CssClass="btn btn-primary" visible="false"></asp:Button>    
                        
            </td>
        </tr>
         <tr>
            <td></td>
            <td colspan="2">
                <p style="color:red"><i >*Required Fields</i></p>
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
