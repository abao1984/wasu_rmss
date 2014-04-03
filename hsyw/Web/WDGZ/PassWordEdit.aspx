<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PassWordEdit.aspx.cs" Inherits="Web_WDGZ_PassWordEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style type="text/css">
    .btn
    {
        display:none;	
    }
    .bor
    {
        border:1px solid red;	
    }
</style>
    <script src="../../jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function() {
            $("#BtnOk").attr("disabled", "disabled");
            $("#Xmm").attr("disabled", "disabled");
            $("#QrMm").attr("disabled", "disabled");
        });
        
        function CheckYsPwd() {
            var ysmm = $("#YsMm").val();
            var username = $("#TxtUserName").val();
            
            Web_WDGZ_PassWordEdit.CheckYsPwd(ysmm, username, function(response) {
                if (response.error) {
                    alert(response.error);
                    $("#BtnOk").addClass("btn");
                }
                else {
                    if (response.value == "0") {
                        alert('原始密码不正确');
                        $("#YsMm").addClass("bor");
                        $("#BtnOk").attr("disabled", "disabled");
                        $("#Xmm").attr("disabled", "disabled");
                        $("#QrMm").attr("disabled", "disabled");
                        
                    }
                    else {
                        $("#YsMm").removeClass("bor");
                        $("#BtnOk").removeAttr("disabled");
                        $("#Xmm").removeAttr("disabled");
                        $("#QrMm").removeAttr("disabled");
                    }
                }
            })
        }

        function CheckXMm() {
            var xmm = $("#Xmm").val();
            var qrmm = $("#QrMm").val();
            if (xmm == "") {
                $("#Xmm").addClass("bor");
                return;
            }
            else if (qrmm == "") {
            $("#QrMm").addClass("bor");
            return;
            }
            if (xmm != qrmm) {
                alert('确认密码和新密码不相等');
                $("#QrMm").addClass("bor");
                $("#QrMm").val("");
                $("#BtnOk").attr("disabled", "disabled");
            }
            else {
                $("#QrMm").removeClass("bor");
                $("#BtnOk").removeAttr("disabled");
            }
        }

        //保存密码
        function BtnOk_Click() {
             var xmm = $("#Xmm").val();
            var qrmm = $("#QrMm").val();
            if (xmm == "") {
                $("#Xmm").addClass("bor");
                return;
            }
            else if (qrmm == "") {
            $("#QrMm").addClass("bor");
            return;
            }
            if (xmm != qrmm) {
                alert('确认密码和新密码不相等');
                $("#QrMm").addClass("bor");
                $("#BtnOk").attr("disabled", "disabled");
                $("#QrMm").val("");
            }
            else {
                var xmm = $("#Xmm").val();
                var username = $("#TxtUserName").val();
                Web_WDGZ_PassWordEdit.SavePwd(xmm, username, function(response) {
                    if (response.error) {
                        alert(response.error);
                    }
                    else {
                        alert('修改成功！');
                        $("#QrMm").val("");
                        $("#Xmm").val("");
                        $("#YsMm").val("");
                    }
                });
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
         <div style="height:20px;">
        
    </div>
      <div style="height:15px;font-weight:bold;font-size:11pt;text-align:center">
        修改密码
    </div>
    <table width="80%"  style="border-collapse: collapse;"cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td width="40%"  align="right" class="tdBak">原始密码</td>
                        <td>
                            <asp:TextBox ID="YsMm" runat="server"  Width="100%" TextMode="Password"  BorderWidth="1" onchange='CheckYsPwd();'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  align="right" class="tdBak">新密码</td>
                        <td>
                            <asp:TextBox ID="Xmm" runat="server"  Width="100%" TextMode="Password"  onchange="CheckXMm();"  BorderWidth="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  align="right" class="tdBak">确认密码</td>
                        <td>
                            <asp:TextBox ID="QrMm" onchange="CheckXMm();" runat="server"  Width="100%" TextMode="Password"  BorderWidth="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  align="center" colspan="2">
                            <input id="BtnOk" class="btn_2k3" onclick="BtnOk_Click();" type="button" value="确认修改" />
                        </td>
                    </tr>
                </table>
                <div style="display:none">
                 <asp:TextBox ID="TxtUserName" runat="server"></asp:TextBox>
                </div>
   
    </form>
</body>
</html>
