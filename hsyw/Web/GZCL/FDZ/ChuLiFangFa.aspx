<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChuLiFangFa.aspx.cs" Inherits="Web_GZCL_FDZ_ChuLiFangFa" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <script language="javascript" type="text/javascript">
         function OpenNew(ID) {
             var zy = document.getElementById("types").value;
             var str = window.showModalDialog("ChuLiFaFangEdit.aspx?GUID=" + ID + "&LB=" + zy, "", "dialogWidth:400px;dialogHeight:300px;center:yes;location:no;status:no;");
             if (window.event != null) {
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
                            <asp:Button ID="BtnQuery" runat="server" CssClass="btn_2k3" Text=" 查 询 " OnClick="BtnQuery_Click" />
                            <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" Text="新增处理方法" OnClientClick="OpenNew('')" />
                            <asp:Button ID="BtnDel" runat="server" CssClass="btn_2k3" ForeColor="Red" Text=" 删 除 "
                                OnClick="BtnDel_Click" OnClientClick="return confirm('确认要删除所选记录吗？');" />
                            <asp:Button ID="btnSX" runat="server" CssClass="btn_2k3" OnClick="btnSX_Click" Style="display: none" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        SkinID="GridView1" DataKeyNames="GUID" 
                        onrowdatabound="GridView1_RowDataBound" AllowPaging="True">
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
                            <asp:BoundField HeaderText="名称" DataField="CODENAME">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="描述" DataField="MS">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="业务类别" DataField="PARENT_NAME">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center"  Width="12%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="是否启用">
                                <ItemTemplate>
                                    <asp:CheckBox ID="SFQY" runat="server" Checked='<%# Eval("SFQY").ToString()=="1"?true:false %>'
                                        Enabled="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td height="1">
                <table class="tdBak" id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td width="100%" style="padding-left: 6px;">
                            <font face="宋体">总共有
                                <asp:Label ID="DataCountLab" runat="server" ForeColor="Red"></asp:Label></font><font
                                    face="宋体">条记录，当前第
                                    <asp:Label ID="PageIndexLab" runat="server" ForeColor="Red"></asp:Label>页，共
                                    <asp:Label ID="PageCountLab" runat="server" ForeColor="Red"></asp:Label>页</font>
                        </td>
                        <td width="1">
                            <asp:Label ID="Label2" runat="server" Width="55px">单页显示</asp:Label>
                        </td>
                        <td width="1">
                            <font face="宋体">
                                <asp:DropDownList ID="PageSize" runat="server" ForeColor="Red" Font-Bold="True" AutoPostBack="True"
                                    Width="60px" OnSelectedIndexChanged="PageSize_SelectedIndexChanged">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                            </font>
                        </td>
                        <td width="1">
                            <asp:LinkButton ID="PrevButton" runat="server" ForeColor="#003797" Width="50px" OnClick="PrevButton_Click">上一页</asp:LinkButton>
                        </td>
                        <td width="1">
                            <asp:DropDownList ID="GridPageList" runat="server" ForeColor="Red" Font-Bold="True"
                                AutoPostBack="True" Width="50px" OnSelectedIndexChanged="GridPageList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="1">
                            <asp:LinkButton ID="NextButton" runat="server" ForeColor="#003797" Width="50px" OnClick="NextButton_Click">下一页</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="types" runat="server" style="display:none" Text="clff"></asp:TextBox>
    </form>
</body>
</html>

