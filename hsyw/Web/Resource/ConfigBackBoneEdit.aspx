<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigBackBoneEdit.aspx.cs"
    Inherits="Web_Resource_ConfigBackBoneEdit" %>

<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>

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
            var url = "PhyEquSelect.aspx?ISEQUCODE=" + isEqucode + "&UNIT_ID=" + unit_id + "&TXT_NAME=" + txt_name + "&TXT_CODE=" + txt_code + "&TXT_GUID=" + txt_guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name) + "&NAME_FILED=" + linkage_code + "&P_TXT_NAME=" + p_txt_name; ;
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
            var url = "ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
            windowOpenPageByWidth(url, "枚举维护", "Btn", "30%", "40%", "10%", "80%");
            document.getElementById("DDLID").value = ddlid;
            document.getElementById("DDLLX").value = enumtype;
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
    <table style="width: 100%;">
        <tr>
            <td class="tableHead">
             <table style="width:100%;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td  width="1" align="right">
                <asp:Button ID="BtnSave" runat="server" Text="保存" CssClass="btn_2k3" OnClick="BtnSave_Click" />
                </td>
                <td  width="1" >
                             <asp:Button ID="BtnZyhs" runat="server" Text="资源回收" CssClass="btn_2k3" 
                    OnClick="BtnZyhs_Click" 
                    onclientclick="return confirm(&quot;确定操作吗？&quot;);" />
                    </td>
                    <td width="1">
                        <asp:Button ID="BtnExp" runat="server" Text="导出Excel" CssClass="btn_2k3" 
                            onclick="BtnExp_Click" />
                        </td>
                    <td align="right">
                     <a href="#" onclick="OpneLogList()" >操作日志</a></td></tr></table>
            </td>
        </tr>
        <tr>
            <td height="1">
                <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td align="center" class="tdBak" width="12%" style="color: Red">
                            业务编码
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="YWBM" runat="server" Width="100%" BorderWidth="0px" 
                                BackColor="#F0F0F0"></asp:TextBox>
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
                        <td width="13%">
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
                            <asp:TextBox ID="SJ" runat="server" Width="100%" BorderWidth="0" onKeyPress="return limitNum(this);" ></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            光缆长度（KM）
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="GLCD" runat="server" Width="100%"  BorderWidth="0" onKeyPress="return limitNum(this);" ></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            申请时间
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="SQSJ" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);" ></asp:TextBox>
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
                            <asp:TextBox ID="JDJRJF_CODE" runat="server" Width="100%" BorderWidth="0" 
                                BackColor="#F0F0F0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak">
                            甲端机房名称
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="JDJRJF" runat="server" Width="100%" BorderWidth="0" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','JDJRJF','','','1')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak">
                            乙端机房编码
                        </td>
                        <td>
                            <asp:TextBox ID="YDJRJF_CODE" runat="server" Width="100%" BorderWidth="0" 
                                BackColor="#F0F0F0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak">
                            乙端机房名称
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="YDJRJF" runat="server" Width="100%" BorderWidth="0" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','YDJRJF','','','1')" />
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
                            <asp:TextBox ID="JDSB_CODE" runat="server" Width="100%" BorderWidth="0" 
                                BackColor="#F0F0F0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak">
                            甲端设备名称
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="JDSB" runat="server" BorderWidth="0" Width="100%" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenEqu('JDSBLX','JDSB','JDJRJF','HOUSE_NAME','1')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak">
                            乙端设备编号
                        </td>
                        <td>
                            <asp:TextBox ID="YDSB_CODE" runat="server" Width="100%" BorderWidth="0" 
                                BackColor="#F0F0F0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak">
                            乙端设备名称
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="YDSB" runat="server" Width="100%" BorderWidth="0" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenEqu('YDSBLX','YDSB','YDJRJF','HOUSE_NAME','1')" />
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
                                        <asp:TextBox ID="JDSBDK" runat="server" Width="100%" BorderWidth="0" 
                                            BackColor="#F0F0F0"></asp:TextBox>
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
                                        <asp:TextBox ID="YDSBDK" runat="server" Width="100%" BorderWidth="0" 
                                            BackColor="#F0F0F0"></asp:TextBox>
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
    </table>
    <asp:TextBox ID="JDJRJF_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="YWGUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="SQR" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="YDJRJF_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="JDSB_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="YDSB_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="JDSBDK_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="YDSBDK_GUID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:Button ID="Btn" runat="server" Text="Button" OnClick="Btn_Click" Style="display: none" />
    <asp:TextBox ID="DDLID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="DDLLX" runat="server" Style="display: none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    <asp:TextBox ID="ZYHS_BJ" runat="server" Style="display: none"></asp:TextBox>
     <asp:TextBox ID="CREATEDATETIME" runat="server" Width="100%" BorderWidth="0" style="display:none;"></asp:TextBox>
       <asp:TextBox ID="UPDATEDATETIME" runat="server" Width="100%" BorderWidth="0" style="display:none;"></asp:TextBox>
        <asp:TextBox ID="UPDATEUSERNAME" runat="server" Width="100%" BorderWidth="0" style="display:none;"></asp:TextBox>
    </form>
</body>
</html>
