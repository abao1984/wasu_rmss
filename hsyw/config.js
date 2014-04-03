document.onkeydown = check;
function check(e) {
    var code;
    if (!e) var e = window.event;
    if (e.keyCode) code = e.keyCode;
    else if (e.which) code = e.which;
    if (((event.keyCode == 8) &&                                                    //BackSpace 
((event.srcElement.type != "text" &&
event.srcElement.type != "textarea" &&
event.srcElement.type != "password") ||
event.srcElement.readOnly == true)) ||
((event.ctrlKey) && ((event.keyCode == 78) || (event.keyCode == 82))) ||    //CtrlN,CtrlR 
(event.keyCode == 116)) {                                                   //F5 
        event.keyCode = 0;
        event.returnValue = false;
    }
    return true;
}

function limitNum(obj) {
    if (event.keyCode < 47 || event.keyCode > 57 ) {
        if (event.keyCode != 46) {
            event.keyCode = 0;
        }
    }
    return;
}

function checkNumber(e) {
    var key = window.event ? e.keyCode : e.which;
    var keychar = String.fromCharCode(key);
   
    reg = /\d/;
    var result = reg.test(keychar);
    if (!result) {       
       alert("只能输入数字!");
        return false;
    }
    else {     
        return true;
    }
}

    function checkTxt(obj){
        if (isNaN(obj.value)  || obj.sourceIndex === ".") {
            obj.value = '0';
        }
}
 