<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestionSolvePlan.aspx.cs"
    Inherits="Web_QuestionMange_QuestionSolvePlan" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="../../calendar.js" type="text/javascript"></script>

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
                            &nbsp;
                        </td>
                        <td style="width: 30%;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridViewEnum" runat="server" DataKeyNames="ID,QUESID" SkinID="GridView1"
                    OnRowDataBound="GridViewEnum_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="时间">
                            <ItemTemplate>
                                <asp:TextBox ID="SJ" Width="100%" BorderStyle="None" BorderWidth="0" runat="server"
                                    Text='<%# Bind("SJ") %>' onfocus="setDayHM(this)"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1" />
                            <ItemStyle Width="30%" BorderColor="#5B9ED1" BorderWidth="1" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="完成情况">
                            <ItemTemplate>
                                <asp:TextBox ID="WCQK" Width="100%" BorderStyle="None" BorderWidth="0" runat="server"
                                    Text='<%# Bind("WCQK") %>'></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1" />
                            <ItemStyle Width="70%" BorderColor="#5B9ED1" BorderWidth="1" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" style="width: 100%; border-collapse: collapse">
                    <tr>
                        <td style="width: 30%;" class="tdBak" align="center">
                            计划解决时间
                        </td>
                        <td style="width: 70%; font-weight: bolder; font-family: 华文中宋; color: Red" align="center">
                            <asp:TextBox ID="JHJJSJ" runat="server" Width="100%" BorderStyle="None" BorderWidth="0" onfocus="setDayHM(this)"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
