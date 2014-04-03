<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YongYouRenXinXi.aspx.cs"
    Inherits="Web_GZCL_YongYouRenXinXi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
        border="1" bordercolor="#5b9ed1">
        <tr>
            <td colspan="4" class="tdHead">
                拥有人信息
            </td>
        </tr>
        <tr>
            <td style="width: 20%" class="tdBak" align="center">
                办公电话</td>
            <td style="width: 30%">
                <asp:Label ID="BGDH" runat="server" Width="100%" style="text-align:left"></asp:Label>
            </td>
            <td style="width: 20%" class="tdBak" align="center">
                传真号码
            </td>
            <td style="width: 30%">
                <asp:Label ID="CZHM" runat="server" Width="100%" style="text-align:left"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 20%" class="tdBak" align="center">
                手&nbsp; 机</td>
            <td style="width: 30%">
                <asp:Label ID="SJ" runat="server" Width="100%" style="text-align:left"></asp:Label>
            </td>
            <td style="width: 20%" class="tdBak" align="center">
                联&nbsp; 网</td>
            <td style="width: 30%">
                <asp:Label ID="LW" runat="server" Width="100%" style="text-align:left"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 20%" class="tdBak" align="center">
                小灵通</td>
            <td style="width: 30%">
                <asp:Label ID="XLT" runat="server" Width="100%" style="text-align:left"></asp:Label>
            </td>
            <td style="width: 20%" class="tdBak" align="center">
                家庭电话</td>
            <td style="width: 30%">
                <asp:Label ID="JTDH" runat="server" Width="100%" style="text-align:left"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 20%" class="tdBak" align="center">
                E-Mail</td>
            <td style="width: 30%">
                <asp:Label ID="EM" runat="server" Width="100%" style="text-align:left"></asp:Label>
            </td>
            <td style="width: 20%" class="tdBak" align="center">
                生&nbsp; 日</td>
            <td style="width: 30%">
                <asp:Label ID="SR" runat="server" Width="100%" style="text-align:left"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 20%" class="tdBak" align="center">
                邮政编码</td>
            <td style="width: 30%">
                <asp:Label ID="BGDH3" runat="server" Width="100%" style="text-align:left"></asp:Label>
            </td>
            <td style="width: 20%" class="tdBak" align="center">
                内&nbsp; 网</td>
            <td style="width: 30%">
                <asp:Label ID="NW" runat="server" Width="100%" style="text-align:left"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 20%" class="tdBak" align="center">
                家庭住址</td>
            <td style="width: 30%">
                <asp:Label ID="BGDH4" runat="server" Width="100%" style="text-align:left"></asp:Label>
            </td>
            <td style="width: 20%" class="tdBak" align="center">
                &nbsp;
            </td>
            <td style="width: 30%">
                &nbsp;</td>
        </tr>
    </table>
    </form>
</body>
</html>
