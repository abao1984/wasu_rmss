<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComputerHouse.aspx.cs" Inherits="Web_Resource_ComputerHouse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <script language="javascript" type="text/javascript">
     function windowOpen(guid) {
         var p_guid = document.getElementById("GUID").value;
         var unit_id = document.getElementById("SUB_UNIT_ID").value;
         document.getElementById("EditPage").src = "ComputerHouseEdit.aspx?P_GUID=" + p_guid + "&GUID=" + guid + "&UNIT_ID=" + unit_id;
         document.getElementById("EditDiv").style.display = "block";
        }
        </script>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td width="20%" valign="top">
                    <asp:GridView ID="GridView_Unit" runat="server"  SkinID="GridView1" 
                        DataKeyNames="GUID" 
                        onselectedindexchanging="GridView_Unit_SelectedIndexChanging" 
                        BorderColor="#5B9ED1" BorderWidth="1px"  >
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:ButtonField Text="按钮" />
                        </Columns>
                        <SelectedRowStyle BackColor="#CCCCFF" />
    </asp:GridView>
                </td>
                <td valign="top">
                    <table style="width:100%;">
                        <tr>
                            <td class="tableHead">
                            <asp:TextBox ID="GUID" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                                <asp:TextBox ID="UNIT_ID" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                                <asp:TextBox ID="SUB_UNIT_ID" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                                <asp:TextBox ID="TABLE_NAME" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                    <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" Text="保存" 
                        onclick="SaveButton_Click" />
                                <asp:Button ID="Btn" runat="server" style="display:none;" onclick="Btn_Click" />
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
                                    onrowdatabound="GridView_SubUnit_RowDataBound" DataKeyNames="GUID">
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
     <div id="EditDiv" runat="server" style="display:none;border: 2px solid #C0C0C0; top: 10%; left: 10%; position: absolute; z-index: inherit; width: 80%; height: 85%;">
    <iframe id="EditPage" style="Z-INDEX: 1; VISIBILITY: inherit; WIDTH: 100%; HEIGHT: 100%" runat="server"
								name="EditPage"  frameBorder="0"  scrolling="no" > </iframe>
    </div>
    </form>
    </body>
</html>
