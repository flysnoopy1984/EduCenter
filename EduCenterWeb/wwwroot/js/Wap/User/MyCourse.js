$(function () {
    var carousel = null;
    var InitDataUrl = "MyCourse?handler=InitData";
    ShowInfo = function (msg) {
        $.alert({
            title: "消息",
            content: msg,
            type:"red",

        });
    }
    Init = function () {
        
        $("#btn_Leave").on("click", LeaveEvent);
        $("#btn_Sign").on("click", SignInEvent);

        callAjax_Query(InitDataUrl, {}, InitDataCallBack, "", function (res) {
            if (res.IntMsg == -1)
                window.location.href = "Login";
        });
    
    };

    InitDataCallBack = function (res) {
        var data = res.List;

        if (data.length == 0) {
            if (res.IntMsg == 0 || res.IntMsg == 10) {
                ShowInfo("您还没有选择每周课程", null, null, 2, function () {
                    window.location.href = "Apply";
                });
            }
            else {
                ShowInfo("您还没有选择假期课程", null, null, 2, function () {
                    window.location.href = "ApplyWinterSummer?type=" + res.IntMsg;
                });
            }
        }
        else {
            $.each(data, function (i) {
                var uc = data[i];
                var td = $("#CourseTable tr td[day=" + uc.Day + "][lesson=" + uc.Lesson + "]");
                var item = $("#HideData .CourseCellContainer").clone();
                item.find(".CourseCell").text(uc.CourseName);
                td.append(item);
            });
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