<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuPrivate.aspx.cs" Inherits="MenuPrivate" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <style type="text/css">
        .style1
        {
            background: #f5fbff;
            font: 12px Tahoma, Verdana;
            padding-left: 6px;
            vertical-align: middle;
            border-bottom: 1px solid #f3f3f3;
            text-align: left;
            width: 180px;
        }
        .style2
        {
            background: #ffffff;
            padding-left: 6px;
            vertical-align: middle;
            font: 12px Tahoma, Verdana;
            border-bottom: 1px solid #f3f3f3;
            text-align: left;
        }
        .style3
        {
            background: #f5fbff;
            font: 12px Tahoma, Verdana;
            height: 9px;
            padding-left: 6px;
            vertical-align: middle;
            border-bottom: 1px solid #f3f3f3;
            text-align: left;
            width: 80px;
        }
        .style4
        {
            background: #ffffff;
            height: 9px;
            padding-left: 6px;
            vertical-align: middle;
            font: 12px Tahoma, Verdana;
            border-bottom: 1px solid #f3f3f3;
            text-align: left;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
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
                    <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                        <asp:Label ID="Label2" runat="server" Font-Size="12px" Text="代码："></asp:Label>
                    </td>
                    <td class="tableBg2" style="text-align: left">
                        <asp:TextBox ID="PCODE" runat="server" CssClass="textBase" 
                        Height="26px" Width="240px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                        <asp:Label ID="Label1" runat="server" Font-Size="12px" Text="名称："></asp:Label>
                    </td>
                    <td class="tableBg2" style="text-align: left">
                        <asp:TextBox ID="PNAME" runat="server" CssClass="textBase" 
                        Height="26px" Width="240px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_MenuName" runat="server" Display="Dynamic"
                            ControlToValidate="PNAME" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                        菜单名称：</td>
                    <td class="tableBg2" style="text-align: left">
                        <asp:TextBox ID="MENUNAME" runat="server" CssClass="textBase" 
                        Height="26px" Width="240px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                        序号：</td>
                    <td class="tableBg2" style="text-align: left">
                        <asp:TextBox ID="XH" runat="server" CssClass="textBase" 
                        Height="26px" Width="240px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            runat="server" ErrorMessage="*" ControlToValidate="XH"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                            ID="RegularExpressionValidator1" runat="server" ErrorMessage="整数！" 
                            ValidationExpression="^[0-9]+$" Display="Dynamic" ControlToValidate="XH"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                
                <tr>
                    <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                        <asp:Label ID="Label9" runat="server" Font-Size="12px" Text="是否使用："></asp:Label>
                    </td>
                    <td class="tableBg2" align="left">
                        <asp:RadioButtonList ID="ISUSE" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem Value="1">使用</asp:ListItem>
                            <asp:ListItem Value="0">停用</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="style1" style="text-align: right;">
                        <asp:Label ID="Label6" runat="server" Font-Size="12px" Text="显示还是隐藏："></asp:Label>
                    </td>
                    <td class="style2" align="left">
                        <asp:RadioButtonList ID="ISVISIBLE" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem Value="1">显示</asp:ListItem>
                            <asp:ListItem Value="0">隐藏</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="style3" style="text-align: right; ">
                    </td>
                    <td class="style4" align="left">
                        <asp:Button ID="BtnSave" CssClass="btn_2k3" runat="server" Text=" 保存 " onclick="BtnSave_Click" CausesValidation="true"
                             />
                        <asp:Button ID="Button_Cancel" CssClass="btn_2k3" runat="server" Text="返回" CausesValidation="False"
                            OnClick="Button_Cancel_Click" />
                    </td>
                </tr>
            </table>
            <asp:TextBox ID="GUID" runat="server" style="display:none"></asp:TextBox>
            <asp:TextBox ID="MENUCODE" runat="server" style="display:none"></asp:TextBox>
        </div>
    </div>
    </form>
</body>
</html>
