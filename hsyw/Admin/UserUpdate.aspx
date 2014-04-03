<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserUpdate.aspx.cs" Inherits="Web_sysUser_UserUpdate" %>

<%@ Register Assembly="ZLTextBox" Namespace="BaseText" TagPrefix="cc1" %>
<%@ Register Src="Include/Ascx/DropBranch.ascx" TagName="DropBranch" TagPrefix="uc1" %>
<%@ Register Src="Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script language="javascript" type="text/javascript">
        function OpenBranch(name, code) {
            windowOpenPageByWidth("../web/resource/BranchTree.aspx?NAME=" + name + "&CODE=" + code, "选择所属区域和部门", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }
        
    </script>

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
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label2" runat="server" Font-Size="12px" Text="用户名："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <cc1:ZLTextBox ID="ZLTextBox_UserName" runat="server" InputType="varchars" MaxLength="100"
                                onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" Height="26px"
                                Width="240px"></cc1:ZLTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_UserName" runat="server" Display="Dynamic"
                                ControlToValidate="ZLTextBox_UserName" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_UserName" runat="server"
                                Display="Dynamic" ValidationExpression="^[A-Za-z0-9]+$" ErrorMessage="数字或字母！"
                                ControlToValidate="ZLTextBox_UserName"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label1" runat="server" Font-Size="12px" Text="真实姓名："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <cc1:ZLTextBox ID="ZLTextBox_UserRealName" runat="server" InputType="varchars" onblurCssName="textBase"
                                onfocusCssName="textBaseFocus" CssClass="textBase" MaxLength="200" Height="26px"
                                Width="240px"></cc1:ZLTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_UserRealName" runat="server"
                                Display="Dynamic" ControlToValidate="ZLTextBox_UserRealName" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label4" runat="server" Font-Size="12px" Text="注册IP："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <cc1:ZLTextBox ID="ZLTextBox_UserRegIp" runat="server" InputType="varchars" onblurCssName="textBase"
                                onfocusCssName="textBaseFocus" CssClass="textBase" MaxLength="100" Height="26px"
                                Width="240px"></cc1:ZLTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label5" runat="server" Font-Size="12px" Text="注册日期："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <cc1:ZLTextBox ID="ZLTextBox_UserRegDate" runat="server" InputType="varchars" onblurCssName="textBase"
                                onfocusCssName="textBaseFocus" CssClass="textBase" MaxLength="100" Height="26px"
                                Width="240px"></cc1:ZLTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label7" runat="server" Font-Size="12px" Text="所在机构："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <table>
                                <tr>
                                    <td style="border: solid 1px #5b9ed1;">
                                        <asp:TextBox ID="BRANCH" runat="server" BorderWidth="0" Width="213px"></asp:TextBox>
                                    </td>
                                    <td style="border: solid 1px #5b9ed1;">
                                        <img alt="" src="../web/Images/Small/bb_table.gif" onclick="OpenBranch('BRANCH','BRANCHCODE')"
                                            style="cursor: hand" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label10" runat="server" Font-Size="12px" Text="上次登陆IP："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <cc1:ZLTextBox ID="ZLTextBox_UserLoginIp" runat="server" InputType="varchars" onblurCssName="textBase"
                                onfocusCssName="textBaseFocus" CssClass="textBase" MaxLength="100" Height="26px"
                                Width="240px"></cc1:ZLTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label11" runat="server" Font-Size="12px" Text="登陆时间："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <cc1:ZLTextBox ID="ZLTextBox_UserLoginDate" runat="server" InputType="varchars" onblurCssName="textBase"
                                onfocusCssName="textBaseFocus" CssClass="textBase" MaxLength="100" Height="26px"
                                Width="240px"></cc1:ZLTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label8" runat="server" Font-Size="12px" Text="显示顺序："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <cc1:ZLTextBox ID="ZLTextBox_DisplayOrder" runat="server" InputType="varchars" MaxLength="100"
                                onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" Height="26px"
                                Width="240px"></cc1:ZLTextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_DisplayOrder" runat="server"
                                Display="Dynamic" ValidationExpression="^[A-Za-z0-9]+$" ErrorMessage="数字或字母！"
                                ControlToValidate="ZLTextBox_DisplayOrder"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label3" runat="server" Font-Size="12px" Text="手机号码："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <cc1:ZLTextBox ID="ZLTextBox_UserPhone" runat="server" InputType="varchars" onblurCssName="textBase"
                                onfocusCssName="textBaseFocus" CssClass="textBase" MaxLength="100" Height="26px"
                                Width="240px"></cc1:ZLTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label12" runat="server" Font-Size="12px" Text="EMAIL："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <cc1:ZLTextBox ID="ZLTextBox_UserId" runat="server" InputType="varchars" MaxLength="100"
                                onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" Height="26px"
                                Width="240px"></cc1:ZLTextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_UserId" runat="server"
                                Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ErrorMessage="EMAIL输入错误！" ControlToValidate="ZLTextBox_UserId" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label9" runat="server" Font-Size="12px" Text="是否使用："></asp:Label>
                        </td>
                        <td class="tableBg2" align="left">
                            <asp:RadioButtonList ID="RadioButtonList_IsUse" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="1">使用</asp:ListItem>
                                <asp:ListItem Value="0">停用</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label6" runat="server" Font-Size="12px" Text="显示还是隐藏："></asp:Label>
                        </td>
                        <td class="tableBg2" align="left">
                            <asp:RadioButtonList ID="RadioButtonList_IsVisible" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="1">显示</asp:ListItem>
                                <asp:ListItem Value="0">隐藏</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 80px; height: 30px">
                        </td>
                        <td class="tableBg2" align="left">
                            <asp:Button ID="Button_Update" CssClass="btn_2k3" runat="server" Text=" 提交 " OnClick="Button_Update_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <asp:TextBox ID="ID" runat="server" Style="display: none;"></asp:TextBox>
    <asp:TextBox ID="BRANCHCODE" runat="server" Style="display: none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
