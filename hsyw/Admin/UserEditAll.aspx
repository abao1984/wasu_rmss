<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserEditAll.aspx.cs" Inherits="Admin_UserEditAll" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="height: 100%; width: 100%;" cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 30px;" class="tdHead">
                &nbsp;&nbsp;
                <asp:LinkButton ID="BtnXGPwd" runat="server" OnClick="BtnXGPwd_Click">修改密码</asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="BtnXGQX" runat="server" OnClick="BtnXGQX_Click">用户权限</asp:LinkButton>&nbsp;&nbsp;
                <asp:LinkButton ID="BtnXGQY" runat="server" OnClick="BtnXGQY_Click">访问区域</asp:LinkButton>&nbsp;&nbsp;
                <asp:Button ID="BtnBack" runat="server" Text="返回" OnClick="BtnBack_Click" CssClass="btn_2k3" />
            </td>
        </tr>
        <tr>
            <td style="height: 100%;">
                <iframe id="MainFrame" runat="server" height="100%" width="100%"></iframe>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="OldBtnId" runat="server" Style="display: none;"></asp:TextBox>
    </form>
</body>
</html>
