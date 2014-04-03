<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceProperty.aspx.cs" Inherits="Web_Resource_ResourceProperty" %>

<%@ Register assembly="GroupingDropDownList" namespace="IDP.Web.UI" tagprefix="idp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="../../config.js"></script>
   
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="tableHead" width="60%">
                    <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" 
                        onclick="SaveButton_Click" Text="保存" />
                    <asp:Button ID="DeletButton" runat="server" CssClass="btn_2k3" Text="删除" 
                        onclick="DeletButton_Click" 
                        onclientclick="return confirm(&quot;确定删除吗?&quot;);" />
                </td>
                <td align="right" class="tableHead" style="padding-right: 5px">
                    <asp:TextBox 
                        ID="PROPERY_ID" runat="server" Width="45px" style="display:none;"></asp:TextBox>
                    <asp:TextBox 
                        ID="UNIT_ID" runat="server" Width="45px" style="display:none;"></asp:TextBox>
                         <asp:TextBox 
                        ID="TABLE_NAME" runat="server" Width="45px" style="display:none;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width:100%;" borderColor="#5b9ed1" border="1" cellpadding="2" 
                        cellspacing="0">
                        <tr>
                            <td class="tdBak"  style="width:15%;">
                                属性名称</td>
                            <td style="width:18%;">
                                <asp:TextBox ID="PROPERY_NAME" runat="server" BorderStyle="None" 
                                    BorderWidth="0px" Width="100%"></asp:TextBox>
                            </td>
                            <td class="tdBak" style="width:15%;">
                                字段名称</td>
                            <td style="width:18%;" align="left">
                                <asp:TextBox ID="FILED_NAME" runat="server" BorderStyle="None" 
                                    BorderWidth="0px" Width="100%"></asp:TextBox>
                            </td>
                            <td style="width:15%;" align="center" class="tdBak">
                                顺 序 号</td>
                            <td style="width:18%;" align="left">
                                <asp:TextBox ID="SEQUENCE" runat="server" BorderStyle="None" 
                                    BorderWidth="0px" Width="100%" onKeyPress="return limitNum(this);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdBak"  style="width:15%;">
                                数据类型</td>
                            <td style="width:18%;">
                                <asp:DropDownList ID="DATA_TYPE" runat="server" Width="100%">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>长文本</asp:ListItem>
                                    <asp:ListItem>中文本</asp:ListItem>
                                    <asp:ListItem>短文本</asp:ListItem>
                                    <asp:ListItem>枚举</asp:ListItem>
                                    <asp:ListItem>复选</asp:ListItem>
                                    <asp:ListItem>数字</asp:ListItem>
                                    <asp:ListItem>日期</asp:ListItem>
                                    <asp:ListItem>日期时间</asp:ListItem>                                    
                                    <asp:ListItem>组织机构</asp:ListItem>                                   
                                     <asp:ListItem>IP资源</asp:ListItem> 
                                     <asp:ListItem>VLAN资源</asp:ListItem>
                                     <asp:ListItem>资源选择</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="tdBak" style="width:15%;">
                                枚举数据编码</td>
                            <td style="width:18%;" align="left">
                                <asp:TextBox ID="ENUM_SORT" runat="server" BorderStyle="None" 
                                    BorderWidth="0px" Width="100%"></asp:TextBox>
                            </td>
                            <td style="width:15%;" align="center" class="tdBak">
                                包含枚举简称</td>
                            <td style="width:18%;" align="left">
                                    <asp:CheckBox ID="ISENUMSHORT" runat="server" Text=" " />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdBak"  style="width:15%;">
                                允许资源复选</td>
                            <td style="width:18%;" align="left">
                                    <asp:CheckBox ID="ISMULTISELECT" runat="server" Text=" " />
                            </td>
                            <td class="tdBak" style="width:15%;">
                                显示设备编码</td>
                            <td style="width:18%;" align="left">
                                    <asp:CheckBox ID="ISEQUCODE" runat="server" Text=" " />
                            </td>
                            <td style="width:15%;" align="center" class="tdBak">
                                关联资源编码</td>
                            <td style="width:18%;" align="left">
                                <asp:TextBox ID="LINKAGE_CODE" runat="server" BorderStyle="None" 
                                    BorderWidth="0px" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdBak">
                                查询字段</td>
                            <td align="left">
                                    <asp:CheckBox ID="ISQUERY" runat="server" Text=" " />
                            </td>
                            <td colspan="1tdBak" class="tdBak" valign="top" align="center">
                                标签列数</td>
                            <td align="left">
                                <asp:TextBox ID="COL_LABLE_NUM" runat="server" BorderStyle="None" BorderWidth="0px" 
                                    Width="100%" onKeyPress="return limitNum(this);"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                文本列数</td>
                            <td align="left">
                                <asp:TextBox ID="COL_TEXT_NUM" runat="server" BorderStyle="None" BorderWidth="0px" 
                                    Width="100%" onKeyPress="return limitNum(this);"></asp:TextBox>
                                        </td>
                        </tr>
                        <tr>
                            <td class="tdBak">
                                列表显示</td>
                            <td align="left">
                                    <asp:CheckBox ID="ISGRIDSHOW" runat="server" Text=" " /></td>
                            <td class="tdBak">
                                列显百分比</td>
                            <td>
                                <asp:TextBox ID="GRIDWIDTH" runat="server" BorderStyle="None" BorderWidth="0px" 
                                    Width="100%" onKeyPress="return limitNum(this);"></asp:TextBox>
                            </td>
                            <td class="tdBak">
                                列显对齐方式</td>
                            <td>
                                <asp:DropDownList ID="ALIGN_TYPE" runat="server" Width="100%">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="居左">居左</asp:ListItem>
                                    <asp:ListItem Value="居右">居右</asp:ListItem>
                                    <asp:ListItem Value="居中">居中</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdBak">
                                选择属性</td>
                            <td align="left" colspan="3">
                                    <asp:CheckBox ID="ISEMPTY" runat="server" Text="允许为空" />
                                    <asp:CheckBox ID="ISEDIT" runat="server" Text="允许编辑" />
                                    <asp:CheckBox ID="ISREPEAT" runat="server" Text="不允许重复" />
                            </td>
                            <td class="tdBak">
                                计算公式</td>
                            <td align="left">
                                <asp:TextBox ID="FORMULA" runat="server" BorderStyle="None" 
                                    BorderWidth="0px" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdBak">
                                特殊公式</td>
                            <td align="left" colspan="5">
                                    <asp:TextBox ID="TSGS" runat="server" Width="100%" TextMode="MultiLine"></asp:TextBox>
                                    <br />
                                    {0} 表示统计表达式,{1}表示主设备GUID</td>
                        </tr>
                        <tr>
                            <td class="tdBak">
                                可选资源</td>
                            <td align="left" colspan="5">
    
                            <asp:CheckBoxList ID="CH_CHILD_UNIT" runat="server" DataTextField="UNIT_NAME" 
                                DataValueField="UNIT_ID" RepeatColumns="9" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4">
                                    &nbsp;</td>
                            <td align="left">
                                    &nbsp;</td>
                            <td align="left">
                                    &nbsp;</td>
                        </tr>
                    </table>
                    </td>
                    </tr>
                    </table>
        <asp:TextBox ID="OLD_FILED_NAME" runat="server" style="display:none"></asp:TextBox>
    </div>
    </form>
</body>
</html>
