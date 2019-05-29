$(function () {

    selectCourse = function (obj) {
        var cell = $(obj);
        var day = cell.attr("day");
        var lesson = cell.attr("lesson");

       // var html = $(".CourseSelect").clone();
        var html = '<div class="CourseSelect">';
        html += '<div class="checkbox checkbox-primary">';
        html += ' <input type="checkbox" id="checkbox1">';
        html += '<label for="checkbox1">Check me out</label>';
        html += '</div>';
        html += '</div>';
        $.dialog({
            title: '选择课程',
            content: html,
        });
    }
});