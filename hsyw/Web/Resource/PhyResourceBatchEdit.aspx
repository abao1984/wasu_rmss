<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhyResourceBatchEdit.aspx.cs" Inherits="Web_Resource_PhyResourceBatchEdit" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="../../config.js"></script>
    <script>
        function PhyChange(isTotal,code) {
            var startObject = document.getElementById("PHY_START_" + code);
            var endObject = document.getElementById("PHY_END_" + code);
            var totalObject = document.getElementById("PHY_TOTAL_" + code);
            if (isTotal && startObject.value != "") {
                endObject.value = startObject.value -1 -(-totalObject.value);         
            }
            else if (endObject.value == "" || startObject.value - 1 > endObject.value - 1) {
               endObject.value = startObject.value;                   
            }
            if (startObject.value != "") {
                totalObject.value = endObject.value - startObject.value + 1;
            }
        }
        function LoadEnumData(drpName, EnumSort, LinkCode) {
            var drp = document.getElementById(drpName);
            var oldValue = drp.value;
            var code = document.getElementById(LinkCode).value;
            while (drp.options.length = 0) {
                drp.remove(0);
            }
            ds = ShareResource.getEnumData(EnumSort, code).value;
            for (i = 0; i < ds.Tables[0].Rows.length; i++) {
                var newOption = document.createElement("OPTION");
                newOption.text = ds.Tables[0].Rows[i].ENUM_NAME;
                newOption.value = ds.Tables[0].Rows[i].ENUM_NAME;
                drp.options.add(newOption);
            }
            drp.value = oldValue;
        }
        function changeEnumData(drpName, EnumSort, LinkCode) {
            var enumName = document.getElementById(drpName).value;
            document.getElementById(drpName + "_SHORT").value = ShareResource.GetEnumDataShort(EnumSort, enumName).value;
        }
        function windowOpenBranchTree(name, code) {
            var url = "BranchTree.aspx?ISQY=1&NAME=" + name + "&CODE=" + code;
            windowOpenPageByWidth(url, "组织机构", "", "30%", "40%", "10%", "80%");
        }
        
        function windowOpenPhyResourceSelect(propery_id, name, code, guid, linkage_code, isEqucode) {
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
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
          <table style="width:100%;">
         <tr>
                <td align="center" class="tableHead">
                    <asp:Button ID="SaveButton" runat="server" onclick="SaveButton_Click" 
                        Text="保存" CssClass="btn_2k3" />
                    <asp:Button ID="DeleteButton" runat="server" onclick="DeleteButton_Click" 
                        Text="删除" onclientclick="return confirm(&quot;确定要删除吗？&quot;);" 
                        CssClass="btn_2k3" />
                   </td>
            </tr>
            <tr>
                <td align="center" class="tdBak">
                <asp:TextBox ID="PARENT_UNIT_ID" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                 <asp:TextBox ID="UNIT_ID" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                 <asp:TextBox ID="CODE_MODE" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                  <asp:TextBox ID="P_CODE_FILED" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                  <asp:TextBox ID="CODE_FILED" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                   <asp:TextBox ID="NAME_FILED" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                    <asp:TextBox ID="TABLE_NAME" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                   <asp:TextBox ID="NAME_FILED_GUID" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                   <asp:TextBox ID="NAME_FILED_NAME" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                    <table border="1" style="width: 500px;" BorderColor = "#5b9ed1">
                        <tr id="PHY_TR_NUM" runat="server">
                            <td class="tdBak" width="17%" align="center">
                                开始数字编码</td>
                            <td width="17%">
                                <asp:TextBox ID="PHY_START_NUM" runat="server" BorderStyle="None" BorderWidth="0px" 
                                    Width="99%" onKeyPress="return limitNum(this);">1</asp:TextBox>
                            </td>
                            <td class="tdBak" width="17%" align="center">
                                结束数字编码</td>
                            <td width="17%">
                                <asp:TextBox ID="PHY_END_NUM" runat="server" BorderStyle="None" BorderWidth="0px" 
                                    Width="99%" onKeyPress="return limitNum(this);">1</asp:TextBox>
                            </td>
                            <td width="15%" class="tdBak" align="center">
                                总数</td>
                            <td width="17%">
                                <asp:TextBox ID="PHY_TOTAL_NUM" runat="server" BorderStyle="None" BorderWidth="0px" 
                                    Width="99%" onKeyPress="return limitNum(this);">1</asp:TextBox>
                            </td>
                        </tr>
                        <tr  id="PHY_TR_CODE" runat="server">
                            <td class="tdBak" width="17%" align="center">
                                开始字母编码</td>
                            <td width="17%">
                                <asp:DropDownList ID="PHY_START_CODE" runat="server" Width="99%">
                                </asp:DropDownList>
                            </td>
                            <td class="tdBak" width="17%" align="center">
                                结束字母编码</td>
                            <td width="17%">
                                <asp:DropDownList ID="PHY_END_CODE" runat="server" Width="99%">
                                </asp:DropDownList>
                            </td>
                            <td width="15%" class="tdBak" align="center">
                                总数</td>
                            <td width="17%">
                                <asp:TextBox ID="PHY_TOTAL_CODE" runat="server" BorderStyle="None" BorderWidth="0px" 
                                    Width="99%" onKeyPress="return limitNum(this);"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td id="TD_PROPERTY" runat="server" valign="top"></td>
            </tr>
        </table>
          <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
