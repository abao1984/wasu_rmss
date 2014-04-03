<%@ Page Language="C#" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="Web_About" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register TagPrefix="foot" TagName="pageFooter"  Src="Include/Ascx/windowFooter.ascx"  %>
<%@ Register TagPrefix="header" TagName="pageHeader"  Src="Include/Ascx/windowHeader.ascx"  %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><% =Session["PageTitle"].ToString().Trim() + " - " + Session["CopyRightAuthor"].ToString().Trim()%></title>
    <script type="text/javascript" language="javascript" src="Include/JavaScript/Window.js" charset="gb2312"></script>
</head>

<body>
    <form id="form1" runat="server">
    <div >
    
    <br />
    <br />

    
    <div class="tableMain">
             <table style="width:70%">

                
                <tr>

                    <td>
                        <img alt="" src="Images/Main/About.gif" />
                    </td>
                </tr>
                
                <tr>
                    <td style="text-align:left">
                        <br />
                        <asp:Label ID="Label1" runat="server" Text="版本："></asp:Label>
                        <br />
                        <asp:Label ID="Label2" runat="server" Text="版本："></asp:Label>
                        <br />
                        <asp:Label ID="Label3" runat="server" Text="版本："></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="Label4" runat="server" Text="版本："></asp:Label>
                        <br />
                        <asp:Label ID="Label5" runat="server" Text="版本："></asp:Label>
                        <br />
                        <asp:Label ID="Label6" runat="server" Text="版本："></asp:Label>
                        <br />
                        <asp:Label ID="Label7" runat="server" Text="版本："></asp:Label>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right">
                        <input id="Button2" type="button" class="btn_2k3" value="关闭本页" 
                                 onclick="WindowClose();" />
                    </td>
                </tr>
                    
                
             </table>
             
    </div>
    
    
    
    
    </div>
    </form>
</body>
<foot:pageFooter ID="pageFooter" runat="server" />
</html>
<%=Session["Msg"].ToString().Trim()%>