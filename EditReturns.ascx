<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditReturns.ascx.cs" Inherits="PrepumaWebApp.EditReturns" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Panel runat="server" ID="panel1">
    <div style="margin: 20px 30px 60px 10px;  padding-left:20px">
      <table>
        <tr>
            <td colspan="4" style="height: 15px"></td>
        </tr>
    <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="lblRtnsId" runat="server" CssClass="rdfLabel" Text="Returns ID" /></td>
                        <td>
                             <telerik:RadTextBox ID="txtRtnsId" runat="server" MaxLength="2"  Width="160px" ToolTip="Enter the  Returns ID" CssClass="upper" Text='<%# Bind("RTNSID") %>'>
                                <ClientEvents OnValueChanging="MyValueChanging" />
                             </telerik:RadTextBox>
                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="returnsVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtRtnsId"></asp:RequiredFieldValidator>
                </td>
            </tr>
          <tr>
              <td style="color:red; text-align:right">*</td>
              <td><asp:Label ID="lblRtnsCpp" runat="server" CssClass="rdfLabel" Text="Returns " /></td>
              <td>
                  <telerik:RadNumericTextBox ID="txtRtnsCppN" runat="server" EmptyMessage="0.00" MaxValue="9.99" MinValue="0.00" MaxLength="4"  
                        ShowSpinButtons="true" Width="180px" Text='<%# Bind("RTNCPP") %>'>
                      <NumberFormat AllowRounding="false" KeepNotRoundedValue="true"
                      PositivePattern="n" 
                      NumericPlaceHolder="n" DecimalDigits="2" />
                      <IncrementSettings Step="0.01" />
                  </telerik:RadNumericTextBox>
              </td>
              <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="returnsVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtRtnsCppN"></asp:RequiredFieldValidator>
                </td>

          </tr>
          <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="lblDesc" runat="server" CssClass="rdfLabel" Text="Description" /></td>
                        <td>
                             <telerik:RadTextBox ID="txtDesc" runat="server" MaxLength="50" Width="160px" ToolTip="Enter the Description" Text='<%# Bind("DESC") %>' />
                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="returnsVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtDesc"></asp:RequiredFieldValidator>
                </td>
            </tr>

            
          <tr>
                 <td></td>
            </tr>
          <tr>
                <td></td>
                <td></td>
                <td>

                    <asp:Button ID="Button1" causesValidation ="true" ValidationGroup="returnsVG" CssClass="btn btn-primary" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' 
                        Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>' />
                    &nbsp;
                    <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" /><p></p>
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
