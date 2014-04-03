<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FanDanZiList.aspx.cs" Inherits="Web_GZCL_FDZ_FanDanZiList" %>

<%@ Register Src="windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html>
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
        function windowOpen(guid) {
            var per = document.getElementById("per").value;
            var url = "FanDanZiEdit.aspx?ZBGUID=" + guid + "&per=" + per;
            windowOpenPage(url, "故障单", "BtnJS");
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
        }
        function OpenBranch(name, code) {
            windowOpenPageByWidth("../../Resource/BranchTree.aspx?NAME=" + name + "&CODE=" + code + "&ISQY=1", "选择所属区域", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;
        height: 100%" width="100%">
        <tr>
            <td style="height: 1px;">
                <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                    bordercolor="#5b9ed1" width="100%">
                    <tr>
                        <td width="15%" class="tdBak" align="center">
                            故障名称
                        </td>
                        <td width="18%">
                            <asp:TextBox ID="KHMC" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="15%" class="tdBak" align="center">
                            故障编号
                        </td>
                        <td width="18%">
                            <asp:TextBox ID="TSBH" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="15%" class="tdBak" align="center">
                            所属区域
                        </td>
                        <td width="18%">
                            <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                                bordercolor="#5b9ed1" width="100%">
                                <tr>
                                    <td width="90%">
                                        <asp:TextBox ID="KHQY" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../../Images/Small/bb_table.gif" onclick="OpenBranch('KHQY','KHQYID');" />
                                    </td>
                                </tr>
                            </table>
                            <asp:TextBox ID="KHQYID" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="right">
                            &nbsp;<asp:Button ID="BtnJS" runat="server" CssClass="btn_2k3" Text=" 检 索 " OnClick="BtnJS_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%;">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        SkinID="GridView1" DataKeyNames="ZBGUID" OnRowDataBound="GridView1_RowDataBound"
                        AllowPaging="True" PageSize="50" AllowSorting="True" onsorting="GridView1_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="故障编号">
                                <ItemTemplate>
                                    <a href="javascript:windowOpen('<%# Eval("ZBGUID") %>');" style="text-decoration: underline;">
                                        <%# Eval("GZBH") %></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%"  />
                            </asp:TemplateField>
                            <%--<asp:BoundField HeaderText="所属区域" DataField="KHQY">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障名称" DataField="GZMC">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>--%>
                            <asp:BoundField HeaderText="投诉时间" DataField="TSSJ" DataFormatString="{0:yyyy-MM-dd HH:mm}" SortExpression="TSSJ">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="电话处理时间" DataField="DHSLSJ" DataFormatString="{0:yyyy-MM-dd HH:mm}" SortExpression="DHSLSJ">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="发单时间" DataField="DDFDSJ" DataFormatString="{0:yyyy-MM-dd HH:mm}" SortExpression="DDFDSJ">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="遗留时间" DataField="LDSJ" DataFormatString="{0:yyyy-MM-dd HH:mm}" SortExpression="LDSJ">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="业务主体" DataField="YWZT" SortExpression="YWZT">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="行业分类" DataField="HYFL" SortExpression="HYFL">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障名称" DataField="GZMC"  SortExpression="GZMC">
                                <HeaderStyle />
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障描述" DataField="GZMS" Visible="false" SortExpression="GSMS">
                                <HeaderStyle />
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="地址" DataField="KHDZ" SortExpression="KHDZ">
                                <HeaderStyle />
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="联系人" DataField="LXRNAME" SortExpression="LXRNAME">
                                <HeaderStyle />
                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="客户等级" DataField="CUSTOMER_LEVEL" SortExpression="CUSTOMER_LEVEL">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障等级" DataField="GZDJ" SortExpression="GZDJ">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="处理人员" DataField="DDFDR" SortExpression="DDFDR">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="锁定人员" DataField="SDRY" SortExpression="SDRY">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="处理说明" DataField="FDYLSM">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障时长(分)" DataField="GZSC" >
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="联系电话" DataField="LXDH" SortExpression="LXDH">
                                <HeaderStyle />
                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="处理人员" >
                                <HeaderStyle />
                                <ItemStyle Width="5%" HorizontalAlign="Center" />
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
    <div id="BoneEditDiv" runat="server" style="display: none; border: 2px solid #5b9ed1;
        position: absolute; z-index: inherit; width: 100%; height: 100%; top: 0px; left: 0px;">
        <table style="width: 100%; height: 100%" border="0px" cellpadding="0" cellspacing="0">
            <tr style="height: 29px" ondblclick="MaxWinodw(document.getElementById('XPMax'));">
                <td width="29px" style="background-image: url('../../images/IE7.gif')">
                    <asp:Label ID="Label2" runat="server" Text="" Width="29px"></asp:Label>
                </td>
                <td style="background-image: url('../../images/WindowXPHead.gif')" width="60%">
                    <asp:Label ID="LabelHead" runat="server" ForeColor="White" Font-Bold="True"></asp:Label>
                </td>
                <td style="background-image: url('../../images/WindowXPHead.gif'); width: 40%" align="right"
                    valign="middle">
                    <img alt="" id="XPMin" src="../../images/WindowXPMin.gif" border="0" title="最小化"
                        onclick="MinWindow();"><img alt="" src="../../images/WindowXPMax.gif" border="0"
                            id="XPMax" title="最大化" onclick="MaxWinodw();"><img alt="" src="../../images/WindowXPClose.gif"
                                border="0" id="XPColse" title="关闭退出" onclick="WindowClose();">
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
    <asp:TextBox ID="per" runat="server" Style="display: none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
