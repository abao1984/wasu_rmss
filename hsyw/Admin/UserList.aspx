﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="Admin_UserList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="Include/Ascx/DropData.ascx" TagName="DropData" TagPrefix="uc1" %>
<%@ Register Src="Include/Ascx/DropBranch.ascx" TagName="DropBranch" TagPrefix="uc2" %>
<%@ Register Src="Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head" runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
        function OpenBranch(name, code) {
            windowOpenPageByWidth("../web/resource/BranchTree.aspx?NAME=" + name + "&CODE=" + code, "选择所属区域和部门", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }
        function CheckSelect() {
            var controls = document.getElementById("GridView1").getElementsByTagName("input");
            var count = controls.length;
            var num = 0;
            for (var i = 0; i < count; i++) {
                if (controls[i].type == "checkbox" && controls[i].checked == true) {
                    num++;
                }
            }
            if (num == 0) {
                alert("请至少选择一条数据！");
                window.event.returnValue = false;
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="height: 100%; width: 100%;" cellpadding="0" cellspacing="0">
        <tr>
            <td class="tdHead" align="center">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            用户名&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_UserName" runat="server" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;&nbsp;姓名&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_Name" runat="server" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;&nbsp;区域部门&nbsp;&nbsp;
                        </td>
                        <td style="border: solid 1px #5b9ed1;">
                            <asp:TextBox ID="SSQY" runat="server" BorderWidth="0" Width="200px"></asp:TextBox>
                        </td>
                        <td style="border: solid 1px #5b9ed1;">
                            <img alt="" src="../web/Images/Small/bb_table.gif" onclick="OpenBranch('SSQY','SSQY_CODE')"
                                style="cursor: hand" />
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox_SubBranch" Checked="true" runat="server" Text="含子部门" />
                        </td>
                        <td>
                            <asp:Button ID="Button_Query" runat="server" CssClass="btn_2k3" Text=" 查询 " OnClick="Button_Query_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%" class="tdBak">
                <div style="height: 100%; width: 100%; overflow: auto">
                    <asp:GridView ID="GridView1" SkinID="GridView1" DataKeyNames="UserName" runat="server"
                        OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="序号">
                                <ItemTemplate>
                                    <font color="#000000" style="font-size: 10px">
                                        <%# GetCount()%>
                                    </font>
                                </ItemTemplate>
                                <ItemStyle Height="40px" HorizontalAlign="Center" />
                                <HeaderStyle Width="40px" Height="30px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckBox_All" runat="server" Text="选" AutoPostBack="true" OnCheckedChanged="CheckAll" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ItemCheckBox" runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="36px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="用户名">
                                <ItemTemplate>
                                    <%#strTrim(Convert.ToString(Eval("UserName")))%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="姓名">
                                <ItemTemplate>
                                    <%#strTrim(Convert.ToString(Eval("UserRealName")))%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                                <HeaderStyle Width="120px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="手机号码">
                                <ItemTemplate>
                                    <%#strTrim(Convert.ToString(Eval("UserPhone")))%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                                <HeaderStyle Width="120px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="所在机构">
                                <ItemTemplate>
                                    <%#strTrim(Convert.ToString(Eval("BranchName")))%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="机构代码">
                                <ItemTemplate>
                                    <font color="#000000" style="font-size: 10px">
                                        <%#strTrim(Convert.ToString(Eval("BranchCode")))%>
                                    </font>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="详细内容">
                                <ItemTemplate>
                                    <%#urlUpdate(Convert.ToString(Eval("UserName")), Convert.ToString(Eval("FWQY")))%>
                                    <%#urlCopy(Convert.ToString(Eval("UserName")))%>
                                    <%--<%#urlUpdate(Convert.ToString(Eval("UserName")))%>
                                    <%#urlPass(Convert.ToString(Eval("UserName")))%>
                                    <%#urlGroup(Convert.ToString(Eval("UserName")))%>
                                    <%#urlFWQY("'"+Convert.ToString(Eval("UserName"))+"'",Convert.ToString(Eval("FWQY")))%>--%>
                                </ItemTemplate>
                                <ItemStyle Width="250px" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="250px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 400px; text-align: right; vertical-align: middle">
                            <uc1:DropData ID="DropData_GridView" runat="server" />
                            <asp:Button ID="Button_GridView" CssClass="btn_2k3" runat="server" Text="列表" CausesValidation="False"
                                UseSubmitBehavior="False" OnClick="Button_GridView_Click" />
                        </td>
                        <td align="left">
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                PageIndexBoxType="DropDownList" 
                                ShowPageIndexBox="Always" AlwaysShow="True" Width="100%" />
                        </td>
                        <td style="width: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="tdBak">
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center" class="tdBak">
                            <asp:Button ID="Button_AddUser" CssClass="btn_2k3" runat="server" Text="增加用户" OnClick="Button_AddUser_Click" />
                            <asp:Button ID="Button_DeleteUser" CssClass="btn_2k3" runat="server" Text="删除" OnClientClick='return confirm("确认删除吗？");'
                                OnClick="Button_DeleteUser_Click" />
                            <asp:Button ID="BtnPLSetUp" runat="server" Text="批量设置" CssClass="btn_2k3" 
                                OnClientClick="CheckSelect();" onclick="BtnPLSetUp_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="SSQY_CODE" runat="server" Style="display: none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
