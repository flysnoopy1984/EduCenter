$(function () {
    var InitPageUrl = "MySign?handler=InitPage";
    Init = function () {

        //callAjax_Query(InitPageUrl, {}, InitPageCallBack, "", function (res) {
        //    if (res.IntMsg == -1)
        //        window.location.href = "Login";
        //});
        $(".UnSignBtn").on("click", SignUp)
    //    $("#HideData ")
    };

    InitPageCallBack = function (res) {
        var list = res.List;
        var data = list[0];

        var title = $(".Title");
        var html;
        if (!data.CanSign) {
            html = $("#HideData .NoCourse").clone();
            html.find(".CourseName").text("[" + data.CourseScheduleTypeName + "] " + data.CourseName);
            html.find(".CourseDate").text(data.CourseDate + " " + data.StartTime);
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
                html = $("#HideData .UnSignArea .PreSign").clone();
                html.find(".CourseName").text("[" + data.CourseScheduleTypeName + "] " + data.CourseName);
                html.find(".CourseDate").text(data.CourseDate + " " + data.StartTime);


            });
            
        }
    }


    SignUp = function (e) {
        var obj = e.currentTarget;
        $(obj).removeClass("btn-info");
        $(obj).addClass("btn-block");
        $(obj).addClass("disabled");
       
        $(obj).find("i").removeClass("fa-sign-in");
        $(obj).find("i").addClass("fa fa-check-circle");
        $(obj).find(".CourseStatus").text("您已签到");

        $(obj).off("click");
      
    }

    Init();
});