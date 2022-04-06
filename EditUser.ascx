<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditUser.ascx.cs" Inherits="PrepumaWebApp.EditUser" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>


<telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" RelativeTo="Element"
                               Position="MiddleRight" AutoTooltipify="true" ContentScrolling="Default" Width="150"
                               Height="10">
    </telerik:RadToolTipManager>

<telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
<asp:Panel ID="Panel1" runat="server" GroupingText=''>
     <table border="0" >
         <tr>
             <td style="height: 10px" colspan="4"></td>
         </tr>
            <tr>
                <td style="width: 10px;"></td>
                <td style="width: 180px;">
                    <b>User Role Maintenance </b>
      
                </td>
                <td style="text-align:right">
                     <asp:Label ID="lblLastUpdated" runat="server" Text='Last Updated by '  Enabled="false" Visible="true">
                     </asp:Label><br />
                </td>
                <td >
                      <asp:Label ID="lblLastUpdatedBy" CssClass="alert-link" runat="server" Text='<%# Bind("role_UpdatedBy") %>' Visible="true"></asp:Label><br />

                </td>
            </tr>
         <tr>
             <td></td>
             <td></td>
             <td></td>
             <td>
                    <asp:Label ID="lblLastUpdatedOn" CssClass="alert-link" runat="server" Text='<%# Bind("role_UpdatedOn") %>' Visible="true"></asp:Label>
              </td>
         </tr>
         
        </table>
    <table>
        
        <tr>
            <td style="height: 40px" colspan="3"></td>
        </tr>
        <tr>
            <td style="width: 30px; color:red; text-align:right " >*</td>
            <td ><asp:Label ID="lblUserName" runat="server" CssClass="rdfLabel" Text="User Name" /></td>
            <td>
                <asp:HiddenField ID="hdnUser" runat="server" Value= '<%# Bind("idEmployee") %>' />
                <asp:HiddenField ID="hdnApplicationUser" runat="server" Value= '<%# Bind("idPI_ApplicationUser") %>' />
                 <telerik:RadComboBox ID="cbxUsers" runat="server" ToolTip="Select User" EmptyMessage="Select User Name" Width="150px" AutoPostBack="true" ></telerik:RadComboBox>
            </td>
            <td>
                <asp:RequiredFieldValidator runat="server" ID="rfuser" ControlToValidate="cbxUsers" ErrorMessage="required" ForeColor="Red" ValidationGroup="Submitgroup"></asp:RequiredFieldValidator>
            </td>
        </tr> 
      
         <tr>
            <td style="width: 30px; color:red; text-align:right " >*</td>
            <td style="width:160px;"><asp:Label ID="lblUserRole" runat="server" CssClass="rdfLabel" Text="User Role" /></td>
            <td style="width:160px;">
                <asp:HiddenField ID="hdnUserRole" runat="server" Value= '<%# Bind("idPI_ApplicationRole") %>' />
                <telerik:RadComboBox ID="cbxUserRole" runat="server" ToolTip="Select Role" EmptyMessage="Select User Role" Width="150px" AutoPostBack="true" ></telerik:RadComboBox>
            </td>
             <td style="padding-top:5px" colspan="3">
                 <asp:RequiredFieldValidator runat="server" ID="rfuserRole" ControlToValidate="cbxUserRole" ErrorMessage="required" ForeColor="Red" ValidationGroup="Submitgroup"></asp:RequiredFieldValidator>
             </td>
        </tr>
        
                <tr>
            <td colspan="2"></td>
            <td><asp:CheckBox ID="CheckBox1" runat="server" Text="Change Password" AutoPostBack="True" oncheckedchanged="CheckBox1_CheckedChanged" Visible="false"/></td>
             <td></td>
        </tr>
        <tr>
            <td style="width: 30px; color:red; text-align:right " ><asp:Label ID="lblpwdReq" runat="server" CssClass="rdfLabel" Text="*" Visible="true"/></td>
             <td ><asp:Label ID="lblUserPwd" runat="server" CssClass="rdfLabel" Text="Password" Visible="false"/></td>
            <td>
                <telerik:RadTextBox ID="txtPWD" TextMode="Password" runat="server"  Width="150px" ToolTip="Enter the Password" MaxLength="25" Visible="true"></telerik:RadTextBox>
            </td>
             <td>

            </td>
        </tr>
        <tr>
            <td style="width: 30px; color:red; text-align:right " ><asp:Label ID="lblpwdReq2" runat="server" CssClass="rdfLabel" Text="*" Visible="true"/></td>
             <td ><asp:Label ID="lblUserPwd2" runat="server" CssClass="rdfLabel" Text="Password Confirm" Visible="false"/></td>
            <td>
                <telerik:RadTextBox ID="txtPWD2" TextMode="Password" runat="server"  Width="150px" ToolTip="Confirm Password" MaxLength="25" Visible="true"></telerik:RadTextBox>
            </td>
             <td>

            </td>
        </tr>  



        <tr>
            <td></td>
        </tr>
        <tr>
            <td> </td>
            <td> </td>
            <td>
                 <asp:Button CssClass="btn btn-primary" ValidationGroup="Submitgroup" ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                 <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False"  CssClass="btn btn-primary" CommandName="Cancel"></asp:Button>    
                        
            </td>
        </tr>
         
    </table>
    <div style="padding-left:10px">
        <p style="color:red"><i >*Required Fields</i></p>
        <asp:Label ID="lblNoExisting" runat="server" CssClass="rdfLabel" Text="You cannot view existing passwords, but you can change a user's password" Visible="false"/>
                <asp:Label ID="lblErrorMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblSuccessMessage" runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>
    </div>
    <style>
        .upper{
            text-transform:uppercase;
        }
        
   
    </style>
</asp:Panel>