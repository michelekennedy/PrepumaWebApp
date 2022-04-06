<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchContractRenewal.aspx.cs" Inherits="PrepumaWebApp.SearchContractRenewal" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
       
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
                        popUp.style.left = ((gridWidth - 300 - popUpWidth) / 2 + sender.get_element().offsetLeft).toString() + "px";
                        popUp.style.top = ((gridHeight - 375 - popUpHeight) / 2 + sender.get_element().offsetTop).toString() + "px";
                    }
                    function OpenWin(idContractRenewal) {
                        window.open('ViewRenewal.aspx?ContractRenewalID=' + idContractRenewal, 'null', 'scrollbars =1,width=850,height=900,top=50,left=50');
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
                    function OpenFile(path) {
                        //window.open(path, 'null', 'scrollbars =1,width=900,height=800,top=200,left=600');
                        window.open(path);
                        return false;
                    }
                    var $ = $telerik.$;

                    function onClientFileUploaded(radAsyncUpload, args) {
                        //var $row = $(args.get_row());
                        //var inputName = radAsyncUpload.getAdditionalFieldID("TextBox");
                        //var inputType = "text";
                        //var inputID = inputName;
                        //var input = createInput(inputType, inputID, inputName);
                        //var label = createLabel(inputID);
                        //$row.append("<br/>");
                        //$row.append(label);
                        //$row.append(input);

                    }

                    function createInput(inputType, inputID, inputName) {
                        var input = '<input type="' + inputType + '" id="' + inputID + '" name="' + inputName + '" />';
                        return input;
                    }

                    function createLabel(forArrt) {
                        var label = '<label for=' + forArrt + '>Enter Description: </label>';
                        return label;
                    }
                    

                </script>
            </telerik:RadCodeBlock>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <ClientEvents OnRequestStart="onRequestStart" />
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rgContracts">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgContracts" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                        <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                        <telerik:AjaxUpdatedControl ControlID="pnlDanger" />

                    </UpdatedControls>

                </telerik:AjaxSetting>
                  <telerik:AjaxSetting AjaxControlID="btnSaveUpload">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgUpload" />     
                    <telerik:AjaxUpdatedControl ControlID="RadAsyncUpload1" />                         
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="rgUpload">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgUpload" />  
                    <telerik:AjaxUpdatedControl ControlID="btnSaveUpload" />  
                    <telerik:AjaxUpdatedControl ControlID="RadAsyncUpload1" />                           
                 </UpdatedControls>
            </telerik:AjaxSetting>
              </AjaxSettings>
        </telerik:RadAjaxManager>
         <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="40" >

    </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="windowManager" runat="server">
        </telerik:RadWindowManager>
         <telerik:RadToolTip ID="RadToolTip2" runat="server" Width="300px" ShowEvent="OnClick"
                        TargetControlID="HeaderLink" IsClientID="true" HideEvent="LeaveToolTip" Position="Center"
                         Animation="Resize" ShowDelay="0" RelativeTo="Element"
                 Text="Use the Filters to Search for Contracts in the Client Logs. </p>To modify information that appears in the data grid, use the edit link.</p>">
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
            <b> Client Logs</b>
                <a id="HeaderLink" href="#" onclick="return false;">
                    <img src="Images/help-icon16.png" />
                </a>
             <br /><i>The Client Log screen is used to view and edit Client Log information.</i><br /><p />
            <asp:Label ID="lblFilterOn" runat="server" CssClass="alert-link"></asp:Label>
        </div>
    </div>
    <br />
    <div>
        
        <telerik:RadGrid ID="rgContracts" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" AllowPaging="True" AllowSorting="true"
            ClientSettings-Scrolling-AllowScroll="true" AllowFilteringByColumn="True" ShowStatusBar="True" OnPreRender="rgContracts_PreRender"
            PageSize="20" ShowFooter="True" OnItemCommand="rgContracts_ItemCommand" OnNeedDataSource="rgContracts_NeedDataSource" OnPageSizeChanged="rgContracts_PageSizeChanged"
            OnItemDataBound="rgContracts_ItemDataBound" OnInsertCommand="rgContracts_InsertCommand" OnUpdateCommand="rgContracts_UpdateCommand" OnDeleteCommand="rgContracts_DeleteCommand">
            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
            <ClientSettings>
                <Scrolling AllowScroll="True" ScrollHeight="550"></Scrolling>
                <ClientEvents OnPopUpShowing="PopUpShowing" />
            </ClientSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="true" OpenInNewWindow="true">
            </ExportSettings>
            <MasterTableView AutoGenerateColumns="False" EditMode="PopUp" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage" DataKeyNames="idContractRenewal">
                <CommandItemSettings ShowExportToExcelButton="True" ShowRefreshButton="False" ShowAddNewRecordButton="true" />
                <EditFormSettings UserControlName="EditContractRenewal.ascx" EditFormType="WebUserControl" PopUpSettings-Height="820px" PopUpSettings-Width="930px" >
                  
                    <FormStyle Height="820px"></FormStyle>
                    <PopUpSettings Modal="true"/>
 
                </EditFormSettings>
                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="Edit">
                    <HeaderStyle Width="36px"></HeaderStyle>
                    </telerik:GridEditCommandColumn>
                    <telerik:GridHyperLinkColumn DataTextField="Relationship" Target="_parent" NavigateUrl="javascript:void(0);" 
                         AllowFiltering="true" FilterControlAltText="Filter Relationship column" HeaderText="Relationship Name" SortExpression="Relationship" UniqueName="Relationship" ItemStyle-ForeColor="Blue">
                         <HeaderStyle Width="300px"></HeaderStyle>
                    </telerik:GridHyperLinkColumn>                

                    <telerik:GridBoundColumn DataField="idContractRewewal" FilterControlAltText="Filter idContractRewewal column" HeaderText="idContractRewewal" SortExpression="idContractRewewal" UniqueName="idContractRewewal" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="idContractRenewalType" FilterControlAltText="Filter idContractRewewalType column" HeaderText="idContractRewewalType" SortExpression="idContractRewewalType" UniqueName="idContractRewewalType" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="idContractRenewalRouting" FilterControlAltText="Filter idContractRenewalRouting column" HeaderText="idContractRenewalRouting" SortExpression="idContractRenewalRouting" UniqueName="idContractRenewalRouting" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="effectiveDate"  DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" EnableTimeIndependentFiltering="true" FilterControlAltText="Filter effectiveDate column" HeaderText="effectiveDate" SortExpression="effectiveDate" UniqueName="effectiveDate" FilterControlWidth="150px" HeaderStyle-Width="50%" Visible="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn DataField="expiryDate" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" EnableTimeIndependentFiltering="true"  FilterControlAltText="Filter expiryDate column" HeaderText="expiryDate" SortExpression="expiryDate" UniqueName="expiryDate" FilterControlWidth="150px" HeaderStyle-Width="50%" Visible="true">
                    <HeaderStyle Width="50px"></HeaderStyle>
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="currentlyRoutedTo"  FilterControlAltText="Filter currentlyRoutedTo column" HeaderText="currentlyRoutedTo" SortExpression="currentlyRoutedTo" UniqueName="currentlyRoutedTo" FilterControlWidth="150px" HeaderStyle-Width="50%" Visible="true">
                    <HeaderStyle Width="100px"></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RenewalType"  FilterControlAltText="Filter RenewalType column" HeaderText="Renewal Type" SortExpression="RenewalType" UniqueName="RenewalType" Visible="true">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="sowFlag"  FilterControlAltText="Filter sowFlag column" HeaderText="sowFlag" SortExpression="sowFlag" UniqueName="sowFlag" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="rateOfChangeFlag"  FilterControlAltText="Filter rateOfChangeFlag column" HeaderText="rateOfChangeFlag" SortExpression="rateOfChangeFlag" UniqueName="rateOfChangeFlag" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="currTargetGrMarginPct"  FilterControlAltText="Filter currTargetGrMarginPct column" HeaderText="currTargetGrMarginPct" SortExpression="currTargetGrMarginPct" UniqueName="currTargetGrMarginPct" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="expTargetGrMarginPct"  FilterControlAltText="Filter expTargetGrMarginPct column" HeaderText="expTargetGrMarginPct" SortExpression="expTargetGrMarginPct" UniqueName="expTargetGrMarginPct" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="newTargetGrMarginPct"  FilterControlAltText="Filter newTargetGrMarginPct column" HeaderText="newTargetGrMarginPct" SortExpression="newTargetGrMarginPct" UniqueName="newTargetGrMarginPct" Visible="false">
                    </telerik:GridBoundColumn>
                       <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete"  HeaderText="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false" ConfirmDialogType="RadWindow" ConfirmText="Delete This Client Log?">
                    <HeaderStyle Width="36px"></HeaderStyle></telerik:GridButtonColumn>   

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
