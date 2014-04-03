<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintSelect.aspx.cs" Inherits="Web_GZCL_PrintSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self">
  <meta http-equiv="expires" content="-1">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:CheckBoxList ID="Ch_Select" runat="server" RepeatColumns="5" 
            RepeatDirection="Horizontal">
            <asp:ListItem Value="a.LLMC" Selected="True">链路名称</asp:ListItem>
            <asp:ListItem Value="b.CUSTOMER_CODE">客户编码</asp:ListItem>
            <asp:ListItem Value="b.CUSTOMER_NAME">客户名称</asp:ListItem>
            <asp:ListItem Value="b.CUSTOMER_LEVEL">客户等级</asp:ListItem>
            <asp:ListItem Value="b.CUSTTYPE1">客户大类</asp:ListItem>
            <asp:ListItem Value="b.CUSTTYPE">客户类型</asp:ListItem>
            <asp:ListItem Value="b.REGION">区&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;域</asp:ListItem>
            <asp:ListItem Value="b.SUBSCRIBER_CODE" Selected="True">用户编码</asp:ListItem>
            <asp:ListItem Value="b.SUB_NAME" Selected="True">用户名称</asp:ListItem>
            <asp:ListItem Value="b.MOBILE_NO">手机号码</asp:ListItem>
            <asp:ListItem Value="b.PHONE_NO">电话号码</asp:ListItem>
            <asp:ListItem Value="b.FAX_NO">传真号码</asp:ListItem>
            <asp:ListItem Value="b.ZIP_CODE">邮&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;编</asp:ListItem>
            <asp:ListItem Value="b.SALE_NAME">销&nbsp;&nbsp;售&nbsp;&nbsp;员</asp:ListItem>
            <asp:ListItem Value="b.CREATTIME">创建时间</asp:ListItem>
            <asp:ListItem Value="b.PARTNER_NAME">代&nbsp;&nbsp;理&nbsp;&nbsp;商</asp:ListItem>
            <asp:ListItem Value="b.ADDRESS">用户地址</asp:ListItem>           
            <asp:ListItem Value="b.EMAIL">EMAIL&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
            <asp:ListItem Value="b.LINKMAN">业务联系人</asp:ListItem>
        </asp:CheckBoxList>
    
        <asp:Button ID="AllButton" runat="server" CssClass="btn_2k3" 
            onclick="AllButton_Click" Text="全选" />
        <asp:Button ID="Button1" runat="server" CssClass="btn_2k3" 
            onclick="Button1_Click" Text="全部不选" />
    
        <asp:Button ID="ExpButton" runat="server" onclick="ExpButton_Click" 
            Text="确定导出" CssClass="btn_2k3" />
        <asp:TextBox ID="ZBGUID" runat="server" style="display:none"></asp:TextBox>
    </div>
    </form>
</body>
</html>
