<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserPassUpdateAll.aspx.cs" Inherits="Web_sysUser_UserPassUpdateAll" %>


<%@ Register Assembly="ZLTextBox" Namespace="BaseText" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    
    <div class="tableMain">
    <div class="tableSpaceBorder">
             <table>
             
                <tr>
                    <td class="tableHead" colspan="2" align="left">
                    </td>
                </tr>
                
                <tr>
                    <td colspan="2" class="tableCategory">
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
                        
                        </td>
                    <td class="tableBg2" align="left">
                        
                        <asp:Button ID="Button_Update" CssClass="btn_2k3" runat="server" 
                            Text=" 提交 " onclick="Button_Update_Click"  />
                        
                    </td>
                </tr>
                
            </table>
                                
            </div>
            
            
            </div>
    
    
    </div>
    </form>
</body>
</html>
