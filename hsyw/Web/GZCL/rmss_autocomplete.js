$(function() {
	if (typeof (JSON) == 'undefined') {
     //如果浏览器不支持JSON，则载入json2.js
     $.getScript('json2.js');
	}

	$.post("/hsyw/ws.asmx/get_area",{isArea:1}, function(data){
		var areaList = JSON.parse(data);
		
		$("#KHQY").autocomplete({
			source:areaList
		});
	});
	
	$.post("/hsyw/ws.asmx/get_area",{isArea:""},function(data){
		var departments = JSON.parse(data);
		
		$("#txtCJBM").autocomplete({
			source:departments
		});
		
		$("#txtYRBM").autocomplete({
			source:departments
		});
	});
	
	$.post("/hsyw/ws.asmx/get_user",function(data){
		var users = JSON.parse(data);
		
		$("#txtCJRY").autocomplete({
			source:users
		});
		
		$("#txtYRR").autocomplete({
			source:users
		});
	});
	
});
 