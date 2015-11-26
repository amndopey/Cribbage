<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Winner.aspx.cs" Inherits="Cribbage.Winner" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/Winner.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="Title" runat="server">
        <h1>
            <asp:Label ID="WinnerLabel" runat="server" Text="Winner Whoever Wins!"></asp:Label>
        </h1>    
    </div>
    </form>
</body>
</html>
