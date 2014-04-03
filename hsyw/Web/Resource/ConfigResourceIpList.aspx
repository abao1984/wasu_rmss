<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigResourceIpList.aspx.cs"
    Inherits="Web_Resource_ConfigResourceIpList" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<%@ Register Assembly="IDP.WebControls" Namespace="IDP.WebControls" TagPrefix="idp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function windowOpen(guid) {
            var url = "ConfigResourceIpEdit.aspx?GUID=" + guid;
            windowOpenPage(url, "IP资源配置", "BtnQuery");
            window.event.returnValue = false;
        }
        function OpenBranch(name, code) {
            windowOpenPageByWidth("BranchTree.aspx?NAME=" + name + "&CODE=" + code + "&ISQY=1", "选择所属区域", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }

        function windowOpenRmssSelect() {
            var url = "RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID";
            windowOpenPage(url, "选择客户资源", "BtnRmss");
        }

        function windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, isEqucode) {
            var txt_code = txt_name + "_CODE";
            var txt_guid = txt_name + "_GUID";
            var res_guid = "";
            var res_code = "";
            var res_name = "";
            if (linkage_code != "") {
                try {
                    res_guid = document.getElementById(p_txt_name + "_GUID").value;
                } catch (e) { }
                //if (isEqucode == "1") {
                try {
                    res_code = document.getElementById(p_txt_name + "_CODE").value;
                } catch (e) { }
                res_name = document.getElementById(p_txt_name).value;
            }
            var url = "PhyEquSelect.aspx?ISEQUCODE=" + isEqucode + "&UNIT_ID=" + unit_id + "&TXT_NAME=" + txt_name + "&TXT_CODE=" + txt_code + "&TXT_GUID=" + txt_guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name) + "&NAME_FILED=" + linkage_code + "&P_TXT_NAME=" + p_txt_name; ;
            windowOpenPage(url, "资源选择", "");
        }
        function windowOpenEqu(txt_fl, txt_name, p_txt_name, linkage_code) {
            var unit_id = document.getElementById(txt_fl).value;
            windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, '1')
        }
        function OpenSelect(enumtype, pname, ddlid) {
            var url = "ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
            windowOpenPageByWidth(url, "枚举维护", "Btn", "30%", "40%", "10%", "80%");
            document.getElementById("DDLID").value = ddlid;
            document.getElementById("DDLLX").value = enumtype;
        }
        function windowOpenLogicResourceVlanSelect() {
            var wljf_guid = "";
            var wljf = document.getElementById("JFMC").value;
            var ssqy_code = document.getElementById("REGION_CODE").value;
            var ssqy = document.getElementById("REGION").value;

            var url = "LogicResourceVlanSelect.aspx?SSJF_GUID=" + wljf_guid + "&SSJF=" + encodeURI(wljf) + "&SSQY_CODE=" + ssqy_code + "&SSQY=" + encodeURI(ssqy);
            windowOpenPage(url, "选择Vlan资源", "");
            window.event.returnValue = false;
        }
        function windowOpenLogicResourceVpnSelect(name) {
            var url = "LogicResourceVpnSelect.aspx?NAME=" + name;
            windowOpenPage(url, "选择VPN资源", "");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%;">
        <tr>
            <td class="tableHead">
                <asp:Button ID="BtnQuery" runat="server" Text="查询" CssClass="btn_2k3" OnClick="BtnQuery_Click" />
                <asp:Button ID="BtnAdd" runat="server" Text="新增" CssClass="btn_2k3" OnClientClick="windowOpen('','')" />
                <asp:Button ID="BtnDel" runat="server" Text="删除" OnClientClick="return confirm('确定要删除吗？')"
                    CssClass="btn_2k3" OnClick="BtnDel_Click" />
                <asp:Button ID="BtnRmss" runat="server" OnClick="BtnRmss_Click" Style="display: none" />
                <asp:Button ID="BtnExport" runat="server" Text="导出Excel" CssClass="btn_2k3" OnClick="BtnExport_Click" />
            </td>
        </tr>
        <tr>
            <td height="1">
                <table style="width: 100%; border-collapse: collapse;" cellpadding="0" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            业务编码
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="SUBSCRIBER_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick=" windowOpenRmssSelect()" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            业务类型
                        </td>
                        <td width="13%">
                            <asp:DropDownList ID="YWLX" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            机房名称
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="JFMC" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','JFMC','REGION','HOUSE_AREA','1')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            机房编码
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="JF_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            客户等级
                        </td>
                        <td width="13%">
                            <asp:DropDownList ID="CUSTOMER_LEVEL" runat="server" Width="100%" AutoPostBack="false">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="1">1级</asp:ListItem>
                                <asp:ListItem Value="2">2级</asp:ListItem>
                                <asp:ListItem Value="3">3级</asp:ListItem>
                                <asp:ListItem Value="4">4级</asp:ListItem>
                                <asp:ListItem Value="5">5级</asp:ListItem>
                                <asp:ListItem Value="6">6级</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            客户编码
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="CUSTOMER_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            所属区域
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
                            客户地址
                        </td>
                        <td style="width: 26%">
                            <asp:TextBox ID="ADDRESS" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            接入单位
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="JRDW" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            ONUMAC地址
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="ONUMAC" runat="server" BackColor="#F0F0F0" BorderWidth="0" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('6745b937-7642-459b-b237-aac52ee7fbff','ONUMAC','','','0','')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            设备配置信息
                        </td>
                        <td>
                            <asp:TextBox ID="SBPZXX" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak">
                            PVCID值
                        </td>
                        <td>
                            <asp:TextBox ID="PVCID" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            互通VPN
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="HTVPN" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td id="td_HTVPN" runat="server">
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenLogicResourceVpnSelect('HTVPN')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            VPN
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="VPN" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td id="td_VPN" runat="server">
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenLogicResourceVpnSelect('VPN')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            IP网关
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="YHZYIP" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            VLAN编号
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="VLAN" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenLogicResourceVlanSelect('VLAN')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            配置时间
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="PZKSSJ" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" >至</td>
                        <td width="13%">
                            <asp:TextBox ID="PZJSSJ" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%" valign="top">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="gvLogicEquIp" runat="server" SkinID="GridView1" DataKeyNames="GUID,SUBSCRIBER_ID"
                        Width="100%" BorderColor="#5B9ED1" BorderWidth="1px" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" OnRowDataBound="gvLogicEquIp_RowDataBound" OnSorting="gvLogicEquIp_Sorting">
                        <PagerSettings PageButtonCount="100" />
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="5%" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="SUBSCRIBER_CODE" HeaderText="业务编码" ItemStyle-BorderColor="#5b9ed1"
                                SortExpression="SUBSCRIBER_CODE">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SBPZXX" HeaderText="设备配置信息" ItemStyle-BorderColor="#5b9ed1"
                                SortExpression="SBPZXX">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JRDW" HeaderText="客户名称" ItemStyle-BorderColor="#5b9ed1" SortExpression="JRDW">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="YWLX" HeaderText="业务类型" ItemStyle-BorderColor="#5b9ed1" SortExpression="YWLX">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="14%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="REGION" HeaderText="所属区域" ItemStyle-BorderColor="#5b9ed1" SortExpression="REGION">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="WLJF" HeaderText="机房名称" SortExpression="WLJF">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="14%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PVCID" HeaderText="PVCID" SortExpression="PVCID">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" HorizontalAlign="Center" Width="12%">
                                </ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td height="1">
                 <table class="tdBak" id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td width="100%" style="padding-left: 6px;">
                            <font face="宋体">总共有
                                <asp:Label ID="DataCountLab" runat="server" ForeColor="Red"></asp:Label></font><font
                                    face="宋体">条记录，当前第
                                    <asp:Label ID="PageIndexLab" runat="server" ForeColor="Red"></asp:Label>页，共
                                    <asp:Label ID="PageCountLab" runat="server" ForeColor="Red"></asp:Label>页</font>
                        </td>
                        <td width="1">
                            <asp:Label ID="Label1" runat="server" Width="55px">单页显示</asp:Label>
                        </td>
                        <td width="1">
                            <font face="宋体">
                                <asp:DropDownList ID="PageSize" runat="server" ForeColor="Red" Font-Bold="True" AutoPostBack="True"
                                    Width="60px" OnSelectedIndexChanged="PageSize_SelectedIndexChanged">
                                    
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="500">500</asp:ListItem>
                                </asp:DropDownList>
                            </font>
                        </td>
                        <td width="1">
                            <asp:LinkButton ID="PrevButton" runat="server" ForeColor="#003797" Width="50px" OnClick="PrevButton_Click">上一页</asp:LinkButton>
                        </td>
                        <td width="1">
                            <asp:DropDownList ID="GridPageList" runat="server" ForeColor="Red" Font-Bold="True"
                                AutoPostBack="True" Width="50px" OnSelectedIndexChanged="GridPageList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="1">
                            <asp:LinkButton ID="NextButton" runat="server" ForeColor="#003797" Width="50px" OnClick="NextButton_Click">下一页</asp:LinkButton>
                        </td>
                    </tr>
                </table>
                
                <%--<idp:Pager ID="Pager1" runat="server" OnPageIndexChanged="Pager1_PageIndexChanged" />--%>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="ZYHS_BJ" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="REGION_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JFMC_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="GXH_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="GLDMC_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JFMC_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="DDLID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="DDLLX" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="SUBSCRIBER_ID" runat="server" Width="0px" Style="display: none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
