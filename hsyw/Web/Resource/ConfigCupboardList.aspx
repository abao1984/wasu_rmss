<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigCupboardList.aspx.cs"
    Inherits="Web_Resource_ConfigCupboardList" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<%@ Register Assembly="IDP.WebControls" Namespace="IDP.WebControls" TagPrefix="idp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>机柜配置单</title>

    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript">
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

        function windowOpen(guid) {
            windowOpenPage("ConfigCupboardEdit.aspx?GUID=" + guid, "机柜配置单", "Btn");
            window.event.returnValue = false;
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
            </td>
        </tr>
        <tr>
            <td height="1">
                <table style="width: 100%; border-collapse: collapse;" cellpadding="0" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
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
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','JFMC','','','1')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdBak" align="center" width="12%">
                            设备编号
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="SBMC_CODE" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenEqu('SB_UNIT_ID','SBMC','JFMC','HOUSE_NAME','1')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            启用日期
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="QYSJ1" runat="server" Width="100%" BackColor="#F0F0F0" BorderWidth="0"
                                onfocus="setDay(this);"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            至
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="QYSJ2" runat="server" Width="100%" BorderWidth="0" BackColor="#F0F0F0"
                                onfocus="setDay(this);"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%" valign="top">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="gvCupboard" runat="server" SkinID="GridView1" DataKeyNames="GUID"
                        Width="100%" BorderColor="#5B9ED1" BorderWidth="1px" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" OnRowDataBound="gvCupboard_RowDataBound" OnSorting="gvCupboard_Sorting">
                        <PagerSettings PageButtonCount="100" />
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="5%" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="HOUSE_NAME" HeaderText="机房名称">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="14%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CUPBOARD_NAME" HeaderText="机柜编号" >
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="14%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CUPBOARD_U" HeaderText="机柜占用U" >
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px"></ItemStyle>
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="POWER_CODE" HeaderText="电源" >
                               <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px"></ItemStyle>
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SBMC_CODE" HeaderText="设备编号" >
                               <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px"></ItemStyle>
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ZRRBM" HeaderText="责任人/责任部门" >
                               <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px"></ItemStyle>
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="QYRQ" HeaderText="启用日期"  DataFormatString="{0:yyyy-MM-dd}">
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px"></ItemStyle>
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ZCBH" HeaderText="资产编号" >
                               <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px"></ItemStyle>
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" />
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
            </td>
        </tr>
    </table>
    <div style="display: none">
        <asp:TextBox ID="JFMC_GUID" runat="server"></asp:TextBox>
        <asp:TextBox ID="JFMC_CODE" runat="server"></asp:TextBox>
        <asp:TextBox ID="SBMC_GUID" runat="server"></asp:TextBox>
        <asp:TextBox ID="SBMC" runat="server"></asp:TextBox>
        <asp:TextBox ID="SB_UNIT_ID" runat="server"></asp:TextBox>
        <asp:Button ID="Btn" runat="server" Text="Button" OnClick="Btn_Click" />
    </div>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
