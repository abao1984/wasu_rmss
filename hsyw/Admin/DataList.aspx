<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataList.aspx.cs" Inherits="Web_sysData_DataList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="Include/Ascx/DropData.ascx" TagName="DropData" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
    </title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="tableMain">
            <table>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr class="tableBg1">
                    <td style="height: 40px; text-align: center">
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="12px" Text="关键字"></asp:Label>
                        &nbsp;
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="textBase" Width="240px" Height="26px"></asp:TextBox>
                        &nbsp;&nbsp;
                        <asp:Button ID="Button_Query" runat="server" CssClass="btn_2k3" Text=" 查询 "
                            OnClick="Button_Query_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="GridView1" SkinID="GridView1" DataKeyNames="DataCode" runat="server"
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
                            <%#strTrim(Convert.ToString(Eval("DataName")))%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="MENU" HeaderText="所属菜单">
                    <ItemStyle Width="300px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="新记录">
                        <ItemTemplate>
                            <%#strAdd(Convert.ToString(Eval("DataCode")))%>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="详细资料">
                        <ItemTemplate>
                            <%#strUrl(Convert.ToString(Eval("DataCode")))%>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <table style="width:100%">
                <tr class="tableCategory">
                    <td style="width: 400px; text-align: right; vertical-align: middle">
                        <uc1:DropData ID="DropData_GridView" runat="server" />
                        <asp:Button ID="Button_GridView" CssClass="buttonSmall" runat="server" Text="列表"
                            CausesValidation="False" UseSubmitBehavior="False" OnClick="Button_GridView_Click" />
                    </td>
                    <td style="text-align: left; vertical-align: middle">
                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                            EnableTheming="True" CustomInfoSectionWidth="200px" PageIndexBoxType="DropDownList"
                            ShowDisabledButtons="False" Wrap="False" CenterCurrentPageButton="True" Direction="LeftToRight"
                            HorizontalAlign="Left" CssClass="btn_2k3" FirstPageText="首页" LastPageText="尾页"
                            NextPageText="后页" PrevPageText="前页" ShowPageIndexBox="Always" ShowFirstLast="true"
                            ShowPrevNext="true" AlwaysShow="True" />
                    </td>
                    <td style="width: 10px">
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Button ID="Button_AddData" CssClass="btn_2k3" runat="server" Text="增加新字典"
                OnClick="Button_AddData_Click" />
        </div>
    </div>
    </form>
</body>
</html>
