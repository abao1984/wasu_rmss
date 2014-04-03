<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuZhangBaoBiao.aspx.cs" Inherits="Web_GZCL_GZBB_GuZhangBaoBiao" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script src="../../../calendar.js" language="javascript" type="text/javascript"></script>

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%" border="1" height="100%" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td align="center" width="15%" class="tdBak">
                报表类型
            </td>
            <td width="18%">
                <asp:DropDownList ID="dropBBLX" runat="server" Width="100%" 
                    OnSelectedIndexChanged="dropBBLX_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Text="---请选择---"></asp:ListItem>
                    <asp:ListItem Text="今日统计"></asp:ListItem>
                    <asp:ListItem Text="处理方法报表"></asp:ListItem>
                    <asp:ListItem Text="故障类型报表"></asp:ListItem>
                    <asp:ListItem Text="历史数据统计"></asp:ListItem>
                    <asp:ListItem Text="历史记录查询"></asp:ListItem>
                    <asp:ListItem Text="客户类型报表"></asp:ListItem>
                    <asp:ListItem Text="故障数量统计"></asp:ListItem>
                    <asp:ListItem Text="行业分类报表"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td width="15%" class="tdBak" align="center">
                时间
            </td>
            <td width="15%">
                <asp:TextBox ID="TSSJ1" runat="server" onfocus="setDay(this);" BorderStyle="None"
                    Width="100%"></asp:TextBox>
            </td>
            <td width="15%" class="tdBak" align="center" runat="server" id="time1">
                至
            </td>
            <td width="15%" runat="server" id="time2">
                <asp:TextBox ID="TSSJ2" runat="server" onfocus="setDay(this);" BorderStyle="None"
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" class="tdBak">
                业务类别
            </td>
            <td>
                <asp:DropDownList ID="dropYWLB" runat="server" Width="100%" 
                    OnSelectedIndexChanged="dropYWLB_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td align="center" class="tdBak">
                故障专业
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="dropGZZY" runat="server" Width="100%" 
                            onselectedindexchanged="dropGZZY_SelectedIndexChanged"  AutoPostBack="True">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dropYWLB" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="center" class="tdBak">
                故障层次
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="dropGZCC" runat="server" Width="100%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dropGZZY" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="center" class="tdBak">
                故障类型
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="dropGZLX" runat="server" Width="100%" 
                            onselectedindexchanged="dropGZLX_SelectedIndexChanged"  AutoPostBack="True">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dropYWLB" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td align="center" class="tdBak">
                故障原因
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="dropGZYY" runat="server" Width="100%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dropGZLX" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td colspan="2" class="tdBak" align="right">
                <asp:Button ID="Button1" runat="server" Text="生成报表" CssClass="btn_2k3" OnClick="Button1_Click" />
                <%-- <asp:Button ID="Button2" runat="server" Text="导出报表" />--%>
            </td>
        </tr>
        <tr height="100%" >
            <td colspan="6" align="center" valign="top">
                <table  runat="server" id="tbbb" border="1" cellpadding="0" cellspacing="0" >
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
