<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectBM.aspx.cs" Inherits="Web_SelectBM" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />
    <meta http-equiv="expires" content="-1" />
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" style="height: 100%; width: 100%;">
        <tr>
            <td class="tableHead">
                <asp:Button ID="BtnSure" runat="server" Text="确定" CssClass="btn_2k3" OnClick="BtnSure_Click" />
            </td>
        </tr>
        <tr>
            <td style="height: 100%" valign="top">
                <div style="overflow: auto; width: 100%; height: 100%;">
                    <asp:TreeView ID="TreeView1" ShowLines="True" runat="server" ExpandDepth="0" LineImagesFolder="Images/TreeLineImages"
                        Style="margin-top: 0px; margin-left: 0px" >
                    </asp:TreeView>
                </div>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="BranchCodes" runat="server"></asp:TextBox>
    </form>
</body>
</html>
