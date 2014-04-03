<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tets2.aspx.cs" Inherits="tets2" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 117px;
        }
        .style2
        {
            height: 56px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="导入IP地址" />
        <asp:Button ID="Button2" runat="server"  Text="导入Vlan" 
            onclick="Button2_Click" />
    </form>
</body>
</html>
