<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogicResourceIpEdit.aspx.cs"
    Inherits="Web_Resource_LogicResourceIpEdit" %>

<%@ Register Assembly="IDP.WebControls" Namespace="IDP.WebControls" TagPrefix="idp" %>
<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../jquery-1.2.6.min.js" type="text/javascript"></script>

    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../../config.js"></script>

    <script language="javascript" type="text/javascript" src="ResourceScript.js"></script>

    <script language="javascript" type="text/javascript">
        function windowOpen(guid, name) {
            if (document.getElementById("IPFD").value == "32") {
                alert("已为最小分段了，不能再分了！");
                return;
            }
            var p_guid = document.getElementById("GUID").value;
            var ywdl = document.getElementById("YWDL").value;
            var ssqy = document.getElementById("SSQY").value;
            var ssqy_code = document.getElementById("SSQY_CODE").value;
            var url = "LogicResourceIpEdit.aspx?GUID=" + guid + "&P_GUID=" + p_guid + "&YWDL=" + encodeURI(ywdl)
            + "&SSQY=" + encodeURI(ssqy) + "&SSQY_CODE=" + ssqy_code;
            windowOpenPage(url, "IP地址：" + name, "BtnSX");
        }
        function windowOpen1(guid, name) {

            windowOpenPage("LogicResourceIpEdit.aspx?GUID=" + guid, "IP地址：" + name, "BtnSX");
        }
        function windowOpen(guid) {
            var url = "ConfigResourceIpEdit.aspx?GUID=" + guid;
            windowOpenPage(url, "IP资源配置", "");
            window.event.returnValue = false;
        }

        function OpenSelect1(enumtype, pname, BtnName) {
            pname = $("#" + pname).val();

            var url = "../Resource/ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + encodeURIComponent(pname);
            windowOpenPageByWidth(url, "枚举维护", BtnName, "30%", "40%", "10%", "80%");
        }
        //        function windowOpenBranchTree(name, code) {
        //            var url = "BranchTree.aspx?NAME=" + name + "&CODE=" + code;
        //            windowOpenPageByWidth(url, "组织机构", "", "30%", "40%", "10%", "80%");
        //        }
        //        function windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, isEqucode, branchName) {
        //            var txt_code = txt_name + "_CODE";
        //            var txt_guid = txt_name + "_GUID";
        //            var res_guid = "";
        //            var res_code = "";
        //            var res_name = "";
        //            var branch = "";
        //            var branch_code = "";
        //            if (linkage_code != "") {
        //                res_guid = document.getElementById(p_txt_name + "_GUID").value;
        //                if (isEqucode == "1") {
        //                    res_code = document.getElementById(p_txt_name + "_CODE").value;
        //                }
        //                res_name = document.getElementById(p_txt_name).value;
        //            }
        //            if (branchName != "" && typeof (branchName) != "undefined") {
        //                branch = document.getElementById(branchName).value;
        //                branch_code = document.getElementById(branchName + "_CODE").value;
        //            }
        //            var url = "PhyEquSelect.aspx?ISEQUCODE=" + isEqucode + "&UNIT_ID=" + unit_id + "&TXT_NAME=" + txt_name + "&TXT_CODE=" + txt_code + "&TXT_GUID=" + txt_guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name)
        //            + "&NAME_FILED=" + linkage_code + "&P_TXT_NAME=" + p_txt_name + "&BRANCH=" + encodeURI(branch) + "&BRANCH_CODE=" + branch_code;
        //            windowOpenPage(url, "资源选择", "");
        //        }

        //        function OpenSelect(enumtype, pname, BtnName) {
        //            var url = "ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
        //            windowOpenPageByWidth(url, "枚举维护", BtnName, "30%", "40%", "10%", "80%");
        //        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%">
        <tr>
            <td class="tableHead">
                <input id="AddButton" class="btn_2k3" type="button" value="新增下级分段" onclick="windowOpen('','新增')" /><asp:Button
                    ID="SaveButton" runat="server" OnClick="SaveButton_Click" Text="保存" CssClass="btn_2k3" />
                <asp:Button ID="DeleteButton" runat="server" CssClass="btn_2k3" OnClick="DeleteButton_Click"
                    OnClientClick="return confirm('确定删除！');" Text="删除" />
                <asp:Button ID="BtnYwdl" runat="server" OnClick="BtnYwdl_Click" Style="display: none;" />
                <asp:Button ID="BtnDzfl" runat="server" OnClick="BtnDzfl_Click" Style="display: none;" />
                <asp:Button ID="BtnIpywlx" runat="server" OnClick="BtnIpywlx_Click" Style="display: none;" />
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td class="tdBak" align="center" style="width: 10%">
                            总体规划
                        </td>
                        <td colspan="1" style="width: 15%">
                            <asp:CheckBox ID="ISGH" runat="server" />
                        </td>
                        <td class="tdBak" align="center" style="width: 10%">
                            地址分类
                        </td>
                        <td colspan="1" align="center" style="width: 15%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:DropDownList ID="DZFL" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="DZFL_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenSelect1('DZFL','','BtnYwdl')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdBak" align="center" style="width: 10%">
                            业务大类
                        </td>
                        <td colspan="1" align="center" style="width: 15%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:DropDownList ID="YWDL" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="YWDL_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenSelect1('YWDL','DZFL','BtnYwdl')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdBak" align="center" style="width: 10%">
                            IP业务类型
                        </td>
                        <td align="left" style="width: 15%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:DropDownList ID="IPYWLX" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenSelect1('IPYWLX','YWDL','BtnIpywlx')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center" style="width: 10%">
                            是否全网
                        </td>
                        <td colspan="1" style="width: 15%">
                            <asp:CheckBox ID="SFQW" runat="server" AutoPostBack="True" OnCheckedChanged="SFQW_CheckedChanged" />
                        </td>
                        <td class="tdBak" align="center" style="width: 10%">
                            所属区域
                        </td>
                        <td colspan="1" align="center" style="width: 15%">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 100%;">
                                        <asp:TextBox ID="SSQY" runat="server" Width="100%" BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td width="1">
                                        <img align='right' src='../Images/Small/bb_table.gif' onclick="windowOpenBranchTree('SSQY','SSQY_CODE')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdBak" align="center" style="width: 10%">
                            所属机房名称
                        </td>
                        <td colspan="1" align="center" style="width: 15%">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 100%;">
                                        <asp:TextBox ID="SSJF" runat="server" Width="100%" BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td id="TD_SSJF" runat="server">
                                        <img align='right' src='../Images/Small/bb_table.gif' onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','SSJF','SSQY','HOUSE_AREA','1','')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdBak" align="center" style="width: 10%">
                            &nbsp;
                        </td>
                        <td align="left" style="width: 15%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            IP地址
                        </td>
                        <td colspan="3" align="center">
                            <table style="width: 100%; height: 100%;" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="10%">
                                        <asp:TextBox ID="IP1" runat="server" Width="100%" onKeyPress="return limitNum(this);"
                                            BorderStyle="None"></asp:TextBox>
                                    </td>
                                    <td width="2%">
                                        .
                                    </td>
                                    <td width="20%">
                                        <asp:DropDownList ID="IP2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="IP2_SelectedIndexChanged"
                                            Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="2%">
                                        .
                                    </td>
                                    <td width="20%">
                                        <asp:DropDownList ID="IP3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="IP3_SelectedIndexChanged"
                                            Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="2%">
                                        .
                                    </td>
                                    <td width="20%">
                                        <asp:DropDownList ID="IP4" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="2%">
                                        /
                                    </td>
                                    <td width="10%">
                                        <asp:DropDownList ID="IPFD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="IPFD_SelectedIndexChanged"
                                            Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdBak" align="center">
                            子网掩码
                        </td>
                        <td align="left">
                            <asp:TextBox ID="ZWYM" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center">
                            &nbsp;
                        </td>
                        <td align="left">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            IP分配状态
                        </td>
                        <td>
                            <asp:Label ID="IPFPZT" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="tdBak" align="center">
                            可用地址数
                        </td>
                        <td>
                            <asp:Label ID="KYDZS" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="tdBak" align="center">
                            是否启用
                        </td>
                        <td align="center">
                            <asp:CheckBox ID="SFQY" runat="server" />
                        </td>
                        <td class="tdBak" align="center">
                            &nbsp;
                        </td>
                        <td align="center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            登记人
                        </td>
                        <td align="center">
                            <asp:TextBox ID="DJR" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center">
                            登记日期
                        </td>
                        <td align="center">
                            <asp:TextBox ID="DJRQ" runat="server" Width="100%" BorderStyle="None" onfocus="setDay(this);"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center">
                            修改人
                        </td>
                        <td align="center">
                            <asp:TextBox ID="UPDATEUSERNAME" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center">
                            修改时间
                        </td>
                        <td align="center">
                            <asp:TextBox ID="UPDATEDATETIME" runat="server" Width="100%" BorderStyle="None" onfocus="setDay(this);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            用途描述
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="YTMS" runat="server" Width="100%" BorderStyle="None" TextMode="MultiLine"
                                Rows="2"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center">
                            使用情况
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="SYQK" runat="server" Width="100%" BorderStyle="None" TextMode="MultiLine"
                                Rows="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" align="center">
                            备 注
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="BZ" runat="server" Width="100%" BorderStyle="None" TextMode="MultiLine"
                                Rows="2"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tableHead">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="BtnIpCsh" runat="server" Text="" CssClass="btn" OnClick="BtnIpCsh_Click" />
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="BtnIpPzd" runat="server" Text="" CssClass="btn" OnClick="BtnIpPzd_Click" />
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="BtnIpKFP" runat="server" Text="可分配ip" CssClass="btn" OnClick="BtnIpKFP_Click" />
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr runat="server" id="ipzypz_tr">
            <td valign="top" style="height: 100%">
                <asp:GridView ID="GridViewLogicEquIP" runat="server" SkinID="GridView1" DataKeyNames="GUID"
                    BorderWidth="1px" AllowPaging="True" AllowSorting="True" CellSpacing="1" OnRowDataBound="GridViewLogicEquIP_RowDataBound"
                    AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField HeaderText="序号">
                            <ItemStyle HorizontalAlign="Center" Width="4%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IP" HeaderText="IP地址">
                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="所属区域" DataField="SSQY">
                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SSJF" HeaderText="所属机房">
                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="YTMS" HeaderText="用途描述">
                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IPYWLX" HeaderText="业务类型">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="KYDZS" HeaderText="可用地址数">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IPFPZT" HeaderText="IP分配状态">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="启用">
                            <ItemTemplate>
                                <%--<asp:CheckBox ID="SFQY" runat="server" Checked='<%# Eval("SFQY").ToString()=="1"?true:false %>'
                                    Enabled="false" />--%>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SFQY").ToString()=="1"?"√":"" %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="详细">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr runat="server" id="ipzypzfy_tr">
            <td style="height: 1px; border: 1px solid #F0F0F0">
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
                            <asp:Label ID="Label5" runat="server" Width="55px">单页显示</asp:Label>
                        </td>
                        <td width="1">
                            <font face="宋体">
                                <asp:DropDownList ID="PageSize" runat="server" ForeColor="Red" Font-Bold="True" AutoPostBack="True"
                                    Width="60px" OnSelectedIndexChanged="PageSize_SelectedIndexChanged">
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
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
        <tr runat="server" id="ippzd_tr" style="display: none">
            <td valign="top" style="height: 100%">
                <%--IP资源配置单位--%>
                <asp:GridView ID="gvLogicEquIp" runat="server" SkinID="GridView1" DataKeyNames="GUID,SUBSCRIBER_ID"
                    Width="100%" BorderColor="#5B9ED1" BorderWidth="1px" AllowSorting="True" AutoGenerateColumns="False"
                    OnRowDataBound="gvLogicEquIp_RowDataBound">
                    <PagerSettings PageButtonCount="100" />
                    <Columns>
                        <asp:BoundField DataField="SUBSCRIBER_CODE" HeaderText="业务编码" ItemStyle-BorderColor="#5b9ed1">
                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                            <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ipdz" HeaderText="IP地址" ItemStyle-BorderColor="#5b9ed1">
                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                            <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="vlan" HeaderText="VLAN" ItemStyle-BorderColor="#5b9ed1">
                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                            <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SBPZXX" HeaderText="设备配置信息" ItemStyle-BorderColor="#5b9ed1">
                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                            <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="客户名称" ItemStyle-BorderColor="#5b9ed1">
                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                            <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="YWLX" HeaderText="业务类型" ItemStyle-BorderColor="#5b9ed1">
                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                            <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="REGION" HeaderText="所属区域" ItemStyle-BorderColor="#5b9ed1">
                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                            <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="WLJF" HeaderText="机房名称">
                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                            <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr id="ippzdfy_tr" runat="server" style="display: none">
            <td style="height: 1">
                <idp:Pager ID="Pager1" runat="server" OnPageIndexChanged="Pager1_PageIndexChanged"
                    PageSize="20" />
            </td>
        </tr>
        <tr runat="server" id="Tr1" style="display: none">
            <td valign="top" style="height: 100%">
                <asp:GridView ID="GridView1" runat="server" SkinID="GridView1" Width="100%" BorderColor="#5B9ED1"
                    BorderWidth="1px" AllowSorting="True" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="IPDZ" HeaderText="IP地址">
                            <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" Width="12%" BorderColor="#5B9ED1" BorderWidth="1px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr runat="server" id="Tr2" style="display: none">
            <td style="height: 1px; border: 1px solid #F0F0F0">
                <table class="tdBak" id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td width="100%" style="padding-left: 6px;">
                            <font face="宋体">总共有
                                <asp:Label ID="DataCountLab1" runat="server" ForeColor="Red"></asp:Label></font><font
                                    face="宋体">条记录，当前第
                                    <asp:Label ID="PageIndexLab1" runat="server" ForeColor="Red"></asp:Label>页，共
                                    <asp:Label ID="PageCountLab1" runat="server" ForeColor="Red"></asp:Label>页</font>
                        </td>
                        <td width="1">
                            <asp:Label ID="Label2" runat="server" Width="55px">单页显示</asp:Label>
                        </td>
                        <td width="1">
                            <font face="宋体">
                                <asp:DropDownList ID="PageSize1" runat="server" ForeColor="Red" Font-Bold="True"
                                    AutoPostBack="True" Width="60px" OnSelectedIndexChanged="PageSize1_SelectedIndexChanged">
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                            </font>
                        </td>
                        <td width="1">
                            <asp:LinkButton ID="PrevButton1" runat="server" ForeColor="#003797" Width="50px"
                                OnClick="PrevButton1_Click">上一页</asp:LinkButton>
                        </td>
                        <td width="1">
                            <asp:DropDownList ID="GridPageList1" runat="server" ForeColor="Red" Font-Bold="True"
                                AutoPostBack="True" Width="50px" OnSelectedIndexChanged="GridPageList1_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="1">
                            <asp:LinkButton ID="NextButton1" runat="server" ForeColor="#003797" Width="50px"
                                OnClick="NextButton1_Click">下一页</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="SSJF_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="CREATEDATETIME" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="P_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="START_FD" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="IP" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="P_IP1" runat="server" Style="display: none">10</asp:TextBox>
    <asp:TextBox ID="P_IP2" runat="server" Style="display: none">0</asp:TextBox>
    <asp:TextBox ID="P_IP3" runat="server" Style="display: none">0</asp:TextBox>
    <asp:TextBox ID="P_IP4" runat="server" Style="display: none">0</asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    <asp:TextBox ID="IPDZ1" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="IPDZ2" runat="server" Style="display: none"></asp:TextBox>
    <asp:Button ID="BtnSX" runat="server" Text="Button" OnClick="BtnSX_Click" Style="display: none" />
    <asp:TextBox ID="SSQY_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="SSJF_GUID" runat="server" Style="display: none"></asp:TextBox>
    </form>
</body>
</html>
