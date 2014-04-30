<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Announcement_add.aspx.cs" Inherits="Announcement_add" Theme="" StylesheetTheme="" EnableTheming="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="zh-cn">
<head runat="server">
    <title>公告添加</title>
<meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script src="../jquery-ui/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="../ckeditor/ckeditor.js"></script>
    <link rel="stylesheet" href="/hsyw/jquery-ui/jquery-ui.css" type="text/css" />
    <script src="/hsyw/jquery-ui/jquery-ui.js" type="text/javascript"></script>
	<script src="/hsyw/web/rmss_announcement_object_autocomplete.js" type="text/javascript" ></script>
<link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css">
<link rel="stylesheet" href="../bootstrap/css/bootstrap-theme.min.css">
<script src="../bootstrap/js/bootstrap.min.js"></script>
    
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
    
    <div class="info">
    <% if (Request.HttpMethod.Equals("POST") && create_message > 0) { Response.Write("新建公告成功！"); } %>
    </div>
    <div class="form-group">
    <label for="post_title" class="col-sm-2 control-label"> 标题：</label>
    <div class="col-sm-8">
    <input type="text" class="form-control" name="post_title" />
    <div class="error">
    <% if (title.Length == 0 && Request.HttpMethod.Equals("POST")){ Response.Write(showErrorMessage("标题")); }  %>
    </div>
    </div>
    </div>
    
    
    <div class="form-group">
    <label for="post_owner" class="col-sm-2 control-label">发布到：</label>
    <div class="col-sm-8"">
    <input type="text" class="form-control" name="post_owner" id="post_owner" />
    <input type="hidden" class="form-control" id="post_owner_ids" name="post_owner_ids" />
        <div class="error">
    <% if (owner.Length == 0 && Request.HttpMethod.Equals("POST")){ Response.Write(showErrorMessage("发布人")); }  %>
    </div>
    </div>
    </div>
    
    <div class="form-group">
    <label for="post_content" class="col-sm-2 control-label"> 内容：</label>
    <div class="col-sm-8">
   <textarea cols="5" rows="3" name="post_content" id="editor" class="form-control"></textarea>
           <div class="error">
        
    <% if (content.Length == 0 && Request.HttpMethod.Equals("POST")){ Response.Write(showErrorMessage("内容")); }  %>
    </div>
   </div>
    </div>
    <div class="form-group">
    <div class="col-sm-offset-2 col-sm-10">
              <asp:Button ID="post_to_list" runat="server" Text="添加" 
            onclick="post_to_list_Click" />
    <asp:Button   name="post_to_new" Text="继续添加" id="post_to_new" runat="server" 
            onclick="post_to_new_Click" />
    </div>
  </div>
 
    <script type="text/javascript">
                CKEDITOR.replace( 'editor');
    </script>
    </form>
    </div>
</body>
</html>
