<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeiXingEdit.aspx.cs" Inherits="Web_GZCL_GZSZ_LeiXingEdit" %>

<html>
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="expires" content="-1" />
    <base target="_self" />
    <script>
     var code=null;
		function CheckAndGetCode(obj)
		{
		    if(obj.value=="")
		    {
		       document.getElementById("CODEID").value="";
		       return;
		    }
		     
		    code=ShareGZCL.GetModuleCode(obj.value).value;
		    document.getElementById("CODEID").value = code;		   
		}
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" style="border-collapse: collapse;" border="1"
        bordercolor="#5b9ed1" width="100%">
        <%--<tr>
            <td width="30%" class="tdBak" align="center">
                类型编码
            </td>
            <td>
                <asp:TextBox ID="CODEID" runat="server" BorderStyle="None" Width="100%"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
            <td width="30%" class="tdBak" align="center">
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="CODENAME" runat="server" BorderStyle="None" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="30%" class="tdBak" align="center">
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="MS" runat="server" BorderStyle="None" Width="100%" Rows="4" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="30%" class="tdBak" align="center">
                业务类别
            </td>
            <td>
                <asp:DropDownList ID="PARENT_SPECIALTY" runat="server" Width="100%" 
                    AutoPostBack="True" onselectedindexchanged="PARENT_SPECIALTY_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="30%" class="tdBak" align="center">
                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="PARENT_NAME" runat="server" Width="100%">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="PARENT_SPECIALTY" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td width="30%" class="tdBak" align="center">
                是否可用
            </td>
            <td>
                <asp:RadioButtonList ID="SFQY" runat="server" RepeatDirection="Horizontal" Width="20%">
                    <asp:ListItem Value="1">是</asp:ListItem>
                    <asp:ListItem Value="0">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="tdBak" align="center" colspan="2">
                <asp:Button ID="BtnSave" runat="server" Text="保 存" CssClass="btn_2k3" OnClick="BtnSave_Click" /><asp:Button
                    ID="BtnCZ" runat="server" Text="重 置" CssClass="btn_2k3" OnClick="BtnCZ_Click" />
                <asp:Button ID="BtnGB" runat="server" Text="关 闭" CssClass="btn_2k3" OnClick="BtnGB_Click" />
                <asp:TextBox ID="GUID" runat="server" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="LB" runat="server" Style="display: none;"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
