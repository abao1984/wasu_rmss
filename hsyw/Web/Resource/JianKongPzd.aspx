<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JianKongPzd.aspx.cs" Inherits="Web_Resource_JianKongPzd" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <script type="text/javascript">
        function windowOpen(guid,lx) {
            var url = "JianKongSbEdit.aspx?guid=" + guid;
            windowOpenPage(url, lx+"监控配置单", "Btn");
        }

        function Delete() {
            if (!confirm("删除主记录时会把了记录也删除，确认要删除吗?")) {
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" height="100%"  style="border-collapse: collapse;"cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
        <tr height="30px">
            <td align="left"  colspan="6" class="tdBak">
                <input id="BtnAdd" class="btn_2k3" type="button" value="新增" onclick="windowOpen('','新增');" />
                <asp:Button ID="BtnDelete" CssClass="btn_2k3" runat="server" Text="删除" 
                    onclick="BtnDelete_Click" OnClientClick="return Delete();" />
                <asp:Button ID="BtnQuery" CssClass="btn_2k3" runat="server" Text="查询" 
                    onclick="BtnQuery_Click" />
                <asp:Button ID="BtnExport" runat="server" Text="导出Excel" CssClass="btn_2k3" 
                    onclick="BtnExport_Click" />
            </td>
        </tr>
        <tr height="30px">
            <td class="tdBak" align="right" width="12%">客户编号</td>
            <td width="20%">
                <asp:TextBox ID="TxtKhBh" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
            </td>
            <td class="tdBak" align="right" width="13%">客户名称</td>
            <td width="20%">
                <asp:TextBox ID="TxtKhMc" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>  
            </td>
            <td class="tdBak" align="right" width="15%">客户所属区域</td>
            <td width="20%">
                <asp:TextBox ID="TxtKhQy" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="6" valign="top">
                <asp:GridView ID="GridViewJK" SkinID="GridView1" runat="server" 
                    AutoGenerateColumns="False" DataKeyNames="GUID" 
                    onrowdatabound="GridViewJK_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText=" 选择">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="KHBH" HeaderText="客户编号" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="KHQY" HeaderText="客户区域" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="XMFL" HeaderText="项目分类" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="JKPTXX" HeaderText="监控平台信息" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IPDZ" HeaderText="IP地址" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="JKSB" HeaderText="监控设备" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
           <tr>
            <td height="1" colspan="6">
                <table class="tdBak" id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td width="100%" style="padding-left: 6px;">
                            <font face="宋体">总共有
                                <asp:Label ID="DataCountLab" runat="server" ForeColor="Red"></asp:Label></font><font
                                    face="宋体">条记录，当前第
                                    <asp:Label ID="PageIndexLab" runat="server" ForeColor="Red"></asp:Label>页，共
                                    <asp:Label ID="PageCountLab" runat="server" ForeColor="Red"></asp:Label>页</font>
                        </td>
                        <td width="1">
                            <asp:Label ID="Label1" runat="server" Width="55px">单页显示</asp:Label>
                        </td>
                        <td width="1">
                            <font face="宋体">
                                <asp:DropDownList ID="PageSize" runat="server" ForeColor="Red" Font-Bold="True" AutoPostBack="True"
                                    Width="60px" OnSelectedIndexChanged="PageSize_SelectedIndexChanged">
                                    
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="500">500</asp:ListItem>
                                </asp:DropDownList>
                            </font>
                        </td>
                        <td width="1">
                            <asp:LinkButton ID="PrevButton" runat="server" ForeColor="#003797" Width="50px" OnClick="PrevButton_Click">上一页</asp:LinkButton>
                        </td>
                        <td width="1">
                            <asp:DropDownList ID="GridPageList" runat="server" ForeColor="Red" Font-Bold="True"
                                AutoPostBack="True" Width="50px" OnSelectedIndexChanged="GridPageList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="1">
                            <asp:LinkButton ID="NextButton" runat="server" ForeColor="#003797" Width="50px" OnClick="NextButton_Click">下一页</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
     <uc1:windowHeader ID="windowHeader1" runat="server" />
     <div style="display:none">
        <asp:Button ID="Btn" runat="server" Text="Button" onclick="Btn_Click" />
     </div>
    </form>
</body>
</html>
