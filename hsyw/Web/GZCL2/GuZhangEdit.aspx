<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuZhangEdit.aspx.cs" Inherits="Web_GZCL_GuZhangEdit" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html>
<head runat="server">
    <title></title>
 <script language="javascript" type="text/javascript" src="../Resource/ResourceScript.js"></script>
    
    <script language="javascript" type="text/javascript" src="../../calendar.js"></script>

    <script language="javascript" type="text/javascript">

        function windowOpenEqu(txt_fl, txt_name, p_txt_name, linkage_code) {
            var unit_id = document.getElementById(txt_fl).value;
            windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, '1')
        }
        
        function divShow(obj1, obj2) {//obj1图片对象，obj2 tr的ID
            var obj = document.getElementById(obj2);
            var objImage = document.getElementById(obj1);
            if (obj.style.display == "none") {
                objImage.src = "../Images/del_up.gif";
                obj.style.display = "block";
            }
            else {
                objImage.src = "../Images/add_up.gif";
                obj.style.display = "none";
            }
        }

        function OpenCL(type) {
            var ZBGUID = document.getElementById("ZBGUID").value;
            var per = document.getElementById("per").value;
            var str = window.showModalDialog("GuZhangChuLi.aspx?ZBGUID=" + ZBGUID + "&Type=" + type + "&per=" + per, "", "dialogWidth:400px;dialogHeight:300px;center:yes;location:no;status:no;");
            window.event.returnValue = false;
            if (str == "true") {
                parent.WindowClose();
            }
        }

        function OpenXF() {
            var ZBGUID = document.getElementById("ZBGUID").value;
            var str = window.showModalDialog("GuZhangXiuFu.aspx?ZBGUID=" + ZBGUID, "", "dialogWidth:400px;dialogHeight:420px;center:yes;location:no;status:no;");
            window.event.returnValue = false;
            if (str == "true") {
                parent.WindowClose();
            }
        }

        function OpenPC() {
            var ZBGUID = document.getElementById("ZBGUID").value;
            var str = window.showModalDialog("PuChongEdit.aspx?ZBGUID=" + ZBGUID, "", "dialogWidth:400px;dialogHeight:420px;center:yes;location:no;status:no;");
            window.event.returnValue = false;
            document.getElementById("btnSX").click();
        }

        function OpenBranch(name, code) {
            windowOpenPageByWidth("../Resource/BranchTree.aspx?NAME=" + name + "&CODE=" + code + "&ISQY=1", "选择所属区域", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }

        function windowOpenRmssTQ() {
            var url = "../Resource/RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID&SUBSCRIBER_CODE=" + document.getElementById("YWBH").value;
            windowOpenPage(url, "选择客户资源", "BtnRmss");
            // event.returnValue = false;
        }

        function windowOpenRmssSelect() {
            var url = "../Resource/RmssSelect.aspx?YWBM_NAME=SUBSCRIBER_ID";
            windowOpenPage(url, "选择客户资源", "BtnRmss");
            window.event.returnValue = false;
        }

        function OpenGZ() {
            var guid = document.getElementById("zbguid").value;
            var url = "JinChengGengZhong.aspx?id=" + guid;
            windowOpenPageByWidth(url, "进程跟踪", "btnSX", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }

        function OpenPrintSelect() {
            var url = "PrintSelect.aspx?ZBGUID="+document.getElementById("ZBGUID").value;
            var str = window.showModalDialog(url, '', 'dialogHeight:300px; dialogWidth: 500px;center: yes; help: no;resizable: no; status:no;scroll:no;');
            if (typeof (str) != "undefined") {
                document.getElementById("SELECT_CH").value = str;
            }
            else {
                window.event.returnValue = false;
            }
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
                            <asp:Button ID="BtnEXCEL" runat="server" CssClass="btn_2k3" Text="导出EXCEL" Width="88px"
                                OnClick="BtnEXCEL_Click" Style="display: none" />
                            <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" Text="保存修改" OnClick="SaveButton_Click" />
                            <%--<asp:Button ID="BtnJCGZ" runat="server" CssClass="btn_2k3" Text="进程跟踪" OnClientClick="OpenGZ()" />--%>
                            <asp:Button ID="BtnBL" runat="server" CssClass="btn_2k3" Text="故障保留" Visible="False" />
                            <asp:Button ID="BtnYJ" runat="server" CssClass="btn_2k3" Text="故障移交" Visible="False" />
                            <asp:Button ID="BtnPY" runat="server" CssClass="btn_2k3" Text="故障评议" OnClick="BtnPY_Click" Visible="False" />
                            <asp:Button ID="BtnXF" runat="server" CssClass="btn_2k3" Text="故障修复" />
                            <asp:Button ID="BtnBC" runat="server" CssClass="btn_2k3" Text="故障补充" OnClientClick="OpenPC()" Visible="False" />
                            <asp:Button ID="BtnSD" runat="server" CssClass="btn_2k3" Text=" 锁 定 " OnClick="BtnSD_Click" />
                            <asp:Label ID="Label1" runat="server" Text="点击锁定后进行故障处理" ForeColor="Black"></asp:Label>
                            <asp:TextBox ID="ZBGUID" runat="server" Style="display: none"></asp:TextBox>
                            <asp:TextBox ID="GZYYR" runat="server" Style="display: none"></asp:TextBox>
                            <asp:TextBox ID="Per" runat="server" Style="display: none"></asp:TextBox>
                            <asp:Button ID="btnSX" runat="server" Text="Button" OnClick="btnSX_Click" Style="display: none" />
                            <asp:Button ID="BtnRmss" runat="server" OnClick="BtnRmss_Click" Style="display: none" />
                        </td>
                    </tr>
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
                            <td width="100%" colspan="6" class="tableTitle">
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
                                                        <img src="../Images/Small/bb_table.gif" onclick="OpenBranch('KHQY','KHQYID');" />
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
                                                        <asp:ImageButton ID="TQ" ImageUrl="../Images/Small/gif-0403.gif" runat="server" OnClick="TQ_Click"
                                                            ToolTip="提取用户信息" />
                                                    </td>
                                                    <td>
                                                        <td>
                                                            <asp:ImageButton ID="SelectBOSS" runat="server" ToolTip="选择用户信息" OnClientClick="windowOpenRmssSelect()"
                                                                src="../Images/Small/bb_table.gif" />
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
                                                onselectedindexchanged="YWZT_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            *业务类别
                                        </td>
                                        <td width="18%">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="YWLB" runat="server" Width="100%" 
                                                        AutoPostBack="True">
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
                                            主送人员/部门
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtZS" runat="server" Width="100%" BorderStyle="None"  ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            抄送人员/部门
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtCS" runat="server" Width="100%" BorderStyle="None"  ReadOnly="true"></asp:TextBox>
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
                                                <tr>
                                                    <td style="border-bottom-color: #5b9ed1; border-bottom-width: 1px; border-bottom-style: solid;">
                                                        <input id="File1" type="file" runat="server" style="width: 300px;" />
                                                        <asp:Button ID="BtnUpLoad" runat="server" Text="上传" CssClass="btn_2k3" OnClick="BtnUpLoad_Click" />
                                                    </td>
                                                </tr>
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
                                                                <asp:TemplateField HeaderText="删除">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="BtnDel" runat="server" Text="删除" CssClass="btn_2k3" Width="100%"
                                                                            OnClientClick="return confirm('确定要删除吗？')" OnClick="BtnDel_Click" CommandName="del"
                                                                            CommandArgument="" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                </asp:TemplateField>
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
                        <tr id="t_cllc" style="display: none" onclick="divShow('Imageclc','tr_cllc')" runat="server">
                            <td width="100%" colspan="6" class="tableTitle ">
                                &nbsp;&nbsp;
                                <asp:Image ID="Imageclc" runat="server" ImageUrl="~/Web/Images/del_up.gif" ImageAlign="Middle" />
                                &nbsp;处理流程
                            </td>
                        </tr>
                        <tr id="tr_cllc" style="display: none" runat="server">
                            <td width="100%" colspan="6">
                                <asp:GridView ID="GridView_CLCC" runat="server" BorderColor="#5B9ED1" BorderWidth="1px"
                                    SkinID="GridView1" BorderStyle="Solid" CellPadding="3" CellSpacing="1" 
                                    DataKeyNames="GUID,ZBGUID" onrowdatabound="GridView_CLCC_RowDataBound">
                                    <RowStyle BorderColor="#5B9ED1" BorderWidth="1px" BorderStyle="Solid" />
                                    <Columns>
                                        <asp:BoundField HeaderText="处理时间" DataField="CLSJ">
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="处理部门" DataField="CLBM">
                                            <ItemStyle Width="8%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="处理人员" DataField="CLRY">
                                            <ItemStyle Width="8%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="流程操作" DataField="LCCZ">
                                            <ItemStyle Width="8%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="故障状态" DataField="GZZT">
                                            <ItemStyle Width="8%" HorizontalAlign="Center" />
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
                        <%--<tr id="t_gzztxx" style="display: none" onclick="divShow('Imageztxx','tr_gzztxx')"
                            runat="server">
                            <td width="100%">
                                &nbsp;&nbsp;
                                <asp:Image ID="Imageztxx" runat="server" ImageUrl="~/Web/Images/del_up.gif" ImageAlign="Middle" />
                                &nbsp;故障状态更改信息
                            </td>
                        </tr>
                        <tr id="tr_gzztxx" style="display: none" runat="server">
                            <td width="100%">
                                <asp:GridView ID="GridView_GZBG" runat="server" BorderColor="#5B9ED1" BorderWidth="1px"
                                    SkinID="GridView1" BorderStyle="Solid" CellPadding="3" CellSpacing="1" DataKeyNames="GUID,ZBGUID">
                                    <RowStyle BorderColor="#5B9ED1" BorderWidth="1px" BorderStyle="Solid" />
                                    <Columns>
                                        <asp:BoundField HeaderText="变更时间" DataField="CLSJ"></asp:BoundField>
                                        <asp:BoundField HeaderText="来源部门" DataField="LYBM"></asp:BoundField>
                                        <asp:BoundField HeaderText="变更人" DataField="CLR"></asp:BoundField>
                                        <asp:BoundField HeaderText="状态" DataField="GBZT"></asp:BoundField>
                                    </Columns>
                                    <PagerStyle BorderColor="#5B9ED1" BorderStyle="None" BorderWidth="1px" />
                                    <HeaderStyle BorderColor="#5B9ED1" />
                                    <AlternatingRowStyle BorderColor="#5B9ED1" BorderWidth="1px" />
                                </asp:GridView>
                            </td>
                        </tr>--%>
                        <tr id="t_xfjg" style="display: none" onclick="divShow('Imagexfjg','tr_xfjg')" runat="server">
                            <td width="100%" class="tableTitle ">
                                &nbsp;&nbsp;
                                <asp:Image ID="Imagexfjg" runat="server" ImageUrl="~/Web/Images/del_up.gif" ImageAlign="Middle" />
                                &nbsp;修复结果
                            </td>
                        </tr>
                        <tr id="tr_xfjg" runat="server" style="display: none">
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
                        <tr id="t_gzpy" style="display: none" onclick="divShow('Imagegzpy','tr_gzpy')" runat="server">
                            <td width="100%" class="tableTitle ">
                                &nbsp;&nbsp;
                                <asp:Image ID="Imagegzpy" runat="server" ImageUrl="~/Web/Images/del_up.gif" ImageAlign="Middle" />
                                &nbsp;故障评议
                            </td>
                        </tr>
                        <tr id="tr_gzpy" style="display: none" runat="server">
                            <td>
                                <table cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                                    bordercolor="#5b9ed1" width="100%">
                                    <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            评议人员
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="PYRY" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            评议时间
                                        </td>
                                        <td width="18%">
                                            <asp:Label ID="PYSJ" runat="server" Text=""></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td width="15%" class="tdBak" align="center">
                                            故障评分
                                        </td>
                                        <td width="18%">
                                            <asp:TextBox ID="GZPF" runat="server" BorderStyle="None" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="15%" class="tdBak" align="center">
                                            故障评语
                                        </td>
                                        <td colspan="5">
                                            <asp:TextBox ID="GZPY" runat="server" Width="100%" BorderStyle="None" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        
                        <tr id="T_yxyh"  onclick="divShow('ImageYxyh','tr_yxyh')" runat="server">
                            <td width="100%" class="tableTitle ">
                                &nbsp;&nbsp;
                                <asp:Image ID="ImageYxyh" runat="server" ImageUrl="~/Web/Images/del_up.gif" ImageAlign="Middle" />
                                &nbsp;影响分析
                            </td>
                        </tr>
                         <tr id="tr_yxyh"  runat="server">
                            <td>
                                <table  cellpadding="0" cellspacing="0" border="1" style="border-collapse: collapse;"
                                    bordercolor="#5b9ed1" width="100%">
                                    <tr>
                                        <td  class="tdBak" width="15%" align="center">
                                            影响机房&nbsp;
                                        </td>
                                        <td  width="35%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="JF" runat="server" Width="100%" BorderWidth="0" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','JF','','','1','')" />
                                    </td>
                                </tr>
                            </table>
                                        </td>
                                        <td  class="tdBak" width="15%" align="center">
                                            &nbsp;影响设备
                                        </td>
                                        <td  width="35%">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="SB" runat="server" BorderWidth="0" Width="100%" 
                                            BackColor="#F0F0F0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="windowOpenEqu('SBLX','SB','JF','HOUSE_NAME','1')" />
                                    </td>
                                </tr>
                            </table></td>
                                        <td  width="5%">
                                            <asp:Button ID="gjfxButton" runat="server" CssClass="btn_2k3" 
                                                Text="确定分析" onclick="gjfxButton_Click" /></td> 
                                                <td   width="5%">
                                            <asp:Button ID="ExpButton" runat="server" Text="导出" CssClass="btn_2k3" 
                                                        onclick="ExpButton_Click" onclientclick=" OpenPrintSelect() " />
                                        </td>
                                    </tr>
                                    </table>
                                <asp:GridView ID="GridViewYxyh" runat="server" BorderColor="#5B9ED1" BorderWidth="1px"
                                    SkinID="GridView1" BorderStyle="Solid" CellPadding="3" CellSpacing="1" 
                                    onrowdatabound="GridViewYxyh_RowDataBound">
                                    <RowStyle BorderColor="#5B9ED1" BorderWidth="1px" BorderStyle="Solid" />
                                    <Columns>
                                        <asp:BoundField HeaderText="序号">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="链路" DataField="LLMC">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="用户编码" DataField="YWBM">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="用户名称" DataField="YWMC">
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle BorderColor="#5B9ED1" BorderStyle="None" BorderWidth="1px" />
                                    <HeaderStyle BorderColor="#5B9ED1" />
                                    <AlternatingRowStyle BorderColor="#5B9ED1" BorderWidth="1px" />
                                </asp:GridView>
                            </td>
                         </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    <asp:TextBox ID="GZYDRID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="SBLX" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JF_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="JF_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="SB_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="SB_CODE" runat="server" Style="display: none"></asp:TextBox>
     <asp:TextBox ID="SELECT_CH" runat="server" Style="display: none"></asp:TextBox>
    </form>
</body>
</html>
