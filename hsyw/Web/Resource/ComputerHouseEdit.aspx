<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComputerHouseEdit.aspx.cs" Inherits="Web_Resource_ComputerHouseEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script language="javascript" type="text/javascript">
        function windowClose() {
            window.close();
            parent.document.getElementById("EditDiv").style.display = "none";
            parent.document.getElementById("Btn").click();
        }

        function limitNum(obj) {
            if (event.keyCode < 47 || event.keyCode > 57) {
                event.keyCode = 0;
            }
            return;
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
       <div>
    
        <table style="width:100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tableHead" width="60%">
                    <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" Text="保存" 
                        onclick="SaveButton_Click" />
                    <asp:Button ID="Button2" runat="server" CssClass="btn_2k3" Text="Button" />
                </td>
                <td align="right" class="tableHead" style="padding-right: 5px">
                    <img align="right" alt="" src="../Images/Header/WindowXPClose.jpg" onclick="windowClose()"  /><asp:TextBox 
                        ID="PROPERY_ID" runat="server" Width="45px" style="display:none;"></asp:TextBox>
                    <asp:TextBox ID="UNIT_ID" runat="server" Width="45px" style="display:none;"></asp:TextBox>
                    <asp:TextBox ID="GUID" runat="server" Width="45px" style="display:none;"></asp:TextBox>
                     <asp:TextBox ID="P_GUID" runat="server" Width="45px" style="display:none;"></asp:TextBox>
                     <asp:TextBox ID="TABLE_NAME" runat="server" Width="45px" style="display:none;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2"  id="TD_PROPERTY" runat="server">
               
                    </td>
                    </tr>
                    </table>
    </div>
    </form>
</body>
</html>
