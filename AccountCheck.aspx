<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountCheck.aspx.cs" Inherits="PrepumaWebApp.AccountCheck" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
     <style type="text/css">
    .RadGrid 
{
   border-radius: 10px;
   overflow: hidden;
}
    .RadUploadProgressArea {
    position: relative;
    top: 5px;
}
        </style>
    <link href="styles.css" rel="stylesheet" type="text/css" /> 
    <script src="scripts.js" type="text/javascript"></script>



    
   <div style="width: 853px">

           <div>
         <p></p> <p></p><div id="selectedMenu"><b>Account Check </b>
    <a id="HeaderLink" href="#" onclick="return false;">
       <img src="Images/help-icon16.png" />
    </a>
            <br /><i>The Account check screen is used to compare accounts in an Excel spreadsheet to identify new accounts.</i>
              <hr /><p></p>
        </div>

    <telerik:RadAjaxPanel ID="pnlSuccess" runat="server" Visible="false" >
      <div class="alert alert-success" role="alert">
        <asp:Label ID="lblSuccess" Cssclass="alert-link" runat="server" ></asp:Label>
      </div>
     </telerik:RadAjaxPanel>
       <telerik:RadAjaxPanel  ID="pnlInfo" runat="server" Visible="false" >
        <div class="alert alert-info" role="alert">
          <asp:Label id="lblInfo" Cssclass="alert-link" runat="server" ></asp:Label>
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
        <telerik:RadToolTip ID="RadToolTip2" runat="server" Width="200px" ShowEvent="OnClick"
          TargetControlID="HeaderLink" IsClientID="true" HideEvent="LeaveToolTip" Position="Center"
           Animation="Resize" ShowDelay="0" RelativeTo="Element" Text="Use the Account Check Screen to compare accounts in an Excel spreadsheet to identify new account numbers.  The Excel spreadsheet must have a column named <i>account</i>.">
     </telerik:RadToolTip>

     

           <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" />


          <table>
              <tr><td>    
                   
                  
                  <asp:Label ID="lblFileType" runat="server" Text="Browse to select Excel file. Allowed file types: xls, xlsx" /><p></p>
                  <asp:Label ID="Label1" runat="server" Text="Choose any Excel file with a column labeled <i>Account</i>.  You may have one column labeled <i>Account</i>, or several columns at least one of which is labeled <i>Account</i>." /><p></p>
                  <asp:FileUpload ID="ExcelFile" runat="server" ValidateRequestMode="Enabled" Width="497px"   /> 
                  <asp:RegularExpressionValidator
                    id="RegularExpressionValidator1"
                    runat="server"
                    ErrorMessage="Only Excel Files Allowed" style="color: red"
                    ValidationExpression ="^.+(.xls|.xlsx)$"
                    ControlToValidate="ExcelFile">
                    </asp:RegularExpressionValidator>
                  <asp:Label ID="lblFilePath" runat="server" Text="" />
              
                  <p></p>
               </td></tr>
                <tr><td>
                  Template Layout:<br />
                    <asp:Image ID="imgLayoutLogistics" runat = "server" ImageUrl="~/images/AccountTemplate.png" AlternateText=""/>  <p></p>
              </td></tr>
                    
              <tr><td>
                  <asp:Button ID="btnUpload" runat="server" Text="Check Accounts" OnClick="btnUpload_Click"  CssClass ="btn btn-primary"  ValidationGroup="group1" />
              </td></tr>
              <tr><td>
                   <div class="demo-container size-narrow">
                   <telerik:RadProgressManager ID="RadProgressManager1" runat="server" />
                   
                   <telerik:RadProgressArea RenderMode="Lightweight" ID="RadProgressArea1" runat="server" Width="500px" />
                   </div>

            </td></tr>
              <tr>
                  <td>


                <telerik:RadGrid ID="grid" runat="server" AutoGenerateColumns="false">
                          <ExportSettings
   HideStructureColumns="true"
   ExportOnlyData="true"
   IgnorePaging="true"
   OpenInNewWindow="true">
</ExportSettings>
                        <MasterTableView CommandItemDisplay="Top" >
                            <CommandItemSettings ShowExportToExcelButton="True" ShowRefreshButton="False" ShowAddNewRecordButton="false" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="" HeaderText="New Accounts">
                            </telerik:GridBoundColumn>
                        </Columns>
                        </MasterTableView>
                    </telerik:RadGrid> 





                  </td>
              </tr>
            
          </table>
      
     </div>
    


            </div>
</asp:Content>
