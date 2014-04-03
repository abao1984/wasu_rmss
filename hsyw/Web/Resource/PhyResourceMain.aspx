<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhyResourceMain.aspx.cs" Inherits="Web_Resource_PhyResourceMain" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title><% =Session["PageSubTite"].ToString().Trim() + " - " + Session["PageTitle"].ToString().Trim() + " - " + Session["CopyRightAuthor"].ToString().Trim()%></title>

    <script>
        var timeFlag;
        var timeFlag2;
        function DisPlayTR() {
            var msg = document.getElementById("Message");
            if (window.document.readyState != null && window.document.readyState != 'complete') {
                msg.style.display = "block";
                document.getElementById("tr_Main").style.display = "none";
            }
            else {
                msg.style.display = "none";
                document.getElementById("tr_Main").style.display = "block";
                clearInterval(timeFlag);
                return;
            }
            timeFlag = setInterval("DisPlayTR", 500);
        }
        window.onload = DisPlayTR;
        
    </script>

</head>
<body>
    <form id="form1" runat="server" >
        <table style="width:100%; height: 100%;">
            <tr>
                <td width="124px" valign="top" bgcolor="#E2F0FB" 
                    style="border: 1px solid #5b9ed1">                 
                <asp:TreeView ID="TreeView_Unit" ShowLines="True" runat="server" 
                                ExpandDepth="0" LineImagesFolder="../Images/TreeLineImages" 
                                style="margin-top: 0px; margin-left: 0px;overflow:auto;" Width="124px" Height="100%" ></asp:TreeView>
                               
                </td>
                <td id="tr_Main" style="DISPLAY: none">
                   <iframe id="PhyPage" style="Z-INDEX: 1; VISIBILITY: inherit; WIDTH: 100%; HEIGHT: 100%" runat="server"
								name="PhyPage"  frameborder="0" scrolling="no" > </iframe></td>
				<td id="Message" valign="middle" align="center"><IMG alt="" src="../Images/waiting.gif">页面正在加载..请稍候</td>
            </tr>
        </table>
    </form>
</body>
</html>
