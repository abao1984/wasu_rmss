<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigTransmissionEdit1.aspx.cs"
    Inherits="Web_Resource_ConfigTransmissionEdit1" %>
<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
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
            var url = "PhyEquSelect.aspx?ISEQUCODE=" + isEqucode + "&UNIT_ID=" + unit_id + "&TXT_NAME=" + txt_name + "&TXT_CODE=" + txt_code + "&TXT_GUID=" + txt_guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name) + "&NAME_FILED=" + linkage_code;
            windowOpenPage(url, "资源选择", "");
        }
        function windowOpenEqu(txt_fl, txt_name, p_txt_name, linkage_code) {
            var unit_id = document.getElementById(txt_fl).value;
            windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, '1')
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="tableHead">
                <asp:Button ID="BtnSave" runat="server" Text="保存" CssClass="btn_2k3" OnClick="BtnSave_Click" />
            </td>
        </tr>
        <tr>
            <td class="tableTitle" align="center">
                传输业务管理
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <table style="border-collapse: collapse; width: 100%" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td colspan="1" class="tdBak" align="center">
                            链路方向</td>
                        <td>
                           
                            <asp:DropDownList ID="LLFX" runat="server" Width="100%">
                                <asp:ListItem Value="1">甲端→乙端</asp:ListItem>
                                <asp:ListItem Value="-1">甲端←乙端</asp:ListItem>
                                <asp:ListItem Value="0">甲端～乙端</asp:ListItem>
                            </asp:DropDownList>
                           
                        </td>
                        <td colspan="1" class="tdBak" align="center">
                            说明</td>
                        <td colspan="5">
                            <asp:TextBox ID="SM" runat="server" Width="100%" Rows="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tableTitle" align="center">
                            甲端
                        </td>
                        <td colspan="4" class="tableTitle" align="center">
                            乙端
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" style="width: 12%">
                            接入机房
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="JDJRJF" runat="server" Width="100%" BorderWidth="0" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','JDJRJF','','','1')"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            接入设备
                        </td>
                        <td style="width: 13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="JDJRSB" runat="server" Width="100%" BorderWidth="0" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenEqu('JRSB_UNIT_ID','JDJRSB','JDJRJF','HOUSE_NAME','1')"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            接入机房
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="YDJRJF" runat="server" Width="100%" BorderWidth="0" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','YDJRJF','','','1')"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            接入设备
                        </td>
                        <td style="width: 13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="YDJRSB" runat="server" Width="100%" BorderWidth="0" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenEqu('JRSB_UNIT_ID','YDJRSB','YDJRJF','HOUSE_NAME','1')"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" style="width: 12%">
                            设备端口号
                        </td>
                        <td width="38%" colspan="3">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="JDSBDK" runat="server" Width="100%" BorderWidth="0" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                      <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('bfc13d2d-eab8-4784-a96a-b8ffc21b4e88','JDSBDK','JDJRSB','EQU_NAME','0')"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            设备端口号
                        </td>
                        <td width="38%" colspan="3">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="YDSBDK" runat="server" Width="100%" BorderWidth="0" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('bfc13d2d-eab8-4784-a96a-b8ffc21b4e88','YDSBDK','YDJRSB','EQU_NAME','0')"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" style="width: 12%">
                            接入网络时隙
                        </td>
                        <td width="38%" colspan="3">
                            <asp:TextBox ID="JDJRWLSX" runat="server" Width="100%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            接入网络时隙
                        </td>
                        <td width="38%" colspan="3">
                            <asp:TextBox ID="YDJRWLSX" runat="server" Width="100%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="YWGUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="BH" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JDJRJF_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JDJRJF_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JDJRSB_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JDJRSB_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JDSBDK_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="YDJRJF_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="YDJRJF_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="YDJRSB_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="YDJRSB_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="YDSBDK_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JRSB_UNIT_ID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="ZYHS_BJ" runat="server" Style="display: none"></asp:TextBox>
     <uc1:windowHeader ID="windowHeader1" runat="server" />  
    </form>
</body>
</html>
