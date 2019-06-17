$(function () {
    var SubmitTecLeaveUrl = "NewLeave?handler=SubmitTecLeave";
    var QueryLessonListUrl = "NewLeave?handler=QueryLessonList";
    Init = function () {
        var sysDate = new Date();
     
        var tdate = new Date(sysDate)
        tdate.setDate(tdate.getDate() + 2);

        var date = getDateStr(tdate);
        laydate.render({
            elem: ".StartDateInput",
            eventElem: '#btn_StartDatePick',
            done: LayDataSelect,
            type: 'date',
            theme: 'molv',
            min:2,
            value: date,
            isInitValue: false
        });
        $("#btn_submitLeave").on("click", SubmitTecLeave);
        $("#selTecCode").on("change", TecSelect)
        $("#btn_selectAll").on("click", SelectAllLesson);
        
    }

    LayDataSelect = function (date) {
        var tecCode = $("#selTecCode").val();
        QueryLessonList(tecCode, date);
       
    }

    TecSelect = function () {
        var tecCode = $("#selTecCode").val();
        var date = $(".StartDateInput").text();
        QueryLessonList(tecCode, date);
    }

    QueryLessonList = function (tecCode, date) {

        if (tecCode != -1 && date!="") {
            var data = {
                "tecCode": tecCode,
                "date": date,
            }
            StartQuery();

            callAjax_Query(QueryLessonListUrl, data, QueryLessonListCallBack, "");
        }
     
    }

    QueryLessonListCallBack = function (res) {
        var data = res.List;
        if (data.length == 0) {
           
            $(".NoLesson").slideDown();
        }
        else {
            $(".LessonList").find(".checkbox").remove();
            $.each(data, function (i) {
                var item = data[i];
                var html = $("#HideData .checkbox").clone();
                var id = "cb_" + item.Lesson;

                var inputObj = html.find("input");
                inputObj.attr("id", id);
                inputObj.attr("tcId",item.Id);
                if (item.CoursingStatus == 1) {
                    inputObj.attr("checked", "checked");
                    inputObj.attr("disabled", "true");
                }
                var labelObj = html.find("label");
                labelObj.attr("for", id);
                labelObj.text(item.CourseName + " | " + item.TimeRange)
                $(".LessonList #btn_selectAll").after(html);
            });
            //是否没有可选的时间段
            var canselected = $(".LessonList input[type=checkbox]:checked").length;
            if (canselected == data.length) {
                $("#btn_submitLeave").attr("disabled","disabled");
                $("#btn_submitLeave").val("课程已都请假");
            }
           
            $(".LessonList").slideDown();
        }
    }

    SelectAllLesson = function () {
        //   $(".LessonList input[type=checkbox]").removeAttr("checked");
        $(".LessonList input[type=checkbox]").attr("checked", "checked");

     //  $(".LessonList label").append(":after");
    }

    StartQuery = function () {
        $(".LessonList").hide();
        $(".NoLesson").hide();
        $("#btn_submitLeave").removeAttr("disabled");
        $("#btn_submitLeave").val("提交请假");
    }

    ShowData = function () {

    }

    SubmitTecLeave = function () {
      
        var list = new Array();

        $(".LessonList input[type=checkbox]:checked:not(:disabled)").each(function () {
            var Id = $(this).attr("tcId");
            list.push(Id);
        });
      

        if (list.length == 0) {
            ShowInfo("请先选择请假时间段", null, null, 1);
        }
        else
            callAjax_Query(SubmitTecLeaveUrl, { "list": list }, SubmitTecLeaveCallBack, "");
    }

    SubmitTecLeaveCallBack = function (res) {

        ShowInfo("请假成功", null, null, 0, function(){

            parent.CloseNewLeave();
        });
        
    }


    Init();
})