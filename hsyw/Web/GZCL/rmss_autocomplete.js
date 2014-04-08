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
    var availableTags = [
      "ActionScript",
      "AppleScript",
      "Asp",
      "BASIC",
      "C",
      "C++",
      "Clojure",
      "COBOL",
      "ColdFusion",
      "Erlang",
      "Fortran",
      "Groovy",
      "Haskell",
      "Java",
      "JavaScript",
      "Lisp",
      "Perl",
      "PHP",
      "Python",
      "Ruby",
      "Scala",
      "Scheme"
    ];
});
 