<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaintenanceRenewalRoutingCC.aspx.cs" Inherits="PrepumaWebApp.MaintenanceRenewalRoutingCC" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgGrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGrid" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                <script type="text/javascript">
                    var popUp;
                    function PopUpShowing(sender, eventArgs) {
                        popUp = eventArgs.get_popUp();
                        var gridWidth = sender.get_element().offsetWidth;
                        var gridHeight = sender.get_element().offsetHeight;
                        var popUpWidth = popUp.style.width.substr(0, popUp.style.width.indexOf("px"));
                        var popUpHeight = popUp.style.height.substr(0, popUp.style.height.indexOf("px"));
                        popUp.style.left = ((gridWidth - popUpWidth) / 2 + sender.get_element().offsetLeft).toString() + "px";
                        popUp.style.top = ((gridHeight - 175 - popUpHeight) / 2 + sender.get_element().offsetTop).toString() + "px";
                    }
                    function onRequestStart(sender, args) {
                        if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0
                                ) {
                            args.set_enableAjax(false);
                        }
                    }
                    function assignCallBackFn(arg) {
                    }
                </script>
            </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
    </div>
    <div style="width: 853px">

        <asp:Panel ID="pnlsuccess" runat="server" Visible="false">
            <div class="alert alert-success" role="alert">
                <asp:Label ID="lblSuccess" CssClass="alert-link" runat="server"></asp:Label>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlInfo" runat="server" Visible="false">
            <div class="alert alert-info" role="alert">
                <asp:Label ID="lblInfo" CssClass="alert-link" runat="server"></asp:Label>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlwarning" runat="server" Visible="false">
            <div class="alert alert-warning" role="alert">
                <asp:Label ID="lblWarning" CssClass="alert-link" runat="server"></asp:Label>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlDanger" runat="server" Visible="false">
            <div class="alert alert-danger" role="alert">
                <asp:Label ID="lblDanger" runat="server" CssClass="alert-link"></asp:Label>

            </div>

        </asp:Panel>
    </div>
    <br />
   
 
    <div class="container">
        Contract Renewal Routing CC Emails - enter emails addresses to CC on each Routing Email, separated by a comma<p></p><hr /><p></p>
   <table border="0">
        
      
        <tr>

            <td ><asp:Label ID="lblRenewalRouteEmail" runat="server" CssClass="rdfLabel" Text="CC Email" />
            </td>
                        <td style="width: 30px; color:red; text-align:right " ></td>
            <td>
                 <asp:TextBox id="txtRoutingCCemail" TextMode="multiline" Columns="80" Rows="1" runat="server"  Text='' Visible="true" />
            </td>
            <td>
                
                <p></p><hr /><p></p>
            </td>
        </tr>
        
       
       
        
        <tr>
            <td></td>
        </tr>
        <tr>
            <td> </td>
            <td> </td>
            <td>
                 <asp:Button CssClass="btn btn-primary" ValidationGroup="Submitgroup" ID="Button1" Text='Save' runat="server" OnClick="updateEmails"></asp:Button>&nbsp;
                 <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False"  CssClass="btn btn-primary"  OnClick="cancelcode"></asp:Button>  
                               
                        
            </td>
        </tr>
         
    </table>



    </div>
    </div>
  </div>
</asp:Content>