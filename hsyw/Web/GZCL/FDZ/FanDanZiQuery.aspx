<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FanDanZiQuery.aspx.cs" Inherits="Web_GZCL_FDZ_FanDanZiQuery" %>

<%@ Register Src="windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../../calendar.js" language="javascript" type="text/javascript"></script>

    <meta http-equiv="Expires" content="0">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Pragma" content="no-cache">

    <script type="text/javascript">
        function windowOpen(guid) {
            var per = document.getElementById("per").value;
            var url = "FanDanZiEdit.aspx?ZBGUID=" + guid;
            windowOpenPage(url, "故障单", "BtnJS");
            //window.event.returnValue = false;
        }
        function OpenBranch(name, code) {
            windowOpenPageByWidth("../../Resource/BranchTree.aspx?NAME=" + name + "&CODE=" + code + "&ISQY=1", "选择所属区域", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }

        function OpenChuLi() {
            var WLeft = Math.ceil((window.screen.width - 300) / 2);
            var WTop = Math.ceil((window.screen.height - 200) / 2);
            var url = "FanDanChuLi.aspx";
            var str = window.showModalDialog(url, 'title', 'scrollbars=no;resizable=no;help=no;status=no;dialogHeight=200px;dialogwidth=300px;dialogTop=' + WTop + ';dialogLeft=' + WLeft + ';');

            if (str != undefined) {
                //alert(str);
                document.getElementById('xggzzt').value = str;
                document.getElementById('btn').click();
                window.event.returnValue = false;
            }

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
                        <td class="tdBak" align="right" colspan="6">
                            <asp:TextBox ID="xggzzt" runat="server" Style="display: none"></asp:TextBox>
                            <asp:Button ID="BtnJS" runat="server" CssClass="btn_2k3" Text=" 检 索 " OnClick="BtnJS_Click" />
                            <asp:Button ID="BtnXG" runat="server" CssClass="btn_2k3" Text="修改状态" OnClientClick="OpenChuLi()"
                                Style="display: none" />
                            <asp:Button ID="BtnExp" runat="server" CssClass="btn_2k3" Text="导出Excel" 
                                onclick="BtnExp_Click" />    
                        </td>
                    </tr>
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
                    </tr>
                    <tr>
                        <td width="15%" class="tdBak" align="center">
                            时间
                        </td>
                        <td width="18%">
                            <asp:TextBox ID="TSSJ1" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                Width="100%"></asp:TextBox>
                        </td>
                        <td width="15%" class="tdBak" align="center" runat="server" id="time1">
                            至
                        </td>
                        <td width="18%" runat="server" id="time2">
                            <asp:TextBox ID="TSSJ2" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                Width="100%"></asp:TextBox>
                        </td>
                        <td width="15%" class="tdBak" align="center">
                            故障状态
                        </td>
                        <td width="18%">
                            <asp:DropDownList ID="dropZT" runat="server" Width="100%">
                                <asp:ListItem Text="全部" Value=""></asp:ListItem>
                                <asp:ListItem Text="电话受理" Value="电话受理"></asp:ListItem>
                                <asp:ListItem Text="调度发单" Value="调度发单"></asp:ListItem>
                                <asp:ListItem Text=" 维修返单 " Value=" 维修返单 "></asp:ListItem>
                                <asp:ListItem Text="遗单" Value="遗单"></asp:ListItem>
                                <asp:ListItem Text="留单" Value="留单"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%;">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        SkinID="GridView1" DataKeyNames="ZBGUID" 
                        OnRowDataBound="GridView1_RowDataBound" AllowSorting="True" 
                        AllowPaging="True" PageSize="20" onsorting="GridView1_Sorting">
                        <Columns>
                            <asp:TemplateField Visible="false">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="CheckAll" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ItemCheckBox" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="故障编号">
                                <ItemTemplate>
                                    <a href="javascript:windowOpen('<%# Eval("ZBGUID") %>');" style="text-decoration: underline;">
                                        <%# Eval("GZBH") %></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="投诉时间" DataField="TSSJ" DataFormatString="{0:yyyy-MM-dd HH:mm}" SortExpression="TSSJ">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="电话处理时间" DataField="DHSLSJ" DataFormatString="{0:yyyy-MM-dd HH:mm}" SortExpression="DHSLSJ">
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
                            <asp:BoundField HeaderText="客户等级" DataField="CUSTOMER_LEVEL" SortExpression="CUSTOMER_LEVEL">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障等级" DataField="GZDJ" SortExpression="GZDJ">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障名称" DataField="GZMC" SortExpression="GZMC">
                                <HeaderStyle />
                                <ItemStyle Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="地址" DataField="KHDZ" SortExpression="KHDZ">
                                <HeaderStyle />
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="联系人" DataField="LXRNAME" SortExpression="LXRNAME">
                                <HeaderStyle />
                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="处理人员" DataField="DDFDR" SortExpression="DDFDR">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="处理说明" DataField="FDYLSM">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障状态" DataField="FDZZT" SortExpression="FDZZT">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
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
    <asp:TextBox ID="per" runat="server" Style="display: none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    <asp:Button ID="btn" runat="server" Text="Button" OnClick="btn_Click1" Style="display: none" />
    </form>
</body>
</html>
