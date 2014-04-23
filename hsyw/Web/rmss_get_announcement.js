if (typeof (JSON) == 'undefined') {
    //如果浏览器不支持JSON，则载入json2.js
    $.getScript('json2.js');
}

$(function(){
	var user_id = $("#user_id").val();
	$.post("/hsyw/ws.asmx/get_announcement",{user_id:user_id},function(data){
		var data_list = JSON.parse(data);
		if (data_list.length>0)
		{
			var announcement = data_list[0];
			var title = announcement.title;
			var content = announcement.content;
			
			$("#mywindows").html("<div class=\"announcement\">"+
			"<h2>"+title+"</h2>"+
			"<div>"+content+"</div>"+
			"</div>");
		}
	});
});