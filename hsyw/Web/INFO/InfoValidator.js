function validator(arr) {
    var idx = 0;
    $(arr).each(function(i, val) {
        var list = val.split(':');
        var val = $(list[0]).val()
        if (val == "" || val == null) {
            $(list[0]).focus().parent().css("border", "2px solid red");
            alert("【"+list[1] + "】 未填写");
            idx = 1;
            return false;
        }
        else {
            $(list[0]).parent().css("border", "1px solid #CDCDCD")
        }
        if (list[2] == "int") {
            if (val && !/^[0-9]\d*$/.test(val)) {
                $(list[0]).focus().parent().css("border", "2px solid red");
                alert("【"+list[1] + "】 要为数字类型");
                idx = 1;
                return false;
            }
        }
        else {
            $(list[0]).parent().css("border","1px solid #CDCDCD")
        }
        idx = 0;
    });
    if (idx == 0) {
        return true;
    }
    else {
        return false;
    }
}

//判断删除时选没选行
function ValidatorDelRow(gridView) {
    var idx = 0;
     gridView.find("TR").each(function(i, row) {
        var check = $(row).children("TD:first").children("input").attr("checked");
        if (check == true) {
            idx = 1;
        }
    });
    if (idx == 0) {
        alert('请选择要删除的行');
        return false;
    }

    if (confirm("确认要删除吗")) {
        return true;
    }
    return false;
}

//判断编辑时选没选行
function ValidatorEditRow(gridView) {
    var idx = 0;
    gridView.find("TR").each(function(i, row) {
        var check = $(row).children("TD:first").children("input").attr("checked");
        if (check == true) {
            idx = 1;
        }
    });
    if (idx == 0) {
        alert('请选择要编辑的行');
        return false;
    }
    return true;
}

//信息管理页面中判断方法
function Btn_Del() {
    var GridView = $("#GridView1>Tbody");
    return ValidatorDelRow(GridView);
}

function Btn_Edit() {
    var GridView = $("#GridView1>Tbody");
    return ValidatorEditRow(GridView);
}
