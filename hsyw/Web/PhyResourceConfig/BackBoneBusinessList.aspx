<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BackBoneBusinessList.aspx.cs" Inherits="Web_PhyResourceConfig_BackBoneBusinessList" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
  
        <table style="width:100%; height: 100%;">
            <tr>
                <td class="tableHead">
                    &nbsp;</td>
            </tr>
            <tr>
                <td height="1">
                    <table style="width:100%;BORDER-COLLAPSE:collapse;" cellpadding="1" cellspacing="0" border="1"  bordercolor = "#5b9ed1">
                        <tr>
                            <td align="center" class="tdBak" width="12%">
                                业务编码</td>
                            <td width="13%">
                                &nbsp;</td>
                            <td align="center" class="tdBak" width="12%">
                                业务分类</td>
                            <td width="13%">
                                &nbsp;</td>
                            <td align="center" class="tdBak" width="12%">
                                链路名称</td>
                            <td width="13%">
                                &nbsp;</td>
                            <td align="center" class="tdBak" width="12%">
                                完整纤号</td>
                            <td width="13%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak" width="12%">
                                甲端接入机房</td>
                            <td width="13%">
                                &nbsp;</td>
                            <td align="center" class="tdBak" width="12%">
                                甲端设备类型</td>
                            <td width="13%">
                                &nbsp;</td>
                            <td align="center" class="tdBak" width="12%">
                                甲端设备编号</td>
                            <td width="13%">
                                &nbsp;</td>
                            <td align="center" class="tdBak" width="12%">
                                甲端设备端口</td>
                            <td width="13%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak" width="12%">
                                乙端接入机房</td>
                            <td width="13%">
                                &nbsp;</td>
                            <td align="center" class="tdBak" width="12%">
                                乙端设备类型</td>
                            <td width="13%">
                                &nbsp;</td>
                            <td align="center" class="tdBak" width="12%">
                                乙端设备编号</td>
                            <td width="13%">
                                &nbsp;</td>
                            <td align="center" class="tdBak" width="12%">
                                乙端设备端口</td>
                            <td width="13%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak" width="12%">
                                启用时间</td>
                            <td width="13%">
                                &nbsp;</td>
                            <td align="center" class="tdBak" width="12%">
                                至</td>
                            <td width="13%">
                                &nbsp;</td>
                            <td align="center" class="tdBak" width="12%">
                                申请时间</td>
                            <td width="13%">
                                &nbsp;</td>
                            <td align="center" class="tdBak" width="12%">
                                至</td>
                            <td width="13%">
                                &nbsp;</td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td>
                                <asp:GridView ID="GridViewPhyResource" runat="server" SkinID="GridView1" 
                                     DataKeyNames="GUID"
                                    BorderStyle="None" BorderWidth="0px" AllowPaging="True" 
                                    AllowSorting="True"  
                                   >                                    
                                </asp:GridView>
                                </td>
            </tr>
            <tr>
                <td height="1">
                     <table class="tdBak" id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
							<tr>
								<td width="100%" style="padding-left:6px;"><font face="宋体">总共有
										<asp:label id="DataCountLab" runat="server" ForeColor="Red"></asp:label></font><font face="宋体">条记录，当前第
										<asp:label id="PageIndexLab" runat="server" ForeColor="Red"></asp:label>页，共
										<asp:label id="PageCountLab" runat="server" ForeColor="Red"></asp:label>页</font></td>
								<td width="1"><asp:label id="Label1" runat="server" Width="55px">单页显示</asp:label></td>
								<td width="1"><font face="宋体"><asp:dropdownlist id="PageSize" runat="server" 
                                        ForeColor="Red" Font-Bold="True" AutoPostBack="True"
											Width="60px" onselectedindexchanged="PageSize_SelectedIndexChanged">
											
											<asp:ListItem Value="50">50</asp:ListItem>
											<asp:ListItem Value="100">100</asp:ListItem>
											<asp:ListItem Value="200">200</asp:ListItem>
											<asp:ListItem Value="500">500</asp:ListItem>
										</asp:dropdownlist></font></td>
								<td width="1"><asp:linkbutton id="PrevButton" runat="server" ForeColor="#003797" 
                                        Width="50px" onclick="PrevButton_Click">上一页</asp:linkbutton></td>
								<td width="1"><asp:dropdownlist id="GridPageList" runat="server" ForeColor="Red" 
                                        Font-Bold="True" AutoPostBack="True"
										Width="50px" onselectedindexchanged="GridPageList_SelectedIndexChanged"></asp:dropdownlist></td>
								<td width="1"><asp:linkbutton id="NextButton" runat="server" ForeColor="#003797" 
                                        Width="50px" onclick="NextButton_Click">下一页</asp:linkbutton></td>
							</tr>
						</table></td>
            </tr>
        </table>
    </form>
</body>
</html>
