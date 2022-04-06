<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountRealignment.aspx.cs" Inherits="PrepumaWebApp.AccountRealignment" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <div>
      <telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <%--<ClientEvents OnRequestStart="onRequestStart" />--%>
        <AjaxSettings>           
            
              <telerik:AjaxSetting AjaxControlID="cbxQtr">
                <UpdatedControls>
                    
                     <telerik:AjaxUpdatedControl ControlID="cbxQtr" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="cbxYear">
                <UpdatedControls>
                    
                     <telerik:AjaxUpdatedControl ControlID="cbxYear" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />
                </UpdatedControls>
            </telerik:AjaxSetting>

         </AjaxSettings>


       
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="40" >
    <asp:Image ID="imgLoading" Style="margin-top: 90px" runat="server" ImageUrl="~/Images/Loading640.gif" width="850px"    
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
                    //function OpenWin(invoiceId) {
                    //    window.open('ViewDetails.aspx?InvoiceID=' + invoiceId, 'null', 'scrollbars =1,width=1100,height=700,top=200,left=600');
                    //    return false;
                    //}
                   <%-- function ConfirmCallbackFn(arg) {

                        if (arg == true) {
                            window.open('ViewTargetData.aspx', 'null', 'scrollbars =1,width=1100,height=700,top=200,left=600');
                        } else {
                            var btn = document.getElementById("<%=btnImport.ClientID%>");
                            btn.style.visibility = 'hidden';

                        }

                    }--%>
                </script>

            </telerik:RadCodeBlock>
    </div>
        <div>
         <p></p> <p></p><div id="selectedMenu"><b>Account Realignment Data Import & Update </b>
    <a id="HeaderLink" href="#" onclick="return false;">
       <img src="Images/help-icon16.png" />
    </a>
            <br />
                 <asp:Label ID="lblDesc" CssClass="alert-link" runat="server" Text="Use the Account Realignment to import the account realignment updates and perform the realignments." Visible="true"></asp:Label>
                  
              <hr /><p></p>
        </div>
        <p></p>
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
           Animation="Resize" ShowDelay="0" RelativeTo="Element" Text="Use the Account Realignment to import the account realignment updates and perform the realignment.  Accounts must be backuped up prior to import. ">
     </telerik:RadToolTip>

        
       
       
        <asp:Label ID="lblLayout" CssClass="alert-link" runat="server" Text="Template Layout:" Visible="true"/>
        <br /><asp:Image ID="imgTemplate" runat = "server" ImageUrl="~/images/RealignTemplate.png" AlternateText=""/>  <p></p>

           <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" /> <p></p>


        
        <table>
              <tr><td>    
                  
                  <asp:Label ID="lblFileTypes" CssClass="alert-link" runat="server" Text="Allowed file types: xls, xlsx" Visible="true"></asp:Label>
                  <asp:FileUpload ID="ImportFile" runat="server" ValidateRequestMode="Enabled" Width="550px"   /> 
                   <asp:RequiredFieldValidator runat=server 
                          ControlToValidate=ImportFile
                          validationgroup="import"
                          ErrorMessage=" *Please Select a File." style="color: red">
                          </asp:RequiredFieldValidator>
                  <br /><asp:Label ID="lblFilePath" runat="server" Text="" />
                  <asp:RegularExpressionValidator
                    id="RegularExpressionValidator1"
                    runat="server"
                    ErrorMessage="Only Excel Files Allowed" style="color: red"
                    ValidationExpression ="^.+(.xls|.xlsx)$"
                    ControlToValidate="ImportFile" validationgroup="import">
                    </asp:RegularExpressionValidator>
                  
              
                  <p></p>
               </td></tr>
           

             
             
              
            
          </table>
        <table border="0">
             <tr>
                <td style="width: 200px;">Select Effective Quarter </td>
                <td style="width: 200px;">
                     <telerik:RadComboBox ID="cbxQtr" runat="server" autopostback="true">
                      
                     </telerik:RadComboBox> 
                </td>
               <td >                   
                   
                </td>
                </tr>
                <tr>
                <td>Select Effective Year </td>
                <td >
                     <telerik:RadComboBox ID="cbxYear" runat="server" autopostback="true"></telerik:RadComboBox> 
                </td>
               <td >                   
                   
                </td>
                </tr>        
            <tr><td td colspan="3">
                   <div class="demo-container size-narrow">
                   <telerik:RadProgressManager ID="RadProgressManager1" runat="server" />
                   
                   <telerik:RadProgressArea ID="RadProgressArea1" runat="server" Width="500px" />
                   </div>
            </td></tr> 
              <tr><td colspan="3">
                 <asp:Button ID="btnCancelImport" runat="server"  causesvalidation="true" Text="Cancel Import" OnClick="btnCancelImport_Click"  CssClass ="btn btn-primary" Visible="false"/>
                  <asp:Button ID="btnSubmit" runat="server"  causesvalidation="true" validationgroup="import" Text="Upload" OnClick="btnSubmit_Click"  CssClass ="btn btn-primary"  />
                  
                 <asp:Button ID="btnRestart" runat="server"  causesvalidation="true" Text="Restart Import" OnClick="btnRestartImport_Click"  CssClass ="btn btn-primary" Visible="false"/>
                 <asp:Button ID="btnImport" runat="server"  causesvalidation="true" Text="Do Import" OnClick="btnImport_Click"  CssClass ="btn btn-primary" Visible="false"/>
                 <asp:Button ID="btnView" runat="server"  causesvalidation="true" Text="Exit" OnClick="btnView_Click"  CssClass ="btn btn-primary" Visible="false"/>
                 
             </td></tr>

            </table>
              

       
      
                    </div>

        
            </div>   



</asp:Content>
