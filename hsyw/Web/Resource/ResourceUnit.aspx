<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResourceUnit.aspx.cs" Inherits="Web_Resource_ResourceUnit" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><% =Session["PageSubTite"].ToString().Trim() + " - " + Session["PageTitle"].ToString().Trim() + " - " + Session["CopyRightAuthor"].ToString().Trim()%></title>
  <script language="javascript" type="text/javascript" src="../../config.js"></script>
 <script language="javascript" type="text/javascript">
     function windowOpen(Property_Id) {
         var unit_id = document.getElementById("UNIT_ID").value;
         var table_name = document.getElementById("TABLE_NAME").value;
         var url = "ResourceProperty.aspx?PROPERY_ID=" + Property_Id + "&UNIT_ID=" + unit_id + "&TABLE_NAME=" + table_name;
         windowOpenPage(url, "物理资源定义", "Btn");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
 <table border="0" cellpadding="0" cellspacing="0"     style=" width: 100%;height:100%">
 <tr>
 <td style="height:1px;">
 
    <table border="0" cellpadding="0" cellspacing="0"     style=" width: 100%;">
        <tr>
            <td align="left" class="tableHead">
                <asp:Button ID="AddButton" runat="server" CssClass="btn_2k3" 
                    onclick="AddButton_Click" Text="新增同级单元" />
                <asp:Button ID="AddSubButton" runat="server" CssClass="btn_2k3" 
                    onclick="AddSubButton_Click" Text="新增子单元" />
                <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" 
                    onclick="SaveButton_Click" Text="保存" />
                <asp:TextBox ID="UNIT_ID" runat="server" Width="24px"  style="display:none;"></asp:TextBox>
                <asp:TextBox ID="PARENT_UNIT_ID" runat="server" Width="24px"  style="display:none;"></asp:TextBox>
                <asp:Button ID="Btn" runat="server" onclick="Btn_Click" style="display:none;" />
            </td>
        </tr>
        <tr>
            <td align="left">
                <table style="width:100%;" border="1" cellpadding="3" cellspacing="0"  borderColor="#5b9ed1" >
                    <tr>
                        <td class="tdBak"  align="center" >
                资源名称</td>
                        <td  >
                <asp:TextBox ID="UNIT_NAME" runat="server" 
                    Width="100%" BorderStyle="None" BorderWidth="0px"></asp:TextBox>
                        </td>
                        <td class="tdBak"  align="center">
                数据表名称</td>
                        <td colspan="3">
                <asp:TextBox ID="TABLE_NAME" runat="server" BorderStyle="None" BorderWidth="0px" 
                    Width="100%"></asp:TextBox>                       
                        </td>
                        <td   align="center" class="tdBak">
                顺序号</td>
                        <td  >
                <asp:TextBox ID="SEQUENCE" runat="server" BorderStyle="None" BorderWidth="0px" 
                    Width="100%"  onKeyPress="return limitNum(this);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center" style="width:10%;">
                            界面列总数</td>
                        <td style="width:15%;">
                <asp:TextBox ID="COLCOUNT" runat="server" BorderStyle="None" BorderWidth="0px" 
                    Width="100%"  onKeyPress="return limitNum(this);"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center" style="width:10%;">
                            查询列总数</td>
                        <td style="width:15%;">
                <asp:TextBox ID="Q_COLCOUNT" runat="server" BorderStyle="None" BorderWidth="0px" 
                    Width="100%"  onKeyPress="return limitNum(this);"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center" style="width:10%;">是否建表</td>
                        <td style="width:15%;">
    
                            <asp:CheckBox ID="IS_CREATE_TABLE" runat="server" Text=" " />
                        </td>
                        <td align="center" class="tdBak" style="width:10%;"> 是否显示</td>
                        <td style="width:15%;">    
                            <asp:CheckBox ID="ISSHOW" runat="server" Text=" " />
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            设备名称字段</td>
                        <td>
                <asp:TextBox ID="NAME_FILED" runat="server" 
                    Width="100%" BorderStyle="None" BorderWidth="0px"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center">
                            &nbsp;设备编码字段</td>
                        <td>
                <asp:TextBox ID="EQU_CODE_FILED" runat="server" 
                    Width="100%" BorderStyle="None" BorderWidth="0px"></asp:TextBox>
                        </td>
                        <td  class="tdBak"  align="center">设备状态字段</td>
                        <td>    
                <asp:TextBox ID="EQU_STATE_FILED" runat="server" 
                    Width="100%" BorderStyle="None" BorderWidth="0px"></asp:TextBox>
                        </td>
                        <td colspan="1" align="center" class="tdBak">    
                            &nbsp;</td>
                        <td>    
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            列表编辑</td>
                        <td>
                            <asp:CheckBox ID="IS_GRID_EDIT" runat="server" Text=" " />
                        </td>
                        <td class="tdBak" align="center">
                            列表模式</td>
                        <td>    
                            <asp:DropDownList ID="GRID_MODE" runat="server" Width="100%">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>含下级所有属性</asp:ListItem>
                                <asp:ListItem>含下级分类名称</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td  class="tdBak"  align="center">编码方式</td>
                        <td>
    
                            <asp:DropDownList ID="CODE_MODE" runat="server" Width="100%">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>数字编码</asp:ListItem>
                                <asp:ListItem>字母编码</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="1" align="center" class="tdBak">    
                                                        编码字段</td>
                        <td>    
    
                <asp:TextBox ID="CODE_FILED" runat="server" BorderStyle="None" BorderWidth="0px" 
                    Width="100%"  ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td class="tdBak" align="center">
                            默认排序</td>
                        <td colspan="3">    
                <asp:TextBox ID="SEQUENCE_FILED" runat="server" BorderStyle="None" BorderWidth="0px" 
                    Width="100%" style="margin-bottom: 0px"  ></asp:TextBox>
                        </td>
                        <td colspan="1" align="center" class="tdBak">    
                                                        &nbsp;</td>
                        <td>    
    
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            下属资源</td>
                        <td colspan="7">
                            <asp:CheckBoxList ID="CH_CHILD_UNIT" runat="server" DataTextField="UNIT_NAME" 
                                DataValueField="UNIT_ID" RepeatColumns="9" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
        </table>
        </td>
 </tr>
 <tr>
 <td>

 <div id="PhyResourceDIV" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%" align="center" runat="server"  >
    <asp:GridView ID="GridView_PROPERY" runat="server"  SkinID="GridView1"  DataKeyNames="PROPERY_ID" 
        onrowdatabound="GridView_PROPERY_RowDataBound">
        <RowStyle BorderColor="#5B9ED1" BorderWidth="1px" BorderStyle="Solid" />
        <Columns>
            <asp:BoundField DataField="SEQUENCE" HeaderText="顺序号">
            <HeaderStyle BorderColor="#5B9ED1"  BorderWidth="1"/>
            <ItemStyle BorderColor="#5B9ED1"  BorderWidth="1" HorizontalAlign="Center"/>
            </asp:BoundField>
            <asp:BoundField DataField="PROPERY_NAME" HeaderText="属性名称" >
            <HeaderStyle BorderColor="#5B9ED1"  BorderWidth="1"/>
            <ItemStyle BorderColor="#5B9ED1"  BorderWidth="1" HorizontalAlign="Center"/>
            </asp:BoundField>
            <asp:BoundField DataField="DATA_TYPE" HeaderText="数据类型" >
            <HeaderStyle BorderColor="#5B9ED1"  BorderWidth="1"/>
            <ItemStyle BorderColor="#5B9ED1"  BorderWidth="1"  HorizontalAlign="Center"/>
            </asp:BoundField>
             <asp:BoundField DataField="FILED_NAME" HeaderText="字段名称" >
            <HeaderStyle BorderColor="#5B9ED1"  BorderWidth="1"/>
            <ItemStyle BorderColor="#5B9ED1"  BorderWidth="1"  HorizontalAlign="Center"/>
            </asp:BoundField>
           
            <asp:BoundField DataField="ISGRIDSHOW" HeaderText="列表显示" >
             <HeaderStyle BorderColor="#5B9ED1"  BorderWidth="1"/>
            <ItemStyle BorderColor="#5B9ED1"  BorderWidth="1"  HorizontalAlign="Center"/>
            </asp:BoundField>
            <asp:BoundField DataField="ISQUERY" HeaderText="查询字段" >
             <HeaderStyle BorderColor="#5B9ED1"  BorderWidth="1"/>
            <ItemStyle BorderColor="#5B9ED1"  BorderWidth="1"  HorizontalAlign="Center"/>
            </asp:BoundField>
           <asp:BoundField DataField="ISEMPTY" HeaderText="允许为空" >
             <HeaderStyle BorderColor="#5B9ED1"  BorderWidth="1"/>
            <ItemStyle BorderColor="#5B9ED1"  BorderWidth="1"  HorizontalAlign="Center"/>
            </asp:BoundField>
            <asp:BoundField HeaderText="详细">
            <HeaderStyle BorderColor="#5B9ED1"  BorderWidth="1"/>
            <ItemStyle BorderColor="#5B9ED1"  BorderWidth="1"  HorizontalAlign="Center"/>
            </asp:BoundField>           
        </Columns>
    </asp:GridView>
</div>
    </td>
 </tr>
 </table>  
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
