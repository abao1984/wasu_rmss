<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuZhangShengGao.aspx.cs"
    Inherits="Web_GZCL_GuZhangShengGao" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html>
<head runat="server" xmlns="http://www.w3.org/1999/xhtml">
    <title></title>
    <%--<script language="javascript">
        //直接创建
        var WLeft = Math.ceil((window.screen.width - 800) / 2);
        var WTop = Math.ceil((window.screen.height - 500) / 2);
        function OpenGZSG() {
            var url = "GuZhangEdit.aspx?SG=1";
            window.open(url, "", "scrollbars=yes,resizable=yes,width=800,height=500,top=" + WTop + ",left=" + WLeft);
        }
    </script>--%>

    <script language="javascript" type="text/javascript">
        function windowOpen(num, ywbm) {//ywbmid
            var url = "GuZhangEdit.aspx?SG=" + num + "&YWBM=" + ywbm;
            windowOpenPage(url, "故障申告", "");
            //window.event.returnValue = false;
        }
        function MinWindow() {
            document.getElementById("BoneEditTR").style.display = "none";
            document.getElementById("BoneEditDiv").style.height = "30px";
            document.getElementById("BoneEditDiv").style.top = document.body.offsetHeight - 30;
        }
        function MaxWinodw() {
            document.getElementById("BoneEditTR").style.display = "block";
            document.getElementById("BoneEditDiv").style.height = "100%";
            document.getElementById("BoneEditDiv").style.top = "0px";
        }
        function WindowClose() {
            document.getElementById("BoneEditDiv").style.display = "none";
            //document.getElementById("Btn").click();
        }
        function OpenBranch(name, code) {
            windowOpenPageByWidth("../Resource/BranchTree.aspx?NAME=" + name + "&CODE=" + code + "&ISQY=1", "选择所属区域", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;
        height: 100%" width="100%">
        <tr>
            <td class="tableHead" style="height: 31px;">
                <asp:Button ID="QueryBtn" runat="server" Text="客户查询" CssClass="btn_2k3" OnClick="QueryBtn_Click" />
                <asp:Button ID="BtnCJ" runat="server" Text="直接创建" CssClass="btn_2k3" OnClick="BtnCJ_Click" />
            </td>
        </tr>
        <tr>
            <td style="height: 31px;">
                <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                    bordercolor="#5b9ed1" width="100%">
                    <tr>
                        <td width="10%" class="tdBak" align="center">
                            业务编码
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="YWBM" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="10%" class="tdBak" align="center">
                            用户名称
                        </td>
                        <td width="10%">
                            <asp:TextBox ID="KHMC" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="10%" class="tdBak" align="center">
                            用户地址
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="KHDZ" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="10%" class="tdBak" align="center">
                            所属区域
                        </td>
                        <td >
                            <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                                bordercolor="#5b9ed1" width="100%">
                                <tr>
                                    <td width="90%">
                                        <asp:TextBox ID="KHQY" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenBranch('KHQY','KHQYID');" />
                                    </td>
                                </tr>
                            </table>
                            <asp:TextBox ID="KHQYID" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                        </td>
                    </tr>
                    <%--<tr>
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
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>IP业务</asp:ListItem>
                                <asp:ListItem>IDC业务</asp:ListItem>
                                <asp:ListItem>传输业务</asp:ListItem>
                                <asp:ListItem>其它</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2" class="tdBak">
                            &nbsp;
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%;">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" SkinID="GridView1"
                        OnRowDataBound="GridView1_RowDataBound" BorderWidth="1px" AllowPaging="True"
                        AutoGenerateColumns="False" AllowSorting="True" DataKeyNames="SUBSCRIBER_ID">
                        <Columns>
                            <%-- <asp:TemplateField HeaderText="选择">
                                <ItemStyle Width="4%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick='windowOpen("","<%# Eval("SUBSCRIBERNO") %>")' >创建</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField HeaderText="选择">
                                <HeaderStyle Width="4%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="所属区域" DataField="REGION">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="业务编号" DataField="subscriber_code">
                                <HeaderStyle Width="10%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="用户名称" DataField="sub_name">
                                <HeaderStyle Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="用户安装地址" DataField="ADDRESS">
                                <HeaderStyle Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="客户等级" DataField="CUSTOMER_LEVEL">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <%-- <asp:BoundField HeaderText="业务联系人" DataField="LINKMAN">
                                <HeaderStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="联系电话" DataField="MOBILE_NO">
                                <HeaderStyle Width="8%" />
                            </asp:BoundField>--%>
                            <asp:BoundField HeaderText="销售" DataField="SALE_NAME">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Center" />
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
                                    <asp:ListItem Value="20" Selected="True">20</asp:ListItem>
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
    <div id="BoneEditDiv" runat="server" style="display: none; border: 2px solid #5b9ed1;
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
            <tr id="BoneEditTR">
                <td colspan="3">
                    <iframe id="BoneEditPage" style="z-index: 1; visibility: inherit; width: 100%; height: 100%"
                        runat="server" name="BoneEditPage" frameborder="0" scrolling="no"></iframe>
                </td>
            </tr>
        </table>
    </div>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
