<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditProductType.ascx.cs" Inherits="PrepumaWebApp.EditProductType" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Panel runat="server" ID="panel1">
   <div style="margin: 20px 30px 60px 10px;  padding-left:20px">
      <table>
        <tr>
            <td colspan="4" style="height: 15px"></td>
        </tr>
    <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="lblProductType" runat="server" CssClass="rdfLabel" Text="Product Type:" /></td>
                        <td>
                             <telerik:RadTextBox ID="txtProductType" runat="server" MaxLength="25"  Width="160px" ToolTip="Enter the  Product Type" Text='<%# Bind("ProductType") %>'/>
                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="prodtypeVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtProductType"></asp:RequiredFieldValidator>
                </td>
            </tr>
          <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="lblProductDesc" runat="server" CssClass="rdfLabel" Text="Product Description" /></td>
                        <td>
                             <telerik:RadTextBox ID="txtProductDesc" runat="server" MaxLength="50" Width="160px" ToolTip="Enter the Product Description" Text='<%# Bind("ProductDesc") %>'></telerik:RadTextBox>
                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="prodtypeVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtProductDesc"></asp:RequiredFieldValidator>
                </td>
            </tr>
          <tr>
                <td></td>
                <td></td>
                <td>

                    <asp:Button ID="Button1" causesValidation ="true" ValidationGroup="prodtypeVG" CssClass="btn btn-primary" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' 
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
            
          <tr>
                <td></td>
            </tr>
          
    </table>
       
    </div>
</asp:Panel>