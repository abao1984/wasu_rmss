<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserGroup.aspx.cs" Inherits="Web_sysUser_UserGroup" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
                   <td class="tableBg1" style="width:200px"> 
                   </td>
                   <td class="tableBg2" style="text-align:left; width:auto">
                   <br />
                        <asp:TreeView ID="TreeView_Group" ShowLines="True" runat="server" 
                            ExpandDepth="0" LineImagesFolder="Images/TreeLineImages" 
                            style="margin-top: 0px; margin-left: 0px" Width="10px" 
                            ShowCheckBoxes="All" 
                            OnTreeNodeCheckChanged="TreeView_Group_TreeNodeCheckChanged"
                            
                            ></asp:TreeView> 
                   <br />
                   </td>
                </tr>
                <tr>
                   <td class="tableBg1" style="text-align:right; width:80px; height:30px"> 
                   </td>
                   <td class="tableBg2" style="text-align:left; width:auto">
                   
                   <asp:Button ID="Button_Update_Group" CssClass="btn_2k3" runat="server" 
                           Text=" 变更 "  UseSubmitBehavior="False" onclick="Button_Update_Group_Click" />
                           
                   </td>
                </tr>
                
            </table>
            
            </div>
            </div>        
    
    </div>
    </form>
</body>
</html>
