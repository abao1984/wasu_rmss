<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Admin_AdminLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ZLTextBox" Namespace="BaseText" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><% =Session["PageSubTite"].ToString().Trim() + " - " + Session["PageTitle"].ToString().Trim()%></title>
    
</head>
<base target="_top" /> 
<body style="font-size: 12px; font-family: 宋体; text-align: center; background-image:url(Images/login/PageBj.gif)">
    <form id="form1" runat="server">
   
    <div id="Layer1" style="text-align: center; vertical-align:middle; height:100%;width:100%">
        
            
        <br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>
    

        <table style="width: 560px; height:400px; border-collapse:collapse; background-image:url(Images/Login/loginBj.jpg)" 
            align="center">
                                
           	<tr>
		        <td rowspan="2" style=" width:300px"></td>
		        <td style=" height:120px"></td>
	        </tr>
	        <tr>
		        <td style="text-align:left">
		        
		                <asp:Label ID="Label1" runat="server" Height="18px" Text="用户名：" Font-Size="12px"></asp:Label>
                        <cc1:ZLTextBox ID="ZLTextBox_UserName" runat="server" InputType="varchars" MaxLength="100"
                        Width="150px"></cc1:ZLTextBox>
                                               
                                        
                                        <br />
                        <asp:Label ID="Label2"  runat="server" Height="18px" Text="密　码：" Font-Size="12px" ></asp:Label>
                        <cc1:ZLTextBox ID="ZLTextBox_UserPass" runat="server" InputType="varchars" MaxLength="100"
                        Width="150px" TextMode="Password"></cc1:ZLTextBox>
                                         <br />
                                         
                        <asp:Label ID="Label3" runat="server" Height="18px" Text="验证码：" Font-Size="12px" ></asp:Label>
                        <cc1:ZLTextBox ID="ZLTextBox_CheckCode" runat="server" InputType="varchars" MaxLength="100"
                        Width="60px" ></cc1:ZLTextBox>
                        <img src="SysCheckCode.aspx" id="IMG1" runat="server" />
                                        
                                         
                                           <br />
                                           <br />
                                           
                                           &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button_Post" CssClass="btn_2k3" runat="server" Text="登陆" onclick="Button_Post_Click"  />
                                        <input id="Button_Cancel" class="btn_2k3" type="button" onclick="window.close()" value="关闭窗口"  />
		        
		        
		        </td>
	        </tr>  
	        
	        <tr>
		        <td style=" height:120px">　</td>
	        </tr>
                   
                                
        </table>
    
    
            
        <br />
        

    </div>
    </form>
</body>
</html>
<%=strMsg%>