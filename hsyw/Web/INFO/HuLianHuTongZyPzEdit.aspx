<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HuLianHuTongZyPzEdit.aspx.cs" Inherits="Web_INFO_HuLianHuTongZyPzEdit" %>

<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
        function validat() {
            var arr = ["#BH:编号:str",
                      "#MC:名称:str"];
            return validator(arr);
            
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
   
    <div style="height:20px;font:宋体;font-weight:bold;font-size:13pt;margin-top:20px">
            互联互通资源配置数据维护
    </div>
    <table width="80%">
        <tr>
            <td align="right" width="20%"><span lang="zh-cn">编号</span></td>
            <td width="23%">
                <asp:TextBox ID="BH" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td width="2%">
                <span style="color:Red">*</span>
            </td>
            <td align="right" width="20%">名称</td>
            <td width="23%">
                <asp:TextBox ID="MC" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td width="2%">&nbsp;</td>
        </tr>
        
        <tr>
            <td align="right" width="20%">本地接入设备</td>
            <td width="23%">
                <asp:TextBox ID="BDJRSB" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td width="2%">
                &nbsp;</td>
            <td align="right" width="20%">本地接入端口</td>
            <td width="23%">
                <asp:TextBox ID="BDJRDK" runat="server" Width="100%" 
                   ></asp:TextBox>
            </td>
            <td width="2%">&nbsp;</td>
        </tr>
        
        <tr>
            <td align="right" width="20%">对端单位信息</td>
            <td width="23%">
                <asp:TextBox ID="DDDWXX" runat="server" Width="100%" 
                    ></asp:TextBox>
            </td>
            <td width="2%">
                </td>
            <td align="right" width="20%">对端联系信息</td>
            <td width="23%">
                <asp:TextBox ID="DDLXXX" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td width="2%"></td>
        </tr>
       
        <tr>
            <td align="right" width="20%">中间链路商</td>
            <td width="23%">
                <asp:TextBox ID="ZJLLS" runat="server" Width="100%"></asp:TextBox>
                </td>
            <td width="2%">
                </td>
            <td align="right" width="20%">中间链路商联系信息</td>
            <td width="23%">
                <asp:TextBox ID="ZJLLSLXFS" runat="server" Width="100%"></asp:TextBox>
                </td>
            <td width="2%"></td>
        </tr>
       
        <tr>
            <td align="right" width="20%">光缆路由信息</td>
            <td colspan="4">
                <asp:TextBox ID="GLLYXX" runat="server" Width="100%" Rows="3" 
                    TextMode="MultiLine"></asp:TextBox>
                </td>
            <td width="2%"></td>
        </tr>
       
        <tr>
            <td align="right" width="20%">备注</td>
            <td colspan="4">
                <asp:TextBox ID="BZ" runat="server" Width="100%" Rows="3" 
                    TextMode="MultiLine"></asp:TextBox>
                </td>
            <td width="2%"></td>
        </tr>
       
        <tr>
            <td align="center" width="20%" colspan="6">
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