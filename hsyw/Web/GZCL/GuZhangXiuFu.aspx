<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuZhangXiuFu.aspx.cs" Inherits="Web_GZCL_GuZhangXiuFu" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html>
<head runat="server">
    <title>故障修复</title>
    <meta http-equiv="expires" content="-1" />
    <base target="_self" />

    <script type="text/javascript">
        function OpenBranch(name, code) {
            windowOpenPageByWidth("GZSZ/BranchsTree.aspx?NAME=" + name + "&CODE=" + code + "&DX=1", "选择所属区域", "", "10%", "80%", "10%", "80%");
            window.event.returnValue = false;

        }
        function OpenUser(name, code, bmCode, bmname) {
            //var codes = document.getElementById(bmCode).value;
            windowOpenPageByWidth("GZSZ/UserList.aspx?NAME=" + name + "&CODE=" + code + "&DX=0&khwhxs=1&Branch=" + bmCode + "&BMNAME=" + bmname, "选择用户", "", "10%", "80%", "20%", "80%");
            window.event.returnValue = false;
        }

        function windowOpenEnumDataPage(EnumSort, LinkCode) {
            var P_Enum_Name = "";
            if (LinkCode != "") {
                P_Enum_Name = document.getElementById(LinkCode).value;
            }
            var url = "GZSZ/GuZhangMeiJu.aspx?ENUM_SORT=" + EnumSort + "&P_ENUM_NAME=" + encodeURI(P_Enum_Name);
            windowOpenPageByWidth(url, "枚举维护", "btn", "10%", "80%", "10%", "80%");
        }

        function windowOpenYYPage(EnumSort) {
            var lx = document.getElementById("GZLX").value;
            //var lxguid=document.getElementById("GZLX")
            var url = "XiuFuYuanYing.aspx?NAME=" + EnumSort + "&lx=" + encodeURI(lx);
            windowOpenPageByWidth(url, "故障原因", "", "10%", "80%", "10%", "80%");
            window.event.returnValue = false;
        }
        function windowOpenFFPage(EnumSort) {
            var url = "XiuFuChuLiFangFa.aspx?NAME=" + EnumSort;
            windowOpenPageByWidth(url, "处理方法", "", "10%", "80%", "10%", "80%");
            window.event.returnValue = false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" style="border-collapse: collapse;" border="1"
        bordercolor="#5b9ed1" width="100%">
        <tr>
            <td width="30%" class="tdBak" align="center">
                结单时间
            </td>
            <td>
                <asp:TextBox ID="JDSJ" runat="server" BorderStyle="None" Width="100%" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="30%" class="tdBak" align="center">
                *业务主体
            </td>
            <td>
                <asp:DropDownList ID="YWZT" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="YWLB_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="30%" class="tdBak" align="center">
                *业务类型
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="YWLB" runat="server" Width="100%" OnSelectedIndexChanged="GZZY_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="YWZT" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td width="30%" class="tdBak" align="center">
                *故障层次
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="GZCC" runat="server" Width="100%" AutoPostBack="True" onselectedindexchanged="GZCC_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="YWLB" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr  runat="server" id="tdGZLX">
            <td width="30%" class="tdBak" align="center">
                *故障类型
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="GZLX" runat="server" Width="100%" AutoPostBack="True" >
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="YWZT" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
         <tr runat="server" id="tdGZLX_YW">
            <td width="30%" class="tdBak" align="center">
                *故障类型（运维专用）
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="GZLX_YW" runat="server" Width="100%" >
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GZCC" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr runat="server" id="tdGZYY">
            <td width="30%" class="tdBak" align="center">
                *故障原因
            </td>
            <td>
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td style="width: 100%">
                            <asp:TextBox ID="GZYY" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td>
                            <img src="../Images/Small/bb_table.gif" onclick="windowOpenYYPage('GZYY')" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="30%" class="tdBak" align="center">
                *故障原因描述
            </td>
            <td>
                <asp:TextBox ID="ZJYY" runat="server" BorderStyle="None" Width="100%" Rows="2" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr id="tr_gzzt" runat="server">
            <td class="tdBak" align="center">
                *处理方法
            </td>
            <td id="td_gzzt" runat="server">
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td style="width: 100%">
                            <asp:DropDownList ID="GZCLFF" runat="server" Width="100%" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <img src="../Images/Small/bb_table.gif" onclick="windowOpenEnumDataPage('CLFF','')" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="30%" class="tdBak" align="center">
                *处理方法描述
            </td>
            <td>
                <asp:TextBox ID="GZFFMS" runat="server" BorderStyle="None" Width="100%" Rows="2"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="30%" class="tdBak" align="center">
                拥 有 人
            </td>
            <td align="left">
                <asp:Label ID="GZYYR" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="30%" class="tdBak" align="center">
                修复部门
            </td>
            <td>
                <asp:TextBox ID="XFBM" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                <asp:TextBox ID="XFBMCODE" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="30%" class="tdBak" align="center">
                修复人员
            </td>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" style="border-collapse: collapse;"
                    width="100%">
                    <tr>
                        <td width="100%">
                            <asp:TextBox ID="XFRY" runat="server" Width="100%" BorderStyle="None" AutoPostBack="True"></asp:TextBox>
                            <asp:TextBox ID="XFRYCODE" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                        </td>
                        <td width="1px">
                            <img src="../Images/Small/bb_table.gif" onclick="OpenUser('XFRY','XFRYCODE','XFBMCODE','XFBM');" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td width="30%" class="tdBak" align="center">
                修复通知
            </td>
            <td align="left">
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem>邮件通知</asp:ListItem>
                    <%--<asp:ListItem>手机短信通知</asp:ListItem>--%>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td class="tdBak" align="center" colspan="2">
                <asp:Button ID="BtnSave" runat="server" Text="保 存" CssClass="btn_2k3" OnClick="BtnSave_Click" /><asp:Button
                    ID="BtnCZ" runat="server" Text="重 置" CssClass="btn_2k3" OnClick="BtnCZ_Click" />
                <asp:Button ID="BtnGB" runat="server" Text="关 闭" CssClass="btn_2k3" OnClick="BtnGB_Click" /><asp:TextBox
                    ID="ZBGUID" runat="server" Style="display: none;"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Button ID="btn" runat="server" Text="Button" OnClick="btn_Click" Style="display: none" />
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
