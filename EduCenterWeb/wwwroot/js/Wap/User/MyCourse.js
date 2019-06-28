$(function () {
    var carousel = null;
    var InitDataUrl = "MyCourse?handler=InitData";
    
    Init = function () {

        //$("#btn_Leave").on("click", LeaveEvent);
        //$("#btn_Sign").on("click", SignInEvent);

        callAjax_Query(InitDataUrl, {}, InitDataCallBack, "", function (res) {
            if (res.IntMsg == -1)
                window.location.href = "Login";
        });

    };

    InitDataCallBack = function (res) {
        var data = res.Entity.UserCourseList;
        var showCourse = res.Entity.UserShowCourse;

        
        if (data.length == 0) {
            if (res.IntMsg == 0 || res.IntMsg == 10) {
                ShowInfo("您还没有选择每周课程", null, null, 2, function () {
                    window.location.href = "Apply";
                });
            }
            else {
                ShowInfo("您还没有选择假期课程", null, null, 2, function () {
                    window.location.href = "ApplyWinterSummer";
                });
            }
            return;
        }
        else {
            //课程表显示
            $.each(data, function (i) {
                var uc = data[i];
                var td = $("#CourseTable tr td[day=" + uc.Day + "][lesson=" + uc.Lesson + "]");
                var item = $("#HideData .CourseCellContainer").clone();

                var cell = item.find(".CourseCell");
                cell.text(uc.CourseName);
                if (showCourse.LessonCode == uc.LessonCode) {
                    cell.addClass("bg-danger");
                }
                else
                    cell.addClass("bg-primary");
              
                td.append(item);
            });
        }

        //当前课程显示
        if (showCourse) {
            var root = $(".ShowCourseContainer");
            html = $("#HideData .CurrentCourse").clone();

            var courseName = showCourse.NextCourseName;
            if (showCourse.IsCurrent)
                courseName = "当前：" + courseName;
            else
                courseName = "下节课:" + courseName;
            html.find(".CourseName").text(courseName);

            var courseTime = "时间：" + showCourse.NextCourseDate;
            html.find(".CourseTime").text(courseTime);

            var courseStatus = showCourse.UserCourseLogStatusName;
            html.find(".CourseStatus").text("(" + courseStatus + ")");

            var btn = html.find("#btn_Leave");
            if (showCourse.CanLeave) {
                btn.attr("date", showCourse.NextCourseDate);
                btn.on("click", LeaveEvent);
            }
            else
                btn.hide();
            btn = html.find("#btn_Sign");
            if (showCourse.CanSign) {

                btn.attr("date", showCourse.NextCourseDate);
                btn.on("click", SignInEvent);
            }
            else
                btn.hide();
            var skipList = showCourse.CourseSkipList;
            if (skipList.length > 0) {
                var spRoot = html.find(".CourseSkipReason");
                $.each(skipList, function(j)
                {
                    var spData = skipList[j];
                    var spHtml = $("#HideData .ReasonLine");
                    spHtml.text("(" + spData.Date + " | " + spData.CourseName + " 跳过：" + spData.CourseSkipReasonName + ")");
                    spRoot.append(spHtml);
                });
              
            }


            root.append(html);

        }
    }

    LeaveEvent = function () {
        var date = $("#btn_Leave").attr("date");
        window.location.href = "MyLeave?date=" + date;
    }
    SignInEvent = function () {
        var date = $("#btn_Leave").attr("date");
        window.location.href = "MySign?date="+date;
    }
 
    Init();
});