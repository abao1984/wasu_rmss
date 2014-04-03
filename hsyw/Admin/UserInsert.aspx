<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInsert.aspx.cs" Inherits="Web_sysUser_UserInsert" %>


<%@ Register Assembly="ZLTextBox" Namespace="BaseText" TagPrefix="cc1" %>

<%@ Register src="Include/Ascx/DropBranch.ascx" tagname="DropBranch" tagprefix="uc1" %>

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
                        <asp:Label ID="Label4"  runat="server" Font-Size="12px" Text="密码："></asp:Label></td>
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
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label7"  runat="server" Font-Size="12px" Text="所在机构："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left">
                        
                        
                                     
                        <uc1:DropBranch ID="DropBranch1" runat="server" />
                                                 
                        </td>
                </tr>
                                
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label3"  runat="server" Font-Size="12px" Text="手机号码："></asp:Label></td>
                    <td class="tableBg2" align="left">
                        
                        <cc1:ZLTextBox ID="ZLTextBox_UserPhone" runat="server" InputType="varchars" 
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" MaxLength="200"
                        Height="26px" Width="240px"></cc1:ZLTextBox>
                        
                    </td>
                </tr>
                
                
                
                
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label8"  runat="server" Font-Size="12px" Text="EMAIL："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left" >
                        
                        <cc1:ZLTextBox ID="ZLTextBox_UserId" runat="server" InputType="varchars" MaxLength="100"
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" 
                        Height="26px" Width="240px" ></cc1:ZLTextBox>
                        
                        <asp:RegularExpressionValidator id="RegularExpressionValidator_UserId" 
                            runat="server" Display="Dynamic" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="EMAIL输入错误！" 
                            ControlToValidate="ZLTextBox_UserId" />
                        
                        
                        </td>
                </tr>
                
                
                
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label9"  runat="server" Font-Size="12px" Text="是否使用："></asp:Label></td>
                    <td class="tableBg2" align="left">
                        
                        <asp:RadioButtonList ID="RadioButtonList_IsUse" runat="server" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1">使用</asp:ListItem>
                            <asp:ListItem Value="0">停用</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                
                <tr style="display:none">
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label6"  runat="server" Font-Size="12px" Text="显示还是隐藏："></asp:Label></td>
                    <td class="tableBg2" align="left">
                        
                        <asp:RadioButtonList ID="RadioButtonList_IsVisible" runat="server" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1">显示</asp:ListItem>
                            <asp:ListItem Value="0">隐藏</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:80px; height:30px">
                        
                        </td>
                    <td class="tableBg2" align="left">
                        
                        <asp:Button ID="Button_Insert" CssClass="btn_2k3" runat="server" 
                            Text=" 提交 " onclick="Button_Insert_Click"  />
                        
                    </td>
                </tr>
                
            </table>
                                
            </div>
            
            
            </div>
    
    
    </div>
    </form>
</body>
</html>
