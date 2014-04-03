<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataBaseInput.aspx.cs" Inherits="DataBaseInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td align="left">
                    <asp:Button ID="GuGanButton" runat="server" onclick="GuGanButton_Click" 
                        Text="骨干业务" />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True">
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
