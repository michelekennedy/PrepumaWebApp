<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="PrepumaWebApp.DashBoard" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div style="width: 853px">
        <br />
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
        <telerik:RadToolTip ID="RadToolTip2" runat="server" Width="300px" ShowEvent="OnClick"
            TargetControlID="HeaderLink" IsClientID="true" HideEvent="LeaveToolTip" Position="Center"
            Animation="Resize" ShowDelay="0" RelativeTo="Element"
            Text="">
        </telerik:RadToolTip>
    </div>
    <div>
        <table>
            <tr>
                <td>Total Number Of Contracts </td>
                <td>:
                    <asp:Label ID="lblTotalContracts" runat="server" ForeColor="blue"></asp:Label>
                </td>
            </tr>
            <%--<tr>
                <td>Total Number Of Un Assigned Contracts </td>
                <td>:
                    <asp:Label ID="lblTotalUnassignedContracts" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>--%>
            <tr>
                <td>Total Number Of Accounts </td>
                <td>:
                    <asp:Label ID="lblTotalAccounts" runat="server" ForeColor="blue"></asp:Label>
                </td>
             </tr>
             <tr>
                <td>Total Number Of Accounts with No Sales Info </td>
                <td>:
                    <asp:Label ID="lblTotalUnassignedAccounts" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <%--<td>Total Number Of Un Assigned Contracts </td>--%>
                <td><a id="unAssignedAccLink" href="/UnassignedAccounts">Assign Sales Info to Accounts link</a></td>
                 <td></td>
            </tr>
        </table>

    </div>
    <br />
    <br />

    <div class="demo-container">
        <div class="left">
            <telerik:RadHtmlChart runat="server" ID="PieChart1" Width="500" Height="300" Transitions="true" Skin="Silk"  OnClientSeriesHovered="OnSeriesOvered">

                <ChartTitle Text="Sales Professional By District">
                    <Appearance Align="Center" BackgroundColor="Transparent" Position="Bottom">
                        <TextStyle Color="Violet" Bold="true"  />
                    </Appearance>
                </ChartTitle>

                <PlotArea>

                    <Series>

                        <telerik:PieSeries DataFieldY="salesRepsCount" NameField="district" ExplodeField="IsExploded">

                            <LabelsAppearance DataFormatString="{0}%">
                            </LabelsAppearance>

                            <TooltipsAppearance Color="White" DataFormatString="{0}%"></TooltipsAppearance>

                        </telerik:PieSeries>

                    </Series>

                </PlotArea>

            </telerik:RadHtmlChart>
        </div>
        <div class="right">
            <telerik:RadHtmlChart runat="server" ID="ColumnChart" Width="530" Height="300" Transitions="true" Skin="Silk">
                <PlotArea>
                    <Series>
                        <telerik:ColumnSeries Name="UnAssigned" Stacked="false" Gap="1.0" Spacing="0.4" DataFieldY="unAssignedCount">
                            <LabelsAppearance DataFormatString="{0} " Position="OutsideEnd" ></LabelsAppearance>
                            <TooltipsAppearance DataFormatString="{0}" Color="White"></TooltipsAppearance>

                        </telerik:ColumnSeries>
                        <%--<telerik:ColumnSeries Name="Contracts" Stacked="false" Gap="1.5" Spacing="0.1" DataFieldY="contractCount">
                            <LabelsAppearance DataFormatString="{0} " Position="OutsideEnd" ></LabelsAppearance>
                            <TooltipsAppearance DataFormatString="{0}" Color="White"></TooltipsAppearance>

                        </telerik:ColumnSeries>
                        <telerik:ColumnSeries Name="Accounts" DataFieldY="accountCount">
                            <LabelsAppearance DataFormatString="{0} " Position="OutsideEnd" ></LabelsAppearance>
                            <TooltipsAppearance DataFormatString="{0} " Color="White"></TooltipsAppearance>

                        </telerik:ColumnSeries>--%>
                    </Series>
                    <XAxis AxisCrossingValue="0" DataLabelsField="district" Color="Blue" MajorTickType="Outside" MinorTickType="Outside"
                        Reversed="false">

                        <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Skip="0" Step="1" Color="Black" >
                            <TextStyle  FontSize="9"/>
                        </LabelsAppearance>
                        <TitleAppearance Position="Center" RotationAngle="0" Text="Districts">
                        </TitleAppearance>
                    </XAxis>
                    <YAxis AxisCrossingValue="0" Color="Blue" MajorTickSize="1" MajorTickType="Outside"
                        MinorTickType="None" Reversed="false">
                        <LabelsAppearance DataFormatString="{0}" RotationAngle="0" Skip="0" Step="1" Color="Black" >
                        </LabelsAppearance>
                        <TitleAppearance Position="Center" RotationAngle="0" Text="No Of Unassigned Accounts">
                        </TitleAppearance>
                    </YAxis>

                </PlotArea>
                <ChartTitle Text="Unassigned Accounts (No SRID assigned)">
                    <Appearance Align="Center" BackgroundColor="Transparent" Position="Bottom">
                        <TextStyle Color="Violet" Bold="true"  />
                    </Appearance>
                </ChartTitle>
            </telerik:RadHtmlChart>
        </div>
    </div>
    <style>
        div.left {
            float: left;
            padding-left:initial;
            height: 400px;
            width: 500px;
        }

        div.right {
            float: right;
            padding-right:initial;
            height: 400px;
            width: 500px;
        }

        .container {
            height: 500px;
            width: 700px;
        }
    </style>
    <script type="text/javascript">
        function OnSeriesOvered(sender, args)
        {
            ////Toggle the explode state of the clicked item
            //args.get_dataItem().IsExploded = !args.get_dataItem().IsExploded;         
            //sender.set_transitions(false);
            ////Repaint the chart
            //sender.repaint();
        }
    </script>
</asp:Content>
