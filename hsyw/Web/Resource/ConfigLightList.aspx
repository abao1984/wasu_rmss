<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigLightList.aspx.cs"
    Inherits="Web_Resource_ConfigLightList" %>

<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function windowOpen(guid, ywlx) {
            var url = "";
            if (ywlx == "vpn") {
                url = "ConfigLightEditVpn.aspx?YWGUID=" + guid;
            }
            else if (ywlx == "gg") {
                url = "ConfigLightEditBone.aspx?YWGUID=" + guid;
            }
            else {
                url = "ConfigLightEdit.aspx?YWGUID=" + guid;
            }
            windowOpenPage(url, "光缆资源配置", "BtnQuery");
            window.event.returnValue = false;
        }

        function windowOpenRmssSelect() {
            var url = "RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID";
            windowOpenPage(url, "选择客户资源", "BtnRmss");
        }
        
        function windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, isEqucode) {
            var txt_code = txt_name + "_CODE";
            var txt_guid = txt_name + "_GUID";
            var res_guid = "";
            var res_code = "";
            var res_name = "";
            if (linkage_code != "") {
                res_guid = document.getElementById(p_txt_name + "_GUID").value;
                if (isEqucode == "1") {
                    res_code = document.getElementById(p_txt_name + "_CODE").value;
                }
                res_name = document.getElementById(p_txt_name).value;
            }
            var url = "PhyEquSelect.aspx?ISEQUCODE=" + isEqucode + "&UNIT_ID=" + unit_id + "&TXT_NAME=" + txt_name + "&TXT_CODE=" + txt_code + "&TXT_GUID=" + txt_guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name) + "&NAME_FILED=" + linkage_code + "&P_TXT_NAME=" + p_txt_name; ;
            windowOpenPage(url, "资源选择", "");     
        }
        function windowOpenEqu(txt_fl, txt_name, p_txt_name, linkage_code) {
            var unit_id = document.getElementById(txt_fl).value;
            windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, '1')
        }
        function OpenSelect(enumtype, pname, ddlid) {
            var url = "ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
            windowOpenPageByWidth(url, "枚举维护", "Btn", "30%", "40%", "10%", "80%");
            document.getElementById("DDLID").value = ddlid;
            document.getElementById("DDLLX").value = enumtype;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%;">
        <tr>
            <td class="tableHead">
                <asp:Button ID="BtnQuery" runat="server" Text="查询" CssClass="btn_2k3" OnClick="BtnQuery_Click" />
                <asp:Button ID="BtnAdd" runat="server" Text="新增" CssClass="btn_2k3" OnClick="BtnAdd_Click"
                    OnClientClick="windowOpen('','')" />
                <asp:Button ID="BtnDel" runat="server" Text="删除" OnClientClick="return confirm('确定要删除吗？')"
                    CssClass="btn_2k3" OnClick="BtnDel_Click" />
                    <asp:Button ID="BtnRmss" runat="server" onclick="BtnRmss_Click"  style="display:none" />
                <asp:Button ID="BtnExport" runat="server" Text="导出Excel" CssClass="btn_2k3" 
                    onclick="BtnExport_Click" />
            </td>
        </tr>
        <tr>
            <td height="1">
                <table style="width: 100%; border-collapse: collapse;" cellpadding="0" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            业务编码
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="SUBSCRIBER_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick=" windowOpenRmssSelect()" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            业务类型
                        </td>
                        <td width="13%">
                            <asp:DropDownList ID="YWLX" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            客户类别
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:DropDownList ID="CUSTTYPE" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenSelect('KHLB','','KHLB')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            客户名称
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="CUSTOMER_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            机房编码
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="JFMC_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            机房名称
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="JFMC" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','JFMC','','','1')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            光缆段编码
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="GLDMC_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            光缆段名称
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="GLDMC" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('53d347a2-5be1-4b85-af53-ffd35b3ccfc7','GLDMC','JFMC','QSD','1')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            设备名称</td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="SBMC" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenEqu('JRSB_UNIT_ID','SBMC','JFMC','HOUSE_NAME','1')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            设备端口</td>
                        <td width="13%">
                             <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="SBDK" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('bfc13d2d-eab8-4784-a96a-b8ffc21b4e88','SBDK','SBMC','EQU_NAME','0')"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                       
                        <td align="center" class="tdBak" width="12%">
                            纤芯号
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="GXH" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('3b5bfe00-e5dd-4f02-bf41-b347ca9c7624','GXH','GLDMC','GLDMC','0')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdBak" colspan="2"></td>
                    </tr>
                    <tr>
                         <td align="center" class="tdBak" width="12%">
                            客户地址
                        </td>
                        <td width="13%" colspan="5">
                            <asp:TextBox ID="ADDRESS" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td class="tdBak" colspan="2"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%" valign="top">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="gvLightList" runat="server" SkinID="GridView1" DataKeyNames="YWGUID"
                        Width="100%" BorderColor="#5B9ED1" BorderWidth="1px" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" OnRowDataBound="gvLightList_RowDataBound" 
                        onsorting="gvLightList_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="5%" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="SUBSCRIBER_CODE" HeaderText="业务编码" ItemStyle-BorderColor="#5b9ed1"  SortExpression="SUBSCRIBER_CODE">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="YWLX" HeaderText="业务类型" ItemStyle-BorderColor="#5b9ed1" SortExpression="YWLX">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CUSTTYPE" HeaderText="客户类别" ItemStyle-BorderColor="#5b9ed1" SortExpression="CUSTTYPE">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SUB_NAME" HeaderText="用户名称" ItemStyle-BorderColor="#5b9ed1" SortExpression="SUB_NAME">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ADDRESS" HeaderText="用户地址" ItemStyle-BorderColor="#5b9ed1" SortExpression="ADDRESS">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JFMC" HeaderText="机房名称" SortExpression="JFMC">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
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
                            <asp:Label ID="Label1" runat="server" Width="55px">单页显示</asp:Label>
                        </td>
                        <td width="1">
                            <font face="宋体">
                                <asp:DropDownList ID="PageSize" runat="server" ForeColor="Red" Font-Bold="True" AutoPostBack="True"
                                    Width="60px" OnSelectedIndexChanged="PageSize_SelectedIndexChanged">
                                    
                                    <asp:ListItem Value="50">50</asp:ListItem>
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
                </table>
            </td>
        </tr>
    </table>
    <asp:Button ID="Btn1" runat="server" Text="" onclick="Btn1_Click" Style="display: none"/>
    <asp:TextBox ID="ZYHS_BJ" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="GXH_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="GLDMC_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JFMC_GUID" runat="server" Style="display: none"></asp:TextBox> 
    <asp:TextBox ID="DDLID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="SUBSCRIBER_ID" runat="server" Width="0px" Style="display:none"></asp:TextBox>
    <asp:TextBox ID="DDLLX" runat="server" Style="display: none"></asp:TextBox>
    <asp:Button ID="Btn" runat="server" Text="" OnClick="Btn_Click" Style="display: none" />
    <asp:TextBox ID="JRSB_UNIT_ID" runat="server" Style="display: none"></asp:TextBox>
    
    <asp:TextBox ID="SBMC_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="SBMC_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="SBDK_GUID" runat="server" Style="display: none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
