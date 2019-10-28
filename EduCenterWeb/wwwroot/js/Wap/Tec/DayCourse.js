$(function () {
    var QueryTecDayCourseUrl = "DayCourse?handler=QueryTecDayCourse";
    var QueryUserCourseUrl = "DayCourse?handler=QueryUserCourse";
    var SignForUserUrl = "DayCourse?handler=SignForUser";

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

        //用户请假微信消息
        var queryDate = GetUrlParam("date");
        if (queryDate != undefined) {
            date = queryDate;
            $(".DateInput").text(date);
        }
        
        QueryTecDayCourse(date);

        $(".btnRefresh").on("click", RefreshUserData);
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
                    html.find(".UserName").text("学生:" + data[i].ChildName);

                //    html.find(".RemainTime").text("剩余课时:[" + data[i].UserName+"]小时");

                    var userStatus = html.find(".UserStatus");
                    userStatus.text("状态:" + data[i].UserCourseLogStatusName);

                    if (data[i].UserCourseLogStatus == 1) userStatus.addClass("text-danger");
                    if (data[i].UserCourseLogStatus == 10) userStatus.addClass("text-success"); //已签到

                    if (data[i].UserCourseLogStatus == 2 || data[i].UserCourseLogStatus == 3)
                        userStatus.addClass("text-warning");

                    var btnSign = html.find("#btnSignForUser");
                    if (data[i].UserCourseLogStatus == 1) {
                        btnSign.on("click", {
                            "openId": data[i].UserOpenId,
                            "memberType": data[i].MemberType,
                            "lessonCode": data[i].LessonCode
                        }, SignForUser);
                    }
                    else {
                        btnSign.hide();
                    }
                 
                  
                    
                    userList.append(html);
                });

            
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
        var date = $(".DateInput").text();
        QueryTecDayCourse(date);
    }

    SignForUser = function (e) {
        var date = $(".DateInput").text();
        var openId = e.data.openId;
        var memberType = e.data.memberType;
        var lessonCode = e.data.lessonCode;

        aq(SignForUserUrl,
            {
                "openId": openId,
                "lessonCode": lessonCode,
                "memberType": memberType,
                "date": date,
            },
            function (res) {
                var OneUser = $(e.currentTarget).closest(".OneUser");
                var userStatus = OneUser.find(".UserStatus");
                var btnSign = OneUser.find("#btnSignForUser");
                btnSign.hide();
                userStatus.text("状态:" + res.SuccessMsg);
                userStatus.addClass("text-success");

                ShowInfo("签到完成", null, null, 1);
            },
          )
    }

    Init();
  
})