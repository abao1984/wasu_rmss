<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWQYtree.aspx.cs" Inherits="Admin_FWQYtree" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script  language="javascript" type="text/javascript">
    function postBackByObject()
    {
      var o = window.event.srcElement;
      if (o.tagName == "INPUT" && o.type == "checkbox")
      {
          __doPostBack("","");
      }
    }  
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%;">
        <tr>
            <td class="tableHead">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" OnClick="SaveButton_Click"
                                Text="确定" />
                        </td>
                        <td width="50%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%;" valign="top">
                <div style="width:100%; height:100%; overflow:auto">
                <asp:TreeView ID="TreeViewBranch" runat="server" 
                    ShowCheckBoxes="All" 
                        ontreenodecheckchanged="TreeViewBranch_TreeNodeCheckChanged" 
                        ShowLines="True">
                </asp:TreeView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
