<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RmssSelect.aspx.cs" Inherits="Web_Resource_RmssSelect" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;
        height: 100%" width="100%">
        <tr>
            <td class="tableHead" style="height: 31px;">
                <asp:Button ID="QueryButton" runat="server" CssClass="btn_2k3" Text="查询" 
                    onclick="QueryButton_Click" />
                <asp:Button ID="CancelButton" runat="server" CssClass="btn_2k3" 
                    onclick="CancelButton_Click" Text="取消选择" />
            </td>
        </tr>
        <tr>
            <td style="height: 31px;">
                <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                    bordercolor="#5b9ed1" width="100%">
                    <tr>
                        <td width="15%" class="tdBak" align="center">
                            业务编码
                        </td>
                        <td width="18%">
                            <asp:TextBox ID="YWBM" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                            <asp:TextBox ID="YWBMID" runat="server" Width="100%" style="display:none"></asp:TextBox>
                        </td>
                        <td width="15%" class="tdBak" align="center">
                            客户名称
                        </td>
                        <td width="18%">
                            <asp:TextBox ID="KHMC" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="15%" class="tdBak" align="center">
                            所属区域
                        </td>
                        <td width="18%">
                            <asp:DropDownList ID="SSQY" runat="server" Width="100%" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%" class="tdBak" align="center">
                            客户类型
                        </td>
                        <td width="18%">
                            <asp:DropDownList ID="KHLX" runat="server" Width="100%" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td width="15%" class="tdBak" align="center">
                            业务类型
                        </td>
                        <td width="18%">
                            <asp:DropDownList ID="YWLX" runat="server" Width="100%" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td colspan="2" class="tdBak">
                            &nbsp;
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
                        onselectedindexchanging="GridView1_SelectedIndexChanging" 
                        DataKeyNames="SUBSCRIBER_ID" AllowPaging="True">
                        <Columns>
                           <%-- <asp:TemplateField HeaderText="选择">
                                <ItemStyle Width="4%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick='windowOpen("","<%# Eval("SUBSCRIBERNO") %>")' >创建</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:CommandField HeaderText="选择" ShowSelectButton="True" SelectText="选择">
                            <HeaderStyle Width="10%" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:BoundField HeaderText="业务编号" DataField="SUBSCRIBER_CODE">
                                <HeaderStyle Width="10%" />
                            </asp:BoundField>                           
                            <asp:BoundField HeaderText="客户名称" DataField="CUSTOMER_NAME">
                                <HeaderStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="客户地址" DataField="ADDRESS">
                                <HeaderStyle />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="客户类型">
                                <HeaderStyle Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="业务联系人" DataField="LINKMAN">
                                <HeaderStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="联系电话" DataField="MOBILE_NO">
                                <HeaderStyle Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="业务类型">
                                <HeaderStyle Width="6%" />
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
    <asp:TextBox ID="YWBM_NAME" runat="server" Width="100%"  style="display:none;"></asp:TextBox>
    </form>
</body>
</html>
