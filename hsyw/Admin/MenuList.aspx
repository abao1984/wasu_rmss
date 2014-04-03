<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuList.aspx.cs" Inherits="Web_sysMenu_MenuList" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;height:100%">
        <tr>
            <td class="tableHead">
            <asp:Button ID="Button_Delete" CssClass="btn_2k3" runat="server" 
            Text="删除" onclick="Button_Delete_Click" />
            </td>
        </tr>
        <tr>
            <td>               
            <asp:TreeView ID="TreeView_Menu" ShowLines="True" runat="server" 
                ExpandDepth="0" LineImagesFolder="Images/TreeLineImages" 
                style="margin-top: 0px; margin-left: 0px;overflow:auto;" Width="100%" Height="100%" ShowCheckBoxes="Leaf"></asp:TreeView> 
            </td>
        </tr>
    </table>   
    </form>
</body>
</html>
