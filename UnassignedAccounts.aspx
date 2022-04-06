<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnassignedAccounts.aspx.cs" Inherits="PrepumaWebApp.UnassignedAccounts" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgAccount">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgAccount" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
     <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                <script type="text/javascript">
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
                        popUp.style.top = ((gridHeight - 175 - popUpHeight) / 2 + sender.get_element().offsetTop).toString() + "px";
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
   
 <div style="width: 853px">
      
    
    <br />
      

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
     <telerik:RadToolTip ID="RadToolTip2" runat="server" Width="200px" ShowEvent="OnClick"
          TargetControlID="HeaderLink" IsClientID="true" HideEvent="LeaveToolTip" Position="Center"
           Animation="Resize" ShowDelay="0" RelativeTo="Element" Text="Use this Exception Screen to find Accounts that do not have Sales Information fully set up.<p/> Click the edit link to add Sales Info for an Account number.<p/>The filter can be used to search for existing accounts.">
     </telerik:RadToolTip>
        <div id="selectedMenu"><asp:Label ID="lblType" runat="server" Text="Accounts With No Sales Information"></asp:Label>
    <a id="HeaderLink" href="#" onclick="return false;">
       <img src="Images/help-icon16.png" />
    </a>
            <br /><i>The Exception screen is used to view accounts that do not have Sales Information set up.</i>

        </div>

         <telerik:RadDropDownList RenderMode="Lightweight" ID="RadDropDownType" runat="server"  DropDownHeight="50px" Width="250px" Skin="Silk" 
                DefaultMessage="Select a Report Type" DropDownWidth="250px" OnSelectedIndexChanged="RadDropDownType_SelectedIndexChanged" AutoPostBack="true" 
                DataTextField="ExceptionName" DataValueField="ExceptionID">
          </telerik:RadDropDownList>
                
            <p></p>


        <telerik:RadGrid ID="rgAccount" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" Width="1093px" AllowPaging="True" ShowStatusBar="true"
            OnNeedDataSource="rgAccount_NeedDataSource" AllowFilteringByColumn="True"  OnUpdateCommand="rgAccount_UpdateCommand" OnItemCommand="rgAccount_ItemCommand"
            AllowAutomaticDeletes="True" AllowAutomaticInserts="False" AllowAutomaticUpdates="False" OnItemDataBound="rgAccount_ItemDataBound"  >
            <GroupingSettings CaseSensitive="false" />
            <ClientSettings>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
             <ClientEvents OnPopUpShowing="PopUpShowing" />
           </ClientSettings>
           <ExportSettings ExportOnlyData="True" IgnorePaging="true" OpenInNewWindow="true">
                     <Excel Format="ExcelML" />
            </ExportSettings>
            <MasterTableView Width="100%" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage" AutoGenerateColumns="False" DataKeyNames="Acctnbr" EditMode="PopUp" >
                 <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false"  />
                <RowIndicatorColumn Visible="False">
                </RowIndicatorColumn>
                <ExpandCollapseColumn Created="True">
                </ExpandCollapseColumn>
                <Columns>
                     <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="Edit">
                        <HeaderStyle Width="36px"></HeaderStyle>
                    </telerik:GridEditCommandColumn>
                     <telerik:GridBoundColumn DataField="ClientID" FilterControlAltText="Filter ClientID column" Visible="false" HeaderText="ClientID" SortExpression="ClientID" UniqueName="ClientID" DataType="System.Double">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ClientName" Visible="true" FilterControlAltText="Filter ClientName column" HeaderText="Client Name" SortExpression="ClientName" UniqueName="ClientName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ContractID" DataType="System.Double" Visible="false" FilterControlAltText="Filter ContractID column" HeaderText="ContractID" SortExpression="ContractID" UniqueName="ContractID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ContractNumber" Visible="true" FilterControlAltText="Filter ContractNumber column" HeaderText="ContractNumber" SortExpression="ContractNumber" UniqueName="ContractNumber">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="ContractName" FilterControlAltText="Filter ContractName column" HeaderText="ContractName" SortExpression="ContractName" UniqueName="ContractName" Visible="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="AcctID" DataType="System.Double" Visible="false" FilterControlAltText="Filter AcctID column" HeaderText="AcctID" SortExpression="AcctID" UniqueName="AcctID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Acctnbr" FilterControlAltText="Filter Acctnbr column" HeaderText="Account Number" SortExpression="Acctnbr" UniqueName="Acctnbr">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Expr1" FilterControlAltText="Filter Expr1 column" HeaderText="Control Branch" SortExpression="Expr1" UniqueName="Expr1" Visible="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Territory" FilterControlAltText="Filter Territory column" HeaderText="Territory" SortExpression="Territory" UniqueName="Terrirtory" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SalesType" FilterControlAltText="Filter SalesType column" HeaderText="SalesType" SortExpression="SalesType" UniqueName="SalesType" Visible="false">
                    </telerik:GridBoundColumn>
                    <%--adding 4 new feilds--%>
                    <telerik:GridBoundColumn DataField="gatewayID" Visible="false" FilterControlAltText="Filter SalesType column" HeaderText="gatewayID" SortExpression="gatewayID" UniqueName="gatewayID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="MATHANDCPPID" Visible="false" FilterControlAltText="Filter SalesType column" HeaderText="MATHANDCPPID" SortExpression="MATHANDCPPID" UniqueName="MATHANDCPPID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RTNSID" Visible="false" FilterControlAltText="Filter SalesType column" HeaderText="RTNSID" SortExpression="RTNSID" UniqueName="RTNSID">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="CDCPLBID" Visible="false" FilterControlAltText="Filter SalesType column" HeaderText="CDCPLBID" SortExpression="CDCPLBID" UniqueName="CDCPLBID">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="BusinessType" FilterControlAltText="Filter BusinessType column" HeaderText="BusinessType" SortExpression="BusinessType" UniqueName="BusinessType" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="LocalSRID" FilterControlAltText="Filter LocalSRID column" HeaderText="LocalSRID" SortExpression="LocalSRID" UniqueName="LocalSRID" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="StrategicSRID" FilterControlAltText="Filter StrategicSRID column" HeaderText="StrategicSRID" SortExpression="StrategicSRID" UniqueName="StrategicSRID" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ProductType" Visible="false" FilterControlAltText="Filter ProductType column" HeaderText="ProductType" SortExpression="ProductType" UniqueName="ProductType">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CCID" Visible="false" HeaderText="CCID" SortExpression="CCID" UniqueName="CCID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ISP" Visible="false" FilterControlAltText="Filter ISP column" HeaderText="ISP" SortExpression="ISP" UniqueName="ISP">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RelationshipName" Visible="false" FilterControlAltText="Filter RelationshipName column" HeaderText="RelationshipName" SortExpression="RelationshipName" UniqueName="RelationshipName">
                    </telerik:GridBoundColumn>
                    <telerik:GridCheckBoxColumn DataField="ISPpaid" Visible="false" DataType="System.Boolean" FilterControlAltText="Filter ISPpaid column" HeaderText="ISPpaid" SortExpression="ISPpaid" UniqueName="ISPpaid">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridCheckBoxColumn DataField="LostBiz" Visible="false" DataType="System.Boolean" FilterControlAltText="Filter LostBiz column" HeaderText="LostBiz" SortExpression="LostBiz" UniqueName="LostBiz">
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridBoundColumn DataField="Region" FilterControlAltText="Filter Region column" HeaderText="Region (Account)" SortExpression="Region" UniqueName="Region" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ShippingLocation" Visible="false" FilterControlAltText="Filter ShippingLocation column" HeaderText="ShippingLocation" SortExpression="ShippingLocation" UniqueName="ShippingLocation">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EstAnnualRev" Visible="false" DataType="System.Single" FilterControlAltText="Filter EstAnnualRev column" HeaderText="EstAnnualRev" SortExpression="EstAnnualRev" UniqueName="EstAnnualRev">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EstMarginPct" Visible="false" DataType="System.Single" FilterControlAltText="Filter EstMarginPct column" HeaderText="EstMarginPct" SortExpression="EstMarginPct" UniqueName="EstMarginPct">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SecondaryNAICS" Visible="false" FilterControlAltText="Filter SecondaryNAICS column" HeaderText="SecondaryNAICS" SortExpression="SecondaryNAICS" UniqueName="SecondaryNAICS">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Source" Visible="false" FilterControlAltText="Filter Source column" HeaderText="Source" SortExpression="Source" UniqueName="Source">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SharePercent" Visible="false" DataType="System.Single" FilterControlAltText="Filter SharePercent column" HeaderText="SharePercent" SortExpression="SharePercent" UniqueName="SharePercent">
                    </telerik:GridBoundColumn>
                    
                      
                </Columns>
                <EditFormSettings UserControlName="ClientInfoDetails.ascx" EditFormType="WebUserControl" FormStyle-Height="700" PopUpSettings-Height="680px" PopUpSettings-Width="750px" >
                    <EditColumn ButtonType="ImageButton" UniqueName="EditCommandColumn1" ></EditColumn>
                    <PopUpSettings Modal="true"/>
                </EditFormSettings>
                <PagerStyle AlwaysVisible="True" />
            </MasterTableView>
            <PagerStyle AlwaysVisible="True" />
        </telerik:RadGrid>

  

    </div>
</asp:Content>