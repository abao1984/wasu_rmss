<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Msg.aspx.cs" Inherits="Web_Msg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="foot" TagName="pageFooter"  Src="Include/Ascx/pageFooter.ascx"  %>
<%@ Register TagPrefix="header" TagName="pageHeader"  Src="Include/Ascx/pageHeader.ascx"  %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><% =Session["PageSubTite"].ToString().Trim() + " - " + Session["PageTitle"].ToString().Trim() + " - " + Session["CopyRightAuthor"].ToString().Trim()%></title>
</head>
<header:pageHeader ID="PageHeader" runat="server" />
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
                    
                        <asp:Button ID="Button1" CssClass="btn_2k3" runat="server" Text="回首页" OnClick="Button1_Click" UseSubmitBehavior="False" />
                        &nbsp;
                        <input value="返回上一页" class="btn_2k3" type="button" onclick="history.go(-1);" id="Button2" />
    
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