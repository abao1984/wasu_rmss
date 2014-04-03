<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JianKongSbEdit.aspx.cs" Inherits="Web_INFO_JianKongSbEdit" %>

<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
     <script src="../../jquery-1.2.6.min.js" type="text/javascript"></script>
    <script src="InfoValidator.js" type="text/javascript"></script>
     <script src="../../calendar.js" language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../config.js"></script>
    <script type="text/javascript">
        function OpenSelect(enumtype, pname, ddlid) {
            var url = "../Resource/ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
            windowOpenPageByWidth(url, "枚举维护", "Btn", "30%", "40%", "10%", "80%");
            document.getElementById("DDLID").value = ddlid;
            document.getElementById("DDLLX").value = enumtype;
        }
        //验证数据
        function validat() {
            var arr = ["#SBBH:设备编号:str",
                        "#SBMC:设备名称:str",
                        "#JRFS:接入方式/IP:str",
                        "#SBZT:设备状态:str"
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
            <td align="right" width="20%"><span lang="zh-cn">设备</span>编号</td>
            <td width="30%">
                <asp:TextBox ID="SBBH" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td align="right" width="20%"><span lang="zh-cn">设备</span>名称</td>
            <td>
                <asp:TextBox ID="SBMC" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        
         <tr>
            <td align="right"><span lang="zh-cn">接入方式/IP</span></td>
            <td>
                <asp:TextBox ID="JRFS" runat="server" Width="100%"></asp:TextBox>
             </td>
            <td align="right"><span lang="zh-cn">设备状态</span></td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <asp:DropDownList ID="SBZT" runat="server" Height="16px" Width="97%">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <img onclick="OpenSelect('JKSBSBZT','','SBZT')" 
                                src="../Images/Small/bb_table.gif" />
                        </td>
                    </tr>
                </table>
             </td>
        </tr>
        
         <tr>
            <td align="right"><span lang="zh-cn">监控设备地址</span></td>
            <td>
                <asp:TextBox ID="SBDZ" runat="server" Width="100%"></asp:TextBox>
             </td>
            <td align="right">&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        
         <tr>
            <td align="center" colspan="4">
                <asp:Button ID="BtnSave" runat="server" CssClass="btn_2k3" Text="保存" 
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
