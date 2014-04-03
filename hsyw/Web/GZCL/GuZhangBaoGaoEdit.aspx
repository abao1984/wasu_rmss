<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuZhangBaoGaoEdit.aspx.cs"
    Inherits="Web_GZCL_GuZhangBaoGaoEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" src="../../calendar.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="tableHead">
                &nbsp;
                <asp:Button ID="BtnSave" runat="server" Text="保存" CssClass="btn_2k3" OnClick="BtnSave_Click" />
            </td>
        </tr>
        <tr>
            <td height="1">
                <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td align="center" class="tdBak" style="width: 12%">
                            故障编号
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="GZBH" runat="server" Width="100%" BorderWidth="0" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            受理人
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="SLR" runat="server" Width="100%" BorderWidth="0" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            故障来源
                        </td>
                        <td style="width: 13%">
                            <asp:DropDownList ID="GZLY" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            业务主体
                        </td>
                        <td style="width: 13%">
                            <asp:DropDownList ID="GZZY" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" style="width: 12%">
                            故障评估
                        </td>
                        <td width="88%" colspan="7">
                            <asp:TextBox ID="GZPG" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" style="width: 12%">
                            处理人
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="CLR" runat="server" Width="100%" BorderWidth="0" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            部门
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="BM" runat="server" Width="100%" BorderWidth="0" Height="18px"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            开始时间
                        </td>
                        <td style="width: 13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="KSSJ" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" alt="" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            结束时间
                        </td>
                        <td style="width: 13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="JSSJ" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" alt="" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" style="width: 12%">
                            故障描述
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="GZMS" runat="server" Width="100%" BorderWidth="0" TextMode="MultiLine"
                                Rows="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" style="width: 12%">
                            过程描述
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="GCMS" runat="server" Width="100%" BorderWidth="0" TextMode="MultiLine"
                                Rows="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" style="width: 12%">
                            设备资源变更
                        </td>
                        <td colspan="7" style="border-collapse: collapse">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border-color: #5b9ed1;
                                border-collapse: collapse">
                                <tr>
                                    <td style="width: 10%; border: solid 1 #5b9ed1" align="center" class="tdBak">
                                        领用
                                    </td>
                                    <td style="width: 90%; border: solid 1 #5b9ed1">
                                        <asp:TextBox ID="LY" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 10%; border: solid 1 #5b9ed1" align="center" class="tdBak">
                                        返还
                                    </td>
                                    <td style="width: 90%; border: solid 1 #5b9ed1">
                                        <asp:TextBox ID="FH" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" style="width: 12%">
                            已采取的措施
                        </td>
                        <td colspan="7" style="border-collapse: collapse">
                            <asp:TextBox ID="YCQCS" runat="server" Width="100%" BorderWidth="0" TextMode="MultiLine"
                                Rows="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" style="width: 12%">
                            将进一步采取的措施
                        </td>
                        <td colspan="7" style="border-collapse: collapse">
                            <asp:TextBox ID="JYBCQCS" runat="server" Width="100%" BorderWidth="0" TextMode="MultiLine"
                                Rows="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" style="width: 12%">
                            主管签字
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="ZGQZ" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            日期时间
                        </td>
                        <td colspan="5" style="width: 63%">
                            <asp:TextBox ID="RQSJ" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="ZT" runat="server" style="display:none;"></asp:TextBox>
    <asp:TextBox ID="GZBGGUID" runat="server" style="display:none;"></asp:TextBox>
     <asp:TextBox ID="SLRBCODE" runat="server" style="display:none;"></asp:TextBox>
    </form>
</body>
</html>
