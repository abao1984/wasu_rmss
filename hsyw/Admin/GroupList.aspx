<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GroupList.aspx.cs" Inherits="Web_sysGroup_GroupList" %>

<%@ Register Src="Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function CheckSelect() {
            var controls = document.getElementById("TreeView_Group").getElementsByTagName("input");
            var count = controls.length;
            var num = 0;
            for (var i = 0; i < count; i++) {
                if (controls[i].type == "checkbox" && controls[i].checked == true) {
                    num++;
                }
            }
            if (num == 0) {
                alert("请至少选择一条数据！");
                window.event.returnValue = false;
            }
            else if(!confirm("确认要删除吗？"))
            {
                window.event.returnValue = false;
            }
        }
        function OpenUsers(code) {
            windowOpenPageByWidth("GroupUsers.aspx?groupcode=" + code, "该角色相关用户", "", "30%", "40%", "10%", "80%");
//            window.event.returnValue = false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;height:100%">
        <tr>
            <td class="tableHead">
        <asp:Button ID="Button_Delete" CssClass="btn_2k3" runat="server" OnClientClick="CheckSelect();"
            Text="删除角色" onclick="Button_Delete_Click" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                        <asp:TreeView ID="TreeView_Group" ShowLines="True" runat="server" 
                            ExpandDepth="0" LineImagesFolder="Images/TreeLineImages" 
                            style="margin-top: 0px; margin-left: 0px;overflow:auto;" Width="100%" 
                            Height="100%" ShowCheckBoxes="Leaf" EnableClientScript="False"></asp:TreeView> 
            </td>
        </tr>
    </table>   
    <a href="javascript:OpenUsers(06)"><font color='#0000FF'>用户</font></a><input id="Button1" onclick="OpenUsers('06')"
        type="button" value="button" />
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
