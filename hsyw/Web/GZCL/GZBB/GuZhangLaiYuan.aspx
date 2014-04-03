<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuZhangLaiYuan.aspx.cs" Inherits="Web_GZCL_GZBB_GuZhangLaiYuan" %>

<html>
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
         .th{
	    text-align: center;
	    width:80px;
	    line-height: 22px;
	    background-color:Blue;
	    color: #323234;
	    font-weight: normal;
	    font-size: 12px;
	    font-weight: bold;
	    font-family: "宋体";
        }
        .time
        {
            width:80px;	
        }
    </style>
    <script src="../../../calendar.js" language="javascript" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table border="1" height="100%" cellpadding="0" cellspacing="0" width="100%">
        <tr height="1px">
            <td colspan="2" class="tdBak">
                <table>
                    <tr>
                        <td width="40px" class="tdBak" align="center">
                            时间
                        </td>
                        <td width="100px">
                            <asp:TextBox ID="TSSJ1" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                Width="100%"></asp:TextBox>
                        </td>
                        <td width="40px" class="tdBak" align="center" runat="server" id="time1">
                            至
                        </td>
                        <td width="100px" runat="server" id="time2">
                            <asp:TextBox ID="TSSJ2" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                Width="100%"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="right" width="200px">
                            <asp:Button ID="Button1" runat="server" Text="生成报表" CssClass="btn_2k3" OnClick="Button1_Click" />
                            &nbsp;
                            <asp:Button ID="Button2" runat="server" Text="导出报表" CssClass="btn_2k3" OnClick="Button2_Click" />
                        </td>
                        <td class="tdBak">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdBak" style="border-right-width: 0px;" align="center">
                <asp:Label ID="Label1" runat="server" Text="业务主体" Width="100" BorderWidth="0"></asp:Label>
            </td>
            <td class="tdBak" style="width: 100%; border-left-width: 0px;">
                <asp:CheckBoxList ID="YWZT" runat="server" RepeatColumns="10" RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="tdBak" style="border-right-width: 0px;" align="center">
                <asp:Label ID="Label2" runat="server" Text="业务来源" Width="100" BorderWidth="0"></asp:Label>
            </td>
            <td class="tdBak" style="width: 100%; border-left-width: 0px;">
                <asp:CheckBoxList ID="YWLY" runat="server" RepeatColumns="10" RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr height="100%">
            <td colspan="2" valign="top">
                <table runat="server" style="width:100%" id="tbbb" border="1" cellpadding="0" cellspacing="0">
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
