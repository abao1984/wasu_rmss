<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JianKongDianZyEdit.aspx.cs" Inherits="Web_INFO_JinKongDianZyEdit" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="../../jquery-1.2.6.min.js" type="text/javascript"></script>
    <script src="InfoValidator.js" type="text/javascript"></script>
    <style type="text/css">
        html,body
        {
            text-align:center;	
        }
        div
        {
        	height:20px;
        }
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
    <script type="text/javascript">
        //验证数据
        function validat() {
            var arr = ["#JKDBH:监控点编号:str",
                        "#JKDMC:监控点名称:str",
                        "#JKDDZ:监控点地址:str",
                        "#BCSX:保持时限:str"
                      ];
            return validator(arr);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    <div style="font:宋体;font-size:12pt;font-weight:bold;text-align:center;vertical-align:middle">
        监控点资源信息
    </div>
    <table width="80%">
        <tr>
            <td align="right" width="20%">监控点编号</td>
            <td width="30%">
                <asp:TextBox ID="JKDBH" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td align="right" width="20%">监控点名称</td>
            <td>
                <asp:TextBox ID="JKDMC" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        
         <tr>
            <td align="right">监控点地址</td>
            <td>
                <asp:TextBox ID="JKDDZ" runat="server" Width="100%"></asp:TextBox>
             </td>
            <td align="right">保持时限</td>
            <td>
                <asp:TextBox ID="BCSX" runat="server" Width="100%"></asp:TextBox>
             </td>
        </tr>
        
         <tr>
            <td align="right">码流</td>
            <td>
                <asp:TextBox ID="ML" runat="server" Width="100%"></asp:TextBox>
             </td>
            <td align="right">所属设备编码</td>
            <td>
                <asp:TextBox ID="SBBH" runat="server" Width="100%"></asp:TextBox>
             </td>
        </tr>
        
         <tr>
            <td></td>
            <td align="right">
                <asp:Button ID="BtnSave" runat="server" CssClass="btn_2k3" Text="保存" 
                    onclick="BtnSave_Click" OnClientClick="return validat();" />
             </td>
            <td></td>
            <td></td>
        </tr>
    </table>
     <uc1:windowHeader ID="windowHeader1" runat="server" />
    <div style="display:none">
         <asp:TextBox ID="TxtLx" runat="server"></asp:TextBox>
         <asp:TextBox ID="GUID" runat="server"></asp:TextBox>
          <asp:Button ID="Btn" runat="server" Text="Button" OnClick="Btn_Click" />
    </div> 
    </form>
</body>
</html>
