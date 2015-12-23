<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Color_Selection.aspx.cs" Inherits="Cribbage.Color_Selection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/Color_Selection.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="Title">
            Pick your color!
        </div>
        
        <div id="Blue">
            <a class="fill-div" href="GameBoard.aspx?color=Blue"></a>
        </div>
        <div id="Orange">
            <a class="fill-div" href="GameBoard.aspx?color=Orange"></a>
        </div>
        <div id="Red">
            <a class="fill-div" href="GameBoard.aspx?color=Red"></a>
        </div>
        <div id="White">
            <a class="fill-div" href="GameBoard.aspx?color=White"></a>
        </div>
        <div id="Purple">
            <a class="fill-div" href="GameBoard.aspx?color=Purple"></a>
        </div>
        <div id="Yellow">
            <a class="fill-div" href="GameBoard.aspx?color=Yellow"></a>
        </div>
        <div id="Pink">
            <a class="fill-div" href="GameBoard.aspx?color=HotPink"></a>
        </div>
    </form>
</body>
</html>
