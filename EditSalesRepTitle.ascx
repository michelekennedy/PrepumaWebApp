<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditSalesRepTitle.ascx.cs" Inherits="PrepumaWebApp.EditSalesRepTitle" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>

    <telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="ddlBusinessType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtThresholdN"  LoadingPanelID="RadAjaxLoadingPanel1e" />
                
            </UpdatedControls>
        </telerik:AjaxSetting>
           </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1e" runat="server" />

<div style="margin: 20px 30px 60px 10px;  padding-left:20px">
      <table>
        <tr>
            <td colspan="4" style="height: 15px"></td>
        </tr>
   
       

            <tr>
                <td style="color:red; text-align:right">*</td>
                 <td><asp:Label ID="lblTitle" runat="server" CssClass="rdfLabel" Text="Title:" />
                     
                      <asp:HiddenField ID="hdidSalesRepTitle" runat="server" Value='<%# Bind("idSalesRepTitle") %>' />
                 </td>
                <td>
                     <telerik:RadTextBox ID="txtTitle" runat="server" Width="160px" ToolTip="Enter the  Title" Text='<%# Bind("SalesRepTitle") %>' />
                </td>
                        <td>
                           <asp:RequiredFieldValidator ID="rfSlsRepTitle" runat="server" ValidationGroup="sales" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
                           
                        </td>
               

                <td>                    
                    
                   
                    
                                </td>
            </tr>

           
           
           <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="lblThreshold" runat="server" CssClass="rdfLabel" Text="Threshold:" /></td>
                        <td>
                            <telerik:RadNumericTextBox ID="txtThresholdN" runat="server" Width="160px" ToolTip="Enter the  Threshold" Text='<%# Bind("Threshold") %>' >
                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true"   KeepNotRoundedValue="false"  />
                            </telerik:RadNumericTextBox>
                        </td>
                <td>
                      <asp:RequiredFieldValidator ID="rfThreshold" runat="server" ValidationGroup="sales" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtThresholdN"></asp:RequiredFieldValidator>
                </td>
            </tr>
          
          
        <tr>
                <td></td>
            <td> <asp:Label ID="lblActive" runat="server" CssClass="rdfLabel" Text="ActiveFlag"  /></td>
            <td>
                <telerik:RadButton ID="activeChk" runat="server" ToggleType="CheckBox" ButtonType="LinkButton" AutoPostBack="false" Checked='<%# Eval("ActiveFlag") == DBNull.Value ? false : Convert.ToBoolean(Eval("ActiveFlag")) %>'>
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Yes" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox" />
                    </ToggleStates>
                </telerik:RadButton>

            </td>
               <td></td>
            </tr>
          
          
          <tr>
                <td></td>
                <td></td>
                <td>

                    <asp:Button ID="Button1" causesValidation ="true" ValidationGroup="sales" CssClass="btn btn-primary" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' 
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
    <style>
        .upper
            {
                text-transform:uppercase;
            }
    </style>
        
    </div>
