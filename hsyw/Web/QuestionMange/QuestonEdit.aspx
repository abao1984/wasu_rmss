<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestonEdit.aspx.cs" Inherits="Web_QuestionMange_QuestonEdit" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../../jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <script src="../INFO/InfoValidator.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Validator() {
            var op = $("#TxtPo").val();
            var arr = null;
            if (op == "XJ") {
                arr = ["#WTLY:问题来源:str",
                      "#FZBM:负责部门:str",
                      "#ZXTLB:子系统类别:str",
                      "#WTYXJ:优先级:str",
                      "#WTMC:问题名称:str",
                      "#WTMS:问题描述:str"];

            }
            else if (op == "CL") {
                arr = ["#FZR:负责人:str",
                    "#WTFX:问题分析:str",
                    "#LSJJCS:临时解决措施:str",
                    "#JJBF:解决方法:str"]
            }
            else if (op == "PS") {
                arr = ["#PF:评分:int"]
            }
            else {
                arr = ["#FZR:负责人:str"]
            }
            return validator(arr);
        }
        function OpenBranch(name, code) {
            windowOpenPageByWidth("BranchTree.aspx?NAME=" + name + "&CODE=" + code, "选择负责部门", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }
        function OpenUser(realname, username) {
           
         
            
            windowOpenPageByWidth("SelectUsers.aspx?realname=" + realname + "&username=" + username + "&IsDX=1&usernames=" + document.getElementById(username).value, "选择负责人", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }
        function OpenCL() {
            var id = document.getElementById("ID").value;
            var response = Web_QuestionMange_QuestonEdit.Validator(id);
            if (response.value == "已完成") {
                alert('该问题已经解决，不能再进行处理....');
                return false;
            }
            response = Web_QuestionMange_QuestonEdit.ValidatorYj(id);
   
            if (response.value != "") {
                alert('问题已移交给 【' + response.value + '】,  【' + response.value + '】正在处理.....');
                return false;
            }
            windowOpenPage("QuestonEdit.aspx?id=" + id + "&op=CL", "问题--处理", "BtnRef");
            window.event.returnValue = false;
         
           
        }
        function OpenZJ() {
            var ztxz = $("#ZTXZ").val();
            var id = document.getElementById("ID").value;
            var response = Web_QuestionMange_QuestonEdit.Validator(id);
            if (response.value == "已完成") {
                alert('该问题已经解决，不能再进行转交....');
                return false;
            }
            response = Web_QuestionMange_QuestonEdit.ValidatorYj(id);
            if (response.value != "") {
                alert('问题已移交给 【' + response.value + '】 不能再进行转交！');
                return false;
            }
            windowOpenPageByWidth("SelectUsers.aspx?ztxz=" + encodeURIComponent(ztxz) + "&QUESID=" + id + "&IsDX=1", "转交", "BtnRef", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }

        //退回方法
        function Btn_Back() {
            if (!confirm("确认要退回吗!")) {
                return false;
            }
            var ztxz = $("#ZTXZ").val();
            var id = $("#ID").val();
            var response = Web_QuestionMange_QuestonEdit.Back(ztxz, id);
            if (response.error) {
                alert(response.error);
            }
            else {
                alert("退回成功!....");
               parent.WindowClose();
            }
            $("#windowHeader1_PropertyDiv").css("display", "none");
            return false;
        }
        
        function OpenJJJH() {
            var id = document.getElementById("ID").value;
            windowOpenPageByWidth("QuestionSolvePlan.aspx?QUESID=" + id, "问题解决计划", "", "30%", "40%", "10%", "80%");
            window.event.returnValue = false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="tableHead">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 30%;">
                            <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" Text="保存" OnClick="SaveButton_Click" OnClientClick="return Validator();" />
                            <asp:Button ID="BtnJJJH" runat="server" CssClass="btn_2k3" Text="解决计划" OnClientClick="OpenJJJH()" />
                            <asp:Button ID="BtnDel" runat="server" CssClass="btn_2k3" Text="删除" OnClick="BtnDel_Click"
                                OnClientClick="return confirm('确定要删除吗？')" />
                            <asp:Button ID="BtnCL" runat="server" CssClass="btn_2k3" Text="处理" OnClientClick="OpenCL()" />
                            <asp:Button ID="BtnZJ" runat="server" CssClass="btn_2k3" Text="转交" OnClientClick="OpenZJ()" />
                            <asp:Button ID="BtnWC" runat="server" CssClass="btn_2k3" Text="完成" OnClick="BtnWC_Click" OnClientClick="return Validator();" />
                            <span lang="zh-cn">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </span>
                            <asp:Button ID="BtnBack" runat="server" CssClass="btn_2k3" Text="退回"  OnClientClick="return Btn_Back();"/>
                            <asp:Button ID="BtnRef" runat="server" Text="Button" style="display:none" onclick="BtnRef_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="td_new" runat="server" style="display: block">
                <table style="width: 100%; border-collapse: collapse;" cellpadding="0" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td class="tdBak" style="width: 12%" align="center">
                            问题来源
                        </td>
                        <td style="width: 13%">
                            <asp:DropDownList ID="WTLY" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td class="tdBak" style="width: 12%" align="center">
                            负责部门
                        </td>
                        <td style="width: 13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="FZBM" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenBranch('FZBM','FZBM_CODE')" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdBak" style="width: 12%" align="center">
                            子系统类别
                        </td>
                        <td style="width: 13%">
                            <asp:DropDownList ID="ZXTLB" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td class="tdBak" style="width: 12%" align="center">
                            优先级
                        </td>
                        <td style="width: 13%">
                            <asp:DropDownList ID="WTYXJ" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" style="width: 12%" align="center">
                            问题名称
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="WTMC" runat="server" BorderWidth="0" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" style="width: 12%" align="center">
                            问题描述
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="WTMS" runat="server" BorderWidth="0" Width="100%" TextMode="MultiLine"
                                Rows="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" style="width: 12%" align="center">
                            影响度
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="YXD" runat="server" BorderWidth="0" Width="100%" TextMode="MultiLine"
                                Rows="3"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="td_tackle" runat="server" style="display: none">
                <table style="width: 100%; border-collapse: collapse;" cellpadding="0" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td class="tdBak" style="width: 12%" align="center">
                            问题状态
                        </td>
                        <td style="width: 13%">
                            <asp:DropDownList ID="WTZT" runat="server" Width="100%" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td class="tdBak" style="width: 12%" align="center">
                            负责人
                        </td>
                        <td style="width: 13%">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 100%">
                                        <asp:TextBox ID="FZR" runat="server" Width="100%" BorderWidth="0"></asp:TextBox>
                                    </td>
                                    <td>
                                        <img src="../Images/Small/bb_table.gif" onclick="OpenUser('FZR','FZR_UNAME')" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tdBak" colspan="4" style="width: 50%">
                        </td>
                    </tr>
<%--                    </table>
                    <table id="info" style="width: 100%; border-collapse: collapse;" cellpadding="0" cellspacing="0"
                    border="1" bordercolor="#5b9ed1" runat="server">--%>
                    <tr>
                        <td class="tdBak" style="width: 12%" align="center">
                            问题分析
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="WTFX" runat="server" BorderWidth="0" Width="100%" TextMode="MultiLine"
                                Rows="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" style="width: 12%" align="center">
                            临时解决措施
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="LSJJCS" runat="server" BorderWidth="0" Width="100%" TextMode="MultiLine"
                                Rows="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" style="width: 12%" align="center">
                            解决办法
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="JJBF" runat="server" BorderWidth="0" Width="100%" TextMode="MultiLine"
                                Rows="3"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="td_wtps" runat="server">
                <table style="width: 100%; border-collapse: collapse;" cellpadding="0" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td class="tdHead" colspan="7" align="center">
                            问题评审
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" style="width: 12%" align="center">
                            评审人
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="PSR" runat="server" Width="100%" BorderWidth="0" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="tdBak" style="width: 12%" align="center">
                            评审时间
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="PSSJ" runat="server" Width="100%" BorderWidth="0" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="tdBak" style="width: 12%" align="center">
                            评分
                        </td>
                        <td style="width: 13%">
                            <asp:TextBox ID="PF" runat="server" Width="100%" BorderWidth="0" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="tdBak" align="center" style="width: 25%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdBak" style="width: 12%" align="center">
                            评语</td>
                        <td colspan="6" style="width:88%">
                            <asp:TextBox ID="PY" runat="server" Width="100%" BorderWidth="0" 
                                ReadOnly="true" TextMode="MultiLine" Rows="4"></asp:TextBox>
                           </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="FZBM_CODE" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="FZR_UNAME" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="ID" runat="server" Style="display: none"></asp:TextBox>
      <asp:TextBox ID="TxtPo" runat="server" Style="display: none"></asp:TextBox>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    <asp:TextBox ID="ZTXZ" runat="server"  Style="display: none"></asp:TextBox>
    <asp:TextBox ID="ZPR" runat="server"  Style="display: none">
    </asp:TextBox>
   
    </form>
</body>
</html>
