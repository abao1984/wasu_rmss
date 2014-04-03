<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigCupboardEdit.aspx.cs"
    Inherits="Web_Resource_ConfigCupboardEdit" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>机柜配置单</title>

    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../config.js"></script>

    <script language="javascript" type="text/javascript" src="ResourceScript.js"></script>

    <script type="text/javascript">
        //windowOpenPhyResourceSelect('79e2b404-66f0-4b58-949f-310de5dd496a', 'CUPBOARD_NAME', 'CUPBOARD_NAME_CODE', 'CUPBOARD_NAME_GUID', 'HOUSE_NAME', '0')
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
            var url = "PhyEquSelect.aspx?ISEQUCODE=" + isEqucode + "&UNIT_ID=" + unit_id + "&TXT_NAME=" + txt_name + "&TXT_CODE=" + txt_code + "&TXT_GUID=" + txt_guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name) + "&NAME_FILED=" + linkage_code + "&P_TXT_NAME=" + p_txt_name;
            windowOpenPage(url, "资源选择", "btn");
            window.event.returnValue = false;
        }

        function windowOpenPhyResourceSelect2(txt_name, p_txt_name, linkage_code, isEqucode) {

            var unit_id = document.getElementById("SB_UNIT_ID").value;
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
            var url = "PhyEquSelect.aspx?ISEQUCODE=" + isEqucode + "&UNIT_ID=" + unit_id + "&TXT_NAME=" + txt_name + "&TXT_CODE=" + txt_code + "&TXT_GUID=" + txt_guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name) + "&NAME_FILED=" + linkage_code + "&P_TXT_NAME=" + p_txt_name;
            windowOpenPage(url, "资源选择", "btn");
            window.event.returnValue = false;
        }

        function windowOpenPhyResourceSelect1(propery_id, name, code, guid, linkage_code, isEqucode) {
            var res_guid = "";
            var res_code = "";
            var res_name = "";
            if (linkage_code != "") {
                res_guid = document.getElementById(linkage_code + "_GUID").value;
                if (isEqucode == "1") {
                    res_code = document.getElementById(linkage_code + "_CODE").value;
                }
                res_name = document.getElementById(linkage_code).value;
            }
            var url = "PhyResourceSelect.aspx?ISEQUCODE=" + isEqucode + "&PROPERY_ID=" + propery_id + "&TXT_NAME=" + name + "&TXT_CODE=" + code + "&TXT_GUID=" + guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name) + "&NAME_FILED=" + linkage_code;
            windowOpenPage(url, "资源选择", "");
            window.event.returnValue = false;
        }


//        function windowOpenEqu(txt_fl, txt_name, p_txt_name, linkage_code) {
//            var unit_id = document.getElementById(txt_fl).value;
//            windowOpenPhyResourceSelect2(unit_id, txt_name, p_txt_name, linkage_code, '1')

//        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%;">
        <tr height="1px">
            <td align="center" class="tableHead">
                <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td width="1">
                            <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" OnClick="SaveButton_Click"
                                Text="保存" />
                        </td>
                        <td width="1">
                            &nbsp;</td>
                        <td align="right" width="100%">
                            <asp:Button ID="btn" runat="server" Style="display: none" OnClick="btn_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%" valign="top">
            <td>
                <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td class="tdBak" align="center" width="15%">
                            设备编号
                        </td>
                        <td width="18%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                         <asp:TextBox ID="GUID" runat="server" style="display:none"></asp:TextBox>
                                        <asp:TextBox ID="SBMC_GUID" runat="server" style="display: none"></asp:TextBox>
                                        <asp:TextBox ID="SBMC_CODE" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect2('SBMC','HOUSE_NAME','HOUSE_NAME','1')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdBak" align="center" width="15%">
                            机房名称
                        </td>
                        <td width="18%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="HOUSE_NAME" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img runat="server" id="imghouse" src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','HOUSE_NAME','','','1')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdBak" align="center" width="15%">
                            机柜编号
                        </td>
                        <td width="18%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="CUPBOARD_NAME" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img runat="server" id="imgcupboard" src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('ee01eccb-7c95-4b96-b122-ed9913026f24','CUPBOARD_NAME','HOUSE_NAME','HOUSE_NAME','0')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center" width="15%">
                            机柜占用
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="CUPBOARD_U" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img runat="server" id="imgcupboardu" src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect1('4f8ea023-636d-441c-bce5-d734b7b004cf','CUPBOARD_U','CUPBOARD_U_CODE','CUPBOARD_U_GUID','CUPBOARD_NAME','0')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdBak" align="center" width="15%">
                            启用日期
                        </td>
                        <td width="18%">
                            <asp:TextBox ID="QYRQ" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);" BackColor="#F0F0F0"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center" width="15%">
                            电源需求
                        </td>
                        <td width="18%">
                             <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="POWER_CODE" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img runat="server" id="imgpower" src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect1('af553cae-040d-4730-a6d1-e49aafdce8b6','POWER_CODE','POWER_CODE_CODE','POWER_CODE_GUID','CUPBOARD_NAME','0')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center" width="15%">
                            责任人
                        </td>
                        <td width="18%">
                            <asp:TextBox ID="ZRR" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                         <td class="tdBak" align="center" width="15%">
                            责任部门
                        </td>
                        <td width="18%">
                            <asp:TextBox ID="ZRBM" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center" width="15%">
                            资产编号
                        </td>
                        <td width="18%">
                            <asp:TextBox ID="ZCBH" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div style="display: none">
        <asp:TextBox ID="HOUSE_NAME_GUID" runat="server"></asp:TextBox>
        <asp:TextBox ID="HOUSE_NAME_CODE" runat="server"></asp:TextBox>
        
        <asp:TextBox ID="SB_UNIT_ID" runat="server"></asp:TextBox>
        <asp:TextBox ID="CUPBOARD_U_CODE" runat="server"></asp:TextBox>
        <asp:TextBox ID="CUPBOARD_U_GUID" runat="server"></asp:TextBox>
        <asp:TextBox ID="CUPBOARD_NAME_CODE" runat="server"></asp:TextBox>
        <asp:TextBox ID="CUPBOARD_NAME_GUID" runat="server"></asp:TextBox>
        <asp:TextBox ID="SBMC" runat="server"></asp:TextBox>
        <asp:TextBox ID="tbname" runat="server"></asp:TextBox>
    </div>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
