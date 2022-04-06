<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditMaterialHnlg.ascx.cs" Inherits="PrepumaWebApp.EditMaterialHnlg" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Panel runat="server" ID="panel1">
    <div style="margin: 20px 30px 60px 10px;  padding-left:20px">
      <table>
        <tr>
            <td colspan="4" style="height: 15px"></td>
        </tr>
    <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="lblCPPMATHANID" runat="server" CssClass="rdfLabel" Text="Material Handling ID"></asp:Label></td>
                        <td>
                             <telerik:RadTextBox ID="txtCPPMATHANID" runat="server" MaxLength="1"  Width="160px" ToolTip="Enter the Material Handling ID" CssClass="upper" Text='<%# Bind("CPPMATHANID") %>'>
                                 <ClientEvents OnValueChanging="MyValueChanging" />
                             </telerik:RadTextBox>
                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="MaterialHandlingVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtCPPMATHANID"></asp:RequiredFieldValidator>
                </td>
            </tr>
           <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="Label1" runat="server" CssClass="rdfLabel" Text="Material Handling " /></td>
                        <td>
                            <telerik:RadNumericTextBox ID="rtxtCppmathanN" runat="server" ToolTip="Enter the Material Handling" Type="Number" MaxLength="4" MaxValue="9.99" MinValue="0.00" Text='<%# Bind("CPPMATHAN") %>' >
                                <NumberFormat DecimalSeparator="." DecimalDigits="2" KeepNotRoundedValue="true"></NumberFormat>
                            </telerik:RadNumericTextBox>
                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="MaterialHandlingVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="rtxtCppmathanN"></asp:RequiredFieldValidator>
                </td>
            </tr>
          <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="lblDesc" runat="server" CssClass="rdfLabel" Text="Description " /></td>
                        <td>
                             <telerik:RadTextBox ID="txtDesc" runat="server" Width="160px" ToolTip="Enter the Description"  Text='<%# Bind("DESC") %>'/>
                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="MaterialHandlingVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtDesc"></asp:RequiredFieldValidator>
                </td>
            </tr>
            
          <tr>
                <td></td>
            </tr>
          <tr>
                <td></td>
                <td></td>
                <td>

                    <asp:Button ID="Button1" causesValidation ="true" ValidationGroup="MaterialHandlingVG" CssClass="btn btn-primary" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' 
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