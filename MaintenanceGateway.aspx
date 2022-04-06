<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaintenanceGateway.aspx.cs" Inherits="PrepumaWebApp.MaintenanceGateway" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgGateway">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGateway" LoadingPanelID="RadAjaxLoadingPanel1" />
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
                        popUp.style.top = ((gridHeight - 375 - popUpHeight) / 2 + sender.get_element().offsetTop).toString() + "px";
                    }
                    function onRequestStart(sender, args) {
                        if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0
                                ) {
                            args.set_enableAjax(false);
                        }
                    }
                    function callBackFn(arg) {
                        if (arg == true) {
                            this.window.close();
                        }
                    }
                </script>
            </telerik:RadCodeBlock>
     <telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadToolTip ID="RadToolTip2" runat="server" Width="200px" ShowEvent="OnClick"
          TargetControlID="HeaderLink" IsClientID="true" HideEvent="LeaveToolTip" Position="Center"
           Animation="Resize" ShowDelay="0" RelativeTo="Element" Text="Use the Gateway Maintenance Screen to Add/Edit Gateway Information that are not in the database.">
     </telerik:RadToolTip>
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
     <div id="selectedMenu" style="padding-left:20px"><b>Maintain Gateway Information </b>
    <a id="HeaderLink" href="#" onclick="return false;">
       <img src="Images/help-icon16.png" />
    </a></div><br />
    <div>
        <telerik:RadGrid ID="rgGateway" runat="server"  GroupPanelPosition="Top" ShowFooter="True" CellSpacing="-1" GridLines="Both" AllowFilteringByColumn="true" 
            OnInsertCommand="rgGateway_InsertCommand" OnItemCommand="rgGateway_ItemCommand" OnItemDataBound="rgGateway_ItemDataBound" OnNeedDataSource="rgGateway_NeedDataSource">
            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="true" OpenInNewWindow="true" Excel-Format="ExcelML">
            </ExportSettings>
            <MasterTableView AutoGenerateColumns="False" CommandItemDisplay="Top" EditMode="PopUp" InsertItemPageIndexAction="ShowItemOnFirstPage" AllowPaging="True" ShowFooter="False" >
                <CommandItemSettings ShowExportToExcelButton="True" ShowRefreshButton="False" />
                <EditFormSettings UserControlName="EditGateway.ascx" EditFormType="WebUserControl"  PopUpSettings-Height="250px" PopUpSettings-Width="400px">
                    <FormStyle Height="500px"></FormStyle>
                    <PopUpSettings Modal="true" />
                </EditFormSettings>
                <Columns>
                    <telerik:GridBoundColumn DataField="gatewayID" DataType="System.Double" FilterControlAltText="Filter gatewayID column" AllowFiltering="false" HeaderText="gateway ID" SortExpression="gatewayID" UniqueName="gatewayID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="gateway" FilterControlAltText="Filter gateway column" HeaderText="gateway" SortExpression="gateway" UniqueName="gateway">
                    </telerik:GridBoundColumn>
                </Columns>
                <PagerStyle AlwaysVisible="True" />
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
