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
        <div id="DealButtonDiv" runat="server">
            <asp:Button ID="DealButton" runat="server" Text="Deal!" Width="99px" OnClick="DealButton_Click" />
        </div>
        <div id="PlayerCard1Div" runat="server">
            <asp:ImageButton ID="PlayerCard1" runat="server" CommandName="1" Enabled="false" OnCommand="CardClick" />
        </div>
        <div id="PlayerCard2Div" runat="server">
            <asp:ImageButton ID="PlayerCard2" runat="server" OnCommand="CardClick" CommandName="2" Enabled="false" />
        </div>
        <div id="PlayerCard3Div" runat="server">
            <asp:ImageButton ID="PlayerCard3" runat="server" OnCommand="CardClick" CommandName="3" Enabled="false" />
        </div>
        <div id="PlayerCard4Div" runat="server">
            <asp:ImageButton ID="PlayerCard4" runat="server" OnCommand="CardClick" CommandName="4" Enabled="false" />
        </div>
        <div id="PlayerCard5Div" runat="server">
            <asp:ImageButton ID="PlayerCard5" runat="server" OnCommand="CardClick" CommandName="5" Enabled="false" />
        </div>
        <div id="PlayerCard6Div" runat="server">
            <asp:ImageButton ID="PlayerCard6" runat="server" OnCommand="CardClick" CommandName="6" Enabled="false" />
        </div>
        <div id="PlayerCard7Div" runat="server">
            <asp:ImageButton ID="PlayerCard7" runat="server" Enabled="false" />
        </div>
        <div id="PlayerCard8Div" runat="server">
            <asp:ImageButton ID="PlayerCard8" runat="server" Enabled="false" />
        </div>
        <div id="PlayerCard9Div" runat="server">
            <asp:ImageButton ID="PlayerCard9" runat="server" Enabled="false" />
        </div>
        <div id="PlayerCard10Div" runat="server">
            <asp:ImageButton ID="PlayerCard10" runat="server" Enabled="false" />
        </div>
        <div id="PlayerCard11Div" runat="server">
            <asp:ImageButton ID="PlayerCard11" runat="server" Enabled="false" />
        </div>
        <div id="PlayerCard12Div" runat="server">
            <asp:ImageButton ID="PlayerCard12" runat="server" Enabled="false" />
        </div>
        <div id="PlayerCard13Div" runat="server">
            <asp:ImageButton ID="PlayerCard13" runat="server" Enabled="false" />
        </div>
        <div id="CribDiv" runat="server">
            <asp:Image ID="CribCard1" runat="server" CssClass="CardBacks" />
            <asp:Image ID="CribCard2" runat="server" CssClass="CardBacks" />
            <asp:Image ID="CribCard3" runat="server" CssClass="CardBacks" />
            <asp:Image ID="CribCard4" runat="server" CssClass="CardBacks" />
        </div>

    </form>
</body>
</html>
