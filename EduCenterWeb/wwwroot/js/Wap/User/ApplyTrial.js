$(function () {

    var SubmitTrialUrl = "ApplyTrial?handler=SubmitTrial";
    Init = function () {

        var phone = $("#hUserPhone");
        if (phone.val() == "" || phone.val() == null) {
            ShowInfo("请先留下您的联系方式，便于工作人员联系您，谢谢！", null, null, -1, function () {
                window.location.href = "/Independent/RegPhone?rurl=/User/ApplyTrial";
            });
        }

        var sysDate = new Date();
        var year = sysDate.getFullYear();
        var month = sysDate.getMonth() + 1;
        var monthName = month;
        if (month < 10)
            monthName = "0" + month;
        var day = sysDate.getDate();

        $(".DateInput").text(year + "-" + monthName + "-" + day);
        laydate.render({
            elem: '.DateInput',
            eventElem: '#btn_DatePick',
            min: 0,
            theme: 'molv',
            trigger: 'click',
            done: function (value, date) {
                //   alert('你选择的日期是：' + value + '\n获得的对象是' + JSON.stringify(date));
                GetTrialList();
            }
        });
        layui.use('form', function () {
            var form = layui.form;
            form.on('select(selCourseCode)', function (data) {

                GetTrialList();
            });
        });

        $("#btn_submitTrial").on("click", SubmitTrial);
        $("#TimeFilterTable tr").on("click", TimeSelectedEvent);
    }

    StartLoading = function () {
        $(".buttonArea").hide();
        $(".TimeFilter").slideUp();
    }

    LoadingDone = function () {
        $(".buttonArea").show();
        $(".TimeFilter").slideDown();
    }

    TimeSelectedEvent = function (e) {
        var obj = e.currentTarget;
        $("#TimeFilterTable tr").css("background-color", "")
        $("#TimeFilterTable").find("input[type=radio]").removeAttr("checked");
        $(obj).find("input[type=radio]").attr("checked", "checked");
        $(obj).css("background-color", "#f3d19c");


    }

   

    GetTrialList = function () {

        StartLoading();

        var dateStr = $(".DateInput").text();
        var courseCode = $("#selCourseCode").val();
        var option = $("#selCourseCode option:selected");
        var ctype = option.attr("cType");
        var date = new Date(dateStr);
        var day = date.getDay();
      
        if ((day == 3 || day == 4) && ctype == "WQ") {
            ShowInfo("周三周四没有围棋试听课");
            return;
        }
        if (ctype == "MS" && (day == 1 || day == 2)) {
            ShowInfo("周一周二没有国画试听课");
            return;
        }
    
        if (dateStr != "" && courseCode != "-1") {

            FilterLesson(day);

            LoadingDone();
        }
    }

    FilterLesson = function (day) {
        $(".TimeFilter .TimeFilterTableTr").show();
        $(".TimeFilter .TimeFilterTableTr").each(function () {
            var lesson = parseInt($(this).children(":first").attr("lesson"));
            if (day >= 1 && day <= 5) {
                if (lesson >= 1 && lesson <= 3)
                    $(this).hide();
            }
            else {
                if (lesson >= 10 && lesson <= 11)
                    $(this).hide();
            }
        });
       
    }

    SubmitTrial = function () {
        var courseCode = $("#selCourseCode").val();
        var date = $(".DateInput").text();
        var Lesson = $(".TimeFilterTableTr input[type=radio]:checked").closest(".TimeFilterTableTr").children(".tdTime").attr("lesson");

        if (date == "") { ShowInfo("请选择日期"); return;}
        if (courseCode == "-1") { ShowInfo("请选择课程"); return; }
        if (Lesson == undefined) { ShowInfo("请选择时间段"); return; }

        var data = {
            "courseCode": courseCode,
            "Lesson": Lesson,
            "date": date,
        }
      
        callAjax_Query(SubmitTrialUrl, data, SubmitTrialCallBack, "", SubmitError)
    }

    SubmitTrialCallBack = function () {
        ShowInfo("申请试听成功！", null, null, 2, function () {
            window.location.href = "MyTrial";
        });     
        
    }

    SubmitError = function (res) {
        if (res.IntMsg == -1) {
            window.location.href = "Login";
        }
    }
        
    Init();

})