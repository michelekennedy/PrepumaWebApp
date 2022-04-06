<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditClient.ascx.cs" Inherits="PrepumaWebApp.EditClient" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

 <telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" RelativeTo="Element"
                               Position="MiddleRight" AutoTooltipify="true" ContentScrolling="Default" Width="150"
                               Height="10">
    </telerik:RadToolTipManager>

  <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="cbClientName">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="clientNameTextBox"  LoadingPanelID="RadAjaxLoadingPanel1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />

<asp:Panel ID="Panel1" runat="server" GroupingText=''>

    <table>
        
        <tr>
            <td style="height: 50px" colspan="3"></td>
           
        </tr>
        <tr>
            <td style="width: 50px; color:red; text-align:right " >*</td>
            <td style="width: 85px"><asp:Label ID="labelClientName" runat="server" CssClass="rdfLabel" Text="Client Name:" />
            </td>
            <td style="width: 200px">
                <asp:Label ID="lblClientId" runat="server" Text='<%# Eval("ClientID") %>' Visible="false"/>
                <telerik:RadTextBox ID="clientNameTextBox" runat="server" Text='<%# Bind("ClientName") %>'  Width="150px" ToolTip="*Enter the Client Name"/>
            </td>
        </tr>
         <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="clientNameTextBox" ErrorMessage="Client Name is required." style="color: red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
        <tr>
            <td style="height: 25px" colspan="3"></td>
           
        </tr>
        <tr>
            <td> </td>
            <td> </td>
            <td>
                 <asp:Button CssClass="btn btn-primary" ValidationGroup="Submitgroup" ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
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

    </table>
</asp:Panel>