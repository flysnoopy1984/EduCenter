$(function () {
    var QueryOneDayCourseUrl = "CoursingDay?handler=QueryOneDayCourse";
    var QueryUserCourseUrl = "CoursingDay?handler=QueryUserCourse";

    Init = function () {

        var sysDate = new Date();
        var year = sysDate.getFullYear();
        var month = sysDate.getMonth() + 1;
        var monthName = month;
        if (month < 10)
            monthName = "0" + month;
        var day = sysDate.getDate();

        $("#selDate").text(year + "-" + monthName + "-" + day);

        laydate.render({
            elem: "#selDate",
            theme: 'molv',
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

            html.on("click", {"LessonCode": tc.LessonCode}, QueryUserCourseList)
            $(".CellContainer[lesson=" + tc.Lesson + "]").append(html);
        });
    }
    QueryUserCourseList = function (e) {
        var lessonCode = e.data.LessonCode;
        var date = $("#selDate").val();
        var data = {
            "lessonCode": lessonCode,
            "date": date,
        }
        callAjax_Query(QueryUserCourseUrl, data, QueryUserCourseListCallBack);
    }

    QueryUserCourseListCallBack = function (res) {
        var data = res.List;
        var root = $("#UserTable");
        root.children(".UserTableRow").remove();
        $.each(data, function (i) {
            var item = data[i];
            var html = $("#HideData .UserTableRow").clone();

            html.children(".UserName").text(item.UserName);
            html.children(".CourseStatus").text(item.UserCourseLogStatusName);
            html.children(".SignDate").text(item.SignDateTime);
            root.append(html);
        });


    }

    Init();
});