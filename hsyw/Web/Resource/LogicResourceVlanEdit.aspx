<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogicResourceVlanEdit.aspx.cs"
    Inherits="Web_Resource_LogicResourceVlanEdit" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<script language="javascript" type="text/javascript" src="ResourceScript.js"></script>
 <script language="javascript" type="text/javascript" src="../../config.js"></script>
    <script language="javascript" type="text/javascript">
//        function windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, isEqucode, branchName) {
//            var txt_code = txt_name + "_CODE";
//            var txt_guid = txt_name + "_GUID";
//            var res_guid = "";
//            var res_code = "";
//            var res_name = "";
//            var branch = "";
//            var branch_code = "";
//            if (linkage_code != "") {
//                res_guid = document.getElementById(p_txt_name + "_GUID").value;
//                if (isEqucode == "1") {
//                    res_code = document.getElementById(p_txt_name + "_CODE").value;
//                }
//                res_name = document.getElementById(p_txt_name).value;
//            }
//            if (branchName != "" && typeof (branchName) != "undefined") {
//                branch = document.getElementById(branchName).value;
//                branch_code = document.getElementById(branchName + "_CODE").value;
//            }
//            var url = "PhyEquSelect.aspx?ISEQUCODE=" + isEqucode + "&UNIT_ID=" + unit_id + "&TXT_NAME=" + txt_name + "&TXT_CODE=" + txt_code + "&TXT_GUID=" + txt_guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name)
//            + "&NAME_FILED=" + linkage_code + "&P_TXT_NAME=" + p_txt_name + "&BRANCH=" + encodeURI(branch) + "&BRANCH_CODE=" + branch_code;
//            windowOpenPage(url, "资源选择", "");
        //        }

        function OpenSelect(enumtype, pname, ddlid) {
            var url = "ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
            windowOpenPageByWidth(url, "枚举维护", ddlid, "30%", "40%", "10%", "80%");            
        }
        function windowOpenEqu(txt_fl, txt_name, p_txt_name, linkage_code) {
            var unit_id = document.getElementById(txt_fl).value;
            windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, '1')
        }
        function OpenSelectSXQY(name, code) {
            windowOpenPageByWidth("BranchTree.aspx?ISQY=1&NAME=" + name + "&CODE=" + code, "选择所属区域", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }
        function SFZS(obj) {
            if (obj.value.search("^-?\\d+$") != 0) {
                obj.value = "";
                obj.focus();
            }
            else { 
                var ksbh = document.getElementById("KSBH").value;
                var jsbh = document.getElementById("JSBH").value;
                if (ksbh != "" && jsbh != "" && jsbh < ksbh) {
                    document.getElementById("JSBH").value = ksbh;
                }
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="tableHead">
                <asp:Button ID="BtnSave" runat="server" Text="保存" OnClick="BtnSave_Click" CssClass="btn_2k3" />
                &nbsp; <asp:Button ID="BtnYwdl" runat="server" onclick="BtnYwdl_Click" style="display:none;" />
                <asp:Button ID="BtnDel" runat="server" Text="删除" OnClick="BtnDel_Click" CssClass="btn_2k3" OnClientClick="return confirm('确定要删除吗？')" />
            </td>
        </tr>
        <tr id="tr_plcz" runat="server">
            <td class="tdBak" style="height: 29px" valign="top" align="center">
                <table border="1" bordercolor="#5b9ed1" style="height: 27px">
                    <tr>
                        <td style="width: 110px" align="center">
                            VLAN开始编号
                        </td>
                        <td>
                            <asp:TextBox ID="KSBH" runat="server" Width="100px" BorderWidth="0" onblur="SFZS(this);"></asp:TextBox>
                        </td>
                        <td style="width: 110px">
                            VLAN结束编号
                        </td>
                        <td>
                            <asp:TextBox ID="JSBH" runat="server" Width="100px" BorderWidth="0" onblur="SFZS(this);"></asp:TextBox>
                        </td>
                       
                    </tr>
                </table>
            </td>
        </tr>
        <tr >
            <td class="tdBak">
                <table style="border-collapse: collapse; width: 100%" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                      <tr id="tr_vlan" runat="server">
                        <td class="tdBak"  align="center"  style="width: 10%">
                                VLAN编号</td>
                        <td colspan="1" align="center"  style="width: 15%">
                            <asp:TextBox ID="VLANBH" runat="server" Width="100%" BorderWidth="0" 
                               onKeyPress="return limitNum(this);"></asp:TextBox>
                                    </td>
                        <td class="tdBak"   align="center"  style="width: 10%">
                             VLAN分类</td>
                         <td   align="left"  style="width: 15%">                          
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:DropDownList ID="YWDL" runat="server" Width="100%" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenSelect('V_YWDL','','BtnYwdl')" />
                                    </td>
                                </tr>
                            </table>
                                        </td>
                    </tr>
                      <%--<tr>
                        <td class="tdBak"  align="center"  style="width: 10%">
                              业务大类</td>
                        <td colspan="1" align="center"  style="width: 15%">
                           -- <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:DropDownList ID="YWDL" runat="server" Width="100%" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenSelect('YWDL','','BtnYwdl')" />
                                    </td>
                                </tr>
                            </table>
                                    </td>
                        <td class="tdBak"   align="center"  style="width: 10%">
                             &nbsp;</td>
                         <td   align="left"  style="width: 15%">                          
                             &nbsp;</td>
                    </tr>--%>
                    <tr>
                     <td align="center" class="tdBak" style="width: 20%">
                            所属区域
                        </td>
                        <td style="width: 30%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="SSQY" runat="server" Width="100%" BorderWidth="0" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenSelectSXQY('SSQY','SSQY_CODE')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" style="width: 20%">
                            所属机房
                        </td>
                        <td width="30%">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="SSJF" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" runat="server" id="img" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','SSJF','SSQY','HOUSE_AREA','1','')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                       
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="20%">
                            是否可复用
                        </td>
                        <td width="30%">
                            <asp:DropDownList ID="SFKFY" runat="server" Width="100%">
                                <asp:ListItem Value="Y">是</asp:ListItem>
                                <asp:ListItem Value="N" Selected="True">否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="center" class="tdBak" width="20%">
                           <%-- VLAN占用状态--%>
                             是否全网</td>
                        <td width="30%">
                           <%-- <asp:DropDownList ID="ZYZT" runat="server" Width="100%">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>占用</asp:ListItem>
                                <asp:ListItem>空闲</asp:ListItem>
                            </asp:DropDownList>--%>
                            <asp:CheckBox ID="SFQW" runat="server" AutoPostBack="True" 
                                oncheckedchanged="SFQW_CheckedChanged" />
                        </td>
                    </tr>
                  
                </table>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="GUID" runat="server" Style="display: none"></asp:TextBox> 
    <asp:TextBox ID="SSJF_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="SSJF_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="SSQY_CODE" runat="server" Style="display: none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
   
    </form>
</body>
</html>
