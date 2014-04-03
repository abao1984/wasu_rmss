<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuZhangChuLi.aspx.cs" Inherits="Web_GZCL_GuZhangChuLi" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>故障处理</title>
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

        function OpenBranch(name, code) {
            windowOpenPageByWidth("GZSZ/BranchsTree.aspx?NAME=" + name + "&CODE=" + code , "选择所属区域", "", "10%", "80%", "10%", "80%");
            window.event.returnValue = false;
            
        }

        function OpenUser(name, code, bmCode,bm,rybm) {
            //var codes = document.getElementById(bmCode).value;
            var clType = document.getElementById("CLtype").value;
            var url = "GZSZ/UserList.aspx?NAME=" + name + "&CODE=" + code + "&Branch=" + bmCode + "&BMNAME=" + bm + "&RYBM=" + rybm;
            if (clType == "YJ") {
                url += "&type=yj";
            }
            windowOpenPageByWidth(url, "选择用户", "", "5%", "90%", "10%", "80%");
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
                处理部门
            </td>
            <td>
                <asp:Label ID="CLBM" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdBak" align="center">
                处理人员
            </td>
            <td>
                <asp:Label ID="CLR" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <%--<tr id="t_yjbm" runat="server" style="display: none">
            <td class="tdBak" align="center">
                主送部门
            </td>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" style="border-collapse: collapse;" width="100%">
                    <tr>
                        <td width="100%">
                            <asp:TextBox ID="YJBM" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                            <asp:TextBox ID="YJBMCODE" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                        </td>
                        <td width="1px">
                            <img src="../Images/Small/bb_table.gif" onclick="OpenBranch('YJBM','YJBMCODE');" />
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>--%>
        <tr id="t_yjr" runat="server" style="display: none">
            <td class="tdBak" align="center">
                                主送
            </td>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" style="border-collapse: collapse;" width="100%">
                    <tr>
                        <td width="100%">
                             <asp:TextBox ID="YJRYBM" runat="server" Width="100%" BorderStyle="None" ></asp:TextBox>
                            <asp:TextBox ID="YJR" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                            <asp:TextBox ID="YJRID" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                        </td>
                        <td width="1px">
                            <img src="../Images/Small/bb_table.gif" onclick="OpenUser('YJR','YJRID','YJBMCODE','YJBM','YJRYBM');"/>
                             <asp:TextBox ID="YJBMCODE" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                             <asp:TextBox ID="YJBM" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%--<tr id="tr_csbm" runat="server" style="display: none">
            <td class="tdBak" align="center">
                抄送部门
            </td>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" style="border-collapse: collapse;"  width="100%">
                    <tr>
                        <td width="100%">
                            <asp:TextBox ID="CSBM" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                            <asp:TextBox ID="CSBMCODE" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                        </td>
                        <td width="1px">
                            <img src="../Images/Small/bb_table.gif" onclick="OpenBranch('CSBM','CSBMCODE');" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
        <tr id="tr_csry" runat="server" style="display: none">
            <td class="tdBak" align="center">
                                抄送
            </td>
            <td>
                <table cellpadding="0" cellspacing="0" border="0" style="border-collapse: collapse;" width="100%">
                    <tr>
                        <td width="100%">
                            <asp:TextBox ID="CSRYBM" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                            <asp:TextBox ID="CSRNAME" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                            <asp:TextBox ID="CSRID" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                             <asp:TextBox ID="CSBMCODE" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                             <asp:TextBox ID="CSBM" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                        </td>
                        <td width="1px">
                            <img src="../Images/Small/bb_table.gif"  onclick="OpenUser('CSRNAME','CSRID','CSBMCODE','CSBM','CSRYBM');"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="t_gzyj" runat="server" style="display: none">
            <td class="tdBak" align="center">
                故障移交通知
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="邮件通知">邮件通知</asp:ListItem>
                  <%--  <asp:ListItem Value="手机短信通知">手机短信通知</asp:ListItem>--%>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" class="tdBak">
                <asp:Button ID="BtnCL" runat="server" Text="" class="btn_2k3" OnClick="BtnCL_Click" />
                <asp:Button ID="BtnQX" runat="server" Text=" 取 消 " class="btn_2k3" OnClick="BtnQX_Click" />
                <asp:TextBox ID="ZBGUID" runat="server" Style="display: none"></asp:TextBox>
                <asp:TextBox ID="GUID" runat="server" Style="display: none"></asp:TextBox>
                <asp:TextBox ID="CLtype" runat="server" Style="display: none"></asp:TextBox>
                <asp:Button ID="btn" runat="server" Text="Button" onclick="btn_Click"  Style="display: none"/>
            </td>
        </tr>
    </table>
     <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
