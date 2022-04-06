<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditSalesType.ascx.cs" Inherits="PrepumaWebApp.EditSalesType" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Panel runat="server" ID="panle1">
    <div style="margin: 20px 30px 60px 10px;  padding-left:20px">
      <table>
        <tr>
            <td colspan="4" style="height: 15px"></td>
        </tr>
    <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="lblSalesType" runat="server" CssClass="rdfLabel" Text="Sales Type:" /></td>
                        <td>
                             <telerik:RadTextBox ID="txtSalesType" runat="server" MaxLength="20"  Width="160px" ToolTip="Enter the  Sales Type" CssClass="upper" Text='<%# Bind("SalesType") %>'>
                                 <ClientEvents OnValueChanging="MyValueChanging" />
                             </telerik:RadTextBox>
                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="salestypeVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtSalesType"></asp:RequiredFieldValidator>
                </td>
            </tr>
          <tr><td></td></tr>
          <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="lblSalesDescription" runat="server" CssClass="rdfLabel" Text="Sales Description" /></td>
                        <td>
                             <telerik:RadTextBox ID="txtSalesDescription" runat="server" MaxLength="50" Width="160px" ToolTip="Enter the  SalesRep Name" Text='<%# Bind("SalesDescription") %>'/>
                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="salestypeVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtSalesDescription"></asp:RequiredFieldValidator>
                </td>
            </tr>
          <tr><td></td></tr>
          <tr>
                <td></td>
                <td></td>
                <td>

                    <asp:Button ID="Button1" causesValidation ="true" ValidationGroup="salestypeVG" CssClass="btn btn-primary" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' 
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
      
    </div>
    <style type="text/css">
        .upper
            {
                 text-transform:uppercase;
            }
    </style>
</asp:Panel>
