<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JianKongYeWuPzEdit.aspx.cs" Inherits="Web_INFO_JianKongYeWuPzEdit" %>

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
            var arr = ["#YWBH:业务编号:str",
                        "#KHBH:客户编号:str",
                        "#KHMC:客户名称:str",
                        "#KTSJ:开通时间:str",
                        "#LXR:联系人:str",
                        "#DH:电话:str",
                        "#KHDZ:客户地址:str"
                      ];
            return validator(arr);
        }
        function Close() {

            window.returnValue = "refresh";
           
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
            <td align="right" width="20%"><span lang="zh-cn">业务</span>编号</td>
            <td width="30%">
                <asp:TextBox ID="YWBH" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td align="right" width="20%"><span lang="zh-cn">客户编号</span></td>
            <td>
                <asp:TextBox ID="KHBH" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        
         <tr>
            <td align="right"><span lang="zh-cn">客户名称</span></td>
            <td>
                <asp:TextBox ID="KHMC" runat="server" Width="100%"></asp:TextBox>
             </td>
            <td align="right"><span lang="zh-cn">开通时间</span></td>
            <td>
                <asp:TextBox ID="KTSJ" runat="server" onfocus="setDay(this);" BorderStyle="None"
                                            Width="100%"></asp:TextBox>
             </td>
        </tr>
        
         <tr>
            <td align="right"><span lang="zh-cn">联系人</span></td>
            <td>
                <asp:TextBox ID="LXR" runat="server" Width="100%"></asp:TextBox>
             </td>
            <td align="right"><span lang="zh-cn">电话</span></td>
            <td>
                <asp:TextBox ID="DH" runat="server" Width="100%"></asp:TextBox>
             </td>
        </tr>
        <tr>
            <td align="right">客户地址</td>
            <td align="right" colspan="3">
                <asp:TextBox ID="KHDZ" runat="server" Width="100%" Rows="3" 
                    TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
         <tr>
            <td align="right"><span lang="zh-cn">备注</span></td>
            <td align="right" colspan="3">
                <asp:TextBox ID="BZ" runat="server" Width="100%" Rows="3" TextMode="MultiLine"></asp:TextBox>
                </td>
        </tr>
         <tr>
            <td align="center" colspan="4">
                <asp:Button ID="BtnSave" runat="server" CssClass="btn_2k3" Text="保存" 
                    onclick="BtnSave_Click" OnClientClick="return validat();" />
                <%--<input id="Button1" type="button" value="关闭" class="btn_2k3" onclick="Close()" />--%></td>
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