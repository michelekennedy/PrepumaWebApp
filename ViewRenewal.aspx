<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewRenewal.aspx.cs" Inherits="PrepumaWebApp.ViewRenewal" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Client Log</title>
    <style>
        body {
            background-color: 	#ffffff;
        }
       
        table.clientInfo {
            border-collapse: collapse;
            width:inherit;
        }

        th{
            color:red;
        }
         td {
            /*padding: 0.25rem;*/
          
        }
         div.right {
            float:right;
            height: 90px;
            width: 240px;
            font-style:italic;
            font-size:small;
            color:blue;
        }
    
    </style>
</head>
<body style="font-family:Calibri">
        <form id="form1" runat="server">
   <%-- <div class="right" >
        <p></p>
             <table>
                  <tr>
                     <td><b>Submitted By: </b></td>
                     <td>
                         <asp:Label ID="lblSubmittedBy" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td><b>Last Updated By: </b></td>
                     <td>
                         <asp:Label ID="lblUpdatedBy" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td><b>Last Updated On: </b></td>
                     <td><asp:Label ID="lblUpdatedOn" runat="server"></asp:Label></td>
                 </tr>
             </table>
         </div>--%>

    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
      <%--  <div style="width: 453px">
         <asp:Panel ID="pnlDanger" runat="server" Visible="false">
            <div class="alert alert-danger" role="alert">
                <asp:Label ID="lblDanger" runat="server" CssClass="alert-link"></asp:Label>
            </div>
        </asp:Panel>
        </div>--%>
     <div style="margin: 20px 20px 20px 10px;  padding-left:20px" >

        
        
        
       

       <img src="../Images/divrb2.png" />
       <asp:Panel runat="server" ID="pnlclientInfo" BorderColor="DarkBlue" style="padding-left:5px">
       
           
          <asp:Panel runat="server" style="background-color:white !important; -webkit-print-color-adjust: exact;padding-left:5px">
            <table class="clientInfo" style="width:765px" border="0">
               <tr>
                   <td style="width:110px"></td>
                   <td style="width:400px"></td>
                   <td style="width:130px"></td>
                   <td style="width:120px"></td>
               </tr>
               <tr>                   
                   <td colspan="2" style="color:blue; text-align:left"; ><p></p><b><asp:Label ID="lblCustomerName" runat="server" ></asp:Label></b></td>
                   <td><b>Effective Date: </b></td>
                   <td><b>Expiry Date: </b></td>
               </tr>
                  <tr>                  
                    <td colspan="2" style="color:blue;""><asp:Label ID="lblReason" runat="server"></asp:Label></td>
                    <td><asp:Label ID="lblEffectiveDate" runat="server"></asp:Label></td>
                    <td><asp:Label ID="lblExpiryDate" runat="server"></asp:Label></td>
                </tr>   
                 <tr>
                    <td ><b><asp:Label ID="lblFSDl" runat="server" Text="First Ship Date:"></asp:Label></b></td>
                    <td>
                        <asp:Label ID="lblFSD" runat="server"></asp:Label>
                    </td>
                    <td></td>    
                    <td></td>
                </tr>
                <tr>
                    <td><b>Contract #: </b></td>
                    <td colspan="3">
                        <asp:Label ID="lblContractNumbers" runat="server"></asp:Label>
                    </td>    
                </tr>
                <tr>           
                   
                 </tr>   
                 <tr>
                    <td>Sales Territory:</td>
                    <td><asp:Label ID="lblSalesInfo" runat="server"></asp:Label></td>            
                    <td></td>
                    <td></td>                    
                  </tr>              

                  <tr>
                      <td></td>
                       <td><asp:Label ID="lblslsName" runat="server"></asp:Label></td>
                       <td></td>
                       <td></td>
                   </tr>
                               
               
                 <tr>
                         <td colspan="2" style="text-align:left"><asp:Label ID="lblVDA" runat="server" Text="<p>Yearly Revenue Commitment Per VDA" Visible="false"></asp:Label>&nbsp;&nbsp;
                             <asp:Label ID="lblVDARevenue" runat="server" Visible="false" Text=""></asp:Label>
                         </td>
                         <td style="text-align:left"></td>
                 </tr>

                    
            </table>          
               
              </asp:Panel>
            <div style="text-align:left;width:765px">
             <img src="../Images/divrb.png" /> 
        </div>
            
            <asp:Panel runat="server" >
                <table class="clientInfo" border="0">                   
                    <tr><td colspan="11"></td></tr>
                     <tr style="background-color:gray">
                        <td  style="width:160px;"><b>Annualized:</b></td>

                        <td><asp:Label ID="lblpctIncreases" runat="server" Text="% Increases"></asp:Label></td>
                        <td style="width:20px"></td>

                        <td  style="width:<%=tdcurwidth%>; text-align:right;"><asp:Label ID="lblCurrent" runat="server" Text="  Current  " Visible="true"></asp:Label></td>
                        <td style="width:10px"></td>
                        <td  style="width:<%=tdsowwidth%>; text-align:right;"><asp:Label ID="lblSOW" runat="server" Text=" Share of Wallet " Visible="true"></asp:Label></td>
                        <td style="width:10px"></td>
                        <td  style="width:<%=tdrocwidth%>; text-align:right;"><asp:Label ID="lblROC" runat="server" Text=" Rate of Change " Visible="true"></asp:Label></td>
                        <td style="width:20px"></td>
                        <td  style="width:<%=tdexpwidth%>;text-align:right;"><asp:Label ID="lblExpected" runat="server" Text="  Increase  " Visible="true"></asp:Label></td>
                       <td style="width:20px"></td>                     
                        <td  style="width:<%=tdnewwidth%>; text-align:right;"><asp:Label ID="lblNewProfile" runat="server" Text=" New Profile " Visible="true"></asp:Label></td>
                       <%-- <td  style="width:100px; text-align:right;"><asp:Label ID="lblCurrent" runat="server" Text="  Current  " Visible="true"></asp:Label></td>
                        <td style="width:10px"></td>
                        <td  style="width:100px; text-align:right;"><asp:Label ID="lblSOW" runat="server" Text=" Share of Wallet " Visible="true"></asp:Label></td>
                        <td style="width:10px"></td>
                        <td  style="width:100px; text-align:right;"><asp:Label ID="lblROC" runat="server" Text=" Rate of Change " Visible="true"></asp:Label></td>
                        <td style="width:20px"></td>
                        <td  style="width:100px;text-align:right;"><asp:Label ID="lblExpected" runat="server" Text="  Increase  " Visible="true"></asp:Label></td>
                        <td style="width:20px"></td>                     
                        <td  style="width:100px; text-align:right;"><asp:Label ID="lblNewProfile" runat="server" Text=" New Profile " Visible="true"></asp:Label></td>--%>
                    </tr>
                    <tr>                        
                        <td>Margin</td>
                        <td></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblcurMargin" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblsowMargin" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblrocMargin" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpMargin" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblnewMargin" runat="server"></asp:Label></td>
                    </tr>
                      <tr style="background-color:lightgray">
                        <td>Revenue</td>
                        <td></td>
                        <td></td>
                        <td style="text-align:right;color:green;font-weight:700;"><asp:Label ID="lblcurRev" runat="server"></asp:Label></td>
                        <td></td>
                          <td style="text-align:right;color:green;font-weight:700;"><asp:Label ID="lblsowRev" runat="server"></asp:Label></td>
                        <td></td>
                          <td style="text-align:right;color:green;font-weight:700;"><asp:Label ID="lblrocRev" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right;color:green;font-weight:700;"><asp:Label ID="lblexpRev" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right;color:green;font-weight:700;"><asp:Label ID="lblnewRev" runat="server"></asp:Label></td>
                    </tr>
                      <tr>
                        <td>Courier</td>
                        <td style="text-align:right"><asp:Label ID="lblpctCourier" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblcurCourier" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblsowCourier" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblrocCourier" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpCourier" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblnewCourier" runat="server"></asp:Label></td>
                    </tr>
                       <tr style="background-color:lightgray">
                        <td>Freight Fwd</td>
                        <td style="text-align:right"><asp:Label ID="lblpctFreight" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblcurFF" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblsowFF" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblrocFF" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpFF" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblnewFF" runat="server"></asp:Label></td>
                    </tr>
                      <tr>
                        <td>LTL</td>
                        <td style="text-align:right"><asp:Label ID="lblpctLTL" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblcurLTL" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblsowLTL" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblrocLTL" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpLTL" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblnewLTL" runat="server"></asp:Label></td>
                    </tr>
                     <tr style="background-color:lightgray">
                        <td>PPST</td>
                         <td style="text-align:right"><asp:Label ID="lblpctPPST" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblcurPPST" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblsowPPST" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblrocPPST" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpPPST" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblnewPPST" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>CPC</td>
                        <td style="text-align:right"><asp:Label ID="lblpctCPC" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblcurCPC" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblsowCPC" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblrocCPC" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpCPC" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblnewCPC" runat="server"></asp:Label></td>
                    </tr>
                     <tr style="background-color:lightgray">
                        <td>Other</td>
                        <td style="text-align:right"><asp:Label ID="lblpctOther" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblcurOther" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblsowOther" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblrocOther" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpOther" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblnewOther" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                                 
                                   <td colspan="11" style="text-align:left">  
                                       <asp:Label ID="lblOtherDesc" runat="server"></asp:Label>
                                  </td>
                                 
                     <td></td>
                 </tr>
                     <tr>
                        <td colspan="2"><b>Target Gross Margin%</b></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblcurGrossMargin" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblsowGrossMargin" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblrocGrossMargin" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpGrossMargin" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblnewGrossMargin" runat="server"></asp:Label></td>
                    </tr>
                    
            </table>           
            <div style="text-align:left;width:765px">
             <img src="../Images/divrb2.png" /> 
            </div>

             <!-- Accessorials -->   

                <table id="accessorials" class="clientInfo" border="0">
                    <tr style="background-color:gray">
                        <td style="width:200px;"><b><asp:Label ID="lblaAccessorials" runat="server" Text="Accessorials:"></asp:Label></b></td>
                        <td style="width:150px;text-align:right"><asp:Label ID="lblaCurrent1" runat="server" Text="  Current"></asp:Label></td>
                        <td style="width:10px;"></td>
                        <td style="width:150px;text-align:right"><asp:Label ID="lblaExpected1" runat="server" Text="Expected"></asp:Label></td>
                        <td style="width:30px;"></td>
                        <td style="width:200px;"></td>
                        <td style="width:60px;"><asp:Label ID="lblaCurrent2" runat="server" Text="  Current"></asp:Label></td>
                        <td style="width:10px"></td>
                        <td style="width:60px;"><asp:Label ID="lblaExpected2" runat="server" Text="Expected"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblResiFees" runat="server" Text="ResiFees"></asp:Label></td>
                        <td style="text-align:right"><asp:Label ID="lblcurResiFees" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpResiFees" runat="server"></asp:Label></td>
                        <td></td>
                        <td><asp:Label ID="lblDimsGround" runat="server" Text="Dims Ground"></asp:Label></td>
                        <td style="text-align:right"><asp:Label ID="lblcurDimsGround" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpDimsGround" runat="server"></asp:Label></td>
                    </tr>
                    <tr style="background-color:lightgray">
                        <td><asp:Label ID="lblshFP" runat="server" Text="Sp Hndlg Flat Pkg"></asp:Label></td>
                        <td style="text-align:right"><asp:Label ID="lblcurshFP" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpshFP" runat="server"></asp:Label></td>
                        <td></td>
                        <td><asp:Label ID="lblDimsAir" runat="server" Text="Dims Air"></asp:Label></td>
                        <td style="text-align:right"><asp:Label ID="lblcurDimsAir" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpDimsAir" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblshAH" runat="server" Text="Sp Hndlg Addl Handling"></asp:Label></td>
                        <td style="text-align:right"><asp:Label ID="lblcurshAH" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpshAH" runat="server"></asp:Label></td>
                        <td></td>
                        <td><asp:Label ID="lbldgFR" runat="server" Text="Dng Gds Fully Regulated"></asp:Label></td>
                        <td style="text-align:right"><asp:Label ID="lblcurdgFR" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpdgFR" runat="server"></asp:Label></td>
                    </tr>
                    <tr style="background-color:lightgray">
                        <td><asp:Label ID="lblshLP" runat="server" Text="Sp Hndlg Large Package"></asp:Label></td>
                        <td style="text-align:right"><asp:Label ID="lblcurshLP" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpshLP" runat="server"></asp:Label></td>
                        <td></td>
                        <td><asp:Label ID="lbldgUN3373" runat="server" Text="Dng Gds - UN3373"></asp:Label></td>
                        <td style="text-align:right"><asp:Label ID="lblcurdgUN3373" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpdgUN3373" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblshOML" runat="server" Text="Sp Hndlg Over Max Limit"></asp:Label></td>
                        <td style="text-align:right"><asp:Label ID="lblcurshOML" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpshOML" runat="server"></asp:Label></td>
                        <td></td>
                        <td><asp:Label ID="lbldgUN1845" runat="server" Text="Dng Gds - UN1845"></asp:Label></td>
                        <td style="text-align:right"><asp:Label ID="lblcurdgUN1845" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpdgUN1845" runat="server"></asp:Label></td>
                    </tr>
                    <tr style="background-color:lightgray">
                        <td><asp:Label ID="lblshO" runat="server" Text="Sp Hndlg Oversized"></asp:Label></td>
                        <td style="text-align:right"><asp:Label ID="lblcurshO" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpshO" runat="server"></asp:Label></td>
                        <td></td>
                        <td><asp:Label ID="lbldgLT500kg" runat="server" Text="Dng Gds - < 500kg"></asp:Label></td>
                        <td style="text-align:right"><asp:Label ID="lblcurdgLT500kg" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpdgLT500kg" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                        <td><asp:Label ID="lblshRAH" runat="server" Text="Sp Hndlg Res Area HvyWt"></asp:Label></td>
                        <td style="text-align:right"><asp:Label ID="lblcurshRAH" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpshRAH" runat="server"></asp:Label></td>
                        <td></td>
                        <td><asp:Label ID="lbldgLQ" runat="server" Text="Dng Gds Limited qty"></asp:Label></td>
                        <td style="text-align:right"><asp:Label ID="lblcurdgLQ" runat="server"></asp:Label></td>
                        <td></td>
                        <td style="text-align:right"><asp:Label ID="lblexpdgLQ" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="9" style="color:blue;">
                            <asp:Label ID="lblAccessorialComments" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>

                <!-- FUEL -->
                <p></p>
                <table>
                     <tr style="background-color:gray">
                        <%--<td></td>--%>
                        <td><asp:Label ID="lblCFuelAlign" runat="server" Text="  Current Fuel Alignment"></asp:Label></td>
                        <td><asp:Label ID="lblCFuelDisc" runat="server" Text="  Current Fuel Discount"></asp:Label></td>
                        <%--<td style="width:10px;"></td>--%>
                        <td><asp:Label ID="lblEFuelAlign" runat="server" Text="  New Fuel Alignment"></asp:Label></td>
                        <td><asp:Label ID="lblEFuelDisc" runat="server" Text="  New Fuel Discuont"></asp:Label></td>
                        <%--<td style="width:10px;"></td>--%>
                        <td><asp:Label ID="lblBeyondOrigin" runat="server" Text="Origin Beyond Discount?"></asp:Label></td>
                        <td><asp:Label ID="lblBeyondDest" runat="server" Text="Destination Beyond Discount?"></asp:Label></td>
                    </tr>
                     <tr>
                        <%--<td></td>--%>
                        <td><asp:Label ID="lblcurFuelAlignment" runat="server" Text=""></asp:Label></td>
                        <td><asp:Label ID="lblcurFuelDiscount" runat="server" Text=""></asp:Label></td>
                        <%--<td></td>--%>
                        <td><asp:Label ID="lblexpFuelAlignment" runat="server" Text=""></asp:Label></td>
                        <td><asp:Label ID="lblexpFuelDiscount" runat="server" Text=""></asp:Label></td>
                        <%--<td></td>--%>
                        <td><asp:Label ID="lblBeyondOriginDiscount" runat="server" Text=""></asp:Label></td>
                        <td><asp:Label ID="lblBeyondDestDiscount" runat="server" Text=""></asp:Label></td>
                    </tr>

                </table>
                       

            
               <!-- Notes -->
                <table>
                       <tr>
                         <td style="color:blue; text-align:left"><p></p><b><asp:Label ID="lblNotesl" runat="server" Text="Notes"></asp:Label></b></td>
                         <td ></td>
                     </tr>   
                     <tr>                        
                        <td colspan="2" style="font-size:small;">                             
                            <asp:Label ID="lblNotes" runat="server"></asp:Label><p></p>                            
                        </td>                            
                    </tr>
                 </table>   
                
                <!-- Print Button -->
                 <div style="margin: 20px 20px 20px 10px;">
            <table>
                 <tr>
                     <td>
                         <telerik:RadButton ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" Skin="Web20"  ></telerik:RadButton>
                     </td>
                     <td>
                         <telerik:RadButton ID="btnCancel" runat="server" Text="Cancel" Skin="Web20" OnClick="btnCancel_Click"  ></telerik:RadButton>
                     </td>
                      <td>
                         <telerik:RadButton ID="btnHide" runat="server" Text="Hide Accessorials" Skin="Web20" OnClick="btnHide_Click"  ></telerik:RadButton>
                     </td>
                       <td>
                         <telerik:RadButton ID="btnShow" runat="server" Text="Show Accessorials" Skin="Web20" OnClick="btnShow_Click" Visible="false" ></telerik:RadButton>
                     </td>
                 </tr>
             </table>
            <br />
        </div>
                
                  
                        </asp:Panel>
        </asp:Panel>
         <br />
       
         
        </div>
    </div>
    </form>
</body>
</html>
