<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminPassSelf.aspx.cs" Inherits="Admin_AdminPassSelf" %>



<%@ Register Assembly="ZLTextBox" Namespace="BaseText" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register TagPrefix="foot" TagName="pageFooter"  Src="Include/Ascx/windowFooter.ascx"  %>
<%@ Register TagPrefix="header" TagName="pageHeader"  Src="Include/Ascx/windowHeader.ascx"  %>

<%@ Register src="Include/Ascx/DropBranch.ascx" tagname="DropBranch" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><% =Session["PageSubTite"].ToString().Trim() + " - " + Session["PageTitle"].ToString().Trim() + " - " + Session["CopyRightAuthor"].ToString().Trim()%></title>
     <script type="text/javascript" language="javascript" src="Include/JavaScript/Window.js" charset="gb2312"></script>
</head>
<header:pageHeader ID="PageHeader" runat="server" />
<body>
    <form id="form1" runat="server">
    <div>
    
    
    <div class="tableMain">
    <div class="tableSpaceBorder">
             <table>
             
               
                
                <tr>
                    <td colspan="2" class="tableCategory">
                    </td>
                </tr>
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label2"  runat="server" Font-Size="12px" Text="用户名："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left" >
                        
                        <cc1:ZLTextBox ID="ZLTextBox_AdminName" runat="server" InputType="varchars" MaxLength="100"
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" 
                        Height="26px" Width="240px"></cc1:ZLTextBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_AdminName" runat="server" Display="Dynamic"
                        ControlToValidate="ZLTextBox_AdminName"
                        ErrorMessage="*" ></asp:RequiredFieldValidator>
                        
                        <asp:RegularExpressionValidator id="RegularExpressionValidator_AdminName" runat="server" Display="Dynamic" 
                        ValidationExpression="^[A-Za-z0-9]+$" ErrorMessage="数字或字母！"
                        ControlToValidate="ZLTextBox_AdminName"></asp:RegularExpressionValidator>
                        
                        </td>
                </tr>
                
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label1"  runat="server" Font-Size="12px" Text="真实姓名："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left">
                        
                        <cc1:ZLTextBox ID="ZLTextBox_AdminRealName" runat="server" InputType="varchars" 
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" MaxLength="200"
                        Height="26px" Width="240px"></cc1:ZLTextBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_AdminRealName" runat="server" Display="Dynamic"
                        ControlToValidate="ZLTextBox_AdminRealName"
                        ErrorMessage="*" ></asp:RequiredFieldValidator>
                        
                        
                        </td>
                </tr>
                
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label3"  runat="server" Font-Size="12px" Text="老密码："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left" >
                        
                        <cc1:ZLTextBox ID="ZLTextBox_AdminPassOld" runat="server" InputType="varchars" MaxLength="100"
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" 
                        Height="26px" Width="240px" TextMode="Password"></cc1:ZLTextBox>
                        
                        
                        </td>
                </tr>
                
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label4"  runat="server" Font-Size="12px" Text="新密码："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left" >
                        
                        <cc1:ZLTextBox ID="ZLTextBox_AdminPass" runat="server" InputType="varchars" MaxLength="100"
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" 
                        Height="26px" Width="240px" TextMode="Password"></cc1:ZLTextBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_AdminPass" runat="server" Display="Dynamic"
                        ControlToValidate="ZLTextBox_AdminPass"
                        ErrorMessage="*" ></asp:RequiredFieldValidator>
                        
                        <asp:RegularExpressionValidator id="RegularExpressionValidator_AdminPass" runat="server" Display="Dynamic" 
                        ValidationExpression=".{1,15}$" ErrorMessage="1-15位字符！" ControlToValidate="ZLTextBox_AdminPass" />
                        
                        
                        </td>
                </tr>
                
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label5"  runat="server" Font-Size="12px" Text="再输入一次密码："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left" >
                        
                        <cc1:ZLTextBox ID="ZLTextBox_AdminPassAgain" runat="server" InputType="varchars" MaxLength="100"
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" 
                        Height="26px" Width="240px" TextMode="Password"></cc1:ZLTextBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                        ControlToValidate="ZLTextBox_AdminPassAgain"
                        ErrorMessage="*" ></asp:RequiredFieldValidator>
                        
                        <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" Display="Dynamic" 
                        ValidationExpression=".{1,15}$" ErrorMessage="1-15位字符！" ControlToValidate="ZLTextBox_AdminPassAgain" />
                        
                        <asp:CompareValidator ID="CompareValidator_AdminPassAgain" runat="server" ControlToCompare="ZLTextBox_AdminPass"
                            ControlToValidate="ZLTextBox_AdminPassAgain" ErrorMessage="密码两次输入不一致！"></asp:CompareValidator>
                        
                        </td>
                </tr>
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:80px; height:30px">
                        
                        </td>
                    <td class="tableBg2" align="left">
                        
                        
                        
                    </td>
                </tr>
                
            </table>
                                
            </div>
                
                <br />
                <br />
                <br />
                <asp:Button ID="Button_Update" CssClass="btn_2k3" runat="server" 
                                Text=" 提交 " onclick="Button_Update_Click"  />
                
                <input id="Button1" type="button" class="btn_2k3" value=" 关闭 " 
                                 onclick="WindowClose();" />
                
            </div>
    
        
    </div>
    </form>
</body>
<foot:pageFooter ID="pageFooter" runat="server" />
</html>
<%=Session["Msg"].ToString().Trim()%>