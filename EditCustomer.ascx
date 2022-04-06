<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditCustomer.ascx.cs" Inherits="PrepumaWebApp.EditCustomer" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" RelativeTo="Element"
                               Position="MiddleRight" AutoTooltipify="true" ContentScrolling="Default" Width="150"
                               Height="10">
    </telerik:RadToolTipManager>
<telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="txtCustomerName">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtContractNumber"  LoadingPanelID="RadAjaxLoadingPanel1" />
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
            <asp:HiddenField ID="hdCustomerID" runat="server" Value='<%# Bind("idCustomer") %>' />
            <td style="width: 75px"><asp:Label ID="labelCustomer" runat="server" CssClass="rdfLabel" Text="Customer:" />
            </td>
            <td style="width: 200px">                
                <telerik:RadTextBox  ID="txtCustomerName" MaxLength="50" runat="server" Text='<%# Bind("RelationshipName") %>'  Width="250px" ToolTip="Enter the Relationship Name" Visible="true">
                </telerik:RadTextBox>                
            </td>
        </tr>

         <tr>
            <td style="width: 50px; color:red; text-align:right " >*</td>           
            <td style="width: 75px"><asp:Label ID="lblMCNumber" runat="server" CssClass="rdfLabel" Text="MCNumber:" />
            </td>
            <td style="width: 200px">                
                <telerik:RadTextBox  ID="txtMCNumber" MaxLength="50" runat="server" Text='<%# Bind("MCNumber") %>'  Width="250px" ToolTip="Enter the MC Number" Visible="true">
                </telerik:RadTextBox>                
            </td>
        </tr>


        <tr>
            <td></td>
            <td>
                <asp:Label ID="lblActive" runat="server" CssClass="rdfLabel" Text="ActiveFlag" />
            </td>
            <td>
                <telerik:RadButton ID="cbxActive" runat="server" ToggleType="CheckBox" ButtonType="StandardButton" AutoPostBack="false" ToolTip= "ActiveFlag" Checked='<%# Eval("ActiveFlag") == DBNull.Value ? true : Convert.ToBoolean(Eval("ActiveFlag")) %>'>
                                   <ToggleStates>
                                    <telerik:RadButtonToggleState Text="True" PrimaryIconCssClass="rbToggleCheckboxChecked"/>
                                    <telerik:RadButtonToggleState Text="False" PrimaryIconCssClass="rbToggleCheckbox" />
                                 </ToggleStates>
                            </telerik:RadButton>
            </td>
        </tr>
         <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCustomerName" ValidationGroup="CustomerInfo" ErrorMessage="Relationship Name is required." style="color: red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
        
        <tr>
            <td> </td>
            <td> </td>
            <td>
                 <asp:Button CssClass="btn btn-primary" ValidationGroup="CustomerInfo" ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                runat="server" CausesValidation="True" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                 <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False"  CssClass="btn btn-primary" CommandName="Cancel"></asp:Button>  
                                <asp:Button ID="btnAddCustomer" Text="Add Customer" runat="server" CausesValidation="False"  CssClass="btn btn-primary" visible="false"></asp:Button>    
                        
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

