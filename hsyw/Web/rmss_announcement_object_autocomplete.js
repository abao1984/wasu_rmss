if (typeof (JSON) == 'undefined') {
    //如果浏览器不支持JSON，则载入json2.js
    $.getScript('json2.js');
}

function split(val){
	return val.split(/,\s*/);
}

function extractLast(term){
	return split(term).pop();
}

function keyDown(event){
	if(event.keyCode ===$.ui.keyCode.TAB && $(this).data("autocomplete").menu.active){
		event.preventDefault();
	}
}

var options = {
	source:function(request,response){
		response($.ui.autocomplete.filter(
			list, extractLast(request.term)
		));
	},
	focus:function(){
		return false;
	},
	select:function(event,ui){
		var terms = split(this.value);
		terms.pop();
		terms.push("");
		this.value = teerms.join(", ")
		return false;
	}
};
	
$(function(){
	$.post("/hsyw/ws.asmx/get_area",{isArea:""},function(data){
		var department_list = JSON.parse(data); 
		
		$.post("/hsyw/ws.asmx/get_user",function(data){
			var user_list = JSON.parse(data);
			var list = department_list.concat(user_list);
			
			$("#post_owner").bind("keydown",keyDown);
			$("#post_owner").autocomplete({
				minLength:0,
				source:function(request,response){
					response($.ui.autocomplete.filter(
						list,extractLast(request.term)
					));
				},
				focus:function(){
					return false;
				},
				select:function(event, ui){
					var terms = split (this.value);
					var ids_input = $("#post_owner_ids");
					terms.pop();
					terms.push(ui.item.value);
					terms.push("");
					this.value = terms.join(", ");
					
					//var val = ids_input.val();
					var id_list = [];
					
					$.each(terms, function(i,item){
						var text = item;
						$.each(list, function(j,obj){
							if (obj.value === text){
								id_list.push(obj.id);
								return false;
							}	
							
						});
					});
					
					ids_input.val(id_list.join(","));
					
					
					return false;
				}
			});
		});
	});
});