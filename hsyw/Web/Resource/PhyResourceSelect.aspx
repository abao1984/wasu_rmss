<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhyResourceSelect.aspx.cs" Inherits="Web_Resource_PhyResourceSelect" %>
<%@ Register src="../Include/Ascx/windowHeader.ascx" tagname="windowHeader" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>      
        function windowOpenBranchTree(name, code) {
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
        function changeCheck(oCheck) {
            var oldCheck = document.getElementById("oldCheckID").value;
            if (oCheck.checked) {
                if (oldCheck !="") {
                    document.getElementById(oldCheck).checked = false;
                }
                document.getElementById("oldCheckID").value = oCheck.id;
            }
        }
        function scrollSave() {
            document.getElementById("PhyResourceText").value = self['PhyResourceDIV'].scrollTop;

        }
        function SetScroll() {
            self['PhyResourceDIV'].scrollTop = document.getElementById("PhyResourceText").value;
        }
        function getSelectRowId() {
            var DataGrid = document.getElementById("GridViewPhyResource");
            var elements = DataGrid.all.tags("input");
            var RowId = "";
            for (var i = 0; i < elements.length; i++) {
                if (elements[i].type.toLowerCase() == "checkbox") {
                    if (elements[i].checked == true) {
                        if (RowId != "") {
                            RowId += ",";
                        }
                        RowId += i;
                    }
                }
            }
            document.getElementById("TXT_ROWID").value = RowId;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <table id="Main_Table" style="width:100%;height:100%;">
        <tr>
            <td class="tableHead">
            <asp:TextBox ID="PhyResourceText" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
            <asp:TextBox ID="TXT_ROWID" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                <asp:TextBox ID="PROPERY_ID" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                <asp:TextBox ID="UNIT_ID" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                <asp:TextBox ID="UNIT_NAME" runat="server" Width="46px"  style="display:none;"></asp:TextBox>
                <asp:TextBox ID="TXT_NAME" runat="server" Width="103px" style="display:none;"></asp:TextBox>               
                <asp:TextBox ID="TXT_GUID" runat="server" Width="103px" style="display:none;"></asp:TextBox>
                <asp:TextBox ID="TXT_CODE" runat="server" Width="103px" style="display:none;"></asp:TextBox>
                 <asp:TextBox ID="RES_GUID" runat="server" Width="103px" style="display:none;"></asp:TextBox>
                 <asp:TextBox ID="RES_CODE" runat="server" Width="103px" style="display:none;"></asp:TextBox>
                  <asp:TextBox ID="RES_NAME" runat="server" Width="103px" style="display:none;"></asp:TextBox>
                   <asp:TextBox ID="NAME_FILED" runat="server" Width="103px" style="display:none;"></asp:TextBox>
                    <asp:TextBox ID="ISEQUCODE" runat="server" Width="103px" style="display:none;"></asp:TextBox>
                <table style="width:100%;">
                    <tr>
                        <td>
                <asp:Button ID="Btn" runat="server"  CssClass="btn_2k3"   Text="查询" onclick="Btn_Click"   />
                </td><td>
                <asp:Button ID="OKButton" runat="server"  CssClass="btn_2k3"   Text="确定" onclick="OKButton_Click"  />
                            
                        </td>
                        <td width="100%"><asp:RadioButtonList ID="RadioResource" runat="server" 
                                RepeatDirection="Horizontal" AutoPostBack="True" 
                                
                                DataTextField="UNIT_NAME" DataValueField="UNIT_ID" 
                                onselectedindexchanged="RadioResource_SelectedIndexChanged">
                            </asp:RadioButtonList>
                        </td>
                        <td style="padding-right: 6px">
                            <asp:TextBox 
                                ID="oldCheckID" runat="server" Width="68px" style="display:none;"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="TD_QUERY" runat="server" style="height:1px;">
               </td>
        </tr>
        <tr>
            <td>
            <div id="PhyResourceDIV" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%" align="center" runat="server"  onscroll="scrollSave()">
                <asp:GridView ID="GridViewPhyResource" runat="server" SkinID="GridView1" 
                     DataKeyNames="GUID" onrowdatabound="GridViewPhyResource_RowDataBound1" 
                    AllowPaging="True"  >                    
                </asp:GridView>
                 </div>
            </td>
        </tr>
        <tr>
            <td style="height:1px;">  <table class="tdBak" id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="100%"   style="padding-left:6px;"><FONT face="宋体">总共有
										<asp:label id="DataCountLab" runat="server" ForeColor="Red"></asp:label></FONT><FONT face="宋体">条记录，当前第
										<asp:label id="PageIndexLab" runat="server" ForeColor="Red"></asp:label>页，共
										<asp:label id="PageCountLab" runat="server" ForeColor="Red"></asp:label>页</FONT></td>
								<td width="1" class="style1"><asp:label id="Label1" runat="server" Width="55px">单页显示</asp:label></td>
								<td width="1" class="style1"><FONT face="宋体"><asp:dropdownlist id="PageSize" runat="server" 
                                        ForeColor="Red" Font-Bold="True" AutoPostBack="True"
											Width="60px" onselectedindexchanged="PageSize_SelectedIndexChanged">
											
											<asp:ListItem Value="50">50</asp:ListItem>
											<asp:ListItem Value="100">100</asp:ListItem>
											<asp:ListItem Value="200">200</asp:ListItem>
											<asp:ListItem Value="500">500</asp:ListItem>
										</asp:dropdownlist></FONT></td>
								<td width="1" class="style1"><asp:linkbutton id="PrevButton" runat="server" ForeColor="#003797" 
                                        Width="50px" onclick="PrevButton_Click">上一页</asp:linkbutton></td>
								<td width="1" class="style1"><asp:dropdownlist id="GridPageList" runat="server" 
                                        ForeColor="Red" Font-Bold="True" AutoPostBack="True"
										Width="50px" onselectedindexchanged="GridPageList_SelectedIndexChanged"></asp:dropdownlist></td>
								<td width="1" class="style1"><asp:linkbutton id="NextButton" runat="server" ForeColor="#003797" 
                                        Width="50px" onclick="NextButton_Click">下一页</asp:linkbutton></td>
							</tr>
						</table>
               </td>
        </tr>
    </table>   
    <uc1:windowHeader ID="windowHeader1" runat="server" />
    </form>
</body>
</html>
