<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZyPzList.aspx.cs" Inherits="Web_Resource_ZyPzList" %>
<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
        .right
        {
            width: 10px;
            height: 28px;
            background: url(../Images/wf-title-right.jpg) no-repeat;
        }
        .bg
        {
            height: 28px;
            background: url(../Images/wf-title-bg.jpg) repeat-x;
          
        }
        .left
        {
            width: 23px;
            height: 28px;
            background: url(../Images/wf-title-left.gif) no-repeat;
        }
        .content
        {
            margin-bottom:30px;	
            text-align:left;
        }
    </style>
    <script type="text/javascript">
         function windowOpen(url,title) {
          //windowOpenPage(url, title, "");
          //window.open(url);
          debugger;
          var width = 900;
	    var left = window.screen.availWidth / 2 - 450;
	    if (left < 0)
	    {
	        left = 0;
	    }
	    var height = window.screen.availHeight - 40;
	    window.open(url, title, 'left=' + left + 'px,top=0,width=' + width + 'px,height=' + height + 'px,resizable=1,status=0,toolbar=0,menubar=0,scrollbars=1,location=0');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <div class="header">
    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="border-collapse: collapse;">
        <tr>
            <td class="left"></td>
            <td class="bg">光缆资源配置</td>
            <td class="right"></td>
        </tr>
    </table>
    </div>
    <div class="content">
        <asp:GridView ID="GvGl" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" BorderColor="#5B9ED1" 
            BorderWidth="1px" DataKeyNames="YWGUID" 
            SkinID="GridView1" Width="100%" onrowdatabound="GvGl_RowDataBound">
            <Columns>
                <asp:BoundField DataField="SUBSCRIBER_CODE" HeaderText="业务编码" 
                    ItemStyle-BorderColor="#5b9ed1">
                    <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" />
                    <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="YWLX" HeaderText="业务类型" 
                    ItemStyle-BorderColor="#5b9ed1">
                    <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" />
                    <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="CUSTTYPE" HeaderText="客户类别" 
                    ItemStyle-BorderColor="#5b9ed1">
                    <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" />
                    <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%" />
                </asp:BoundField>
                <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="客户名称" 
                    ItemStyle-BorderColor="#5b9ed1">
                    <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" />
                    <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="ADDRESS" HeaderText="客户地址" 
                    ItemStyle-BorderColor="#5b9ed1">
                    <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" />
                    <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="JFMC" HeaderText="机房名称">
                    <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" />
                    <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    
    
       <div class="header">
    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="border-collapse: collapse;">
        <tr>
            <td class="left"></td>
            <td class="bg">传输业务配置</td>
            <td class="right"></td>
        </tr>
    </table>
    </div>
    <div class="content">
                    <asp:GridView ID="GvCs" runat="server" SkinID="GridView2" DataKeyNames="YWGUID"
                        BorderColor="#5B9ED1" BorderWidth="1px" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" onrowdatabound="GvCs_RowDataBound" >
                        <Columns>
                             <asp:TemplateField HeaderText="编号">
                        <ItemTemplate>
                            <%#(Container.DataItemIndex + 1) %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="False" />
                    </asp:TemplateField>
                     
                            <asp:BoundField DataField="REGION" HeaderText="所属区域">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%" Wrap="true"></ItemStyle>
                            </asp:BoundField>                            
                            <asp:BoundField DataField="ZWFS" HeaderText="组网方式">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="7%" />
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="7%" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LLDK" HeaderText="链路带宽">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="6%"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="6%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JD_SUBSCRIBER_CODE" HeaderText="甲端用户编号" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="9%"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="9%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JD_SUB_NAME" HeaderText="甲端用户名称" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="15%"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="15%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="YD_SUBSCRIBER_CODE" HeaderText="乙端用户编号" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="9%"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="9%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="YD_SUB_NAME" HeaderText="乙端用户名称">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="15%"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="15%" Wrap="true"></ItemStyle>
                            </asp:BoundField>                           
                            <asp:BoundField DataField="VLANID" HeaderText="VLAN-ID">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="9%"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="9%" Wrap="true"></ItemStyle>
                            </asp:BoundField>   
                            <asp:BoundField DataField="PZRQ" HeaderText="配置日期" HtmlEncode=false DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%" Wrap="true"></ItemStyle>
                            </asp:BoundField>                          
                        </Columns>
                    </asp:GridView>
    </div>
    
    
       <div class="header">
    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="border-collapse: collapse;">
        <tr>
            <td class="left"></td>
            <td class="bg">IP资源配置</td>
            <td class="right"></td>
        </tr>
    </table>
    </div>
    <div class="content">
                    <asp:GridView ID="GvIp" runat="server" SkinID="GridView1" DataKeyNames="GUID,SUBSCRIBER_ID"
                        Width="100%" BorderColor="#5B9ED1" BorderWidth="1px" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" onrowdatabound="GvIp_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="SUBSCRIBER_CODE" HeaderText="业务编码" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SBPZXX" HeaderText="设备配置信息" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="客户名称" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="YWLX" HeaderText="业务类型" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="REGION" HeaderText="所属区域" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="WLJF" HeaderText="机房名称">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
    </div>
    
    
    <div style="display:none">
        <asp:TextBox ID="TxtBh" runat="server"></asp:TextBox>
        <asp:TextBox ID="TxtYBh" runat="server"></asp:TextBox>
    </div>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
