<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuZhangMeiJu.aspx.cs" Inherits="Web_GZCL_GZSZ_GuZhangMeiJu" %>
<%@ Register src="../FDZ/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>

<html>
<head runat="server">
    <title></title>
   
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="tableHead">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" Text="保存" OnClick="SaveButton_Click" />
                        </td>
                        <td style="padding-right: 6px">
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
