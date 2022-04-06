<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditRegion.ascx.cs" Inherits="PrepumaWebApp.EditRegion" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
<asp:Panel runat="server" ID="panel1">
    <div style="margin: 20px 30px 60px 10px;  padding-left:20px">
      <table>
        <%--<tr>
            <td colspan="4" style="height: 15px"></td>
        </tr>--%>
    <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="lblAirport" runat="server" CssClass="rdfLabel" Text="Airport "/></td>
                        <td>
                             <telerik:RadTextBox ID="txtAirport" runat="server" MaxLength="3"  Width="160px" ToolTip="Enter the Airport Code in 3 letters" CssClass="upper" Text='<%# Bind("Airport") %>'>
                                <ClientEvents OnValueChanging="MyValueChanging" /> 
                             </telerik:RadTextBox>
                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("Airport") %>' />
                            <telerik:RadComboBox ID="RadComboBox1" runat="server" EmptyMessage="select Airport" DataTextField="Airport" DataValueField="Airport" Visible="false"  ></telerik:RadComboBox>
                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="regionVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtAirport"></asp:RequiredFieldValidator>
                </td>
            </tr>
           <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="Label2" runat="server" CssClass="rdfLabel" Text=" Branch Manager " /></td>
                        <td>
                             <telerik:RadTextBox ID="txtBranchManager" runat="server" Width="160px" ToolTip="Enter the Branch Manager Name" Text='<%# Bind("BranchManager") %>'></telerik:RadTextBox>
                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="regionVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtBranchManager" ></asp:RequiredFieldValidator>
                </td>
            </tr>
          <tr>
               <%-- <td style="color:red; text-align:right">*</td>--%>
              <td></td>
                <td><asp:Label ID="Label3" runat="server" CssClass="rdfLabel" Text="Cost Center " /></td>
                        <td>
                             <telerik:RadNumericTextBox ID="txtCostCenter" runat="server" Width="160px" ToolTip="Enter the Cost Center" MaxLength="7" MinValue="1000000" Text='<%# Bind("CostCenter") %>'>
                                 <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true"   KeepNotRoundedValue="false"  />
                             </telerik:RadNumericTextBox>
                        </td>
                <td>
                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="regionVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="txtCostCenter"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
          <tr>
                <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="Label4" runat="server" CssClass="rdfLabel" Text=" Jusrisdiction " /></td>
                        <td style="vertical-align:bottom">
                            <telerik:RadComboBox ID="cbxEditJusrisdiction" runat="server" Width="160px" ToolTip="Select the Jusrisdiction" Filter="StartsWith" EmptyMessage="Select the Jusrisdiction"
                                 OnSelectedIndexChanged="cbxEditJusrisdiction_SelectedIndexChanged" AutoPostBack="true"></telerik:RadComboBox>
                              <asp:HiddenField ID="hdJusrisdiction" runat="server" Value='<%# Bind("Jusrisdiction") %>' /> 

                        </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="regionVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="cbxEditJusrisdiction"></asp:RequiredFieldValidator>
                </td>
            </tr>
          <tr>
                <td style="color:red; text-align:right"></td>
                <td><asp:Label ID="Label5" runat="server" CssClass="rdfLabel" Text=" Cost Center Location " /></td>
                         <td>
                            <telerik:RadComboBox ID="cbxCCLocation" runat="server" Width="160px" ToolTip="Select the Cost Center Location" DataTextField="city" DataValueField="city" Filter="StartsWith" AppendDataBoundItems="true" EmptyMessage="Select CostCenter Location"></telerik:RadComboBox>
                             <asp:HiddenField ID="hdCCLocation" runat="server" Value='<%# Bind("CostCenterLocation") %>' />
                        </td>
                <td>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="regionVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="cbxCCLocation"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
          <tr>
               <td style="color:red; text-align:right">*</td>
                <td><asp:Label ID="Label6" runat="server" CssClass="rdfLabel" Text=" District " /></td>
                        <td>
                            <telerik:RadComboBox ID="cbxDistrict" runat="server" Width="160px" ToolTip="Select the District" EmptyMessage="Select the District"></telerik:RadComboBox>
                            <asp:HiddenField ID="hdDistrict" runat="server" Value='<%# Bind("District") %>' />
                             <telerik:RadTextBox ID="txtDistrict" runat="server"  Width="150px" MaxLength="50" Visible="false" ToolTip="Enter the District Name "/>
                        </td>
                <td>
                      <asp:ImageButton ID="imgAddAccount" class="btnAddNew" runat="server" ImageUrl="~/Images/plus-icon.png" Height="20px" Width="20px" OnClick="imgAddAccount_Click" />
                    <asp:ImageButton ID="ibAccountminus" runat="server" ImageUrl="~/Images/minus-icon.png" Height="20px" Visible="false" Width="20px" OnClick="ibAccountminus_Click"  />             
                    <asp:RequiredFieldValidator ID="rfacctypeplus" runat="server" ValidationGroup="regionVG" ErrorMessage="Required" ForeColor="Red" ControlToValidate="cbxDistrict"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator runat="server" ID="rfacctypeminus" ControlToValidate="txtDistrict" ErrorMessage="required" ForeColor="Red" Enabled="false" ValidationGroup="regionVG"></asp:RequiredFieldValidator>
  
                </td>
                 </tr>
          <tr>
              <td></td>
                       <td><asp:Label ID="Label1" runat="server" CssClass="rdfLabel" Text=" District Manager " /></td>
                        <td>
                            <telerik:RadTextBox ID="txtDistManager" runat="server" Text='<%# Bind("DistrictManager") %>'></telerik:RadTextBox>
                        </td>
                <td>
                                   </td>
                 </tr>

           <tr>
                <td></td>
            <td><asp:Label ID="Label8" runat="server" CssClass="rdfLabel" Text="Stack Report?"  /></td>
            <td>
                <telerik:RadButton ID="StackRptChk" runat="server" ToggleType="CheckBox" ButtonType="LinkButton" Checked='<%# Eval("StackReport") == DBNull.Value ? false : Convert.ToBoolean(Eval("StackReport")) %>' AutoPostBack="false">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Yes" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox" />
                    </ToggleStates>
                </telerik:RadButton>

            </td>
                <td></td>
            </tr>
          <tr><td></td></tr>

          <tr>
                <td></td>
            <td><asp:Label ID="Label7" runat="server" CssClass="rdfLabel" Text="Reported?"  /></td>
            <td>
                <telerik:RadButton ID="ReportedChk" runat="server" ToggleType="CheckBox" ButtonType="LinkButton" Checked='<%# Eval("Reported") == DBNull.Value ? false : Convert.ToBoolean(Eval("Reported")) %>' AutoPostBack="false">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Yes" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox" />
                    </ToggleStates>
                </telerik:RadButton>

            </td>
                <td></td>
            </tr>
          <tr><td></td></tr>
          
          <tr>
                <td></td>
                <td></td>
                <td>

                    <asp:Button ID="Button1" causesValidation ="true" ValidationGroup="regionVG" CssClass="btn btn-primary" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' 
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
     <style>
        .upper
            {
                text-transform:uppercase;
            }
    </style>
        
    </div>
</asp:Panel>

