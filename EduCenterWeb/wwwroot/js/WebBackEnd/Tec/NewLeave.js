$(function () {
    var SubmitTecLeaveUrl = "NewLeave?handler=SubmitTecLeave";
    var QueryLessonListUrl = "NewLeave?handler=QueryLessonList";
    Init = function () {
        var date = getDateStr(new Date());
       


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
        $("#btn_SubmitLeave").on("click", SubmitNewLeave);
        $("#selTecCode").on("change", TecSelect)
    }

    LayDataSelect = function (date) {
        var tecCode = $("#selTecCode").val();

        if (tecCode != -1) {
            QueryLessonList(tecCode,date);
        }
    }

    TecSelect = function () {
        var tecCode = $("#selTecCode").val();
        var date = $(".StartDateInput").text();
        QueryLessonList(tecCode, date);
    }

    QueryLessonList = function (tecCode, date) {
       

        var data = {
            "tecCode": tecCode,
            "date":date,
        }
        StartQuery();

        callAjax_Query(QueryLessonListUrl, data, QueryLessonListCallBack, "");
    }

    QueryLessonListCallBack = function (res) {
        var data = res.List;
        if (data.length == 0) {
           
            $(".NoLesson").slideDown();
        }
        else {
            $(".LessonList").find(".checkbox:not(:first)").remove();
            var html = $("#HideData .checkbox").clone();

            $(".LessonList").slideDown();
        }
    }
    StartQuery = function () {
        $(".LessonList").hide();
        $(".NoLesson").hide();
    }
    ShowData = function () {

    }

    SubmitNewLeave = function () {

    }


    Init();
})