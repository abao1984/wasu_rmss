<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FAU_GRADE.aspx.cs" Inherits="Web_GZCL_GZSZ_FAU_GRADE" %>

<html>
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function OpenNew(ID, zy) {
            var str = window.showModalDialog("FAU_GRADE_Edit.aspx?GUID=" + ID, "", "dialogWidth:400px;dialogHeight:300px;center:yes;location:no;status:no;");
            //alert(window.event)
            if(window.event!=null) {
                window.event.returnValue = false;
            }
            document.getElementById("btnSX").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;
        height: 100%" width="100%">
        <tr>
            <td style="height: 1px">
                <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;
                    height: 1px" width="100%">
                    <tr>
                        <td class="tableHead">
                            <asp:Label ID="Label1" runat="server" Text="关键字" ForeColor="Black"></asp:Label>
                            <asp:TextBox ID="txtZYMC" runat="server"></asp:TextBox>
                            <asp:Button ID="BtnQuery" runat="server" CssClass="btn_2k3" Text=" 查 询 " 
                                onclick="BtnQuery_Click" />
                            <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" Text="新增等级" OnClientClick="OpenNew('')" />
                            <asp:Button ID="BtnDel" runat="server" CssClass="btn_2k3" ForeColor="Red" 
                                Text=" 删 除 " onclick="BtnDel_Click" OnClientClick="return confirm('确认要删除所选故障专业吗？');" />
                            <asp:Button ID="btnSX" runat="server" CssClass="btn_2k3" onclick="btnSX_Click"  style="display:none"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        SkinID="GridView1" DataKeyNames="GUID">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="CheckAll" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ItemCheckBox" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="等级名称">
                                <ItemTemplate>
                                    <a href="javascript:OpenNew('<%# Eval("GUID")%>','ZY');" style="text-decoration: underline;">
                                        <%# Eval("MC")%></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="等级描述" DataField="MS">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SJSC" HeaderText="升级时长（h）">
                            <ItemStyle Width="12%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CSSC" HeaderText="超时时长（h）">
                            <ItemStyle Width="12%" HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="是否启用">
                                <ItemTemplate>
                                    <asp:CheckBox ID="SFQY" runat="server" Checked='<%# Eval("SFQY").ToString()=="1"?true:false %>' Enabled="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:TemplateField>
                           
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
