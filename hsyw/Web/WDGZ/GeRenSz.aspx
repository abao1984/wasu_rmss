<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GeRenSz.aspx.cs" Inherits="Web_WDGZ_GeRenSz" %>
<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
      
    </style>
    <script type="text/javascript">
        function PassWordEdit() {
            var url = "PassWordEdit.aspx";
            windowOpenPageByWidth(url, "密码修改","", "30%", "40%", "10%", "80%");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height:30px;">
        
    </div>
      <div style="height:15px;font-weight:bold;font-size:11pt;text-align:center">
        个人信息
    </div>
 <table width="80%" style="border-collapse: collapse;"cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
        <tr>
            <td align="right" width="20%" class="tdBak">用户账号</td>
            <td width="30%">
                <asp:TextBox ID="USERNAME" ReadOnly="true" runat="server" Width="100%"  BorderWidth="0"></asp:TextBox>
            </td>
            <td align="right" width="20%" class="tdBak">用户姓名</td>
            <td width="30%">
                <asp:TextBox ID="USERREALNAME" runat="server" Width="99%"  BorderWidth="0"></asp:TextBox>
            </td>
        </tr>
        
         <tr>
            <td align="right" class="tdBak">联系电话</td>
            <td>
                <asp:TextBox ID="USERPHONE" runat="server" Width="100%"  BorderWidth="0"></asp:TextBox>
             </td>
            <td align="right" class="tdBak">邮箱</td>
            <td>
                <asp:TextBox ID="MAILBOX" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                            Width="100%"  BorderWidth="0"></asp:TextBox>
             </td>
        </tr>
        
       
        <tr>
            <td align="right" class="tdBak">身份证号</td>
            <td align="right" colspan="3">
                <asp:TextBox ID="USERID" runat="server" Width="100%" Rows="3"  BorderWidth="0"></asp:TextBox>
                </td>
        </tr>
         <tr>
            <td align="center" colspan="4">
            <table width="100%">
                <tr>
                    <td align="right" width="50%"><asp:Button ID="BtnSave" runat="server" CssClass="btn_2k3" Text="保存" 
                    onclick="BtnSave_Click"  /></td>
                    <td align="left">
             
                        <input id="BtnEditPwd" Class="btn_2k3" value="修改密码" onclick="PassWordEdit();" type="button" value="button" /></td>
                </tr>
            </table>
                
                <%--<input id="Button1" type="button" value="关闭" class="btn_2k3" onclick="Close()" />--%></td>
        </tr>
    </table>
    <div style="display:none">
    </div>
        <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
