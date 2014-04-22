<%@ Page EnableTheming="false" Language="C#" AutoEventWireup="true" CodeFile="Announcement_list.aspx.cs" Inherits="Announcement_list" Theme="" StylesheetTheme="" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公告列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <a href="announcement_add.aspx">添加新工告</a>
    </div>
    <div>
        <table>
        <%
            foreach (System.Collections.Generic.Dictionary<string,string> dict in data_list)
            {
               
                string s = String.Format("<tr><td><a href=\"announcement_delete.aspx?id={0}\">删除</a></td><td><a target=\"_blank\" href=\"Announcement_edit.aspx?id={0}\">{1}</a></td></tr>",dict["id"],dict["title"]);
                Response.Write(s);
                Response.Write("\n");
            }
        %>
        </table>
    </div>
    </form>
</body>
</html>
