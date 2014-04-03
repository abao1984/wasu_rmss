<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JinRiTongJi.aspx.cs" Inherits="Web_GZCL_GZBB_JinRiTongJi" %>

<html>
<head runat="server">
    <title>今日统计</title>

    <script src="../../../calendar.js" language="javascript" type="text/javascript"></script>

</head>

<body>
    <form id="form1" runat="server">
    <table width="100%" border="1" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td align="center" class="tdBak" style="width:15%;">
                业务主体
            </td>
            <td width="35%" class="tdBak">
                <asp:DropDownList ID="DropDownList1" runat="server" Width="100px" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td width="15%" class="tdBak" align="center">
                时间
            </td>
            <td width="15%" class="tdBak" style="border-right-width:0px;">
                <asp:TextBox ID="TSSJ1" runat="server" onfocus="setDay(this);" BorderStyle="None"
                    Width="100px"></asp:TextBox>
            </td>
            <td class="tdBak" align="right" style="border-left-width:0px;">
                <asp:Button ID="Button2" runat="server" Text="导出Excel" CssClass="btn_2k3" onclick="Button2_Click" 
                   />
                <asp:Button ID="Button1" runat="server" Text="生成报表" CssClass="btn_2k3" OnClick="Button1_Click" />
            </td>
        </tr>
        <tr>
         
            <td colspan="5" valign="top">
                <table style="width: 100%;" id="tbbb" runat="server" border="1" cellpadding="0" cellspacing="0">
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="5" valign="top">
                <table style="width: 100%;" id="GzlxGrid" runat="server" border="1" cellpadding="0" cellspacing="0">
                </table>
             </td>
        </tr>
    </table>
    </form>
</body>
</html>
