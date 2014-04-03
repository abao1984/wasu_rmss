<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhyResourceEdit.aspx.cs"
    Inherits="Web_Resource_PhyResourceEdit" %>

<%@ Register Src="../Include/Ascx/windowHeader.ascx" TagName="windowHeader" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <% =Session["PageSubTite"].ToString().Trim() + " - " + Session["PageTitle"].ToString().Trim() + " - " + Session["CopyRightAuthor"].ToString().Trim()%>
    </title>

    <script language="javascript" type="text/javascript" src="../../calendar.js"></script>

    <script language="javascript" type="text/javascript" src="../../config.js"></script>

    <script type="text/javascript">
        function windowOpen(unit_id, unit_name, guid, name, name_filed, filedGuid, filedName, parent_unit_id) {
            if (document.getElementById("UPDATEDATETIME").value == "") {
                alert("请先保存主记录后，新增记录！");
                return;
            }
            if (filedName == "") {
                filedName = document.getElementById(document.getElementById("NAME_FILED").value).value;
            }
            var url = "PhyResourceEdit.aspx?GUID=" + guid + "&UNIT_ID=" + unit_id + "&NAME_FILED=" + name_filed
            + "&NAME_FILED_GUID=" + filedGuid + "&NAME_FILED_NAME=" + encodeURI(filedName) + "&PARENT_UNIT_ID=" + parent_unit_id;
            if (name == "") {
                name = "新增";
                document.getElementById("ISAdd").value = "y";
            }
            else {
                document.getElementById("ISAdd").value = "";
            }
            var strName = unit_name + "-----" + name;
            windowOpenPage(url, strName, "Btn");  
        }
        function ReplaceLabelHead(name) {
            parent.document.getElementById("LabelHead").innerText = parent.document.getElementById("LabelHead").innerText.replace("-----新增", "-----" + name);
        }
        function windowOpenEnumDataPage(EnumSort, LinkCode) {
            var P_Enum_Name = "";
            if (LinkCode != "") {
                P_Enum_Name = document.getElementById(LinkCode).value;
            }
            var url = "ResourceEnumData.aspx?ENUM_SORT=" + EnumSort + "&P_ENUM_NAME=" + encodeURI(P_Enum_Name);
            windowOpenPageByWidth(url, "枚举维护", "Btn", "30%", "40%", "10%", "80%");
        }
        function LoadEnumData(drpName, EnumSort, LinkCode) {
            var drp = document.getElementById(drpName);
            var oldValue = drp.value;
            var code = document.getElementById(LinkCode).value;
            while (drp.options.length = 0) {
                drp.remove(0);
            }
            ds = ShareResource.getEnumData(EnumSort, code).value;
            for (i = 0; i < ds.Tables[0].Rows.length; i++) {
                var newOption = document.createElement("OPTION");
                newOption.text = ds.Tables[0].Rows[i].ENUM_NAME;
                newOption.value = ds.Tables[0].Rows[i].ENUM_NAME;
                drp.options.add(newOption);
            }
            drp.value = oldValue;
        }

        function changeEnumData(drpName, EnumSort, LinkCode) {
            var enumName = document.getElementById(drpName).value;
            document.getElementById(drpName + "_SHORT").value = ShareResource.GetEnumDataShort(EnumSort,enumName).value;
        }
        
        function windowOpenBranchTree(name, code) {
            var url = "BranchTree.aspx?ISQY=1&NAME=" + name + "&CODE=" + code;
            windowOpenPageByWidth(url, "组织机构", "", "30%", "40%", "10%", "80%");
        }

        function windowOpenLogicResourceIpSelect(name, linkage_code) {
            var house_guid = "";
            var house_name = "";
            if (linkage_code != "") {
                house_guid = document.getElementById(linkage_code + "_GUID").value;
                house_name= document.getElementById(linkage_code).value;
            }
            var url = "LogicResourceIpSelect.aspx?NAME=" + name + "&HOUSE_GUID=" + house_guid + "&HOUSE_NAME=" + encodeURI(house_name);
            windowOpenPage(url, "资源选择", ""); 
        }
        
        function windowOpenPhyResourceSelect(propery_id, name, code,guid,linkage_code,isEqucode) {
            var res_guid = "";
            var res_code = "";
            var res_name = "";
            if (linkage_code != "") {
                res_guid = document.getElementById(linkage_code + "_GUID").value;
                if (isEqucode == "1") {
                    res_code = document.getElementById(linkage_code + "_CODE").value;
                }
                res_name=document.getElementById(linkage_code).value;
            }
            var url = "PhyResourceSelect.aspx?ISEQUCODE=" + isEqucode + "&PROPERY_ID=" + propery_id + "&TXT_NAME=" + name + "&TXT_CODE=" + code + "&TXT_GUID=" + guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name) + "&NAME_FILED=" + linkage_code;
            windowOpenPage(url, "资源选择", ""); 
        }

        function windowOpenPhyResourceBatchEdit() {
            var father_unit_id = document.getElementById("UNIT_ID").value;
            var unit_id = document.getElementById("CHILD_UNIT_ID").value;
            var guid = document.getElementById("GUID").value;
            var name = document.getElementById("NAME_FILED").value;
            var name_file_name = document.getElementById(name).value;           
            var url = "PhyResourceBatchEdit.aspx?PARENT_UNIT_ID=" + father_unit_id + "&UNIT_ID=" + unit_id + "&NAME_FILED_GUID=" + guid + "&NAME_FILED_NAME=" + encodeURI(name_file_name);
            var strName = document.getElementById("CHILD_UNIT_NAME").value + "批量操作";
            windowOpenPage(url, strName, "Btn"); 
            event.returnValue = false;
            
        }
        function changeMenu(child_unit_id) {
            document.getElementById("CHILD_UNIT_ID").value = child_unit_id;
            document.getElementById("MenuButton").click();
        }
        function scrollSave() {
        
            document.getElementById("PhyResourceText").value = self['PhyResourceDIV'].scrollTop;
        }
        function SetScroll() {           
           self['PhyResourceDIV'].scrollTop = document.getElementById("PhyResourceText").value;          
        }
      
        function changeChildUnitId() {
            if (document.getElementById("EXIST_GROOVY").checked) {
                document.getElementById("CHILD_UNIT_ID").value = "00c3a457-24eb-4c00-a7e0-5e7f0116fe68";
            }
            else {
                document.getElementById("CHILD_UNIT_ID").value = "bfc13d2d-eab8-4784-a96a-b8ffc21b4e88";
            }
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
        function OpneLogList() {
         var guid = document.getElementById("GUID").value;
         var url = "../LogList.aspx?PK_GUID=" + guid;
            windowOpenPage(url, "操作日志", ""); 
        }
        function AddGld(guid, lsid) {
            if (document.getElementById("UPDATEDATETIME").value == "") {
                alert("请先保存主记录后，新增记录！");
                return;
            }

            var url = "GuangLanDuanEdit.aspx?GUID=" + guid + "&LSID=" + lsid;
            windowOpenPage(url, "新增光缆段", "Btn");  
        }
        function CopyData()
        {
            document.getElementById("ImportDiv").style.display="block";
            return false;
        }
        function ValidatorEqu()
        {
             var equ_code = document.getElementById("EQU_CODE").value;
             var equ_name = document.getElementById("EQU_NAME").value;
             
             var new_equ_code = document.getElementById("New_Equ_Code").value;
             var new_equ_name = document.getElementById("New_Equ_Name").value;
             if(equ_code==new_equ_code)
             {
                alert("设备编码不能和新设备编码一致，请填写别的编码");
                return false;
             }
             if(equ_name==new_equ_name)
             {
                alert("设备名称不能和新设备名称一致，请填写别的名称");
                return false;
             }
             if(new_equ_name=="")
             {
                alert("设备名称不能为空");
                return false;
             }
             if(new_equ_code=="")
             {
                alert("设备编码不能为空");
                return false;
             }
             var lx = document.getElementById("UNIT_NAME").value;
             var response=ShareResource.checkSbMc(new_equ_name,new_equ_code,lx);
             if(response.value!="")
             {
                alert(response.value);
                return false;
             }
             document.getElementById("BtnCopy").style.display="none";
            return true;
         
       }
       
        function Close() {
            document.getElementById("ImportDiv").style.display = "none";
        }

        function windowOpenGX(guid, ywlx) {
            var url = "";
            if (ywlx == "vpn") {
                url = "ConfigLightEditVpn.aspx?YWGUID=" + guid+"&query=0";
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
    <table style="width: 100%; height: 100%">
        <tr>
            <td class="tableHead" height="1">
                <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="50%">
                            <asp:TextBox ID="PhyResourceText" runat="server" Width="46px" Style="display: none;">0</asp:TextBox>
                            <asp:TextBox ID="UNIT_ID" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                            <asp:TextBox ID="UNIT_NAME" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                            <asp:TextBox ID="CHILD_UNIT_NAME" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                            <asp:TextBox ID="CREATEDATETIME" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                            <asp:TextBox ID="UPDATEDATETIME" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                            <asp:TextBox ID="UPDATEUSERNAME" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                            <asp:TextBox ID="CHILD_UNIT_ID" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                            <asp:TextBox ID="CHILD_GRID_MODE" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                            <asp:TextBox ID="PARENT_UNIT_ID" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                            <asp:TextBox ID="PARENT_UNIT_GUID" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                            <asp:TextBox ID="CHILD_NAME_FILED" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                            <asp:TextBox ID="NAME_FILED" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                            <asp:TextBox ID="NAME_FILED_GUID" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                            <asp:TextBox ID="NAME_FILED_NAME" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                            <asp:TextBox ID="GUID" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                            <asp:TextBox ID="TABLE_NAME" runat="server" Width="46px" Style="display: none;"></asp:TextBox>
                            <asp:Button ID="SaveButton" runat="server" CssClass="btn_2k3" OnClick="SaveButton_Click"
                                Text="保存" />
                            <asp:Button ID="DeleteButton" runat="server" CssClass="btn_2k3" OnClick="DeleteButton_Click"
                                Text="删除" OnClientClick="return confirm(&quot;确定删除吗？&quot;);" />
                            <asp:Button ID="changeButton" runat="server" CssClass="btn_2k3" OnClick="changeButton_Click"
                                Text="隐藏基本属性" />
                            <asp:Button ID="Btn" runat="server" OnClick="Btn_Click" Style="display: none;" />
                            <asp:Button ID="MenuButton" runat="server" OnClick="MenuButton_Click" Style="display: none;"
                                Text="Button" />
                            <asp:Button ID="CoppyButton" runat="server" CssClass="btn_2k3" Text="复制" Visible="false"
                                OnClientClick="return CopyData();" />
                        </td>
                        <td width="50%" style="padding-right: 2px" align="right">
                            <a href="#" onclick="OpneLogList()">操作日志</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="TR_PROPERTY" runat="server">
            <td id="TD_PROPERTY" runat="server" valign="top" height="1">
            </td>
        </tr>
        <tr id="Menu_TR" runat="server">
            <td style="height: 34px; background-image: url('../Images/Header/tr_top02.jpg');">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr id="MenuTr" runat="server">
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="Menu_Button_TR" runat="server">
            <td class="tableHead" align="right">
                <table align="right">
                    <tr>
                        <td>
                            <asp:Button ID="AddButton" runat="server" CssClass="btn_2k3" Text="新增" OnClick="AddButton_Click" />
                        </td>
                        <td>
                            <asp:Button ID="DeleButton" runat="server" CssClass="btn_2k3" Text="删除" OnClientClick="return confirm('确定需要删除吗？');"
                                OnClick="DeleButton_Click" />
                        </td>
                        <td>
                            <asp:Button ID="SelectButton" runat="server" CssClass="btn_2k3" Text="全选" OnClientClick="SelectAll();" />
                        </td>
                        <td>
                            <asp:Button ID="BatchButton" runat="server" CssClass="btn_2k3" Text="批量操作" OnClientClick="windowOpenPhyResourceBatchEdit()" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <div id="PhyResourceDIV" style="overflow: auto; width: 100%; height: 100%" align="center"
                    runat="server" onscroll="scrollSave()">
                    <asp:GridView ID="GridViewPhyResource" runat="server" SkinID="GridView1" DataKeyNames="GUID"
                        BorderStyle="None" BorderWidth="0px" AllowPaging="True" AllowSorting="True" OnRowDataBound="GridViewPhyResource_RowDataBound"
                        OnSorting="GridViewPhyResource_Sorting">
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr id="TR_PAGE" runat="server">
            <td height="1">
                <table class="tdBak" id="Table2" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td width="100%" style="padding-left: 6px;">
                            <font face="宋体">总共有
                                <asp:Label ID="DataCountLab" runat="server" ForeColor="Red"></asp:Label></font><font
                                    face="宋体">条记录，当前第
                                    <asp:Label ID="PageIndexLab" runat="server" ForeColor="Red"></asp:Label>页，共
                                    <asp:Label ID="PageCountLab" runat="server" ForeColor="Red"></asp:Label>页</font>
                        </td>
                        <td width="1" class="style1">
                            <asp:Label ID="Label1" runat="server" Width="55px">单页显示</asp:Label>
                        </td>
                        <td width="1" class="style1">
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
                        <td width="1" class="style1">
                            <asp:LinkButton ID="PrevButton" runat="server" ForeColor="#003797" Width="50px" OnClick="PrevButton_Click">上一页</asp:LinkButton>
                        </td>
                        <td width="1" class="style1">
                            <asp:DropDownList ID="GridPageList" runat="server" ForeColor="Red" Font-Bold="True"
                                AutoPostBack="True" Width="50px" OnSelectedIndexChanged="GridPageList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td width="1" class="style1">
                            <asp:LinkButton ID="NextButton" runat="server" ForeColor="#003797" Width="50px" OnClick="NextButton_Click">下一页</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    <asp:TextBox ID="ISAdd" runat="server" Style="display: none"></asp:TextBox>
    <div id="ImportDiv" runat="server" style="border: 2px solid #5b9ed1; display: none;
        position: absolute; z-index: inherit; width: 500px; height: 100px; top: 50%;
        left: 50%; margin-top: -50px; margin-left: -250px;">
        <table style="width: 100%; height: 100%" border="0px" cellpadding="0" cellspacing="0">
            <tr style="height: 29px" ondblclick="MaxWinodw(document.getElementById('XPMax'));">
                <td width="29px" style="background-image: url('../images/IE7.gif')">
                    <asp:Label ID="Label3" runat="server" Text="" Width="29px"></asp:Label>
                </td>
                <td style="background-image: url('../images/WindowXPHead.gif')" width="60%">
                    <asp:Label ID="Label4" runat="server" ForeColor="White" Font-Bold="True"></asp:Label>
                </td>
                <td style="background-image: url('../images/WindowXPHead.gif'); width: 40%" align="right"
                    valign="middle">
                    <img alt="" src="../images/WindowXPClose.gif" border="0" id="Img3" title="关闭退出" onclick="javascript:Close();">
                </td>
            </tr>
            <tr id="Tr1">
                <td colspan="3">
                    <table width="100%">
                        <tr>
                            <td width="40%" align="right" class="tdBak">
                                输入设备编码
                            </td>
                            <td class="tdBak">
                                <asp:TextBox ID="New_Equ_Code" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="40%" align="right" class="tdBak">
                                输入设备名称
                            </td>
                            <td class="tdBak">
                                <asp:TextBox ID="New_Equ_Name" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="tdBak" align="center">
                                <asp:Button ID="BtnCopy" runat="server" Text="复制" CssClass="btn_2k3" OnClick="BtnCopy_Click"
                                    OnClientClick="return ValidatorEqu();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
