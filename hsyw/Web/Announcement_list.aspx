<%@ Page EnableTheming="false" Language="C#" AutoEventWireup="true" CodeFile="Announcement_list.aspx.cs" Inherits="Announcement_list" Theme="" StylesheetTheme="" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="zh-cn">
<head runat="server">
    <title>公告列表</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
<link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css">
<link rel="stylesheet" href="../bootstrap/css/bootstrap-theme.min.css">
<script src="../jquery-ui/jquery-1.10.2.js"></script>
<script src="../bootstrap/js/bootstrap.min.js"></script>
</head>
<body>
<div class="container">
    <form id="form1" runat="server">
    <div>
    <a href="announcement_add.aspx" class="btn btn-default">添加新工告</a>
    </div>
    <div>
        <table class="table table-hover">
        <tr>
        <th>#</th>
        <th>公告标题</th>
        </tr>
        
        <%
            foreach (System.Collections.Generic.Dictionary<string,string> dict in data_list)
            {
               
                string s = String.Format("<tr><td class=\"col-sm-1\"><a href=\"announcement_delete.aspx?id={0}\">删除</a></td><td><a href=\"Announcement_edit.aspx?id={0}\">{1}</a></td></tr>",dict["id"],dict["title"]);
                Response.Write(s);
                Response.Write("\n");
            }
        %>
        </table>
    </div>
    </form>
    </div>
</body>
</html>
