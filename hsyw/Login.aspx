<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Admin_AdminLogin" %>
<%@ Register Assembly="ZLTextBox" Namespace="BaseText" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <link   rel="shortcut icon" href="favicon.ico" type="image/x-icon" />

     <style type="text/css">
         .style1
         {
             width: 367px;
         }
         .style2
         {
             height: 179px;
         }
     </style>

</head>
<base target="_top" /> 
<body style="font-size: 12px; font-family: 宋体; text-align: center;">
    <form id="form1" runat="server">
    <table style="background-image:url(Web/Images/Login/login12.jpg);width:100%; height:100%;">
    <tr>
    <td>
        <table  style="width:930px; height:400px; border-collapse:collapse; background-image:url(Web/Images/Login/login11.jpg)" 
            align="center">   
           	<tr>
		        <td rowspan="2" class="style1" ></td>
		        <td class="style2" ></td>
	        </tr>
	        <tr>
		        <td style="text-align:left" valign="top" >		        
		                <asp:Label ID="Label1" runat="server" Height="18px" Text="用户名：" Font-Size="12px"></asp:Label>
                                        
                        <cc1:ZLTextBox ID="ZLTextBox_UserName" runat="server" InputType="varchars" MaxLength="100"
                        Width="150px"></cc1:ZLTextBox>
                                        <br />
                        <asp:Label ID="Label2"  runat="server" Height="18px" Text="密　码：" Font-Size="12px" ></asp:Label>
                        <cc1:ZLTextBox ID="ZLTextBox_UserPass" runat="server" InputType="varchars" MaxLength="100"
                        Width="150px" TextMode="Password"></cc1:ZLTextBox>
                                           <br />
                                           <br />
                                           &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button_Post" CssClass="btn_2k3" runat="server" Text="登陆" onclick="Button_Post_Click"  />
                                        <input id="Button_Cancel" class="btn_2k3" type="button" onclick="window.close()" value="关闭窗口"  />
		        </td>
	        </tr> 
	        <tr>
		        <td class="style1">　</td>
	        </tr>            
        </table>
        </td>
    </tr>
    </table>
    </form>
</body>
</html>
