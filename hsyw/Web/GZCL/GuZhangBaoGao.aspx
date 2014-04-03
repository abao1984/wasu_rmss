<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuZhangBaoGao.aspx.cs" Inherits="Web_GZCL_GuZhangBaoGao" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
        function windowOpen(gzbh) {
            document.getElementById("EditPage").src = "GuZhangBaoGaoEdit.aspx?GZBH=" + gzbh;
            document.getElementById("EditDiv").style.display = "block";
            document.getElementById("LabelHead").innerText = "故障报告";
        }
        function MinWindow() {
            document.getElementById("EditTR").style.display = "none";
            document.getElementById("EditDiv").style.height = "30px";
            document.getElementById("EditDiv").style.top = document.body.offsetHeight - 30;
        }
        function MaxWinodw() {
            document.getElementById("EditTR").style.display = "block";
            document.getElementById("EditDiv").style.height = "100%";
            document.getElementById("EditDiv").style.top = "0px";
        }
        function WindowClose() {
            document.getElementById("EditDiv").style.display = "none";
            document.getElementById("Btn").click();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
        <tr>
            <td style="height: 31px;" class="tdHead">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center">
                            <asp:Button ID="BtnQuery" runat="server" Text="查询" CssClass="btn_2k3" OnClick="BtnQuery_Click" />
                        </td>
                        <td>
                            <asp:Button ID="BtnSJ" runat="server" Text="随机" CssClass="btn_2k3" OnClick="BtnSJ_Click" />
                        </td>
                        <td align="center">
                            <asp:Button ID="BtnDel" runat="server" Text="删除" CssClass="btn_2k3" OnClick="BtnDel_Click" OnClientClick="return confirm('确定要删除吗?');"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdBak">
                <table cellpadding="0" cellspacing="0" width="100%" border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td style="width: 12%" align="center" class="tdBak">
                                                        故障名称
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="GZMS" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td style="width: 12%" class="tdBak" align="center">
                            业务主体
                        </td>
                        <td style="width: 13%">
                            <asp:DropDownList ID="YWZT" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td width="12%" class="tdBak" align="center">
                            投诉时间
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="TSSJ1" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                Width="100%"></asp:TextBox>
                        </td>
                        <td width="2%" class="tdBak" align="center">
                            至
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="TSSJ2" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%;">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="gvGZBG" runat="server" Width="100%" AutoGenerateColumns="False"
                        SkinID="GridView1" DataKeyNames="GZBGGUID,GZBH" AllowPaging="True">
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="投诉编号">
                                <ItemTemplate>
                                    <a href="javascript:windowOpen('<%# Eval("GZBH") %>')" style="text-decoration: underline;">
                                        <%# Eval("GZBH") %></a>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="业务主体" DataField="GZZY">
                                <ItemStyle Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障来源" DataField="GZLY">
                                <ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="创建人员" DataField="SLR">
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="所属部门" DataField="SSBM">
                                <ItemStyle Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="状态" DataField="ZT">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
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
    <div id="EditDiv" runat="server" style="display: none; border: 2px solid #5b9ed1;
        position: absolute; z-index: inherit; width: 100%; height: 100%; top: 0px; left: 0px;">
        <table style="width: 100%; height: 100%" border="0px" cellpadding="0" cellspacing="0">
            <tr style="height: 29px" ondblclick="MaxWinodw(document.getElementById('XPMax'));">
                <td width="29px" style="background-image: url('../images/IE7.gif')">
                    <asp:Label ID="Label2" runat="server" Text="" Width="29px"></asp:Label>
                </td>
                <td style="background-image: url('../images/WindowXPHead.gif')" width="60%">
                    <asp:Label ID="LabelHead" runat="server" ForeColor="White" Font-Bold="True"></asp:Label>
                </td>
                <td style="background-image: url('../images/WindowXPHead.gif'); width: 40%" align="right"
                    valign="middle">
                    <img alt="" id="XPMin" src="../images/WindowXPMin.gif" border="0" title="最小化" onclick="MinWindow();"><img
                        alt="" src="../images/WindowXPMax.gif" border="0" id="XPMax" title="最大化" onclick="MaxWinodw();"><img
                            alt="" src="../images/WindowXPClose.gif" border="0" id="XPColse" title="关闭退出"
                            onclick="WindowClose();">
                </td>
            </tr>
            <tr id="EditTR">
                <td colspan="3">
                    <iframe id="EditPage" style="z-index: 1; visibility: inherit; width: 100%; height: 100%"
                        runat="server" name="BoneEditPage" frameborder="0" scrolling="no"></iframe>
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="Btn" runat="server" Text="Button" OnClick="Btn_Click" Style="display: none;" />
    </form>
</body>
</html>
