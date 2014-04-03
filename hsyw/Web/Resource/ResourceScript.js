//枚举类型维护界面
function windowOpenEnumDataPage(EnumSort, LinkCode, BtnName) {
    //EnumSort:枚举分类，LinkCode：关联的枚举大类，BtnName：用于刷新的按钮名称
    var P_Enum_Name = "";
    if (LinkCode != "") {
        P_Enum_Name = document.getElementById(LinkCode).value;
    }
    var url = "../Resource/ResourceEnumData.aspx?ENUM_SORT=" + EnumSort + "&P_ENUM_NAME=" + encodeURI(P_Enum_Name);
    windowOpenPageByWidth(url, "枚举维护", BtnName, "30%", "40%", "10%", "80%");
}
//加载级联的下拉列表
function LoadEnumData(drpName, EnumSort, LinkCode) {
    //drpName:需要级联下拉的控件名称，EnumSort：枚举分类，LinkCode：上级级联编码
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

//改变级联下拉框内容
function changeEnumData(drpName, EnumSort, LinkCode) {
    //drpName:需要级联下拉的控件名称，EnumSort：枚举分类，LinkCode：上级级联编码
    var enumName = document.getElementById(drpName).value;
    document.getElementById(drpName + "_SHORT").value = ShareResource.GetEnumDataShort(EnumSort, enumName).value;
}

//组织结构选择
function windowOpenBranchTree(name, code) {
//name:组织机构文本框，code：组织机构编码文本框
    var url = "../Resource/BranchTree.aspx?ISQY=1&NAME=" + name + "&CODE=" + code;
    windowOpenPageByWidth(url, "组织机构", "", "30%", "40%", "10%", "80%");
}
//IP资源选择
function windowOpenLogicResourceIpSelect(name, linkage_code) {
    var house_guid = "";
    var house_name = "";
    if (linkage_code != "") {
        house_guid = document.getElementById(linkage_code + "_GUID").value;
        house_name = document.getElementById(linkage_code).value;
    }
    var url = "../Resource/LogicResourceIpSelect.aspx?NAME=" + name + "&HOUSE_GUID=" + house_guid + "&HOUSE_NAME=" + encodeURI(house_name);
    windowOpenPage(url, "Ip资源选择", "");
}


//枚举类型选择
function OpenSelect(enumtype, pname, BtnName) {
    //enumtype:枚举分类，pname：关联枚举分类，BtnName：刷新按钮
    var url = "../Resource/ResourceEnumData.aspx?ENUM_SORT=" + enumtype + "&P_ENUM_NAME=" + pname;
    windowOpenPageByWidth(url, "枚举维护", BtnName, "30%", "40%", "10%", "80%");
}

//配置单资源选择
function windowOpenPhyResourceSelect(unit_id, txt_name, p_txt_name, linkage_code, isEqucode, ljjf, zysx) {
    //unit_id 资源主键 txt_name 所选资源文本框名称 p_txt_name 反推上层次资源文本框名称 linkage_code 上一层次资源编码
    //isEqucode 是否存在编码 ljjf 逻辑机房编码 zysx 返回资源属性，格式为 文本框名称1,文本框名称2:资源属性名称1,资源属性名称2
    var txt_code = txt_name + "_CODE";
    var txt_guid = txt_name + "_GUID";
    var res_guid = "";
    var res_code = "";
    var res_name = "";
    if (linkage_code != "") {
        try {
            res_guid = document.getElementById(p_txt_name + "_GUID").value;
        } catch (e) { }
        //if (isEqucode == "1") {
        try {
            res_code = document.getElementById(p_txt_name + "_CODE").value;
        } catch (e) { }
        //}
        res_name = document.getElementById(p_txt_name).value;
    }
    var url = "../Resource/PhyEquSelect.aspx?ISEQUCODE=" + isEqucode + "&UNIT_ID=" + unit_id + "&TXT_NAME=" + txt_name + "&TXT_CODE=" + txt_code + "&TXT_GUID=" + txt_guid + "&RES_GUID=" + res_guid + "&RES_CODE=" + encodeURI(res_code) + "&RES_NAME=" + encodeURI(res_name) + "&NAME_FILED=" + linkage_code + "&P_TXT_NAME=" + p_txt_name + "&LJJF_NAME=" + ljjf + "&ZYSX=" + zysx;
    windowOpenPage(url, "资源选择", "");
}

function windowOpenLogicResourceVpnSelect(name) {
    var url = "../Resource/LogicResourceVpnSelect.aspx?NAME=" + name;
    windowOpenPage(url, "选择VPN资源", "");
}

function windowOpenLogicResourceVlanSelect(name) {
    var url = "../Resource/LogicResourceVlanSelect.aspx?NAME=" + name;
    windowOpenPage(url, "选择Vlan资源", "");
}

function windowOpenRmssSelect(name,btnName) {
    var url = "../Resource/RmssSelect.aspx?YWBM_NAME=" + name;
    windowOpenPage(url, "选择客户资源", btnName);
}