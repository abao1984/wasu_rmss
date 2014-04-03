<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogPart.aspx.cs" Inherits="Admin_LogPart" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label1"  runat="server" Font-Size="12px" Text="操作简介："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left">
                        <asp:Label ID="Label_Title"  runat="server" Font-Size="12px" Text=""></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label4"  runat="server" Font-Size="12px" Text="IP："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left">
                        <asp:Label ID="Label_Ip"  runat="server" Font-Size="10px" Text=""></asp:Label>
                    </td>
                </tr>
                
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label7"  runat="server" Font-Size="12px" Text="操作人："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left">
                        <asp:Label ID="Label_UserName"  runat="server" Font-Size="12px" Text=""></asp:Label>
                    </td>
                </tr>
                
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:30px">
                        <asp:Label ID="Label9"  runat="server" Font-Size="12px" Text="时间："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left">
                        <asp:Label ID="Label_UserDateTime"  runat="server" Font-Size="10px" Text=""></asp:Label>
                    </td>
                </tr>
                
                
                <tr>
                    <td class="tableBg1" style="text-align:right; width:180px; height:190px; vertical-align:top">
                        <asp:Label ID="Label11"  runat="server" Font-Size="12px" Text="内容："></asp:Label></td>
                    <td class="tableBg2" style="text-align:left; vertical-align:top">
                        <asp:TextBox ID="TextBox_Memo" CssClass="textBase" Height="180px" Width="360px" runat="server" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        
        </div>
        
        <br />
        <br />
        <asp:Button ID="Button_Cancel" CssClass="btn_2k3" runat="server" 
                            Text=" 返回 "  CausesValidation="False" onclick="Button_Cancel_Click" />
        <br />
        
        
        </div>
        
    </div>
    </form>
</body>
</html>
