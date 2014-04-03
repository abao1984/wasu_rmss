<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestionSet.aspx.cs" Inherits="Web_QuestionMange_QuestionSet" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="tableHead">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 30%;">
                            <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" Text="保存" OnClick="SaveButton_Click" />
                        </td>
                        <td style="width: 40%; font-weight: bolder; font-family: 华文中宋; color: Red" align="center">
                            <asp:Label ID="LblTitle" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="width: 30%;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridViewEnum" runat="server" DataKeyNames="ENUM_GUID" SkinID="GridView1"
                    OnRowDataBound="GridViewEnum_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:TextBox ID="SEQUENCE" Width="100%" BorderStyle="None" BorderWidth="0" runat="server"
                                    Text='<%# Bind("SEQUENCE") %>'></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1" />
                            <ItemStyle Width="10%" BorderColor="#5B9ED1" BorderWidth="1" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="数据名称">
                            <ItemTemplate>
                                <asp:TextBox ID="ENUM_NAME" Width="100%" BorderStyle="None" BorderWidth="0" runat="server"
                                    Text='<%# Bind("ENUM_NAME") %>'></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1" />
                            <ItemStyle Width="30%" BorderColor="#5B9ED1" BorderWidth="1" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="数据简称">
                            <ItemTemplate>
                                <asp:TextBox ID="ENUM_SHORT" Width="100%" BorderStyle="None" BorderWidth="0" runat="server"
                                    Text='<%# Bind("ENUM_SHORT") %>'></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1" />
                            <ItemStyle Width="20%" BorderColor="#5B9ED1" BorderWidth="1" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="图标路径">
                            <ItemTemplate>
                                <asp:TextBox ID="IMAGE_URL" Width="100%" BorderStyle="None" BorderWidth="0" runat="server"
                                    Text='<%# Bind("IMAGE_URL") %>'></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1" />
                            <ItemStyle Width="40%" BorderColor="#5B9ED1" BorderWidth="1" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="ENUM_SORT" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="P_ENUM_NAME" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
    </form>
</body>
</html>
