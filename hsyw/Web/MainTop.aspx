<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainTop.aspx.cs" Inherits="MainTop" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <script type="text/javascript" language="javascript" src="Include/JavaScript/Window.js" charset="gb2312"></script>
 <script language="javascript" type="text/javascript" src="../config.js"></script>

    <style type="text/css">
        .MenuStyle
        {
            color: #315EB9;
            font-weight: bold;
        }
          .TopStyle
        {
            color: #ffffff;
            font-weight: bold;
        }
    </style>

</head>
<body style="margin:0px 0px -1px 0px;">
    <form id="form1" runat="server">
    <table style="width: 100%; height: 16px;" border="0" cellpadding="0" 
        cellspacing="0">
        <tr>
            <td style="background-image:url(Images/Header/top1.jpg); height: 70px; width: 420px;">
                &nbsp;
            </td>
            <td style="background-image:url(Images/Header/top2.jpg); padding-bottom: 4px;"  align="right" valign="bottom">
                <table  border="0" cellpadding="0" cellspacing="0">
                <tr>
                <td colspan="19" height="35" align="right" valign="top" style="padding-right: 15px">
                <img align="absMiddle" alt="" src="Images/Small/calendar.gif" /> 
                <asp:Label ID="LabelDate" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                <img align="absMiddle" alt="" src="Images/Small/mytopic.gif" /> 
                    <asp:Label ID="LabelUser" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
                    <img align="absMiddle" alt="" src="Images/Small/emailx.gif" /> 
                    <a href="#" onclick="WindowOpen('MainPassSelf.aspx', '640px', '400px')" class="TopStyle" >修改密码</a>
                    <img align="absMiddle" alt="" src="Images/Small/headtopic_1.gif" /> 
                    <a href="Logout.aspx" target="_top" class="TopStyle" >重新登录</a>
                </td>
                </tr>
                <tr>
                    <td  style="background-image:url(Images/Header/top3.jpg); width: 30px; height: 25px;"></td>
                    <td  style="background-image:url(Images/Header/top4.jpg); width: 58px; padding-bottom: 2px;" align="center" valign="bottom">
                       <a href="MainLeft.aspx?PMENUCODE=01&SJGGG=<% =Session["SJGGG"].ToString()%>" target="LeftFrame" class="MenuStyle" >我的工作</a>                       
                    </td>
                     <td  style="background-image:url(Images/Header/top4.jpg); width: 2px; height: 25px; padding-top: 4px;">
                        <img align="absMiddle" alt="" src="Images/Header/navspacer.gif" /> </td>
                         <td  style="background-image:url(Images/Header/top4.jpg); width: 58px; padding-bottom: 2px;" align="center" valign="bottom">
                       <a href="MainLeft.aspx?PMENUCODE=13&SJGGG=<% =Session["SJGGG"].ToString()%>" target="LeftFrame" class="MenuStyle" >资源管理</a>                       
                    </td>
                     <%--  <td  style="background-image:url(Images/Header/top4.jpg); width: 2px; height: 25px; padding-top: 4px;">
                        <img align="absMiddle" alt="" src="Images/Header/navspacer.gif" /> </td>
                         <td  style="background-image:url(Images/Header/top4.jpg); width: 65px; padding-bottom: 2px;" align="center" valign="bottom">
                       <a href="MainLeft2.aspx?PMENUCODE=02&SJGGG=<% =Session["SJGGG"].ToString()%>" target="LeftFrame" class="MenuStyle" >资源管理2</a>                       
                    </td>--%>
                     <td  style="background-image:url(Images/Header/top4.jpg); width: 2px; height: 25px; padding-top: 4px;">
                        <img align="absMiddle" alt="" src="Images/Header/navspacer.gif" /> </td>
                    <td  style="background-image:url(Images/Header/top4.jpg); width: 58px; padding-bottom: 2px;" align="center" valign="bottom">
                         <a href="MainLeft.aspx?PMENUCODE=03&SJGGG=<% =Session["SJGGG"].ToString()%>" target="LeftFrame" class="MenuStyle" >故障管理</a> 
                    </td>
                     <td  style="background-image:url(Images/Header/top4.jpg); width: 2px; height: 25px; padding-top: 4px;">
                        <img align="absMiddle" alt="" src="Images/Header/navspacer.gif" /> </td>
                         <td  style="background-image:url(Images/Header/top4.jpg); width: 58px; padding-bottom: 2px;" align="center" valign="bottom">
                         <a href="MainLeft.aspx?PMENUCODE=04&SJGGG=<% =Session["SJGGG"].ToString()%>" target="LeftFrame" class="MenuStyle" >流程管理</a> 
                    </td>
                     <td  style="background-image:url(Images/Header/top4.jpg); width: 2px; height: 25px; padding-top: 4px;">
                        <img align="absMiddle" alt="" src="Images/Header/navspacer.gif" /> </td>
                         <td  style="background-image:url(Images/Header/top4.jpg); width: 58px; padding-bottom: 2px;" align="center" valign="bottom">
                         <a href="MainLeft.aspx?PMENUCODE=05&SJGGG=<% =Session["SJGGG"].ToString()%>" target="LeftFrame" class="MenuStyle" >问题管理</a> 
                    </td>
                     <td  style="background-image:url(Images/Header/top4.jpg); width: 2px; height: 25px; padding-top: 4px;">
                        <img align="absMiddle" alt="" src="Images/Header/navspacer.gif" /> </td>
                         <td  style="background-image:url(Images/Header/top4.jpg); width: 58px; padding-bottom: 2px;" align="center" valign="bottom">
                         <a href="MainLeft.aspx?PMENUCODE=06&SJGGG=<% =Session["SJGGG"].ToString()%>" target="LeftFrame" class="MenuStyle" >信息管理</a> 
                    </td>
                     <td  style="background-image:url(Images/Header/top4.jpg); width: 2px; height: 25px; padding-top: 4px;">
                        <img align="absMiddle" alt="" src="Images/Header/navspacer.gif" /> </td>
                    <td  style="background-image:url(Images/Header/top4.jpg); width: 58px; padding-bottom: 2px;" align="center" valign="bottom">
                        <a href="MainLeft.aspx?PMENUCODE=10&SJGGG=<% =Session["SJGGG"].ToString()%>" target="LeftFrame" class="MenuStyle" >返单管理</a> 
                    </td>
                     <td  style="background-image:url(Images/Header/top4.jpg); width: 2px; height: 25px; padding-top: 4px;">
                        <img align="absMiddle" alt="" src="Images/Header/navspacer.gif" /> </td>
                         <td  style="background-image:url(Images/Header/top4.jpg); width: 58px; padding-bottom: 2px;" align="center" valign="bottom">
                         <a href="MainLeft.aspx?PMENUCODE=11&SJGGG=<% =Session["SJGGG"].ToString()%>" target="LeftFrame" class="MenuStyle" >统计分析</a> 
                    </td>
                     <td  style="background-image:url(Images/Header/top4.jpg); width: 2px; height: 25px; padding-top: 4px;">
                        <img align="absMiddle" alt="" src="Images/Header/navspacer.gif" /> </td>
                    <td  style="background-image:url(Images/Header/top4.jpg); width: 58px; padding-bottom: 2px;" align="center" valign="bottom">
                      <a href="MainLeft.aspx?PMENUCODE=12&SJGGG=<% =Session["SJGGG"].ToString()%>" target="LeftFrame" class="MenuStyle" >系统管理</a> 
                    </td>                   
                    <td  style="background-image:url(Images/Header/top5.jpg); width: 28px; height: 25px;"></td>
                </tr>
                </table>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>