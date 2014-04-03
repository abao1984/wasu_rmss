<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigLightEditVpn.aspx.cs"
    Inherits="Web_Resource_ConfigLightEditVpn" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script language="javascript" type="text/javascript" src="../../config.js"></script>

    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="ResourceScript.js"></script>

    <script language="javascript" type="text/javascript">
        function ShowUser() {
            var ret = window.showModalDialog("../SelectUsers.aspx?username=" + document.getElementById("SQR").value + "&IsDX=1", "", "dialogWidth:700px;dialogHeight:500px;center:yes;location:no;status:no;");
            if (typeof (ret) != "undefined") {
                document.getElementById("SQRNAME").value = ret[0];
                document.getElementById("SQR").value = ret[1];
            }
        }
        //        function windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, isEqucode) {
        //            var txt_code = txt_name + "_CODE";
        //            var txt_guid = txt_name + "_GUID";
        //            var res_guid = "";
        //            var res_code = "";
        //            var res_name = "";
        //            if (linkage_code != "") {
        //                res_guid = document.getElementById(p_txt_name + "_GUID").value;
        //                if (isEqucode == "1") {
        //                    res_code = document.getElementById(p_txt_name + "_CODE").value;
        //                }
        //                res_name = document.getElementById(p_txt_name).value;
        //            }
        //            var url = "PhyEquSelect.aspx?ISEQUCODE=" + isEqucode + "&UNIT_ID=" + unit_id + "&TXT_NAME=" + txt_name + "&TXT_CODE=" + txt_code + "&TXT_GUID=" + txt_guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name) + "&NAME_FILED=" + linkage_code + "&P_TXT_NAME=" + p_txt_name; ;
        //            windowOpenPage(url, "资源选择", "");  
        //        }
        function windowOpenEqu(txt_fl, txt_name, p_txt_name, linkage_code) {
            var unit_id = document.getElementById(txt_fl).value;
            windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, '1')
        }
        function windowOpen(guid, lb, house) {
            var yhmc = document.getElementById("CUSTOMER_NAME").value;
            var house_guid = document.getElementById(house + "_GUID").value;
            var house_name = document.getElementById(house).value;
            var url = "GuangLanDuan.aspx?HOUSE_GUID=" + house_guid + "&HOUSE_NAME=" + encodeURI(house_name) + "&GUID=" + guid + "&LIGHTGUID=" + document.getElementById("YWGUID").value + "&LB=" + lb + "&YWBM=" + document.getElementById("SUBSCRIBER_CODE").value + "&yhmc=" + encodeURI(yhmc);
            windowOpenPage(url, "光缆段", "Btn1");
            window.event.returnValue = false;
        }

        function OpenSelect(enumtype, pname, ddlid) {
            var url = "ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
            windowOpenPageByWidth(url, "枚举维护", "Btn", "30%", "40%", "10%", "80%");
            document.getElementById("DDLID").value = ddlid;
            document.getElementById("DDLLX").value = enumtype;
        }

        function windowOpenRmssSelect() {
            var url = "RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID";
            windowOpenPage(url, "选择客户资源", "BtnRmss");
            event.returnValue = false;
        }
        function windowOpenRmssTQ() {
            var url = "RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID&SUBSCRIBER_CODE=" + document.getElementById("SUBSCRIBER_CODE").value;
            windowOpenPage(url, "选择客户资源", "BtnRmss");
            //event.returnValue = false;
        }

        function OpneLogList() {
            var guid = document.getElementById("YWGUID").value;
            var url = "../LogList.aspx?PK_GUID=" + guid;
            windowOpenPage(url, "操作日志", "");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%;">
        <tr>
            <td class="tableHead" runat="server" id="query">
                <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="1" align="right">
                            <asp:Button ID="BtnSave" runat="server" Text="保存" CssClass="btn_2k3" OnClick="BtnSave_Click" />
                        </td>
                        <td width="1">
                            <asp:Button ID="BtnZyhs" runat="server" Text="资源回收" CssClass="btn_2k3" OnClientClick="return confirm(&quot;确定操作吗？&quot;);"
                                OnClick="BtnZyhs_Click" />
                            <asp:Button ID="BtnRmss" runat="server" OnClick="BtnRmss_Click" Style="display: none" />
                        </td>
                        <td align="right">
                            <a href="#" onclick="OpneLogList()">操作日志</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tableTitle" style="height: 1px">
                <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td style="width: 15%; color: Red" align="center">
                            业务类型
                        </td>
                        <td style="width: 18%; background-color: White">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:DropDownList ID="YWLX" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="YWLX_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenSelect('GLYWLX','','YWLX')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 15%; color: Red" align="center">
                            业务编码
                        </td>
                        <td style="width: 18%; background-color: White">
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
                                    <td>
                                        <asp:ImageButton ID="SelectBOSS" runat="server" OnClientClick="windowOpenRmssSelect()"
                                            src="../Images/Small/bb_table.gif" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 15%;" align="center" class="tdBak">
                            工单来源
                        </td>
                        <td style="width: 15%;" align="center">
                            <asp:RadioButtonList ID="SUBSCRIBER_GDLY" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="SUBSCRIBER_GDLY_SelectedIndexChanged">
                                <asp:ListItem Selected="True">BOSS</asp:ListItem>
                                <asp:ListItem Value="SGD">手工单</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="100%" valign="top">
                <div style="overflow: auto; width: 100%; height: 100%">
                    <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                        border="1" bordercolor="#5b9ed1">
                        <tr>
                            <td align="center" class="tdHead" colspan="2">
                                用户信息
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                                    border="1" bordercolor="#5b9ed1">
                                    <tr>
                                        <td align="center" class="tdBak">
                                            客户编码
                                        </td>
                                        <td class="style2">
                                            <asp:TextBox ID="CUSTOMER_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak">
                                            客户名称
                                        </td>
                                        <td class="style2" colspan="3">
                                            <asp:TextBox ID="CUSTOMER_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak">
                                            &nbsp;客户等级
                                        </td>
                                        <td class="style2">
                                            <asp:DropDownList ID="CUSTOMER_LEVEL" runat="server" AutoPostBack="True" Width="100%">
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
                                        <td align="center" class="tdBak" style="width: 12%;">
                                            客户类型1
                                        </td>
                                        <td style="width: 13%;">
                                            <asp:TextBox ID="CUSTTYPE1" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak" style="width: 12%;">
                                            客户类型
                                        </td>
                                        <td style="width: 13%;">
                                            <asp:TextBox ID="CUSTTYPE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak" style="width: 12%;">
                                            区域
                                        </td>
                                        <td colspan="3">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="REGION" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenBranchTree('REGION','REGION_CODE')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tdBak">
                                            用户地址
                                        </td>
                                        <td colspan="7">
                                            <asp:TextBox ID="ADDRESS" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tdBak">
                                            传真号码
                                        </td>
                                        <td>
                                            <asp:TextBox ID="FAX_NO" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak">
                                            邮编
                                        </td>
                                        <td>
                                            <asp:TextBox ID="ZIP_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak">
                                            业务联系人
                                        </td>
                                        <td>
                                            <asp:TextBox ID="LINKMAN" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak">
                                            用户名称
                                        </td>
                                        <td>
                                            <asp:TextBox ID="SUB_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tdBak" width="12%">
                                            手机
                                        </td>
                                        <td width="13%">
                                            <asp:TextBox ID="MOBILE_NO" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak" width="12%">
                                            E-Mail
                                        </td>
                                        <td width="13%">
                                            <asp:TextBox ID="EMAIL" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak" width="12%">
                                            销售员
                                        </td>
                                        <td width="13%">
                                            <asp:TextBox ID="SALE_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak" width="12%">
                                            座机
                                        </td>
                                        <td width="13%">
                                            <asp:TextBox ID="PHONE_NO" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;" class="tdHead" align="center" colspan="2">
                                光设备
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;" colspan="2">
                                <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                                    border="1" bordercolor="#5b9ed1">
                                    <tr>
                                        <td class="tdBak" align="center" width="12%">
                                            机房编号
                                        </td>
                                        <td width="13%">
                                            <asp:TextBox ID="VJF_CODE" runat="server" BorderWidth="0" Width="100%" BackColor="#F0F0F0"></asp:TextBox>
                                        </td>
                                        <td class="tdBak" align="center" width="12%">
                                            机房名称
                                        </td>
                                        <td width="13%">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="VJF" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','VJF','REGION','HOUSE_AREA','1')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="12%" align="center" class="tdBak">
                                            光设备编码
                                        </td>
                                        <td width="13%">
                                            <asp:TextBox ID="VJRSB_CODE" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                        </td>
                                        <td width="12%" align="center" class="tdBak">
                                            光设备名称
                                        </td>
                                        <td width="13%">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="VJRSB" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenEqu('JRSB_UNIT_ID','VJRSB','VJF','HOUSE_NAME','1')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdBak" align="center">
                                            &nbsp;光设备端口
                                        </td>
                                        <td colspan="3">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="VJRSBDK" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('bfc13d2d-eab8-4784-a96a-b8ffc21b4e88','VJRSBDK','VJRSB','EQU_NAME','0')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" class="tdBak">
                                            光缆客户端设置
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="VGLKHDSZ" runat="server" Width="100%" BorderWidth="0" Style="margin-bottom: 0px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdBak" align="center" colspan="2">
                                            传输设备修改记录
                                        </td>
                                        <td align="center" colspan="6">
                                            <asp:TextBox ID="VCSSBXGJL" runat="server" Rows="3" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;" class="tdHead" align="center" colspan="2">
                                ODF信息
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;" align="center" colspan="2">
                                <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                                    border="1" bordercolor="#5b9ed1">
                                    <tr>
                                        <td class="tdBak" style="width: 12%;" align="center">
                                            ODF架
                                        </td>
                                        <td style="width: 13%;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="V_ODF_J" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('2d738ce5-2200-48bc-9ce8-c767190a608d','V_ODF_J','REGION','HOUSE_AREA','0')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="tdBak" align="center" style="width: 12%;">
                                            ODF框
                                        </td>
                                        <td style="width: 13%;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="V_ODF_K" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('a0305fa0-eea8-4910-b018-bba0f5be86c7','V_ODF_K','V_ODF_J','ODF_NUM','0')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="tdBak" style="width: 12%;" align="center">
                                            ODF盘
                                        </td>
                                        <td style="width: 13%;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="V_ODF_P" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('bb1e11db-b1ec-4705-aecf-aa5018aa5fe6','V_ODF_P','V_ODF_K','ODF_NUM','0')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="tdBak" align="center" style="width: 12%;">
                                            法兰
                                        </td>
                                        <td style="width: 13%;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="V_ODF_FL" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('bfc13d2d-eab8-4784-a96a-b8ffc21b4e88','V_ODF_FL','V_ODF_P','EQU_NAME','0')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server" id="tr_gld">
                            <td colspan="8" class="tdBak">
                                <asp:Button ID="BtnAdd" runat="server" Text="新增光缆段" CssClass="btn_2k3" OnClientClick="windowOpen('','0','VJF')" />
                                <asp:Button ID="BtnDel" runat="server" Text="删除光缆段" CssClass="btn_2k3" OnClientClick="return confirm('确定要删除吗？')"
                                    OnClick="BtnDel_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <asp:GridView ID="gvVGLD" SkinID="GridView1" runat="server" AutoGenerateColumns="False"
                                    Width="100%" DataKeyNames="GUID,LIGHTGUID,LB,GXH_GUID" OnRowDataBound="gvVGLD_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="选择">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="10%" />
                                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="GLDXH" HeaderText="光缆段序号">
                                            <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%" />
                                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GLDMC_CODE" HeaderText="光缆段编号">
                                            <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%" />
                                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GLDMC" HeaderText="光缆段名称">
                                            <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="30%" />
                                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GXH" HeaderText="光缆段纤芯号">
                                            <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%" />
                                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                                    border="1" bordercolor="#5b9ed1">
                                    <tr>
                                        <td align="center" class="tdBak">
                                            整条链路
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="ZTLL" runat="server" Width="100%" BorderWidth="0" Rows="3"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="13%" align="center" class="tdBak">
                                            链路方向
                                        </td>
                                        <td width="20%">
                                            <asp:DropDownList ID="DropDownList1" runat="server" Width="100%">
                                                <asp:ListItem Value="1">甲端→乙端</asp:ListItem>
                                                <asp:ListItem Value="-1">甲端←乙端</asp:ListItem>
                                                <asp:ListItem Value="0">甲端～乙端</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="13%" align="center" class="tdBak">
                                            链路长度(KM)
                                        </td>
                                        <td width="20%">
                                            <asp:TextBox ID="LLCD" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                        <td width="13%" align="center" class="tdBak">
                                            全程损耗值(DB)
                                        </td>
                                        <td width="20%">
                                            <asp:TextBox ID="QCSHZ" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tdBak">
                                            备注
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="PZJL_BZ" runat="server" Width="100%" BorderWidth="0" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="V_ODF_J_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="V_ODF_K_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="V_ODF_P_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="V_ODF_FL_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="SLSB_UNIT_ID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="JRSB_UNIT_ID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="YWGUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="VJF_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="VSLSB_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="VSLSBDK_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="VJRSB_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="VJRSBDK_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:Button ID="Btn1" runat="server" Text="Button" OnClick="Btn1_Click" Style="display: none;" />
    <div id="EnumDataDiv" runat="server" style="z-index: 1; display: none; border: 1px solid #5b9ed1;
        position: absolute; width: 40%; height: 100%; top: 50px; left: 30%;">
        <iframe id="Iframe1" style="z-index: 1; visibility: inherit; width: 100%; height: 100%"
            runat="server" name="ProperyPage" frameborder="0" scrolling="no"></iframe>
    </div>
    <asp:TextBox ID="LLFX" runat="server" Style="display: none;">1</asp:TextBox>
    <asp:TextBox ID="CREATEDATETIME" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="UPDATEDATETIME" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="UPDATEUSERNAME" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="ZYHS_BJ" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="DDLID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="DDLLX" runat="server" Style="display: none"></asp:TextBox>
    <asp:Button ID="Btn" runat="server" Text="Button" OnClick="Btn_Click" Style="display: none;" />
    <asp:TextBox ID="REGION_CODE" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
