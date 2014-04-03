<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GroupMenu.aspx.cs" Inherits="Web_sysGroup_GroupMenu" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><% =Session["PageSubTite"].ToString().Trim() + " - " + Session["PageTitle"].ToString().Trim() + " - " + Session["CopyRightAuthor"].ToString().Trim()%></title>
    <script type="text/javascript">
     // 点击复选框时触发事件
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
    <table style="width: 100%;height:100%">
        <tr>
            <td class="tableHead"> <asp:Button ID="Button_Update_Menu" CssClass="btn_2k3" runat="server" 
                           Text=" 提交 "  UseSubmitBehavior="False" 
                    onclick="Button_Update_Menu_Click" />
                           
                   <asp:Button ID="Button_Cancel" CssClass="btn_2k3" runat="server" 
                            Text="返回"  CausesValidation="False" 
                    onclick="Button_Cancel_Click" />
            </td>
        </tr>
        <tr>
            <td valign="top">
            <asp:TreeView ID="TreeView_Menu" ShowLines="True" runat="server" 
                ExpandDepth="0" LineImagesFolder="Images/TreeLineImages" 
                style="margin-top: 0px; margin-left: 0px;overflow:auto;"  Width="100%" Height="100%"  
                ShowCheckBoxes="All" 
                OnTreeNodeCheckChanged="TreeView_Menu_TreeNodeCheckChanged"                            
                ></asp:TreeView> 
            </td>
        </tr>
    </table>   
    </form>
</body>
</html>
