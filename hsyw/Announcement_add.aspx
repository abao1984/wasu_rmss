<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Announcement_add.aspx.cs" Inherits="Announcement_add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公告添加</title>
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
    <div class="info">
    <% if (Request.HttpMethod.Equals("POST") && create_message > 0) { Response.Write("新建公告成功！"); } %>
    </div>
    <div>
    标题：<input type="text" name="post_title" />
    <br/>
    <div class="error">
    <% if (title.Length == 0 && Request.HttpMethod.Equals("POST")){ Response.Write(showErrorMessage("标题")); }  %>
    </div>
    发布人：<input type="text" name="post_owner" />
    <div class="error">
    <% if (owner.Length == 0 && Request.HttpMethod.Equals("POST")){ Response.Write(showErrorMessage("发布人")); }  %>
    </div>
    内容：<textarea cols="20" rows="3" name="post_content"></textarea>
        <div class="error">
    <% if (content.Length == 0 && Request.HttpMethod.Equals("POST")){ Response.Write(showErrorMessage("内容")); }  %>
    </div>
    <br/>
        <asp:Button ID="post_to_list" runat="server" Text="添加" 
            onclick="post_to_list_Click" />
    <asp:Button   name="post_to_new" Text="继续添加" id="post_to_new" runat="server" 
            onclick="post_to_new_Click" />
    
    </div>
    </form>
</body>
</html>
