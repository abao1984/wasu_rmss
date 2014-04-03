<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserList.aspx.cs" Inherits="Web_GZCL_GZSZ_UserList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
        function postBackByObject() {
            var o = window.event.srcElement;
            if (o.tagName == "INPUT" && o.type == "checkbox") {
                __doPostBack("", "");
            }
        }  
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%; height: 100%;">
            <tr>
                <td class="tableHead">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" OnClick="SaveButton_Click"
                                    Text="确定" />
                                <asp:TextBox ID="TXT_RYBM" runat="server" Width="103px" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="TXT_NAME" runat="server" Width="103px" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="TXT_CODE" runat="server" Width="103px" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="TXT_Branch" runat="server" Width="103px" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="TXT_BMNAME" runat="server" Width="103px" Style="display: none;"></asp:TextBox>
                                <asp:TextBox ID="TXT_TYPE" runat="server" Width="103px" Style="display: none;"></asp:TextBox>
                            </td>
                            <td width="50%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdBak" style="height: 100%;">
                    <div style="height: 100%; width: 100%; overflow: auto">
                        <asp:TreeView ID="TreeViewBranch" runat="server" OnTreeNodeCheckChanged="TreeViewBranch_TreeNodeCheckChanged"
                            ShowCheckBoxes="All" ShowLines="True" ExpandDepth="1">
                        </asp:TreeView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:TextBox ID="bmCode" runat="server" Style="display: none"></asp:TextBox>
    </form>
</body>
</html>
