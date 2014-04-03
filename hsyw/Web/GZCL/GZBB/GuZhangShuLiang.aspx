<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuZhangShuLiang.aspx.cs" Inherits="Web_GZCL_GZBB_GuZhangShuLiang" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../../calendar.js" language="javascript" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
       <table border="1" height="100%" cellpadding="0" cellspacing="0" align="center" width="100%">
        <tr>
            <td align="center" class="tdBak">
                业务主体
            </td>
            <td width="15%">
                <asp:DropDownList ID="dropYWLB" runat="server" Width="100%"
                   >
                </asp:DropDownList>
            </td>
            <td width="15%" class="tdBak" align="center">
                时间
            </td>
            <td width="15%">
                <asp:TextBox ID="TSSJ1" runat="server" onfocus="setDay(this);" BorderStyle="None"
                    Width="100%"></asp:TextBox>
            </td>
            <td width="3%" class="tdBak" align="center" runat="server" id="time1">
                至
            </td>
            <td width="15%" runat="server" id="time2">
                <asp:TextBox ID="TSSJ2" runat="server" onfocus="setDay(this);" BorderStyle="None"
                    Width="100%"></asp:TextBox>
            </td>
             <td class="tdBak" align="right">
                <asp:Button ID="Button1" runat="server" Text="生成报表" CssClass="btn_2k3" 
                     onclick="Button1_Click" />
                     <asp:Button ID="Button2" runat="server" Text="导出报表" onclick="Button2_Click" CssClass="btn_2k3"  />
                <%-- <asp:Button ID="Button2" runat="server" Text="导出报表" />--%>
            </td>
        </tr>
        <tr height="100%">
            <td colspan="7" valign="top" align="center">
                <asp:GridView ID="GridView1" runat="server"  SkinID="GridView1" 
                    AutoGenerateColumns="False" onrowdatabound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="TSSJ" HeaderText="时间" 
                            DataFormatString="{0:yyyy-MM-dd}"  >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="处理量"  >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="投诉量" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    
                </asp:GridView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

