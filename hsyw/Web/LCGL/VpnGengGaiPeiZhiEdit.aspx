<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VpnGengGaiPeiZhiEdit.aspx.cs"
    Inherits="Web_LCGL_VpnGengGaiPeiZhiEdit" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" language="javascript">
        function sendPageShow(lcbm, lczt, qfbj) {
            var guid = document.getElementById("GUID").value;
            var url = "SendPage.aspx?LCQFBJ=" + qfbj + "&LCBM=" + lcbm + "&LC_GUID=" + guid + "&LCZT=" + lczt;
            var str = window.showModalDialog(url, '', 'dialogHeight:300px; dialogWidth: 500px;center: yes; help: no;resizable: no; status:no;scroll:no;');
            if (str == "OK") {
                window.close();
                parent.document.getElementById("MenuButton").click();
            }
        }

        function windowOpenRmssSelect() {
            var url = "../Resource/RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID";
            windowOpenPage(url, "选择客户资源", "BtnRmss");
            event.returnValue = false;
        }
        function windowOpenRmssTQ() {
            var url = "../Resource/RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID&SUBSCRIBER_CODE=" + document.getElementById("SUBSCRIBER_CODE").value;
            windowOpenPage(url, "选择客户资源", "BtnRmss");
            //event.returnValue = false;
        }
  
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;" border="0" cellpadding="1" cellspacing="0">
        <tr id="Tr_Button" runat="server" style="display: none;">
            <td align="center" class="tableHead">
                <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" OnClick="SaveButton_Click"
                    Text="保存" />
                <asp:Button ID="SendButton" runat="server" CssClass="btn_2k3" OnClick="SendButton_Click"
                    Text="签发" />
                <asp:Button ID="BackButton" runat="server" CssClass="btn_2k3" OnClick="BackButton_Click"
                    Text="驳回" />
            </td>
        </tr>
        <tr>
            <td align="center" class="tableTitle">
                <asp:Label ID="HeadTitle" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table bordercolor="#5b9ed1" cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                    width="100%">
                    <tr>
                        <td align="center" class="tdBak" width="25%">
                            申请单编号
                        </td>
                        <td>
                            <asp:TextBox ID="SQDBH" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="tableTitle1">
                用户基本信息
            </td>
        </tr>
        <tr>
            <td>
                <table bordercolor="#5b9ed1" cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                    width="100%">
                    <tr>
                        <td align="center" class="tdBak" width="25%">
                            业务编码
                        </td>
                        <td width="25%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="SUBSCRIBER_ID" runat="server" BorderWidth="0" Style="display: none"
                                            Width="100%"></asp:TextBox>
                                        <asp:TextBox ID="SUBSCRIBER_CODE" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="TQ" ImageUrl="../Images/Small/gif-0403.gif" runat="server" OnClick="TQ_Click" />
                                        <asp:Button ID="BtnRmss" runat="server" OnClick="BtnRmss_Click" Style="display: none" />
                                    </td>
                                    <td>
                                    <td>
                                        <asp:ImageButton ID="SelectBOSS" runat="server" OnClientClick="windowOpenRmssSelect()"
                                            src="../Images/Small/bb_table.gif" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="25%" align="center" class="tdBak">
                            业务名称
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="SUB_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak">
                            销售
                        </td>
                        <td>
                            <asp:TextBox ID="SALE_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak">
                            代理商
                        </td>
                        <td>
                            <asp:TextBox ID="PARTNER_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="tableTitle1">
                配置基本信息
            </td>
        </tr>
        <tr>
            <td>
                <table border="1" bordercolor="#5b9ed1" cellpadding="0" cellspacing="0" style="border-collapse: collapse;"
                    width="100%">
                    <tr>
                        <td align="center" class="tdBak" width="25%">
                            用户自有网关
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="YHZYIP" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="25%">
                            用户路由网段
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="YHLYWD" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak">
                            局方IP
                        </td>
                        <td>
                            <asp:TextBox ID="JFIP" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak">
                            VPN编号
                        </td>
                        <td>
                            <asp:TextBox ID="VPNBH" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak">
                            设备配置信息
                        </td>
                        <td>
                            <asp:TextBox ID="SBPZXX" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak">
                            Wlan
                        </td>
                        <td>
                            <asp:TextBox ID="Wlan" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak">
                            IP地址
                        </td>
                        <td>
                            <asp:TextBox ID="IPDZ" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak">
                            个数
                        </td>
                        <td>
                            <asp:TextBox ID="GS" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak">
                            接入方式
                        </td>
                        <td>
                            <asp:DropDownList ID="JRFS" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="JRFS_SelectedIndexChanged">
                                <asp:ListItem>交换机改路由器</asp:ListItem>
                                <asp:ListItem>路由器改交换机接入</asp:ListItem>
                                <asp:ListItem>交换机接入VPN用户业务变更</asp:ListItem>
                                <asp:ListItem>路由器接入VPN用户业务变更</asp:ListItem>
                                <asp:ListItem>所属VPN（VRF）变更</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="center" class="tdBak" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr id="TR_PZHLDZ" runat="server" style="display: none">
                        <td align="center" class="tdBak">
                            现需要配置的互连地址
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="PZHLDZ" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="TR_PZLYWD" runat="server" style="display: none">
                        <td align="center" class="tdBak">
                            需要配置（分发）的路由网段
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="PZLYWD" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="TR_LYFFYQ" runat="server" style="display: none">
                        <td align="center" class="tdBak">
                            路由分发要求
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="LYFFYQ" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="TR_PZJHDZWD" runat="server" style="display: none">
                        <td align="center" class="tdBak">
                            需要配置的交换地址网段
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="PZJHDZWD" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="TR_HLDZSFSH" runat="server" style="display: none">
                        <td align="center" class="tdBak">
                            原互连地址是否收回
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="HLDZSFSH" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="TR_SSVPN" runat="server" style="display: none">
                        <td align="center" class="tdBak">
                            更改后所属VPN名称（VRF）
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="SSVPN" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="TR_YJRFS" runat="server" style="display: none">
                        <td align="center" class="tdBak">
                            原接入方式（交换机/路由器）
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="YJRFS" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="TR_TSSM" runat="server" style="display: none">
                        <td align="center" class="tdBak">
                            特殊说明
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="TSSM" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="TR_BZ" runat="server" style="display: none">
                        <td align="center" class="tdBak">
                            备注
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="BZ" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JDBM" runat="server" Style="display: none"></asp:TextBox>
    <asp:GridView ID="GridViewList" runat="server" SkinID="GridView1" BorderWidth="1px"
        AllowPaging="True" AllowSorting="True" CellSpacing="1" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField HeaderText="操作" DataField="QFBJ">
                <HeaderStyle Width="5%" />
                <ItemStyle HorizontalAlign="Center" Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="LCJRSJ" HeaderText="流程进入时间" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                <HeaderStyle Width="12%" />
                <ItemStyle HorizontalAlign="Center" Width="12%" />
            </asp:BoundField>
            <asp:BoundField HeaderText="当前状态" DataField="DQZT">
                <HeaderStyle Width="8%" />
                <ItemStyle Width="8%" />
            </asp:BoundField>
            <asp:BoundField DataField="LCQFSJ" HeaderText="流程操作时间" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                <HeaderStyle Width="12%" />
                <ItemStyle HorizontalAlign="Center" Width="12%" />
            </asp:BoundField>
            <asp:BoundField DataField="QFHZT" HeaderText="操作后状态">
                <HeaderStyle Width="8%" />
                <ItemStyle Width="8%" />
            </asp:BoundField>
            <asp:BoundField DataField="LCQFR" HeaderText="流程操作人">
                <HeaderStyle Width="14%" />
                <ItemStyle Width="14%" />
            </asp:BoundField>
            <asp:BoundField DataField="LCCLSJ" HeaderText="流程处理时间">
                <HeaderStyle Width="12%" />
                <ItemStyle HorizontalAlign="Right" Width="12%" />
            </asp:BoundField>
            <asp:BoundField DataField="LCSM" HeaderText="流程说明">
                <HeaderStyle Width="30%" />
                <ItemStyle Width="30%" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
