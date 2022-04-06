<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="PrepumaWebApp.Contracts" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <%--<telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>--%>
           <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                <script type="text/javascript">
                              
                    //function openWindow() {
                    //    window.showModalDialog('Regions.aspx', 'null', 'dialogHeight:430px; dialogWidth:500px; scroll:no;status=no'); return false;
                    //}
                    
                    function RowDblClick(sender, eventArgs) {
                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                    }
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
                    function OpenWin(id, cNumber, acctNum) {
                        window.open('View.aspx?ClientID=' + id + ' & ContractNumber=' + cNumber + '& Acctnbr=' + acctNum, 'null', 'scrollbars =1,width=700,height=650,top=100,left=200');
                        return false;
                    }
                    function onRequestStart(sender, args) {
                        if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0
                                ) {
                            args.set_enableAjax(false);
                        }
                    }
                    function alertCallBackFn(arg) {
                       
                    }

                </script>
            </telerik:RadCodeBlock>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <ClientEvents OnRequestStart="onRequestStart" />
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rgClients">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgClients" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                        <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                        <telerik:AjaxUpdatedControl ControlID="pnlDanger" />

                    </UpdatedControls>

                </telerik:AjaxSetting>
              </AjaxSettings>
        </telerik:RadAjaxManager>
         <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="40" >
    <asp:Image ID="imgLoading" Style="margin-top: 90px" runat="server" ImageUrl="~/Images/Loading640.gif" height="250px"    
        BorderWidth="0px" AlternateText="Loading" />     
    </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="windowManager" runat="server">
        </telerik:RadWindowManager>
         <telerik:RadToolTip ID="RadToolTip2" runat="server" Width="300px" ShowEvent="OnClick"
                        TargetControlID="HeaderLink" IsClientID="true" HideEvent="LeaveToolTip" Position="Center"
                         Animation="Resize" ShowDelay="0" RelativeTo="Element"
                 Text="Use the Filters to Search for Contracts and Accounts. </p>To modify information that appears in the data grid, use the edit link.  </p>To Add a new contract or account, you must use the search criteria drop down boxes to select the client, contact and account, then click the <i>Add new record</i> plus sign.  </p>To view or print account details, click on the <i>Contract number</i> column.</p>Data in the grid can be exported to Excel using the <i>Excel</i> icon at the top right corner of the data grid. </p>">
         </telerik:RadToolTip>
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
    <div>
        <div id="selectedMenu">
            <b>Search Clients /Maintain Sales Info</b>
                <a id="HeaderLink" href="#" onclick="return false;">
                    <img src="Images/help-icon16.png" />
                </a>
             <br /><i>The Search screen is used to view and maintain Sales Information in the Contract Classification and Contract Account Classification tables.</i>
        </div>
    </div>
    <br />
    <div>
        
        <telerik:RadGrid ID="rgClients" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" AllowPaging="True" AllowSorting="true"
            ClientSettings-Scrolling-AllowScroll="true" AllowFilteringByColumn="True" ShowStatusBar="True" OnPreRender="rgClients_PreRender"
            PageSize="20" ShowFooter="True" OnItemCommand="rgClients_ItemCommand" OnNeedDataSource="rgClients_NeedDataSource" OnPageSizeChanged="rgClients_PageSizeChanged"
            OnItemDataBound="rgClients_ItemDataBound" OnInsertCommand="rgClient_InsertCommand" OnUpdateCommand="rgClient_UpdateCommand" >
            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
            <ClientSettings>
                <Scrolling AllowScroll="True" ScrollHeight="550"></Scrolling>
                <ClientEvents OnPopUpShowing="PopUpShowing" />
            </ClientSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="true" OpenInNewWindow="true">
            </ExportSettings>
            <MasterTableView AutoGenerateColumns="False" EditMode="PopUp" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage">
                <CommandItemSettings ShowExportToExcelButton="True" ShowRefreshButton="False" ShowAddNewRecordButton="false" />
                <EditFormSettings UserControlName="ClientInfoDetails.ascx" EditFormType="WebUserControl" FormStyle-Height="700" PopUpSettings-Height="680px" PopUpSettings-Width="750px" >
                    <%--<EditColumn ButtonType="ImageButton"  >
                    </EditColumn>--%>
                    <FormStyle Height="680px"></FormStyle>
                    <PopUpSettings Modal="true"/>
 
                </EditFormSettings>
                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="Edit">
                        <HeaderStyle Width="36px"></HeaderStyle>
                    </telerik:GridEditCommandColumn>
                     <telerik:GridBoundColumn DataField="RelationshipName" Visible="true" FilterControlAltText="Filter RelationshipName column" HeaderText="RelationshipName" SortExpression="RelationshipName" UniqueName="RelationshipName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="MCNumber" Visible="true" FilterControlAltText="Filter MCNumber column" HeaderText="MC Number" SortExpression="MCNumber" UniqueName="MCNumber">
                    </telerik:GridBoundColumn>
                      <telerik:GridBoundColumn DataField="ContractNumber" FilterControlAltText="Filter ContractNumber column" HeaderText="Contract Number" SortExpression="ContractNumber" UniqueName="ContractNumber" DataFormatString="&nbsp;{0}" >
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ContractName" FilterControlAltText="Filter ContractName column" HeaderText="Contract Name" SortExpression="ContractName" UniqueName="ContractName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ClientID" FilterControlAltText="Filter ClientID column" Visible="false" HeaderText="ClientID" SortExpression="ClientID" UniqueName="ClientID" DataType="System.Double">
                    </telerik:GridBoundColumn>
                     <telerik:GridHyperLinkColumn DataTextField="Acctnbr" Target="_parent" NavigateUrl="javascript:void(0);"
                         AllowFiltering="true" FilterControlAltText="Filter Acctnbr column" HeaderText="Account Number" SortExpression="Acctnbr" UniqueName="Acctnbr" DataTextFormatString="&nbsp;{0}">
                    </telerik:GridHyperLinkColumn>
                    <telerik:GridBoundColumn DataField="ClientName" Visible="true" FilterControlAltText="Filter ClientName column" HeaderText="Account Name" SortExpression="ClientName" UniqueName="ClientName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ContractID" DataType="System.Double" Visible="false" FilterControlAltText="Filter ContractID column" HeaderText="ContractID" SortExpression="ContractID" UniqueName="ContractID">
                    </telerik:GridBoundColumn>
                   
                    <telerik:GridBoundColumn DataField="AcctID" DataType="System.Double" Visible="false" FilterControlAltText="Filter AcctID column" HeaderText="AcctID" SortExpression="AcctID" UniqueName="AcctID">
                    </telerik:GridBoundColumn>
                   
                   
                    <telerik:GridBoundColumn DataField="Expr1" FilterControlAltText="Filter Expr1 column" HeaderText="Contract Region" SortExpression="Expr1" UniqueName="Expr1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Territory" FilterControlAltText="Filter Territory column" HeaderText="Territory" SortExpression="Territory" UniqueName="Terrirtory">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SalesType" FilterControlAltText="Filter SalesType column" HeaderText="SalesType" SortExpression="SalesType" UniqueName="SalesType">
                    </telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn DataField="Currency" FilterControlAltText="Filter Currency column" HeaderText="Currency" SortExpression="Currency" UniqueName="Currency">
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn DataField="gateway" Visible="true" FilterControlAltText="Filter Gateway column" HeaderText="Gateway" SortExpression="gateway" UniqueName="gateway">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="MaterialHandling" Visible="true" FilterControlAltText="Filter MaterialHandling column" HeaderText="Material Handling" SortExpression="MaterialHandling" UniqueName="MaterialHandling">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Returns" Visible="true" FilterControlAltText="Filter Returns column" HeaderText="Returns" SortExpression="Returns" UniqueName="Returns">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="CDCPLB" Visible="true" FilterControlAltText="Filter CDCPLB column" HeaderText="CrossDock" SortExpression="CDCPLB" UniqueName="CDCPLB">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="BusinessType" FilterControlAltText="Filter BusinessType column" HeaderText="BusinessType" SortExpression="BusinessType" UniqueName="BusinessType">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="LocalSRID" FilterControlAltText="Filter LocalSRID column" HeaderText="Field SRID" SortExpression="LocalSRID" UniqueName="LocalSRID">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="LocalRepEmployeeID" FilterControlAltText="Filter LocalRepEmployeeID column" HeaderText="LocalRep EmployeeID" SortExpression="LocalRepEmployeeID" UniqueName="LocalRepEmployeeID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="StrategicSRID" FilterControlAltText="Filter StrategicSRID column" HeaderText="Strategic SRID" SortExpression="StrategicSRID" UniqueName="StrategicSRID">
                    </telerik:GridBoundColumn>                    
                     <telerik:GridBoundColumn DataField="StrategicRepEmployeeID" FilterControlAltText="Filter StrategicRepEmployeeID column" HeaderText="StrategicRepEmployeeID" SortExpression="StrategicRepEmployeeID" UniqueName="StrategicRepEmployeeID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ProductType" Visible="true" FilterControlAltText="Filter ProductType column" HeaderText="ProductType" SortExpression="ProductType" UniqueName="ProductType">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CCID" DataType="System.Double" Visible="false" FilterControlAltText="Filter CCID column" HeaderText="CCID" SortExpression="CCID" UniqueName="CCID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ISP" Visible="true" FilterControlAltText="Filter ISP column" HeaderText="ISP" SortExpression="ISP" UniqueName="ISP">
                    </telerik:GridBoundColumn>                   
                    <telerik:GridCheckBoxColumn DataField="ISPpaid" Visible="true" DataType="System.Boolean" FilterControlAltText="Filter ISPpaid column" HeaderText="ISPpaid" SortExpression="ISPpaid" UniqueName="ISPpaid">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridCheckBoxColumn DataField="LostBiz" Visible="true" DataType="System.Boolean" FilterControlAltText="Filter LostBiz column" HeaderText="LostBiz" SortExpression="LostBiz" UniqueName="LostBiz">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridBoundColumn DataField="Region" FilterControlAltText="Filter Region column" HeaderText="Account Region" SortExpression="Region" UniqueName="Region">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ShippingLocation" Visible="true" FilterControlAltText="Filter ShippingLocation column" HeaderText="ShippingLocation" SortExpression="ShippingLocation" UniqueName="ShippingLocation">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EstAnnualRev" Visible="true" DataType="System.Single" DataFormatString="{0:C}" FilterControlAltText="Filter EstAnnualRev column" HeaderText="EstAnnualRev" SortExpression="EstAnnualRev" UniqueName="EstAnnualRev">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EstMarginPct" Visible="true" DataType="System.Single" DataFormatString="{0:P}" FilterControlAltText="Filter EstMarginPct column" HeaderText="EstMarginPct" SortExpression="EstMarginPct" UniqueName="EstMarginPct">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SecondaryNAICS" Visible="true" FilterControlAltText="Filter SecondaryNAICS column" HeaderText="SecondaryNAICS" SortExpression="SecondaryNAICS" UniqueName="SecondaryNAICS">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Source" Visible="true" FilterControlAltText="Filter Source column" HeaderText="Source" SortExpression="Source" UniqueName="Source">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SharePercent" Visible="true" DataType="System.Single" FilterControlAltText="Filter SharePercent column" HeaderText="SharePercent" SortExpression="SharePercent" UniqueName="SharePercent">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="AcctUpdatedby"  FilterControlAltText="Filter AcctUpdatedby column" HeaderText="Acct Updated by" SortExpression="AcctUpdatedby" UniqueName="AcctUpdatedby" FilterControlWidth="150px" HeaderStyle-Width="50%" Visible="true">
                    </telerik:GridBoundColumn>
                   <telerik:GridBoundColumn DataField="AcctUpdatedOn"  FilterControlAltText="Filter AcctUpdatedOn column" HeaderText="Acct Updated On" SortExpression="AcctUpdatedOn" UniqueName="AcctUpdatedOn" FilterControlWidth="150px" HeaderStyle-Width="50%" Visible="true">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="InfoUpdatedby"  FilterControlAltText="Filter InfoUpdatedby column" HeaderText="Info Updated by" SortExpression="InfoUpdatedby" UniqueName="InfoUpdatedby" FilterControlWidth="150px" HeaderStyle-Width="50%" Visible="true">
                    </telerik:GridBoundColumn>
                   <telerik:GridBoundColumn DataField="InfoUpdatedOn"  FilterControlAltText="Filter InfoUpdatedOn column" HeaderText="Info Updated On" SortExpression="InfoUpdatedOn" UniqueName="InfoUpdatedOn" FilterControlWidth="150px" HeaderStyle-Width="50%" Visible="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="StartDate"  FilterControlAltText="Filter StartDate column" HeaderText="Start Date" SortExpression="StartDate" UniqueName="StartDate" FilterControlWidth="150px" HeaderStyle-Width="50%" Visible="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EndDate"  FilterControlAltText="Filter EndDate column" HeaderText="End Date" SortExpression="EndDate" UniqueName="EndDate" FilterControlWidth="150px" HeaderStyle-Width="50%" Visible="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="rolloff_quarter"  FilterControlAltText="Filter rolloff_quarter column" HeaderText="rolloff quarter" SortExpression="rolloff_quartere" UniqueName="rolloff_quarter" FilterControlWidth="150px" HeaderStyle-Width="50%" Visible="true">
                    </telerik:GridBoundColumn>
                      <telerik:GridBoundColumn DataField="MissionCriticalSRID" FilterControlAltText="Filter MissionCriticalSRID column" HeaderText="Mission Critical SRID" SortExpression="MissionCriticalSRID" UniqueName="MissionCriticalSRID">
                    </telerik:GridBoundColumn>
                </Columns>
                <PagerStyle AlwaysVisible="True" />
            </MasterTableView>
            <PagerStyle AlwaysVisible="True" />
        </telerik:RadGrid>
        <br />
    </div>
<style>
        .right {
            float: right;
            width: 50%;
        }
        
    </style>  

</asp:Content>

