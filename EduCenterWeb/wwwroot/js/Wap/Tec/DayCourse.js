$(function () {
    var QueryTecDayCourseUrl = "DayCourse?handler=QueryTecDayCourse";
    var QueryUserCourseUrl = "DayCourse?handler=QueryUserCourse";

    Init = function () {
        laydate.render({
            elem: '.DateInput',
            eventElem: '#btn_DatePick',
            trigger: 'click',
            theme: 'grid',
            done: function (value, date) {
                //   alert('你选择的日期是：' + value + '\n获得的对象是' + JSON.stringify(date));
                QueryTecDayCourse(value);
            }
        });
        var date = $(".DateInput").text();
        QueryTecDayCourse(date);

    }

    QueryTecDayCourse = function (date) {
        callAjax_Query(QueryTecDayCourseUrl, {"date":date}, QueryTecDayCourseCallBack);
    }

    QueryTecDayCourseCallBack = function (res) {
        var data = res.List;
        var root = $(".AllCourse");
        root.empty();

        if (data.length == 0) {
            ShowInfo("没有课程");
            return;
        }
        $.each(data, function (i) {
            var html = $("#HideData .OneCourseData").clone();
            var startTime = data[i].TimeRange.substring(0, 5);
            html.find(".CouseInfo").text("【" + data[i].CourseName + "】 " + startTime + " 开始")
            var switchDetail = html.find(".SwitchDetail");
            switchDetail.attr("lCode", data[i].LessonCode);
            switchDetail.attr("date", $(".DateInput").text());
            switchDetail.on("click", SwitchDetail);
            root.append(html);
        });
    }

    SwitchDetail = function (e) {

        var barIcon = $(e.currentTarget).children("i");
        var oneCourse = $(e.currentTarget).closest(".OneCourseData");
        var userList = oneCourse.find(".UserInfoList");
        if (barIcon.hasClass("fa-caret-left")) {

            var lessonCode = $(e.currentTarget).attr("lCode");
            var date = $(e.currentTarget).attr("date");
            userList.empty();

            callAjax_Query(QueryUserCourseUrl, { "lessonCode": lessonCode, "date": date }, function (res) {
                //Icon图标变换
                barIcon.removeClass("fa-caret-left");
                barIcon.addClass("fa-caret-down");
                userList.slideDown();
                //userData
                var data = res.List;
                $.each(data, function (i) {
                    var html = $("#HideData .OneUser").clone();
                    html.find(".UserName").text("学生:"+data[i].UserName);
                    var userStatus = html.find(".UserStatus");
                    userStatus.text("状态:" + data[i].UserCourseLogStatusName);

                    if (data[i].UserCourseLogStatus == 1) userStatus.addClass("text-danger");
                    if (data[i].UserCourseLogStatus == 10) userStatus.addClass("text-success");
                    if (data[i].UserCourseLogStatus == 1 || data[i].UserCourseLogStatus == 3)
                        userStatus.addClass("text-warning");
                    
                    userList.append(html);
                });

                var btn = $("#HideData .btnRefresh").clone();
                btn.on("click", RefreshUserData);
                userList.append(btn); 
            });
        }
        else {
            barIcon.removeClass("fa-caret-down");
            barIcon.addClass("fa-caret-left");
            userList.slideUp();
        }
    }

    GetUserData = function () {

    }

    RefreshUserData = function () {
        ShowInfo("还没开发好，您可选择日期再次查询！");
    }

    Init();
  
})