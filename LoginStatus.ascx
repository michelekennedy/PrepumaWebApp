<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginStatus.ascx.cs" Inherits="PrepumaWebApp.LoginStatus" %>

<asp:Panel ID="pnlLogin" runat="server" Visible="false">
                        PI Contracts - Welcome <span class="bold">
                             <asp:label ID="lblName" runat="server"></asp:label>
                        </span>! [
                         <asp:Label ID="lblRole" runat="server"></asp:Label>
                        ]
                    <span class="bold">
                        <img src="Images/logout-icon.png" height="22" width="22" /> <asp:LinkButton runat="server" ID="lnkLogout" Text="LogOut" OnClick="lnkLogout_Click" ></asp:LinkButton></span>
             </asp:Panel>       
