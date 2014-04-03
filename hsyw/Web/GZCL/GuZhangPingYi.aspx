<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuZhangPingYi.aspx.cs" Inherits="Web_GZCL_GuZhangPingYi" %>
<%@ Register Src="../../Admin/Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" src="../../config.js"></script>
    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
 
        function OpenBranch(name, code) {
            windowOpenPageByWidth("../Resource/BranchTree.aspx?NAME=" + name + "&CODE=" + code, "选择所属区域和部门", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }
        
    
        function windowOpen(guid) {
            //var per = document.getElementById("per").value;
            document.getElementById("BoneEditPage").src = "GuZhangEdit.aspx?ZBGUID=" + guid + "&per=py";
            document.getElementById("BoneEditDiv").style.display = "block";
            document.getElementById("LabelHead").innerText = "故障单";
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
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;
        height: 100%" width="100%">
        <tr>
            <td style="height: 31px;" class="tdHead">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Button ID="BtnSJ" runat="server" Text=" 查 询 " CssClass="btn_2k3" OnClick="BtnSJ_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdBak">
                <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                    bordercolor="#5b9ed1" width="100%">
                    <tr>
                        <td width="12%" class="tdBak" align="center">
                            投诉编号
                        </td>
                        <td width="12.5%">
                            <asp:TextBox ID="GZBH" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="12%" class="tdBak" align="center">
                            投诉时间
                        </td>
                        <td colspan="3">
                            <table width="100%" border="0" style="border-collapse: collapse;" bordercolor="#5b9ed1">
                                <tr>
                                    <td width="33.4%">
                                        <asp:TextBox ID="TSSJ1" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                            Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="34%" class="tdBak" align="center">
                                        至
                                    </td>
                                    <td width="33.3%">
                                        <asp:TextBox ID="TSSJ2" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                            Width="100%"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                         <td class="tdBak" align="center">
                            区域
                        </td>
                        <td width="12.5%">
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
                    </tr>
                    <tr>
                        <td width="12.5%" class="tdBak" align="center">
                            故障名称
                        </td>
                        <td width="12.5%">
                            <asp:TextBox ID="GZMC" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="12.5%" class="tdBak" align="center">
                            业务编号
                        </td>
                        <td width="12.5%">
                            <asp:TextBox ID="YWBH" runat="server" Width="100%" BorderStyle="None" MaxLength="10"></asp:TextBox>
                        </td>
                        <td width="12.5%" class="tdBak" align="center">
                            联系人
                        </td>
                        <td width="12.5%">
                            <asp:TextBox ID="LXRNAME" runat="server" Width="100%" BorderStyle="None" MaxLength="10"></asp:TextBox>
                        </td>
                        <td width="12.5%" class="tdBak" align="center">
                            联系电话
                        </td>
                        <td width="12.5%">
                            <asp:TextBox ID="LXDH" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="12%" class="tdBak" align="center">
                            联系地址
                        </td>
                        <td width="12.5%">
                            <asp:TextBox ID="KHDZ" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="12.5%" class="tdBak" align="center">
                            业务主体
                        </td>
                        <td>
                            <asp:DropDownList ID="YWZT" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td class="tdBak" align="center">
                            故障等级
                        </td>
                        <td width="12.5%">
                            <asp:DropDownList ID="GZDJ" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td width="12.5%" class="tdBak" align="center">
                            故障来源
                        </td>
                        <td>
                            <asp:DropDownList ID="GZLY" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                   <%-- <td width="12.5%" class="tdBak" align="center">拥有部门</td>
                    <td>
                    <table>
                    <tr>
                    <td style="border: solid 1px #5b9ed1;">
                                        <asp:TextBox ID="BRANCH" runat="server" BorderWidth="0" Width="213px"></asp:TextBox>
                                    </td>
                                    <td style="border: solid 1px #5b9ed1;">
                                        <img alt="" src="../../web/Images/Small/bb_table.gif" onclick="OpenBranch('BRANCH','BRANCHCODE')"
                                            style="cursor: hand" />
                                    </td>
                    </tr>
                    </table>
                    </td>--%>
                    <td width="12.5%" class="tdBak" align="center">业务类型</td>
                    <td>
                    <asp:DropDownList ID="ywlxDropDownList" runat="server" Width="100%">
                            </asp:DropDownList>
                    </td>
                    </tr>
                   <%-- <tr>
                        <td width="12%" class="tdBak" align="center">
                            故障状态
                        </td>
                        <td width="12.5%">
                            <asp:DropDownList ID="dropGZZT" runat="server" Width="100%">
                                <asp:ListItem Value="">---请选择---</asp:ListItem>
                                <asp:ListItem>处理中</asp:ListItem>
                                <asp:ListItem>结单</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="12.5%" class="tdBak" align="center">
                            创建人员
                        </td>
                        <td>
                            <asp:TextBox ID="GZCJRNAME" runat="server" Width="100%" BorderStyle="None" MaxLength="10"></asp:TextBox>
                        </td>
                      
                        <td width="12.5%" class="tdBak" align="center" colspan="4">
                        </td>
                       
                    </tr>--%>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%;">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        SkinID="GridView1" DataKeyNames="ZBGUID" AllowPaging="True" PageSize="20" 
                        onrowdatabound="GridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="投诉编号">
                                <ItemTemplate>
                                    <a href="javascript:windowOpen('<%# Eval("ZBGUID") %>');" style="text-decoration: underline;">
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
                            <asp:BoundField HeaderText="拥有人" >
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障时长(分)">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="评分" DataField="GZPF">
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
    <asp:TextBox ID="ID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="BRANCHCODE" runat="server" Style="display: none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
