<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogList.aspx.cs" Inherits="Web_LogList" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;height:100%">
                        <tr>
                            <td align="center" class="tableTitle" style="height:10px;">
                <asp:Label ID="LabelTitle" runat="server">操作日志</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
            <div id="PhyResourceDIV" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%" align="center" runat="server"  >
                    <asp:GridView ID="LogGrid" runat="server" SkinID="GridView1" Width="100%"  AllowSorting="True"  AutoGenerateColumns="False" >
                        <Columns>
                            <asp:BoundField DataField="USERDATETIME" HeaderText="操作时间" 
                                DataFormatString="{0:yy年MM月dd日HH:mm}" >
                            <HeaderStyle Width="14%" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="USERNAME" HeaderText="操作用户" >
                            <HeaderStyle Width="18%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IP" HeaderText="操作地址">
                            <HeaderStyle Width="10%" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TITLE" HeaderText="操作主题" >
                            <HeaderStyle Width="15%" />
                            </asp:BoundField>
                           <asp:TemplateField HeaderText="操作内容">
                        <ItemTemplate>
                            <%#Convert.ToString(Eval("MEMO"))%>
                        </ItemTemplate>
                        <HeaderStyle Width="43%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                           
                        </Columns>
                    </asp:GridView>
                </div>
                            </td>
                        </tr>
                    </table>

    </form>
</body>
</html>
