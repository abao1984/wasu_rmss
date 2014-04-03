<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainLeft2.aspx.cs" Inherits="MainLeft2" %>

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
                            <asp:TreeNode Text="资源初始化" Value="资源初始化"  ImageUrl="Images/Small/notes.gif">
                                <asp:TreeNode Text="IP资源管理" Value="IP资源管理" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/IP_Souce_List.aspx?temp=&amp;MenuID=20060224103&amp;Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="客户资源管理" Value="客户资源管理" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/Customer_List.aspx?temp=&MenuID=20060616001&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="机房资源" Value="机房资源" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/MachineRoom_List.aspx?temp=&MenuID=20060224104&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="设备ip资源管理" Value="设备ip资源管理" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/SbIPzygl.aspx?temp=&MenuID=20090910100&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="VPN资源" Value="VPN资源" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/VPN_List.aspx?temp=&MenuID=20060224105&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="传输设备资源" Value="传输设备资源" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/Assembly_List.aspx?temp=&MenuID=20060224106&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="VLan资源" Value="VLan资源" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/Vlan_list.aspx?temp=&MenuID=20060820001&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="地址总体规划" Value="地址总体规划" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/Zy_dzztgh_list.aspx?temp=&MenuID=20060820002&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="地址详细规划" Value="地址详细规划" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/T_dzxxgh_list.aspx?temp=&MenuID=20060820003&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="机柜资源" Value="机柜资源" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/JG_List.aspx?temp=&MenuID=20060224107&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="网络设备资源" Value="网络设备资源" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/AssemblyPort_List.aspx?temp=&MenuID=20060224111&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="PVCID资源" Value="PVCID资源" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/PVCID.aspx?temp=&MenuID=20060820004&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="光缆资源" Value="光缆资源" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/Glzy_List.aspx?temp=&MenuID=20070315009&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="光设备资源" Value="光设备资源" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/Gsbzy_List.aspx?temp=&MenuID=20070329007&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="交接箱资源" Value="交接箱资源" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/Jjx_List.aspx?temp=&MenuID=20070816001&Pagetype=2"></asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Text="资源配置" Value="资源配置"  ImageUrl="Images/Small/notes.gif">
                                <asp:TreeNode Text="IP资源配置" Value="IP资源配置" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/IPZypz_List.aspx?temp=&MenuID=20060817001&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="ONU地址查询" Value="ONU地址查询" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/IPZypz_ONU.aspx?temp=&MenuID=20080318001&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="MAC地址ONU修改" Value="MAC地址ONU修改" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/IPZypz_List.aspx?MACEdit=Y&temp=&MenuID=20081201002&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="传输业务管理" Value="传输业务管理" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/TransfersBusiness_List.aspx?temp=&MenuID=20060224112&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="IP数据割接" Value="IP数据割接" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/ResourceReCombine/ResrouceReCombine_List.aspx?temp=&MenuID=20060831001&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="光缆资源配置" Value="光缆资源配置" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/GlZypz_List.aspx?temp=&zt=1&MenuID=20070424001&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="骨干业务管理" Value="骨干业务管理" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/Ggywgl_List.aspx?temp=&MenuID=20091130001&Pagetype=2"></asp:TreeNode>
                            </asp:TreeNode>
                            <asp:TreeNode Text="资源回收" Value="New Node"  ImageUrl="Images/Small/notes.gif">
                                <asp:TreeNode Text="IP资源回收" Value="IP资源回收" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/IP_Restore_List.aspx?temp=&jrfl=200607031714096562&MenuID=20060224116&Pagetype=2"></asp:TreeNode>
                                <asp:TreeNode Text="光缆资源回收" Value="光缆资源回收" NavigateUrl="http://125.210.208.56/hsywnew-sbgl/Resource/GlZypz_List.aspx?temp=&zt=2&MenuID=20070509001&Pagetype=2"></asp:TreeNode>
                            </asp:TreeNode>
<asp:TreeNode Text="故障查询" Value="New Node"  ImageUrl="Images/Small/notes.gif">
                                <asp:TreeNode Text="综合故障查询" Value="综合故障查询" NavigateUrl="http://125.210.208.56/hsywnew-sbgl//workflow/gzts/gzts_list.aspx?temp=&MenuID=20070223054&Pagetype=2"></asp:TreeNode>
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
