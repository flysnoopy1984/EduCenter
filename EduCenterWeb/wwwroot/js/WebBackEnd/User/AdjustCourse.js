$(function () {
    var QueryUserCourseUrl = "AdjustCourse?handler=QueryUserCourse";

    var GetCourseScheduleUrl = "AdjustCourse?handler=GetCourseScheduleList";
    var _OptionForm;
    var openId;
    var CourseScheduleData = null;

    Init = function () {

        openId = GetUrlParam("openId");
     
        layui.use('form', function () {
            _OptionForm = layui.form;
            if (openId != undefined && openId != "") {
                _OptionForm.val("OptionBar", {
                    "selectMember": openId
                });
            }
            _OptionForm.on('select(selectMember)', function (data) {
                QueryUserCourse(data.value);
            });
          
        });
        $("#btnNew").on("click", NewCourse);
       // $(".sel_CourseScheduleType")
    }

    QueryUserCourse = function (openId) {
        aq(QueryUserCourseUrl, { "openId": openId }, function (res) {
            var data = res.List;
            $.each(data, function (i) {


            })
        })
    }

    NewCourse = function () {
        var html = $("#hideData #TableData tr").clone();
        var d = new Date();
        var year = d.getFullYear();  //得到今年

        html.find(".sel_Year").val(year);
        html.find(".sel_Year").on("change", selYear);
        html.find(".sel_CourseScheduleType").on("change", selCourseScheduleType);
        html.find(".sel_Day").on("change", selDay);
        html.find(".sel_Lesson").on("change", selLesson);
        html.find(".sel_Course").on("change", selCourse);


        $(".CourseTable").append(html);
    }
    selYear = function (e) {
        var root = $(e.currentTarget).closest(".OneCourse");
        root.find(".sel_CourseScheduleType").val("-1");
        root.find(".sel_Day").val("-1");
        root.find(".sel_Lesson").val("-1");
        root.find(".sel_Course").val("-1");
    }

    selCourseScheduleType = function (e) {
        var root = $(e.currentTarget).closest(".OneCourse");
        root.find(".sel_Day").val("-1");
        root.find(".sel_Lesson").val("-1");
        root.find(".sel_Course").val("-1");

    };
    selDay = function (e) {
        var root = $(e.currentTarget).closest(".OneCourse");
        var csType = root.find(".sel_CourseScheduleType");
        if (csType.val() == -1) {
            ShowInfo("请选择课程类型");
            root.find(".sel_Day").val(-1);
            return;
        }

        var selLesson = root.find(".sel_Lesson");
        if (selLesson.val() != -1) {
            GetCourseSchedule(root);
        }

        
    };
    selLesson = function (e) {
        var root = $(e.currentTarget).closest(".OneCourse");
        var csType = root.find(".sel_CourseScheduleType");
        if (csType.val() == -1) {
            ShowInfo("请选择课程类型");
            root.find(".sel_Lesson").val(-1);
            return;
        }
        var selDay = root.find(".sel_Day");
        if (selDay.val() == -1) {
            ShowInfo("请选择上课日");
            root.find(".sel_Lesson").val(-1);
            return;
        }

        GetCourseSchedule(root);
    };
    selCourse = function (e) { };

    GetCourseSchedule = function (root) {
        var year = root.find(".sel_Year").val();
        var courseScheduleType = root.find(".sel_CourseScheduleType").val();
        var day = root.find(".sel_Day").val();
        var lesson = root.find(".sel_Lesson").val();
        var selCourse = root.find(".sel_Course");
        aq(GetCourseScheduleUrl,
            {
                "year": year,
                "day": day,
                "lesson": lesson,
                "courseScheduleType": courseScheduleType
            }, function (res) {
                selCourse.empty();
                var list = res.List;
                if (list.length == 0) {
                    selCourse.append("<option value=-1>没有课程</option>");
                }
                else {
                    $.each(list, function (i) {
                        selCourse.append("<option value=" + list[i].LessonCode + ">" + list[i].CourseName+"</option>");
                    })
                }
        })
     

    }


    Init();
})