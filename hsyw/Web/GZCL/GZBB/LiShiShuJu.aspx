<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LiShiShuJu.aspx.cs" Inherits="Web_GZCL_GZBB_LiShiShuJu" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="../../../calendar.js" language="javascript" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="1" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td align="center" class="tdBak">
                业务主体
            </td>
            <td width="15%">
                <asp:DropDownList ID="DropDownList1" runat="server" Width="100%" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td width="15%" class="tdBak" align="center">
                时间
            </td>
            <td width="15%">
                <asp:TextBox ID="TSSJ1" runat="server" onfocus="setDay(this);" BorderStyle="None"
                    Width="100%"></asp:TextBox>
            </td>
            <td width="3%" class="tdBak" align="center" runat="server" id="time1">
                至
            </td>
            <td width="15%" runat="server" id="time2">
                <asp:TextBox ID="TSSJ2" runat="server" onfocus="setDay(this);" BorderStyle="None"
                    Width="100%"></asp:TextBox>
            </td>
            <td class="tdBak" align="right">
                <asp:Button ID="Button2" runat="server" Text="导出Excel" CssClass="btn_2k3" OnClick="Button2_Click" />
                <asp:Button ID="Button1" runat="server" Text="生成报表" CssClass="btn_2k3" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="1" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td colspan="7" valign="top">
                <table id="tbbb" style="width: 100%;" runat="server" border="1" cellpadding="0" cellspacing="0">
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="7" valign="top">
                <table style="width: 100%;" id="GzlxGrid" runat="server" border="1" cellpadding="0"
                    cellspacing="0">
                </table>
                <%--  <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" OnItemDataBound="DataList1_ItemDataBound"
                    BorderStyle="None" CellPadding="0" CellSpacing="0">
                    <ItemTemplate>
                        <table style="width: 100%; height:100%" cellpadding="0" cellspacing="0" border="1"  >
                            <tr class="GridViewHead">
                                <td align="center" width="80px"  valign="top">
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("GZLX") %>'></asp:Label>
                                </td>
                                <td width="20px" align="center"  valign="top">
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("num") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" width="100%" valign="top">
                                    <asp:DataList ID="DataList2" runat="server" Width="100%" CellPadding="0" CellSpacing="0"
                                        BorderStyle="None">
                                        <ItemTemplate>
                                            <table border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" width="80px">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("GZYY") %>'></asp:Label>
                                                    </td>
                                                    <td width="19px" align="center">
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("num") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>--%>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
