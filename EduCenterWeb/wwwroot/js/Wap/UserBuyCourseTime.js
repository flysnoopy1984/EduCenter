$(function () {
    Init = function () {
      
        var list = GetSessonUserApplyCourse();
        if (list) {
            var html = "";
            $.each(list, function (i) {
                html += list[i].lcode;
                html += "<br />";
            });
            $("#title").html(html);
        }
       
    }

    Init();
});