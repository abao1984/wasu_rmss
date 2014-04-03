<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogicResourceVlanSelect.aspx.cs" Inherits="Web_Resource_LogicResourceVlanSelect" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
        function windowOpen(guid, plcz) {
            windowOpenPage("LogicResourceVlanEdit.aspx?GUID=" + guid+"&PLCZ="+plcz, "VLAN资源", "Btn1");
            window.event.returnValue = false;
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
                branch_code = document.getElementById(branchName + "_CODE").value;
            }
            var url = "PhyEquSelect.aspx?ISEQUCODE=" + isEqucode + "&UNIT_ID=" + unit_id + "&TXT_NAME=" + txt_name + "&TXT_CODE=" + txt_code + "&TXT_GUID=" + txt_guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name)
            + "&NAME_FILED=" + linkage_code + "&P_TXT_NAME=" + p_txt_name + "&BRANCH=" + encodeURI(branch) + "&BRANCH_CODE=" + branch_code;
            windowOpenPage(url, "资源选择", "");
        }
        function windowOpenEqu(txt_fl, txt_name, p_txt_name, linkage_code) {
            var unit_id = document.getElementById(txt_fl).value;
            windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, '1')
        }
        function OpenSelect(name, code) {
            windowOpenPageByWidth("BranchTree.aspx?NAME=" + name + "&CODE=" + code, "选择所属区域", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }

        function sfqy(names) {
            document.getElementById("SFFY").value = confirm('Vlan' + names + '已占用，是否复用?');
        }

        function test(vr1, vr2, vr3, vr4, vr5, vr6) {
            var qy = document.getElementById("SFFY").value;
            if (qy != "false") {
                document.getElementById('Button1').click();
                window.close();
                parent.WindowClose();
                parent.document.getElementById('BtnVlan').click();
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%;">
        <tr>
            <td class="tableHead">
                &nbsp;
                <asp:Button ID="BtnQuery" runat="server" Text="查询" CssClass="btn_2k3" OnClick="BtnQuery_Click" />
                &nbsp;
                &nbsp;
                &nbsp;<asp:Button ID="OKButton" runat="server" CssClass="btn_2k3" Text="确定" 
                    onclick="OKButton_Click" />
            </td>
        </tr>
        <tr>
            <td height="1">
                <table style="border-collapse: collapse; width: 100%" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                      <td align="center" class="tdBak" style="width: 12%">
                            所属区域
                        </td>
                        <td style="width: 13%">
                             <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="SSQY" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenSelect('SSQY','SSQY_CODE')"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            所属机房
                        </td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="SSJF" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','SSJF','','','1','SSQY')"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                      
                        <td align="center" class="tdBak" style="width: 12%">
                            VLAN编号
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="BH1" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" style="width: 12%">
                            至
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="BH2" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            是否可复用
                        </td>
                        <td width="13%">
                            <asp:DropDownList ID="SFKFY" runat="server" Width="100%">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="Y">是</asp:ListItem>
                                <asp:ListItem Value="N">否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            VLAN占用状态
                        </td>
                        <td width="13%">
                            <asp:DropDownList ID="ZYZT" runat="server" Width="100%">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>占用</asp:ListItem>
                                <asp:ListItem>空闲</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="center" class="tdBak" colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%" valign="top">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="VlanList" runat="server" SkinID="GridView2" 
                        DataKeyNames="GUID" Width="100%"
                        BorderColor="#5B9ED1" BorderWidth="1px" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" onrowdatabound="VlanList_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="XZ" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="5%"
                                    Wrap="true" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="VLANBH" HeaderText="编号">
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="15%"
                                    Wrap="true" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SSJF" HeaderText="所属机房" 
                                ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SSQY" HeaderText="所属区域">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SFKFY" HeaderText="是否可复用">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Wrap="true" HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ZYZT" HeaderText="VLAN占用状态">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"  />
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Wrap="true" HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="GUID" HeaderText="GUID">
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
    <asp:Button ID="Btn1" runat="server" Text="" onclick="Btn1_Click" style="display:none" />
    <asp:Button ID="Button1" runat="server" Text="" onclick="Button1_Click" style="display:none"  />
     <asp:TextBox ID="PK_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="SSJF_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="SSJF_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="SSQY_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="SFFY" name="SFFY" runat="server" style="display:none;"></asp:TextBox>
    <asp:TextBox ID="txtVlanGuids" runat="server" style="display:none;"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />  
     <div id="EnumDataDiv" runat="server" style="z-index: 1; display: none; border: 1px solid #5b9ed1;
        position: absolute; width: 40%; height: 100%; top: 50px; left: 30%;">
        <iframe id="Iframe1" style="z-index: 1; visibility: inherit; width: 100%; height: 100%"
            runat="server" name="ProperyPage" frameborder="0" scrolling="no"></iframe>
    </div> 
    </form>
</body>
</html>
