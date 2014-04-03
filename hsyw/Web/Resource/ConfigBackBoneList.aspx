﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigBackBoneList.aspx.cs"
    Inherits="Web_Resource_ConfigBackBoneList" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>  
    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function windowOpen(guid) {
            var url = "ConfigBackBoneEdit.aspx?YWGUID=" + guid;
            windowOpenPage(url, "骨干资源配置", "Btn");
            window.event.returnValue = false;
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

        function windowOpenEquSB(txt_fl, txt_name, p_txt_name, linkage_code) {
            var unit_id = document.getElementById(txt_fl).value;
            //alert(unit_id);
            if (unit_id == "") {
                unit_id = "64602091-d4fe-4c89-ac6a-52f6acdd836d,9e2393f1-931d-4b14-b44f-0ba9ff846853,41d1081d-7925-485b-996c-72f4519c7898";
            }
            
            windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, '1')
            
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
                    OnClientClick="windowOpen('')" />
                
                <asp:Button ID="BtnDel" runat="server" Text="删除" OnClientClick="return confirm('确定要删除吗？')"
                    CssClass="btn_2k3" OnClick="BtnDel_Click" />
                <asp:Button ID="BtnExport" runat="server" Text="导出Excel" CssClass="btn_2k3" 
                    onclick="BtnExport_Click" />
            </td>
        </tr>
        <tr>
            <td height="1">
                <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            业务编码
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="YWBM" runat="server" Width="100%" BorderWidth="0px"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            业务类型
                        </td>
                        <td width="13%">
                            <asp:DropDownList ID="YWLX" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            链路名称
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="LLMC" runat="server" width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            完整纤号
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="WZXH" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            甲端机房编码</td>
                        <td width="13%">
                                        <asp:TextBox ID="JDJRJF_CODE" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            甲端机房名称</td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="JDJRJF" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif"  onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','JDJRJF','','','1')"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            乙端机房编码</td>
                        <td width="13%">
                                        <asp:TextBox ID="YDJRJF_CODE" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            乙端机房名称</td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="YDJRJF" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif"  onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','YDJRJF','','','1')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            甲端设备类型</td>
                        <td width="13%">
                            <asp:DropDownList ID="JDSBLX" runat="server" Width="100%">
                             <asp:ListItem></asp:ListItem>
                              <asp:ListItem Value="64602091-d4fe-4c89-ac6a-52f6acdd836d">网络设备</asp:ListItem>
                                <asp:ListItem Value="9e2393f1-931d-4b14-b44f-0ba9ff846853">传输设备</asp:ListItem>
                                <asp:ListItem Value="41d1081d-7925-485b-996c-72f4519c7898">楼宇设备</asp:ListItem>                               
                            </asp:DropDownList>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            甲端设备编码</td>
                        <td width="13%">
                                        <asp:TextBox ID="JDSB_CODE" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            甲端设备名称</td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="JDSB" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenEquSB('JDSBLX','JDSB','JDJRJF','HOUSE_NAME','1')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            甲端设备端口</td>
                        <td width="13%">
                             <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="JDSBDK" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('bfc13d2d-eab8-4784-a96a-b8ffc21b4e88','JDSBDK','JDSB','EQU_NAME','0')"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            乙端设备类型
                        </td>
                        <td width="13%">
                            <asp:DropDownList ID="YDSBLX" runat="server" Width="100%">
                            <asp:ListItem></asp:ListItem>
                              <asp:ListItem Value="64602091-d4fe-4c89-ac6a-52f6acdd836d">网络设备</asp:ListItem>
                                <asp:ListItem Value="9e2393f1-931d-4b14-b44f-0ba9ff846853">传输设备</asp:ListItem>
                                <asp:ListItem Value="41d1081d-7925-485b-996c-72f4519c7898">楼宇设备</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            乙端设备编号</td>
                        <td width="13%">
                                        <asp:TextBox ID="YDSB_CODE" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            乙端设备名称</td>
                        <td width="13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="YDSB" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenEquSB('YDSBLX','YDSB','YDJRJF','HOUSE_NAME','1')"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            乙端设备端口</td>
                        <td width="13%">
                             <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="YDSBDK" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif"  onclick="windowOpenPhyResourceSelect('bfc13d2d-eab8-4784-a96a-b8ffc21b4e88','YDSBDK','YDSB','EQU_NAME','0')"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="12%">
                            启用时间
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="QYSJ1" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            至
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="QYSJ2" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            申请时间
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="SQSJ1" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);"></asp:TextBox>
                        </td>
                        <td align="center" class="tdBak" width="12%">
                            至
                        </td>
                        <td width="13%">
                            <asp:TextBox ID="SQSJ2" runat="server" Width="100%" BorderWidth="0" onfocus="setDay(this);"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%" valign="top">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="gvBoneList" runat="server" SkinID="GridView1" DataKeyNames="YWGUID"
                        Width="100%" BorderColor="#5B9ED1" BorderWidth="1px" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" OnRowDataBound="gvBoneList_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="5%" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="YWBM" HeaderText="业务编码" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="YWLX" HeaderText="业务类型" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="LLMC" HeaderText="链路名称" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="WZXH" HeaderText="完整纤号" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SQRNAME" HeaderText="申请人" ItemStyle-BorderColor="#5b9ed1">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SQSJ" DataFormatString="{0:yyyy-MM-dd}" HeaderText="申请时间">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="QDSJ" DataFormatString="{0:yyyy-MM-dd}" HeaderText="启用时间">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="10%"></ItemStyle>
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
    <asp:Button ID="Btn" runat="server" Text="Button" OnClick="Btn_Click" Style="display: none;" />
    <asp:TextBox ID="JDJRJF_GUID" runat="server" style="display:none"></asp:TextBox>   
    <asp:TextBox ID="YDJRJF_GUID" runat="server" style="display:none"></asp:TextBox>   
    <asp:TextBox ID="JDSB_GUID" runat="server" style="display:none"></asp:TextBox>   
    <asp:TextBox ID="YDSB_GUID" runat="server" style="display:none"></asp:TextBox>   
    <asp:TextBox ID="JDSBDK_GUID" runat="server" style="display:none"></asp:TextBox>
    <asp:TextBox ID="YDSBDK_GUID" runat="server" style="display:none"></asp:TextBox>
    <asp:TextBox ID="ZYHS_BJ" runat="server" style="display:none"></asp:TextBox>
 
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
