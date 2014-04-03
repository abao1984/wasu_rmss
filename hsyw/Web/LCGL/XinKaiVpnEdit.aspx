<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XinKaiVpnEdit.aspx.cs" Inherits="Web_LCGL_XinKaiVpnEdit" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>
<%@ Import namespace="System.Data"%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function sendPageShow(lcbm,lczt,qfbj) {
            var guid = document.getElementById("GUID").value;
            var url = "SendPage.aspx?LCQFBJ=" + qfbj + "&LCBM="+lcbm+"&LC_GUID=" + guid + "&LCZT=" + lczt;
            var str = window.showModalDialog(url, '', 'dialogHeight:300px; dialogWidth: 500px;center: yes; help: no;resizable: no; status:no;scroll:no;');
            if (str == "OK") {
                window.close();
                parent.document.getElementById("MenuButton").click();
            }
        }

        function windowOpenRmssSelect() {
            var url = "../Resource/RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID";
            windowOpenPage(url, "选择客户资源", "BtnRmss");
            event.returnValue = false;
        }
        function windowOpenRmssTQ() {
            var url = "../Resource/RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID&SUBSCRIBER_CODE=" + document.getElementById("SUBSCRIBER_CODE").value;
            windowOpenPage(url, "选择客户资源", "BtnRmss");
            //event.returnValue = false;
        }
        function OpenSelect(enumtype, pname, ddlid) {
            var url = "../Resource/ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
            windowOpenPageByWidth(url, "枚举维护", "Btn1", "30%", "40%", "10%", "80%");
            //document.getElementById("DDLID").value = ddlid;
            //document.getElementById("DDLLX").value = enumtype;
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%;" border="0" cellpadding="1" cellspacing="0">
            <tr id="Tr_Button" runat="server" style="display:none;">
                <td align="center" class="tableHead">
                    <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" 
                        onclick="SaveButton_Click" Text="保存" />
                    <asp:Button ID="SendButton" runat="server" CssClass="btn_2k3" 
                        onclick="SendButton_Click" Text="签发" />
                    <asp:Button ID="BackButton" runat="server" CssClass="btn_2k3" 
                        onclick="BackButton_Click" Text="驳回" />
                    <asp:Button ID="Btn1" runat="server" onclick="Btn1_Click"  style="display:none"/>
                </td>
            </tr>
             <tr>
            <td align="center" class="tableTitle">            
                <asp:Label ID="HeadTitle" runat="server"></asp:Label>            
            </td>
            </tr>
            <tr>
                <td align="center">
                    <table border="1" bordercolor="#5b9ed1" cellpadding="0" cellspacing="0" 
                        style="border-collapse: collapse;" width="100%">
                        <tr>
                            <td align="center" class="tdBak" style="width:20%" >
                                申请单编号</td>
                            <td>
                                <asp:TextBox ID="SQDBH" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" class="tableTitle">
                    &nbsp;&nbsp;&nbsp;
                    1．申请资料</td>
            </tr>
            <tr>
                <td>
                    <table bordercolor="#5b9ed1"  cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;" width="100%">
                        <tr>
                            <td align="center" class="tdBak" width="20%" >
                                申请部门</td>
                            <td width="30%" >
                                <asp:TextBox ID="SQBM" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
                            </td>
                            <td  align="center" class="tdBak" width="20%">
                                部门负责人</td>
                            <td >
                                            <asp:TextBox ID="BMFZR" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                销售负责人</td>
                            <td>
                                            <asp:TextBox ID="XSFZR" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                            <td align="center" class="tdBak">
                                项目负责人</td>
                            <td>
                                            <asp:TextBox ID="XMFZR" runat="server" Width="100%" 
                                    BorderWidth="0"></asp:TextBox>
                                        </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                申请日期</td>
                            <td>
                            <asp:TextBox ID="SQRQ" runat="server" Width="100%" BorderWidth="0" 
                                onfocus="setDay(this);"></asp:TextBox>
                            </td>
                            <td align="center" class="tdBak">
                                经 办 人</td>
                            <td>
                                            <asp:TextBox ID="JBR" runat="server" Width="100%" 
                                    BorderWidth="0"></asp:TextBox>
                                        </td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td align="left" class="tableTitle">
                                        &nbsp;&nbsp; 2．客户资料</td>
            </tr>
            <tr>
                <td>
                    <table border="1" bordercolor="#5b9ed1" cellpadding="0" cellspacing="0" 
                        style="border-collapse: collapse;" width="100%">
                        <tr>
                            <td align="center" class="tdBak" width="20%" >
                                接入单位</td>
                            <td width="30%" >
                                            <asp:TextBox ID="JRDW" runat="server" Width="100%" 
                                    BorderWidth="0"></asp:TextBox>
                                        </td>
                            <td align="center" class="tdBak" width="20%" >
                                用户类型</td>
                            <td width="30%" >
                                            
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                 <td colspan=2><asp:RadioButtonList ID="YHLX" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem>政府单位</asp:ListItem>
                                                <asp:ListItem>公司</asp:ListItem>
                                            </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>                                   
                                    <td style="width:100%;">
                                        <asp:DropDownList ID="GSMC" runat="server" Width="100%" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenSelect('GSMC','','')" />
                                    </td>
                                </tr>
                            </table>
                                        </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                用户负责人</td>
                            <td>
                                            <asp:TextBox ID="YHFZR" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                            <td align="center" class="tdBak">
                                用户电话</td>
                            <td>
                                            <asp:TextBox ID="YHDH" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                单位地址</td>
                            <td>
                                            <asp:TextBox ID="DWDZ" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                            <td align="center" class="tdBak">
                                E-mail</td>
                            <td>
                                            <asp:TextBox ID="EMAIL" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        </tr>
                        </table>
                </td>
            </tr>
             <tr>
                <td align="left" class="tableTitle">
                    &nbsp;&nbsp; 3．技术资料（请在□上打勾）</td>
            </tr>
            <tr>
                <td>
                    <table border="1" bordercolor="#5b9ed1" cellpadding="0" cellspacing="0" 
                        style="border-collapse: collapse;" width="100%">
                        <tr>
                            <td align="center" class="tdBak" width="20%" >
                                接入点总数</td>
                            <td style="width: 80%">
                                            <asp:TextBox ID="JRDZS" runat="server" Width="13%" BorderWidth="0"></asp:TextBox>
                                            接入点（一期工程）</td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak" >
                                申请带宽</td>
                            <td style="width: 75%">
                                            <asp:GridView ID="DKGridView" runat="server" AutoGenerateColumns="False" 
                                                BorderStyle="None" BorderWidth="0px" CellPadding="0" ShowHeader="False">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="SFQY" runat="server" Checked='<%# (((DataRowView)Container.DataItem)["SFQY"].ToString()=="1")?true:false %>'/>
                                                        </ItemTemplate>                                                      
                                                    </asp:TemplateField>                                                   
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="DK" runat="server" Text='<%# Bind("DK") %>'></asp:Label>
                                                        </ItemTemplate>                                                       
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="JRD" runat="server" Width="28%" Text='<%# Bind("JRD") %>'></asp:TextBox>
                                                            接入点
                                                        </ItemTemplate>
                                                        <ItemStyle Width="150px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak" >
                                客户接入方式</td>
                            <td style="width: 75%">
                                            <table style="width:100%;" border=1px>
                                                <tr>
                                                    <td width="10%">
                                                        <asp:CheckBox ID="JRFS_CH_LYQ" runat="server" Text="路由器" />
                                                    </td>
                                                    <td width="15%">
                                                        <asp:RadioButtonList ID="JRFS_LYQ" runat="server">
                                                            <asp:ListItem>静态路由</asp:ListItem>
                                                            <asp:ListItem>动态路由</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td valign="bottom">
                                                        <asp:TextBox ID="JRFS_LYXY" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="JRFS_CH_JHJ" runat="server" Text="交换机" />
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:RadioButtonList ID="JRFS_JHJ" runat="server">
                                                            <asp:ListItem>使用我们提供的IP地址</asp:ListItem>
                                                            <asp:ListItem>使用用户提供的IP地址</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:CheckBox ID="JRFS_CH_SFXYTS" runat="server" Text="是否需要本公司调试" />
                                                    </td>
                                                </tr>
                                            </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak" >
                                VPN名称（中文）</td>
                            <td style="width: 75%">
                                            <table style="width:100%;">
                                                <tr>
                                                    <td width="20%">
                                                        新申请名vpn</td>
                                                    <td width="80%">
                                                        <asp:TextBox ID="VPNMC" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="SFXYHTVPN" runat="server" Text="需要互通的VPN" />
                                                    </td>
                                                    <td width="100%">
                                                        <asp:TextBox ID="HTVPNMC" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak" >
                                附件</td>
                            <td style="width: 75%">
                                            <asp:CheckBox ID="SFTGFJ" runat="server" Text="是否已经提供详细的接入点用户资料和技术资料" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak" >
                                备 注</td>
                            <td style="width: 75%">
                                                        <asp:TextBox ID="BZ" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak" >
                                VPN名称（英文）</td>
                            <td style="width: 75%">
                                                        <asp:TextBox ID="YWVPNMC" runat="server" Width="100%"></asp:TextBox>
                                                    </td>
                        </tr>
                        </table>
                </td>
            </tr>
        </table>
        <asp:TextBox ID="GUID" runat="server" style="display:none"></asp:TextBox>
         <asp:TextBox ID="JDBM" runat="server" style="display:none"></asp:TextBox>
                    <asp:GridView ID="GridViewList" runat="server" SkinID="GridView1" 
                        BorderWidth="1px" AllowPaging="True" AllowSorting="True" CellSpacing="1" 
                        AutoGenerateColumns="False"   >
                        <Columns>
                         <asp:BoundField HeaderText="操作" DataField="QFBJ">
                            <HeaderStyle Width="5%" />
                            <ItemStyle HorizontalAlign="Center"  Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LCJRSJ" HeaderText="流程进入时间" 
                                DataFormatString="{0:yyyy-MM-dd HH:mm}">
                            <HeaderStyle Width="12%" />
                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="当前状态" DataField="DQZT">
                            <HeaderStyle Width="8%" />
                            <ItemStyle Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LCQFSJ" HeaderText="流程操作时间" 
                                DataFormatString="{0:yyyy-MM-dd HH:mm}" >
                            <HeaderStyle Width="12%" />
                            <ItemStyle HorizontalAlign="Center" Width="12%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="QFHZT" HeaderText="操作后状态" >
                            <HeaderStyle Width="8%" />
                            <ItemStyle Width="8%" />
                            </asp:BoundField>  
                            <asp:BoundField DataField="LCQFR" HeaderText="流程操作人" >
                            <HeaderStyle Width="14%" />
                            <ItemStyle Width="14%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LCCLSJ" HeaderText="流程处理时间" >
                            <HeaderStyle Width="12%" />
                            <ItemStyle HorizontalAlign="Right" Width="12%" />
                            </asp:BoundField>                           
                            <asp:BoundField DataField="LCSM" HeaderText="流程说明" >
                            <HeaderStyle Width="30%" />
                            <ItemStyle Width="30%" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
        <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
