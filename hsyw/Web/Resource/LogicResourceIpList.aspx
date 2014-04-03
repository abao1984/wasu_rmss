<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogicResourceIpList.aspx.cs"
    Inherits="Web_Resource_LogicResourceIpList" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="../../config.js"></script>
    <script language="javascript" type="text/javascript">
        function windowOpen(guid, name) {
            windowOpenPage("LogicResourceIpEdit.aspx?GUID=" + guid, "IP地址：" + name, "QueryButton");
        }
        function scrollSave() {
            document.getElementById("LogicEquIPScroll").value = self['LogicEquIPDIV'].scrollTop;

        }
        function SetScroll() {
            self['LogicEquIPDIV'].scrollTop = document.getElementById("LogicEquIPScroll").value;
        }

        function windowOpenBranchTree(name, code) {
            var url = "BranchTree.aspx?ISQY=1&NAME=" + name + "&CODE=" + code;
            windowOpenPageByWidth(url, "组织机构", "", "30%", "40%", "10%", "80%");
        }
        function windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, isEqucode, branchName) {
            var txt_code = txt_name + "_CODE";
            var txt_guid = txt_name + "_GUID";
            var res_guid = "";
            var res_code = "";
            var res_name = "";
            var branch = "";
            var branch_code = "";
            if (linkage_code != "") {
                res_guid = document.getElementById(p_txt_name + "_GUID").value;
                if (isEqucode == "1") {
                    res_code = document.getElementById(p_txt_name + "_CODE").value;
                }
                res_name = document.getElementById(p_txt_name).value;
            }
            if (branchName != "" && typeof (branchName) != "undefined") {
                branch = document.getElementById(branchName).value;
                branch_code = document.getElementById(branchName+"_CODE").value;
            }
            var url = "PhyEquSelect.aspx?ISEQUCODE=" + isEqucode + "&UNIT_ID=" + unit_id + "&TXT_NAME=" + txt_name + "&TXT_CODE=" + txt_code + "&TXT_GUID=" + txt_guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name)
            + "&NAME_FILED=" + linkage_code + "&P_TXT_NAME=" + p_txt_name + "&BRANCH=" + encodeURI(branch) + "&BRANCH_CODE=" + branch_code;
            windowOpenPage(url, "资源选择", "");
        }
    </script>

</head>
<body  onload="SetScroll()">
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="tableHead">
                <input id="Button1" type="button" value="新增" onclick="windowOpen('','新增')" class="btn_2k3" /><asp:Button
                    ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="查询" 
                    CssClass="btn_2k3" />
            </td>
        </tr>
        <tr>
            <td style="height:1px">
                <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td width="10%" align="center" class="tdBak">
                            所属区域</td>
                        <td width="15%">
                            <table style="width:100%;">
                                <tr>
                                    <td  style="width:100%;">
                                        <asp:TextBox ID="SSQY" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                       <img align='right' src='../Images/Small/bb_table.gif' onclick="windowOpenBranchTree('SSQY','SSQY_CODE')" /></td>
                                </tr>
                            </table>
                        </td>
                        <td width="10%" align="center" class="tdBak">
                            机房编码</td>
                        <td width="15%">
                                        <asp:TextBox ID="SSJF_CODE" runat="server" Width="100%"></asp:TextBox>
                           </td>
                        <td width="10%" align="center" class="tdBak">
                            机房名称</td>
                        <td width="15%">
                            <table style="width:100%;">
                                <tr>
                                    <td  style="width:100%;">
                                        <asp:TextBox ID="SSJF" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                    <td>
                                       <img align='right' src='../Images/Small/bb_table.gif'  onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','SSJF','','','1','SSQY')" /></td>
                                </tr>
                            </table>
                            </td>
                        <td width="10%" align="center" class="tdBak">
                            总体规划</td>
                        <td width="15%">
                            <asp:DropDownList ID="ISGH" runat="server">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="1">是</asp:ListItem>
                                <asp:ListItem Value="0">否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%" align="center" class="tdBak">
                            地址分类</td>
                        <td width="15%">
                                        <asp:DropDownList ID="DZFL" runat="server" Width="100%" AutoPostBack="True">
                                        </asp:DropDownList>
                        </td>
                        <td width="10%" align="center" class="tdBak">
                            业务大类</td>
                        <td width="15%">
                                        <asp:DropDownList ID="YWDL" runat="server" Width="100%" AutoPostBack="True">
                                        </asp:DropDownList>
                           </td>
                        <td width="10%" align="center" class="tdBak">
                            业务类型</td>
                        <td width="15%">
                            <asp:DropDownList ID="IPYWLX" runat="server" Width="100%">
                            </asp:DropDownList>
                            </td>
                        <td width="10%" align="center" class="tdBak">
                            分配状态</td>
                        <td width="15%">
                            <asp:DropDownList ID="IPFPZT" runat="server" Width="100%">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>已分配</asp:ListItem>
                                <asp:ListItem>部分分配</asp:ListItem>
                                <asp:ListItem>未分配</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak">
                            IP地址</td>
                        <td colspan="3">
                            <table style="width: 100%; height: 100%;" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="16%">
                                        <asp:TextBox ID="IP1" runat="server" Width="100%" onKeyPress="return limitNum(this);"
                                            BorderStyle="None"></asp:TextBox>
                                    </td>
                                    <td width="2%">
                                        .
                                    </td>
                                    <td width="16%">
                                        <asp:TextBox ID="IP2" runat="server" Width="100%" onKeyPress="return limitNum(this);"
                                            BorderStyle="None"></asp:TextBox>
                                    </td>
                                    <td width="2%">
                                        .
                                    </td>
                                    <td width="16%">
                                        <asp:TextBox ID="IP3" runat="server" Width="100%" onKeyPress="return limitNum(this);"
                                            BorderStyle="None"></asp:TextBox>
                                    </td>
                                    <td width="2%">
                                        .
                                    </td>
                                    <td width="16%">
                                        <asp:TextBox ID="IP4" runat="server" Width="100%" onKeyPress="return limitNum(this);"
                                            BorderStyle="None"></asp:TextBox>
                                    </td>
                                    <td width="2%">
                                        /
                                    </td>
                                    <td width="16%">
                                        <asp:DropDownList ID="IPFD" runat="server"  Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak">
                            是否启用</td>
                        <td>
                            <asp:DropDownList ID="SFQY" runat="server" Width="100%">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="1">是</asp:ListItem>
                                <asp:ListItem Value="0">否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="center" class="tdBak">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>                   
                    </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
            <asp:TextBox ID="LogicEquIPScroll" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
             <div id="LogicEquIPDIV" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%" align="center" runat="server"  onscroll="scrollSave()">
                <asp:GridView ID="GridViewLogicEquIP" runat="server" SkinID="GridView1" DataKeyNames="GUID"
                    BorderWidth="1px" AllowSorting="True" CellSpacing="1" OnRowDataBound="GridViewLogicEquIP_RowDataBound"
                    AutoGenerateColumns="False">
                    <PagerSettings PageButtonCount="100" />
                    <Columns>
                        <asp:BoundField HeaderText="序号">
                            <ItemStyle HorizontalAlign="Center" Width="4%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IP" HeaderText="IP地址">
                            <ItemStyle HorizontalAlign="left" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="P_IP" HeaderText="上级IP地址">
                            <ItemStyle HorizontalAlign="left" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="所属区域" DataField="SSQY">
                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SSJF" HeaderText="所属机房">
                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="YTMS" HeaderText="用途描述" >
                            <ItemStyle HorizontalAlign="Left" Width="12%" />
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
                </div>
            </td>
        </tr>
        <tr>
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
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    <asp:TextBox ID="SSQY_CODE" runat="server" Style="display: none"></asp:TextBox>
     <asp:TextBox ID="SSJF_GUID" runat="server" Style="display: none"></asp:TextBox>
    </form>
</body>
</html>
