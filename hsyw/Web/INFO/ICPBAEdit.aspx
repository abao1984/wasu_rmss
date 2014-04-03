<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ICPBAEdit.aspx.cs" Inherits="Web_INFO_ICPBAEdit" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <style type="text/css">
        td
        {
            background:#e6f3fc;
	        background-repeat:repeat-x;
	        background-position: 0,0;
	        color: #003797;
	        font-family: "宋体";
	        font-size: 12px;
	        border:1px solid #CDCDCD;
	        /*font-weight: bold;*/
	        line-height:26px;	
        }
    </style>
     <script src="../../jquery-1.2.6.min.js" type="text/javascript"></script>
    <script src="InfoValidator.js" type="text/javascript"></script>
    <script src="../../calendar.js" language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../config.js"></script>
    <script type="text/javascript">
        function windowOpenEqu(txt_fl, txt_name, p_txt_name, linkage_code) {
            var unit_id = document.getElementById(txt_fl).value;
            windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, '1')
        }
        function MinWindow(str) {
            document.getElementById(str + "TR").style.display = "none";
            document.getElementById(str + "Div").style.height = "30px";
            document.getElementById(str + "Div").style.top = document.body.offsetHeight - 30;
        }
        function MaxWinodw(str) {
            document.getElementById(str + "TR").style.display = "block";
            document.getElementById(str + "Div").style.height = "100%";
            document.getElementById(str + "Div").style.top = "0px";
        }
        function WindowClose(str) {
            document.getElementById(str + "Div").style.display = "none";
            //document.getElementById("Btn").click();
        }
        function OpenSelect(enumtype, pname, ddlid) {
            var url = "../Resource/ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
            windowOpenPageByWidth(url, "枚举维护", "Btn", "30%", "40%", "10%", "80%");
            document.getElementById("DDLID").value = ddlid;
            document.getElementById("DDLLX").value = enumtype;
        }
        //验证数据
        function validat() {
            var arr = ["#ZBDW:主办单位名称:str",
                      "#ZBDWXZ:主办单位性质ID:str",
                      "#ZBDWZJ:主办单位证件号码:str",
                      "#ZBDWSZS:主办单位所在省ID",
                      "#SJZGDW:投资者或上级主管单位:str",
                      "#ZBDWDZ:主办单位通信地址:str",
                      "#WZFZRMC:网站负责人名称:str",
                      "#WZFZRDZYJ:网站负责人电子邮件:str",
                      "#WZFZRZJ:网站负责人有效证件号:str",
                      "#WZFZRDH:网站负责人电话:str",
                      "#WZFZRSJ:网站负责人手机:str",
                      "#WZMC:网站名称:str",
                      "#WZSYDZ:网站首页地址:str",
                      "#WZJRDW:网站接入服务提供单位ID:str",
                      "#WZJRFS:网站接入方式ID:str",
                      "#FYQFZDD:服务器放置地点ID:str",
                      "#SPNR:需前置审批或专项审批内容ID:str",
                      "#DRRQ:导入日期:str"
                      ];
            return validator(arr);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%;top:20px;font:宋体;font-weight:bold;font-size:13pt;height:20pt;">
        ICP备案信息
    </div>
    <table width="90%">
        <tr>
            <td align="right"><span lang="zh-cn">主办单位名称</span></td>
            <td colspan="4">
                <asp:TextBox ID="ZBDW" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
        </tr>
        
          <tr>
            <td align="right"><span lang="zh-cn">主办单位性质ID</span></td>
            <td colspan="4">
                <asp:TextBox ID="ZBDWXZ" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
        </tr>
        
          <tr>
            <td  align="right"><span lang="zh-cn" >主办单位证件号码</span></td>
            <td colspan="4">
                <asp:TextBox ID="ZBDWZJ" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
        </tr>
        
          <tr>
            <td width="20%"  align="right"><span lang="zh-cn">主办单位所在省ID</span></td>
            <td width="30%">
                <asp:TextBox ID="ZBDWSZS" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td width="2%"><span lang="zh-cn" style="color:Red">*</span></td>
            <td width="18%"  align="right"><span lang="zh-cn">主办单位所在市ID</span></td>
            <td width="28%">
                <asp:TextBox ID="ZBDWSZSHI" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td width="2%"></td>
        </tr>
        
          <tr>
            <td  align="right"><span lang="zh-cn">主办单位所在县ID</span></td>
            <td colspan="4">
                <asp:TextBox ID="ZBDWSZX" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td></td>
        </tr>
        
          <tr>
            <td  align="right">投资者或上级主管单位</td>
            <td colspan="4">
                <asp:TextBox ID="SJZGDW" runat="server" Rows="5" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
              </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
        </tr>
        
          <tr>
            <td  align="right">主办单位通信地址</td>
            <td colspan="4">
                <asp:TextBox ID="ZBDWDZ" runat="server" Rows="5" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
              </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
        </tr>
        
          <tr>
            <td   align="right">网站负责人名称</td>
            <td colspan="4">
                <asp:TextBox ID="WZFZRMC" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
        </tr>
        
          <tr>
            <td  align="right">网站负责人电子邮件</td>
            <td colspan="4">
                <asp:TextBox ID="WZFZRDZYJ" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
        </tr>
        
          <tr>
            <td  align="right">网站负责人有效证件号码</td>
            <td colspan="4">
                <asp:TextBox ID="WZFZRZJ" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
        </tr>
        
          <tr>
            <td  align="right">网站负责人电话号码</td>
            <td colspan="4">
                <asp:TextBox ID="WZFZRDH" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
        </tr>
        
          <tr>
            <td  align="right">网站负责人手机号码</td>
            <td colspan="4">
                <asp:TextBox ID="WZFZRSJ" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
        </tr>
        
          <tr>
            <td  align="right">网站名称</td>
            <td colspan="4">
                <asp:TextBox ID="WZMC" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
        </tr>
        
          <tr>
            <td  align="right">网站首页网址</td>
            <td colspan="4">
                <asp:TextBox ID="WZSYDZ" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
        </tr>
        
          <tr>
            <td  align="right">网站接入服务提供单位ID</td>
            <td>
                <asp:TextBox ID="WZJRDW" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
            <td  align="right">网站接入方式ID</td>
            <td>
                <asp:TextBox ID="WZJRFS" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
        </tr>
        
          <tr>
            <td  align="right">服务器放置地点ID</td>
            <td>
                <asp:TextBox ID="FYQFZDD" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
            <td  align="right">是否动态IP</td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <asp:DropDownList ID="SFDTIP" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <img onclick="OpenSelect('ICPBASFDTIP','','SFDTIP')" 
                                src="../Images/Small/bb_table.gif" />
                        </td>
                    </tr>
                </table>
              </td>
            <td></td>
        </tr>
        
          <tr>
            <td  align="right">网站域名列表</td>
            <td colspan="4">
                <asp:TextBox ID="WZYMLB" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td></td>
        </tr>
        
          <tr>
            <td  align="right">IP地址列表</td>
            <td colspan="4">
                <asp:TextBox ID="IPDZLB" runat="server" Width="100%"></asp:TextBox>
              </td>
            <td></td>
        </tr>
        
          <tr>
            <td  align="right">需前置审批或专项审批内容ID</td>
            <td colspan="4">
                <asp:TextBox ID="SPNR" runat="server" Rows="2" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
              </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
        </tr>
        
          <tr>
            <td  align="right">审批结果</td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <asp:DropDownList ID="SPJG" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <img onclick="OpenSelect('ICPBASPJG','','SPJG')" 
                                src="../Images/Small/bb_table.gif" />
                        </td>
                    </tr>
                </table>
              </td>
            <td></td>
            <td  align="right">导入日期</td>
            <td>
                <asp:TextBox ID="DRRQ" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                            Width="100%"></asp:TextBox>
              </td>
            <td><span lang="zh-cn" style="color:Red">*</span></td>
        </tr>
        
          <tr>
            <td  align="right">备注</td>
            <td colspan="4">
                <asp:TextBox ID="BZ" runat="server" Rows="4" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
              </td>
            <td></td>
        </tr>
        
          <tr>
            <td align="center" colspan="6">
                <asp:Button ID="BtnSave" runat="server" Text="保存" CssClass="btn_2k3" 
                    onclick="BtnSave_Click" OnClientClick="return validat();" />
              </td>
        </tr>    
    </table>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    <div style="display:none">
         <asp:TextBox ID="DDLID" runat="server"></asp:TextBox>
         <asp:TextBox ID="DDLLX" runat="server"></asp:TextBox>
         <asp:TextBox ID="TxtLx" runat="server"></asp:TextBox>
         <asp:TextBox ID="GUID" runat="server"></asp:TextBox>
          <asp:Button ID="Btn" runat="server" Text="Button" OnClick="Btn_Click" />
    </div> 
    </form>
</body>
</html>
