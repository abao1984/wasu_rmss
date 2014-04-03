<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FanDanZiEdit.aspx.cs" Inherits="Web_GZCL_FDZ_FanDanZiEdit" %>

<%@ Register Src="windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html>
<head id="Head1" runat="server">
    <title></title>

    <script language="javascript" type="text/javascript" src="../../../calendar.js"></script>

    <script language="javascript" type="text/javascript">
        function divShow(obj1, obj2) {//obj1图片对象，obj2 tr的ID
            var obj = document.getElementById(obj2);
            var objImage = document.getElementById(obj1);
            if (obj.style.display == "none") {
                objImage.src = "../../Images/del_up.gif";
                obj.style.display = "block";
            }
            else {
                objImage.src = "../../Images/add_up.gif";
                obj.style.display = "none";
            }
        }
        function OpenCL(type) {
            var ZBGUID = document.getElementById("ZBGUID").value;
            var per = document.getElementById("per").value;
            //var p = document.getElementById("per").value;
            var lczt = "";
            if (per == "dhsl") {
                lczt = "电话处理";
            }
            var str = window.showModalDialog("FanDanZiChuLi.aspx?ZBGUID=" + ZBGUID + "&Type=" + type + "&per=" + per + "&lczt=" + encodeURI(lczt), "", "dialogWidth:400px;dialogHeight:300px;center:yes;location:no;status:no;");
            window.event.returnValue = false;
            if (str == "true") {
                parent.WindowClose();
            }
        }

        function OpenXF() {
            var ZBGUID = document.getElementById("ZBGUID").value;
            var p = document.getElementById("per").value;
            var lczt = "";
            if (p == "dhsl") {
                lczt = "电话处理";
            }
            var str = window.showModalDialog("../GuZhangXiuFu.aspx?ZBGUID=" + ZBGUID + "&lczt=" + encodeURI(lczt), "", "dialogWidth:400px;dialogHeight:420px;center:yes;location:no;status:no;");
            window.event.returnValue = false;
            if (str == "true") {
                parent.WindowClose();
            }
        }
        function OpenBranch(name, code) {
            windowOpenPageByWidth("../../Resource/BranchTree.aspx?NAME=" + name + "&CODE=" + code + "&ISQY=1", "选择所属区域", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }

        function OpenDDFD(lb) {

            var ZBGUID = document.getElementById("ZBGUID").value;
            
            var str = window.showModalDialog("DiaoDuFaDan.aspx?ZBGUID=" + ZBGUID + "&lb=" + lb , "", "dialogWidth:400px;dialogHeight:420px;center:yes;location:no;status:no;");
            window.event.returnValue = false;
            if (str == "true") {
                parent.WindowClose();
            }
        }

        function windowOpenRmssTQ() {
            var url = "../../Resource/RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID&SUBSCRIBER_CODE=" + document.getElementById("YWBH").value;
            windowOpenPage(url, "选择客户资源", "BtnRmss");
            // event.returnValue = false;
        }

        function windowOpenRmssSelect() {
            var url = "../../Resource/RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID";
            windowOpenPage(url, "选择客户资源", "BtnRmss");
            window.event.returnValue = false;
        }

        function OpenGZ() {
            var p = document.getElementById("per").value;
            var lczt = "";
            if (p == "dhsl") {
                lczt="电话处理";
            }
            var guid = document.getElementById("zbguid").value;
            var url = "../JinChengGengZhong.aspx?id=" + guid + "&lczt=" + encodeURI(lczt);
            windowOpenPageByWidth(url, "进程跟踪", "btnSX", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;
        height: 100%" width="100%">
        <tr>
            <td style="height: 1px">
                <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;
                    height: 1px" width="100%">
                    <tr>
                        <td class="tableHead">
                            <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" Text="保存修改" 
                                OnClick="SaveButton_Click" />
                            <asp:Button ID="BtnJCGZ" runat="server" CssClass="btn_2k3" Text="进程跟踪" OnClientClick="OpenGZ()" />
                            <asp:Button ID="BtnBL" runat="server" CssClass="btn_2k3" Text="留 单" />
                            <asp:Button ID="BtnYL" runat="server" CssClass="btn_2k3" Text="遗 留" />
                            <asp:Button ID="BtnSDDFD" runat="server" CssClass="btn_2k3" Text="送调度发单" />
                            <asp:Button ID="BtnXF" runat="server" CssClass="btn_2k3" Text="故障修复" />
                            <asp:Button ID="BtnFD" runat="server" CssClass="btn_2k3" Text="发 单" />
                            <asp:Button ID="BtnFHWG" runat="server" CssClass="btn_2k3" Text="返回网管中心"/>
                            <asp:Button ID="BtnFHD" runat="server" CssClass="btn_2k3" Text="返调度" />
                            <asp:Button ID="BtnSD" runat="server" CssClass="btn_2k3" Text=" 锁 定 " OnClick="BtnSD_Click" />
                            <asp:Label ID="Label1" runat="server" Text="点击锁定后进行操作" ForeColor="Black"></asp:Label>
                            <asp:TextBox ID="per" runat="server" Style="display: none"></asp:TextBox>
                            <asp:Button ID="BtnRmss" runat="server" OnClick="BtnRmss_Click" Style="display: none" />
                            <asp:Button ID="btnSX" runat="server" Text="Button" OnClick="btnSX_Click" Style="display: none" />
                        </td>
                    </tr>
                    <%--<tr>
                        <td class="tableHead">
                            <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" Text="保存修改" OnClick="SaveButton_Click" />
                            <asp:Button ID="BtnBL" runat="server" CssClass="btn_2k3" Text="故障保留" />
                            <asp:Button ID="BtnYJ" runat="server" CssClass="btn_2k3" Text="故障移交" />
                            <asp:Button ID="BtnPY" runat="server" CssClass="btn_2k3" Text="故障评议" OnClick="BtnPY_Click" />
                            <asp:Button ID="BtnXF" runat="server" CssClass="btn_2k3" Text="故障修复" />
                            <asp:Button ID="BtnBC" runat="server" CssClass="btn_2k3" Text="故障补充" OnClientClick="OpenPC()" />
                            <asp:Button ID="BtnSD" runat="server" CssClass="btn_2k3" Text=" 锁 定 " OnClick="BtnSD_Click" />
                            <asp:Label ID="Label1" runat="server" Text="点击锁定后进行故障处理" ForeColor="Black"></asp:Label>
                            <asp:TextBox ID="ZBGUID" runat="server" Style="display: none"></asp:TextBox>
                            <asp:TextBox ID="GZYYR" runat="server" Style="display: none"></asp:TextBox>
                            <asp:TextBox ID="Per" runat="server" Style="display: none"></asp:TextBox>
                            <
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 100%">
                <div id="divedit" style="overflow: auto; height: 100%; width: 100%;">
                    <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                        width="100%">
                        <tr id="t_gztsd" style="display: none; background-image: url('../../../Images/fg700.jpg')"
                            onclick="divShow('Imagetsd','tr_gztsd')" runat="server">
                            <td width="100%" colspan="6">
                                &nbsp;&nbsp;
                                <asp:Image ID="Imagetsd" runat="server" ImageUrl="~/Web/Images/del_up.gif" ImageAlign="Middle" />
                                &nbsp;故障投诉单
                            </td>
                        </tr>
                        <tr id="tr_gztsd" style="display: block">
                            <td>
                                <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                                    bordercolor="#5b9ed1" width="100%">
                                    <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            故障编号
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="GZBH" runat="server" Text="Label"></asp:Label>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            投诉时间
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="TSSJ" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            区域
                                        </td>
                                        <td width="18%">
                                            <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                                                bordercolor="#5b9ed1" width="100%">
                                                <tr>
                                                    <td width="90%">
                                                        <asp:TextBox ID="KHQY" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <img src="../../Images/Small/bb_table.gif" onclick="OpenBranch('KHQY','KHQYID');" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:TextBox ID="KHQYID" runat="server" Width="100%" BorderStyle="None" Style="display: none"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            故障名称
                                        </td>
                                        <td width="18%" colspan="1">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="100%">
                                                        <asp:TextBox ID="GZMC" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                                                    </td>
                                                    <%-- <td id="td_addUser" style="display: none" runat="server">
                                                        <asp:Button ID="btnAddUser" runat="server" Text="..." />
                                                    </td>--%>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            业务编号
                                        </td>
                                        <td width="18%">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="width: 100%">
                                                        <asp:TextBox ID="SUBSCRIBER_ID" runat="server" BorderWidth="0" Style="display: none"
                                                            Width="100%"></asp:TextBox>
                                                        <asp:TextBox ID="YWBH" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="TQ" ImageUrl="../../Images/Small/gif-0403.gif" 
                                                            runat="server" OnClick="TQ_Click"
                                                            ToolTip="提取用户信息" Width="20px" />
                                                    </td>
                                                        <td>
                                                            <asp:ImageButton ID="SelectBOSS" runat="server" ToolTip="选择用户信息" OnClientClick="windowOpenRmssSelect()"
                                                                src="../../Images/Small/bb_table.gif" />
                                                        </td>
                                                </tr>
                                            </table>
                                            <%-- <asp:TextBox ID="YWBH" runat="server" Width="100%" BorderStyle="None" MaxLength="10"></asp:TextBox>--%>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            *行业
                                        </td>
                                        <td width="18%">
                                            <asp:DropDownList ID="HYFL" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td width="15%" class="tdBak" align="center">
                                            业务类型</td>
                                        <td width="18%" colspan="5">
                                           <asp:CheckBoxList ID="ZYZT" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem>IP业务</asp:ListItem>
                                                <asp:ListItem>IDC业务</asp:ListItem>
                                                <asp:ListItem>传输业务</asp:ListItem>
                                                <asp:ListItem>其它</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                        
                                    </tr>--%>
                                    <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            客户编号
                                        </td>
                                        <td width="18%">
                                            <asp:TextBox ID="KHBH" runat="server" Width="100%" BorderStyle="None" MaxLength="10"></asp:TextBox>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            联系人
                                        </td>
                                        <td width="18%">
                                            <asp:TextBox ID="LXRNAME" runat="server" Width="100%" BorderStyle="None" MaxLength="10"></asp:TextBox>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            *联系电话
                                        </td>
                                        <td width="18%">
                                            <asp:TextBox ID="LXDH" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            联系地址
                                        </td>
                                        <td width="18%" colspan="5">
                                            <asp:TextBox ID="KHDZ" runat="server" Width="100%" BorderStyle="None"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            故障等级
                                        </td>
                                        <td width="18%">
                                            <asp:DropDownList ID="GZDJ" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            客户等级
                                        </td>
                                        <td width="18%">
                                            <asp:DropDownList ID="CUSTOMER_LEVEL" runat="server" Width="100%">
                                                <asp:ListItem Value="1">1级</asp:ListItem>
                                                <asp:ListItem Value="2">2级</asp:ListItem>
                                                <asp:ListItem Value="3">3级</asp:ListItem>
                                                <asp:ListItem Value="4">4级</asp:ListItem>
                                                <asp:ListItem Value="5">5级</asp:ListItem>
                                                <asp:ListItem Value="6">6级</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            *故障来源
                                        </td>
                                        <td width="18%">
                                            <asp:DropDownList ID="GZLY" runat="server" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            *业务主体
                                        </td>
                                        <td width="18%">
                                            <asp:DropDownList ID="YWZT" runat="server" Width="100%" AutoPostBack="True" 
                                                OnSelectedIndexChanged="YWZT_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            *业务类别
                                        </td>
                                        <td width="18%">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="YWLB" runat="server" Width="100%" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="YWZT" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            故障状态
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="GZZT" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            创建人员
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="GZCJRNAME" runat="server"></asp:Label>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            锁定人员
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="SDRY" runat="server"></asp:Label>
                                        </td>
                                        <td colspan="2" class="tdBak">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            故障描述
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="GZMS" runat="server" Width="100%" BorderStyle="None" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            附件
                                        </td>
                                        <td colspan="5">
                                            <table cellpadding="0" cellspacing="0" style="border-collapse: collapse; width: 100%;">
                                                <%--<tr style="display:none">
                                                    <td style="border-bottom-color: #5b9ed1; border-bottom-width: 1px; border-bottom-style: solid;">
                                                        <input id="File1" type="file" runat="server" style="width: 300px;" />
                                                        <asp:Button ID="BtnUpLoad" runat="server" Text="上传" CssClass="btn_2k3" OnClick="BtnUpLoad_Click" />
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="FileGrid" runat="server" AutoGenerateColumns="False" Width="100%"
                                                            SkinID="GridView1" OnRowDataBound="FileGrid_RowDataBound" DataKeyNames="FILEGUID,FILEURL">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="序号">
                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="文件名">
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("FILENAME") %>'></asp:HyperLink>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="UPLOADNAME" HeaderText="上传人">
                                                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="FILESIZE" HeaderText="文件大小（KB）">
                                                                    <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="UPLOADTIME" DataFormatString="{0:yyyy-MM-dd HH:mm}" HeaderText="上传时间">
                                                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                                </asp:BoundField>
                                                                <%--<asp:TemplateField HeaderText="删除">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="BtnDel" runat="server" Text="删除" CssClass="btn_2k3" Width="100%"
                                                                            OnClientClick="return confirm('确定要删除吗？')" OnClick="BtnDel_Click" CommandName="del"
                                                                            CommandArgument="" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                </asp:TemplateField>--%>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="t_cllc" onclick="divShow('Imageclc','tr_cllc')" runat="server">
                            <td width="100%" colspan="6">
                                &nbsp;&nbsp;
                                <asp:Image ID="Imageclc" runat="server" ImageUrl="~/Web/Images/del_up.gif" ImageAlign="Middle" />
                                &nbsp;处理流程
                            </td>
                        </tr>
                        <tr id="tr_cllc" runat="server">
                            <td width="100%" colspan="6">
                                <asp:GridView ID="GridView_CLCC" runat="server" BorderColor="#5B9ED1" BorderWidth="1px"
                                    SkinID="GridView1" BorderStyle="Solid" CellPadding="3" CellSpacing="1" 
                                    DataKeyNames="GUID,ZBGUID" onrowdatabound="GridView_CLCC_RowDataBound">
                                    <RowStyle BorderColor="#5B9ED1" BorderWidth="1px" BorderStyle="Solid" />
                                    <Columns>
                                        <asp:BoundField HeaderText="处理时间" DataField="CLSJ">
                                            <ItemStyle Width="10%"  HorizontalAlign="Center"  />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="处理部门" DataField="CLBM">
                                            <ItemStyle Width="8%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="系统操作人员" DataField="CLRY">
                                            <ItemStyle Width="8%"  HorizontalAlign="Center"  />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="故障处理人员" DataField="SJCLRY">
                                            <ItemStyle Width="8%"  HorizontalAlign="Center"  />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="流程操作" DataField="LCCZ">
                                            <ItemStyle Width="8%"  HorizontalAlign="Center"  />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="故障状态" DataField="GZZT">
                                            <ItemStyle Width="8%"  HorizontalAlign="Center"  />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="处理说明" DataField="CLSM">
                                            <ItemStyle Width="20%" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle BorderColor="#5B9ED1" BorderStyle="None" BorderWidth="1px" />
                                    <HeaderStyle BorderColor="#5B9ED1" />
                                    <AlternatingRowStyle BorderColor="#5B9ED1" BorderWidth="1px" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="t_xfjg" onclick="divShow('Imagexfjg','tr_xfjg')" runat="server">
                            <td width="100%">
                                &nbsp;&nbsp;
                                <asp:Image ID="Imagexfjg" runat="server" ImageUrl="~/Web/Images/del_up.gif" ImageAlign="Middle" />
                                &nbsp;修复结果
                            </td>
                        </tr>
                        <tr id="tr_xfjg" runat="server">
                            <td>
                                <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                                    bordercolor="#5b9ed1" width="100%">
                                    <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            业务主体
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="XFYWZT" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            业务类型
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="XFYWLB" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            故障层次
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="GZCC" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            故障类型
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="GZLX" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            故障原因
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="GZYY" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                           处理方法
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="GZCLFF" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            故障原因描述
                                        </td>
                                        <td colspan="5">
                                            <asp:Label ID="ZJYY" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            处理方法描述
                                        </td>
                                        <td colspan="5">
                                            <asp:Label ID="GZFFMS" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                         <td width="15%" class="tdBak" align="center">
                                            结单时间
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="JDSJ" runat="server" Text=""></asp:Label>
                                            &nbsp;
                                        </td>
                                       <%-- <td width="15%" class="tdBak" align="center">
                                            拥 有 人
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="XFYYR" runat="server" Text=""></asp:Label>
                                        </td>--%>
                                        <td width="15%" class="tdBak" align="center">
                                            修复人员
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="XFRY" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            修复部门
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="XFBM" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                       
                                        <td width="15%" class="tdBak" align="center">
                                            解决方案
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="JJBF" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            总结原因
                                        </td>
                                        <td colspan="5">
                                            <asp:Label ID="ZJYY" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            补充意见
                                        </td>
                                        <td colspan="5">
                                            <asp:Label ID="BCYJ" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="ZBGUID" runat="server" Style="display: none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    <%--<asp:Button ID="fdBtn" runat="server" Text="Button" onclick="Button1_Click" style="display:none"/>--%>
    <asp:TextBox ID="fduser" runat="server" Style="display: none"></asp:TextBox>
    </form>
</body>
</html>
