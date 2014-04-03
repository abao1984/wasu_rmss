<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogicResourceIpSelect.aspx.cs" Inherits="Web_Resource_LogicResourceIpSelect" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script language="javascript" type="text/javascript" src="ResourceScript.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width:100%; height: 100%;">        
        <tr>
            <td style="height:1px;">
                <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td class="tableHead" colspan="6">
                            <asp:Button ID="OKButton" runat="server" CssClass="btn_2k3" 
                                onclick="OKButton_Click" Text="确定" />
                            <asp:Button ID="QueryButton" runat="server" CssClass="btn_2k3" 
                                onclick="QueryButton_Click" Text="查询" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="13%" class="tdBak">
                            业务大类</td>
                        <td align="center" width="20%">
                            <asp:DropDownList ID="YWDL" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td align="center" width="13%" class="tdBak">
                            业务类型</td>
                        <td width="20%">
                                        <asp:DropDownList ID="IPYWLX" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                      <td align="center" class="tdBak"  width="13%">
                            是否全网</td>
                        <td colspan="1"  width="20%">
                          
                            <asp:CheckBox ID="SFQW" runat="server" />
                          
                        </td>
                    </tr>
                    <tr>
                      <td align="center" width="13%" class="tdBak">
                            所属区域</td>
                        <td width="20%">
                            <table style="width:100%;">
                                <tr>
                                    <td  style="width:100%;">
                                        <asp:TextBox ID="SSQY" runat="server" Width="100%" BackColor="#F0F0F0" 
                                            BorderStyle="None"></asp:TextBox>
                                    </td>
                                    <td width="1">
                                       <img align='right' src='../Images/Small/bb_table.gif' onclick="windowOpenBranchTree('SSQY','SSQY_CODE')" /></td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak">所属机房</td>
                        <td align="center">
                             <table style="width:100%;">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="HOUSE_NAME" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="1">
                                        <img src="../Images/Small/bb_table.gif"  onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','HOUSE_NAME','SSQY','HOUSE_AREA','1')"/></td>
                                </tr>
                            </table></td>
                        <td align="center" class="tdBak">
                            IP地址段</td>
                        <td colspan="1">
                          
                            <asp:TextBox ID="IPDZ" runat="server" Width="100%"></asp:TextBox>
                          
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td>
            <div id="LogicEquIPDIV" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%" align="center" runat="server" >
                <asp:GridView ID="GridView1" runat="server" SkinID="GridView1" 
                    BorderWidth="1px" AllowPaging="True" AllowSorting="True" CellSpacing="1" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="选择">
                            <ItemTemplate>
                                <asp:CheckBox ID="XZ" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="XH" HeaderText="序号" />
                        <asp:BoundField DataField="IP" HeaderText="IP" />
                    </Columns>
                </asp:GridView>
                </div></td>
        </tr>
         <tr>
            <td style="height: 1px">   <table class="tdBak" id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td width="100%" style="padding-left: 6px;">
                            <font face="宋体">总共有<asp:Label ID="DataCountLab" runat="server" ForeColor="Red"></asp:Label></font><font
                                    face="宋体">条记录，当前第
                                    <asp:Label ID="PageIndexLab" runat="server" ForeColor="Red"></asp:Label>页，共
                                    <asp:Label ID="PageCountLab" runat="server" ForeColor="Red"></asp:Label>页</font>
                        </td>
                        <td width="1">
                            <asp:Label ID="Label1" runat="server" Width="55px">单页显示</asp:Label>
                        </td>
                        <td width="1">
                            <font face="宋体">
                                <asp:DropDownList ID="PageSize" runat="server" ForeColor="Red" Font-Bold="True" AutoPostBack="True"
                                    Width="60px" OnSelectedIndexChanged="PageSize_SelectedIndexChanged">
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="500">500</asp:ListItem>
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
                </table> </td></tr>
    </table>
      <asp:TextBox ID="HOUSE_NAME_GUID" runat="server"  style="display:none;"></asp:TextBox>
      <asp:TextBox ID="HOUSE_NAME_CODE" runat="server"  style="display:none;"></asp:TextBox>
      <asp:TextBox ID="SSQY_CODE" runat="server"  style="display:none;"></asp:TextBox>
      <asp:TextBox ID="NAME" runat="server"  style="display:none;"></asp:TextBox>
        <uc1:windowHeader ID="windowHeader1" runat="server" />
    
    </form>
</body>
</html>
