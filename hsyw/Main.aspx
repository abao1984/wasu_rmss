<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>华数运维支撑系统</title>
       <script type="text/javascript" src="config.js"></script>
      
       <link   rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
           <script language="javascript" type="text/javascript">

               function SwitchSysBar() {
                   var imgSwitch = document.getElementById('imgSwitch');
                   var obj = document.getElementById('divleft');
                   var str = obj.style.display;
                   if (str == 'block') {
                       obj.style.display = 'none';
                       imgSwitch.src = "web/images/bar_left.jpg";
                       imgSwitch.title = "显示左边菜单";

                   }
                   else {
                       obj.style.display = 'block';
                       imgSwitch.src = "web/images/bar_right.jpg";
                       imgSwitch.title = "隐藏左边菜单";

                   };
               }
               function ChangeClass(id) {
                   document.getElementById("TOPID").value = id;
                   document.getElementById("Btn").click();
               }
    </script>
</head>
<body>
    <form id="form1" runat="server">
      <table cellpadding="0px" cellspacing="0px" border="0px" width="100%" height="100%">
        <tr>
            <td style="height: 70px">
                <iframe id="TopFrame" style="z-index: 2; visibility: inherit; width: 100%" name="TopFrame"
                    frameborder="0" marginwidth="0" marginheight="0" scrolling="no" height="100%"
                    src="web/MainTop.aspx"></iframe>
            </td>
        </tr>
        <tr>
            <td style="height: 100%">
                <table width="100%" style="height: 100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="160" valign="top">
                            <div id="divleft" style="display: block; width: 160px; height: 100%">
                                <iframe id="LeftFrame" style="z-index: 1; visibility: inherit; width: 160px" name="LeftFrame"
                                    src="Web/MainLeft.aspx?SJGGG=<% =Session["SJGGG"].ToString()%>" frameborder="0" marginwidth="0" marginheight="0" scrolling="no"
                                    height="100%"></iframe>
                            </div>
                        </td>
                        <td width="9"  style="background-color:Menu">
                            <img src="web/images/bar_right.jpg" title="隐藏右边菜单" onclick="SwitchSysBar();" id="imgSwitch" />
                        </td>
                        <td valign="top" width="100%">
                            <iframe id="fraMain" style="z-index: 2; visibility: inherit; width: 100%" name="fraMain"
                                frameborder="0" marginwidth="0" marginheight="0" scrolling="yes" height="100%"
                                src="Web/main.aspx"></iframe>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="display: none">
            <td valign="middle" align="center" class="foot" height="30px">
                <img alt="" id="footer" src="images/foot.jpg" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
