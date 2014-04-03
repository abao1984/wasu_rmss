<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackBoneProcList.aspx.cs"
    Inherits="Web_LCGL_BackBoneProcList" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
       <script src="../../calendar.js" language="javascript" type="text/javascript"></script>
    <script  language="javascript" type="text/javascript">
        function changeMenu(jdbm) {
            document.getElementById("JDBM").value = jdbm;
            document.getElementById("MenuButton").click();
        }

        function windowOpenEdit(guid) {
            var jdbm = document.getElementById("JDBM").value;

            var lcbm = document.getElementById("TxtLcbm").value;
            var firstJdbm = document.getElementById("TxtfirstJdbm").value;
            var lastJdbm = document.getElementById("TxtlastJdbm").value;
            var last1Jdbm = document.getElementById("Txtlast1Jdbm").value;
            var url = "BackBoneProcEdit.aspx?GUID=" + guid + "&JDBM=" + jdbm + "&lcbm=" + lcbm + "&firstJdbm=" + firstJdbm + "&lastJdbm=" + lastJdbm + "&last1Jdbm=" + last1Jdbm;
            windowOpenPage(url, "骨干业务", "MenuButton");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td id="Menu_TR" runat="server" align="left" style="background-image: url('../Images/Header/tr_top02.jpg');">
                <table>
                    <tr id="MenuTr" runat="server">
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="TR_ADD" runat="server" style="display: none;">
            <td class="tableHead">
                <input id="AddButton" runat="server" onclick="windowOpenEdit('NEW')" type="button"
                    value="新增" class="btn_2k3" />
            </td>
        </tr>
        <tr id="TR_QUERY" runat="server" style="display: none;">
            <td>
                <table bordercolor="#5b9ed1" cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                    width="100%">
                    <tr>
                        <td class="tdBak" width="10%" align="center">
                            创建时间
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="KSRQ" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);"></asp:TextBox>
                        </td>
                        <td class="tdBak" width="10%" align="center">
                            至
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="JSRQ" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);"></asp:TextBox>
                        </td>
                        <td class="tdBak" width="10%" align="center">
                            业务编码
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="SUBSCRIBER_CODE" runat="server" BorderStyle="None" BorderWidth="0px"
                                Width="100%"></asp:TextBox>
                        </td>
                        <td class="tdBak" width="10%" align="center">
                            业务名称
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="SUB_NAME" runat="server" BorderStyle="None" BorderWidth="0px" Width="100%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="QueryButton" runat="server" Text="查询" CssClass="btn_2k3" OnClick="QueryButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridViewList" runat="server" SkinID="GridView1" DataKeyNames="GUID"
                    BorderWidth="1px" AllowPaging="True" AllowSorting="True" CellSpacing="1" AutoGenerateColumns="False"
                    OnRowDataBound="GridViewList_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="SQDBH" HeaderText="申请单编号">
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="业务编码"  DataField="SUBSCRIBER_CODE"></asp:BoundField>
                        <asp:BoundField HeaderText="用户名称"  DataField="SUB_NAME"/>
                        <asp:BoundField HeaderText="链路名称" DataField="LLMC" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" CurrentPageButtonPosition="End"
                    AlwaysShow="True" CustomInfoHTML="&nbsp;共<font color='red'>%RecordCount%</font>条数据&nbsp;当前第<font color='red'>%CurrentPageIndex%</font>页/共<font color='red'>%PageCount%</font>页"
                    FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PageIndexBoxType="DropDownList"
                    PrevPageText="上一页" ShowCustomInfoSection="Left" CustomInfoTextAlign="Left" HorizontalAlign="Right"
                    OnPageChanging="AspNetPager1_PageChanging" PageSize="15" ShowPageIndexBox="Always"
                    SubmitButtonText="Go" TextAfterPageIndexBox="&nbsp;页&nbsp;" TextBeforePageIndexBox="转到&nbsp;"
                    CssClass="anpager" CurrentPageButtonClass="cpb" CustomInfoSectionWidth="30%"
                    ShowMoreButtons="False" ShowPageIndex="False">
                </webdiyer:AspNetPager>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="JDBM" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="TxtLcbm" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="TxtfirstJdbm" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="Txtlast1Jdbm" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="TxtlastJdbm" runat="server" Style="display: none"></asp:TextBox>
    <asp:Button ID="MenuButton" runat="server" Text="Button" Style="display: none" OnClick="MenuButton_Click" />
    <uc1:windowheader id="windowHeader1" runat="server" />
    </form>
</body>
</html>
