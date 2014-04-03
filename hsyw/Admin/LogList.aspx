<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogList.aspx.cs" Inherits="Admin_LogList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="Include/Ascx/DropData.ascx" TagName="DropData" TagPrefix="uc1" %>
<%@ Register Assembly="ZLTextBox" Namespace="BaseText" TagPrefix="cc1" %>
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
                <tr class="tableBg1">
                    <td style="height: 40px; text-align: center">
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="12px" Text="起始时间"></asp:Label>
                        &nbsp;
                        <cc1:ZLTextBox ID="ZLTextBox_DateBegin" runat="server" InputType="date" MaxLength="100"
                            CssClass="textBase" Height="26px" Width="120px" IsDisplayTime="true"></cc1:ZLTextBox>
                        &nbsp;&nbsp;
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="12px" Text="终止时间"></asp:Label>
                        &nbsp;
                        <cc1:ZLTextBox ID="ZLTextBox_DateEnd" runat="server" InputType="date" MaxLength="100"
                            CssClass="textBase" Height="26px" Width="120px" IsDisplayTime="false"></cc1:ZLTextBox>
                        &nbsp;&nbsp;
                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="12px" Text="关键字"></asp:Label>
                        &nbsp;
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="textBase" Width="240px" Height="26px"></asp:TextBox>
                        &nbsp;&nbsp;
                        <asp:Button ID="Button_Query" runat="server" CssClass="btn_2k3" Text=" 查询 "
                            OnClick="Button_Query_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="GridView1" SkinID="GridView1" DataKeyNames="ID" runat="server"
                OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="序号">
                        <ItemTemplate>
                            <font color="#000000" style="font-size: 10px">
                                <%# GetCount()%>
                            </font>
                        </ItemTemplate>
                        <ItemStyle Height="40px" HorizontalAlign="Center" />
                        <HeaderStyle Width="5%" Height="30px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False">
                        <HeaderTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="选" AutoPostBack="true" OnCheckedChanged="CheckAll" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="ItemCheckBox" runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="36px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="操作时间" DataField="USERDATETIME" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                        <ItemStyle Width="120px" HorizontalAlign="center" Font-Size="10px" />
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="IP">
                        <ItemTemplate>
                            <font color="#000000" style="font-size: 10px">
                                <%#strTrim(Convert.ToString(Eval("Ip")))%>
                            </font>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="center" />
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="用户">
                        <ItemTemplate>
                            <font color="#000000" style="font-size: 11px">
                                <%#strTrim(Convert.ToString(Eval("UserName")))%>
                            </font>
                        </ItemTemplate>
                        <ItemStyle Width="240px" HorizontalAlign="left" />
                        <HeaderStyle HorizontalAlign="center" Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="标题">
                        <ItemTemplate>
                            <%#strTrim(Convert.ToString(Eval("Title")))%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="center" Width="20%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作内容">
                        <ItemTemplate>
                            <%#strTrim(Convert.ToString(Eval("MEMO")))%>
                        </ItemTemplate>
                        <HeaderStyle Width="40%" />
                        <ItemStyle HorizontalAlign="Left" />
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
            <asp:Button ID="Button_Delete" CssClass="btn_2k3" runat="server" Text="删除日志"
                OnClick="Button_Delete_Click" />
        </div>
    </div>
    </form>
</body>
</html>
