<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataSubList.aspx.cs" Inherits="Web_sysData_DataSubList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="Include/Ascx/DropData.ascx" TagName="DropData" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="tableMain">
            <asp:GridView ID="GridView1" SkinID="GridView1" DataKeyNames="ID" runat="server"
                OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False">
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
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="选" AutoPostBack="true" OnCheckedChanged="CheckAll" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="ItemCheckBox" runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="36px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="字典代码">
                        <ItemTemplate>
                            <font color="#000000" style="font-size: 11px">
                                <%#strTrim(Convert.ToString(Eval("DataCode")))%>
                            </font>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="center" />
                        <HeaderStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="字典名称">
                        <ItemTemplate>
                            <font color="#000000" style="font-size: 11px">
                                <%#strTrim(Convert.ToString(Eval("DataName")))%>
                            </font>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="center" />
                        <HeaderStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="字段代码">
                        <ItemTemplate>
                            <font color="#000000" style="font-size: 11px">
                                <%#strTrim(Convert.ToString(Eval("DataDm")))%>
                            </font>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="center" />
                        <HeaderStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="字段名称">
                        <ItemTemplate>
                            <%#strTrim(Convert.ToString(Eval("DataMc")))%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="字段值">
                        <ItemTemplate>
                            <%#strTrim(Convert.ToString(Eval("ZDZ")))%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="MENU" HeaderText="所属菜单">
                        <ItemStyle Width="300px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="详细资料">
                        <ItemTemplate>
                            <%#strUrl(Convert.ToString(Eval("ID")))%>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <table>
                <tr class="tableCategory">
                    <td style="width: 400px; text-align: left; vertical-align: middle">
                        <uc1:DropData ID="DropData_GridView" runat="server" />
                        <asp:Button ID="Button_GridView" CssClass="buttonSmall" runat="server" Text="列表"
                            CausesValidation="False" UseSubmitBehavior="False" OnClick="Button_GridView_Click" />
                    </td>
                    <td style="text-align: right; vertical-align: middle">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" SkinID="AspNetPager1" OnPageChanged="AspNetPager1_PageChanged"
                            EnableTheming="True" />
                    </td>
                    <td style="width: 10px">
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Button ID="Button_Delete" CssClass="btn_2k3" runat="server" Text="删除" OnClick="Button_Delete_Click" />
            <asp:Button ID="Button_AddData" CssClass="btn_2k3" runat="server" Text="增加新字段" OnClick="Button_AddData_Click" />
            <asp:Button ID="Button_Cancel" CssClass="btn_2k3" runat="server" Text="返回" OnClick="Button_Cancel_Click" />
        </div>
    </div>
    </form>
</body>
</html>
