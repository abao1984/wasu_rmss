<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Announcement_edit.aspx.cs" Inherits="Announcement_edit"  Theme="" StylesheetTheme="" EnableTheming="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="zh-cn">
<head runat="server">
    <title>公告修改</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script type="text/javascript" src="../ckeditor/ckeditor.js"></script>
<link rel="stylesheet" href="http://cdn.bootcss.com/twitter-bootstrap/3.0.3/css/bootstrap.min.css">
<link rel="stylesheet" href="http://cdn.bootcss.com/twitter-bootstrap/3.0.3/css/bootstrap-theme.min.css">
<script src="http://cdn.bootcss.com/jquery/1.10.2/jquery.min.js"></script>
<script src="http://cdn.bootcss.com/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
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
<div class="container">
    <form id="form1" runat="server" class="form-horizontal" role="form">
    
    <div class="form-group">
        <label for="post_title" class="col-sm-2 control-label"> 标题：</label>
    <div class="col-sm-8">
    <input type="text" class="form-control" name="post_title" value="<%=title %>" />
    <div class="error">
    <% if (title.Length == 0 && Request.HttpMethod.Equals("POST")){ Response.Write(showErrorMessage("标题")); }  %>
    </div>
    </div>
    </div>    
         
             <div class="form-group">
    <label for="post_owner" class="col-sm-2 control-label">发布人：</label>
    <div class="col-sm-8"">
    <input type="text" class="form-control" name="post_owner" value="<%=owner %>" />
        <div class="error">
    <% if (owner.Length == 0 && Request.HttpMethod.Equals("POST")){ Response.Write(showErrorMessage("发布人")); }  %>
    </div>
    </div>
    </div>
    
        <div class="form-group">
    <label for="post_content" class="col-sm-2 control-label"> 内容：</label>
    <div class="col-sm-8">
   <textarea cols="5" rows="3" name="post_content" id="editor" class="form-control"><%=content %></textarea>
           <div class="error">
        
    <% if (content.Length == 0 && Request.HttpMethod.Equals("POST")){ Response.Write(showErrorMessage("内容")); }  %>
    </div>
   </div>
    </div>
    


        <div>
            <input type="hidden" name="post_id" value="<%=id %>" />
        </div>
        <div class="form-group">
        <div class="col-sm-offset-2 col-sm-10">
        <input type="submit" value="确定" name="post_submit" />
        </div>
        </div>
        <script type="text/javascript">
                CKEDITOR.replace( 'editor');
    </script>
    </form>
    </div>
</body>
</html>
