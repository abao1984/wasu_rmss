<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhyResourceList.aspx.cs"
    Inherits="Web_Resource_PhyResourceList" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <% =Session["PageSubTite"].ToString().Trim() + " - " + Session["PageTitle"].ToString().Trim() + " - " + Session["CopyRightAuthor"].ToString().Trim()%>
    </title>

    <script language="javascript" type="text/javascript" src="../../config.js"></script>

    <script language="javascript" type="text/javascript">
        function windowOpen(guid, name, name_filed) {
            var unit_id = document.getElementById("UNIT_ID").value;
            var url = "PhyResourceEdit.aspx?PAGE_TYPE=LIST&GUID=" + guid + "&UNIT_ID=" + unit_id + "&NAME_FILED=" + name_filed;
            var strName = document.getElementById("UNIT_NAME").value + "-----" + name;
            windowOpenPage(url, strName, "Btn");
        }
        function BranchTree(name, code) {
            var url = "BranchTree.aspx?ISQY=1&NAME=" + name + "&CODE=" + code;
            windowOpenPageByWidth(url, "组织机构", "", "30%", "40%", "10%", "80%");
        }
        function windowOpenPhyResourceSelect(propery_id, name, code, guid, linkage_code, isEqucode) {
            var res_guid = "";
            var res_code = "";
            var res_name = "";
            if (linkage_code != "") {
                res_guid = document.getElementById(linkage_code + "_GUID").value;
                if (isEqucode == "1") {
                    res_code = document.getElementById(linkage_code + "_CODE").value;
                }
                res_name = document.getElementById(linkage_code).value;
            }
            var url = "PhyResourceSelect.aspx?ISEQUCODE=" + isEqucode + "&PROPERY_ID=" + propery_id + "&TXT_NAME=" + name + "&TXT_CODE=" + code + "&TXT_GUID=" + guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name) + "&NAME_FILED=" + linkage_code;
            windowOpenPage(url, "资源选择", "");
        }
        function limitNum(obj) {
            if (event.keyCode < 47 || event.keyCode > 57) {
                event.keyCode = 0;
            }
            return;
        }
        function scrollSave() {
            document.getElementById("PhyResourceText").value = self['PhyResourceDIV'].scrollTop;

        }
        function SetScroll() {
            self['PhyResourceDIV'].scrollTop = document.getElementById("PhyResourceText").value;
        }

        function SelectAll() {
            var DataGrid = document.getElementById("GridViewPhyResource");
            var elements = DataGrid.all.tags("input");
            for (var i = 0; i < elements.length; i++) {
                if (elements[i].type.toLowerCase() == "checkbox") {
                    elements[i].checked = true;
                }
            }
            event.returnValue = false;
        }

        function ExpOpen() {
            // UNIT_ID
            var unitid = document.getElementById("UNIT_ID").value;
            if (unitid != "") {
                var url = "PhyResourceExp.aspx?UNIT_ID=" + unitid;
                windowOpenPageByWidth(url, "选择打印数据", "BtnExp", "30%", "40%", "10%", "80%");
            }
            event.returnValue = false;
//            var strName = "";
//            strNamed = document.getElementById(TxtName).value;
//            if (strName == "") {
//                
//            }
        }

        function windowOpenGX(guid, ywlx) {
            var url = "";
            if (ywlx == "vpn") {
                url = "ConfigLightEditVpn.aspx?YWGUID=" + guid + "&query=0";
            }
            else if (ywlx == "gg") {
                url = "ConfigLightEditBone.aspx?YWGUID=" + guid + "&query=0";
            }
            else {
                url = "ConfigLightEdit.aspx?YWGUID=" + guid + "&query=0";
            }
            windowOpenPage(url, "光缆资源配置", "BtnQuery");
            window.event.returnValue = false;
        }
    </script>

</head>
<body onload="SetScroll()">
    <form id="form1" runat="server">
    <table id="Main_Table" style="width: 100%; height: 100%;">
        <tr>
            <td class="tableHead" style="border: 1px solid #F0F0F0">
                <asp:TextBox ID="UNIT_ID" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="UNIT_NAME" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="NAME_FILED" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                <asp:TextBox ID="PhyResourceText" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                <asp:Button ID="AddButton" runat="server" Text="新增" CssClass="btn_2k3" OnClick="AddButton_Click" />
                <asp:Button ID="DeleteButton" runat="server" OnClick="DeleteButton_Click" CssClass="btn_2k3"
                    Text="删除" OnClientClick="return confirm('确定要删除吗？');" />
                <asp:Button ID="SelectButton" runat="server" OnClick="Btn_Click" CssClass="btn_2k3"
                    Text="全选" OnClientClick="SelectAll();" />
                <asp:Button ID="QueryButton" runat="server" CssClass="btn_2k3" Text="查询" OnClick="QueryButton_Click" />
                <asp:Button ID="ExpButton" runat="server" CssClass="btn_2k3" Text="导出" OnClientClick="ExpOpen('strMC')" />
                <asp:Button ID="Btn" runat="server" OnClick="Btn_Click" CssClass="btn_2k3" Text="查询"
                    Style="display: none;" />
                <asp:Button ID="BtnExp" runat="server" CssClass="btn_2k3" Text="导出" OnClick="BtnExp_Click"
                    Style="display: none;" />
        <tr>
            <td class="tableTitle" style="border: 1px solid #F0F0F0" align="center" height="26">
                <asp:Label ID="LabelTitle" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="TD_QUERY" runat="server" height="1" style="border: 1px solid #F0F0F0">
            </td>
        </tr>
        <tr>
            <td valign="top" style="border: 1px solid #F0F0F0">
                <div id="PhyResourceDIV" style="overflow: auto; width: 100%; height: 100%" align="center"
                    runat="server" onscroll="scrollSave()">
                    <asp:GridView ID="GridViewPhyResource" runat="server" SkinID="GridView1" DataKeyNames="GUID"
                        OnRowDataBound="GridViewPhyResource_RowDataBound" BorderStyle="None" BorderWidth="0px"
                        AllowPaging="True" AllowSorting="True" OnSorting="GridViewPhyResource_Sorting">
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td height="1" style="border: 1px solid #F0F0F0">
                <table class="tdBak" id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td width="100%" style="padding-left: 6px;">
                            <font face="宋体">总共有
                                <asp:Label ID="DataCountLab" runat="server" ForeColor="Red"></asp:Label></font><font
                                    face="宋体">条记录，当前第
                                    <asp:Label ID="PageIndexLab" runat="server" ForeColor="Red"></asp:Label>页，共
                                    <asp:Label ID="PageCountLab" runat="server" ForeColor="Red"></asp:Label>页</font>
                        </td>
                        <td width="1">
                            <asp:Label ID="Label1" runat="server" Width="55px">单页显示</asp:Label>
                        </td>
                        <td width="1">
                            <font face="宋体">
                                <asp:DropDownList ID="PageSize" runat="server" ForeColor="Red" Font-Bold="True" AutoPostBack="True"
                                    Width="60px" OnSelectedIndexChanged="PageSize_SelectedIndexChanged">
                                    <asp:ListItem Value="50">50</asp:ListItem>
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
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
