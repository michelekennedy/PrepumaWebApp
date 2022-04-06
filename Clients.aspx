<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clients.aspx.cs" Inherits="PrepumaWebApp.Clients" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgClient">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgClient" LoadingPanelID="RadAjaxLoadingPanel1" />
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
                        popUp.style.top = ((gridHeight - 375 - popUpHeight) / 2 + sender.get_element().offsetTop).toString() + "px";
                        
                    }
                    function confirmCallBackFn(arg) {
                        //rad confirm returns true then trigger onclick event
                        if (arg == true) {
                            window.location.href = './Contracts.aspx?addmode=true';
                            }
                    }
                    Telerik.Web.UI.RadWindowUtils.Localization =
                                                {
                                                    "OK": "Yes",
                                                    "Cancel": "No",
                                                };
                    function onRequestStart(sender, args) {
                        if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0
                                ) {
                            args.set_enableAjax(false);
                        }
                    }
                </script>
            </telerik:RadCodeBlock>
    



 <div style="width: 853px">
      
    
    <br />
      
     <telerik:RadAjaxPanel ID="pnlsuccess" runat="server" Visible="false">
         <div class="alert alert-success" role="alert">
                <asp:Label ID="lblSuccess" CssClass="alert-link" runat="server"></asp:Label>
            </div>
     </telerik:RadAjaxPanel>


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
           Animation="Resize" ShowDelay="0" RelativeTo="Element" Text="Use the Client Maintenance Screen to enter New Clients that are not in the database.<p/> Click the edit link to edit a client name.<p/>Click the plus sign to add a new client.  <p/>The filter can be used to search for existing clients.">
     </telerik:RadToolTip>
        <div id="selectedMenu"><b>Maintain Clients </b>
    <a id="HeaderLink" href="#" onclick="return false;">
       <img src="Images/help-icon16.png" />
    </a>
            <br /><i>The Client screen is used to enter new client names into the Client Table.</i>
        </div>

        <telerik:RadGrid ID="rgClient" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" Width="1093px" AllowPaging="True" AllowSorting="true" ShowStatusBar="true"
            OnNeedDataSource="rgClient_NeedDataSource" OnUpdateCommand="rgClient_UpdateCommand" 
            OnInsertCommand="rgClient_InsertCommand" AllowFilteringByColumn="True"  OnItemCommand="rgClient_ItemCommand"
            AllowAutomaticDeletes="True" AllowAutomaticInserts="False" AllowAutomaticUpdates="False"  >
            <GroupingSettings CaseSensitive="false" />
            <ClientSettings>
            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                <ClientEvents OnPopUpShowing="PopUpShowing" />
           </ClientSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="true" OpenInNewWindow="true" FileName="Clients">
                     <Excel Format="ExcelML"/>
            </ExportSettings>

            <MasterTableView Width="100%" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage" AutoGenerateColumns="False" DataKeyNames="ClientId" EditMode="PopUp" >
                 <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="true" ShowRefreshButton="false" />
                <RowIndicatorColumn Visible="False">
                </RowIndicatorColumn>
                <ExpandCollapseColumn Created="True">
                </ExpandCollapseColumn>
                <Columns>
                   <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="Edit">
                        <HeaderStyle Width="36px"></HeaderStyle>
                    </telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="ClientId" DataType="System.Int32" FilterControlAltText="Filter ClientId column" HeaderText="ClientId" ReadOnly="True" SortExpression="ClientId" UniqueName="ClientId" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ClientName"  FilterControlAltText="Filter ClientName column" HeaderText="Client Name" SortExpression="ClientName" UniqueName="ClientName" FilterControlWidth="150px" HeaderStyle-Width="50%">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ContractNumber"  FilterControlAltText="Filter ContractNumber column" HeaderText="Contract Number" SortExpression="ContractNumber" UniqueName="ContractNumber" FilterControlWidth="150px" HeaderStyle-Width="50%">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Updatedby"  FilterControlAltText="Filter Updatedby column" HeaderText="Updated by" SortExpression="Updatedby" UniqueName="Updatedby" FilterControlWidth="150px" HeaderStyle-Width="50%">
                    </telerik:GridBoundColumn>
                   <telerik:GridBoundColumn DataField="UpdatedOn"  FilterControlAltText="Filter UpdatedOn column" HeaderText="Updated On" SortExpression="UpdatedOn" UniqueName="UpdatedOn" FilterControlWidth="150px" HeaderStyle-Width="50%">
                    </telerik:GridBoundColumn>
                      
                </Columns>
                <EditFormSettings UserControlName="EditClient.ascx" EditFormType="WebUserControl" PopUpSettings-Height="275px" PopUpSettings-Width="350px" >
                    <EditColumn ButtonType="ImageButton" UniqueName="EditCommandColumn1" ></EditColumn>
                    <PopUpSettings Modal="true"/>
                     <FormCaptionStyle Font-Bold="true" Font-Underline="true" CssClass="EditFormHeader"></FormCaptionStyle>
                </EditFormSettings>
                <PagerStyle AlwaysVisible="True" />
            </MasterTableView>
            <PagerStyle AlwaysVisible="True" />
        </telerik:RadGrid>

  

    </div>
    </asp:Content>
