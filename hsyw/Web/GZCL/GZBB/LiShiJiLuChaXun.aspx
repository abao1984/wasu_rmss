<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LiShiJiLuChaXun.aspx.cs"
    Inherits="Web_GZCL_GZBB_LiShiJiLuChaXun" %>

<%@ Register Src="../FDZ/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../../calendar.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function windowOpen(guid) {
            var url = "../GuZhangEdit.aspx?ZBGUID=" + guid;
            windowOpenPage(url, "故障单", "BtnJS");

            //window.event.returnValue = false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%; height: 100%;">
        <tr>
            <td height="1">
                <table style="border-collapse: collapse; width: 100%" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td width="12%" class="tdBak" align="center">
                            申告时间
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="TSSJ1" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                Width="140px"></asp:TextBox>
                        </td>
                        <td width="12%" class="tdBak" align="center" runat="server" id="time1">
                            至
                        </td>
                        <td width="13%" runat="server" id="time2">
                            <asp:TextBox ID="TSSJ2" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                Width="140px"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="left">
                            <asp:Button ID="Button1" runat="server" Text="生成报表" CssClass="btn_2k3" OnClick="Button1_Click" />
                            <asp:Button ID="Button2" runat="server" Text="导出Excel" CssClass="btn_2k3" OnClick="Button2_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            结束时间
                        </td>
                        <td>
                            <asp:TextBox ID="JDSJ1" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                Width="140px"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center" runat="server" id="Td1">
                            至
                        </td>
                        <td runat="server" id="Td2">
                            <asp:TextBox ID="JDSJ2" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                Width="140px"></asp:TextBox>
                        </td>
                        <td class="tdBak">
                            <asp:Button ID="Button3" runat="server" Text="切換" CssClass="btn_2k3" OnClick="Button3_Click"
                                Width="85px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak">
                            故障编号
                        </td>
                        <td class="tdBak" >
                            <asp:TextBox ID="txt_GZBH" runat="server" BorderStyle="None"
                                Width="140px"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak">
                            业务主体
                        </td>
                        <td class="tdBak" colspan="3">
                            <asp:DropDownList ID="dropYWZT" runat="server" Width="140px" AutoPostBack="True"
                                OnSelectedIndexChanged="dropYWZT_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            业务类型
                        </td>
                        <td colspan="4" class="tdBak">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="6">
                                    </asp:CheckBoxList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dropYWZT" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            内容
                        </td>
                        <td>
                            <asp:DropDownList ID="dropNR" runat="server" Width="140px" AutoPostBack="True">
                                <asp:ListItem>----请选择----</asp:ListItem>
                                <asp:ListItem Value="GZMC">故障名称</asp:ListItem>
                                <asp:ListItem Value="LXRNAME">联系人</asp:ListItem>
                                <asp:ListItem Value="LXDH">联系电话</asp:ListItem>
                                <asp:ListItem Value="KHDZ">单位地址</asp:ListItem>
                                <asp:ListItem Value="XFRY">维修人员</asp:ListItem>
                                <asp:ListItem Value="GZYY">故障原因</asp:ListItem>
                                <asp:ListItem Value="GZCLFF">处理方法</asp:ListItem>
                                <asp:ListItem Value="HYFL">行业分类</asp:ListItem>
                                <asp:ListItem Value="GZLY">故障来源</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="4" class="tdBak">
                            <asp:TextBox ID="txtNR" runat="server" Width="200px" BorderStyle="None"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%" valign="top">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="GridView1" runat="server" SkinID="GridView2" DataKeyNames="ZBGUID"
                        BorderColor="#5B9ED1" BorderWidth="1px" AllowPaging="True" AllowSorting="True"
                        Width="200%" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                        OnSorting="GridView1_Sorting" PageSize="50">
                        <Columns>
                            <asp:BoundField HeaderText="序" DataField="XH">
                                <ItemStyle Width="1%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GZBH" HeaderText="故障编号">
                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:GridView ID="GridView2" runat="server" Width="100%" AutoGenerateColumns="False"
                                        ShowHeader="false" Height="100%">
                                        <Columns>
                                            <asp:BoundField DataField="clsj" HeaderText="处理时间">
                                                <ItemStyle Width="20%" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="clcz" HeaderText="处理操作">
                                                <ItemStyle Width="18%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="clry" HeaderText="系统操作人">
                                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SJCLRY" HeaderText="处理人">
                                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="clsm" HeaderText="处理说明">
                                                <ItemStyle HorizontalAlign="Left" Width="22%" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0" border="1">
                                        <tr>
                                            <td colspan="4" align="center">
                                                记录
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="20%" align="center">
                                                时间
                                            </td>
                                            <td width="18%" align="center">
                                                事件
                                            </td>
                                            <td width="20%" align="center">
                                                系统操作人
                                            </td>
                                            <td width="20%" align="center">
                                                处理人
                                            </td>
                                            <td align="center" width="22%">
                                                处理说明
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemStyle Width="25%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="申告时间" DataField="gzsgsj" SortExpression="gzsgsj">
                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="电话处理时间" DataField="sddfdsj" SortExpression="sddfdsj">
                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="发单时间" DataField="fdsj" SortExpression="fdsj">
                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="修复时间" DataField="gzxfsj" SortExpression="gzxfsj">
                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="业务主体" DataField="YWZT" SortExpression="YWZT">
                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障名称" DataField="GZMC" SortExpression="GZMC">
                                <ItemStyle Width="6%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="联系电话" DataField="LXDH" SortExpression="LXDH">
                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="单位地址" DataField="KHDZ" SortExpression="KHDZ">
                                <ItemStyle Width="8%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="业务类别" DataField="YWLB" SortExpression="YWLB">
                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="行业分类" DataField="HYFL" SortExpression="HYFL">
                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障来源" DataField="GZLY" SortExpression="GZLY">
                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障处理结果" DataField="FDZZT" SortExpression="FDZZT">
                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障层次" DataField="GZCC" SortExpression="GZCC">
                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障类型" DataField="GZLX" SortExpression="GZLX">
                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障原因" DataField="GZYY" SortExpression="GZYY">
                                <ItemStyle Width="6%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="原因描述" DataField="ZJYY" SortExpression="ZJYY">
                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="处理方法" DataField="GZCLFF" SortExpression="GZCLFF">
                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="方法描述" DataField="GZFFMS" SortExpression="GZFFMS">
                                <ItemStyle Width="6%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="处理人" DataField="XFRY" SortExpression="XFRY">
                                <ItemStyle Width="4%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障描述" DataField="GZMS" SortExpression="GZMS">
                                <ItemStyle Width="6%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CUSTOMER_LEVEL" HeaderText="客户等级" ReadOnly="True" 
                                SortExpression="CUSTOMER_LEVEL" />
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td height="1" width="60%">
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
    </form>
</body>
</html>
