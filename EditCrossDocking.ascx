<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditCrossDocking.ascx.cs" Inherits="PrepumaWebApp.EditCrossDocking" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Panel runat="server" ID="panel1">
    <div style="margin: 20px 30px 60px 10px;  padding-left:20px">
      <table>
        <tr>
            <td colspan="4" style="height: 15px"></td>
        </tr>
    <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="lblCdId" runat="server" CssClass="rdfLabel" Text="Cross Docking ID" /></td>
                        <td>
                             <telerik:RadTextBox ID="txtCdId" runat="server" MaxLength="1"  Width="160px" ToolTip="Enter the  Cross Docking ID" CssClass="upper" Text='<%# Bind("CDCPLBID") %>'>
                                <ClientEvents OnValueChanging="MyValueChanging" />
                             </telerik:RadTextBox>
                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="CrossDkVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtCdId"></asp:RequiredFieldValidator>
                </td>
            </tr>
          <tr>
              <td style="color:red; text-align:right">*</td>
              <td><asp:Label ID="lblCrossDckFee" runat="server" CssClass="rdfLabel" Text="Cross Docking Fees " /></td>
              <td>
                  <telerik:RadNumericTextBox ID="txtCrossDckFeeN" runat="server" EmptyMessage="0.0000" MaxValue="9.9999" MinValue="0.0000"  
                        ShowSpinButtons="true" Width="180px" Text='<%# Bind("CDCPLB") %>'>
                      <NumberFormat AllowRounding="false" KeepNotRoundedValue="true"
                      PositivePattern="n" 
                      NumericPlaceHolder="n" DecimalDigits="4" />
                      <IncrementSettings Step="0.0010" />
                  </telerik:RadNumericTextBox>
              </td>
              <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="CrossDkVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtCrossDckFeeN"></asp:RequiredFieldValidator>
                </td>

          </tr>
          <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="lblDesc" runat="server" CssClass="rdfLabel" Text="Description" /></td>
                        <td>
                             <telerik:RadTextBox ID="txtDesc" runat="server" MaxLength="50" Width="160px" ToolTip="Enter the Description" Text='<%# Bind("DESC") %>'/>
                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="CrossDkVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtDesc"></asp:RequiredFieldValidator>
                </td>
            </tr>

            
          <tr>
                <td></td>
            </tr>
          <tr>
                <td></td>
                <td></td>
                <td>

                    <asp:Button ID="Button1" causesValidation ="true" ValidationGroup="CrossDkVG" CssClass="btn btn-primary" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' 
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
