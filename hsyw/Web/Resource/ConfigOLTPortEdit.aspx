<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigOLTPortEdit.aspx.cs"
    Inherits="Web_Resource_ConfigOLTPortEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Main_Table" style="width: 100%; height: 100%;">
        <tr>
            <td class="tableHead">
                <asp:Button ID="OKButton" runat="server" CssClass="btn_2k3" Text="确定" OnClick="OKButton_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <div id="PhyResourceDIV" style="overflow: auto; width: 100%; height: 100%" align="center"
                    runat="server">
                    <asp:GridView ID="GridViewPhyResource" runat="server" SkinID="GridView1" 
                        DataKeyNames="GUID,ONU1,ONU2,ONU3,ONU4" onrowdatabound="GridViewPhyResource_RowDataBound">
                        <Columns>
                            <%--<asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="Sel" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="3%"
                                    Wrap="true" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" Width="4%" />
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="VIRTUALPORT" HeaderText="虚拟端口">
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="3%"
                                    Wrap="true" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" Width="20%" />
                            </asp:BoundField>
                           <%-- <asp:BoundField DataField="ZT" HeaderText="状态">
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="3%"
                                    Wrap="true" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" Width="20%" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="OUN 1">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" />
                                    <asp:CheckBox ID="CheckBox1" runat="server"  />
                                </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="3%"
                                    Wrap="true" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" Width="20%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OUN 2">
                                <ItemTemplate>
                                    <asp:Image ID="Image2" runat="server" />
                                    <asp:CheckBox ID="CheckBox2" runat="server"  />
                                </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="3%"
                                    Wrap="true" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" Width="20%" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="OUN 3">
                                <ItemTemplate>
                                    <asp:Image ID="Image3" runat="server" />
                                    <asp:CheckBox ID="CheckBox3" runat="server"  />
                                </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="3%"
                                    Wrap="true" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" Width="20%" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="OUN 4">
                                <ItemTemplate>
                                    <asp:Image ID="Image4" runat="server" />
                                    <asp:CheckBox ID="CheckBox4" runat="server"  />
                                </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="3%"
                                    Wrap="true" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" Width="20%" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <div style="display: none">
        <asp:TextBox ID="PORT_GUID" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
