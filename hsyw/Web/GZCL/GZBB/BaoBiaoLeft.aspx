<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaoBiaoLeft.aspx.cs" Inherits="Web_GZCL_GZBB_BaoBiaoLeft" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" height="100%" align="left" width="100%">
        <tr>
            <td width="140px" valign="top">
                <asp:TreeView ID="TreeView1" runat="server" Width="100%" 
                    onselectednodechanged="TreeView1_SelectedNodeChanged">
                    <SelectedNodeStyle ForeColor="Red" />
                    <Nodes>
                        <asp:TreeNode Text="今日统计" Value="今日统计"></asp:TreeNode>
                        <asp:TreeNode Text="处理方法报表" Value="处理方法报表"></asp:TreeNode>
                        <asp:TreeNode Text="故障类型报表" Value="故障类型报表"></asp:TreeNode>
                        <asp:TreeNode Text="故障来源报表" Value="故障来源报表"></asp:TreeNode>
                        <asp:TreeNode Text="历史数据统计" Value="历史数据统计"></asp:TreeNode>
                        <asp:TreeNode Text="历史记录查询" Value="历史记录查询"></asp:TreeNode>
                        <asp:TreeNode Text="业务类型报表" Value="业务类型报表"></asp:TreeNode>
                        <asp:TreeNode Text="故障数量统计" Value="故障数量统计"></asp:TreeNode>
                        <asp:TreeNode Text="行业分类报表" Value="行业分类报表"></asp:TreeNode>
                        <asp:TreeNode Text="故障统计明细" Value="故障统计明细"></asp:TreeNode>
                        <asp:TreeNode Text="留单业务统计" Value="留单业务统计"></asp:TreeNode>
                    </Nodes>
                </asp:TreeView>
            </td>
            <td valign="top"  style="height: 100%;">
                <div >
                    <iframe height="100%" id="iframes" runat="server"   scrolling="auto" width="100%" src=""></iframe>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
