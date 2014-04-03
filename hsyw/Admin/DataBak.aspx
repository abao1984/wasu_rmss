<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataBak.aspx.cs" Inherits="Admin_Sys_DataBase_Bak" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
</head>
<body>
    <form id="form1" runat="server">
    <div>

       <div class="tableMain">
       <div class="tableSpaceBorder">

       <table>
                <tr>
                    <td class="tableHead" align="left">
                        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="12px" 
                            Text="数据备份 "></asp:Label>
                           
                            </td>
                </tr>
                <tr>
                    <td class="tableCategory">
                    </td>
                </tr>
                
                <tr>
                <td class="tableBg1" style="text-align:left">
                    <br />
                    <div class="text">
                    <ul>
                        <li>数据备份功能根据您的需要备份全部数据。</li>
                        <li>备份的数据均不包含程序件和附件文件。程序、附件备份只能通过 FTP 等下载即可，系统本身不提供单独备份。</li>
                        <li>数据备份选项中的设置，仅供高级用户的特殊用途使用，当您尚未对数据库做全面细致的了解之前，请使用默认参数备份，否则将导致备份数据错误等严重问题。 </li>
                        <li>备份的数据只能通过SQL SERVER 的企业管理器方式来还原，还原时请在技术人员的协助下进行！</li>
                        <li>压缩备份文件可以让您的备份文件占用更小的空间。</li>
                        <li>请及时将你备份文件再次备份到异地。</li>
                    </ul>
                    </div>
                    <br />
                </td>

                </tr>
        </table>
            
    </div>
    </div>
    
    
    <br />
       <div class="tableMain">
       <div class="tableSpaceBorder">

        <table>
                <tr>
                    <td class="tableHead" colspan="2">
                        <asp:Label ID="Label2" runat="server" Text="数据库备份"></asp:Label></td>
                </tr>
            <tr>
                <td class="tableBg1" style="width: 150px; text-align:left; height:40px">
                    <div class="text">
                        <asp:Label ID="Label3" runat="server" Text="备份文件名："></asp:Label>
                    </div>
                    </td>
                <td class="tableBg2" style="text-align:left">
                    <asp:TextBox ID="TextBox1" CssClass="textBase" runat="server" Width="400px" Height="26px"></asp:TextBox>&nbsp;
                    </td>
            </tr>
            
            <tr>
                <td class="tableBg1" style="width: 150px; text-align:left; height:40px">

                    </td>
                <td class="tableBg2" style="text-align:left">
                    <asp:Button ID="Button1" CssClass="btn_2k3" runat="server" Text="备份数据" 
                    OnClick="Button1_Click" UseSubmitBehavior="False" />
                        <asp:Button ID="Button2" CssClass="btn_2k3" runat="server" Text="下载此备份文件" OnClick="Button2_Click" CausesValidation="False" UseSubmitBehavior="False" />
     </td>
            </tr>
        </table>
            
    </div>
    <br />
                <div style="text-align:left">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="注意：若非必要，请勿修改备份文件名！"></asp:Label>
                </div>
                <br />
    </div>
    
    <br />
        <br />
        <div style="text-align:center">
            <asp:Label ID="Label_BAK" runat="server" Text="Label" ForeColor="Red" Visible="False"></asp:Label>
        
        </div>
    
                   
    </div>
    </form>
</body>
</html>
