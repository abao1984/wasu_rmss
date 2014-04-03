<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserEdit.aspx.cs" Inherits="Admin_UserEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="height: 100%; width: 100%;" cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 30px;" class="tdHead">
                &nbsp;&nbsp;<asp:LinkButton ID="BtnXGZL" runat="server" onclick="BtnXGZL_Click">修改资料</asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="BtnXGPwd" runat="server" onclick="BtnXGPwd_Click">修改密码</asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="BtnXGQX" runat="server" onclick="BtnXGQX_Click">用户权限</asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="BtnXGQY" runat="server" onclick="BtnXGQY_Click">访问区域</asp:LinkButton>&nbsp;&nbsp;
                <asp:Button ID="BtnBack" runat="server" Text="返回" onclick="BtnBack_Click" CssClass="btn_2k3"/>
            </td>
        </tr>
        <tr>
            <td style="height: 100%;">
                <iframe id="MainFrame" runat="server" height="100%" width="100%"></iframe>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="OldBtnId" runat="server" style="display:none;"></asp:TextBox>
    </form>
</body>
</html>
