<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JianKongSbQuery.aspx.cs" Inherits="Web_Resource_JianKongSbQuery" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            background: #e6f3fc;
            background-repeat: repeat-x;
            background-position: 0,0;
            color: #003797;
            font-family: "宋体";
            font-size: 12px; /*font-weight: bold;*/;
            line-height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" height="100%">
        <tr height="30px">
            <td align="right" class="tdBak" width="13%">所属区域</td>
            <td width="20%">
                <asp:TextBox ID="TxtSsqy" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
            </td>
            <td align="right" class="tdBak" width="12%">设备编号</td>
            <td width="20%">
            <asp:TextBox ID="TxtSbBh" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
            </td>
            <td align="right" class="tdBak" width="15%">设备名称</td>
            <td width="20%">
            <asp:TextBox ID="TxtSbMc" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
            </td>
        </tr>
        <tr height="30px">
            <td align="left" class="tdBak" colspan="6">
                <asp:Button ID="BtnQuery" CssClass="btn_2k3" runat="server" Text="查询" 
                    onclick="BtnQuery_Click" />
                    <asp:Button ID="BtnOk" runat="server" Text="确认" CssClass="btn_2k3" 
                    onclick="BtnOk_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="6" valign="top">
                <asp:GridView ID="GridView1" SkinID="GridView1" runat="server" 
                    AutoGenerateColumns="False" DataKeyNames="GUID,EQU_NAME">
                    <Columns>
                        <asp:TemplateField HeaderText="选择">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="house_area" HeaderText="所属区域">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="equ_code" HeaderText="设备编号">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="equ_name" HeaderText="设备名称">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="sblx" HeaderText="设备类型">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
           <tr>
            <td colspan="6"  style="height: 1px; border: 1px solid #F0F0F0">
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
    <div style="display:none">
        <asp:TextBox ID="SBMC" runat="server"></asp:TextBox>
        <asp:TextBox ID="SBID" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
