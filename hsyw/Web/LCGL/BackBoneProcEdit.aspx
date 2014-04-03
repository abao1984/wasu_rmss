<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackBoneProcEdit.aspx.cs"
    Inherits="Web_LCGL_BackBoneProcEdit" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../config.js"></script>

    <script language="javascript" type="text/javascript">
        function ShowUser() {
            var ret = window.showModalDialog("../SelectUsers.aspx?username=" + document.getElementById("SQR").value + "&IsDX=1", "", "dialogWidth:700px;dialogHeight:500px;center:yes;location:no;status:no;");
            if (typeof (ret) != "undefined") {
                document.getElementById("SQRNAME").value = ret[0];
                document.getElementById("SQR").value = ret[1];
            }
        }
        function windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, isEqucode) {
            var txt_code = txt_name + "_CODE";
            var txt_guid = txt_name + "_GUID";
            var res_guid = "";
            var res_code = "";
            var res_name = "";
            if (linkage_code != "") {
                res_guid = document.getElementById(p_txt_name + "_GUID").value;
                if (isEqucode == "1") {
                    res_code = document.getElementById(p_txt_name + "_CODE").value;
                }
                res_name = document.getElementById(p_txt_name).value;
            }
            var url = "../Resource/PhyEquSelect.aspx?ISEQUCODE=" + isEqucode + "&UNIT_ID=" + unit_id + "&TXT_NAME=" + txt_name + "&TXT_CODE=" + txt_code + "&TXT_GUID=" + txt_guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name) + "&NAME_FILED=" + linkage_code + "&P_TXT_NAME=" + p_txt_name; ;
            windowOpenPage(url, "资源选择", "");
        }
        function windowOpenEqu(txt_fl, txt_name, p_txt_name, linkage_code) {
            var unit_id = document.getElementById(txt_fl).value;
            windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, '1')
        }
        function MinWindow(str) {
            document.getElementById(str + "TR").style.display = "none";
            document.getElementById(str + "Div").style.height = "30px";
            document.getElementById(str + "Div").style.top = document.body.offsetHeight - 30;
        }
        function MaxWinodw(str) {
            document.getElementById(str + "TR").style.display = "block";
            document.getElementById(str + "Div").style.height = "100%";
            document.getElementById(str + "Div").style.top = "0px";
        }
        function WindowClose(str) {
            document.getElementById(str + "Div").style.display = "none";
            //document.getElementById("Btn").click();
        }
        function OpenSelect(enumtype, pname, ddlid) {
            var url = "../Resource/ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
            windowOpenPageByWidth(url, "枚举维护", "Btn", "30%", "40%", "10%", "80%");
            document.getElementById("DDLID").value = ddlid;
            document.getElementById("DDLLX").value = enumtype;
        }

        function OpneLogList() {
            var guid = document.getElementById("YWGUID").value;
            var url = "../LogList.aspx?PK_GUID=" + guid;
            windowOpenPage(url, "操作日志", "");
        }

        function windowOpenBoneTQ() {
            var url = "../Resource/BackBoneSelect.aspx?YWBM_NAME=YWBM&SUBSCRIBER_CODE=" + document.getElementById("YWBM").value;
            windowOpenPage(url, "选择骨干资源", "BtnRmss");
            //event.returnValue = false;
        }

        function windowOpenRmssSelect() {
            var url = "../Resource/RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID";
            windowOpenPage(url, "选择客户资源", "BtnRmss");
            event.returnValue = false;
        }

        function windowOpenBoneSelect() {
            var url = "../Resource/BackBoneSelect.aspx?YWBM_NAME=YWBM";
            windowOpenPage(url, "选择骨干资源", "BtnBone");
            event.returnValue = false;
        }

        function windowOpen(guid, lb, house) {
            //alert(house);
            var house_guid = document.getElementById(house + "_GUID").value;
            var house_name = document.getElementById(house).value;
            var url = "../Resource/GuangLanDuan.aspx?HOUSE_GUID=" + house_guid + "&HOUSE_NAME=" + encodeURI(house_name) + "&GUID=" + guid + "&LIGHTGUID=" + document.getElementById("GUID").value + "&LB=" + lb + "&YWBM=" + document.getElementById("SUBSCRIBER_CODE").value;
            windowOpenPage(url, "光缆段", "Btn");
            window.event.returnValue = false;
        }

        function windowOpenRmssTQ() {
            var url = "../Resource/RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID&SUBSCRIBER_CODE=" + document.getElementById("SUBSCRIBER_CODE").value;
            windowOpenPage(url, "选择客户资源", "BtnRmss");
            //event.returnValue = false;
        }

        function sendPageShow(lcbm, lczt, qfbj) {
            var guid = document.getElementById("GUID").value;
            var url = "SendPage.aspx?LCQFBJ=" + qfbj + "&LCBM=" + lcbm + "&LC_GUID=" + guid + "&LCZT=" + lczt;
            var str = window.showModalDialog(url, '', 'dialogHeight:300px; dialogWidth: 500px;center: yes; help: no;resizable: no; status:no;scroll:no;');
            if (str == "OK") {
                window.close();
                parent.document.getElementById("MenuButton").click();
            }
        }
    </script>

    <style type="text/css">
        .style1
        {
            background-image: url('../../App_Themes/Default/Images/TD_bg.jpg');
            background-repeat: repeat-x;
            color: #003797;
            font-family: "宋体";
            font-size: 16px;
            font-weight: bold;
            line-height: 26px;
            height: 19px;
        }
        .style2
        {
            background: #e6f3fc repeat-x;
            color: #003797;
            font-family: "宋体";
            font-size: 12px; /*font-weight: bold;*/;
            line-height: 26px;
            height: 29px;
        }
        .style3
        {
            height: 29px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%" border="0" cellpadding="1" cellspacing="0">
        <tr id="Tr_Button" runat="server" style="display: none;">
            <td align="center" class="tableHead" width="100%">
                <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" OnClick="SaveButton_Click"
                    Text="保存" />
                <asp:Button ID="SendButton" runat="server" CssClass="btn_2k3" OnClick="SendButton_Click"
                    Text="签发" />
                <asp:Button ID="BackButton" runat="server" CssClass="btn_2k3" OnClick="BackButton_Click"
                    Text="驳回" />
            </td>
        </tr>
        <tr>
            <td align="center" class="style1" width="100%">
                <asp:Label ID="HeadTitle" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="100%" align="center">
                <table bordercolor="#5b9ed1" cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                    width="100%">
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            申请单编号
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="SQDBH" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td style="width: 12%; color: Red" align="center" class="tdBak">
                            业务编码
                        </td>
                        <td style="width: 13%; background-color: White">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                         <asp:TextBox ID="SUBSCRIBER_GUID" runat="server" BorderWidth="0" Style="display: none"
                                            Width="100%"></asp:TextBox>
                                        <asp:TextBox ID="SUBSCRIBER_ID" runat="server" BorderWidth="0" Style="display: none"
                                            Width="100%"></asp:TextBox>
                                        <asp:TextBox ID="SUBSCRIBER_CODE" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="TQ" ImageUrl="../Images/Small/gif-0403.gif" runat="server" OnClick="TQ_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnRmss" runat="server" Text="Button" onclick="BtnRmss_Click" Style="display: none"/>
                                        <asp:ImageButton ID="SelectBOSS" runat="server" OnClientClick="windowOpenRmssSelect()"
                                            src="../Images/Small/bb_table.gif" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 12%;" align="center" class="tdBak">
                            工单来源
                        </td>
                        <td style="width: 13%;" align="center">
                            <asp:RadioButtonList ID="SUBSCRIBER_GDLY" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="SUBSCRIBER_GDLY_SelectedIndexChanged">
                                <asp:ListItem Selected="True">BOSS</asp:ListItem>
                                <asp:ListItem Value="SGD">手工单</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <%--<td class="tdBak" width="25%" align="center">
                            业务编码
                        </td>
                        <td width="25%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="SUBSCRIBER_ID" runat="server" BorderWidth="0" Style="display: none"
                                            Width="100%"></asp:TextBox>
                                        <asp:TextBox ID="SUBSCRIBER_GUID" runat="server" BorderWidth="0" Style="display: none"
                                            Width="100%"></asp:TextBox>
                                        <asp:TextBox ID="SUBSCRIBER_CODE" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="TQ" ImageUrl="../Images/Small/gif-0403.gif" runat="server" OnClick="TQ_Click" />
                                        <asp:Button ID="BtnRmss" runat="server" OnClick="BtnRmss_Click" Style="display: none" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="SelectBOSS" runat="server" OnClientClick="windowOpenRmssSelect()"
                                            src="../Images/Small/bb_table.gif" OnClick="SelectBOSS_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>--%>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%" width="100%" valign="top">
                <div style="overflow: auto; width: 100%; height: 100%">
                    <table width="100%" cellpadding="0" cellspacing="1" border="1">
                        <tr>
                            <td colspan="2" align="center" class="tdHead" width="100%">
                                用户信息
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" width="100%">
                              <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                                    border="1" bordercolor="#5b9ed1">
                                    <tr>
                                        <td align="center" class="style2">
                                            用户名称
                                        </td>
                                        <td class="style3">
                                            <asp:TextBox ID="SUB_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="style2">
                                            客户编码
                                        </td>
                                        <td class="style3">
                                            <asp:TextBox ID="CUSTOMER_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="style2">
                                            客户名称
                                        </td>
                                        <td colspan="3" class="style3">
                                            <asp:TextBox ID="CUSTOMER_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tdBak" style="width: 12%;">
                                            客户大类
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
                                                        <img src="../Images/Small/bb_table.gif" onclick="OpenBranch('REGION','REGION_CODE')" />
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
                                            客户等级
                                        </td>
                                        <td>
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
                            <td colspan="2" class="tdHead" align="center" width="100%">
                                骨干资源
                            </td>
                        </tr>
                        <tr>
                            <td height="1" colspan="2">
                                <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                                    border="1" bordercolor="#5b9ed1">
                                    <tr>
                                         <td align="center" class="tdBak" width="12%">
                                            骨干业务编码
                                        </td>
                                        <td width="13%" >
                                             <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                       <asp:TextBox ID="YWBM" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                         <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="windowOpenBoneSelect()"
                                            src="../Images/Small/bb_table.gif" />
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                        </td>
                                        <td align="center" class="tdBak" width="12%">
                                            业务类型
                                        </td>
                                        <td width="13%">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:DropDownList ID="YWLX" runat="server" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="OpenSelect('YWLX','','YWLX')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" class="tdBak" width="12%">
                                            链路名称
                                        </td>
                                        <td width="13%" >
                                            <asp:TextBox ID="LLMC" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak" width="12%">
                                            完整纤号
                                        </td>
                                        <td width="13%">
                                            <asp:TextBox ID="WZXH" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tdBak" width="12%">
                                            衰减
                                        </td>
                                        <td width="13%">
                                            <asp:TextBox ID="SJ" runat="server" Width="100%" BorderWidth="0" onKeyPress="return limitNum(this);"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak" width="12%">
                                            光缆长度（KM）
                                        </td>
                                        <td width="13%">
                                            <asp:TextBox ID="GLCD" runat="server" Width="100%" BorderWidth="0" onKeyPress="return limitNum(this);"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak" width="12%">
                                            申请时间
                                        </td>
                                        <td width="13%">
                                            <asp:TextBox ID="SQSJ" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak" width="12%">
                                            申请人
                                        </td>
                                        <td width="13%">
                                            <asp:TextBox ID="SQRNAME" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tdBak" width="12%">
                                            完工时间
                                        </td>
                                        <td width="13%">
                                            <asp:TextBox ID="WGSJ" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak" width="12%">
                                            启动时间
                                        </td>
                                        <td width="13%">
                                            <asp:TextBox ID="QDSJ" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak">
                                            涉及客户
                                        </td>
                                        <td align="center" colspan="3">
                                            <asp:TextBox ID="SJKH" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tdBak" width="12%">
                                            链路方向
                                        </td>
                                        <td align="center" width="13%">
                                            <asp:DropDownList ID="LLFX" runat="server" Width="100%">
                                                <asp:ListItem Value="1">甲端→乙端</asp:ListItem>
                                                <asp:ListItem Value="-1">甲端←乙端</asp:ListItem>
                                                <asp:ListItem Value="0">甲端～乙端</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" class="tdBak" width="12%">
                                            光缆资源备注
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="GLZYBZ" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="tdHead" align="center">
                                            甲端
                                        </td>
                                        <td colspan="4" class="tdHead" align="center">
                                            乙端
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tdBak">
                                            甲端机房编码
                                        </td>
                                        <td>
                                            <asp:TextBox ID="JDJF_CODE" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak">
                                            甲端机房名称
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="JDJF" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','JDJF','','','1')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" class="tdBak">
                                            乙端机房编码
                                        </td>
                                        <td>
                                            <asp:TextBox ID="YDJF_CODE" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak">
                                            乙端机房名称
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="YDJF" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','YDJF','','','1')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tdBak">
                                            甲端设备类型
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="JDSBLX" runat="server" Width="100%">
                                                <asp:ListItem Value="64602091-d4fe-4c89-ac6a-52f6acdd836d">网络设备</asp:ListItem>
                                                <asp:ListItem Value="9e2393f1-931d-4b14-b44f-0ba9ff846853">传输设备</asp:ListItem>
                                                <asp:ListItem Value="41d1081d-7925-485b-996c-72f4519c7898">楼宇设备</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" class="tdBak">
                                            甲端尾纤种类
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:DropDownList ID="JDWXZL" runat="server" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="OpenSelect('WXZL','','JDWXZL')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" class="tdBak">
                                            乙端设备类型
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="YDSBLX" runat="server" Width="100%">
                                                <asp:ListItem Value="64602091-d4fe-4c89-ac6a-52f6acdd836d">网络设备</asp:ListItem>
                                                <asp:ListItem Value="9e2393f1-931d-4b14-b44f-0ba9ff846853">传输设备</asp:ListItem>
                                                <asp:ListItem Value="41d1081d-7925-485b-996c-72f4519c7898">楼宇设备</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" class="tdBak">
                                            乙端尾纤种类
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:DropDownList ID="YDWXZL" runat="server" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="OpenSelect('WXZL','','YDWXZL')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tdBak">
                                            甲端设备编号
                                        </td>
                                        <td>
                                            <asp:TextBox ID="JDSB_CODE" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak">
                                            甲端设备名称
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="JDSB" runat="server" BorderWidth="0" Width="100%" BackColor="#F0F0F0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenEqu('JDSBLX','JDSB','JDJF','HOUSE_NAME','1')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" class="tdBak">
                                            乙端设备编号
                                        </td>
                                        <td>
                                            <asp:TextBox ID="YDSB_CODE" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak">
                                            乙端设备名称
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="YDSB" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenEqu('YDSBLX','YDSB','YDJF','HOUSE_NAME','1')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tdBak">
                                            甲端设备端口
                                        </td>
                                        <td colspan="3">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="JDSBDK" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('bfc13d2d-eab8-4784-a96a-b8ffc21b4e88','JDSBDK','JDSB','EQU_NAME','0')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="center" class="tdBak">
                                            乙端设备端口
                                        </td>
                                        <td colspan="3">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="YDSBDK" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('bfc13d2d-eab8-4784-a96a-b8ffc21b4e88','YDSBDK','YDSB','EQU_NAME','0')" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="tdBak">
                                            甲端端口备注
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="JDDKBZ" runat="server" Width="100%" BorderWidth="0" Height="50px"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td align="center" class="tdBak">
                                            乙端端口备注
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="YDDKBZ" runat="server" Width="100%" BorderWidth="0" Height="50px"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdHead" width="50%" align="center">
                                甲端光缆段
                            </td>
                            <td class="tdHead" width="50%" align="center">
                                乙端光缆段
                            </td>
                        </tr>
                        <tr>
                            <td class="tableTitle">
                                <asp:Button ID="BtnAddJDGLD" runat="server" Text="新增光缆段" CssClass="btn_2k3" OnClientClick="windowOpen('','1','JDJF')" />
                                <asp:Button ID="BtnDelJDGLD" runat="server" Text="删除光缆段" CssClass="btn_2k3" OnClientClick="return confirm('确定要删除吗？')"
                                    OnClick="BtnDelJDGLD_Click" />
                            </td>
                            <td class="tableTitle">
                                <asp:Button ID="BtnAddYDGLD" runat="server" Text="新增光缆段" CssClass="btn_2k3" OnClientClick="windowOpen('','2','YDJF')"
                                    />
                                <asp:Button ID="BtnDelYDGLD" runat="server" Text="删除光缆段" CssClass="btn_2k3" OnClientClick="return confirm('确定要删除吗？')"
                                    OnClick="BtnDelYDGLD_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:GridView ID="gvJDGLD" SkinID="GridView1" runat="server" AutoGenerateColumns="False"
                                    Width="100%" DataKeyNames="GUID,LIGHTGUID,LB,GXH_GUID" OnRowDataBound="gvJDGLD_RowDataBound">
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
                            <td valign="top">
                                <asp:GridView ID="gvYDGLD" SkinID="GridView1" runat="server" AutoGenerateColumns="False"
                                    Width="100%" DataKeyNames="GUID,LIGHTGUID,LB,GXH_GUID" OnRowDataBound="gvYDGLD_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="选择">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox2" runat="server" />
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
    <asp:TextBox ID="GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="YWGUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JDBM" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JDJF_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="SQR" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="YDJF_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="JDSB_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="YDSB_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="JDSBDK_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="YDSBDK_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="ZYHS_BJ" runat="server" Text="1" Style="display: none;"></asp:TextBox>
    <asp:Button ID="Btn" runat="server" Text="Button" OnClick="Btn_Click" Style="display: none" />
     <asp:Button ID="BtnBone" runat="server" Text="Button"   Style="display: none"
        onclick="BtnBone_Click" />
    <asp:TextBox ID="CREATEDATETIME" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="UPDATEDATETIME" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="UPDATEUSERNAME" runat="server" Width="100%" BorderWidth="0" Style="display: none;"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
