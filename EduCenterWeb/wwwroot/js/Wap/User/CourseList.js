$(function () {
    var QueryListUrl = "CourseList?handler=QueryList"
    Init = function () {
        QueryList();
    }

    QueryList = function () {
        callAjax_Query(QueryListUrl, {}, QueryListCallBack, "");
    }
    QueryListCallBack = function (res) {
        var data = res.List;
        var root = $(".dataList");
        if (data.length == 0) {
            $(".nodata").slideDown();
        }
        else {
            $.each(data, function (i) {
                var d = data[i];
                var html = $("#HideData .OneRecord").clone();
                html.find(".courseName").text(d.CourseName);
                html.find(".courseStatus").text(d.CourseStatusName);
                html.find(".courseScheduleType").text(d.CourseScheduleTypeName);
                html.find(".couseTime").text(d.CourseDate + " | " + d.LessonTime);
                root.append(html);
                root.append("<hr />");
            })
        }
       
    }
    Init();
});