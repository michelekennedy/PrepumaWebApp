<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="PrepumaWebApp.View" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>view Info</title>
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
    <div class="right" >
             <table>
                 <tr>
                     <td><b>Last UpDated By: </b></td>
                     <td>
                         <asp:Label ID="lblUpdatedBy" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td><b>Last UpDated On: </b></td>
                     <td><asp:Label ID="lblUpdatedOn" runat="server"></asp:Label></td>
                 </tr>
             </table>
         </div>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="width: 453px">
         <asp:Panel ID="pnlDanger" runat="server" Visible="false">
            <div class="alert alert-danger" role="alert">
                <asp:Label ID="lblDanger" runat="server" CssClass="alert-link"></asp:Label>
            </div>
        </asp:Panel>
        </div>
     <div style="margin: 20px 30px 60px 10px;  padding-left:20px" >
         <h4 style="color:blue">Relationship Name:  <asp:Label ID="lblRelationshipName" runat="server"></asp:Label></h4>
       <asp:Panel runat="server" ID="pnlclientInfo" BorderColor="DarkBlue" style="padding-left:10px">
            <table class="clientInfo">
                <tr><td></td></tr>
               
                <tr>
                    <td><b>Contract Number </b></td>
                    <td>
                        : <asp:Label ID="lblContractNumber" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Contract Name </b></td>
                    <td>
                        : <asp:Label ID="lblContractName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Account Number </b></td>
                    <td>
                        : <asp:Label ID="lblAccountNumber" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td><b>Account Name </b></td>
                    <td>
                        : <asp:Label ID="lblClientName" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
           <p></p>
            <asp:Panel runat="server" >
                <table class="clientInfo">
                   <tr>
                        <th style="color:blue; text-align:left"><b>Sales Info :</b></th>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td><b>Control Branch</b></td>
                        <td>: <asp:Label ID="lblExpr1" runat="server"></asp:Label></td>
                        <td style="width:45px"></td>
                        <td><b>Lost Business </b></td>
                        <td>: <asp:Label ID="lblLostBiz" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Lead Sales Rep </b></td>
                        <td>: <asp:Label ID="lblTerritory" runat="server"></asp:Label></td>
                        <td></td>
                        <td><b>ISP paid </b></td>
                        <td>: <asp:Label ID="lblISPpaid" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Sales Type </b></td>
                        <td>: <asp:Label ID="lblSalesType" runat="server"></asp:Label></td>
                        <td></td>
                        <td><b>ISP Rep</b></td>
                        <td> : <asp:Label ID="lblISP" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                        <td><b>Estimated Annual Revenue </b></td>
                        <td>: <asp:Label ID="lblEstAnnualRev" runat="server"></asp:Label></td>
                        <td></td>
                        <td><b>Industry Code </b></td>
                        <td>: <asp:Label ID="lblSecondaryNAICS" runat="server"></asp:Label></td>
                    </tr>

                    
                    <tr>
                        <td><b>Estimated Margin Percentage </b></td>
                        <td>: <asp:Label ID="lblEstMarginPct" runat="server"></asp:Label></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>  
                        <tr><td colspan="5"></td></tr>
                    <tr>
                        <td><b>Business Type </b></td>
                        <td>: <asp:Label ID="lblBusinessType" runat="server"></asp:Label></td>
                        <td></td>
                        <td><b>Start Date </b></td>
                        <td>: <asp:Label ID="lblStartDate" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Field Rep </b></td>
                        <td>: <asp:Label ID="lblLocalSRID" runat="server"></asp:Label></td>
                        <td></td>
                        <td><b>End Date </b></td>
                        <td>: <asp:Label ID="lblEndDate" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Strategic Rep </b></td>
                        <td>: <asp:Label ID="lblStrategicSRID" runat="server"></asp:Label></td>
                        <td></td>
                        <td><b>Rolloff Date </b></td>
                        <td>: <asp:Label ID="lblRolloffQtr" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                        <td><b>Branch</b></td>
                        <td>: <asp:Label ID="lblRegion" runat="server"></asp:Label></td>
                        <td></td>
                        <td><b>Product Type </b></td>
                        <td>: <asp:Label ID="lblProductType" runat="server"></asp:Label></td>
                    </tr>                   
                    <tr>
                        <td><b>Shipping Location </b></td>
                        <td>: <asp:Label ID="lblShippingLocation" runat="server"></asp:Label></td>
                        <td></td>
                        <td><b>Material Handling  </b></td>
                        <td>: <asp:Label ID="lblMATHANCPPID" runat="server"></asp:Label></td>
                    </tr>      
                    <tr>
                        <td><b>GateWay </b></td>
                        <td>: <asp:Label ID="lblGatewayID" runat="server"></asp:Label></td>
                        <td></td>
                        <td><b>Returns  </b></td>
                        <td>: <asp:Label ID="lblRTNSID" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>Cross Docking Fees </b></td>
                        <td>: <asp:Label ID="lblCDCPLBID" runat="server"></asp:Label></td>
                        <td></td>
                        <td><b>Share Percent </b></td>
                        <td>: <asp:Label ID="lblSharePercent" runat="server"></asp:Label></td>
                    </tr>
                    
                </table>
            </asp:Panel>
        </asp:Panel>
         <br />
         <div style="margin: 20px 100px 60px 10px;">
            <table>
                 <tr>
                     <td>
                         <telerik:RadButton ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" Skin="Web20"  ></telerik:RadButton>
                     </td>
                     <td>
                         <telerik:RadButton ID="btnCancel" runat="server" Text="Cancel" Skin="Web20" OnClick="btnCancel_Click"  ></telerik:RadButton>
                     </td>
                 </tr>
             </table>
            <br />
        </div>
         
        </div>
    </div>
    </form>
</body>
</html>
