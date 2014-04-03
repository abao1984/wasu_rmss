<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XuanZheDaYingZiDuan.aspx.cs"
    Inherits="Web_GZCL_GZBB_XuanZheDaYingZiDuan" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%;">
        <tr height="1px">
            <td class="tableHead">
                <asp:Button ID="BtnQR" runat="server" CssClass="btn_2k3"
                    Text="确定" onclick="BtnQR_Click" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:CheckBoxList ID="CheckBoxList1" runat="server"  Width="100%" RepeatColumns="4" RepeatDirection="Horizontal">
                    <asp:ListItem Text="投诉编号"  Value="GZBH" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="所属区域"  Value="KHQY"  Selected="True"></asp:ListItem>
                    <asp:ListItem Text="故障名称"  Value="GZMC"  Selected="True"></asp:ListItem>
                    <asp:ListItem Text="投诉时间"  Value="TSSJ"  Selected="True"></asp:ListItem>
                    <asp:ListItem Text="业务主体"  Value="YWZT"  Selected="True"></asp:ListItem>
                    <asp:ListItem Text="业务类型"  Value="YWLB"  Selected="True"></asp:ListItem>
                    <asp:ListItem Text="故障层次"  Value="GZCC"  Selected="True"></asp:ListItem>
                    <asp:ListItem Text="故障级别"  Value="GZDJ"  Selected="True"></asp:ListItem>
                    <asp:ListItem Text="创建人"  Value="GZCJRNAME" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="拥有人"  Value="YYR"  Selected="True"></asp:ListItem>
                    <asp:ListItem Text="故障时长(分)"  Value="GZSC"  Selected="True"></asp:ListItem>
                    <asp:ListItem Text="故障状态"  Value="GZZT"  Selected="True"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
