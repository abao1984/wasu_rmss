<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GroupUsers.aspx.cs" Inherits="Admin_GroupUsers" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table class="table">
            <tr>
                <td>
                    <asp:GridView ID="GridView1" SkinID="GridView1" runat="server" 
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="USERREALNAME" HeaderText="姓名">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="USERNAME" HeaderText="登录名">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BRANCHNAME" HeaderText="所属部门">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ZT" HeaderText="状态">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
