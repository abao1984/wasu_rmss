<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainMid.aspx.cs" Inherits="Web_MainMid" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="../config.js"></script>
    <script language="javascript" type="text/javascript">
        function SwitchSysBar() {
            var imgSwitch = document.getElementById('imgSwitch');
            var obj = parent.tFrame;
            var str = obj.cols;
            if (str == '170,9,*') {
                obj.cols = '0,9,*';
                imgSwitch.src = "images/bar_left.jpg";
                imgSwitch.title = "显示左边菜单";

            }
            else {
                obj.cols = '170,9,*';
                imgSwitch.src = "images/bar_right.jpg";
                imgSwitch.title = "隐藏左边菜单";

            };
        }
    </script>
</head>
<body style="margin:0px;">
    <form id="form1" runat="server">
         <table cellpadding="0" cellspacing="0" border="0" style="height:100%">
        <tr>
            <td valign="middle" align="left">
                <img alt="" src="Images/bar_right.jpg" title="隐藏右边菜单" onclick="SwitchSysBar();" id="imgSwitch" style="cursor:hand" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
