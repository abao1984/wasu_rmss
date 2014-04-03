<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigLightJianChe.aspx.cs" Inherits="Web_Resource_ConfigLightJianChe" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <table style="width: 100%; height: 100%;">
        <tr>
            <td class="tableHead">
                <asp:Button ID="BtnQuery" runat="server" Text="监测" CssClass="btn_2k3" OnClick="BtnQuery_Click" />
            </td>
        </tr>
        <tr>
            <td align="center"  class="tdBak" >
                客户资源
            </td>
        </tr>
        <tr>
            <td style="height: 50%" valign="top">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="GridView1" runat="server" Width="100%"
                        SkinID="GridView1"   AllowPaging="True">
                        <Columns>
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
                <table class="tdBak" id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td width="100%" style="padding-left: 6px;">
                            <font face="宋体">总共有
                                <asp:Label ID="DataCountLab" runat="server" ForeColor="Red"></asp:Label></font><font
                                    face="宋体">条记录，当前第
                                    <asp:Label ID="PageIndexLab" runat="server" ForeColor="Red"></asp:Label>页，共
                                    <asp:Label ID="PageCountLab" runat="server" ForeColor="Red"></asp:Label>页</font>
                        </td>
                        <td width="1">
                            <asp:Label ID="Label2" runat="server" Width="55px">单页显示</asp:Label>
                        </td>
                        <td width="1">
                            <font face="宋体">
                                <asp:DropDownList ID="PageSize" runat="server" ForeColor="Red" Font-Bold="True" AutoPostBack="True"
                                    Width="60px" OnSelectedIndexChanged="PageSize_SelectedIndexChanged">
                                    
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
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
        <tr>
            <td align="center"  class="tdBak" >
                骨干资源
            </td>
        </tr>
        <tr>
            <td style="height: 50%" valign="top">
                  <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="GridView2" runat="server" Width="100%"
                        SkinID="GridView1" 
                        DataKeyNames="YWBM" AllowPaging="True">
                        <Columns>
                           <%-- <asp:TemplateField HeaderText="选择">
                                <ItemStyle Width="4%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick='windowOpen("","<%# Eval("SUBSCRIBERNO") %>")' >创建</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:CommandField HeaderText="选择" ShowSelectButton="True" SelectText="选择">
                            <HeaderStyle Width="5%" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:BoundField HeaderText="业务编号" DataField="YWBM">
                                <HeaderStyle Width="10%" />
                            </asp:BoundField>                           
                            <asp:BoundField HeaderText="业务类型" >
                                <HeaderStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="链路名称" DataField="LLMC">
                                <HeaderStyle Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="完整纤号" DataField="WZXH">
                                <HeaderStyle Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="申请人" DataField="SQR">
                                <HeaderStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="申请时间" DataField="SQSJ">
                            <HeaderStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="启动时间" DataField="QDSJ">
                            <HeaderStyle Width="10%" />
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
                                <asp:Label ID="DataCountLab1" runat="server" ForeColor="Red"></asp:Label></font><font
                                    face="宋体">条记录，当前第
                                    <asp:Label ID="PageIndexLab1" runat="server" ForeColor="Red"></asp:Label>页，共
                                    <asp:Label ID="PageCountLab1" runat="server" ForeColor="Red"></asp:Label>页</font>
                        </td>
                        <td width="1">
                            <asp:Label ID="Label1" runat="server" Width="55px">单页显示</asp:Label>
                        </td>
                        <td width="1">
                            <font face="宋体">
                                <asp:DropDownList ID="PageSize1" runat="server" ForeColor="Red" Font-Bold="True" AutoPostBack="True"
                                    Width="60px" OnSelectedIndexChanged="PageSize_SelectedIndexChanged">
                                    
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                            </font>
                        </td>
                        <td width="1">
                            <asp:LinkButton ID="PrevButton1" runat="server" ForeColor="#003797" Width="50px" OnClick="PrevButton1_Click">上一页</asp:LinkButton>
                        </td>
                        <td width="1">
                            <asp:DropDownList ID="GridPageList1" runat="server" ForeColor="Red" Font-Bold="True"
                                AutoPostBack="True" Width="50px" OnSelectedIndexChanged="GridPageList1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="1">
                            <asp:LinkButton ID="NextButton1" runat="server" ForeColor="#003797" Width="50px" OnClick="NextButton1_Click">下一页</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
