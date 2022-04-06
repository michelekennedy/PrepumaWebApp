<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewUserRoles.aspx.cs" Inherits="PrepumaWebApp.ViewUserRoles" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <link href="styles.css" rel="stylesheet" type="text/css" /> 

      
      <div style="width: 900px">

<telerik:RadAjaxPanel ID="pnlSuccess" runat="server" Visible="false" >
      <div class="alert alert-success" role="alert">
        <asp:Label ID="lblSuccess" Cssclass="alert-link" runat="server" ></asp:Label>
      </div>
     </telerik:RadAjaxPanel>
       <telerik:RadAjaxPanel  ID="pnlInfo" runat="server" Visible="false" >
        <div class="alert alert-info" role="alert">
            <img src="~/Images/check1.jpg" id="check1" runat="server" visible="false"/>
          <asp:Label id="lblInfo" Cssclass="alert-link" runat="server" ></asp:Label>
            <img src="~/Images/check1.jpg" id="check2" runat="server" visible="false"/>
          <asp:Label id="lblInfo2" Cssclass="alert-link" runat="server" ></asp:Label>
            <img src="~/Images/check1.jpg" id="check3" runat="server" visible="false"/>
             <img src="~/Images/redX.jpg" id="uncheck3" runat="server" visible="false"/>
          <asp:Label id="lblInfo3" Cssclass="alert-link" runat="server" ></asp:Label>
            <img src="~/Images/check1.jpg" id="check4" runat="server" visible="false"/>
             <img src="~/Images/redX.jpg" id="uncheck4" runat="server" visible="false"/>
          <asp:Label id="lblInfo4" Cssclass="alert-link" runat="server" ></asp:Label>
        </div>
       </telerik:RadAjaxPanel>
      <telerik:RadAjaxPanel ID="pnlDanger" runat="server" Visible="false" >
        <div class="alert alert-danger" role="alert">
          <asp:Label ID="lblDanger" runat="server" CssClass="alert-link" ></asp:Label>
        </div>
       </telerik:RadAjaxPanel>
       <telerik:RadAjaxPanel ID="pnlWarning" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Visible="false">
              <div class="alert alert-warning" role="alert">
                   <asp:Label id="lblWarning" Cssclass="alert-link" runat="server" ></asp:Label>
             </div>
        </telerik:RadAjaxPanel>

       <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" />

         <telerik:RadToolTip ID="RadToolTip1" runat="server" Width="200px" ShowEvent="OnClick"
          TargetControlID="HeaderLink1" IsClientID="true" HideEvent="LeaveToolTip" Position="Center"
           Animation="Resize" ShowDelay="0" RelativeTo="Element" Text="Use the View User Roles Screen to View User Roles for Application Users<p/> ">
          </telerik:RadToolTip>

         <p></p><div><b>User Roles</b>
                 <a id="HeaderLink1" href="#" onclick="return false;"><img src="Images/help-icon16.png" /></a>
                 <br /><i>The View User Roles screen is used to view roles assigned to application users.</i>
                 <hr />
               </div>

       <%-- begin maintenance--%>
        <telerik:RadGrid ID="test1" runat="server">

        </telerik:RadGrid>

         <telerik:RadGrid ID="rgUsers" runat="server" GridLines="Both" CellSpacing="-1" AllowFilteringByColumn="true" OnNeedDataSource="rgUsers_NeedDataSource" >
            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
            </ExportSettings>
            <ClientSettings>
                <Scrolling  AllowScroll="true" ScrollHeight="450"/>
            </ClientSettings>
            <MasterTableView AutoGenerateColumns="False" AllowPaging="True" EditMode="PopUp" AllowFilteringByColumn="true" CommandItemDisplay="Top" TableLayout="Fixed">
                <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False" ShowExportToExcelButton="True"></CommandItemSettings>
                <EditFormSettings UserControlName="EditUser.ascx" EditFormType="WebUserControl"  PopUpSettings-Height="350px" PopUpSettings-Width="450px">
                    <FormStyle Height="500px"></FormStyle>
                    <PopUpSettings Modal="true" />
                </EditFormSettings>
                <Columns>
                    <telerik:GridBoundColumn DataField="ApplicationName" FilterControlAltText="Filter ApplicationName column" HeaderText="Application Name" SortExpression="ApplicationName" UniqueName="ApplicationName" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="UserName" FilterControlAltText="Filter UserName column" HeaderText="User Name" SortExpression="UserName" UniqueName="UserName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ActiveDirectoryName" FilterControlAltText="Filter ActiveDirectoryName column" HeaderText="Login ID" SortExpression="ActiveDirectoryName" UniqueName="ActiveDirectoryName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RoleName" FilterControlAltText="Filter Role column" HeaderText="Role" SortExpression="RoleName" UniqueName="RoleName">
                    </telerik:GridBoundColumn>
               </Columns>
                <PagerStyle AlwaysVisible="True"></PagerStyle>
            </MasterTableView>
        </telerik:RadGrid>
       <%-- end maintenance--%>

   </div>

</asp:Content>
