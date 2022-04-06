<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaintenanceSalesReps.aspx.cs" Inherits="PrepumaWebApp.MaintenanceSalesReps" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgSalesRep">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgSalesRep" LoadingPanelID="RadAjaxLoadingPanel1" />
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
                    function MyValueChanging(sender, args) {
                        args.set_newValue(args.get_newValue().toUpperCase());
                    }
                </script>
            </telerik:RadCodeBlock>
     <telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadToolTip ID="RadToolTip2" runat="server" Width="200px" ShowEvent="OnClick"
          TargetControlID="HeaderLink" IsClientID="true" HideEvent="LeaveToolTip" Position="Center"
           Animation="Resize" ShowDelay="0" RelativeTo="Element" Text="Use the Sales Professional Maintenance Screen to Add/Edit Sales Professional Information that are not in the database.">
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
     <div id="selectedMenu" style="padding-left:20px"><b>Maintain Sales Professional Info </b>
    <a id="HeaderLink" href="#" onclick="return false;">
       <img src="Images/help-icon16.png" />
    </a></div><br />
    <div>
        <telerik:RadGrid ID="rgSalesRep" runat="server" GroupPanelPosition="Top" ShowFooter="True" CellSpacing="-1" GridLines="Both" OnNeedDataSource="rgSalesRep_NeedDataSource" AllowFilteringByColumn="True"
             AllowPaging="True" AllowSorting="true" OnItemCommand="rgSalesRep_ItemCommand" OnItemDataBound="rgSalesRep_ItemDataBound" OnInsertCommand="rgSalesRep_InsertCommand" OnUpdateCommand="rgSalesRep_UpdateCommand">
            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
           <ExportSettings ExportOnlyData="True" IgnorePaging="true" OpenInNewWindow="true" Excel-Format="ExcelML">
            </ExportSettings>
            <MasterTableView AutoGenerateColumns="False" EditMode="PopUp" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage" DataKeyNames="SalesRepID,id" >
                <EditFormSettings UserControlName="EditSalesRep.ascx" EditFormType="WebUserControl"  PopUpSettings-Height="450px" PopUpSettings-Width="400px">
                    <FormStyle Height="600px"></FormStyle>
                    <PopUpSettings Modal="true" />
                </EditFormSettings>
                <CommandItemSettings ShowExportToExcelButton="True" ShowRefreshButton="False" />
                <Columns>
                     <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="Edit">
                        <HeaderStyle Width="36px"></HeaderStyle></telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="SalesRepID" FilterControlAltText="Filter SalesRepID column" HeaderText="SalesRep ID" SortExpression="SalesRepID" UniqueName="SalesRepID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SalesRep" AllowFiltering="true" FilterControlAltText="Filter SalesRep column" HeaderText="SalesRep" SortExpression="SalesRep" UniqueName="SalesRep">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SalesRepTitle" AllowFiltering="true" FilterControlAltText="Filter SalesRepTitle column" HeaderText="SalesRepTitle" SortExpression="SalesRepTitle" UniqueName="SalesRepTitle">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Threshold" AllowFiltering="true" DataType="System.Int32" FilterControlAltText="Filter Threshold column" HeaderText="Threshold" SortExpression="Threshold" UniqueName="Threshold">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="BusinesType" FilterControlAltText="Filter BusinesType column" HeaderText="Busines Type" SortExpression="BusinesType" UniqueName="BusinesType">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="nSMID" FilterControlAltText="Filter nSMID column" HeaderText="NSMID" SortExpression="nSMID" UniqueName="nSMID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="dSM" FilterControlAltText="Filter dSM column" HeaderText="DSM" SortExpression="dSM" UniqueName="dSM">
                    </telerik:GridBoundColumn>
                    <telerik:GridCheckBoxColumn DataField="newBizline" AllowFiltering="true" DataType="System.Boolean" FilterControlAltText="Filter newBizline column" HeaderText="New Biz line?" SortExpression="newBizline" UniqueName="newBizline">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridCheckBoxColumn DataField="StackReport" AllowFiltering="true" DataType="System.Boolean" FilterControlAltText="Filter Stack Report column" HeaderText="Stack Report?" SortExpression="StackReport" UniqueName="StackReport">
                    </telerik:GridCheckBoxColumn>
                     <telerik:GridBoundColumn DataField="EmployeeID" AllowFiltering="true" FilterControlAltText="Filter EmployeeID column" HeaderText="EmployeeID" SortExpression="EmployeeID" UniqueName="EmployeeID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="id" Visible="false" DataType="System.Int32" FilterControlAltText="Filter id column" HeaderText="id" SortExpression="id" UniqueName="id">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="upDatedBy" Visible="false" FilterControlAltText="Filter upDatedBy column" HeaderText="upDatedBy" SortExpression="upDatedBy" UniqueName="upDatedBy">
                    </telerik:GridBoundColumn>
                </Columns>
                <PagerStyle AlwaysVisible="True" />
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
