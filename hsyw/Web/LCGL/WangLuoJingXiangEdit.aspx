<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WangLuoJingXiangEdit.aspx.cs" Inherits="Web_LCGL_WangLuoJingXiangEdit" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>
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
                </td>
            </tr>
           <tr>
            <td align="center" class="tableTitle">            
                <asp:Label ID="HeadTitle" runat="server"></asp:Label>            
            </td>
            </tr>
            <tr>
                <td>
                    <table border="1" bordercolor="#5b9ed1" cellpadding="0" cellspacing="0" 
                        style="border-collapse: collapse;" width="100%">
                        <tr>
                            <td align="center" class="tdBak" width="25%">
                                申请单位</td>
                            <td width="25%">
                                            <asp:TextBox ID="SQDW" runat="server" Width="100%" 
                                    BorderWidth="0"></asp:TextBox>
                                        </td>
                            <td align="center" class="tdBak" width="25%">
                                申请单编号</td>
                            <td width="25%">
                                            <asp:TextBox ID="SQDBH" runat="server" Width="100%" 
                                    BorderWidth="0"></asp:TextBox>
                                        </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                申请人员</td>
                            <td>
                                            <asp:TextBox ID="SQRY" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                            <td align="center" class="tdBak">
                                联系电话</td>
                            <td>
                                            <asp:TextBox ID="LXDH" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                申请事由</td>
                            <td colspan="3">
                                            <asp:TextBox ID="SQSY" runat="server" Width="100%" BorderWidth="0" 
                                                Height="59px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                        </tr>
                        <tr>
                            <td align="center" class="tdBak">
                                监控时间</td>
                            <td>
                            <asp:TextBox ID="JKKSSJ" runat="server" Width="100%" BorderWidth="0" 
                                onfocus="setDay(this);"></asp:TextBox>
                                        </td>
                            <td align="center" class="tdBak">
                                至</td>
                            <td>
                            <asp:TextBox ID="JKJSSJ" runat="server" Width="100%" BorderWidth="0" 
                                onfocus="setDay(this);"></asp:TextBox>
                                        </td>
                        </tr>
                        <tr  >
                            <td align="center" class="tdBak">
                                监控机房</td>
                            <td colspan="3">
                                            <asp:TextBox ID="JKJF" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        </tr>
                        <tr  >
                            <td align="center" class="tdBak">
                                需监控端口数</td>
                            <td colspan="3">
                                            <asp:TextBox ID="XJKDKS" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
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
