<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ICPBA.aspx.cs" Inherits="Web_INFO_ICPBA" %>


<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <script src="InfoValidator.js" type="text/javascript"></script>
     <script language="javascript" src="../../config.js"></script>
    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>
    <script type="text/javascript">
        function windowOpen(lx, id) {
            var url = "ICPBAEdit.aspx?lx=" + lx + "&id=" + id;
            windowOpenPage(url, "信息维护", "BtnRfh");
        }
        function ExpTemp() {
            window.open("../../template/ICP备案模板.xls");
            return false;
        }
        function btnImport() {
            document.getElementById("ImportDiv").style.display = "block";
            return false;
        }
        function Close() {
            document.getElementById("ImportDiv").style.display = "none";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <table width="100%" style="height:100%">
          <%--按钮--%>
            <tr>
                <td class="tdHead">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="BtnQuery" runat="server" CssClass="btn_2k3" Text="查询" 
                                    onclick="BtnQuery_Click" />
                            </td>
                            <td>

                                <input id="BtnAdd" type="button" value="增加" class="btn_2k3" onclick="windowOpen('add','');" />
                            </td>
                            <td>
                                <asp:Button ID="BtnEdit" runat="server" CssClass="btn_2k3" Text="编辑" 
                                    onclick="BtnEdit_Click" OnClientClick="return Btn_Edit();" />
                            </td>
                            <td>
                                <asp:Button ID="BtnDel" runat="server" CssClass="btn_2k3" Text="删除" 
                                    onclick="BtnDel_Click" OnClientClick="return Btn_Del();" />
                            </td>
                            <td>
                                 <asp:Button ID="BtnRfh" runat="server" CssClass="btn_2k3" Text="刷新" 
                                     onclick="BtnRfh_Click" />
                            </td>
                            <td>
                                 <asp:Button ID="BtnExl" runat="server" CssClass="btn_2k3" Text="导出Excel" 
                                     onclick="BtnExl_Click" />
                            </td>
                            <td>
                                 <asp:Button ID="BtnExlTemp" runat="server" CssClass="btn_2k3" Text="导出Excel模板" 
                                     OnClientClick="return ExpTemp();" />
                            </td>
                            <td >
                                 <asp:Button ID="BtnImp" runat="server" CssClass="btn_2k3" Text="导入Excel" OnClientClick="return btnImport();" />
                            </td>
                            <td>
                                 <asp:Button ID="BtnAll" runat="server" CssClass="btn_2k3" Text="全选" 
                                     onclick="BtnAll_Click" />
                            </td>
                            <td >
                                <asp:Button ID="BtnCancel" runat="server" CssClass="btn_2k3" Text="取消" 
                                    onclick="BtnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--查询条件--%>
            <tr>
                <td class="tdBak">
                    <table  cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                    bordercolor="#5b9ed1" width="100%">
                        <tr>
                            <td width="20%" align="right" class="tdBak">主办单位名称</td>
                            <td  width="30%">
                                <asp:TextBox ID="TxtZbdw" Width="100%" runat="server"></asp:TextBox>
                            </td>
                            <td  width="20%" align="right" class="tdBak">网站负责人姓名</td>
                            <td colspan="2"  width="30%">
                                <asp:TextBox ID="TxtWzfzrmc" Width="100%" runat="server"></asp:TextBox> 
                            </td>
                        </tr>
                         <tr>
                            <td align="right" class="tdBak">网站域名列表</td>
                            <td>
                                <asp:TextBox ID="TxtWzymlb" Width="100%" runat="server"></asp:TextBox>
                            </td>
                            <td align="right" class="tdBak">IP地址列表</td>
                            <td colspan="2">
                                <asp:TextBox ID="TxtIpdzlb" Width="100%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td align="right" class="tdBak">审批结果</td>
                            <td>
                                <asp:TextBox ID="TxtSpjg" Width="100%" runat="server"></asp:TextBox>
                            </td>
                            <td align="right" class="tdBak">导入日期</td>
                            <td>
                                 <asp:TextBox ID="Kssj" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                            Width="100%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="Jssj" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                            Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--GridView--%>
            <tr>
                <td style="height: 100%;">
                <div style="overflow: auto; height: 100%; width: 100%;">
                    <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                        SkinID="GridView1" AllowPaging="True" PageSize="20" 
                        onrowdatabound="GridView1_RowDataBound" DataKeyNames="GUID">
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="主办单位名称">
                                <ItemTemplate>
                                    <a href="javascript:windowOpen('Edit','<%# Eval("GUID") %>');" 
                                        style="text-decoration: underline;"><%# Eval("ZBDW")%></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="网站负责人姓名" DataField="WZFZRMC">
                                <HeaderStyle />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="网站首页网址" DataField="WZSYDZ">
                                <HeaderStyle />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="网站域名列表" DataField="WZYMLB">
                                <HeaderStyle />
                            </asp:BoundField>
                             <asp:BoundField HeaderText="IP地址列表" DataField="IPDZLB">
                                <HeaderStyle />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="导入日期" DataField="DRRQ" HtmlEncode=false DataFormatString="{0:yyyy-MM-dd}" >
                                <HeaderStyle />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="审批结果" DataField="SPJG">
                                <HeaderStyle />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
            </tr>
            <%# Eval("TSLX") %>
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

        <div id="ImportDiv" runat="server" style="display:none;border: 2px solid #5b9ed1;
        position: absolute; z-index: inherit; width: 500px; height: 100px;top:500px;left:100px;margin-top:-350px">
        <table style="width: 100%; height: 100%" border="0px" cellpadding="0" cellspacing="0">
            <tr style="height: 29px" ondblclick="MaxWinodw(document.getElementById('XPMax'));">
                <td width="29px" style="background-image: url('../images/IE7.gif')">
                    <asp:Label ID="Label3" runat="server" Text="" Width="29px"></asp:Label>
                </td>
                <td style="background-image: url('../images/WindowXPHead.gif')" width="60%">
                    <asp:Label ID="Label4" runat="server" ForeColor="White" Font-Bold="True"></asp:Label>
                </td>
                <td style="background-image: url('../images/WindowXPHead.gif'); width: 40%" align="right"
                    valign="middle">
                  <img alt="" src="../images/WindowXPClose.gif" border="0" id="Img3" title="关闭退出"
                            onclick="javascript:Close();">
                </td>
            </tr>
            <tr id="Tr1">
                <td colspan="3">
                   <table width="100%">
                        <tr>
                            <td width="40%" align="right" class="tdBak">请选择要上传的文件:</td>
                            <td class="tdBak">
                                <asp:FileUpload ID="FileUpLoad1" runat="server" />
                            </td>
                        </tr>
                         <tr>
                            <td colspan="2" class="tdBak" align="center">
                                <asp:Button ID="BtnOk" runat="server" Text="导入" CssClass="btn_2k3" 
                                    onclick="BtnOk_Click" />
                            </td>
                        </tr>
                   </table>
                </td>
            </tr>
        </table>
    </div>
        
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>