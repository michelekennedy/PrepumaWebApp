<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditGateway.ascx.cs" Inherits="PrepumaWebApp.EditGateway" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Panel runat="server" ID="panel1">
    <div style="margin: 20px 30px 60px 10px;  padding-left:20px">
      <table>
        <tr>
            <td colspan="4" style="height: 15px"></td>
        </tr>
    <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="lblGateway" runat="server" CssClass="rdfLabel" Text="Gateway  " /></td>
                        <td>
                            <telerik:RadComboBox ID="cbxGateway" runat="server" EmptyMessage="select Gateway" Width="160px" ToolTip="Enter the Gateway" DataTextField="Airport" DataValueField="Airport" ></telerik:RadComboBox>
                            <asp:HiddenField ID="hdGateway" runat="server" Value='<%# Bind("gateway") %>' />
                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="gatewayVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="cbxGateway"></asp:RequiredFieldValidator>
                </td>
            </tr>
          <tr><td></td></tr>
            
          <tr>
                 <td></td>
            </tr>
          <tr>
                <td></td>
                <td></td>
                <td>

                    <asp:Button ID="Button1" causesValidation ="true" ValidationGroup="gatewayVG" CssClass="btn btn-primary" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' 
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
</asp:Panel>