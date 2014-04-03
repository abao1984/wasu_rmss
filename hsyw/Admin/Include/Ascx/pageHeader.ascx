<%@ Control Language="C#" AutoEventWireup="true" CodeFile="pageHeader.ascx.cs" Inherits="pageHeader" %>
 <script type="text/javascript" language="javascript" src="Include/JavaScript/Window.js" charset="gb2312"></script>
    
<a name="top"></a>
<!-- 页头开始 -->
	<div id="menu-bar">
        <div id="LeftRight">
		    <div id="menu-welcome" style="text-align:left; vertical-align:middle">
			    
		    </div>
		    <div id="Right" style="vertical-align:middle">
		        
		        <div id="menu-bar-nobg">
		            <ul id="navlist">
			            <li style="width:150px;text-align:right;">
				            <span><% =Session["UserName"].ToString().Trim()%></span>[<% =Session["UserRealName"].ToString().Trim()%>]
				            [<% =Session["BranchName"].ToString().Trim()%>]
			            </li>
            			
			            <li class="headerMore"><a href="#" onclick="WindowOpen('AdminPassSelf.aspx', '640px', '400px')" >修改密码</a></li>
                        <li class="headerMore" style="width:40px"><a href="#" target="MainFrame">帮助</a></li>
                        <li class="headerMore" style="width:40px"><a href="#" onclick="WindowOpen('Temp.aspx', '640px', '500px')" >测试</a></li>
                        <li class="headerLast" style="width:40px"><a href="Logout.aspx" target="_top" >退出</a></li>
		            </ul>
	            </div>
		        
		    </div>
		 </div>
		 
		 <div id="LeftRight">
		    <div id="menu-welcome" style="text-align:left; vertical-align:middle">
			    我的位置 <a href="Default.aspx" target="_self">首页</a>
			    <% =  Session["PageNavigator"].ToString().Trim()%>
		    </div>
		    <div id="Right" style="vertical-align:middle">
		        <a href="#bottom">
                    <asp:Image ID="Image2" runat="server" ImageUrl="../../Images/Header/arrow_down.gif" /></a>
		    </div>
	     </div>
	</div>
    
<br />
<!-- 页头结束 -->
