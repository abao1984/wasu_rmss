<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FanDanGaoJing.aspx.cs" Inherits="Web_GZCL_FDZ_FanDanGaoJing" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="120px" 
                        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Button ID="Button1" runat="server" Text=" 保 存 " onclick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        Width="50%" SkinID="GridView1" DataKeyNames="GUID" 
                       >
                        <Columns>
                            <asp:TemplateField HeaderText="取消警告">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server"  Checked='<%# Eval("SFQY").ToString()=="1"?true:false %>' 
                                         />
                                </ItemTemplate>
                                
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="告警环节" DataField="codename"  >
                                <ItemStyle Width="35%"  HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="告警期限(分钟)">
                                <ItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" BorderStyle="None" Width="100%" 
                                        Text='<%# Bind("MS") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
