<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JinChengGengZhong.aspx.cs" Inherits="Web_GZCL_JinChengGengZhong" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="expires" content="-1" />
    <base target="_self" />
     <script type="text/javascript">
        function windowOpenEnumDataPage(EnumSort, LinkCode) {
            var P_Enum_Name = "";
            if (LinkCode != "") {
                P_Enum_Name = document.getElementById(LinkCode).value;
            }
            var url = "GZSZ/GuZhangMeiJu.aspx?ENUM_SORT=" + EnumSort + "&P_ENUM_NAME=" + encodeURI(P_Enum_Name);
            windowOpenPageByWidth(url, "枚举维护", "btn", "10%", "80%", "10%", "80%");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" style="border-collapse: collapse;" border="1"
        bordercolor="#5b9ed1" width="100%">
        <tr>
            <td class="tdBak" align="center" style="width: 30%">
                处理说明
            </td>
            <td>
                <asp:TextBox ID="CLSM" runat="server" Width="100%" BorderStyle="None" Rows="4" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr_gzzt" runat="server">
            <td class="tdBak" align="center">
                故障状态
            </td>
            <td id="td_gzzt" runat="server">
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td style="width: 100%">
                            <asp:DropDownList ID="ZT" runat="server" Width="100%" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <img src="../Images/Small/bb_table.gif" onclick="windowOpenEnumDataPage('GZZT','')"   />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdBak" align="center">
                跟踪部门
            </td>
            <td>
                <asp:Label ID="CLBM" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdBak" align="center">
                跟踪人员
            </td>
            <td>
                <asp:Label ID="CLRY" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" class="tdBak">
                <asp:Button ID="BtnCL" runat="server" Text=" 确 定 " class="btn_2k3" OnClick="BtnCL_Click" />
                <asp:Button ID="BtnQX" runat="server" Text=" 取 消 " class="btn_2k3" OnClick="BtnQX_Click" />
                <asp:TextBox ID="ZBGUID" runat="server" Style="display: none"></asp:TextBox>
                <asp:TextBox ID="GUID" runat="server" Style="display: none"></asp:TextBox>
                <asp:Button ID="btn" runat="server" Text="Button" onclick="btn_Click"  Style="display: none"/>
            </td>
        </tr>
    </table>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
