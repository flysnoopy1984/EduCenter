$(function () {

    var SubmitTrialUrl = "ApplyTrial?handler=SubmitTrial";
    Init = function () {
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
        var date = $(".DateInput").text();
        var courseCode = $("#selCourseCode").val();

        if (date != "" && courseCode != "-1") {
            LoadingDone();
        }

   
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