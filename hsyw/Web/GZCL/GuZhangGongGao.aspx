<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuZhangGongGao.aspx.cs" Inherits="Web_GZCL_GuZhangGongGao" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
        <tr>
            <td style="height: 1px;">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 100px" align="center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td style="width: 100px" align="center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;&nbsp;
                            &nbsp; 
                        </td>
                        <td>
                            &nbsp;
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 31px;" class="tdHead">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 100px" align="center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td style="width: 100px" align="center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;&nbsp;
                            &nbsp; 
                        </td>
                        <td>
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
                        SkinID="GridView1" DataKeyNames="ZBGUID" AllowPaging="True">
                        <Columns>
                            <asp:TemplateField HeaderText="投诉编号">
                                <ItemTemplate>
                                    <a href="javascript:OpenGZ('<%# Eval("GZBH") %>')" style="text-decoration:underline;"><%# Eval("GZBH") %></a>
                                </ItemTemplate>
                                <ItemStyle Width="8%" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="所属区域" DataField="KHQY">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障名称" DataField="GZMC" 
                                ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="投诉时间" DataField="TSSJ" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                            <asp:BoundField HeaderText="故障专业" DataField="GZZY" />
                            <asp:BoundField HeaderText="故障级别" DataField="GZDJ" >
                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障层次" DataField="GZCC">
                            <ItemStyle Width="70px" HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障类型" DataField="GZLX" >
                            <ItemStyle Width="70px" HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="创建人" DataField="GZCJRNAME" >
                            <ItemStyle Width="6%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="拥有人">
                                <ItemTemplate>
                                     <a href="javascript:OpenYYR('<%# Eval("GZYYR") %>')" style="text-decoration:underline; color:Blue;"><%# Eval("GZYYRNAME")%></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="故障时长(分)" DataField="GZSC" >
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障状态" DataField="GZZT" >
                            <ItemStyle Width="10%" HorizontalAlign="Center"/>
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
    </form>
</body>
</html>
