if (typeof (JSON) == 'undefined') {
    //如果浏览器不支持JSON，则载入json2.js
    $.getScript('json2.js');
}

$(function() {
	$.post("/hsyw/ws.asmx/get_area",{isArea:1}, function(data){
		var areaList = JSON.parse(data);
		
		$("#KHQY").autocomplete({
			source:areaList,
			select:function(event, ui){
				$("#KHQYID").val(ui.item.code);
			}
		});
	});
	
	$.post("/hsyw/ws.asmx/get_area",{isArea:""},function(data){
		var departments = JSON.parse(data);
		
		$("#txtCJBM").autocomplete({
			source:departments,
			select:function(event, ui){
				$("#txtCJBMCODE").val(ui.item.code);
			}
		});
		
		$("#txtYRBM").autocomplete({
			source:departments,
			select:function(event, ui){
				$("#txtYRBM").val(ui.item.code);
				
			}
		});
	});
	
	$.post("/hsyw/ws.asmx/get_user",function(data){
		var users = JSON.parse(data);
		
		$("#txtCJRY").autocomplete({
			source:users,
			select:function(event,ui){
				$("#txtCJRYID").val(ui.item.id);
			}
		});
		
		$("#txtYRR").autocomplete({
			source:users,
			select:function(event,ui){
				$("#txtYRRID").val(ui.item.id);
			}
		});
	});
	
});
 