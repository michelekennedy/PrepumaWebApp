<%@ Page Language="C#"  MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeFile="AccountRevenueCheck.aspx.cs" Inherits="AccountRevenueCheck" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
             <AjaxSettings>           
            
              <telerik:AjaxSetting AjaxControlID="AcctNbr">
                <UpdatedControls>
                    
                     <telerik:AjaxUpdatedControl ControlID="cbxQtr" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="grid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="grid" LoadingPanelID="RadAjaxLoadingPanel1" />          
                </UpdatedControls>
            </telerik:AjaxSetting>

         </AjaxSettings>       
    </telerik:RadAjaxManager>

    <div style="width: 853px">

           <div>
             <p></p> <p></p><div id="selectedMenu"><b>Account Revenue Check </b>
            <a id="HeaderLink" href="#" onclick="return false;">
            <img src="Images/help-icon16.png" />
            </a>
            <br /><i>The Account Revenue Check screen is used to find out if an account has had revenue associated with it.</i>
              <hr /><p></p>
          </div>

                <telerik:RadAjaxPanel ID="pnlSuccess" runat="server" Visible="false" >
      <div class="alert alert-success" role="alert">
        <asp:Label ID="lblSuccess" Cssclass="alert-link" runat="server" ></asp:Label>
      </div>
     </telerik:RadAjaxPanel>
       <telerik:RadAjaxPanel  ID="pnlInfo" runat="server" Visible="false" >
        <div class="alert alert-info" role="alert">
          <asp:Label id="lblInfo" Cssclass="alert-link" runat="server" ></asp:Label>
        </div>
       </telerik:RadAjaxPanel>
      <telerik:RadAjaxPanel ID="pnlDanger" runat="server" Visible="false" >
        <div class="alert alert-danger" role="alert">
          <asp:Label ID="lblDanger" runat="server" CssClass="alert-link" ></asp:Label>
        </div>
       </telerik:RadAjaxPanel>
       <telerik:RadAjaxPanel ID="pnlWarning" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Visible="false">
              <div class="alert alert-warning" role="alert">
                   <asp:Label id="lblWarning" Cssclass="alert-link" runat="server" ></asp:Label>
             </div>
        </telerik:RadAjaxPanel>
        <telerik:RadToolTip ID="RadToolTip2" runat="server" Width="200px" ShowEvent="OnClick"
          TargetControlID="HeaderLink" IsClientID="true" HideEvent="LeaveToolTip" Position="Center"
           Animation="Resize" ShowDelay="0" RelativeTo="Element" Text="Use the Account Revenue Check Utility to see if an account has any revenue associated to it.">
     </telerik:RadToolTip>     

           <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" />
               <table>
                   <tr>
                       <td>Enter Account Number</td>
                       <td>&nbsp;</td>
                       <td>
                           <telerik:RadTextBox  id="AcctNbr" runat="server" autopostback="true"></telerik:RadTextBox>                         
                       </td>
                   </tr>
                   <tr>
                       <td>
                           <asp:Button ID="btnCheck" runat="server" Text="Submit" OnClick="btnCheck_Click"  CssClass ="btn btn-primary"  />
                       </td>
                       <td></td>
                         <td></td>
                   </tr>
                <tr>
                    <td>Contract</td>
                    <td></td>
                    <td> <telerik:RadTextBox  id="ContractNbr" runat="server" autopostback="true" enabled="false"></telerik:RadTextBox> </td>
                </tr>
                     <tr>
                    <td>Min Billing Month</td>
                    <td></td>
                    <td> <telerik:RadTextBox  id="MinRevDate" runat="server" autopostback="true" enabled="false"></telerik:RadTextBox> </td>
                </tr>
                        </tr>
                     <tr>
                    <td>Max Billing Month</td>
                    <td></td>
                    <td> <telerik:RadTextBox  id="MaxRevDate" runat="server" autopostback="true" enabled="false"></telerik:RadTextBox> </td>
                </tr>
               </table>

           


<%--              <table>
              <tr><td>    
                  
                <telerik:RadGrid ID="grid" runat="server" AutoGenerateColumns="false">
                          <ExportSettings
                             HideStructureColumns="true"
                             ExportOnlyData="true"
                             IgnorePaging="true"
                            OpenInNewWindow="true">
                        </ExportSettings>
                        <MasterTableView CommandItemDisplay="Top" >
                            <CommandItemSettings ShowExportToExcelButton="True" ShowRefreshButton="False" ShowAddNewRecordButton="false" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="" HeaderText="Contract"></telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="" HeaderText="MinFiscalMonth"></telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="" HeaderText="MaxFiscalMonth"></telerik:GridBoundColumn>
                        </Columns>
                        </MasterTableView>
                    </telerik:RadGrid> 
               </td></tr>
               </table>               --%>

    </div>
    </div>
 </asp:Content>
