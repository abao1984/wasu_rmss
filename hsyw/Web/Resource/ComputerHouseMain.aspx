<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComputerHouseMain.aspx.cs" Inherits="Web_Resource_ComputerHouseMain" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%; height: 100%;">
            <tr>
                <td  valign="top" width="20%" class="tdBak">
                <asp:TreeView ID="TreeView_Computer" ShowLines="True" runat="server" 
                                ExpandDepth="0" LineImagesFolder="../Images/TreeLineImages" 
                                style="margin-top: 0px; margin-left: 0px" Width="20px" ></asp:TreeView>
                </td>
             
                <td bgcolor="#CCFFCC" >
                   <IFRAME id="UnitPage" style="Z-INDEX: 1; VISIBILITY: inherit; WIDTH: 100%; HEIGHT: 100%" runat="server"
								name="UnitPage"  frameBorder="0" scrolling="no"> </IFRAME></td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
