<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendPage.aspx.cs" Inherits="Web_LCGL_SendPage" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <base target="_self">
  <meta http-equiv="expires" content="-1">
  <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 500px; border-collapse: collapse;" cellpadding="1" cellspacing="0"   border="1" bordercolor="#5b9ed1">
            <tr>
                <td class="tableTitle" colspan="2" align="center">
                    <asp:Label ID="Title" runat="server" Text="签发意见"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdBak"  align="center" colspan="2">
                    <asp:TextBox ID="LCSM" runat="server" Height="101px" TextMode="MultiLine" 
                        Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr id="Tr_Fz" runat="server">
                <td class="tdBak" align="center" width="100px"> 下一流程</td>
                <td align="left"  width="400px">
                    <asp:RadioButtonList ID="SendRadio" runat="server" RepeatColumns="3" 
                        RepeatDirection="Horizontal">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="tdBak" colspan="2" height="40px" align="center">
                    <asp:Button ID="SendButton" runat="server" CssClass="btn_2k3" Text="确定" 
                        onclick="SendButton_Click" />
                    &nbsp;
                    <asp:Button ID="CloseButton" runat="server" CssClass="btn_2k3" Text="关闭" 
                        onclick="CloseButton_Click" />
                </td>
            </tr>
        </table>
      <asp:TextBox ID="LC_GUID" runat="server" style="display:none"></asp:TextBox>
      <asp:TextBox ID="LCZT" runat="server" style="display:none"></asp:TextBox>
      <asp:TextBox ID="XYJDBM" runat="server" style="display:none"></asp:TextBox>  
      <asp:TextBox ID="LCQFBJ" runat="server" style="display:none"></asp:TextBox>      
      <asp:TextBox ID="QFLX" runat="server" style="display:none"></asp:TextBox>
      <asp:TextBox ID="LCBM" runat="server" style="display:none"></asp:TextBox>
    </form>
</body>
</html>
