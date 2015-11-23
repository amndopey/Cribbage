<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameBoard.aspx.cs" Inherits="Cribbage.GameBoard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/GameBoard.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="display:none;" id="ReloadDiv" runat="server">
            <asp:Button ID="ReloadButton" runat="server" OnClick="ReloadButton_Click" ClientIDMode="Static" />
<%--            <asp:Button ID="ComputerLastCardButton" runat="server" OnClick="ComputerLastCardButton_Click" ClientIDMode="Static" />--%>
            <asp:Button ID="FinalCountButton" runat="server" OnClick="FinalCountButton_Click" ClientIDMode="Static" />

        </div>
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
        <div id="CounterDiv" runat="server" visible="false">
            Counter:
            <br />
            <h1 id="CounterHeader" style="font-size:4em;">
                <asp:Label ID="CounterLabel" runat="server" Text="0"></asp:Label>
            </h1>  
        </div>
        <div id="CribGoDiv" runat="server" visible="false">
            <h1 id="CribGoHeader" runat="server" style="font-size:2em;">Go</h1>
        </div>
<%--        <div id="LastCardDiv" runat="server" visible ="false">
            <asp:Button ID="LastCardButton" runat="server" Text="Go" Width="99px" OnClick="LastCardButton_Click" />
        </div>--%>
        <div id="ScoreboardDiv" runat="server">
            <asp:ListBox ID="Scoreboard" runat="server" Height="134px" Width="232px"></asp:ListBox>
        </div>

    </form>

    <script type='text/javascript'>
        function ComputerTurn(id) {
            var refreshFunction = document.getElementById('ReloadButton').click();
            clearInterval(myVar)
            //refreshFunction();
            return false;
        }

        //function ComputerLastCard(id) {
        //    var refreshFunction = document.getElementById('ComputerLastCardButton').click();
        //    clearInterval(myVar)
        //    //refreshFunction();
        //    return false;
        //}

        function FinalCount(id) {
            var refreshFunction = document.getElementById('FinalCountButton').click();
            clearInterval(myVar)
            //refreshFunction();
            return false;
        }
        //var intervalId = setInterval('RefreshResults()', 5000);
    </script>

</body>
</html>
