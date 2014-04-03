<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogicResourceIpSelect1.aspx.cs" Inherits="Web_Resource_LogicResourceIpSelect1" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script language="javascript" type="text/javascript" src="ResourceScript.js"></script>
     <script language="javascript" type="text/javascript">
         function okSel() {
             if (confirm('该IP已分配，是否继续分配?')) {
                 document.getElementById("btnOK").click();
             }
         }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <table  style="width: 100%; HEIGHT: 100%; border-collapse: collapse;" cellpadding="2" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
        <tr>
            <td align="center" class="tableTitle" colspan="2" style="height:1px">
                <asp:Label ID="LabelTitle1" runat="server">可选IP资源</asp:Label>
            </td>
        </tr>
        <tr>
            <td width="50%" valign="top">
                <table  style="width: 100%; HEIGHT: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td style="height:1px" >
                              <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td align="center" width="20%" class="tdBak">
                            业务大类</td>
                        <td width="30%">
                            <asp:DropDownList ID="YWDL" runat="server" Width="100%" AutoPostBack="True" 
                                onselectedindexchanged="YWDL_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="center" width="20%" class="tdBak">
                            业务类型</td>
                        <td width="230%">
                            <asp:DropDownList ID="IPYWLX" runat="server" Width="100%">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="20%" class="tdBak">
                            所属区域</td>
                        <td width="30%">
                            <table style="width:100%;">
                                <tr>
                                    <td  style="width:100%;">
                                        <asp:TextBox ID="SSQY" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="1">
                                       <img align='right' src='../Images/Small/bb_table.gif' onclick="windowOpenBranchTree('SSQY','SSQY_CODE')" /></td>
                                </tr>
                            </table>
                        </td>
                        <td align="center" width="20%" class="tdBak">
                                                        所属机房</td>
                        <td width="230%">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="HOUSE_NAME" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="1">
                                        <img src="../Images/Small/bb_table.gif"  onclick="windowOpenPhyResourceSelect('d86fbb8d-87c4-44f8-abfd-8ca14744299d','HOUSE_NAME','SSQY','HOUSE_AREA','1')"/></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="20%" class="tdBak">
                            IP地址段</td>
                        <td width="30%">
                                        <asp:TextBox ID="IPDZD" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                        <td align="center" width="20%" class="tdBak">
                                                        是否全网</td>
                        <td width="230%">
                            <asp:CheckBox ID="SFQW" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="20%" class="tdBak">
                            IP分类</td>
                        <td width="30%">
                                        <asp:DropDownList ID="DZFL" runat="server" Width="100%" >
                                        </asp:DropDownList>
                                    </td>
                        <td align="center" width="20%" class="tdBak" colspan="2">
                            <asp:Button ID="QueryButton" runat="server" CssClass="btn_2k3" Text="查询" 
                                onclick="QueryButton_Click" />
                        </td>
                    </tr>
                    </table></td>
                    </tr>
                    <tr>
                        <td>
                <div id="LogicEquIPDIV" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%" align="center" runat="server" >
                <asp:GridView ID="LogicEquIpGrid" runat="server" SkinID="GridView1" 
                    BorderWidth="1px" AllowPaging="True" AllowSorting="True" CellSpacing="1" 
                        AutoGenerateColumns="False" DataKeyNames="GUID" 
                        onselectedindexchanging="LogicEquIpGrid_SelectedIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="IP" HeaderText="IP" />
                        <asp:BoundField DataField="YTMS" HeaderText="用途描述" />
                        <asp:ImageField DataImageUrlField="IPZT_URL" HeaderText="分配状态" DataAlternateTextField="ipfpzt">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ImageField>
                        <asp:CommandField ShowSelectButton="True" HeaderText="选择" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
                </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 1px">
                            <table class="tdBak" id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td width="100%" style="padding-left: 6px;">
                           
                            <font face="宋体">总共有<asp:Label ID="DataCountLab" runat="server" ForeColor="Red">1</asp:Label>条记录，当前第
                                    <asp:Label ID="PageIndexLab" runat="server" ForeColor="Red">1</asp:Label>页，共
                                    <asp:Label ID="PageCountLab" runat="server" ForeColor="Red">1</asp:Label>页</font>
                        </td>
                        <td width="1">
                            <asp:Label ID="Label1" runat="server" Width="55px">单页显示</asp:Label>
                        </td>
                        <td width="1">
                            <font face="宋体">
                                <asp:DropDownList ID="PageSize" runat="server" ForeColor="Red" Font-Bold="True" AutoPostBack="True"
                                    Width="60px" OnSelectedIndexChanged="PageSize_SelectedIndexChanged">
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="500">500</asp:ListItem>
                                </asp:DropDownList>
                            </font>
                        </td>
                        <td width="1">
                            <asp:LinkButton ID="PrevButton" runat="server" ForeColor="#003797" Width="50px" OnClick="PrevButton_Click">上一页</asp:LinkButton>
                        </td>
                        <td width="1">
                            <asp:DropDownList ID="GridPageList" runat="server" ForeColor="Red" Font-Bold="True"
                                AutoPostBack="True" Width="50px" OnSelectedIndexChanged="GridPageList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="1">
                            <asp:LinkButton ID="NextButton" runat="server" ForeColor="#003797" Width="50px" OnClick="NextButton_Click">下一页</asp:LinkButton>
                        </td>
                    </tr>
                </table> 
                        </td>
                    </tr>
                </table>
            </td>
            <td width="50%" valign="top">
                <table  style="width: 100%; HEIGHT: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td style="height: 1px" align="center" class="tableTitle">
                            可分配IP资源</td>
                    </tr>
                    <tr>
                        <td style="height: 1px">
                <table  style="width:100%; height: 100%;"  border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td>
                            <table style="width: 100%; height: 100%;" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="10%">
                                        <asp:TextBox ID="IP1" runat="server" Width="100%" onKeyPress="return limitNum(this);"
                                            BorderStyle="None"></asp:TextBox>
                                    </td>
                                    <td width="2%">
                                        .
                                    </td>
                                    <td width="20%">
                                        <asp:DropDownList ID="IP2" runat="server" AutoPostBack="True"
                                            Width="100%" onselectedindexchanged="IP2_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="2%">
                                        .
                                    </td>
                                    <td width="20%">
                                        <asp:DropDownList ID="IP3" runat="server" AutoPostBack="True"
                                            Width="100%" onselectedindexchanged="IP3_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="2%">
                                        .
                                    </td>
                                    <td width="20%">
                                        <asp:DropDownList ID="IP4" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="2%">
                                        /
                                    </td>
                                    <td width="10%">
                                        <asp:DropDownList ID="IPFD" runat="server" AutoPostBack="True" 
                                            Width="100%" onselectedindexchanged="IPFD_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="1"><asp:Button ID="OKButton" runat="server" CssClass="btn_2k3" 
                                onclick="OKButton_Click" Text="确定分配" /></td>
                    </tr>
                </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="tableTitle"  style="height:1px" >
                <asp:Label ID="LabelTitle0" runat="server">已分配IP资源</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td  style="height:1px" >
                 <table style="width: 100%; border-collapse: collapse;" cellpadding="1" cellspacing="0"
                    border="1" bordercolor="#5b9ed1">
                    <tr>
                        <td align="center" width="20%" class="tdBak">
                            所属区域</td>
                        <td width="30%">
                            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="center" width="20%" class="tdBak">
                            所属机房</td>
                        <td width="230%">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TextBox1" runat="server" Width="100%"></asp:TextBox>
                                    </td>
                                    <td width="1">
                                        <img src="../Images/Small/bb_table.gif" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <div id="Div1" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%" align="center" runat="server" >
                <asp:GridView ID="LogicEquIpPzGrid" runat="server" SkinID="GridView1" 
                    BorderWidth="1px" AllowPaging="True" AllowSorting="True" CellSpacing="1" 
                                   AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="IPDZ" HeaderText="IPDZ" />
                    </Columns>
                </asp:GridView>
                </div></td>
                    </tr>
                    <tr>
                        <td  style="height:1px" >
                            <table class="tdBak" id="Table3" cellspacing="0" 
                    cellpadding="0" width="100%" border="0">
                    <tr>
                        <td width="100%" style="padding-left: 6px;">
                            <font face="宋体">总共有<asp:Label ID="DataCountLab0" runat="server" ForeColor="Red">1</asp:Label>条记录，当前第
                                    <asp:Label ID="PageIndexLab0" runat="server" ForeColor="Red">1</asp:Label>页，共
                                    <asp:Label ID="PageCountLab0" runat="server" ForeColor="Red">1</asp:Label>页</font>
                        </td>
                        <td width="1">
                            <asp:Label ID="Label2" runat="server" Width="55px">单页显示</asp:Label>
                        </td>
                        <td width="1">
                            <font face="宋体">
                                <asp:DropDownList ID="PageSize0" runat="server" ForeColor="Red" 
                                Font-Bold="True" AutoPostBack="True"
                                    Width="60px" OnSelectedIndexChanged="PageSize0_SelectedIndexChanged">
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="500">500</asp:ListItem>
                                </asp:DropDownList>
                            </font>
                        </td>
                        <td width="1">
                            <asp:LinkButton ID="PrevButton0" runat="server" ForeColor="#003797" 
                                Width="50px" OnClick="PrevButton0_Click">上一页</asp:LinkButton>
                        </td>
                        <td width="1">
                            <asp:DropDownList ID="GridPageList0" runat="server" ForeColor="Red" Font-Bold="True"
                                AutoPostBack="True" Width="50px" 
                                OnSelectedIndexChanged="GridPageList0_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="1">
                            <asp:LinkButton ID="NextButton0" runat="server" ForeColor="#003797" 
                                Width="50px" OnClick="NextButton0_Click">下一页</asp:LinkButton>
                        </td>
                    </tr>
                </table> </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
      <asp:TextBox ID="CREATEDATETIME" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
      <asp:TextBox ID="UPDATEDATETIME" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
     <asp:TextBox ID="UPDATEUSERNAME" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
    <asp:TextBox ID="SSQY_CODE" runat="server"  style="display:none;"></asp:TextBox>
     <asp:TextBox ID="HOUSE_NAME_GUID" runat="server"  style="display:none;"></asp:TextBox>
      <asp:TextBox ID="HOUSE_NAME_CODE" runat="server"  style="display:none;"></asp:TextBox>
      <asp:TextBox ID="IPDZ" runat="server"  style="display:none;"></asp:TextBox>
     <asp:TextBox ID="IPDZ1" runat="server"  style="display:none;"></asp:TextBox>
     <asp:TextBox ID="IPDZ2" runat="server"  style="display:none;"></asp:TextBox>
      <asp:TextBox ID="GUID" runat="server" Style="display: none"></asp:TextBox>
      <asp:TextBox ID="PK_GUID" runat="server" Style="display: none"></asp:TextBox>
      <asp:TextBox ID="P_GUID" runat="server" Style="display: none"></asp:TextBox>
    <asp:TextBox ID="START_FD" runat="server" Style="display: none"></asp:TextBox>   
    <asp:TextBox ID="P_IP1" runat="server" Style="display: none">10</asp:TextBox>
    <asp:TextBox ID="P_IP2" runat="server" Style="display: none">0</asp:TextBox>
    <asp:TextBox ID="P_IP3" runat="server" Style="display: none">0</asp:TextBox>
    <asp:TextBox ID="P_IP4" runat="server" Style="display: none">0</asp:TextBox>
      <asp:TextBox ID="NAME" runat="server"  style="display:none;"></asp:TextBox>
      <asp:TextBox id="PZ_IPYWLX" runat="server" style="display:none"></asp:TextBox>
        <uc1:windowHeader ID="windowHeader1" runat="server" />
    <asp:Button ID="btnOK" runat="server" Text="Button" onclick="btnOK_Click"  style="display:none"/>
    </form>
</body>
</html>
