<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JianKongUserQuery.aspx.cs" Inherits="Web_Resource_JianKongUserQuery" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
      
    </style>
</head>
<body>
    <form id="form1" runat="server">
       <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;
        height: 100%" width="100%">
        <tr>
            <td class="tableHead" style="height: 31px;">
                <asp:Button ID="BtnOk" runat="server" Text="确认" CssClass="btn_2k3" 
                    onclick="BtnOk_Click" />
                <asp:Button ID="QueryButton" runat="server" CssClass="btn_2k3" Text="查询" 
                    onclick="QueryButton_Click" />
               
            </td>
        </tr>
        <tr>
            <td style="height: 31px;">
                <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                    bordercolor="#5b9ed1" width="100%">
                    <tr>
                        <td width="15%" class="tdBak" align="center">
                            <span lang="zh-cn">用户</span>编码
                        </td>
                        <td width="18%">
                            <asp:TextBox ID="YHBH" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="15%" class="tdBak" align="center">
                           用户名称
                        </td>
                        <td width="18%">
                            <asp:TextBox ID="YHMC" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="15%" align="center">
                            </td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%;">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        SkinID="GridView1" 
                        DataKeyNames="SUBSCRIBER_CODE,SUB_NAME" AllowPaging="True">
                        <Columns>
                           <asp:TemplateField HeaderText="选择">
                                <ItemStyle Width="4%" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:BoundField HeaderText="用户编号" DataField="SUBSCRIBER_CODE">
                                <HeaderStyle Width="10%" />
                            </asp:BoundField>                           
                            <asp:BoundField HeaderText="用户名称" DataField="SUB_NAME">
                                <HeaderStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="业务联系人" DataField="LINKMAN">
                                <HeaderStyle />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="联系电话" DataField="MOBILE_NO">
                                <HeaderStyle Width="8%" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td style="height: 1px; border: 1px solid #F0F0F0">
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
        <asp:TextBox ID="TextYHBH" runat="server"></asp:TextBox>
        <asp:TextBox ID="TextYHMC" runat="server"></asp:TextBox>
        <asp:TextBox ID="KHBM" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
