﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register TagPrefix="foot" TagName="pageFooter"  Src="Include/Ascx/pageFooter.ascx"  %>
<%@ Register TagPrefix="header" TagName="pageHeader"  Src="Include/Ascx/pageHeader.ascx"  %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><% =Session["PageSubTite"].ToString().Trim() + " - " + Session["PageTitle"].ToString().Trim() + " - " + Session["CopyRightAuthor"].ToString().Trim()%></title>
</head>
<header:pageHeader ID="PageHeader" runat="server" />
<body>
    <form id="form1" runat="server">
    <div>
             
    <div class="tablemain">
        
    </div>
        <asp:Label ID="Label1" runat="server" Text="后台管理"></asp:Label>
    
    </div>
    </form>
</body>
<foot:pageFooter ID="pageFooter" runat="server" />
</html>
<%=Session["Msg"].ToString().Trim()%>