﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuangLanDuanEdit.aspx.cs" Inherits="Web_Resource_GuangLanDuanEdit" %>

<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="../../config.js"></script>
      <script language="javascript" type="text/javascript">
          function windowOpenPhyResourceSelect(unit_id,code,txt_name,isEqucode) {
              var txt_code = code + "CODE";
              var txt_guid = code + "GUID";
            var res_guid = "";
            var res_code = "";
            var res_name = "";
            var res_guid = "";
            var res_name = "";
            var linkage_code = "";
            var p_txt_name = "";
            var url = "PhyEquSelect.aspx?ISEQUCODE=" + isEqucode + "&UNIT_ID=" + unit_id + "&TXT_NAME=" + txt_name + "&TXT_CODE=" + txt_code + "&TXT_GUID=" + txt_guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name) + "&NAME_FILED=" + linkage_code + "&P_TXT_NAME=" + p_txt_name; 
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
    <table style="width: 100%">
        <tr>
            <td class="tableHead">
                <asp:Button ID="BtnSave" runat="server" Text="保存" CssClass="btn_2k3" OnClick="BtnSave_Click" />
            </td>
        </tr>
        <tr>
            <td height="1">
                <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td align="center" class="tdBak" style="width:20%">
                            光缆段序号
                        </td>
                        <td style="width:30%">
                            <asp:TextBox ID="GLDXH" runat="server" Width="100%" BorderWidth="0px"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" style="width:20%;">
                            光缆长度(KM)                         </td>
                        <td style="width:30%">
                            <asp:TextBox ID="GLDCD" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" style="width:20%">
                            光光缆段编码</td>
                        <td style="width:30%">
                                        <asp:TextBox ID="GLDCODE" runat="server" BorderWidth="0" 
                                Width="100%" BackColor="#F0F0F0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" style="width:20%;">
                            光缆段名称</td>
                        <td style="width:30%">
                           <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                            <asp:TextBox ID="GLDMC" runat="server" Width="100%" BorderWidth="0px" BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif"  onclick="windowOpenPhyResourceSelect('53d347a2-5be1-4b85-af53-ffd35b3ccfc7','GLD','GLDMC','1')"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" style="width:20%">
                            纤芯号</td>
                        <td style="width:30%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="GXHCODE" runat="server" BorderWidth="0" Width="100%" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif"  onclick="windowOpenPhyResourceSelect('3b5bfe00-e5dd-4f02-bf41-b347ca9c7624','GXH','GXHCODE','0')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" style="width:50%" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>   
    <asp:TextBox ID="LSID" runat="server" style="display:none"></asp:TextBox>
    <asp:TextBox ID="GUID" runat="server" style="display:none"></asp:TextBox>
    <asp:TextBox ID="GLDGUID" runat="server" style="display:none"></asp:TextBox>
    <asp:TextBox ID="GXHGUID" runat="server" style="display:none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>

