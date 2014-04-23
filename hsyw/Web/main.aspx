<%@ Page Language="C#" AutoEventWireup="true" CodeFile="main.aspx.cs" Inherits="Web_main" Theme="" StylesheetTheme=""  EnableTheming="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>main</title>
    <script src="/hsyw/jquery-ui/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="rmss_get_announcement.js" type="text/javascript"></script>
    <link href="rmss_announcement.css" type="text/css" rel="Stylesheet" />
    <META content="text/html; charset=gb2312" http-equiv=Content-Type>
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="Cache-Control" content="no-cache">
    <meta http-equiv="Expires" content="0">
    <link href="style.css" rel="stylesheet" type="text/css">

    <script src="Include/JavaScript/MdiWin.js"></script>
<script language="javascript" type="text/javascript" src="../config.js"></script>
    <style><!-- 
.wintitle{
	position:relative;
	left:-10px;
	z-index:0;
	background: url(Images/Main/tab1.gif);
	background-repeat: no-repeat;
	width:150px;
	height:25px;
	padding-left:15px;
	padding-top:7px;
	cursor:default;
}
.win1{
	width:100%; 
	height:100%; 
}
.win{
	position:absolute; 
	width:100%; 
	height:100%; 
	left: -2px; 
	top: 26px;
	
}
.close{
	border:1pt solid red;
	width:15px;
	height:15px;
	cursor:hand;
}
-->
</style>
</head>
<body style="margin-left: 0; margin-top: 0; margin-right: 0; overflow-y: auto" onload="init()">
<form id="form1" runat="server">
    <table height="100%" width="100%" border="0" cellspacing="0" cellpadding="0" onselectstart="return false"
        style="table-layout: fixed">
        <tr height="25" style="background-image:url(Images/Main/rig_bg.jpg);">
            <td valign="bottom" nowrap>
                <div style="overflow: hidden; width: 100%">
                    <div id="titlelist" style="margin-left: 0; z-index: -1">
                    </div>
                </div>
            </td>
            <td width="46">
                <img src="Images/Main/Refresh.gif" border="0" title="刷新" onclick="win.refreshwin(win.currentwin)"
                    align="middle" onmouseover="this.src='Images/Main/Refresh_over.gif'" onmousedown="this.src='Images/Main/Refresh_down.gif'"
                    onmouseout="this.src='Images/Main/Refresh.gif'" vspace="2"></td>
            <td width="24" style="color: #FFFFFF; cursor: hand;">
                <span onmousedown="tabScroll('left')" onmouseup="tabScrollStop()">
                    <img src="Images/Main/left.gif" border="0" title="向左" align="center" vspace="2"></span>
                <span onmousedown="tabScroll('right')" onmouseup="tabScrollStop()">
                    <img src="Images/Main/right.gif" border="0" title="向右" align="center" vspace="2"></td>
            <td width="20">
                <img src="Images/Main/close_all.gif" border="0" title="全部关闭" onclick="win.removeall()"
                    align="center" onmouseover="this.src='Images/Main/close_all_over.gif'" onmousedown="this.src='Images/Main/close_all_down.gif'"
                    onmouseout="this.src='Images/Main/close_all.gif'" vspace="2"></td>
            <td width="20">
                <img src="Images/Main/close.gif" border="0" title="关闭" onclick="win.removewin(win.currentwin)"
                    align="center" onmouseover="this.src='Images/Main/close_over.gif'" onmousedown="this.src='Images/Main/close_down.gif'"
                    onmouseout="this.src='Images/Main/close.gif'" vspace="2"></td>
        </tr>
        <tr>
            <td colspan="5" bgcolor="#0a6e94" style="height: 1px">
            </td>
            <tr>
        <tr>
            <td colspan="5">
                <table  height="100%" width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#ECE9D8">
                    <tr>
                        <td bgcolor="#f6fafd" id="mywindows" align="left" valign="top">
                            <img alt="" src="Images/Main/Default.jpg" />
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input type="hidden" id="user_id" value="<%=user_id %>" />
    </form>
</body>
</html>

