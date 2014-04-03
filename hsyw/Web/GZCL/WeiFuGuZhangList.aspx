<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WeiFuGuZhangList.aspx.cs"
    Inherits="Web_GZCL_WeiFuGuZhangList" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
        function OpenBM(obj) {
            var ret = window.showModalDialog("../SelectBM.aspx?BCODES=" + document.getElementById("BCODE").value + "&IsDX=1", "", "dialogWidth:700px;dialogHeight:500px;center:yes;location:no;status:no;");
            if (typeof (ret) != "undefined") {
                obj.value = ret[0];
                document.getElementById("BCODE").value = ret[1];
            }
        }
        function OpenYYR(yyr) {
            var ret = window.showModalDialog("YongYouRenXinXi.aspx?YYR=" + yyr, "", "dialogWidth:700px;dialogHeight:500px;center:yes;location:no;status:no;");
        }
        function windowOpen(gzbh, czqx) {
            var url = "GuZhangEdit.aspx?ZBGUID=" + gzbh + "&per=" + czqx;
            windowOpenPage(url, "故障", "BtnQuery");
            //            window.event.returnValue = false;
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
            //            document.getElementById("Btn").click();
        }

        function OpenBranch(name, code) {
            windowOpenPageByWidth("../Resource/BranchTree.aspx?NAME=" + name + "&CODE=" + code +"&ISQY=1" , "选择所属区域", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }
        function OpenBranch1(name, code) {
            windowOpenPageByWidth("../Resource/BranchTree.aspx?NAME=" + name + "&CODE=" + code, "选择所属区域", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }
        function OpenUser(name, code, bmCode) {
            windowOpenPageByWidth("GZSZ/UserList.aspx?NAME=" + name + "&CODE=" + code + "&DX=1&khwhxs=1&Branch=" + bmCode, "选择用户", "", "10%", "80%", "10%", "80%");
            window.event.returnValue = false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
        <tr>
            <td style="height: 31px;" >
                
                <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                    bordercolor="#5b9ed1" width="100%">
                    <tr>
                         <td width="10%" class="tdBak" align="center">
                            投诉编号
                        </td>
                        <td width="15%">
                            <asp:TextBox ID="GZBH" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="10%" class="tdBak" align="center">
                            故障名称
                        </td>
                        <td width="15%" class="tdBak">
                            <asp:TextBox ID="KHMC" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td width="10%" class="tdBak" align="center">
                            所属区域
                        </td>
                        <td width="15%" class="tdBak">
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
                        <td width="10%" class="tdBak" align="center">
                            部门
                        </td>
                        <td width="15%" class="tdBak">
                            <table cellpadding="0" cellspacing="0" border="0" style="border-collapse: collapse;"
                                width="100%">
                                <tr>
                                    <td width="90%">
                                        <asp:TextBox ID="txtBM" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenBranch1('txtBM','txtBMCODE');" />
                                    </td>
                                </tr>
                            </table>
                            <asp:TextBox ID="txtBMCODE" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                        </td>
                        <td  class="tdBak" align="right">
                            <asp:Button ID="BtnQuery" runat="server" Text="检索" class="btn_2k3" Width="50" OnClick="BtnQuery_Click" />
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%;">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        SkinID="GridView1" DataKeyNames="ZBGUID" AllowPaging="True" 
                        onrowdatabound="GridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="投诉编号">
                                <ItemTemplate>
                                    <a href="javascript:windowOpen('<%# Eval("ZBGUID") %>','<%# Eval("CZQX") %>')" style="text-decoration: underline;">
                                        <%# Eval("GZBH") %></a>
                                </ItemTemplate>
                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="故障名称" DataField="GZMC"></asp:BoundField>
                            <asp:BoundField HeaderText="所属区域" DataField="KHQY">
                                <ItemStyle Width="14%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="投诉时间" DataField="TSSJ" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="业务主体" DataField="YWZT">
                                <ItemStyle Width="7%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="业务类型" DataField="YWLX">
                                <ItemStyle Width="7%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障层次" DataField="GZCC">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center"  Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障级别" DataField="GZDJ">
                                <ItemStyle Width="7%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="创建人" DataField="GZCJRNAME">
                                <ItemStyle Width="6%" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="拥有人" DataField="GZYYR">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center"  Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障时长(分)">
                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="故障状态" DataField="GZZT">
                                <ItemStyle Width="7%" HorizontalAlign="Center" />
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
    <asp:TextBox ID="BCODE" runat="server" Style="display: none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
