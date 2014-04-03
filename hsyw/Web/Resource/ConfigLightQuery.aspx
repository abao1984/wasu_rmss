<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigLightQuery.aspx.cs"
    Inherits="Web_Resource_ConfigLightQuery" %>

<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>

<%@ Register Assembly="IDP.WebControls" Namespace="IDP.WebControls" TagPrefix="idp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%;">
        <tr>
            <td class="tableHead">
                <asp:Button ID="BtnQuery" runat="server" Text="光缆配置检测" CssClass="btn_2k3" OnClick="BtnQuery_Click" />
            </td>
        </tr>
        <tr height="1px">
            <td class="tdBak" align="center">
                光缆资源
            </td>
        </tr>
        <tr height="100%">
            <td>
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="gvLightList" runat="server" SkinID="GridView1" DataKeyNames="YWGUID"
                        Width="100%" BorderColor="#5B9ED1" BorderWidth="1px" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" PageSize="20" >
                        <Columns>
                           
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
          <tr>
            <td height="1">
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
                                    
                                    <asp:ListItem Value="20">20</asp:ListItem>
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
                
                <%--<idp:Pager ID="Pager1" runat="server" OnPageIndexChanged="Pager1_PageIndexChanged" />--%>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="REGION_CODE" runat="server" Style="display: none"></asp:TextBox>
    <uc1:windowheader id="windowHeader1" runat="server" />
    </form>
</body>
</html>
