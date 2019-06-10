$(function () {
    var QueryOneDayCourseUrl = "CoursingDay?handler=QueryOneDayCourse";
   var QueryUserCourseUrl = ""

    Init = function () {

        laydate.render({
            elem: "#selDate",
            done: LayDataSelect
        });
        var DateFromUrl = GetUrlParam("date");
        var tecCodeFromUrl = GetUrlParam("tecCode");
        if (DateFromUrl != undefined) {
            $("#selTecCode").val(tecCodeFromUrl);
            $("#selDate").val(DateFromUrl);
        }
      
        QueryOneDayCourse();
   
    }

    LayDataSelect = function () {
        QueryOneDayCourse();
    }

    QueryOneDayCourse = function () {
        var date = $("#selDate").val();
        var tecCode = $("#selTecCode").val();
        var data = { "tecCode": tecCode, "date": date }; 

        callAjax_Query(QueryOneDayCourseUrl, data, QueryOneDayCourseCallBack);
    }

    QueryOneDayCourseCallBack = function (res) {
        var data = res.List;

        $.each(data, function (i) {
            var tc = data[i];
            var html = $("#HideData .CellOneCourse").clone();
            html.children(".cellCourseName").text(tc.CourseName);

            var tecName = $("#selTecCode option[value=" + tc.TecCode + "]").text();
            html.children(".cellCourseTec").text(tecName);
            html.children(".cellCourseStatus").text(tc.CoursingStatusName);

            html.on("click", { "LessonCode": tc.LessonCode}, QueryUserCourseList)
            $(".CellContainer[lesson=" + tc.Lesson + "]").append(html);
        });
    }
    QueryUserCourseList = function (e) {
        var lessonCode = e.data.LessonCode;
        var date = $("#selDate").val();
    }

    QueryUserCourseListCallBack = function () {

    }

    Init();
});