<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DaiBanGz.aspx.cs" Inherits="Web_WDGZ_DaiBanGz" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .div
        {
            margin-top:15px;	
            text-align:left;
        }
        .header table
        {
        	border-collapse: collapse;
        }
        .left
        {
        	width:100%;
        	height:100%;
        	background-image:url(../Images/wf-title-leftv2.jpg);
        	background-repeat:no-repeat;
        }
        .center
        {
             width:100%;
        	height:100%;
            background-image:url(../Images/wf-title-bgv2.jpg);
        	background-repeat:repeat-x;
        	line-height:25px; 
            overflow:hidden; 
            font-weight:bold;
            font-size:8pt;
            color:Black;
            font-style:宋体;
        }
        .right
        {
        	width:100%;
        	height:100%;
        	background-image:url(../Images/wf-title-rightv2.jpg);
        	background-repeat:no-repeat;
        }
    </style>
    
    <script type="text/javascript">
        function windowOpenEdit(guid, url, header) {
            windowOpenPage(url, header, "");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="div">
        <div class="header">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="25px" width="27px">
                        <div class="left">
                        </div>
                    </td>
                    <td>
                        <div class="center">
                        VPN更改配置【<asp:Literal ID="LitVPNGGPZ" runat="server"></asp:Literal>】条
                        </div>
                    </td>
                    <td width="12px">
                        <div class="right">
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <asp:GridView ID="VPNGGPZ" SkinID="GridView1"  runat="server" DataKeyNames="GUID,LCJRZT"
                        BorderWidth="1px" AllowPaging="True" AllowSorting="True" CellSpacing="1" 
                        AutoGenerateColumns="False" 
            onrowdatabound="VPNGGPZ_RowDataBound" >
        <Columns>
                            <asp:BoundField DataField="SQDBH" HeaderText="申请单编号" >
                            <HeaderStyle Width="10%" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SUBSCRIBER_CODE" HeaderText="业务编码" >
                            <HeaderStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SUB_NAME" HeaderText="业务名称" >
                            <HeaderStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="YHZYIP" HeaderText="用户自用IP" >
                            <HeaderStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="YHLYWD" HeaderText="用户路由网段" >
                            <HeaderStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="JFIP" HeaderText="局方IP" >
                            <HeaderStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="JRFS" HeaderText="接入方式" >                           
                            <HeaderStyle Width="15%" />
                            </asp:BoundField>
                        </Columns>
        </asp:GridView>
    </div>
    
    <div class="div">
        <div class="header">
             <div class="header">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="25px" width="27px">
                        <div class="left">
                        </div>
                    </td>
                    <td>
                        <div class="center">
                        IP出口优化申请单【<asp:Literal ID="LitIPDZCKYH" runat="server"></asp:Literal>】条
                        </div>
                    </td>
                    <td width="12px">
                        <div class="right">
                        </div>
                    </td>
                </tr>
            </table>
            
        </div>
        </div>
        <asp:GridView ID="IPDZCKYH" SkinID="GridView1"  runat="server" DataKeyNames="GUID,LCJRZT"
                        BorderWidth="1px" AllowPaging="True" AllowSorting="True" CellSpacing="1" 
                        AutoGenerateColumns="False" 
            onrowdatabound="IPDZCKYH_RowDataBound" >
      <Columns>
                            <asp:BoundField DataField="SQDBH" HeaderText="申请单编号">
                            <HeaderStyle Width="15%" />
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DWMC" HeaderText="单位名称" >
                            <HeaderStyle Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SQR" HeaderText="申请人" >
                            <HeaderStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="YHYY" HeaderText="优化原因" >                                                   
                            <HeaderStyle Width="50%" />
                            </asp:BoundField>
                        </Columns>
        </asp:GridView>
    </div>
    
    <div class="div">
        <div class="header">
              <div class="header">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="25px" width="27px">
                        <div class="left">
                        </div>
                    </td>
                    <td>
                        <div class="center">
                        新开VPN申请单【<asp:Literal ID="litXKVPN" runat="server"></asp:Literal>】条
                        </div>
                    </td>
                    <td width="12px">
                        <div class="right">
                        </div>
                    </td>
                </tr>
            </table>
            
        </div>
        </div>
        <asp:GridView ID="XKVPN" SkinID="GridView1"  runat="server" DataKeyNames="GUID,LCJRZT"
                        BorderWidth="1px" AllowPaging="True" AllowSorting="True" CellSpacing="1" 
                        AutoGenerateColumns="False" 
            onrowdatabound="XKVPN_RowDataBound" >
        <Columns>
                           <asp:BoundField DataField="SQDBH" HeaderText="申请单编号" />
                            <asp:BoundField DataField="SQBM" HeaderText="申请部门" />
                            <asp:BoundField DataField="BMFZR" HeaderText="部门负责人" />
                            <asp:BoundField DataField="SQRQ" HeaderText="申请日期" />
                            <asp:BoundField DataField="VPNMC" HeaderText="VPN名称" />         
                        </Columns>
        </asp:GridView>
    </div>
    
    <div class="div">
        <div class="header">
          <div class="header">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="25px" width="27px">
                        <div class="left">
                        </div>
                    </td>
                    <td>
                        <div class="center">
                        镜像监控申请单【<asp:Literal ID="LitWLJKSQD" runat="server"></asp:Literal>】条
                        </div>
                    </td>
                    <td width="12px">
                        <div class="right">
                        </div>
                    </td>
                </tr>
            </table>
            
        </div>
        </div>
        <asp:GridView ID="WLJKSQD" SkinID="GridView1"  runat="server" DataKeyNames="GUID,LCJRZT"
                        BorderWidth="1px" AllowPaging="True" AllowSorting="True" CellSpacing="1" 
                        AutoGenerateColumns="False" 
            onrowdatabound="WLJKSQD_RowDataBound" >
        <Columns>
            <asp:BoundField DataField="SQDBH" HeaderText="申请单编号" >
                            <HeaderStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SQDW" HeaderText="申请单位" >
                            <HeaderStyle Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SQRY" HeaderText="申请人员" >
                            <HeaderStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SQSY" HeaderText="申请事由" >                                               
                            <HeaderStyle Width="60%" />
                            </asp:BoundField>
        </Columns>
        </asp:GridView>
    </div>
    <div style="display:none">
    <asp:Button runat="server" ID="MenuButton" onclick="MenuButton_Click" />
    </div>
     <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
