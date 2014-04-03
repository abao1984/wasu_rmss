﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataInsert.aspx.cs" Inherits="Web_sysData_DataInsert" %>

<%@ Register Assembly="ZLTextBox" Namespace="BaseText" TagPrefix="cc1" %>
<%@ Register Src="Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script language="javascript" type="text/javascript">
        function OpenMenu(name, code) {
            windowOpenPageByWidth("MenuTree.aspx?NAME=" + name + "&CODE=" + code, "选择所属菜单", "", "30%", "40%", "10%", "80%");
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
                            <asp:Label ID="Label2" runat="server" Font-Size="12px" Text="字典代码："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <cc1:ZLTextBox ID="ZLTextBox_DataCode" runat="server" InputType="varchars" MaxLength="100"
                                onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" Height="26px"
                                Width="240px"></cc1:ZLTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DataCode" runat="server" Display="Dynamic"
                                ControlToValidate="ZLTextBox_DataCode" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_DataCode" runat="server"
                                Display="Dynamic" ValidationExpression="^[A-Za-z0-9]+$" ErrorMessage="数字或字母！"
                                ControlToValidate="ZLTextBox_DataCode"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label1" runat="server" Font-Size="12px" Text="字典名称："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <cc1:ZLTextBox ID="ZLTextBox_DataName" runat="server" InputType="varchars" onblurCssName="textBase"
                                onfocusCssName="textBaseFocus" CssClass="textBase" MaxLength="200" Height="26px"
                                Width="240px"></cc1:ZLTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DataName" runat="server" Display="Dynamic"
                                ControlToValidate="ZLTextBox_DataName" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label3" runat="server" Font-Size="12px" Text="字段代码："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <cc1:ZLTextBox ID="ZLTextBox_DataDm" runat="server" InputType="varchars" MaxLength="100"
                                onblurCssName="textBase" onfocusCssName="textBaseFocus" CssClass="textBase" Height="26px"
                                Width="240px"></cc1:ZLTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DataDm" runat="server" Display="Dynamic"
                                ControlToValidate="ZLTextBox_DataDm" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_DataDm" runat="server"
                                Display="Dynamic" ValidationExpression="^[A-Za-z0-9]+$" ErrorMessage="数字或字母！"
                                ControlToValidate="ZLTextBox_DataDm"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            <asp:Label ID="Label4" runat="server" Font-Size="12px" Text="字段名称："></asp:Label>
                        </td>
                        <td class="tableBg2" style="text-align: left">
                            <cc1:ZLTextBox ID="ZLTextBox_DataMc" runat="server" InputType="varchars" onblurCssName="textBase"
                                onfocusCssName="textBaseFocus" CssClass="textBase" MaxLength="200" Height="26px"
                                Width="240px"></cc1:ZLTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_DataMc" runat="server" Display="Dynamic"
                                ControlToValidate="ZLTextBox_DataMc" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                       <tr>
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            字段值：</td>
                        <td class="tableBg2" style="text-align: left">
                            <asp:TextBox ID="ZDZ" runat="server" Width="240px" Height="26px" CssClass="textBase"></asp:TextBox>
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
                        <td class="tableBg1" style="text-align: right; width: 180px; height: 30px">
                            所属菜单
                        </td>
                        <td class="tableBg2" align="left">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="MENU" runat="server" Width="240px"> </asp:TextBox>
                                        <asp:TextBox ID="MENU_CODE" runat="server" Width="240px" style="display:none;"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img alt="" src="../web/Images/Small/bb_table.gif" onclick="OpenMenu('MENU','MENU_CODE')"
                                            style="cursor: hand" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableBg1" style="text-align: right; width: 80px; height: 30px">
                        </td>
                        <td class="tableBg2" align="left">
                            <asp:Button ID="Button_Insert" CssClass="btn_2k3" runat="server" Text=" 增加 " OnClick="Button_Insert_Click" />
                            <asp:Button ID="Button_Cancel" CssClass="btn_2k3" runat="server" Text="返回" CausesValidation="False"
                                OnClick="Button_Cancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
        <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
