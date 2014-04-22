<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Announcement_delete.aspx.cs" Inherits="Announcement_delete" Theme="" StylesheetTheme="" EnableTheming="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公告删除</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h3>是否确定要删除公告？</h3>
    <div>是否确定要删除"<%=title %>" ？</div>
    <div>
    <input type="hidden" name="id" value="<%=id %>" />
    <asp:Button Text="确定" ID="buttonOK" runat="server" onclick="buttonOK_Click" />
    
        <asp:Button ID="Button1" runat="server" Text="取消" onclick="Button1_Click" /></div>
    </div>
    </form>
</body>
</html>
