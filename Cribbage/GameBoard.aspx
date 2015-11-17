<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameBoard.aspx.cs" Inherits="Cribbage.GameBoard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/GameBoard.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="Cribbage_BoardDiv" runat="server">
            <asp:PlaceHolder ID="Cribbage_Board" runat="server"></asp:PlaceHolder>
        </div>
        <div id="PlayerCard1Div" runat="server">
            <asp:PlaceHolder ID="PlayerCard1" runat="server"></asp:PlaceHolder>
        </div>
        <div id="PlayerCard2Div" runat="server">
            <asp:PlaceHolder ID="PlayerCard2" runat="server"></asp:PlaceHolder>
        </div>
        <div id="PlayerCard3Div" runat="server">
            <asp:PlaceHolder ID="PlayerCard3" runat="server"></asp:PlaceHolder>
        </div>
        <div id="PlayerCard4Div" runat="server">
            <asp:PlaceHolder ID="PlayerCard4" runat="server"></asp:PlaceHolder>
        </div>
        <div id="PlayerCard5Div" runat="server">
            <asp:PlaceHolder ID="PlayerCard5" runat="server"></asp:PlaceHolder>
        </div>
        <div id="PlayerCard6Div" runat="server">
            <asp:PlaceHolder ID="PlayerCard6" runat="server"></asp:PlaceHolder>
        </div>


    </form>
</body>
</html>
