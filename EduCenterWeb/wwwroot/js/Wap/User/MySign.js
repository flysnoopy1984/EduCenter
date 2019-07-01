$(function () {
    var InitPageUrl = "MySign?handler=InitPage";
    var SignCourseUrl = "MySign?handler=SignCourse"
    Init = function () {

        callAjax_Query(InitPageUrl, {}, InitPageCallBack, "", function (res) {
            if (res.IntMsg == -1)
                window.location.href = "Login";
            else if (res.IntMsg == -2)
                window.location.href = "Home";
        });

    };

    InitPageCallBack = function (res) {
        var list = res.List;
        var data = list[0];

        var title = $(".Title");
        var html;
        var hasUnSign = false;
        //不能签到
        if (!data.CanSign) {
            html = $("#HideData .NoCourse").clone();
            html.find(".CourseName").text("[" + data.CourseScheduleTypeName + "] " + data.CourseName);
            html.find(".CourseDate").text(data.CourseDate + " " + data.StartTime);
            var statusName = "我要签到";
            //状态1代表准备上课
            if (data.UserCourseLogStatus != 1)
                statusName = data.UserCourseLogStatusName;

            html.find(".CourseStatus").text(statusName);
            title.after(html);
        }
        else {
            $.each(list, function (i) {
                var item = list[i];
                if (i == 0) {
                    html = $("#HideData .UnSignArea").clone();
                    html.find(".PreSign").remove();
                    title.after(html);
                    title = html.find(".SignInfo");
                }
                //已经签到
                if (item.UserCourseLogStatus >= 10) {

                    html = $("#HideData .SignedArea .PreSign").clone();
                    html.find(".CourseName").text("[" + data.CourseScheduleTypeName + "] " + data.CourseName);
                    html.find(".CourseDate").text(data.CourseDate + " " + data.StartTime);
                    html.append("<hr />");
                }
                else {
                    //还没签到
                    html = $("#HideData .UnSignArea .PreSign").clone();
                    html.find(".CourseName").text("[" + data.CourseScheduleTypeName + "] " + data.CourseName);
                    html.find(".CourseDate").text(data.CourseDate + " " + data.StartTime);
                    html.append("<hr />");

                    var btn = html.find("button");
                    btn.attr("lcode", item.LessonCode);
                    btn.on("click", SignUp);
                    hasUnSign = true;
                }
              
                title.after(html);

            });

            if (!hasUnSign) {
                title.hide();
            }
            
        }

       
    }


    SignUp = function (e) {
        var obj = e.currentTarget;
        var LessonCode = $(obj).attr("lcode");

        callAjax_Query(SignCourseUrl, { "LessonCode": LessonCode }, function (res) {

            $(obj).removeClass("btn-info");
            $(obj).addClass("btn-block");
            $(obj).addClass("disabled");

            $(obj).find("i").removeClass("fa-sign-in");
            $(obj).find("i").addClass("fa fa-check-circle");
            $(obj).find(".CourseStatus").text("您已签到");

            $(obj).off("click");

        }, "",
        function (res) {
            if (res.IntMsg == -1) {
                window.location.href = "Login";
            } 
            else if (res.IntMsg == 100)
                window.location.href = "BuyCourseTime";
        });

      
      
    }

    Init();
});