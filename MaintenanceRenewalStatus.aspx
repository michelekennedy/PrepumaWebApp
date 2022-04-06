﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaintenanceRenewalStatus.aspx.cs" Inherits="PrepumaWebApp.MaintenanceRenewalStatus" %>
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
        Contract Renewal Status Maintenance
    <div>
       <h5><asp:Label ID="lblGridtitle" runat="server" ForeColor ="#4b6c9e" Font-Bold="true" Font-Size="Medium"></asp:Label></h5>
    <div>
        <%-- <telerik:RadGrid ID="rgGrid" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" ShowFooter="True" 
           OnNeedDataSource="rgGrid_NeedDataSource" AllowFilteringByColumn="True" OnItemCommand="rgGrid_ItemCommand"
             OnInsertCommand="rgGrid_InsertCommand" OnUpdateCommand="rgGrid_UpdateCommand"
             OnItemDataBound="rgGrid_ItemDataBound" AllowSorting="true">
            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
            <ClientSettings>
                <ClientEvents OnPopUpShowing="PopUpShowing" />
            </ClientSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True" Excel-Format="ExcelML">
                <Excel Format="ExcelML"></Excel>
            </ExportSettings>
       <MasterTableView  AutoGenerateColumns="False" AllowPaging="True" EditMode="PopUp" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage" DataKeyNames="idContractRenewalStatus">
                <CommandItemSettings ShowExportToExcelButton="True" ShowRefreshButton="False" />
                <EditFormSettings UserControlName="EditRenewalStatus.ascx" EditFormType="WebUserControl"  PopUpSettings-Height="350px" PopUpSettings-Width="500px">
                    <FormStyle Height="500px"></FormStyle>
                    <PopUpSettings Modal="true" />
                </EditFormSettings>--%>


             <telerik:RadGrid ID="rgGrid" runat="server" GroupPanelPosition="Top" ShowFooter="True" CellSpacing="-1" GridLines="Both" OnNeedDataSource="rgGrid_NeedDataSource" AllowFilteringByColumn="True"
             AllowPaging="True" AllowSorting="true" OnItemCommand="rgGrid_ItemCommand" OnItemDataBound="rgGrid_ItemDataBound" OnInsertCommand="rgGrid_InsertCommand" OnUpdateCommand="rgGrid_UpdateCommand">
            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
           <ExportSettings ExportOnlyData="True" IgnorePaging="true" OpenInNewWindow="true" Excel-Format="ExcelML">
            </ExportSettings>
            <MasterTableView AutoGenerateColumns="False" EditMode="PopUp" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage" DataKeyNames="idContractRenewalStatus" >
                <EditFormSettings UserControlName="EditRenewalStatus.ascx" EditFormType="WebUserControl"  PopUpSettings-Height="350px" PopUpSettings-Width="500px">
                    <FormStyle Height="600px"></FormStyle>
                    <PopUpSettings Modal="true" />
                </EditFormSettings>
                <CommandItemSettings ShowExportToExcelButton="True" ShowRefreshButton="False" />


                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="Edit">
                        <HeaderStyle Width="36px"></HeaderStyle></telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="idContractRenewalStatus" FilterControlAltText="Filter idContractRenewalStatus column" HeaderText="idContractRenewalStatus" SortExpression="idContractRenewalStatus" UniqueName="idContractRenewalStatus">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ContractRenewalStatus" AllowFiltering="true" FilterControlAltText="Filter ContractRenewalStatus column" HeaderText="Contract Renewal Status" SortExpression="ContractRenewalStatus" UniqueName="ContractRenewalStatus">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="OrderNumber" AllowFiltering="true" FilterControlAltText="Filter OrderNumber column" HeaderText="Order Number" SortExpression="OrderNumber" UniqueName="OrderNumber">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="ActiveFlag" AllowFiltering="true" FilterControlAltText="Filter ActiveFlag column" HeaderText="Active Flag" SortExpression="ActiveFlag" UniqueName="ActiveFlag">
                    </telerik:GridBoundColumn>
                    </Columns>
               
                <PagerStyle AlwaysVisible="True" />
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    </div>
  </div>
</asp:Content>
