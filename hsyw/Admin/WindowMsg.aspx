<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WindowMsg.aspx.cs" Inherits="Admin_WindowMsg" %>

<%@ Register TagPrefix="foot" TagName="pageFooter"  Src="Include/Ascx/windowFooter.ascx"  %>
<%@ Register TagPrefix="header" TagName="pageHeader"  Src="Include/Ascx/windowHeader.ascx"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><% =Session["PageSubTite"].ToString().Trim() + " - " + Session["PageTitle"].ToString().Trim() + " - " + Session["CopyRightAuthor"].ToString().Trim()%></title>
     <script type="text/javascript" language="javascript" src="Include/JavaScript/Window.js" charset="gb2312"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            
            <div class="tableMain">
            <div class="tableSpaceBorder">
    
             <table>
                <tr>
                    <td class="tableHead" >
                        
                    </td>
                </tr>
             
                <tr>
                    <td class="tableCategory" >
                    </td>
                </tr>
                
                <tr>
                    <td class="tableBg2" style="text-align:center; height:100px; vertical-align:middle" >
                    <br />
                    <br />
                    
                    <div class="table">
                    <table>
                        <tr>
                            <td style="text-align:right; vertical-align:middle; width:30%">
                            
                                <asp:Image ID="Image_Msg" runat="server" />
                            
                            </td>
                            <td>
                                <div class="text">
                                <asp:Label ID="Label_MSG" runat="server"></asp:Label>
                                <div />
                            </td>
                        </tr>
                    </table>
                    </div>
                    
                    <br />
                    <br />
                        
                    </td>
                </tr>
               
               <tr>
                    <td class="tableBg1" style="text-align:center; height:40px" >
                    
               
                <input id="Button1" type="button" class="btn_2k3" value=" 关闭 " 
                                 onclick="WindowClose();" />
    
                    </td>
                </tr>
                
            </table>
            </div>    
            </div>
                        <br />
                        <br />
                        <br />
    
    
    </div>
    </form>
</body>
<foot:pageFooter ID="pageFooter" runat="server" />
</html>
<%=Session["Msg"].ToString().Trim()%>
