<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhyResourceExp.aspx.cs" Inherits="Web_Resource_PhyResourceExp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%; height: 100%;">
            <tr>
                <td valign="top">
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="3" 
                        RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td style="height:1px" align="center">
                    <asp:Button ID="Button1" runat="server" Text="导出"  CssClass="btn_2k3" onclick="Button1_Click"  
                       />
                    <asp:Button ID="Button2"
                        runat="server" Text="取消"  CssClass="btn_2k3" onclick="Button2_Click"/>
                    <asp:TextBox ID="UNIT_ID" runat="server" style="display:none"></asp:TextBox>
                    <asp:TextBox ID="ExpName" runat="server" style="display:none"></asp:TextBox>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
