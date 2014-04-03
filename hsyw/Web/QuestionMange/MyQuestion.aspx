<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyQuestion.aspx.cs" Inherits="Web_QuestionMange_MyQuestion" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
<style>
    .isshow
    {
        display:none;	
    }
</style>
    <script src="../../calendar.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function windowOpen(id, name,op) {
            windowOpenPage("QuestonEdit.aspx?id=" + id+"&op="+op, "问题--" + name, "QueryButton");
            window.event.returnValue = false;
        }
        function OpenBranch(name, code) {
            windowOpenPageByWidth("BranchTree.aspx?NAME=" + name + "&CODE=" + code, "选择负责部门", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="tableHead" style="height: 1px;">
                <input id="BtnNew" type="button" value="新增" onclick="windowOpen('','新增','XJ')" class="btn_2k3" runat="server"/>
                <asp:Button ID="QueryButton" runat="server" class="btn_2k3" style="display:none" Text="查询" OnClick="QueryButton_Click" />
                <asp:Button ID="BtnDel" runat="server" class="btn_2k3" Text="删除" OnClick="Button2_Click" OnClientClick="return confirm('确定要删除吗？');" />
            </td>
        </tr>
        <tr>
            <td valign="top" style="height: 1px;">
                <table style="border-collapse: collapse; width: 100%" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td align="center" class="tdBak" style="width: 12%">
                            问题名称
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="WTMC" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            问题来源</td>
                        <td style="width: 13%">
                           
                            <asp:DropDownList ID="WTLY" runat="server" Width="100%">
                            </asp:DropDownList>
                           
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            子系统类别
                        </td>
                        <td style="width: 13%">
                           
                            <asp:DropDownList ID="ZXTLB" runat="server" Width="100%">
                            </asp:DropDownList>
                           
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            问题优先级
                        </td>
                        <td style="width: 13%">
                           
                            <asp:DropDownList ID="WTYXJ" runat="server" Width="100%">
                            </asp:DropDownList>
                           
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            问题状态</td>
                        <td width="13%">
                           
                            <asp:DropDownList ID="WTZT" runat="server" Width="100%">
                            </asp:DropDownList>
                           
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            &nbsp;负责部门</td>
                        <td width="13%">
                             <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="FZBM" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenBranch('FZBM','FZBM_CODE')" />
                                    </td>
                                </tr>
                            </table></td>
                        <td align="center" class="tdBak" style="width: 12%">
                            完成时间
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="WCSJ1" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this)"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            至
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="WCSJ2" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this)"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" style="height: 100%;">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="GVQuestion" runat="server" SkinID="GridView1" DataKeyNames="ID"
                        BorderWidth="1px" AllowPaging="True" AllowSorting="True" CellSpacing="1" AutoGenerateColumns="False"
                        OnRowDataBound="GVQuestion_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="CheckAll" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ItemCheckBox" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:TemplateField>                           
                            <asp:BoundField DataField="WTMC" HeaderText="问题名称">
                                <ItemStyle HorizontalAlign="Center" Width="12%" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="问题来源" DataField="WTLY">
                                <ItemStyle HorizontalAlign="Center" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="负责部门" DataField="FZBM">
                                <ItemStyle HorizontalAlign="Center" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ZXTLB" HeaderText="子系统类别">
                                <ItemStyle HorizontalAlign="Center" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WTYXJ" HeaderText="优先级">
                                <ItemStyle HorizontalAlign="Center" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WTZT" HeaderText="状态">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WCSJ" HeaderText="完成时间" DataFormatString="{0:yyyy-MM-dd}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="是否已评审">
                                <HeaderTemplate>
                                    <span lang="zh-cn">是否已评审</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Image ID="ImgTp" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="详细">
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
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    <asp:TextBox ID="FZBM_CODE" runat="server" Style="display: none;"></asp:TextBox>
      <asp:TextBox ID="ZTXZ" runat="server">
    </asp:TextBox>
    </form>
</body>
</html>
