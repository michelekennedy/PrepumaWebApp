<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QuarterEndAcctSave.aspx.cs" Inherits="PrepumaWebApp.QuarterEndAcctSave" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
           
             <telerik:AjaxSetting AjaxControlID="btnSubmit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnSubmit" LoadingPanelID="RadAjaxLoadingPanel1" />      
                     <telerik:AjaxUpdatedControl ControlID="btnCancel" />
                     <telerik:AjaxUpdatedControl ControlID="btnContinue" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />
                </UpdatedControls>
            </telerik:AjaxSetting>

             <telerik:AjaxSetting AjaxControlID="btnContinue">
                <UpdatedControls>
                    
                     <telerik:AjaxUpdatedControl ControlID="btnContinue" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />
                </UpdatedControls>
            </telerik:AjaxSetting>

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
    
     <div style="margin-right: auto; margin-left: auto; width: 800px;">

   <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
        <asp:Image ID="imgLoading" Style="margin-right: auto; margin-left: auto; width: 173px; height: 173px" runat="server" ImageUrl="~/Images/loading_spinner.gif" width="173px" height="173px" BorderWidth="0px" AlternateText="Loading" />     
    </telerik:RadAjaxLoadingPanel> 

         </div>
              
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
                <script type="text/javascript">
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
                    function onRequestStart(sender, args) {
                        if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0
                                ) {
                            args.set_enableAjax(false);
                        }
                    }
                    function callBackFn(arg) {
                        if (arg == true) {

                        }
                    }
                    function MyValueChanging(sender, args) {
                        args.set_newValue(args.get_newValue().toUpperCase());
                    }
                    $('#ImageButton1').click(function () {
                        $('#cbxServiceGrpName').hide();
                        $(this).hide();
                    });
                </script>
            </telerik:RadCodeBlock>
     <telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadToolTip ID="RadToolTip2" runat="server" Width="200px" ShowEvent="OnClick"
          TargetControlID="HeaderLink" IsClientID="true" HideEvent="LeaveToolTip" Position="Center"
           Animation="Resize" ShowDelay="0" RelativeTo="Element" Text="Quarter End Account Alignment Save - save historical alignments before doing any realignments">
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
        <div id="selectedMenu" style="padding-left:20px"><b>Quarter End Account Alignment Save</b>
    <a id="HeaderLink" href="#" onclick="return false;">
       <img src="Images/help-icon16.png" />
    </a>
            <br /><i>Quarter End Account Alignment Save - Save the Account Alignments at Quarter End before Re-aligninment are done.</i>
        </div><br />

   
    <div>

         <table border="0">
             <tr>
                <td>Select Quarter </td>
                <td >
                     <telerik:RadComboBox ID="cbxQtr" runat="server" autopostback="true">
                         <Items>
                             <telerik:RadComboBoxItem runat="server" Text="QTR1" Value="1" /> 
                             <telerik:RadComboBoxItem runat="server" Text="QTR2" Value="2" /> 
                             <telerik:RadComboBoxItem runat="server" Text="QTR3" Value="3" /> 
                             <telerik:RadComboBoxItem runat="server" Text="QTR4" Value="4" /> 
                         </Items>
                     </telerik:RadComboBox> 
                </td>
               <td style="width: 550px;">                   
                   
                </td>
                </tr>
                <tr>
                <td>Select Year </td>
                <td >
                     <telerik:RadComboBox ID="cbxYear" runat="server" autopostback="true"></telerik:RadComboBox> 
                </td>
               <td style="width: 550px;">                   
                   
                </td>
                </tr>         
            
               <tr>
              <td>
                  <p></p>
                 <asp:Button ID="btnSubmit" runat="server" Text="Do Quarter Save" CssClass="btn btn-primary" OnClick="btnSubmit_Click"  />
                  
              </td>
              <td></td>
          </tr>

                 <tr>
              <td>
                
                 <asp:Button ID="btnCancel" runat="server"  causesvalidation="true" Text="Cancel" OnClick="btnCancel_Click"  CssClass ="btn btn-primary" Visible="false"/>
                  
              </td>
              <td>
                  <asp:Button ID="btnContinue" runat="server" Text="Remove Saved Records and Do Quarter Save" CssClass="btn btn-primary" OnClick="btnContinue_Click" Visible="false" />
              </td>
          </tr>
            
        </table>

       
      
    </div>
</asp:Content>

