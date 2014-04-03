<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DiZiTouSuEdit.aspx.cs" Inherits="Web_INFO_DiZiTouSuEdit" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../jquery-1.2.6-vsdoc.js" type="text/javascript"></script>
    <script src="InfoValidator.js" type="text/javascript"></script>
     <script src="../../calendar.js" language="javascript" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../../config.js"></script>
    <style type="text/css">
        html, body
        {
            margin: 0;
            padding: 0;
            text-align: center;
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
            
        }
        function OpenSelect(enumtype, pname, ddlid) {
            var url = "../Resource/ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
            windowOpenPageByWidth(url, "枚举维护", "Btn", "30%", "40%", "10%", "80%");
            document.getElementById("DDLID").value = ddlid;
            document.getElementById("DDLLX").value = enumtype;
        }

        function validat() {
            var arr = ["#CXNR:查询内容:str",
                      "#CXJG:查询结果:str",
                      "#YHLX:用户类型:str",
                      "#TSLX:投诉类型:str",
                       "#SLRQ:受理日期:str",
                       "#HFRQ:回访日期:str",
                       "#LJCS:累计次数:int"];
            return validator(arr);
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height:20px">
        
    </div>
    <div style="height: 20px;font:宋体;font-size:12pt;font-weight:bold;text-align:center;vertical-align:middle">
        信息维护
    </div>
    <table width="80%">
        <tr>
            <td align="right">
                <span lang="zh-cn">查询内容</span>
            </td>
            <td colspan="3">
                <asp:TextBox ID="CXNR" runat="server" Width="100%" Rows="2" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <span lang="zh-cn">查询结果</span>
            </td>
            <td colspan="3">
                <asp:TextBox ID="CXJG" runat="server"  Width="100%" Rows="2" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="20%" align="right">
                <span lang="zh-cn">用户类型</span>
            </td>
            <td width="30%">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <asp:DropDownList ID="YHLX" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <img src="../Images/Small/bb_table.gif" onclick="OpenSelect('INFOYHLX','','YHLX')" />
                        </td>
                    </tr>
               
                </table>
            </td>
            <td width="20%" align="right">
                <span lang="zh-cn">受理日期</span>
            </td>
            <td width="30%">
                <asp:TextBox ID="SLRQ" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                            Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <span lang="zh-cn">投诉类型</span>
            </td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <asp:DropDownList ID="TSLX" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <img src="../Images/Small/bb_table.gif" onclick="OpenSelect('INFOTSLX','','TSLX')" />
                        </td>
                    </tr>
                </table>
            </td>
            <td align="right">
                <span lang="zh-cn">回访日期</span>
            </td>
            <td>
                 <asp:TextBox ID="HFRQ" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                            Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <span lang="zh-cn">回访反馈信息</span>
            </td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <asp:DropDownList ID="HFFKXX" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <img onclick="OpenSelect('INFOHFFKXX','','HFFKXX')" 
                                src="../Images/Small/bb_table.gif" />
                        </td>
                    </tr>
                </table>
            </td>
            <td align="right">
                <span lang="zh-cn">整改日期</span>
            </td>
            <td>
                <asp:TextBox ID="GZRQ" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                            Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <span lang="zh-cn">整改原因</span>
            </td>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <asp:DropDownList ID="GZYY" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <img onclick="OpenSelect('INFOGZYY','','GZYY')" 
                                src="../Images/Small/bb_table.gif" />
                        </td>
                    </tr>
                </table>
            </td>
            <td align="right">
                <span lang="zh-cn">累计次数</span>
            </td>
            <td>
                <asp:TextBox ID="LJCS" runat="server"  Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <span lang="zh-cn">备注</span>
            </td>
            <td colspan="3">
                <asp:TextBox ID="BZ" runat="server"  Width="100%" Rows="4" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table width="100%">
                    <tr>
                        <td width="50%" align="center">
                            <asp:Button ID="BtnSave" runat="server" Text="保存"  CssClass="btn_2k3" 
                                onclick="BtnSave_Click" OnClientClick="return validat();" />
                        </td>
                    </tr>
                </table>
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
