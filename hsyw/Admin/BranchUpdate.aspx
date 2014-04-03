<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchUpdate.aspx.cs" Inherits="Web_sysBranch_BranchUpdate" %>

<%@ Register Assembly="ZLTextBox" Namespace="BaseText" TagPrefix="cc1" %>
<%@ Register Src="Include/Ascx/DropData.ascx" TagName="DropData" TagPrefix="uc1" %>
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
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label2" runat="server" Font-Size="12px" Text="代码："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <cc1:ZLTextBox ID="ZLTextBox_BranchCode" runat="server" InputType="varchars" MaxLength="100"
                                onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" Height="26px"
                                Width="240px"></cc1:ZLTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_BranchCode" runat="server"
                                Display="Dynamic" ControlToValidate="ZLTextBox_BranchCode" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_BranchCode" runat="server"
                                Display="Dynamic" ValidationExpression="^[A-Za-z0-9]+$" ErrorMessage="数字或字母！"
                                ControlToValidate="ZLTextBox_BranchCode"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label1" runat="server" Font-Size="12px" Text="名称："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <cc1:ZLTextBox ID="ZLTextBox_BranchName" runat="server" InputType="varchars" onblurCssName="textBase"
                                onfocusCssName="textBaseFocus" CssClass="textBase" MaxLength="200" Height="26px"
                                Width="240px"></cc1:ZLTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_BranchName" runat="server"
                                Display="Dynamic" ControlToValidate="ZLTextBox_BranchName" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label3" runat="server" Font-Size="12px" Text="层数："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <cc1:ZLTextBox ID="ZLTextBox_BranchLevel" runat="server" InputType="number" MaxLength="200"
                                onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" Height="26px"
                                Width="240px"></cc1:ZLTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            路径：</td>
                        <td class="tableBg2" style="text-align: left">
                            <asp:TextBox ID="PATH" runat="server" MaxLength="200" CssClass="textBase" Height="26px" ReadOnly="true"
                                Width="240px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label4" runat="server" Font-Size="12px" Text="机构属性："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <uc1:DropData ID="DropData1" runat="server" />
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
                    <tr>
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
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            区域还是部门：
                        </td>
                        <td class="tableBg2" align="left">
                            <asp:RadioButtonList ID="RadioButtonList_IsQY" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="1">区域</asp:ListItem>
                                <asp:ListItem Value="0">部门</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 80px; height: 30px">
                        </td>
                        <td class="tableBg2" align="left">
                            <asp:Button ID="Button_Update" CssClass="btn_2k3" runat="server" Text="提交" OnClick="Button_Update_Click" />
                            <asp:Button ID="Button_Cancel" CssClass="btn_2k3" runat="server" Text="返回" CausesValidation="False"
                                OnClick="Button_Cancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
