<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigCustomerList.aspx.cs"
    Inherits="Web_Resource_ConfigCustomerList" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>

<%@ Register Assembly="IDP.WebControls" Namespace="IDP.WebControls" TagPrefix="idp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="ResourceScript.js"></script>
    <script language="javascript" type="text/javascript" src="../../config.js"></script>
    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function OpenBranch(name, code) {

            windowOpenPageByWidth("BranchTree.aspx?NAME=" + name + "&CODE=" + code + "&ISQY=1", "选择所属区域", "", "30%", "40%", "10%", "80%");
            //alert("BranchTree.aspx?NAME=" + name + "&CODE=" + code + "&ISQY=1", "选择所属区域");
            window.event.returnValue = false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%;">
        <tr>
            <td class="tableHead">
                <asp:Button ID="BtnQuery" runat="server" Text="查询" CssClass="btn_2k3" 
                    onclick="BtnQuery_Click" />
            </td>
        </tr>
        <tr>
            <td height="1">
                <table style="width: 100%; border-collapse: collapse;" cellpadding="0" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td class="tdBak" align="center" width="12%">
                            区域
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="REGION" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenBranch('REGION','REGION_CODE')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdBak" align="center" width="12%">
                            业务编码
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="CUSTOMER_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center" width="12%">
                            客户等级
                        </td>
                        <td width="13%">
                            <asp:DropDownList ID="CUSTOMER_LEVEL" runat="server" Width="100%" AutoPostBack="True">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="1">1级</asp:ListItem>
                                <asp:ListItem Value="2">2级</asp:ListItem>
                                <asp:ListItem Value="3">3级</asp:ListItem>
                                <asp:ListItem Value="4">4级</asp:ListItem>
                                <asp:ListItem Value="5">5级</asp:ListItem>
                                <asp:ListItem Value="6">6级</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            客户名称
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="CUSTOMER_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            客户类型1
                        </td>
                        <td>
                            <asp:TextBox ID="CUSTTYPE1" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center">
                            客户类型
                        </td>
                        <td>
                            <asp:TextBox ID="CUSTTYPE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center">
                            业务名称
                        </td>
                        <td>
                            <asp:TextBox ID="SUB_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center">
                            业务联系人
                        </td>
                        <td>
                            <asp:TextBox ID="LINKMAN" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            客户地址
                        </td>
                        <td width="13%" colspan="3">
                            <asp:TextBox ID="ADDRESS" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td class="tdBak" colspan="4">
                        </td>
                        <%-- <td align="center" class="tdBak">
                            用户名称
                        </td>
                        <td>
                            <asp:TextBox ID="SUB_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>--%>
                    </tr>
                </table>
            </td>
        </tr>
        <%--<tr>
            <td class="tdBak" align="center">骨干业务</td>
        </tr>
       <tr>
            <td>
                 <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="gvBoneList" runat="server" SkinID="GridView1" DataKeyNames="YWGUID"
                        Width="100%" BorderColor="#5B9ED1" BorderWidth="1px" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" OnRowDataBound="gvBoneList_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="5%" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="YWBM" HeaderText="业务编码" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="YWLX" HeaderText="业务类型" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="LLMC" HeaderText="链路名称" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="WZXH" HeaderText="完整纤号" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SQRNAME" HeaderText="申请人" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SQSJ" DataFormatString="{0:yyyy-MM-dd}" HeaderText="申请时间">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="QDSJ" DataFormatString="{0:yyyy-MM-dd}" HeaderText="启用时间">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>--%>
        <tr height="1px">
            <td class="tdBak" align="center">
                光缆资源
            </td>
        </tr>
        <tr height="200px">
            <td>
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="gvLightList" runat="server" SkinID="GridView1" DataKeyNames="YWGUID"
                        Width="100%" BorderColor="#5B9ED1" BorderWidth="1px" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" OnRowDataBound="gvLightList_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="5%" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="SUBSCRIBER_CODE" HeaderText="业务编码" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="YWLX" HeaderText="业务类型" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CUSTTYPE" HeaderText="客户类别" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SUB_NAME" HeaderText="用户名称" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ADDRESS" HeaderText="用户地址" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JFMC" HeaderText="机房名称">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr height="1px">
            <td class="tdBak" align="center">
                传输业务
            </td>
        </tr>
        <tr height="200px" >
            <td>
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="gvCSYWList" runat="server" SkinID="GridView2" DataKeyNames="YWGUID"
                        BorderColor="#5B9ED1" BorderWidth="1px" AllowPaging="True" AllowSorting="True"
                        Width="130%" AutoGenerateColumns="False" OnRowDataBound="gvCSYWList_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="3%"
                                    Wrap="true" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" Width="4%" />
                            </asp:TemplateField>
                            <%-- <asp:BoundField DataField="BH" HeaderText="编号">
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="5%"
                                    Wrap="true" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" Width="5%" />
                            </asp:BoundField>     --%>
                            <asp:TemplateField HeaderText="编号">
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" Width="4%" />
                                <ItemTemplate>
                                    <%#(Container.DataItemIndex + 1) %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Wrap="False"
                                    Width="3%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ZWFS" HeaderText="组网方式" SortExpression="ZWFS">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="5%" />
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="5%"
                                    Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LLDK" HeaderText="链路带宽" SortExpression="LLDK">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="5%"
                                    Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JD_SUBSCRIBER_CODE" HeaderText="甲端用户编号" ItemStyle-BorderColor="#5b9ed1"
                                SortExpression="JD_SUBSCRIBER_CODE">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="8%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="8%"
                                    Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JD_SUB_NAME" HeaderText="甲端用户名称" ItemStyle-BorderColor="#5b9ed1"
                                SortExpression="甲端用户名称">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JD_LINKMAN" HeaderText="甲端联系人" SortExpression="甲端联系人">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="6%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="6%"
                                    Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JD_LINK" HeaderText="甲端联系方式">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="7%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="7%"
                                    Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="YD_SUBSCRIBER_CODE" HeaderText="乙端用户编号" ItemStyle-BorderColor="#5b9ed1"
                                SortExpression="YD_SUBSCRIBER_CODE">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="8%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="8%"
                                    Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JD_CUSTOMER_LEVEL" HeaderText="客户等级" SortExpression="JD_CUSTOMER_LEVEL">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="5%"
                                    Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="YD_LINKMAN" HeaderText="乙端联系人">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="6%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="6%"
                                    Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="乙端联系方式" DataField="YD_LINK">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="7%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="7%"
                                    Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="YD_SUB_NAME" HeaderText="乙端用户名称">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="15%"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="15%" Wrap="true"></ItemStyle>
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="VLANID" HeaderText="VLAN-ID" SortExpression="VLAN-ID">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="6%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="6%"
                                    Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="XGJL" HeaderText="修改记录">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PZRQ" HeaderText="配置日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}"
                                SortExpression="PZRQ">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="6%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="6%"
                                    Wrap="true"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr height="1px">
            <td class="tdBak" align="center">
                IP资源
            </td>
        </tr>
        <tr height="200px">
            <td>
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="gvLogicEquIp" runat="server" SkinID="GridView1" DataKeyNames="GUID,SUBSCRIBER_ID"
                        Width="100%" BorderColor="#5B9ED1" BorderWidth="1px" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" OnRowDataBound="gvLogicEquIp_RowDataBound">
                        <PagerSettings PageButtonCount="100" />
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="5%" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="SUBSCRIBER_CODE" HeaderText="业务编码" ItemStyle-BorderColor="#5b9ed1"
                                SortExpression="SUBSCRIBER_CODE">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SBPZXX" HeaderText="设备配置信息" ItemStyle-BorderColor="#5b9ed1"
                                SortExpression="SBPZXX">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JRDW" HeaderText="客户名称" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="YWLX" HeaderText="业务类型" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="14%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="REGION" HeaderText="所属区域" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="WLJF" HeaderText="机房名称">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="14%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PVCID" HeaderText="PVCID">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" HorizontalAlign="Center" Width="12%">
                                </ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="REGION_CODE" runat="server" Style="display: none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
