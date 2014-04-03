<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceMain.aspx.cs" Inherits="Web_Resource_ResourceMain" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%;"  border="0" 
        cellpadding="0" cellspacing="0" class="tdBak">
        <tr>
            <td style="width:100px;" valign="top" bgcolor="#E2F0FB" style="border: 1px solid #5b9ed1">
                <asp:TreeView ID="TreeView_Unit" ShowLines="True" runat="server" 
                                ExpandDepth="0" LineImagesFolder="../Images/TreeLineImages" 
                                style="margin-top: 0px; margin-left: 0px;overflow:auto;" Width="124px" Height="100%"></asp:TreeView>
                <asp:Button ID="Btn" runat="server" onclick="Btn_Click" style="display:none;" />
                <asp:TextBox ID="UNIT_ID" runat="server" Width="30px"  style="display:none;"></asp:TextBox>
            </td>
            <td  >
               <IFRAME id="UnitPage" style="Z-INDEX: 1; VISIBILITY: inherit; WIDTH: 100%; HEIGHT: 100%" runat="server"
								name="UnitPage"  frameBorder="0" scrolling="no" src="ResourceUnit.aspx"> </IFRAME></td>
        </tr>
    </table>
    </form>
</body>
</html>
