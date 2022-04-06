<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="PrepumaWebApp.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <fieldset>
           <legend>Search Criteria:</legend>
          <asp:Label ID="Label1" runat="server" Text="Select Searh">

          </asp:Label> 
            <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                <asp:ListItem>Client Name</asp:ListItem>
                <asp:ListItem>Contract name</asp:ListItem>
                <asp:ListItem>Contract Number</asp:ListItem>
                <asp:ListItem Selected="True">Select</asp:ListItem>
           </asp:DropDownList>

        </fieldset>
    </div>
    </form>
</body>
</html>
