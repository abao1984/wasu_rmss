<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceAutoCut.aspx.cs" Inherits="Web_ResourceCut_ResourceAutoCut" %>

<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="../Resource/ResourceScript.js"></script>
    <script language="javascript" type="text/javascript">
        function windowOpenEqu(txt_fl, txt_name, p_txt_name, linkage_code) {
            var unit_id = document.getElementById(txt_fl).value;
            windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, '1')
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
 
        <table  style="width: 100%; border-collapse: collapse;" cellpadding="0" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
            <tr>
                <td class="tableHead" align="center" colspan="6">
                    <asp:Button ID="okCutButton" runat="server" CssClass="btn_2k3" 
                        onclick="okCutButton_Click" Text="确定割接分析" />
                    <asp:Button ID="CreateButton" runat="server" CssClass="btn_2k3" 
                        onclick="CreateButton_Click" Text="创建故障单" />
                </td>
            </tr>
            <tr>
                <td class="tdBak" width="13%" align="center">
                    机房名称</td>
                <td width="20%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="JF" runat="server" Width="100%" BorderWidth="0" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','JF','','','1','')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                <td class="tdBak" width="13%" align="center">
                    设备名称</td>
                <td width="20%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="SB" runat="server" BorderWidth="0" Width="100%" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenEqu('SBLX','SB','JF','HOUSE_NAME','1')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                <td class="tdBak" width="13%" align="center">
                   割接分类</td>
                <td width="20%">
                            <asp:DropDownList ID="GJFL" runat="server" Width="100%">
                                <asp:ListItem Value="V_BUSINESS_IP">IP资源割接分析</asp:ListItem>
                                <asp:ListItem Value="V_BUSINESS_CS">传输割接分析</asp:ListItem>
                                <asp:ListItem Value="V_BUSINESS_GL">光缆割接分析</asp:ListItem>
                            </asp:DropDownList>
                </td>
            </tr>
            <tr style="display:none">
                <td align="center" class="tdBak">
                    割接路径</td>
                <td colspan="5">
                    <asp:Label ID="GJLJ" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
             <asp:TextBox ID="ZBGUID" runat="server" Style="display: none"></asp:TextBox>
             <asp:TextBox ID="SBLX" runat="server" Style="display: none"></asp:TextBox>
            <asp:TextBox ID="JF_GUID" runat="server" Style="display: none"></asp:TextBox>
            <asp:TextBox ID="JF_CODE" runat="server" Style="display: none"></asp:TextBox>
            <asp:TextBox ID="SB_GUID" runat="server" Style="display: none"></asp:TextBox>
             <asp:TextBox ID="SB_CODE" runat="server" Style="display: none"></asp:TextBox>
            <asp:TextBox ID="DK_GUID" runat="server" Style="display: none"></asp:TextBox>
            <uc1:windowHeader ID="windowHeader1" runat="server" />
        <asp:GridView ID="BusinessGridView" runat="server" AutoGenerateColumns="False"  SkinID="GridView1"
           BorderColor="#5B9ED1" BorderWidth="1px" Width="100%" 
            onrowdatabound="BusinessGridView_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="序号" DataField="XH" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="LLMC" HeaderText="链路" />
                <asp:BoundField DataField="YWBM" HeaderText="业务编码" />
                <asp:BoundField DataField="YWMC" HeaderText="业务名称" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
