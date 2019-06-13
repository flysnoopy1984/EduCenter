$(function () {

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

        if (date != "" && courseCode != "") {
            LoadingDone();
        }

        //if (date == "") { ShowInfo("请选择日期"); return;}
        //if (courseCode == "") { ShowInfo("请选择课程"); return; }



    }
    Init();
   
})