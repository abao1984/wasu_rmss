<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuZhangQuery.aspx.cs" Inherits="Web_GZCL_GuZhangQuery" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link rel="stylesheet" href="/hsyw/jquery-ui/jquery-ui.css" type="text/css" />
    <script src="/hsyw/jquery-ui/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="/hsyw/jquery-ui/jquery-ui.js" type="text/javascript"></script>
	<script src="rmss_autocomplete.js" type="text/javascript" />
   <%-- <style type="text/css">
    .ui-autocomplete
    {
        width:300px;
        text-align:left;	
    }
	.ui-menu-item{
		max-width:400px;
		text-align:left;
	}
    </style>--%>
    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>

    <title></title>

    <script language="javascript" type="text/javascript">
        function windowOpen(guid, czqx) {
            var per = document.getElementById("per").value;
            var url = "GuZhangEdit.aspx?ZBGUID=" + guid + "&per=" + per;
            windowOpenPage(url, "故障单", "BtnJS");
        }
        function OpenBranch(name, code) {
            windowOpenPageByWidth("../Resource/BranchTree.aspx?NAME=" + name + "&CODE=" + code + "&ISQY=1", "选择所属区域", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }

        function OpenBranchs(name, code) {
            windowOpenPageByWidth("../Resource/BranchTree.aspx?NAME=" + name + "&CODE=" + code, "选择所属部门", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }

        function OpenUser(name, code, bmCode) {
            var codes = document.getElementById(bmCode).value;
            windowOpenPageByWidth("GZSZ/UserList.aspx?NAME=" + name + "&CODE=" + code + "&BranchCode=" + codes, "选择用户", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }

        function OpenDate(name, code) {
            windowOpenPageByWidth("GZBB/XuanZheDaYingZiDuan.aspx?NAME=" + name + "&CODE=" + code, "选择打印字段", "BtnEXCEL", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;
        height: 100%" width="100%">
        <tr>
            <td style="height: 1px;">
                <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                    bordercolor="#5b9ed1" width="100%">
                    <tr>
                        <td width="12%" class="tdBak" align="center">
                            投诉编号
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="GZBH" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="12%" class="tdBak" align="center">
                            投诉时间
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="TSSJ1" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                Width="100%"></asp:TextBox>
                        </td>
                        <td width="12%" class="tdBak" align="center">
                            至
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="TSSJ2" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                Width="100%"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="right" colspan="2" width="25%">
                            <asp:Button ID="BtnJS" runat="server" CssClass="btn_2k3" Text=" 检 索 " OnClick="BtnJS_Click" />
                            <asp:Button ID="Button1" runat="server" Text="导出EXCEL" CssClass="btn_2k3" OnClientClick="OpenDate('excelName','excelCode')" />
                            <asp:Button ID="BtnEXCEL" runat="server" Text="" OnClick="BtnEXCEL_Click" Style="display: none" />
                            <asp:TextBox ID="excelName" runat="server" Style="display: none"></asp:TextBox>
                            <asp:TextBox ID="excelCode" runat="server" Style="display: none"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="12%" class="tdBak" align="center">
                            故障名称
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="GZMC" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="12%" class="tdBak" align="center">
                            业务编号
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="YWBH" runat="server" Width="100%" BorderStyle="None" MaxLength="10"></asp:TextBox>
                        </td>
                        <td width="12%" class="tdBak" align="center">
                            联系人
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="LXRNAME" runat="server" Width="100%" BorderStyle="None" MaxLength="10"></asp:TextBox>
                        </td>
                        <td width="12%" class="tdBak" align="center">
                            联系电话
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="LXDH" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            联系地址
                        </td>
                        <td>
                            <asp:TextBox ID="KHDZ" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center">
                            业务主体
                        </td>
                        <td>
                            <asp:DropDownList ID="YWZT" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="YWZT_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="tdBak" align="center">
                            业务类别
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="YWLB" runat="server" Width="100%" AutoPostBack="True">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="YWZT" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td class="tdBak" align="center">
                            故障等级
                        </td>
                        <td>
                            <asp:DropDownList ID="GZDJ" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            故障状态
                        </td>
                        <td>
                            <asp:DropDownList ID="dropGZZT" runat="server" Width="100%">
                                <asp:ListItem Value="">---请选择---</asp:ListItem>
                                <asp:ListItem>处理中</asp:ListItem>
                                <asp:ListItem>结单</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="tdBak" align="center">
                            区域
                        </td>
                        <td>
                            <table border="0" bordercolor="#5b9ed1" cellpadding="0" cellspacing="0" style="border-collapse: collapse;"
                                width="100%">
                                <tr>
                                    <td width="90%">
                                        <asp:TextBox ID="KHQY" runat="server" BorderStyle="None" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img onclick="OpenBranch('KHQY','KHQYID');" src="../Images/Small/bb_table.gif" />
                                    </td>
                                </tr>
                            </table>
                            <asp:TextBox ID="KHQYID" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center">
                            故障来源
                        </td>
                        <td>
                            <asp:DropDownList ID="GZLY" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td class="tdBak" align="center" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            创建部门
                        </td>
                        <td>
                            <table border="0" bordercolor="#5b9ed1" cellpadding="0" cellspacing="0" style="border-collapse: collapse;"
                                width="100%">
                                <tr>
                                    <td width="90%">
                                        <asp:TextBox ID="txtCJBM" runat="server" BorderStyle="None" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img onclick="OpenBranchs('txtCJBM','txtCJBMCODE');" src="../Images/Small/bb_table.gif" />
                                    </td>
                                </tr>
                            </table>
                            <asp:TextBox ID="txtCJBMCODE" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center">
                            创建人员
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" style="border-collapse: collapse;"
                                width="100%">
                                <tr>
                                    <td width="100%">
                                        <asp:TextBox ID="txtCJRY" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                                        <asp:TextBox ID="txtCJRYID" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                                    </td>
                                    <td width="1px">
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenUser('txtCJRY','txtCJRYID','txtCJBMCODE');" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdBak" align="center">
                            拥有部门
                        </td>
                        <td>
                            <table border="0" bordercolor="#5b9ed1" cellpadding="0" cellspacing="0" style="border-collapse: collapse;"
                                width="100%">
                                <tr>
                                    <td width="90%">
                                        <asp:TextBox ID="txtYRBM" runat="server" BorderStyle="None" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img onclick="OpenBranchs('txtYRBM','txtYRBMCODE');" src="../Images/Small/bb_table.gif" />
                                    </td>
                                </tr>
                            </table>
                            <asp:TextBox ID="txtYRBMCODE" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center">
                            拥有人员
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" style="border-collapse: collapse;"
                                width="100%">
                                <tr>
                                    <td width="100%">
                                        <asp:TextBox ID="txtYRR" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                                        <asp:TextBox ID="txtYRRID" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                                    </td>
                                    <td width="1px">
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenUser('txtYRR','txtYRRID','txtYRBMCODE');" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%;">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        SkinID="GridView1" DataKeyNames="ZBGUID" AllowPaging="True" OnRowDataBound="GridView1_RowDataBound"
                        PageSize="20">
                        <Columns>
                            <asp:TemplateField HeaderText="投诉编号">
                                <ItemTemplate>
                                    <a href="javascript:windowOpen('<%# Eval("ZBGUID") %>','<%# Eval("CZQX") %>');" style="text-decoration: underline;">
                                        <%# Eval("GZBH") %></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="所属区域" DataField="KHQY">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障名称" DataField="GZMC">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="投诉时间" DataField="TSSJ" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="业务主体" DataField="YWZT">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="业务类型" DataField="YWLB">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障层次" DataField="GZCC">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障级别" DataField="GZDJ">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="创建人" DataField="GZCJRNAME">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="拥有人" DataField="">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障时长(分)">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障状态" DataField="GZZT">
                                <HeaderStyle />
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
                                    <asp:Label ID="PageCountLab" runat="server" ForeColor="Red"></asp:Label>页</font>&nbsp;
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
    <asp:TextBox ID="per" runat="server" Text="query" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="TYPE" runat="server" Style="display: none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
