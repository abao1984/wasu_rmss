<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JianKongSbEdit.aspx.cs" Inherits="Web_Resource_JianKongSbEdit" %>
<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <script type="text/javascript">
        function windowOpenRmssSelectJD() {
            var url = "RmssSelect.aspx?YWBM_NAME=KHBH";
            windowOpenPage(url, "选择客户资源", "BtnSetRmss");
            event.returnValue = false;
        }

        //选择设备数据
        function getJkSb() {
            var url = "JianKongSbQuery.aspx?SBID=JKSBID&SBMC=JKSB";
            windowOpenPage(url, "选择客户资源", "BtnSetRmss");
            event.returnValue = false;
        }

        //删除主表数据
        function DelZb() {
            if (!confirm("删除主表数据时也会很从表中的子数据删除，确认要删除吗?")) {
                return false;
            }
            return true;
        }

        //检查主表记录是否已保存
        function checkZbGuid(guid) {
            //这个GUID是主表的，所以他是从表的LSID，参数就是从表的GUID 罗耀斌
            var lsid = $("#GUID").val();
            if (lsid == "" || lsid == null) {
                alert('请先保存主表数据');
                return false;
            }
            var khbh = $("#KHBH").val();
            var url = "JianKongSbAdd.aspx?lsid=" + lsid + "&guid=" + guid + "&khbh=" + khbh;
            windowOpenPage(url,"监控配置单", "Btn");
            event.returnValue = false;
        }

        //删除从表数据
        function DeleteCb() {
            if (!confirm("确认要删除数据吗?")) {
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" height="100%" style="border-collapse: collapse;" cellpadding="1"
        cellspacing="0" border="1" bordercolor="#5b9ed1">
        <tr height="30px">
            <td align="left" colspan="6" class="tdBak">
                <asp:Button ID="BtnSave" runat="server" Text="保存" CssClass="btn_2k3" 
                    onclick="BtnSave_Click" />
                <asp:Button ID="BtnDelete" runat="server" Text="删除" CssClass="btn_2k3" 
                    onclick="BtnDelete_Click" OnClientClick="return DelZb();" />
                <asp:Button ID="BtnExp" runat="server" Text="导出" CssClass="btn_2k3" 
                    onclick="BtnExp_Click" />
            </td>
        </tr>
        <tr height="30px">
            <td class="tdBak" align="right" width="12%">
                客户编号
            </td>
            <td width="20%">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <asp:TextBox ID="KHBH" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <%--<td>
                            <asp:ImageButton ID="JD_TQ" ImageUrl="../Images/Small/gif-0403.gif" runat="server"
                                OnClick="JD_TQ_Click" />
                        </td>--%>
                        <td>
                            <asp:ImageButton runat="server" ID="JD_Select" ImageUrl="../Images/Small/bb_table.gif"
                                OnClientClick="windowOpenRmssSelectJD()" />
                        </td>
                    </tr>
                </table>
            </td>
            <td class="tdBak" align="right" width="13%">
                客户名称
            </td>
            <td width="20%">
                <asp:TextBox ID="KHMC" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
            </td>
            <td class="tdBak" align="right" width="15%">
                客户所属区域
            </td>
            <td width="20%">
                <asp:TextBox ID="KHQY" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
            </td>
        </tr>
        <tr height="30px">
            <td class="tdBak" align="right" width="15%">
                项目分类
            </td>
            <td>
                <asp:TextBox ID="XMFL" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
            </td>
            <td class="tdBak" align="right" width="15%">
                监控平台信息
            </td>
            <td>
                <asp:TextBox ID="JKPTXX" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
            </td>
            <td class="tdBak" align="right" width="15%">
                基础链路业务编码
            </td>
            <td>
                <asp:TextBox ID="YWBM" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
            </td>
        </tr>
        <tr height="30px">
            <td class="tdBak" align="right" width="15%">
                IP地址
            </td>
            <td>
                <asp:TextBox ID="IPDZ" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
            </td>
            <td class="tdBak" align="right" width="15%">
                监控设备
            </td>
            <td>
                 <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <asp:TextBox ID="JKSB" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <%--<td>
                            <asp:ImageButton ID="JD_TQ" ImageUrl="../Images/Small/gif-0403.gif" runat="server"
                                OnClick="JD_TQ_Click" />
                        </td>--%>
                        <td>
                            <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="../Images/Small/bb_table.gif"
                                OnClientClick="getJkSb()" />
                        </td>
                    </tr>
                </table>
            </td>
            <td colspan="2" bgcolor="silver">
            </td>
        </tr>
        <tr height="30px">
            <td class="tdBak" align="right" colspan="6">
                <asp:Button ID="BtnAdd" runat="server" Text="增加" CssClass="btn_2k3" OnClientClick="return checkZbGuid('');" />
                <asp:Button ID="BtnDel" runat="server" Text="删除" CssClass="btn_2k3" 
                    onclick="BtnDel_Click" OnClientClick="return DeleteCb();" />
            </td>
        </tr>
        <tr>
            <td colspan="6" valign="top">
                <asp:GridView ID="GridViewCb" SkinID="GridView1" runat="server" 
                    AutoGenerateColumns="False" DataKeyNames="GUID" 
                    onrowdatabound="GridViewCb_RowDataBound">
                 <Columns>
                        <asp:TemplateField HeaderText=" 选择">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="TDH" HeaderText="通道号" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="jkywbm" HeaderText="监控点业务编码" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="jkdmc" HeaderText="监控点名称" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="jkazdd" HeaderText="监控点安装地址" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="sccs" HeaderText="生产厂商" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="kgrq" HeaderText="开工日期" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="swys" HeaderText="是否验收" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
<div style="display:none">
    <asp:Button ID="BtnSetRmss" runat="server" Text="Button" 
        onclick="BtnSetRmss_Click" />
        <asp:TextBox ID="JKSBID" runat="server"></asp:TextBox>
     <asp:TextBox ID="GUID" runat="server"></asp:TextBox>
     <asp:Button ID="Btn" runat="server" Text="刷新" onclick="Btn_Click" />
</div>
    
    </form>
</body>
</html>
