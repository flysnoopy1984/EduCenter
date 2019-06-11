$(function () {
    var userCourseUrl = "MyLeave?handler=GetCourseByDate";

    Init = function () {

        laydate.render({
            elem: '.DateInput',
            eventElem: '#btn_DatePick',
            trigger: 'click',
            done: function (value, date) {
             //   alert('你选择的日期是：' + value + '\n获得的对象是' + JSON.stringify(date));
                ShowLoading();

                callAjax_Query_NoBlock(userCourseUrl, { "date": value }, GetCourseByDateCallBack, true, GetCourseByDateError);
;            }
        });
 
    }

    GetCourseByDateCallBack = function (result) {
        var list = result.List;
        var root = $(".LeaveList");
        root.empty();
        if (list.length == 0) {
            root.text("当天没有课程，请重新选择日期");
        }
        $.each(list, function (i) {
            var c = list[i];

            var html = $("#HideData .Course").clone();
            html.attr("lCode", c.LessonCode)
            html.find(".CourseName").text(c.CourseName);
            html.find(".CourseStatus").text(c.UserCourseStatusName);
            html.find(".CourseTime").text(c.CourseTime);

            if (c.UserCourseStatus == 1) {
                var cbId = "cb_" + i;
                var inputObj = html.find("input[type=checkbox]").attr("Id", cbId);

                inputObj.next().attr("for", cbId);
            }
         

            root.append(html);
        })
        LoadingDone();
    }

    GetCourseByDateError = function (errorMsg) {
        LoadingDone();
        $(".LeaveList").text(errorMsg);
    }
    ShowLoading = function ()
    {
        $(".LoadingArea").css("display","flex");
        $(".LeaveList").hide();
        
    }
    LoadingDone = function () {
        $(".LoadingArea").hide();
        $(".LeaveList").show();
    }
    Init();
})