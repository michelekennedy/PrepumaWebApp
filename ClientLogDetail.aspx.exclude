<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientLogDetail.aspx.cs" Inherits="PrepumaWebApp.ClientLogDetail" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
              
              <telerik:AjaxSetting AjaxControlID="RadDropDownType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadDropDownType" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="dpInvoiceDate1" />
                    <telerik:AjaxUpdatedControl ControlID="dpInvoiceDate2" />                   
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />
                </UpdatedControls>
            </telerik:AjaxSetting>
           <telerik:AjaxSetting AjaxControlID="dpInvoiceDate1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dpInvoiceDate1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />
                </UpdatedControls>
            </telerik:AjaxSetting>
         <telerik:AjaxSetting AjaxControlID="dpInvoiceDate2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dpInvoiceDate2" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSubmit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnSubmit" LoadingPanelID="RadAjaxLoadingPanel1" />                   
                    
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
                   
                  
                </script>

     </telerik:RadCodeBlock>
     <telerik:RadToolTip ID="RadToolTip2" runat="server" Width="300px" ShowEvent="OnClick"
                        TargetControlID="HeaderLink" IsClientID="true" HideEvent="LeaveToolTip" Position="Center"
                         Animation="Resize" ShowDelay="0" RelativeTo="Element"
                 Text="View Pricing Details of Client Logs">
     </telerik:RadToolTip>
    <div style="width: 1100px">

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

    <div id="selectedMenu" style="padding-left:20px"><b>Client Log Detail</b>
     <a id="HeaderLink" href="#" onclick="return false;">
       <img src="Images/help-icon16.png" />
     </a>
          
     <br /> 
    </div><br />

        <table border="0">

         
            <tr>
                <td>
                     <asp:Label ID="lblDateField" CssClass="alert-link" runat="server" Text="Date Field" Visible="true"></asp:Label>
                </td>
                <td>
                    
                <telerik:RadComboBox RenderMode="Lightweight" ID="RadDropDownType" runat="server"  DropDownHeight="50px" Width="250px" Skin="Silk" 
                DefaultMessage="Select a date field" DropDownWidth="250px" AutoPostBack="true" >
                </telerik:RadComboBox>
             
                </td>
                <td></td>
                 <td><asp:Label ID="lblDate1" CssClass="alert-link" runat="server" Text="*Date From" Visible="true"></asp:Label></td>
                <td >
                     <telerik:RadDatePicker ID="dpInvoiceDate1"  runat="server" autopostback="true"></telerik:RadDatePicker> 
                     
                </td>
             
                <td><asp:Label ID="lblDate2" CssClass="alert-link" runat="server" Text="*Date To" Visible="true"></asp:Label></td>
                <td >
                     <telerik:RadDatePicker ID="dpInvoiceDate2"  runat="server" autopostback="true"></telerik:RadDatePicker> 
                     

                </td>

            </tr>
            <tr>
                 <td></td>
                 <td></td>
                 <td></td>
                 <td></td>
                 <td>                  
                          <asp:RequiredFieldValidator id= "date1Validator" runat=server 
                          ControlToValidate=dpInvoiceDate1
                          validationgroup="submit"
                          ErrorMessage=" *From Date is required." style="color: red">
                          </asp:RequiredFieldValidator>

                         
                  </td>
                  <td></td>
                  <td>
                  
                          <asp:RequiredFieldValidator id= "date2Validator" runat=server 
                          ControlToValidate=dpInvoiceDate2
                          validationgroup="submit"
                          ErrorMessage=" *To Date is required." style="color: red">
                          </asp:RequiredFieldValidator>
                       
                   </td>
                   
            </tr>
           
          
          <tr>
                      </table>
        <table>
            <tr>
              <td>
                  <asp:Button ID="btnSubmit" runat="server" validationgroup="submit" Text="Run Report to Screen" CssClass="btn btn-primary" OnClick="btnSubmit_Click"  />
                  </td>
                <td>
                  
                   <asp:Button ID="btnExport" runat="server" validationgroup="submit" Text="Export Report to Excel" CssClass="btn btn-primary" OnClick="btnExport_Click"  />
                 
              </td>


          </tr>
        </table>
    
         <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="550" Width="100%" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Visible="true">
            <LocalReport ReportPath="ClientLogDetail.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
                    
                  
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>


         <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName=""></asp:ObjectDataSource>
        
       
    </div>
</asp:Content>