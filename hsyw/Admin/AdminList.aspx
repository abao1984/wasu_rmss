<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminList.aspx.cs" Inherits="Admin_AdminList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="foot" TagName="pageFooter" Src="Include/Ascx/pageFooter.ascx" %>
<%@ Register TagPrefix="header" TagName="pageHeader" Src="Include/Ascx/pageHeader.ascx" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="Include/Ascx/DropData.ascx" TagName="DropData" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <% =Session["PageSubTite"].ToString().Trim() + " - " + Session["PageTitle"].ToString().Trim() + " - " + Session["CopyRightAuthor"].ToString().Trim()%>
    </title>
</head>
<header:pageHeader ID="PageHeader" runat="server" />
<body>
    <form id="form1" runat="server">
    <div>
        <div class="tableMain">
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
                        <HeaderStyle Width="50px" Height="30px" HorizontalAlign="Center" />
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
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="管理员组">
                        <ItemTemplate>
                            <%#strTrim(Convert.ToString(Eval("UserGroup")))%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="管理机构">
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
                            <%#urlUpdate(Convert.ToString(Eval("UserName")))%>
                            <%#urlPass(Convert.ToString(Eval("UserName")))%>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <table cellpadding="0" cellspacing="0" style="width: 100%;" border="0">
                <tr class="tableCategory">
                    <td style="width: 400px; text-align:right; vertical-align: middle">
                        <uc1:DropData ID="DropData_GridView" runat="server" />
                        <asp:Button ID="Button_GridView" CssClass="buttonSmall" runat="server" Text="列表"
                            CausesValidation="False" UseSubmitBehavior="False" OnClick="Button_GridView_Click" />
                    </td>
                    <td align="left">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            EnableTheming="True" CustomInfoSectionWidth="200px" PageIndexBoxType="DropDownList"
                            ShowDisabledButtons="False" Wrap="False" CenterCurrentPageButton="True" 
                            Direction="LeftToRight" HorizontalAlign="Left" CssClass="btn_2k3" 
                            FirstPageText="首页" LastPageText="尾页" NextPageText="后页" PrevPageText="前页" 
                             ShowPageIndexBox="Always" ShowFirstLast="true" ShowPrevNext="true" 
                            AlwaysShow="True" />
                    </td>
                    <td style="width: 10px">
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />
            <asp:Button ID="Button_AddAdmin" CssClass="btn_2k3" runat="server" Text="增加管理员"
                OnClick="Button_AddAdmin_Click" />
            <asp:Button ID="Button_DeleteAdmin" CssClass="btn_2k3" runat="server" Text="删除"
                OnClientClick='return confirm("确认删除吗？");' OnClick="Button_DeleteAdmin_Click" />
        </div>
    </div>
    </form>
</body>
<foot:pageFooter ID="pageFooter" runat="server" />
</html>
<%=Session["Msg"].ToString().Trim()%>