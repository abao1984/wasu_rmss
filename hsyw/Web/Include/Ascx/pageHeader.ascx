<%@ Control Language="C#" AutoEventWireup="true" CodeFile="pageHeader.ascx.cs" Inherits="pageHeader" %>

<a name="top"></a>

<!-- 页头开始 -->
	<div id="menu-bar">
        <div id="LeftRight">
		    <div id="menu-welcome" style="text-align:left; vertical-align:middle">
			    我的位置 <a href="Default.aspx" target="_self">首页</a>
			    <% =  Session["PageNavigator"].ToString().Trim()%>
		    </div>
		    <div id="Right" style="vertical-align:middle">
		        <a href="#bottom">
                    <asp:Image ID="Image1" runat="server" ImageUrl="../../Images/Header/arrow_down.gif" /></a>
		    </div>
	    </div>
	</div>
    
<br />
<!-- 页头结束 -->
