<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FanDanChuLi.aspx.cs" Inherits="Web_GZCL_FDZ_FanDanChuLi" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="expires" content="-1" />
    <base target="_self" />

    <script>
        //        function onchang() {
        //            parent.WindowClose();
        //            parent.document.getElementById("xggzzt").value=
        //        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td align="center">
                <asp:DropDownList ID="DropDownList1" runat="server" Width="140px">
                    <asp:ListItem>电话处理</asp:ListItem>
                    <asp:ListItem>调度发单</asp:ListItem>
                    <asp:ListItem>维修返单</asp:ListItem>
                    <asp:ListItem>遗单</asp:ListItem>
                    <asp:ListItem>留单</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="Button1" runat="server" Text="确认" CssClass="btn_2k3" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
