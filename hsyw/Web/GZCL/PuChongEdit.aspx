<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PuChongEdit.aspx.cs" Inherits="Web_GZCL_PuChongEdit" %>

<html>
<head runat="server">
    <title></title>
    <meta http-equiv="expires" content="-1" />
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" style="border-collapse: collapse;" border="1"
        bordercolor="#5b9ed1" width="100%">
        <tr>
            <td width="20%" class="tdBak" align="center">
               业务主体
            </td>
            <td width="30%">
                <asp:DropDownList ID="YWZT" runat="server" Width="100%" 
                    AutoPostBack="True" onselectedindexchanged="YWZT_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td width="20%" class="tdBak" align="center">
                业务类型
            </td>
            <td width="30%">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="YWLB" runat="server" Width="100%" 
                            AutoPostBack="True" onselectedindexchanged="YWLB_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="YWZT" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td width="20%" class="tdBak" align="center">
               故障层次
            </td>
            <td width="30%">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="GZCC" runat="server" Width="100%" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="YWLB" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td width="20%" class="tdBak" align="center">
                故障类型
            </td>
            <td width="30%">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="GZLX" runat="server" Width="100%" 
                            AutoPostBack="True" onselectedindexchanged="GZLX_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="YWZT" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td width="20%" class="tdBak" align="center">
                故障原因
            </td>
            <td width="30%">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="GZYY" runat="server" Width="100%" AutoPostBack="True">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GZLX" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td width="20%" class="tdBak" align="center">
                故障等级
            </td>
            <td width="30%">
                <asp:DropDownList ID="GZDJ" runat="server" Width="100%" AutoPostBack="True">
                   
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="20%" class="tdBak" align="center">
                修复部门
            </td>
            <td width="30%">
                <asp:Label ID="XFBM" runat="server" Text=""></asp:Label>
            </td>
            <td width="20%" class="tdBak" align="center">
                修复人员
            </td>
            <td width="30%">
                <asp:Label ID="XFRY" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="tdBak" align="center">
                解决方法
            </td>
            <td colspan="3">
                <asp:Label ID="JJBF" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="tdBak" align="center">
                总结原因
            </td>
            <td colspan="3">
                <asp:Label ID="ZJYY" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="tdBak" align="center">
                补充意见人
            </td>
            <td width="30%">
                <asp:Label ID="GZPCR" runat="server" Text=""></asp:Label>
            </td>
            <td width="20%" class="tdBak" align="center">
                处理时间
            </td>
            <td width="30%">
                <asp:Label ID="GZPCSJ" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="tdBak" align="center">
                补充意见
            </td>
            <td colspan="3">
                <asp:TextBox ID="BCYJ" runat="server" BorderStyle="None" Width="100%" Rows="3" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center" class="tdBak">
                <asp:Button ID="BtnCL" runat="server" Text="故障补充" class="btn_2k3" OnClick="BtnCL_Click" />
                <asp:Button ID="BtnQX" runat="server" Text=" 取 消 " class="btn_2k3" OnClick="BtnQX_Click" />
                <asp:TextBox ID="ZBGUID" runat="server" Style="display: none"></asp:TextBox>
                <asp:TextBox ID="GUID" runat="server" Style="display: none"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
