<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuoDanTongJiMingXi.aspx.cs"
    Inherits="Web_GZCL_GZBB_GuoDanTongJiMingXi" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../../calendar.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript">
        function test(obj) {
            obj.options.length = 0;
            var sj1 = document.getElementById("TSSJ1").value;
            var sj2 = document.getElementById("TSSJ2").value;

            var ry = Web_GZCL_GZBB_GuoDanTongJiMingXi.GetTJRY(sj1, sj2).value;
            var tjry = ry.split(",");
            var varItem = new Option("全部", "");
            obj.options.add(varItem);
            for (var i = 0; i <= tjry.length; i++) {
                varItem = new Option(tjry[i], tjry[i]);
                obj.options.add(varItem);
            }
        }
        function test1() {
            var rtl = document.getElementById("drpTJRY");
            document.getElementById("txtBMRY").value = rtl.options[rtl.selectedIndex].value;
        }

        function test2() {
            //alert(1);
            var str = document.getElementById("tb_dhgdl").innerHTML;
            str = "<table border='1' bordercolor='black'>" + str + "</table>";
            //alert(str);
            document.getElementById("TextBox1").value = str;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%; height: 100%;">
        <tr>
            <td height="1">
                <table style="border-collapse: collapse; width: 100%" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td width="12%" class="tdBak" align="center">
                            时间
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="TSSJ1" runat="server" onfocus="setDayH(this);" BorderStyle="None"
                                Width="140px"></asp:TextBox>
                        </td>
                        <td width="12%" class="tdBak" align="center" runat="server" id="time1">
                            至
                        </td>
                        <td width="13%" runat="server" id="time2">
                            <asp:TextBox ID="TSSJ2" runat="server" onfocus="setDayH(this);" BorderStyle="None"
                                Width="140px"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="right">
                            <asp:Button ID="Button1" runat="server" Text="生成报表" CssClass="btn_2k3" OnClick="Button1_Click"
                                onMouseOver="test1()" />
                            <asp:Button ID="Button2" runat="server" Text="导出Excel" CssClass="btn_2k3" OnClick="Button2_Click"
                                OnClientClick="test2()" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak">
                            部门人员
                        </td>
                        <td class="tdBak" width="13%">
                            <%-- <select name="TJRY" runat="server" size="1" id="TJRY" onMouseOver="test(this)" ></select>--%>
                            <asp:DropDownList ID="drpTJRY" runat="server" Width="100%" onMouseOver="test(this)">
                            </asp:DropDownList>
                        </td>
                        <td align="center" class="tdBak">
                            业务主体
                        </td>
                        <td class="tdBak" width="13%">
                            <asp:DropDownList ID="dropYWZT" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="dropYWZT_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="tdBak" align="center">
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            业务类型
                        </td>
                        <td colspan="6" class="tdBak">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="6">
                                    </asp:CheckBoxList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dropYWZT" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="dd" runat="server" style="height: 100%" valign="top">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table runat="server" id="tb_dhgdl" style="border-collapse: collapse;" cellpadding="1"
                            cellspacing="0" border="1" bordercolor="#5b9ed1">
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <div  style="display:none">
        <asp:TextBox ID="txtBMRY" runat="server"></asp:TextBox>
        <asp:Button ID="Button3" runat="server" Text="Button" OnClick="Button3_Click" />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
