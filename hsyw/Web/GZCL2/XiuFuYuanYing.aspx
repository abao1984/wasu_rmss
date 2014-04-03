<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XiuFuYuanYing.aspx.cs" Inherits="Web_GZCL_XiuFuYuanYing" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                    RepeatColumns="1" RepeatDirection="Horizontal">
                    <ItemTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td  class="tdBak" align="center">
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("parent_name") %>'></asp:Label>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("parent_id") %>' Style="display: none"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DataList ID="DataList2" runat="server" Width="100%" 
                                        OnItemDataBound="DataList2_ItemDataBound" RepeatColumns="3" 
                                        RepeatDirection="Horizontal" CellPadding="4" ForeColor="#333333">
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <ItemStyle BackColor="#E3EAEB" />
                                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <ItemTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="center">
                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("codename") %>'></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="txtName" runat="server" Style="display: none"></asp:TextBox>
    </form>
</body>
</html>
