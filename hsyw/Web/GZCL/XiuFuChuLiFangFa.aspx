<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XiuFuChuLiFangFa.aspx.cs" Inherits="Web_GZCL_XiuFuChuLiFangFa" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function onchange(names) {
            parent.WindowClose();
            parent.document.getElementById(document.getElementById('txtName').value).value = names;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="height: 100%">
        <tr style="height: 100%">
            <td valign="top">
                <asp:DataList ID="DataList1" runat="server" Width="100%" OnItemDataBound="DataList1_ItemDataBound"
                    RepeatColumns="5" CellPadding="4" ForeColor="#333333">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <ItemTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td  align="center">
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("codename") %>'></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <AlternatingItemStyle BackColor="White" />
                    <ItemStyle BackColor="#EFF3FB" />
                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                </asp:DataList>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="txtName" runat="server" style="display:none"></asp:TextBox>
    </form>
</body>
</html>
