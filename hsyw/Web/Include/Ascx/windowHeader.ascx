<%@ Control Language="C#" AutoEventWireup="true" CodeFile="windowHeader.ascx.cs" Inherits="App_Ascx_windowHeader" %>
 <script language="javascript" type="text/javascript">

     function windowOpenPage(url,name,btnName) {
         windowOpenPageByWidth(url, name, btnName, "0px", "100%", "0px", "100%");
     }
     var oldTop = "0px";
     var oldHeight = "100%";
     function windowOpenPageByWidth(url, name, btnName,left,width,top,height) {
        //因为如果有打开的页面最小化了，可又打开了同一个页面，代码只是把TOP设成0而已，所以页面显示就全部是HEADER，乱了。
        //在打开页面时加个maxWinodw()方法就是在打开前判断一下有没最小化的。有就显示出来 --罗耀斌
         MaxWinodw();
         document.getElementById("<%=PropertyDiv.ClientID %>").style.width = width;
         document.getElementById("<%=PropertyDiv.ClientID %>").style.left = left;
         document.getElementById("<%=PropertyDiv.ClientID %>").style.top = top;
         document.getElementById("<%=PropertyDiv.ClientID %>").style.height = height;
         oldTop = top;
         oldHeight = height;
         document.getElementById("<%=PropertyPage.ClientID %>").src = url;
         document.getElementById("<%=PropertyDiv.ClientID %>").style.display = "block";
         document.getElementById("<%=LabelHead.ClientID %>").innerText = name;
         document.getElementById("<%=Btn_Name.ClientID %>").value = btnName;
     }
     function changeWindow() {
         if (document.getElementById("<%=PropertyTR.ClientID %>").style.display == "none") {
             MaxWinodw();
         }
         else {
             MinWindow();
         }
     }
     function MinWindow() {
         document.getElementById("<%=PropertyTR.ClientID %>").style.display = "none";
         document.getElementById("<%=PropertyDiv.ClientID %>").style.height = "30px";
         document.getElementById("<%=PropertyDiv.ClientID %>").style.top = document.body.offsetHeight - 30;
     }
     function MaxWinodw() {
         document.getElementById("<%=PropertyTR.ClientID %>").style.display = "block";
         document.getElementById("<%=PropertyDiv.ClientID %>").style.height = oldHeight;
         document.getElementById("<%=PropertyDiv.ClientID %>").style.top = oldTop;
     }
     function WindowClose() {
         document.getElementById("<%=PropertyDiv.ClientID %>").style.display = "none";
         var btnName = document.getElementById("<%=Btn_Name.ClientID %>").value;
         if (btnName != "undefined" && btnName !="")
         {
             var ele = document.getElementById(btnName);
                ele.click();
         }
     }
     function SetLabelHead(name) {
         document.getElementById("<%=LabelHead.ClientID %>").innerText = name;
     }
     function windowDismiss(){
        document.getElementById("<%=PropertyDiv.ClientID %>").style.display = "none";
     }
 </script>
    <div id="PropertyDiv" runat="server"  style="display:none;border: 2px solid #5b9ed1;  position: absolute; z-index: inherit; width: 100%; height: 100%;top:0px;left:0px;">
    <table style="width:100%;height:100%" border="0px" cellpadding="0" cellspacing="0">
    <tr style="HEIGHT:29px"  ondblclick="changeWindow()">
		<td width="25px" style="background-image: url('../images/IE7.gif')" >
            <asp:Label ID="Label1" runat="server" Width="25px"></asp:Label></td>
		<td style="background-image: url('../images/WindowXPHead.gif')" width="100%">
			<asp:Label id="LabelHead" runat="server" ForeColor="White" Font-Bold="True"></asp:Label> <asp:TextBox ID="Btn_Name" runat="server" style="display:none;"></asp:TextBox></td>
		<td style="background-image: url('../images/WindowXPHead.gif')" align="right" valign="middle">
			<img alt="" id="XPMin" src="../images/WindowXPMin.gif" border="0" title="最小化" onclick="MinWindow();"><img alt="" src="../images/WindowXPMax.gif" border="0" id="XPMax" title="最大化" onclick="MaxWinodw();"><img alt="" src="../images/WindowXPClose.gif" border="0" id="XPColse" title="关闭退出"	onclick="windowDismiss();"></td>
	</tr>
    <tr id="PropertyTR" runat="server">
    <td colspan="3">
    <iframe id="PropertyPage" style="Z-INDEX: 1; VISIBILITY: inherit; WIDTH: 100%; HEIGHT: 100%" runat="server"
								name="ProperyPage"  frameborder="0"   scrolling="auto"> </iframe>
		 </td>
    </tr>
    </table>						
    </div>