<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceCutOver.aspx.cs" Inherits="Web_ResourceCut_ResourceCutOver" %>

<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                    <asp:Button ID="PrevCutButton" runat="server" CssClass="btn_2k3" 
                        onclick="PrevCutButton_Click" Text="向上割接" />
                    <asp:Button ID="NextCutButton" runat="server" CssClass="btn_2k3" 
                        onclick="NextCutButton_Click" Text="向下割接" />
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
                    端口</td>
                <td width="20%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="DK" runat="server" Width="100%" BorderWidth="0" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('bfc13d2d-eab8-4784-a96a-b8ffc21b4e88','DK','SB','EQU_NAME','0')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
            </tr>
            <tr>
                <td align="center" class="tdBak">
                    割接路径</td>
                <td colspan="5">
                    <asp:Label ID="GJLJ" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
             <asp:TextBox ID="SBLX" runat="server" Style="display: none"></asp:TextBox>
            <asp:TextBox ID="JF_GUID" runat="server" Style="display: none"></asp:TextBox>
            <asp:TextBox ID="JF_CODE" runat="server" Style="display: none"></asp:TextBox>
            <asp:TextBox ID="SB_GUID" runat="server" Style="display: none"></asp:TextBox>
             <asp:TextBox ID="SB_CODE" runat="server" Style="display: none"></asp:TextBox>
            <asp:TextBox ID="DK_GUID" runat="server" Style="display: none"></asp:TextBox>
            <uc1:windowHeader ID="windowHeader1" runat="server" />
        <asp:GridView ID="BusinessGridView" runat="server" AutoGenerateColumns="False"  SkinID="GridView1"
           BorderColor="#5B9ED1" BorderWidth="1px" Width="100%" 
            DataKeyNames="JDJRJF_GUID,JDSB_GUID,JDSBDK_GUID,YDJRJF_GUID,YDSB_GUID,YDSBDK_GUID,JDJRJF,JDSB,JDSBDK,YDJRJF,YDSB,YDSBDK" 
            onrowcommand="BusinessGridView_RowCommand" 
            onrowdatabound="BusinessGridView_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="序号" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="YWMC" HeaderText="业务名称" />
                <asp:ButtonField CommandName="JDJRJF" DataTextField="JDJRJF" HeaderText="上联机房" 
                    Text="按钮" />
                <asp:ButtonField CommandName="JDSB" DataTextField="JDSB" HeaderText="上联设备" 
                    Text="按钮" />
                <asp:ButtonField CommandName="JDSBDK" DataTextField="JDSBDK" HeaderText="上联端口" 
                    Text="按钮" />
                <asp:ButtonField CommandName="YDJRJF" DataTextField="YDJRJF" HeaderText="下联机房" 
                    Text="按钮" />
                <asp:ButtonField CommandName="YDSB" DataTextField="YDSB" HeaderText="下联设备" 
                    Text="按钮" />
                <asp:ButtonField CommandName="YDSBDK" DataTextField="YDSBDK" HeaderText="下联端口" 
                    Text="按钮" />
                <asp:ButtonField DataTextField="YWBM" HeaderText="用户编码" Text="按钮" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
