<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminUpdate.aspx.cs" Inherits="Admin_AdminUpdate" %>


<%@ Register Assembly="ZLTextBox" Namespace="BaseText" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register TagPrefix="foot" TagName="pageFooter"  Src="Include/Ascx/pageFooter.ascx"  %>
<%@ Register TagPrefix="header" TagName="pageHeader"  Src="Include/Ascx/pageHeader.ascx"  %>

<%@ Register src="Include/Ascx/DropBranch.ascx" tagname="DropBranch" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><% =Session["PageSubTite"].ToString().Trim() + " - " + Session["PageTitle"].ToString().Trim() + " - " + Session["CopyRightAuthor"].ToString().Trim()%></title>
</head>
<header:pageHeader ID="PageHeader" runat="server" />
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
                        <asp:Label ID="Label3"  runat="server" Font-Size="12px" Text="用户组："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left">
                        
                        <cc1:ZLTextBox ID="ZLTextBox_AdminGroup" runat="server" InputType="number" MaxLength="200"
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" 
                        Height="26px" Width="240px"></cc1:ZLTextBox>
                        
                        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_AdminGroup" runat="server" Display="Dynamic"
                        ControlToValidate="ZLTextBox_AdminGroup"
                        ErrorMessage="*" ></asp:RequiredFieldValidator>
                                     
                        </td>
                </tr>
                
                       
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label7"  runat="server" Font-Size="12px" Text="管理机构："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left">
                        
                        
                                     
                        <uc1:DropBranch ID="DropBranch1" runat="server" />
                        
                        
                                     
                        </td>
                </tr>
                                
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label8"  runat="server" Font-Size="12px" Text="显示顺序："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left" >
                        
                        <cc1:ZLTextBox ID="ZLTextBox_DisplayOrder" runat="server" InputType="varchars" MaxLength="100"
                        onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" 
                        Height="26px" Width="240px"></cc1:ZLTextBox>
                         
                        <asp:RegularExpressionValidator id="RegularExpressionValidator_DisplayOrder" runat="server" Display="Dynamic" 
                        ValidationExpression="^[A-Za-z0-9]+$" ErrorMessage="数字或字母！"
                        ControlToValidate="ZLTextBox_DisplayOrder"></asp:RegularExpressionValidator>
                        
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
                        
                        <asp:Button ID="Button_Update" CssClass="btn_2k3" runat="server" 
                            Text=" 提交 " onclick="Button_Update_Click"  />
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
<foot:pageFooter ID="pageFooter" runat="server" />
</html>
<%=Session["Msg"].ToString().Trim()%>