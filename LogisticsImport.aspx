<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LogisticsImport.aspx.cs" Inherits="PrepumaWebApp.LogisticsImport" %>

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
           Animation="Resize" ShowDelay="0" RelativeTo="Element" Text="Use the Logistics Import Screen to import logistics accounts from an Excel spreadsheet.<p/> Click the Choose File button to navigate to the Excel file to import.<p/>Click the upload file to import the data.  <p/>The accounts must be in the Account Table before they can be imported.">
     </telerik:RadToolTip>
    <div>
         <p></p> <p></p><div id="selectedMenu"><b>Logistics Import </b>
    <a id="HeaderLink" href="#" onclick="return false;">
       <img src="Images/help-icon16.png" />
    </a>
            <br /><i>The Logistics screen is used to import Logistics accounts into the Contract Classification and Contract Account Classification tables.</i>
              <hr /><p></p>
        </div>
     

           <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" />


          <table>
              <tr><td>    
                  <strong>Allowed file types:</strong> xls, xlsx<p></p>
                  <asp:FileUpload ID="LogisticsFile" runat="server" ValidateRequestMode="Enabled" Width="497px"   /> 
                  <asp:RegularExpressionValidator
                    id="RegularExpressionValidator1"
                    runat="server"
                    ErrorMessage="Only Excel Files Allowed" style="color: red"
                    ValidationExpression ="^.+(.xls|.xlsx)$"
                    ControlToValidate="LogisticsFile">
                    </asp:RegularExpressionValidator>
                  <asp:Label ID="lblFilePath" runat="server" Text="" />
              
                  <p></p>
               </td></tr>
              <tr><td>
                  Template Layout:<br />
                    <asp:Image ID="imgLayoutLogistics" runat = "server" ImageUrl="~/images/LogisticsTemplate.png" AlternateText=""/>  <p></p>
              </td></tr>
              <tr><td>
                  <p></p>
                  <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click"  CssClass ="btn btn-primary"  ValidationGroup="group1" />
              </td></tr>
              <tr><td>
                   <div class="demo-container size-narrow">
                   <telerik:RadProgressManager ID="RadProgressManager1" runat="server" />
                   
                   <telerik:RadProgressArea ID="RadProgressArea1" runat="server" Width="500px" />
                   </div>
            </td></tr>
            
          </table>
      
     </div>
    


            </div>
</asp:Content>
