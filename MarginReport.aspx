<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MarginReport.aspx.cs" Inherits="PrepumaWebApp.MarginReport" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnMarginReport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMargin" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="btnMarginPrepare">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMargin" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="btnMarginUpdate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMargin" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadDropDownDate">
                 <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadDropDownDate" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />
                     <telerik:AjaxUpdatedControl ControlID="btnMarginReport" />
                     <telerik:AjaxUpdatedControl ControlID="btnMarginPrepare" />
                     <telerik:AjaxUpdatedControl ControlID="btnMarginUpdate" />
                     <telerik:AjaxUpdatedControl ControlID="lblFinalized" />
                     
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="rgMargin">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgMargin" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>
      <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="40" >
    <asp:Image ID="imgLoading" Style="margin-top: 90px" runat="server" ImageUrl="~/Images/Loading640.gif" height="250px"    
        BorderWidth="0px" AlternateText="Loading" />     
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0
                        ) {
                    args.set_enableAjax(false);
                }
            }
            
            function ConfirmCallbackFn(arg) {
                
                    if (arg == true) {
                        var btn = document.getElementById("<%=btnConfirm.ClientID%>");
                        btn.click();
                    }
            }
         
         function onRequestStart(sender, args)
         {
             if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                     args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                     args.get_eventTarget().indexOf("ExportToCsvButton") >= 0)
             {
                 args.set_enableAjax(false);
             }
         }
  
        </script>
       
    </telerik:RadCodeBlock>
    <telerik:RadToolTip ID="RadToolTip2" runat="server" Width="300px" ShowEvent="OnClick"
                        TargetControlID="HeaderLink" IsClientID="true" HideEvent="LeaveToolTip" Position="Center"
                         Animation="Resize" ShowDelay="0" RelativeTo="Element"
                 Text="Margin report. Data is collected from sources and combined into the Margin Report.  The Update button values the Submitted Date with the current date.">
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
        <div id="selectedMenu" style="padding-left:20px"><b>Margin Report</b>
    <a id="HeaderLink" href="#" onclick="return false;">
       <img src="Images/help-icon16.png" />
    </a>
            <br /><i>Margin report. Data is collected from sources and combined into the Margin Report.  The Update button values the Submitted Date with the current date.</i>
        </div><br />
    
   
    <div style="padding-left:40px; width:853px;">
        <table border="0">
            <tr>
                <td style="width:140px;">Select Fiscal Month</td>
                <td style="width:100px;">
                <telerik:RadDropDownList RenderMode="Lightweight" ID="RadDropDownDate" runat="server"  DropDownHeight="150px" Width="100px" Skin="Silk" 
                DefaultMessage="Select a Fiscal Month" DropDownWidth="100px" AutoPostBack="true" OnSelectedIndexChanged="RadDropDownDate_SelectedIndexChanged"
                DataTextField="FiscalMonth" DataValueField="FiscalMonth">
                </telerik:RadDropDownList>
                </td>
                <td><asp:CheckBox ID="ckYTD" Text="YTD" runat="server" /></td>

                 <td style="padding-left:20px; padding-right:20px;">
                    <asp:Button ID="btnMarginReport" runat="server" Text="View Margin Report" CssClass="btn btn-primary" OnClick="btnMarginReport_Click" ValidationGroup="marginVG"  />
                </td>
               
                <td style="padding-left:5px; padding-right:20px;">
                    <asp:Button ID="btnMarginPrepare" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnMarginPrepare_Click" ValidationGroup="marginVG"  />
                </td>         

                <td style="padding-left:5px; ">
                    <asp:Button ID="btnMarginUpdate" runat="server" Text="Finalize Margin Report" CssClass="btn btn-primary" ValidationGroup="marginVG" OnClick="btnMarginUpdate_Click" />
                    <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" style="display:none"/>
                </td>
                <td><asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="RadDropDownDate" ValidationGroup="esignVG" ForeColor="Red"
                            ErrorMessage="Select Fiscal Month"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>               
                

                <td colspan="6"> 
                     <asp:Label ID="lblFinalized" runat="server" Text="" ForeColor="blue" />
                </td>


            </tr>
        </table>
    </div><br />
    <div>
        <telerik:RadGrid ID="rgMargin" runat="server" GridLines="Both" CellSpacing="-1" AllowFilteringByColumn="true" OnItemCommand="rgMargin_ItemCommand" OnNeedDataSource="rgMargin_NeedDataSource" 
            HeaderStyle-Width="125px" AllowSorting="true" AllowPaging="True" >
            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="True" FileName="MarginReport" OpenInNewWindow="True">
                             
            </ExportSettings>
            <ClientSettings>
                <Scrolling  AllowScroll="true" ScrollHeight="500"/>
            </ClientSettings>
            <MasterTableView AutoGenerateColumns="False" AllowPaging="True" AllowFilteringByColumn="true" CommandItemDisplay="Top" TableLayout="Fixed">
                <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False" ShowExportToExcelButton="True"></CommandItemSettings>
                <Columns>
                    <telerik:GridDateTimeColumn DataField="Fiscal Month" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" EnableTimeIndependentFiltering="true" FilterControlAltText="Filter Fiscal Month column" HeaderText="Fiscal Month" SortExpression="Fiscal Month" UniqueName="FiscalMonth">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="Contract Number" FilterControlAltText="Filter Contract Number column" HeaderText="Contract Number" SortExpression="Contract Number" UniqueName="ContractNumber">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Contract Name" FilterControlAltText="Filter Contract Name column" HeaderText="Contract Name" SortExpression="Contract Name" UniqueName="ContractName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Account" FilterControlAltText="Filter Account column" HeaderText="Account" SortExpression="Account" UniqueName="Account">
                    </telerik:GridBoundColumn>                   
                    <telerik:GridBoundColumn DataField="Product Group" FilterControlAltText="Filter Product Group column" HeaderText="Product Group" SortExpression="Product Group" UniqueName="ProductGroup">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Product Name" FilterControlAltText="Filter Product Name column" HeaderText="Product Name" SortExpression="Product Name" UniqueName="ProductName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Accessorial" FilterControlAltText="Filter Accessorial column" HeaderText="Accessorial" SortExpression="Accessorial" UniqueName="Accessorial">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="USD"  DataFormatString="{0:C}" FilterControlAltText="Filter USD column" HeaderText="USD" SortExpression="USD" UniqueName="USD">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SumofShipments"  DataFormatString="{0:C}" FilterControlAltText="Filter SumofShipments column" HeaderText="SumofShipments" SortExpression="SumofShipments" UniqueName="SumofShipments">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Source" FilterControlAltText="Filter Source column" HeaderText="Source" SortExpression="Source" UniqueName="Source">
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="ImportDate" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" EnableTimeIndependentFiltering="true" FilterControlAltText="Filter ImportDate column" HeaderText="ImportDate" SortExpression="ImportDate" UniqueName="ImportDate">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="Region" FilterControlAltText="Filter Region column" HeaderText="Region" SortExpression="Region" UniqueName="Region">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Margin_Header" FilterControlAltText="Filter Margin_Header column" HeaderText="Margin_Header" SortExpression="Margin_Header" UniqueName="Margin_Header">
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="Date_Submitted" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" EnableTimeIndependentFiltering="true" FilterControlAltText="Filter Date_Submitted column" HeaderText="Date Finalized" SortExpression="Date_Submitted" UniqueName="Date_Submitted">
                    </telerik:GridDateTimeColumn>
                </Columns>
                <PagerStyle AlwaysVisible="True"></PagerStyle>
            </MasterTableView>
        </telerik:RadGrid>
    </div><br />
</asp:Content>