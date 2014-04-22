<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Announcement_edit.aspx.cs" Inherits="Announcement_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公告修改</title>
    <style type="text/css">
    .error{
        color :Red;
    }
    .info 
        {
    	color:Green;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="info">
    <% if (Request.HttpMethod.Equals("POST") && create_message > 0) { Response.Write("公告修改成功！"); } %>
        </div>
        
            <div>
            标题：<input type="text" name="post_title" value="<%=title %>" />
            </div>
        <div class="error">
    <% if (title.Length == 0 && Request.HttpMethod.Equals("POST")){ Response.Write(showErrorMessage("标题")); }  %>
        </div>
        <div>
        发布人：<input type="text" name="post_owner" value="<%=owner %>" />
        </div>
        <div class="error">
        <% if (owner.Length == 0 && Request.HttpMethod.Equals("POST")){ Response.Write(showErrorMessage("发布人")); }  %>
        </div>
        <div>
        内容：<textarea cols="20" rows="3" name="post_content"><%=content %></textarea>
        </div>
        <div class="error">
        <% if (content.Length == 0 && Request.HttpMethod.Equals("POST")){ Response.Write(showErrorMessage("内容")); }  %>
        </div>
        <div>
            <input type="hidden" name="post_id" value="<%=id %>" />
        </div>
        <div>
        <input type="submit" value="确定" name="post_submit" />
        </div>
    
    </div>
    </form>
</body>
</html>
