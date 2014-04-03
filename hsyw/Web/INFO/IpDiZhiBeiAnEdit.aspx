<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IpDiZhiBeiAnEdit.aspx.cs"
    Inherits="Web_INFO_IpDiZhiBeiAnEdit" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="../../jquery-1.2.6.min.js" type="text/javascript"></script>
    <script src="InfoValidator.js" type="text/javascript"></script>
       <script src="../../calendar.js" language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../config.js"></script>
    <style type="text/css">
        html,body
        {
        	text-align:center;
        }
        td
        {
            background:#e6f3fc;
	        background-repeat:repeat-x;
	        background-position: 0,0;
	        color: #003797;
	        font-family: "宋体";
	        font-size: 10pt;
	        /*font-weight: bold;*/
	        border:solid 1 #CDCDCD;
	        line-height:26px;	
        }
    </style>
    <script type="text/javascript">
        function OpenSelect(enumtype, pname, ddlid) {
            var url = "../Resource/ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
            windowOpenPageByWidth(url, "枚举维护", "Btn", "30%", "40%", "10%", "80%");
            document.getElementById("DDLID").value = ddlid;
            document.getElementById("DDLLX").value = enumtype;
        }
        //验证数据
        function validat() {
            var arr = ["#QSIPDZ:起始IP地址:str",
                        "#ZZIPDZ:终止IP地址:str",
                        "#SYRQ:使用日期:str",
                        "#DWMC:单位名称:str",
                        "#DWFL:单位所属分类ID:str",
                        "#DWSZS:单位所在省/自治区/直辖市ID:str",
                        "#DWDZ:单位详细地址:str",
                        "#LXRMC:联系人姓名:str",
                        "#LXRDH:联系人电话:str",
                        "#LXRDZYJ:联系人电子邮件:str",
                        "#WGIPDZ:网关IP地址:str",
                        "#DRRQ:导入日期:str",
                        "#WGSZDZ:网关所在地址:str"
                      ];
            return validator(arr);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
   
    <div style="height:20px;font:宋体;font-weight:bold;font-size:13pt;margin-top:20px">
            IP地址备案信息
    </div>
    <table width="80%">
        <tr>
            <td align="right" width="23%">起始IP地址</td>
            <td width="23%">
                <asp:TextBox ID="QSIPDZ" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td width="2%">
                <span style="color:Red">*</span>
            </td>
            <td align="right" width="20%">终止IP地址</td>
            <td width="20%">
                <asp:TextBox ID="ZZIPDZ" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td width="2%"><span style="color:Red">*</span></td>
        </tr>
        
         <tr>
            <td align="right">使用方式</td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <asp:DropDownList ID="SYFF" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <img onclick="OpenSelect('IPDZBZSYFF','','SYFF')" 
                                src="../Images/Small/bb_table.gif" />
                        </td>
                    </tr>
                </table>
             </td>
            <td></td>
            <td align="right">使用日期</td>
            <td>
                <asp:TextBox ID="SYRQ" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                            Width="100%"></asp:TextBox>
             </td>
            <td><span style="color:Red">*</span></td>
        </tr>
        
         <tr>
            <td align="right">单位名称</td>
            <td colspan="4">
                <asp:TextBox ID="DWMC" runat="server" Width="100%"></asp:TextBox>
             </td>
            <td><span style="color:Red">*</span></td>
        </tr>
        
         <tr>
            <td align="right">单位所属分类ID</td>
            <td>
                <asp:TextBox ID="DWFL" runat="server" Width="100%"></asp:TextBox>
             </td>
            <td><span style="color:Red">*</span></td>
            <td align="right">单位所属行业分类ID</td>
            <td>
                <asp:TextBox ID="DWHYFL" runat="server" Width="100%"></asp:TextBox>
             </td>
            <td></td>
        </tr>
        
         <tr>
            <td align="right">单位所在省/自治区/直辖市ID</td>
            <td colspan="4">
                <asp:TextBox ID="DWSZS" runat="server" Width="100%"></asp:TextBox>
             </td>
            <td ><span style="color:Red">*</span></td>
        </tr>
        
         <tr>
            <td align="right">单位所在市ID</td>
            <td>
                <asp:TextBox ID="DWSZSHI" runat="server" Width="100%"></asp:TextBox>
             </td>
            <td></td>
            <td align="right">单位所在县ID</td>
            <td>
                <asp:TextBox ID="DWSZX" runat="server" Width="100%"></asp:TextBox>
             </td>
            <td></td>
        </tr>
        
         <tr>
            <td align="right">单位详细地址</td>
            <td colspan="4">
                <asp:TextBox ID="DWDZ" runat="server" Width="100%"></asp:TextBox>
             </td>
            <td><span style="color:Red">*</span></td>
        </tr>
        
         <tr>
            <td align="right">联系人姓名</td>
            <td>
                <asp:TextBox ID="LXRMC" runat="server" Width="100%"></asp:TextBox>
             </td>
            <td><span style="color:Red">*</span></td>
            <td align="right">联系人电话</td>
            <td>
                <asp:TextBox ID="LXRDH" runat="server" Width="100%"></asp:TextBox>
             </td>
            <td><span style="color:Red">*</span></td>
        </tr>
        
         <tr>
            <td align="right">联系人电子邮件</td>
            <td colspan="4">
                <asp:TextBox ID="LXRDZYJ" runat="server" Width="100%" Rows="3" 
                    TextMode="MultiLine"></asp:TextBox>
             </td>
            <td><span style="color:Red">*</span></td>
        </tr>
        
        <tr>
            <td align="right">网关IP地址</td>
            <td>
                <asp:TextBox ID="WGIPDZ" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td><span style="color:Red">*</span></td>
            <td align="right">导入日期</td>
            <td>
                <asp:TextBox ID="DRRQ" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                            Width="100%"></asp:TextBox>
            </td>
            <td><span style="color:Red">*</span></td>
        </tr>
        
        <tr>
            <td align="right">网关所在地址</td>
            <td colspan="4">
                <asp:TextBox ID="WGSZDZ" runat="server" Width="100%" Rows="3" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
            <td><span style="color:Red">*</span></td>
        </tr>
        
        <tr>
            <td align="center" colspan="6">
                <asp:Button ID="BtnSave" CssClass="btn_2k3" runat="server" Text="保存" 
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
