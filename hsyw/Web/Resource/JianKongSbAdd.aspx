<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JianKongSbAdd.aspx.cs" Inherits="Web_Resource_JianKongSbAdd" %>
<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../jquery-1.2.6-vsdoc.js" type="text/javascript"></script>

    <script src="../../calendar.js" type="text/javascript"></script>

    <script src="../../config.js" type="text/javascript"></script>
    <script type="text/javascript">
        function OpenSelect(enumtype, pname, ddlid) {
            
            var url = "ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
            windowOpenPageByWidth(url, "枚举维护", "Btn", "30%", "40%", "10%", "80%");
            document.getElementById("DDLID").value = ddlid;
            document.getElementById("DDLLX").value = enumtype;
        }

        function windowOpenRmssSelectJD() {
            var khbh = $("#KHBH").val();
            var url = "JianKongUserQuery.aspx?YHBH=JKYWBM&YHMC=JKDMC&KHBM=" + khbh;
            windowOpenPage(url, "选择客户资源", null);
            event.returnValue = false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-size:12pt;font-weight:bold;line-height:40px;overflow:hidden">
        信息维护
    </div>
    <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
        <tr>
            <td align="left" colspan="6">
                <asp:Button ID="BtnSave" CssClass="btn_2k3" runat="server" Text="保存" 
                    onclick="BtnSave_Click" />
            </td>
        </tr>
        <tr>
            <td class="tdBak" align="right" width="13%">通道号</td>
            <td width="20%">
                <asp:TextBox ID="TDH" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
            </td>
            <td class="tdBak" align="right" width="13%">监控点业务编码</td>
            <td width="20%">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <asp:TextBox ID="JKYWBM" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
                        </td>
                        <%--<td>
                            <asp:ImageButton ID="JD_TQ" ImageUrl="../Images/Small/gif-0403.gif" runat="server"
                                OnClick="JD_TQ_Click" />
                        </td>--%>
                        <td>
                            <asp:ImageButton ID="JD_Select" runat="server" 
                                ImageUrl="../Images/Small/bb_table.gif" 
                                OnClientClick="windowOpenRmssSelectJD()" />
                        </td>
                    </tr>
                </table>
            </td>
            <td class="tdBak" align="right" width="14%">监控点名称</td>
            <td width="20%">
                <asp:TextBox ID="JKDMC" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td class="tdBak" align="right">监控点安装地址</td>
            <td>
                <asp:TextBox ID="JKAZDD" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
             </td>
            <td class="tdBak" align="right">摄像机类型</td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <asp:DropDownList ID="SXJLX" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <img onclick="OpenSelect('JKPZDSXJLX','','SXJLX')" 
                                src="../Images/Small/bb_table.gif" />
                        </td>
                    </tr>
                </table>
             </td>
            <td class="tdBak" align="right">摄像机型号</td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <asp:DropDownList ID="SXJXH" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <img onclick="OpenSelect('JKPZDSXJXH','','SXJXH')" 
                                src="../Images/Small/bb_table.gif" />
                        </td>
                    </tr>
                </table>
             </td>
        </tr>
         <tr>
            <td class="tdBak" align="right">生产厂商</td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <asp:DropDownList ID="SCCS" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <img onclick="OpenSelect('JKPZDSCCS','','SCCS')" 
                                src="../Images/Small/bb_table.gif" />
                        </td>
                    </tr>
                </table>
             </td>
            <td class="tdBak" align="right">摄像机编号</td>
            <td>
                <asp:TextBox ID="SXJBH" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
             </td>
            <td class="tdBak" align="right">数字编码</td>
            <td>
                <asp:TextBox ID="SZBM" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
             </td>
        </tr>
         <tr>
             <td class="tdBak" align="right">DB33编码</td>
            <td>
                <asp:TextBox ID="DB33BM" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
             </td>
            <td class="tdBak" align="right">所属街道</td>
            <td>
                <asp:TextBox ID="SSJD" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
             </td>
            <td class="tdBak" align="right">类型</td>
            <td>
                <asp:TextBox ID="LX" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
             </td>
        </tr>
          <tr>
             <td class="tdBak" align="right">立杆型号</td>
            <td>
                <asp:TextBox ID="LGXH" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
              </td>
            <td class="tdBak" align="right">立杆厂商</td>
            <td>
                <asp:TextBox ID="LGCS" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
              </td>
            <td class="tdBak" align="right">开工日期</td>
            <td>
                <asp:TextBox ID="KGRQ" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                            Width="100%"></asp:TextBox>
              </td>
        </tr>
          <tr>
              <td class="tdBak" align="right">调试完成日期</td>
            <td>
                <asp:TextBox ID="TSWCRQ" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                            Width="100%"></asp:TextBox>
              </td>
            <td class="tdBak" align="right">施工单位</td>
            <td>
                <asp:TextBox ID="SGDW" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
              </td>
            <td class="tdBak" align="right">共享单位</td>
            <td>
                <asp:TextBox ID="GXDW" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
              </td>
        </tr>
          <tr>
              <td class="tdBak" align="right">维护单位</td>
            <td>
                <asp:TextBox ID="WHDW" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
              </td>
            <td class="tdBak" align="right">是否验收</td>
            <td>
                <asp:RadioButtonList ID="SWYS" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem>是</asp:ListItem>
                    <asp:ListItem>否</asp:ListItem>
                </asp:RadioButtonList>
              </td>
            <td class="tdBak" align="right">备注</td>
            <td>
                <asp:TextBox ID="BZ" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
              </td>
        </tr>
    </table>
     <uc1:windowHeader ID="windowHeader1" runat="server" />
    <div style="display:none">
               <asp:TextBox ID="DDLID" runat="server"></asp:TextBox>
         <asp:TextBox ID="DDLLX" runat="server"></asp:TextBox>
         <asp:TextBox ID="GUID" runat="server"></asp:TextBox>
         <asp:TextBox ID="LSID" runat="server"></asp:TextBox>
         <asp:TextBox ID="KHBH" runat="server"></asp:TextBox>
          <asp:Button ID="Btn" runat="server" Text="Button" OnClick="Btn_Click" />
    </div>
    </form>
</body>
</html>
