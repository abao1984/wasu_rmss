function WindowOpen(url, width, height){
    var sRet = window.showModalDialog('WindowMain.aspx?url='+url, window, 'dialogHeight:'+ height + ';dialogWidth:'+ width +';center:yes;status:no;help:no;scroll:yes') 
    if(sRet == "refresh") 
    {
            window.location.reload();
    }
}
function WindowListOpen(url, width, height){
    var sRet = window.showModalDialog('WindowMain.aspx?url='+url, window, 'dialogHeight:'+ height + ';dialogWidth:'+ width +';center:yes;status:no;help:no;scroll:yes') 
    if(sRet == "refresh") 
    {
            window.location.reload();
    }
}
function WindowTextOpen(url, width, height){
    var sRet = window.showModalDialog('WindowMain.aspx?url='+ url, window, 'dialogHeight:'+ height + ';dialogWidth:'+ width +';center:yes;status:no;help:no;scroll:no') 
}


function WindowCloseRefresh(){
        window.returnValue = "refresh";
        window.close();
}

function WindowRefresh(){
        window.dialogArguments.location.reload();
}

function WindowClose(){
        window.close();
}

