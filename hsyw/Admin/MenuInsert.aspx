<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuInsert.aspx.cs" Inherits="Web_sysMenu_MenuInsert" %>

<%@ Register Assembly="ZLTextBox" Namespace="BaseText" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><% =Session["PageSubTite"].ToString().Trim() + " - " + Session["PageTitle"].ToString().Trim() + " - " + Session["CopyRightAuthor"].ToString().Trim()%></title>
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
                        <asp:Label ID="Label2"  runat="server" Font-Size="12px" Text="代码："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left" >
                        
                        <cc1:ZLTextBox ID="ZLTextBox_MenuCode" runat="server" InputType="varchars" MaxLength="100"
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" 
                        Height="26px" Width="240px" Enabled="false"></cc1:ZLTextBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_MenuCode" runat="server" Display="Dynamic"
                        ControlToValidate="ZLTextBox_MenuCode"
                        ErrorMessage="*" ></asp:RequiredFieldValidator>
                        
                        <asp:RegularExpressionValidator id="RegularExpressionValidator_MenuCode" runat="server" Display="Dynamic" 
                        ValidationExpression="^[A-Za-z0-9]+$" ErrorMessage="数字或字母！"
                        ControlToValidate="ZLTextBox_MenuCode"></asp:RegularExpressionValidator>
                        
                        </td>
                </tr>
                
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label1"  runat="server" Font-Size="12px" Text="名称："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left">
                        
                        <cc1:ZLTextBox ID="ZLTextBox_MenuName" runat="server" InputType="varchars" 
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" MaxLength="200"
                        Height="26px" Width="240px"></cc1:ZLTextBox>
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_MenuName" runat="server" Display="Dynamic"
                        ControlToValidate="ZLTextBox_MenuName"
                        ErrorMessage="*" ></asp:RequiredFieldValidator>
                        
                        
                        </td>
                </tr>
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label4"  runat="server" Font-Size="12px" Text="执行文件："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left">
                        
                        <cc1:ZLTextBox ID="ZLTextBox_FileName" runat="server" InputType="varchars" MaxLength="200"
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" 
                        Height="26px" Width="360px"></cc1:ZLTextBox>
                                               
                        </td>
                </tr>
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label5"  runat="server" Font-Size="12px" Text="图标："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left">
                        
                        <cc1:ZLTextBox ID="ZLTextBox_Ico" runat="server" InputType="varchars" MaxLength="200"
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" 
                        Height="26px" Width="240px"></cc1:ZLTextBox>
                        
                        </td>
                </tr>
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label7"  runat="server" Font-Size="12px" Text="是否展开："></asp:Label></td>
                    <td class="tableBg2" align="left">
                        
                        <asp:RadioButtonList ID="RadioButtonList_IsExpand" runat="server" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1">默认展开</asp:ListItem>
                            <asp:ListItem Value="0">默认关闭</asp:ListItem>
                        </asp:RadioButtonList>
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
                
                <tr>
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
                        <asp:Button ID="Button_Cancel" CssClass="btn_2k3" runat="server" 
                            Text="返回"  CausesValidation="False" onclick="Button_Cancel_Click" />
                        
                    </td>
                </tr>
                
            </table>
                                
            </div>
            </div>
    
    
    </div>
    </form>
</body>
</html>
