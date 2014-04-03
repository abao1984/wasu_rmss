<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DiaoDuFaDan.aspx.cs" Inherits="Web_GZCL_FDZ_DiaoDuFaDan" %>

<html  xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="expires" content="-1" />
    <base target="_self" />
    <title></title>

    <script type="text/javascript">

        function fd(obj) {
            if (confirm('确定要发单吗？')) {
                window.returnValue = obj;
                window.close();
            }
        }
        
    </script>

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
    <table width="100%" border="0" height="100%" cellpadding="0" cellspacing="0">
        <tr height="95%" class="tdBak" runat="server" id="tr_tree">
            <td valign="top">
                <div style="width: 100%; height: 100%; overflow: auto">
                    <asp:TreeView ID="TreeViewBranch" runat="server" OnTreeNodeCheckChanged="TreeViewBranch_TreeNodeCheckChanged"
                        ShowCheckBoxes="All" ShowLines="True" ExpandDepth="1">
                    </asp:TreeView>
                </div>
            </td>
        </tr>
        <tr height="95%" class="tdBak" runat="server" id="tr_clsm">
            <td valign="top">
                操作原因(返回网管中心填写)：<asp:TextBox ID="CLSM" runat="server" Width="100%" BorderStyle="None"
                    Rows="3" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr height="5%">
            <td align="center" class="tdBak">
                <asp:Button ID="BtnCL" runat="server" Text="返回网管中心" class="btn_2k3" OnClick="BtnCL_Click" />
                <asp:Button ID="BtnFD" runat="server" Text="确认发单" class="btn_2k3" OnClick="BtnFD_Click" />
                <asp:Button ID="BtnQX" runat="server" Text=" 取 消 " class="btn_2k3" OnClick="BtnQX_Click" />
                <asp:TextBox ID="ZBGUID" runat="server" Style="display: none"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
