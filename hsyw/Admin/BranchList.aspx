<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchList.aspx.cs" Inherits="Web_sysBranch_BranchList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
        <tr>
            <td class="tableHead" align="left">
                <asp:Button ID="BtnNewRoot" Text="新增根节点" CssClass="btn_2k3" runat="server" OnClick="BtnNewRoot_Click" />
                <asp:Button ID="Button_Delete" CssClass="btn_2k3" runat="server" Text="删除机构" OnClick="Button_Delete_Click" />
            </td>
        </tr>
        <tr>
            <td class="tableBg2" style="height: 100%" align="left" valign="top">
                <div style="width: 100%; height: 100%; overflow: auto; vertical-align: top; background-color: #ffffff">
                    <asp:TreeView ID="TreeView_Branch" ShowLines="True" runat="server" ExpandDepth="2"
                        LineImagesFolder="Images/TreeLineImages" Style="margin-top: 0px; margin-left: 0px"
                        Width="20px" ShowCheckBoxes="Leaf">
                    </asp:TreeView>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
