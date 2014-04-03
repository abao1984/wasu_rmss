<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainLeft3.aspx.cs" Inherits="MainLeft3" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>系统菜单</title>
    <link rev="stylesheet" media="all" href="Include/Css/Left.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../config.js"></script>
</head>
<body>
    <form id="form1" runat="server" >
    <table style="width:100%; height:100%" border="0" cellpadding="0" cellspacing="0">
        <tr style="height:1px;">       
            <td style="background-image: url(Images/Left/left_top.jpg); font-weight: bold; color: #FFFFFF;"
                height="26px" valign="middle" align="left">&nbsp;<img align="absMiddle" alt="" src="Images/Small/folders.gif" />&nbsp;<asp:Label 
                    ID="LeftTop" runat="server">资源管理</asp:Label>
            </td>            
        </tr>
        <tr style="height:100%">
            <td style="height:100%;" align="left" valign="top" >
                    <asp:TreeView ID="TreeView1" ShowLines="True" runat="server" ExpandDepth="0" LineImagesFolder="Images/TreeLineImages"
                        Style="margin-top: 0px; margin-left: 0px" Width="20px" BorderStyle="None" Target="fraMain">
                        <Nodes>
                            <asp:TreeNode Text="故障处理2" Value="故障处理2"  ImageUrl="Images/Small/notes.gif">
                                <asp:TreeNode Text="故障申告2" Value="IP资源管理" NavigateUrl="GZCL2/GuZhangShengGao.aspx?SJGGG=ca5c430a-e205-4f6b-be80-c22ef1e8a95f"></asp:TreeNode>
                                <asp:TreeNode Text="待办故障2" Value="客户资源管理" NavigateUrl="GZCL2/GuZhangList.aspx?Type=db&SJGGG=ca5c430a-e205-4f6b-be80-c22ef1e8a95f"></asp:TreeNode>
                                <asp:TreeNode Text="已办故障2" Value="机房资源" NavigateUrl="GZCL2/GuZhangList.aspx?Type=yb&SJGGG=ca5c430a-e205-4f6b-be80-c22ef1e8a95f"></asp:TreeNode>
                            </asp:TreeNode>
                        </Nodes>
                        <SelectedNodeStyle BackColor="#FFCCCC" />
                    </asp:TreeView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
