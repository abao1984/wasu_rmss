<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComputerHouseList.aspx.cs" Inherits="Web_Resource_ComputerHouseList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width:100%;">
                        <tr>
                            <td class="tableHead">
                            <asp:TextBox ID="GUID" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                                <asp:TextBox ID="UNIT_ID" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                                <asp:TextBox ID="SUB_UNIT_ID" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                                <asp:TextBox ID="TABLE_NAME" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                    <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" Text="保存" 
                        />
                                <asp:Button ID="Btn" runat="server" style="display:none;" />
                            </td>
                        </tr>
                        <tr>
                            <td id="TD_PROPERTY" runat="server"></td>
                        </tr>
                        <tr>
                            <td align="left" 
                                style="height:34px; background-image: url('../Images/Header/tr_top02.jpg');" 
                                >
    <table border="0" cellpadding="0" cellspacing="0" >
        <tr id="MenuTr" runat="server">           
        </tr>
        </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView_SubUnit" runat="server" SkinID="GridView1" 
                                     DataKeyNames="GUID">
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
    </form>
</body>
</html>
