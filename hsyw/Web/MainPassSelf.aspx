<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainPassSelf.aspx.cs" Inherits="Web_MainPassSelf" %>
<%@ Register Assembly="ZLTextBox" Namespace="BaseText" TagPrefix="cc1" %>
<%@ Register src="Include/Ascx/DropBranch.ascx" tagname="DropBranch" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" language="javascript" src="Include/JavaScript/Window.js" charset="gb2312"></script>
</head>
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
                        
                        <cc1:ZLTextBox ID="ZLTextBox_UserName" runat="server" InputType="varchars" MaxLength="100"
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" 
                        Height="26px" Width="240px"></cc1:ZLTextBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_UserName" runat="server" Display="Dynamic"
                        ControlToValidate="ZLTextBox_UserName"
                        ErrorMessage="*" ></asp:RequiredFieldValidator>
                        
                        <asp:RegularExpressionValidator id="RegularExpressionValidator_UserName" runat="server" Display="Dynamic" 
                        ValidationExpression="^[A-Za-z0-9]+$" ErrorMessage="数字或字母！"
                        ControlToValidate="ZLTextBox_UserName"></asp:RegularExpressionValidator>
                        
                        </td>
                </tr>
                
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label1"  runat="server" Font-Size="12px" Text="真实姓名："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left">
                        
                        <cc1:ZLTextBox ID="ZLTextBox_UserRealName" runat="server" InputType="varchars" 
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" MaxLength="200"
                        Height="26px" Width="240px"></cc1:ZLTextBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_UserRealName" runat="server" Display="Dynamic"
                        ControlToValidate="ZLTextBox_UserRealName"
                        ErrorMessage="*" ></asp:RequiredFieldValidator>
                        
                        
                        </td>
                </tr>
                
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label3"  runat="server" Font-Size="12px" Text="老密码："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left" >
                        
                        <cc1:ZLTextBox ID="ZLTextBox_UserPassOld" runat="server" InputType="varchars" MaxLength="100"
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" 
                        Height="26px" Width="240px" TextMode="Password"></cc1:ZLTextBox>
                        
                        
                        </td>
                </tr>
                
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label4"  runat="server" Font-Size="12px" Text="新密码："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left" >
                        
                        <cc1:ZLTextBox ID="ZLTextBox_UserPass" runat="server" InputType="varchars" MaxLength="100"
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" 
                        Height="26px" Width="240px" TextMode="Password"></cc1:ZLTextBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_UserPass" runat="server" Display="Dynamic"
                        ControlToValidate="ZLTextBox_UserPass"
                        ErrorMessage="*" ></asp:RequiredFieldValidator>
                        
                        <asp:RegularExpressionValidator id="RegularExpressionValidator_UserPass" runat="server" Display="Dynamic" 
                        ValidationExpression=".{1,15}$" ErrorMessage="1-15位字符！" ControlToValidate="ZLTextBox_UserPass" />
                        
                        
                        </td>
                </tr>
                
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label5"  runat="server" Font-Size="12px" Text="再输入一次密码："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left" >
                        
                        <cc1:ZLTextBox ID="ZLTextBox_UserPassAgain" runat="server" InputType="varchars" MaxLength="100"
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" 
                        Height="26px" Width="240px" TextMode="Password"></cc1:ZLTextBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                        ControlToValidate="ZLTextBox_UserPassAgain"
                        ErrorMessage="*" ></asp:RequiredFieldValidator>
                        
                        <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" Display="Dynamic" 
                        ValidationExpression=".{1,15}$" ErrorMessage="1-15位字符！" ControlToValidate="ZLTextBox_UserPassAgain" />
                        
                        <asp:CompareValidator ID="CompareValidator_UserPassAgain" runat="server" ControlToCompare="ZLTextBox_UserPass"
                            ControlToValidate="ZLTextBox_UserPassAgain" ErrorMessage="密码两次输入不一致！"></asp:CompareValidator>
                        
                        </td>
                </tr>
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:80px; height:30px">
                        
                        <asp:Label ID="Label6" Font-Size="12px" runat="server" Text="联系电话："></asp:Label>
                        
                        </td>
                    <td class="tableBg2" align="left">
                        

                        <asp:TextBox ID="text_phone" runat="server" Width="240px"></asp:TextBox>
                        

                    </td>
                </tr>
                
            </table>
                                
            </div>
            
                <br />
                <br />
                <br />
                <asp:Button ID="Button_Update" CssClass="btn_2k3" runat="server" 
                            Text=" 提交 " onclick="Button_Update_Click"  />
                        
                
                <input id="Button2" type="button" class="btn_2k3" value=" 关闭 " 
                                 onclick="WindowClose();" />
            
            </div>
    
    
    </div>
    </form>
</body>
</html>
