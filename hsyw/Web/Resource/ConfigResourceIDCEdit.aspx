<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigResourceIDCEdit.aspx.cs"
    Inherits="Web_Resource_ConfigResourceIDCEdit" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../config.js"></script>

    <script language="javascript" type="text/javascript">

        function windowOpenLogicResourceVpnSelect(name) {
            var url = "LogicResourceVpnSelect.aspx?NAME=" + name;
            windowOpenPage(url, "选择VPN资源", "");
        }

        function windowOpenLogicResourceVlanSelect() {
            var guid = document.getElementById("GUID").value;
            //             var wljf_guid = document.getElementById("WLJF_GUID").value;
            //             var wljf = document.getElementById("WLJF").value;

            var ljjf_guid = document.getElementById("WLJF_GUID").value;
            var ljjf = document.getElementById("WLJF").value;
            var ssqy_code = document.getElementById("REGION_CODE").value;
            var ssqy = document.getElementById("REGION").value;

            var url = "LogicResourceVlanSelect.aspx?PK_GUID=" + guid + "&SSJF_GUID=" + ljjf_guid + "&SSJF=" + encodeURI(ljjf) + "&SSQY_CODE=" + ssqy_code + "&SSQY=" + encodeURI(ssqy);
            windowOpenPage(url, "选择Vlan资源", "");
            window.event.returnValue = false;
        }

        function windowOpenRmssSelect() {
            var url = "RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID";
            windowOpenPage(url, "选择客户资源", "BtnRmss");
            event.returnValue = false;
        }

        function windowOpen() {

            var guid = document.getElementById("GUID").value;
            var ljjf_guid = document.getElementById("WLJF_GUID").value;

            var ljjf = document.getElementById("WLJF").value;
            var ssqy_code = document.getElementById("REGION_CODE").value;
            var ssqy = document.getElementById("REGION").value;

            var url = "LogicResourceIpSelect1.aspx?PK_GUID=" + guid + "&HOUSE_GUID=" + ljjf_guid + "&HOUSE_NAME=" + encodeURI(ljjf) + "&SSQY_CODE=" + ssqy_code + "&SSQY=" + encodeURI(ssqy);
            windowOpenPage(url, "分配局IP地址", "Btn");
            window.event.returnValue = false;
        }

        function OpenBranch(name, code) {
            windowOpenPageByWidth("BranchTree.aspx?NAME=" + name + "&CODE=" + code + "&ISQY=1", "选择所属区域", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }

        function windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, isEqucode, ljjf, zysx) {
            //unit_id 资源主键 txt_name 所选资源文本框名称 p_txt_name 反推上层次资源文本框名称 linkage_code 上一层次资源编码
            //isEqucode 是否存在编码 ljjf 逻辑机房编码 zysx 返回资源属性，格式为 文本框名称1,文本框名称2:资源属性名称1,资源属性名称2
            var txt_code = txt_name + "_CODE";
            var txt_guid = txt_name + "_GUID";
            var res_guid = "";
            var res_code = "";
            var res_name = "";

            //如果为接入端口，保存原端口guid
            if (txt_name == "JRDK" && document.getElementById("oldJRDK_GUID").value == "") {
                document.getElementById("oldJRDK_GUID").value += document.getElementById(p_txt_name + "_GUID").value;
                //alert(document.getElementById("oldJRDK_GUID").value);
            }
            if (linkage_code != "") {
                try {
                    res_guid = document.getElementById(p_txt_name + "_GUID").value;
                } catch (e) { }
                //if (isEqucode == "1") {
                try {
                    res_code = document.getElementById(p_txt_name + "_CODE").value;
                } catch (e) { }
                //}
                res_name = document.getElementById(p_txt_name).value;
            }
            var url = "PhyEquSelect.aspx?ISEQUCODE=" + isEqucode + "&UNIT_ID=" + unit_id + "&TXT_NAME=" + txt_name + "&TXT_CODE=" + txt_code + "&TXT_GUID=" + txt_guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name) + "&NAME_FILED=" + linkage_code + "&P_TXT_NAME=" + p_txt_name + "&LJJF_NAME=" + ljjf + "&ZYSX=" + zysx;
            windowOpenPage(url, "资源选择", "");
        }

        function OpneLogList() {
            var guid = document.getElementById("GUID").value;
            var url = "../LogList.aspx?PK_GUID=" + guid;
            windowOpenPage(url, "操作日志", "");
        }

        function OpenSelect(enumtype, pname, ddlid) {
            var url = "ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
            windowOpenPageByWidth(url, "枚举维护", ddlid, "30%", "40%", "10%", "80%");
        }
        function windowOpenRmssTQ() {
            var url = "RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID&SUBSCRIBER_CODE=" + document.getElementById("SUBSCRIBER_CODE").value;
            windowOpenPage(url, "选择客户资源", "BtnRmss");
            //event.returnValue = false;
        }
        function ViewPzb() {
            var bh = document.getElementById("SUBSCRIBER_CODE").value;
            windowOpenPage("ZyPzList.aspx?bh=" + bh, "操作日志", "");
            event.returnValue = false;

        }
    </script>

    <style type="text/css">
        .style1
        {
            background: #e6f3fc repeat-x;
            color: #003797;
            font-family: "宋体";
            font-size: 12px; /*font-weight: bold;*/
            line-height: 26px;
            height: 29px;
        }
        .style2
        {
            height: 29px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%;">
        <tr>
            <td align="center" class="tableHead">
                <asp:Button ID="Btn" runat="server" Style="display: none;" OnClick="Btn_Click" />
                <asp:Button ID="BtnDk" runat="server" Style="display: none;" OnClick="BtnDk_Click" />
                <asp:Button ID="BtnVlan" runat="server" Style="display: none;" OnClick="BtnVlan_Click" />
                <asp:Button ID="BtnRmss" runat="server" OnClick="BtnRmss_Click" Style="display: none" />
                <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td width="1">
                            <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" OnClick="SaveButton_Click"
                                Text="保存" />
                        </td>
                        <td width="1">
                            <asp:Button ID="BtnZyhs" runat="server" Text="资源回收" CssClass="btn_2k3" OnClick="BtnZyhs_Click"
                                OnClientClick="return confirm(&quot;确定操作吗？&quot;);" style="display:none;" />
                        </td>
                        <td width="1">
                            <asp:Button ID="BtnExp" runat="server" Text="导出Excel" CssClass="btn_2k3" OnClick="BtnExp_Click" />
                        </td>
                        <td width="1">
                            <asp:Button ID="BtnView" runat="server" Text="查看相关配置单" OnClientClick="ViewPzb();"
                                CssClass="btn_2k3" OnClick="BtnView_Click" />
                        </td>
                        <td align="right">
                            <a href="#" onclick="OpneLogList()">操作日志</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                        border="1" bordercolor="#5b9ed1">
                        <tr>
                            <td class="tdBak" width="15%" align="center">
                                业务编码
                            </td>
                            <td width="18%">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:TextBox ID="SUBSCRIBER_ID" runat="server" BorderWidth="0" Style="display: none"
                                                Width="100%"></asp:TextBox>
                                            <asp:TextBox ID="SUBSCRIBER_CODE" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="TQ" ImageUrl="../Images/Small/gif-0403.gif" runat="server" OnClick="TQ_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="SelectBOSS" runat="server" OnClientClick="windowOpenRmssSelect()"
                                                src="../Images/Small/bb_table.gif" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tdBak" width="15%" align="center">
                                工单来源
                            </td>
                            <td width="18%">
                                <asp:RadioButtonList ID="SUBSCRIBER_GDLY" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="SUBSCRIBER_GDLY_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Selected="True">BOSS</asp:ListItem>
                                    <asp:ListItem Value="SGD">手工单</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="tdBak" width="15%" align="center">
                                业务类型
                            </td>
                            <td width="18%">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:DropDownList ID="YWLX" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="YWLX_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnYwfl" runat="server" OnClick="BtnYwfl_Click" Style="display: none;" />
                                            <img src="../Images/Small/bb_table.gif" onclick="OpenSelect('IDC_YWLX','','BtnYwfl')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdBak" width="15%" align="center">
                                设备配置信息
                            </td>
                            <td width="18%">
                                <asp:TextBox ID="SBPZXX" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td class="tdBak" width="15%" align="center">
                                接入单位
                            </td>
                            <td width="18%">
                                <asp:TextBox ID="JRDW" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td class="tdBak" align="center">
                                物理机房
                            </td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:TextBox ID="WLJF" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                        </td>
                                        <td>
                                            <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','WLJF','REGION','HOUSE_AREA','1','LJJF')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdBak" align="center">
                                机柜
                            </td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:TextBox ID="JG" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                        </td>
                                        <td id="td2" runat="server">
                                            <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('ee01eccb-7c95-4b96-b122-ed9913026f24','JG','WLJF','HOUSE_NAME','0','');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tdBak" align="center">
                                U编号
                            </td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:TextBox ID="U1" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                        </td>
                                        <td id="td3" runat="server">
                                            <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('2a936b1a-43d3-4e62-bcea-3073fb9a8ca7','U1','JG','U_CODE','0','');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tdBak" align="center">
                                -
                            </td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:TextBox ID="U2" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                        </td>
                                        <td id="td4" runat="server">
                                            <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('2a936b1a-43d3-4e62-bcea-3073fb9a8ca7','U2','JG','STATION','0','');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdBak" align="center">
                                设备编号
                            </td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:TextBox ID="SBMC_CODE" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                        </td>
                                        <td id="td_SBMC" runat="server">
                                            <%--<img src="../Images/Small/bb_table.gif"  onclick="windowOpenPhyResourceSelect('64602091-d4fe-4c89-ac6a-52f6acdd836d','SBMC','WLJF','HOUSE_NAME','1','','')" />--%>
                                            <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('64602091-d4fe-4c89-ac6a-52f6acdd836d','SBMC','WLJF','HOUSE_NAME','1','LJJF','')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tdBak" align="center">
                                接入端口
                            </td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:TextBox ID="JRDK" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                        </td>
                                        <td>
                                            <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('bfc13d2d-eab8-4784-a96a-b8ffc21b4e88','JRDK','SBMC','EQU_NAME','0','');" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tdBak" align="center">
                                VPN
                            </td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:TextBox ID="VPN" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                        </td>
                                        <td id="td1" runat="server">
                                            <img src="../Images/Small/bb_table.gif" onclick="windowOpenLogicResourceVpnSelect('VPN')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdBak" align="center" width="15%">
                                带宽
                            </td>
                            <td width="18%">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:DropDownList ID="DK" runat="server" Width="100%" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <img src="../Images/Small/bb_table.gif" onclick="OpenSelect('DK','','BtnDk')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tdBak" align="center">
                                用户自用IP网关
                            </td>
                            <td>
                                <asp:TextBox ID="YHZYIP" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td class="tdBak" align="center">
                                用户路由网段
                            </td>
                            <td>
                                <asp:TextBox ID="YHLYWD" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdBak" align="center" colspan="6">
                                用户信息
                            </td>
                        </tr>
                        <tr>
                            <td class="tdBak" align="center">
                                区域
                            </td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100%">
                                            <asp:TextBox ID="REGION" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                        <td>
                                            <img src="../Images/Small/bb_table.gif" onclick="OpenBranch('REGION','REGION_CODE')" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tdBak" align="center">
                                客户编码
                            </td>
                            <td>
                                <asp:TextBox ID="CUSTOMER_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td class="tdBak" align="center">
                                客户名称
                            </td>
                            <td>
                                <asp:TextBox ID="CUSTOMER_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdBak" align="center">
                                客户类型1
                            </td>
                            <td>
                                <asp:TextBox ID="CUSTTYPE1" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td class="tdBak" align="center">
                                客户类型
                            </td>
                            <td>
                                <asp:TextBox ID="CUSTTYPE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td class="tdBak" align="center">
                                客户等级
                            </td>
                            <td>
                                <asp:DropDownList ID="CUSTOMER_LEVEL" runat="server" Width="100%" AutoPostBack="True">
                                    <asp:ListItem Value="1">1级</asp:ListItem>
                                    <asp:ListItem Value="2">2级</asp:ListItem>
                                    <asp:ListItem Value="3">3级</asp:ListItem>
                                    <asp:ListItem Value="4">4级</asp:ListItem>
                                    <asp:ListItem Value="5">5级</asp:ListItem>
                                    <asp:ListItem Value="6">6级</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" align="center">
                                业务名称
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="SUB_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td class="style1" align="center">
                                业务联系人
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="LINKMAN" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td class="style1" align="center">
                                email
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="EMAIL" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" align="center">
                                电话号码
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="PHONE_NO" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td class="style1" align="center">
                                传真号码
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="FAX_NO" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td class="style1" align="center">
                                手机号码
                            </td>
                            <td class="style2">
                                <asp:TextBox ID="MOBILE_NO" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdBak" align="center">
                                邮编
                            </td>
                            <td>
                                <asp:TextBox ID="ZIP_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td class="tdBak" align="center">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td class="tdBak" align="center">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="tdBak" align="center">
                                客户地址
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="ADDRESS" runat="server" Width="100%" BorderWidth="0" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                销售员
                            </td>
                            <td>
                                <asp:TextBox ID="SALE_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                配置人
                            </td>
                            <td>
                                <asp:TextBox ID="PZR" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                配置时间
                            </td>
                            <td>
                                <asp:TextBox ID="PZSJ" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdBak" align="center">
                                备注
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="BZ" runat="server" Width="100%" BorderWidth="0" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="tr_DDXX" runat="server">
                            <td class="tdBak" align="center" colspan="6">
                                IDC信息
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                设备编号
                            </td>
                            <td>
                                <asp:TextBox ID="DEV_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                设备型号
                            </td>
                            <td>
                                <asp:TextBox ID="dev_name" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                应用名称
                            </td>
                            <td>
                                <asp:TextBox ID="dev_app" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                设备序列号
                            </td>
                            <td>
                                <asp:TextBox ID="dev_sn" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                设备U数
                            </td>
                            <td>
                                <asp:TextBox ID="dev_u" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                上联端口IP
                            </td>
                            <td>
                                <asp:TextBox ID="dev_uplink_ip" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                上联端口
                            </td>
                            <td>
                                <asp:TextBox ID="dev_uplink_port" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                资产编号
                            </td>
                            <td>
                                <asp:TextBox ID="asset_code" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                资产所属
                            </td>
                            <td>
                                <asp:TextBox ID="asset_properties" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                设备状态
                            </td>
                            <td>
                                <asp:TextBox ID="dev_stat" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                设备上线时间
                            </td>
                            <td>
                                <asp:TextBox ID="dev_start_time" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                设备下线时间
                            </td>
                            <td>
                                <asp:TextBox ID="dev_end_time" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                操作员
                            </td>
                            <td>
                                <asp:TextBox ID="dev_creater" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                修改员
                            </td>
                            <td>
                                <asp:TextBox ID="dev_updater" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                用户设备编号
                            </td>
                            <td>
                                <asp:TextBox ID="DEV_YHCODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                设备所属人
                            </td>
                            <td>
                                <asp:TextBox ID="DEV_SBSSR" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                设备所属人电话
                            </td>
                            <td>
                                <asp:TextBox ID="DEV_SBSSRDH" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                电源
                            </td>
                            <td>
                                <asp:TextBox ID="DEV_DY" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                备注
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="DEV_BZ" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="tr_xlxx" runat="server">
                            <td class="tdBak" align="center" colspan="6">
                                线路信息
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                所用纤号
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox1" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                纤号总资源
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="TextBox2" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                二级机柜号
                            </td>
                            <td>
                                <asp:TextBox ID="XL_EJJG_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                二级配线架名称
                            </td>
                            <td>
                                <asp:TextBox ID="XL_EJPXJMC" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                二级配线架端口
                            </td>
                            <td>
                                <asp:TextBox ID="XL_EJPXJDK" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                一级机柜号
                            </td>
                            <td>
                                <asp:TextBox ID="XL_YJJG_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                一级配线架名称
                            </td>
                            <td>
                                <asp:TextBox ID="XL_YJPXJMC" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                一级配线架端口
                            </td>
                            <td>
                                <asp:TextBox ID="XL_YJPXJDK" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                备注
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="XL_BZ" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="Btntr" runat="server">
                            <td colspan="3" class="tableHead">
                                <asp:Button ID="AddButton" runat="server" CssClass="btn_2k3" Text="新增局IP地址" OnClientClick="windowOpen()" />
                                <asp:Button ID="deleteButton" runat="server" CssClass="btn_2k3" Text="回收局IP地址" OnClick="deleteButton_Click" />
                            </td>
                            <td colspan="3" class="tableHead">
                                <asp:Button ID="AddButton0" runat="server" CssClass="btn_2k3" Text="新增VLAN" OnClientClick="windowOpenLogicResourceVlanSelect()" />
                                <asp:Button ID="deleteVlanButton" runat="server" CssClass="btn_2k3" Text="回收VLAN"
                                    OnClick="deleteVlanButton_Click" />
                            </td>
                        </tr>
                        <tr id="gvtr" runat="server">
                            <td colspan="3" valign="top">
                                <asp:GridView ID="gvLogicEquIp" runat="server" SkinID="GridView2" DataKeyNames="GUID"
                                    Width="100%" BorderColor="#5B9ED1" BorderWidth="1px" AllowSorting="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="选择">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="XZ" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PZ_IPYWLX" HeaderText="业务类型">
                                            <ItemStyle Width="25%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="WLJF" HeaderText="机房名称">
                                            <ItemStyle Width="25%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IPDZ" HeaderText="局方IP">
                                            <ItemStyle Width="40%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td colspan="3" valign="top">
                                <asp:GridView ID="gvLogicEquVlan" runat="server" SkinID="GridView2" DataKeyNames="GUID,VLANGUID"
                                    Width="100%" BorderColor="#5B9ED1" BorderWidth="1px" AllowSorting="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="选择">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="XZ1" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="WLJF" HeaderText="所属机房">
                                            <ItemStyle Width="50%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VLANBH" HeaderText="VLAN编号">
                                            <ItemStyle Width="30%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="LLFX" runat="server" Style="display: none;">1</asp:TextBox>
    <asp:TextBox ID="ZYHS_BJ" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="DDVLAN_CODE" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="DDVLAN_GUID" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="oldJRDK_GUID" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="JRDK_GUID" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="GUID" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="HTVPN_GUID" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="HTVPN_CODE" runat="server" Width="100%" BorderWidth="0" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="VPN_GUID" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="VPN_CODE" runat="server" Width="100%" BorderWidth="0" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="VLAN_GUID" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="REGION_CODE" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="WLJF_GUID" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="WLJF_CODE" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="LJJF_GUID" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="LJJF_CODE" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="SBMC_GUID" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="SBMC" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="ONUMAC_GUID" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="CREATEDATETIME" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="ISNEW" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="UPDATEDATETIME" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="UPDATEUSERNAME" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="JG_GUID" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="JG_CODE" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="U1_GUID" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="U1_CODE" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="U2_GUID" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="U2_CODE" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="TextBox6" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="TextBox7" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="TextBox8" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
