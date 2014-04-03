<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuZhangList.aspx.cs" Inherits="Web_GZCL_GuZhangList" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html>
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
        //直接创建
        var WLeft = Math.ceil((window.screen.width - 800) / 2);
        var WTop = Math.ceil((window.screen.height - 500) / 2);
        function OpenGZ(ZBGUID) {
            var url = "GuZhangEdit.aspx?ZBGUID=" + ZBGUID;
            window.open(url, "", "scrollbars=yes,resizable=yes,width=800,height=500,top=" + WTop + ",left=" + WLeft);

        }
    </script>

    <script language="javascript" type="text/javascript">
        function windowOpen(guid, czqx) {

            var per = document.getElementById("per").value;
            var url = "GuZhangEdit.aspx?ZBGUID=" + guid + "&per=" + per;
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
            <td style="height: 1px;">
                <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                    bordercolor="#5b9ed1" width="100%">
                    <tr>
                         <td width="12%" class="tdBak" align="center">
                            投诉编号
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="GZBH" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="12%" class="tdBak" align="center">
                            故障名称
                        </td>
                        <td width="15% class="tdBak"">
                            <asp:TextBox ID="KHMC" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="12%" class="tdBak" align="center">
                            所属区域
                        </td>
                        <td width="18%" class="tdBak">
                            <table cellpadding="0" cellspacing="0" border="0" style="border-collapse: collapse;"
                                width="100%">
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
                        <td width="16%" class="tdBak">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server"  style="display:none" AutoPostBack="true"
                                RepeatDirection="Horizontal"  Width="100%"
                                onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                                <asp:ListItem Value="1">未读</asp:ListItem>
                                <asp:ListItem Value="0">已读</asp:ListItem>
                                <asp:ListItem  Selected="True">全部</asp:ListItem>
                            </asp:RadioButtonList>
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
                        SkinID="GridView1" DataKeyNames="ZBGUID" 
                        onrowdatabound="GridView1_RowDataBound" AllowPaging="True" PageSize="20">
                        <Columns>
                            <asp:TemplateField HeaderText="投诉编号">
                                <ItemTemplate>
                                    <a href="javascript:windowOpen('<%# Eval("ZBGUID") %>','<%# Eval("CZQX") %>');" style="text-decoration: underline;">
                                        <%# Eval("GZBH") == null ? Eval("GZMC") : Eval("GZBH")%></a>
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
                            <asp:BoundField HeaderText="拥有人">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="处理人">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="主送">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障时长(分)" >
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障状态" DataField="LCZT">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="状态">
                                <ItemTemplate>
                                    <asp:ImageButton ID="img_wd" runat="server" ToolTip="未读" ImageUrl="~/Web/Images/GZGL/wd.jpg" Width="18px" Height="18px" Visible="false" />
                                    <asp:ImageButton ID="img_yd" runat="server"  ToolTip="已读" ImageUrl="~/Web/Images/GZGL/yd.jpg" Width="16px" Height="16px" Visible="false" />
                                    <asp:ImageButton ID="img_sd" runat="server" ToolTip="锁定" ImageUrl="~/Web/Images/GZGL/lock.JPG" Width="18px" Height="18px" Visible="false" />
                                    <asp:ImageButton ID="img_zs" runat="server"  ToolTip="主送" ImageUrl="~/Web/Images/GZGL/zs.bmp" Width="16px" Height="16px" Visible="false" />
                                    <asp:ImageButton ID="img_cs" runat="server"  ToolTip="抄送" ImageUrl="~/Web/Images/GZGL/cs.gif" Width="16px" Height="16px" Visible="false" />
                                    <asp:ImageButton ID="img_sj" runat="server" ToolTip="升级" ImageUrl="~/Web/Images/GZGL/up.GIF" Width="16px" Height="16px" Visible="false" />
                                    <asp:ImageButton ID="img_yq" runat="server"  ToolTip="超时" ImageUrl="~/Web/Images/GZGL/YanQi.gif" Width="16px" Height="16px" Visible="false" />
                                </ItemTemplate>
                                <ItemStyle  Width="10%" HorizontalAlign="Center" />
                            </asp:TemplateField>
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
                            <font face="宋体">总共有<asp:Label ID="DataCountLab" runat="server" ForeColor="Red"></asp:Label></font><font
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
    <uc1:windowHeader ID="windowHeader2" runat="server" />
    </form>
</body>
</html>
