<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogicResourceVpnEdit.aspx.cs"
    Inherits="Web_Resource_LogicResourceVpnEdit" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../config.js"></script>
    <script type="text/javascript">
        function windowOpenBranchTree(name, code) {
            var url = "BranchTree.aspx?ISQY=1&NAME=" + name + "&CODE=" + code;
            windowOpenPageByWidth(url, "组织机构", "", "30%", "40%", "10%", "80%");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td class="tableHead">
                <asp:Button ID="SaveButton" runat="server" Text="保存" CssClass="btn_2k3" OnClick="SaveButton_Click" />
                <asp:Button ID="DelButton" runat="server" Text="删除" CssClass="btn_2k3" 
                    OnClick="DelButton_Click" onclientclick="return confirm('确定删除');" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td class="tdBak" width="10%" align="center">
                            VPN名称
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="VPNMC" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td class="tdBak" width="10%" align="center">
                            客户名称
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="KHMC" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td class="tdBak" width="10%" align="center">
                            所属区域
                        </td>
                        <td width="15%">
                            <table border="0" width="99%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td bgcolor="WhiteSmoke" width="100%">
                                        <asp:TextBox ID="SSQY" runat="server" Width="99%" BorderStyle="None" Style="background-color: WhiteSmoke"></asp:TextBox>
                                        <asp:TextBox ID="SSQY_CODE" runat="server" Style="display: none"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img align='right' src='../Images/Small/bb_table.gif' onclick="windowOpenBranchTree('SSQY','SSQY_CODE')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdBak" width="10%" align="center">
                            RD值
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="RDZ" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" width="10%" align="center">
                            配置人
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="PZR" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td class="tdBak" width="10%" align="center">
                            配置时间
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="PZSJ" runat="server" Width="100%" BorderStyle="None" onfocus="setDay(this);"></asp:TextBox>
                        </td>
                        <td class="tdBak" width="10%" align="center">
                            修改人
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="UPDATEUSERNAME" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td class="tdBak" width="10%" align="center">
                            修改时间
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="UPDATEDATETIME" runat="server" Width="100%" BorderStyle="None" onfocus="setDay(this);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" width="10%" align="center">
                            说明
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="SM" runat="server" Rows="2" TextMode="MultiLine" BorderStyle="None"
                                Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="CREATEDATETIME" runat="server" Width="100%" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="GUID" runat="server" Width="100%" Style="display: none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
