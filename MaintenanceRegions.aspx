<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaintenanceRegions.aspx.cs" Inherits="PrepumaWebApp.MaintenanceRegions" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgRegions">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRegions" LoadingPanelID="RadAjaxLoadingPanel1" />
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
           Animation="Resize" ShowDelay="0" RelativeTo="Element" Text="Use the Regions Maintenance Screen to enter new Region that are not in the database.">
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
        <div id="selectedMenu" style="padding-left:20px"><b>Maintain Regions Info </b>
    <a id="HeaderLink" href="#" onclick="return false;">
       <img src="Images/help-icon16.png" />
    </a></div><br />
    <div>
        <telerik:RadGrid ID="rgRegions" runat="server" GroupPanelPosition="Top" AllowPaging="True" ShowFooter="True" CellSpacing="-1" GridLines="Both" OnNeedDataSource="rgRegions_NeedDataSource" 
            OnItemDataBound="rgRegions_ItemDataBound" AllowSorting="true"
            OnItemCommand="rgRegions_ItemCommand" OnInsertCommand="rgRegions_InsertCommand" OnUpdateCommand="rgRegions_UpdateCommand" AllowFilteringByColumn="True">
            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
            <ClientSettings>
                <ClientEvents OnPopUpShowing="PopUpShowing" />
            </ClientSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="true" OpenInNewWindow="true" Excel-Format="ExcelML">
            </ExportSettings>
            <MasterTableView AutoGenerateColumns="False" DataKeyNames="RegionID" EditMode="PopUp" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage">
                <CommandItemSettings ShowExportToExcelButton="True" ShowRefreshButton="False" />

                <EditFormSettings UserControlName="EditRegion.ascx" EditFormType="WebUserControl"  PopUpSettings-Height="350px" PopUpSettings-Width="400px">
                    <FormStyle Height="500px"></FormStyle>
                    <PopUpSettings Modal="true" />
                </EditFormSettings>
                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="Edit">
                        <HeaderStyle Width="36px"></HeaderStyle>
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="RegionID" Visible="false" HeaderText="RegionID" SortExpression="RegionID" UniqueName="RegionID" DataType="System.Int32" FilterControlAltText="Filter RegionID column"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Airport" HeaderText="Airport" SortExpression="Airport" UniqueName="Airport" FilterControlAltText="Filter Airport column"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="BranchManager" HeaderText="BranchManager" AllowFiltering="true" SortExpression="BranchManager" UniqueName="BranchManager" FilterControlAltText="Filter BranchManager column"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CostCenter" HeaderText="CostCenter" SortExpression="CostCenter" AllowFiltering="true" UniqueName="CostCenter" FilterControlAltText="Filter CostCenter column"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CostCenterLocation" HeaderText="CostCenterLocation" SortExpression="CostCenterLocation" UniqueName="CostCenterLocation" FilterControlAltText="Filter CostCenterLocation column"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Jusrisdiction" HeaderText="Jusrisdiction" SortExpression="Jusrisdiction" UniqueName="Jusrisdiction" FilterControlAltText="Filter Jusrisdiction column"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="District" HeaderText="District" SortExpression="District" UniqueName="District" FilterControlAltText="Filter District column"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="DistrictManager" HeaderText="DistrictManager" SortExpression="DistrictManager" UniqueName="DistrictManager" FilterControlAltText="Filter District column"></telerik:GridBoundColumn>
                    <telerik:GridCheckBoxColumn DataField="Reported" AllowFiltering="false" HeaderText="Reported" SortExpression="Reported" UniqueName="Reported" DataType="System.Boolean" FilterControlAltText="Filter Reported column"></telerik:GridCheckBoxColumn>
                    <telerik:GridCheckBoxColumn DataField="StackReport" AllowFiltering="false" HeaderText="StackReport" SortExpression="StackReport" UniqueName="StackReport" DataType="System.Boolean" FilterControlAltText="Filter StackReport column"></telerik:GridCheckBoxColumn>
                </Columns>
                <PagerStyle AlwaysVisible="True" />
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
