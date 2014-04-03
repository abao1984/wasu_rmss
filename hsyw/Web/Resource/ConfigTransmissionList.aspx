<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigTransmissionList.aspx.cs"
    Inherits="Web_Resource_ConfigTransmissionList" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../config.js"></script>
    <script language="javascript" type="text/javascript" src="ResourceScript.js"></script>
    <script language="javascript" type="text/javascript">
        function windowOpen(guid) {
            windowOpenPage("ConfigTransmissionEdit.aspx?YWGUID=" + guid, "传输资源配置", "Btn");
            window.event.returnValue = false;
        }
//        function windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, isEqucode) {
//            var txt_code = txt_name + "_CODE";
//            var txt_guid = txt_name + "_GUID";
//            var res_guid = "";
//            var res_code = "";
//            var res_name = "";
//            if (linkage_code != "") {
//                res_guid = document.getElementById(p_txt_name + "_GUID").value;
//                if (isEqucode == "1") {
//                    res_code = document.getElementById(p_txt_name + "_CODE").value;
//                }
//                res_name = document.getElementById(p_txt_name).value;
//            }
//            var url = "PhyEquSelect.aspx?ISEQUCODE=" + isEqucode + "&UNIT_ID=" + unit_id + "&TXT_NAME=" + txt_name + "&TXT_CODE=" + txt_code + "&TXT_GUID=" + txt_guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name) + "&NAME_FILED=" + linkage_code + "&P_TXT_NAME=" + p_txt_name;
//            windowOpenPage(url, "资源选择", "");
//        }
        function windowOpenEqu(txt_fl, txt_name, p_txt_name, linkage_code) {
            var unit_id = document.getElementById(txt_fl).value;
            windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, '1')
        }
        function OpenSelect(enumtype, pname, ddlid) {
            var url = "ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
            windowOpenPageByWidth(url, "枚举维护", "Btn1", "30%", "40%", "10%", "80%");
            document.getElementById("DDLID").value = ddlid;
            document.getElementById("DDLLX").value = enumtype;
        }

        function windowOpenRmssSelect() {
            var url = "RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID";
            windowOpenPage(url, "选择客户资源", "BtnRmss");
            event.returnValue = false;
        }       
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%;">
        <tr>
            <td class="tableHead" >
                &nbsp;
                <asp:Button ID="BtnQuery" runat="server" Text="查询" CssClass="btn_2k3" OnClick="BtnQuery_Click" />
                &nbsp;
                <asp:Button ID="BtnAdd" runat="server" Text="新增" CssClass="btn_2k3" OnClientClick="windowOpen('')" />
                &nbsp;
                <asp:Button ID="BtnDel" runat="server" Text="删除" OnClientClick="return confirm('确定要删除吗？')"
                    CssClass="btn_2k3" OnClick="BtnDel_Click" />
                <asp:Button ID="BtnRmss" runat="server" OnClick="BtnRmss_Click" Style="display: none" />
                <asp:Button ID="BtnExport" runat="server" Text="导出Excel" CssClass="btn_2k3" OnClick="BtnExport_Click" />
            </td>
        </tr>
        <tr>
            <td height="1">
                <table style="border-collapse: collapse; width: 100%" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            组网方式
                        </td>
                        <td width="13%">
                            <asp:DropDownList ID="ZWFS" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            链路宽带
                        </td>
                        <td width="13%">
                            <asp:DropDownList ID="LLDK" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            配置日期
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="PZKSRQ" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            至
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="PZJSRQ" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);"></asp:TextBox>
                        </td>
                    </tr>
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
                                        <asp:ImageButton runat="server" ID="Select" ImageUrl="../Images/Small/bb_table.gif"
                                            OnClientClick="windowOpenRmssSelect()" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            业务名称
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="SUB_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            所属区域
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="REGION" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenBranch('REGION','REGION_CODE')" />
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
                            客户类型
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="CUSTTYPE1" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            接入机房
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="JRJF" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','JRJF','REGION','HOUSE_AREA','1')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            接入设备名称
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="JRSB" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenEqu('JRSB_UNIT_ID','JRSB','JRJF','HOUSE_NAME','1')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            设备端口
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="SBDK" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('bfc13d2d-eab8-4784-a96a-b8ffc21b4e88','SBDK','JRSB','EQU_NAME','0')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%" valign="top" >
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="gvCSYWList" runat="server" SkinID="GridView2" DataKeyNames="YWGUID"
                        BorderColor="#5B9ED1" BorderWidth="1px" AllowPaging="True" AllowSorting="True"
                        Width="130%" AutoGenerateColumns="False" OnRowDataBound="gvCSYWList_RowDataBound"
                        OnSorting="gvCSYWList_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="3%"
                                    Wrap="true" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" Width="4%" />
                            </asp:TemplateField>
                            <%-- <asp:BoundField DataField="BH" HeaderText="编号">
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="5%"
                                    Wrap="true" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" Width="5%" />
                            </asp:BoundField>     --%>
                            <asp:TemplateField HeaderText="编号">
                                 <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" Width="4%" />
                                <ItemTemplate>
                                    <%#(Container.DataItemIndex + 1) %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Wrap="False" Width="3%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ZWFS" HeaderText="组网方式" SortExpression="ZWFS">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="5%" />
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="5%" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LLDK" HeaderText="链路带宽" SortExpression="LLDK">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="5%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JD_SUBSCRIBER_CODE" HeaderText="甲端用户编号" ItemStyle-BorderColor="#5b9ed1"
                                SortExpression="JD_SUBSCRIBER_CODE">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="8%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="8%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JD_SUB_NAME" HeaderText="甲端用户名称" 
                                ItemStyle-BorderColor="#5b9ed1" SortExpression="JD_SUB_NAME">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px"  Wrap="true"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="JD_LINKMAN" HeaderText="甲端联系人" 
                                SortExpression="JD_LINKMAN">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="6%"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="6%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField  DataField="JD_LINK" HeaderText="甲端联系方式" SortExpression="JD_LINK">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="7%"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="7%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="YD_SUBSCRIBER_CODE" HeaderText="乙端用户编号" ItemStyle-BorderColor="#5b9ed1"
                                SortExpression="YD_SUBSCRIBER_CODE">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="8%"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="8%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="JD_CUSTOMER_LEVEL" HeaderText="客户等级" 
                                SortExpression="JD_CUSTOMER_LEVEL">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="5%"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="5%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="YD_LINKMAN" HeaderText="乙端联系人" SortExpression="YD_LINKMAN">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="6%"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="6%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="乙端联系方式"  DataField="YD_LINK" SortExpression="YD_LINK">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="7%"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="7%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <%--<asp:BoundField DataField="YD_SUB_NAME" HeaderText="乙端用户名称">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="15%"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="15%" Wrap="true"></ItemStyle>
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="VLANID" HeaderText="VLAN-ID" 
                                SortExpression="VLANID">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="6%"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="6%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="XGJL" HeaderText="修改记录" SortExpression="XGJL">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PZR" HeaderText="配置人" SortExpression="PZR">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="4%"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="4%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="PZRQ" HeaderText="配置日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}"
                                SortExpression="PZRQ">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="6%"></HeaderStyle>
                                <ItemStyle  HorizontalAlign="Center" BorderColor="#5B9ED1" BorderWidth="1px" Width="6%" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td height="1" width="60%">
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
    <asp:Button ID="Btn" runat="server" Text="" OnClick="Btn_Click" Style="display: none" />
    <asp:TextBox ID="SUBSCRIBER_ID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JRJF_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JRJF_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JRSB_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JRSB_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="SBDK_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JRSB_UNIT_ID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="ZYHS_BJ" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="txtorder" runat="server" Style="display: none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
