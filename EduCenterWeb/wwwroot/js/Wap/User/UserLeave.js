$(function () {
    var userCourseUrl = "MyLeave?handler=GetCourseByDate";
    var submitLeaveUrl = "MyLeave?handler=CourseLeave";

    Init = function () {
      
        $("#btn_SubmitLeave").on("click", SubmitLeave);

        laydate.render({
            elem: '.DateInput',
            eventElem: '#btn_DatePick',
            min:0,
            trigger: 'click',
            theme: 'molv',
            done: function (value, date) {
             //   alert('你选择的日期是：' + value + '\n获得的对象是' + JSON.stringify(date));
                QueryCourse(value);
;            }
        });

        var date = GetUrlParam("date");
        if (date != undefined && date !="") {
            $(".DateInput").text(date);
            QueryCourse(date);
        }
 
    }

    QueryCourse = function (date) {
        ShowLoading();

        callAjax_Query_NoBlock(userCourseUrl, { "date": date }, GetCourseByDateCallBack, true, GetCourseByDateError);
    }

 
    SubmitLeave = function () {
        var list = new Array();
        $(".LeaveList .UserOption input[type=checkbox]:checked").each(function () {
            var root = $(this).closest(".Course");

            var course = new Object();
            course.LessonCode = root.attr("lCode");
            course.CourseScheduleType = root.attr("csType");
            course.CourseDateTime = $(".DateInput").text();
            course.UserCourseLogStatus = root.attr("cStatus");
            list.push(course);
            
        });
        if (list.length == 0) {
            ShowInfo("请先选择课程");
        }
        else {
            callAjax(submitLeaveUrl, { "list": list }, SubmitLeaveCallBack,"已获知您的请假申请");
        }

    }


    ShowLoading = function ()
    {
        $(".LoadingArea").css("display","flex");
        $(".LeaveArea").hide();
        $(".ButtonArea").hide();
        
    }
    LoadingDone = function () {
        $(".LeaveArea").show();
        $(".LoadingArea").hide();
       
    }

    SubmitLeaveCallBack = function (result) {
        var date = $(".DateInput").text();
        window.location.href = "MyLeave?date=" + date;
    }

    GetCourseByDateCallBack = function (result) {
        var list = result.List;
        var root = $(".LeaveList");
        root.empty();

        if (list.length == 0) {
            ShowInfo("当天没有课程，请重新选择日期", "");

        }
        else {

            var cbNum = 0; //当remove 的 checkbox和课程数相同，说明没有可请假的课
            $.each(list, function (i) {
                var c = list[i];

                var html = $("#HideData .Course").clone();
                html.attr("lCode", c.LessonCode);
                html.attr("csType", c.CourseScheduleType);
                html.attr("cStatus", c.UserCourseLogStatus);
                html.find(".CourseName").text(c.CourseName);
                html.find(".CourseStatus").text(c.UserCourseLogStatusName);
                html.find(".CourseTime").text(c.CourseTime);

                if (c.UserCourseLogStatus == 1) {
                    var cbId = "cb_" + i;
                    var inputObj = html.find("input[type=checkbox]").attr("Id", cbId);

                    inputObj.next().attr("for", cbId);

                }
                else {
                    html.find(".UserOption").empty();
                    cbNum++;
                }
                root.append(html);
            });
            if (cbNum < list.length)
                $(".ButtonArea").show();
        }


        LoadingDone();
    }

    GetCourseByDateError = function (res) {
        LoadingDone();
        if (res.IntMsg = -1) {
            window.location.href = "Login";
        }
        else
            ShowError(res.ErrorMsg);
        // $(".LeaveList").text(errorMsg);
    }


    Init();
})