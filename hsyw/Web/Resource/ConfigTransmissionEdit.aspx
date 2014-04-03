<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigTransmissionEdit.aspx.cs"
    Inherits="Web_Resource_ConfigTransmissionEdit" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style>
    .td
    {
       border:solid 1px red; 	
    }
</style>
    <script src="../../jquery-1.2.6.min.js" type="text/javascript"></script>
    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="../../config.js"></script>
    <script language="javascript" type="text/javascript">
        function windowOpen(guid) {
        var ywguid=document.getElementById("YWGUID").value;
        var zyhs_bj=document.getElementById("ZYHS_BJ").value;
        var url = "ConfigTransmissionEdit1.aspx?GUID=" + guid + "&YWGUID=" + ywguid + "&ZYHS_BJ=" + zyhs_bj;
            windowOpenPage(url, "传输业务管理", "Btn");
            window.event.returnValue = false;
        }
        function OpenBranch(name, code) {
            windowOpenPageByWidth("BranchTree.aspx?NAME=" + name + "&CODE=" + code+"&ISQY=1", "选择所属区域", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }
        function OpenSelect(enumtype, pname, ddlid) {
            var url = "ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
            windowOpenPageByWidth(url, "枚举维护", "Btn1", "30%", "40%", "10%", "80%");
            document.getElementById("DDLID").value = ddlid;
            document.getElementById("DDLLX").value = enumtype;
        }
        function windowOpenRmssSelectJD() {
            var url = "RmssSelect.aspx?YWBM_NAME=JD_SUBSCRIBER_ID";
            windowOpenPage(url, "选择客户资源", "BtnRmssJD");
           event.returnValue = false;
        }
        function windowOpenRmssSelectYD() {
            var url = "RmssSelect.aspx?YWBM_NAME=YD_SUBSCRIBER_ID";
            windowOpenPage(url, "选择客户资源", "BtnRmssYD"); 
            event.returnValue = false;
        }
        function windowOpenRmssJDTQ() {
            var url = "RmssSelect.aspx?YWBM_NAME=JD_SUBSCRIBER_ID&SUBSCRIBER_CODE="+document.getElementById("JD_SUBSCRIBER_CODE").value;
            windowOpenPage(url, "选择客户资源", "BtnRmssJD"); 
           // event.returnValue = false;
        }
         function windowOpenRmssYDTQ() {
            var url = "RmssSelect.aspx?YWBM_NAME=YD_SUBSCRIBER_ID&SUBSCRIBER_CODE="+document.getElementById("YD_SUBSCRIBER_CODE").value;
            windowOpenPage(url, "选择客户资源", "BtnRmssYD"); 
           // event.returnValue = false;
        }

        function OpneLogList() {
            var guid = document.getElementById("YWGUID").value;
            var url = "../LogList.aspx?PK_GUID=" + guid;
            windowOpenPage(url, "操作日志", "");
        }

        function YwBh(mc, txtid, rblid) {
            var lx = $('input[@name='+rblid+'][@checked]').val();

            var bh = $(txtid).val();
     
            if (lx == "SGD") {
                Web_Resource_ConfigTransmissionEdit.getKhBhRow(bh, function(response) {
                    if (response.error) {
                        alert(response.error);
                    }
                    else {
                        if (response.value != "0") {
                            alert(mc + "重复，请重新输入");
                            $("#BtnSave").css("display", "none");
                            $(txtid).parent().addClass("td");
                        }
                        else {
                            $("#BtnSave").css("display", "block");
                            $(txtid).parent().removeClass("td");
                        }
                    }
                })
              
            }
        }
         function ViewPzb()
        {
            var bh = document.getElementById("JD_SUBSCRIBER_CODE").value;
            var ybh = document.getElementById("YD_SUBSCRIBER_CODE").value;
            windowOpenPage("ZyPzList.aspx?bh="+bh +"&ybh="+ybh,"操作日志", "");
            event.returnValue=false;
            
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%;">
     <tr>
            <td class="tableHead">
             <table style="width:100%;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td  width="1" align="right">
                <asp:Button ID="BtnSave" runat="server" Text="保存" CssClass="btn_2k3" OnClick="BtnSave_Click" />
                <asp:Button ID="BtnRmssJD" runat="server" onclick="BtnRmssJD_Click"  style="display:none" />
                <asp:Button ID="BtnRmssYD" runat="server" onclick="BtnRmssYD_Click"  style="display:none" />
                </td>
                <td>
                  <asp:Button ID="BtnZyhs" runat="server" Text="资源回收" CssClass="btn_2k3" OnClick="BtnZyhs_Click" 
                    onclientclick="return confirm(&quot;确定操作吗？&quot;);" />
                    
                    <asp:Button ID="BtnExp" runat="server" Text="导出Excel" CssClass="btn_2k3" 
                        onclick="BtnExp_Click" />
                         <asp:Button ID="BtnView" runat="server" Text="查看相关配置单" OnClientClick="ViewPzb();" CssClass="btn_2k3" />
                    </td>
                    <td>
                       
                    </td>
                    <td align="right">
                    <a href="#" onclick="OpneLogList()" >操作日志</a></td></tr></table>
            </td>
        </tr>
     <tr>
    <td>
     <div style="overflow: auto; width: 100%; height: 100%">
    <table style="width: 100%; height: 100%;">
      
        <tr>
            <td>
                <table  style="border-collapse: collapse; width: 100%" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                       
                        <td class="tdBak" width="10%" align="center">
                            组网方式</td>
                        <td width="15%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:DropDownList ID="ZWFS" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenSelect('ZWFS','','ZWFS')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="10%">
                            链路带宽</td>
                        <td width="15%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:DropDownList ID="LLDK" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenSelect('LLDK','','LLDK')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="10%">
                            配置日期</td>
                        <td width="15%">
                            <asp:TextBox ID="PZRQ" runat="server" Width="100%" BorderWidth="0" 
                                onfocus="setDay(this);"></asp:TextBox>
                            </td>
                             <td align="center" class="tdBak" width="10%">
                            配置人</td>
                        <td width="15%">
                                            <asp:TextBox ID="PZR" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                            </td>
                    </tr>
                    <tr>
                     <td align="center" class="tdBak" width="10%">
                            VLAN-ID</td>
                        <td width="15%">
                            <asp:TextBox ID="VLANID" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                       
                        <td class="tdBak" width="10%" align="center">
                            修改记录</td>
                        <td width="15%" colspan="5">
                                            <asp:TextBox ID="XGJL" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                            </td>
                    </tr>
                    <tr>
                     <td align="center" class="tdBak" width="10%">
                            第三方运营商</td>
                        <td width="15%">
                            <asp:TextBox ID="DSFYYS" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                            </td>
                       
                        <td class="tdBak" width="10%" align="center">
                            <span lang="zh-cn">联系人</span></td>
                            <td>
                            <asp:TextBox ID="LXR" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                            <td class="tdBak" width="10%" align="center"><span lang="zh-cn">联系电话</span></td>
                            <td>
                            <asp:TextBox ID="LXDH" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                        </td>
                        <td width="15%" colspan="2"  class="tdBak">
                          </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" colspan="4">
                            甲端信息<asp:TextBox ID="JD_SUBSCRIBER_ID" runat="server" BorderWidth="0" style="display:none;"></asp:TextBox>
                                    </td>
                        <td align="center" class="tdBak" colspan="4">
                            乙端信息<asp:TextBox ID="YD_SUBSCRIBER_ID" runat="server" BorderWidth="0" style="display:none;"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="10%">
                            业务编码</td>
                        <td width="15%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="JD_SUBSCRIBER_CODE" runat="server" Width="100%" 
                                            BorderWidth="0" Onchange="YwBh('甲端业务编码','#JD_SUBSCRIBER_CODE','JD_SUBSCRIBER_GDLY')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="JD_TQ" ImageUrl="../Images/Small/gif-0403.gif" 
                                            runat="server" onclick="JD_TQ_Click" /></td>
                                    <td>
                                        <asp:ImageButton  runat="server" ID="JD_Select" ImageUrl="../Images/Small/bb_table.gif" OnClientClick="windowOpenRmssSelectJD()"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdBak" width="10%" align="center">
                            工单来源</td>
                        <td width="15%">
                            <asp:RadioButtonList ID="JD_SUBSCRIBER_GDLY" runat="server" 
                                RepeatDirection="Horizontal" AutoPostBack="True" 
                                onselectedindexchanged="JD_SUBSCRIBER_GDLY_SelectedIndexChanged">
                                <asp:ListItem Selected="True">BOSS</asp:ListItem>
                                <asp:ListItem Value="SGD">手工单</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="center" class="tdBak" width="10%">
                            业务编码</td>
                        <td width="15%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="YD_SUBSCRIBER_CODE" runat="server" Width="100%" 
                                            BorderWidth="0" Onchange="YwBh('乙端业务编码','#YD_SUBSCRIBER_CODE','YD_SUBSCRIBER_GDLY')"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="YD_TQ" ImageUrl="../Images/Small/gif-0403.gif" 
                                            runat="server" onclick="YD_TQ_Click"  /></td>
                                    
                                    <td>
                                         <asp:ImageButton  runat="server" ID="YD_Select"  src="../Images/Small/bb_table.gif"  OnClientClick="windowOpenRmssSelectYD()"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" class="tdBak" width="10%">
                            工单来源</td>
                        <td width="15%">
                            <asp:RadioButtonList ID="YD_SUBSCRIBER_GDLY" runat="server" 
                                RepeatDirection="Horizontal" AutoPostBack="True" 
                                onselectedindexchanged="YD_SUBSCRIBER_GDLY_SelectedIndexChanged">
                                <asp:ListItem Selected="True">BOSS</asp:ListItem>
                                <asp:ListItem Value="SGD">手工单</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="10%">
                            业务联系人</td>
                        <td width="15%">
                                            <asp:TextBox ID="JD_LINKMAN" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td class="tdBak" width="10%" align="center">
                            所属区域</td>
                        <td width="15%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="JD_REGION" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="OpenBranch('JD_REGION','JD_REGION_CODE')" />
                                                    </td>
                                                </tr>
                                            </table> </td>
                        <td align="center" class="tdBak" width="10%">
                            业务联系人</td>
                        <td width="15%">
                                            <asp:TextBox ID="YD_LINKMAN" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td align="center" class="tdBak" width="10%">
                            所属区域</td>
                        <td width="15%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="YD_REGION" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../Images/Small/bb_table.gif" onclick="OpenBranch('YD_REGION','YD_REGION_CODE')" />
                                                    </td>
                                                </tr>
                                            </table> </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="10%">
                            业务名称</td>
                        <td colspan="3" style="width: 26%">
                                            <asp:TextBox ID="JD_SUB_NAME" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td align="center" class="tdBak" width="10%">
                            业务名称</td>
                        <td colspan="3" style="width: 26%">
                                            <asp:TextBox ID="YD_SUB_NAME" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="10%">
                            客户编码</td>
                        <td width="15%">
                                            <asp:TextBox ID="JD_CUSTOMER_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td class="tdBak" width="10%" align="center">
                            客户等级</td>
                        <td width="15%">
                                            <asp:DropDownList ID="JD_CUSTOMER_LEVEL" runat="server" AutoPostBack="True" 
                                                Width="100%">
                                                <asp:ListItem Value="1">1级</asp:ListItem>
                                                <asp:ListItem Value="2">2级</asp:ListItem>
                                                <asp:ListItem Value="3">3级</asp:ListItem>
                                                <asp:ListItem Value="4">4级</asp:ListItem>
                                                <asp:ListItem Value="5">5级</asp:ListItem>
                                                <asp:ListItem Value="6">6级</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                        <td align="center" class="tdBak" width="10%">
                            客户编码</td>
                        <td width="15%">
                                            <asp:TextBox ID="YD_CUSTOMER_CODE" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td align="center" class="tdBak" width="10%">
                            客户等级</td>
                        <td width="15%">
                                            <asp:DropDownList ID="YD_CUSTOMER_LEVEL" runat="server" AutoPostBack="True" 
                                                Width="100%">
                                                <asp:ListItem Value="1">1级</asp:ListItem>
                                                <asp:ListItem Value="2">2级</asp:ListItem>
                                                <asp:ListItem Value="3">3级</asp:ListItem>
                                                <asp:ListItem Value="4">4级</asp:ListItem>
                                                <asp:ListItem Value="5">5级</asp:ListItem>
                                                <asp:ListItem Value="6">6级</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="10%">
                            客户类型</td>
                        <td width="15%">
                                            <asp:TextBox ID="JD_CUSTTYPE1" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td class="tdBak" width="10%" align="center">
                            客户类型</td>
                        <td width="15%">
                                            <asp:TextBox ID="JD_CUSTTYPE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td align="center" class="tdBak" width="10%">
                            客户类型</td>
                        <td width="15%">
                                            <asp:TextBox ID="YD_CUSTTYPE1" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td align="center" class="tdBak" width="10%">
                            客户类型</td>
                        <td width="15%">
                                            <asp:TextBox ID="YD_CUSTTYPE" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="10%">
                            客户名称</td>
                        <td colspan="3" style="width: 26%">
                                            <asp:TextBox ID="JD_CUSTOMER_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td align="center" class="tdBak" width="10%">
                            客户名称</td>
                        <td colspan="3" style="width: 26%">
                                            <asp:TextBox ID="YD_CUSTOMER_NAME" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="10%">
                            客户地址</td>
                        <td colspan="3" style="width: 26%">
                                            <asp:TextBox ID="JD_ADDRESS" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td align="center" class="tdBak" width="10%">
                            客户地址</td>
                        <td colspan="3" style="width: 26%">
                                            <asp:TextBox ID="YD_ADDRESS" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="10%">
                            Email</td>
                        <td width="15%">
                                            <asp:TextBox ID="JD_EMAIL" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td class="tdBak" width="10%" align="center">
                            手机号码</td>
                        <td width="15%">
                                            <asp:TextBox ID="JD_MOBILE_N" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td align="center" class="tdBak" width="10%">
                            Email</td>
                        <td width="15%">
                                            <asp:TextBox ID="YD_EMAIL" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td align="center" class="tdBak" width="10%">
                            手机号码</td>
                        <td width="15%">
                                            <asp:TextBox ID="YD_MOBILE_NO" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="10%">
                            电话号码</td>
                        <td width="15%">
                                            <asp:TextBox ID="JD_PHONE_NO" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td class="tdBak" width="10%" align="center">
                            传真号码</td>
                        <td width="15%">
                                            <asp:TextBox ID="JD_FAX_NO" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td align="center" class="tdBak" width="10%">
                            电话号码</td>
                        <td width="15%">
                                            <asp:TextBox ID="YD_PHONE_NO" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td align="center" class="tdBak" width="10%">
                            传真号码</td>
                        <td width="15%">
                                            <asp:TextBox ID="YD_FAX_NO" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tdBak" width="10%">
                            邮编</td>
                        <td width="15%">
                                            <asp:TextBox ID="JD_ZIP_CODE" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td class="tdBak" width="10%" align="center">
                            销售员</td>
                        <td width="15%">
                                            <asp:TextBox ID="JD_SALE_NAME" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td align="center" class="tdBak" width="10%">
                            邮编</td>
                        <td width="15%">
                                            <asp:TextBox ID="YD_ZIP_CODE" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                                        </td>
                        <td align="center" class="tdBak" width="10%">
                            销售员</td>
                        <td width="15%">
                                            <asp:TextBox ID="YD_SALE_NAME" runat="server" Width="100%" 
                                BorderWidth="0"></asp:TextBox>
                                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
        <td class="tableHead">
            <asp:Button ID="AddButton" runat="server" Text="新增" CssClass="btn_2k3" 
                OnClientClick="windowOpen('NEW')" />
            <asp:Button ID="DeleteButton" runat="server" Text="删除" CssClass="btn_2k3" 
             OnClientClick="return confirm(&quot;确定要删除吗？&quot;);"   onclick="DeleteButton_Click" />
            </td>
        </tr>
        <tr>
            <td style="height: 100%" valign="top">               
                    <asp:GridView ID="gvCSYWList" runat="server" SkinID="GridView1" DataKeyNames="GUID,JDSBDK_GUID,YDSBDK_GUID"
                        BorderColor="#5B9ED1" BorderWidth="1px" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" OnRowDataBound="gvCSYWList_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <ItemTemplate>
                                    <asp:CheckBox ID="XZ1" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="40px"
                                    Wrap="true" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" Width="40px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="BH" HeaderText="编号">
                                <ItemStyle HorizontalAlign="Center" BorderColor="#5b9ed1" BorderWidth="1px" Width="50px"
                                    Wrap="true" />
                                <HeaderStyle BorderColor="#5b9ed1" BorderWidth="1px" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="JDJRJF" HeaderText="甲端接入机房">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="100px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="100px" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JDJRSB" HeaderText="接入设备">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="100px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="100px" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JDSBDK" HeaderText="端口号">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="50px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="50px" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="JDJRWLSX" HeaderText="网络时隙">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="150px" />
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="150px" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="YDJRJF" HeaderText="乙端接入机房">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="100px" HorizontalAlign="Center">
                                </HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="100px" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="YDJRSB" HeaderText="接入设备">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="100px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="100px" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="YDSBDK" HeaderText="端口号">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="50px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="50px" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="YDJRWLSX" HeaderText="网络时隙">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="150px" />
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="150px" Wrap="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SM" HeaderText="说明">
                                <HeaderStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="200px"></HeaderStyle>
                                <ItemStyle BorderColor="#5B9ED1" BorderWidth="1px" Width="200px" Wrap="true"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>               
            </td>
        </tr>
    </table>
    </div>
    </td>
    </tr>
    </table>
    <asp:TextBox ID="ZYHS_BJ" runat="server" Width="100%" BorderWidth="0" style="display:none;"></asp:TextBox>
    <asp:Button ID="Btn" runat="server" Text="Button" OnClick="Btn_Click" Style="display: none;" />
    <asp:Button ID="Btn1" runat="server" Text="Button" OnClick="Btn1_Click" Style="display: none;" />
    <asp:TextBox ID="YWGUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="SSQY_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="BH" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="DDLID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="DDLLX" runat="server" Style="display: none"></asp:TextBox>
     <asp:TextBox ID="JD_REGION_CODE" runat="server" Width="100%" BorderWidth="0" style="display:none;"></asp:TextBox>
      <asp:TextBox ID="YD_REGION_CODE" runat="server" Width="100%" BorderWidth="0" style="display:none;"></asp:TextBox>
       <asp:TextBox ID="CREATEDATETIME" runat="server" Width="100%" BorderWidth="0" style="display:none;"></asp:TextBox>
       <asp:TextBox ID="UPDATEDATETIME" runat="server" Width="100%" BorderWidth="0" style="display:none;"></asp:TextBox>
        <asp:TextBox ID="UPDATEUSERNAME" runat="server" Width="100%" BorderWidth="0" style="display:none;"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
