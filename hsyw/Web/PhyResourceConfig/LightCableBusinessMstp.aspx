<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LightCableBusinessMstp.aspx.cs" Inherits="Web_PhyResourceConfig_LightCableBusinessMstp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
    <table style="width:100%;height:100%">
        <tr width="1">
            <td class="tableHead">
                <table>
                    <tr>
                        <td width="60">
                            业务编码</td>
                        <td width="80">
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="Button1" runat="server" CssClass="btn_2k3" Text="保存" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr width="1">
            <td class="tableTitle" align="center" height="30">
                数字电路、MSTP业务</td>
        </tr>
        <tr>
            <td valign="top">
            <div  style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%" align="center" runat="server" >
                <table  style="width:100%;BORDER-COLLAPSE:collapse;" cellpadding="1" cellspacing="0" border="1"  bordercolor = "#5b9ed1">
                    <tr>
                        <td class="tdBak" width="14%" align="center">
                            业务编码</td>
                        <td width="19%">
                            &nbsp;</td>
                        <td class="tdBak" width="14%"  align="center">
                            客户名称</td>
                        <td width="19%">
                            &nbsp;</td>
                        <td class="tdBak" width="14%"  align="center">
                            客户编码</td>
                        <td width="19%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdBak"  align="center">
                            客户类别</td>
                        <td >
                            &nbsp;</td>
                        <td class="tdBak"   align="center">
                            客户地址</td>
                        <td colspan="3" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdBak"  align="center">
                            传真号码</td>
                        <td >
                            &nbsp;</td>
                        <td class="tdBak"   align="center">
                            邮政编码</td>
                        <td >
                            &nbsp;</td>
                        <td class="tdBak"   align="center">
                            业务联系人</td>
                        <td >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdBak"  align="center">
                            手机号码</td>
                        <td >
                            &nbsp;</td>
                        <td class="tdBak"   align="center">
                            固定电话</td>
                        <td >
                            &nbsp;</td>
                        <td class="tdBak"   align="center">
                            E-mail </td>
                        <td >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdBak"  align="center">
                            备注</td>
                        <td colspan="5" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdBak"  align="center">
                            整条链路名称</td>
                        <td colspan="3" >
                            &nbsp;</td>
                        <td class="tdBak"   align="center">
                            链路长度（KM）</td>
                        <td >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tdBak"  align="center">
                            甲端接入机房</td>
                        <td >
                            &nbsp;</td>
                        <td class="tdBak"   align="center">
                            乙端接入机房</td>
                        <td >
                            &nbsp;</td>
                        <td class="tdBak"   align="center">
                            链路耗值（DB）</td>
                        <td >
                            &nbsp;</td>
                    </tr>
                    </table>
               
    <table  style="width:100%;BORDER-COLLAPSE:collapse;" cellpadding="2" cellspacing="0" border="1"  bordercolor = "#5b9ed1">
        <tr>
            <td class="tableHead" colspan="2">
                &nbsp;</td>
            <td class="tableHead" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" class="tableTitle" colspan="2">
                甲端光缆段</td>
            <td align="center" class="tableTitle" colspan="2">
                乙端光缆段</td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" class="tableTitle" colspan="2">
                            甲端设备</td>
            <td align="center" class="tableTitle" colspan="2">
                            乙端设备</td>
        </tr>
        <tr>
                                    <td class="tdBak" style="width: 20%" align="left">
                                        上联设备名称及编号</td>
            <td style="width: 30%">
                &nbsp;</td>
                                    <td class="tdBak" style="width:20%" align="left">
                                        上联设备名称及编号</td>
            <td style="width: 30%">
                &nbsp;</td>
        </tr>
        <tr>
                                    <td class="tdBak"  align="left">
                                        上联设备端口</td>
            <td >
                &nbsp;</td>
                                    <td class="tdBak"  align="left">
                                        上联设备端口</td>
            <td >
                &nbsp;</td>
        </tr>
        <tr>
                                    <td class="tdBak"  align="left">
                                        网络间隙</td>
            <td >
                &nbsp;</td>
                                    <td class="tdBak"  align="left">
                                        网络间隙</td>
            <td >
                &nbsp;</td>
        </tr>
        <tr>
                                    <td class="tdBak"  align="left">
                                        接入设备名称及编号</td>
            <td >
                &nbsp;</td>
                                    <td class="tdBak"  align="left">
                                        接入设备名称及编号</td>
            <td >
                &nbsp;</td>
        </tr>
        <tr>
                                    <td class="tdBak"  align="left">
                                        接入设备端口</td>
            <td >
                &nbsp;</td>
                                    <td class="tdBak"  align="left">
                                        接入设备端口</td>
            <td >
                &nbsp;</td>
        </tr>
        <tr>
                                    <td class="tdBak"  align="left">
                                        端口型号</td>
            <td >
                &nbsp;</td>
                                    <td class="tdBak"  align="left">
                                        端口型号</td>
            <td >
                &nbsp;</td>
        </tr>
        <tr>
                                    <td class="tdBak"  align="left">
                                        接入设备位置</td>
            <td >
                &nbsp;</td>
                                    <td class="tdBak"  align="left">
                                        接入设备位置</td>
            <td >
                &nbsp;</td>
        </tr>
        <tr>
                                    <td class="tdBak"  align="left">
                                        光缆客户端设置</td>
            <td >
                &nbsp;</td>
                                    <td class="tdBak"  align="left">
                                        光缆客户端设置</td>
            <td >
                &nbsp;</td>
        </tr>
        <tr>
                                    <td class="tdBak"  align="left">
                                        传输设备修改记录</td>
            <td >
                &nbsp;</td>
                                    <td class="tdBak"  align="left">
                                        传输设备修改记录</td>
            <td >
                &nbsp;</td>
        </tr>
        <tr>
                                    <td class="tdBak"  align="left">
                                        备注</td>
            <td colspan="3" >
                &nbsp;</td>
        </tr>
        </table>
          </div>     
            </td>
        </tr>
        </table>
    
    </form>
    </body>
</html>
