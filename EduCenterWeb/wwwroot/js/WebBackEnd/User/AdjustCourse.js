$(function () {
    var QueryUserCourseUrl = "AdjustCourse?handler=QueryUserCourse";
    var GetUserInfoUrl = "AdjustCourse?handler=GetUserInfo";
    var GetCourseScheduleUrl = "AdjustCourse?handler=GetCourseScheduleList";
    var DeleteUserCourseUrl = "AdjustCourse?handler=DeleteUserCourse";
    var SaveUserCourseUrl = "AdjustCourse?handler=SaveUserCourse";
    var QueryUserCourseLogUrl = "AdjustCourse?handler=QueryUserCourseLog";
    var _OptionForm;
    var currentOpenId;

    /*Detail  */
    var glaypage;
    var pageSize = 20;
    var pageIndex = 1;

    GetWeekDayName = function(day)
    {
        var dayname = "";
        switch (day) {
            case 0:
                dayname = "星期日"; break;
            case 1:
                dayname = "星期一"; break;
            case 2:
                dayname = "星期二"; break;
            case 3:
                dayname = "星期三"; break;
            case 4:
                dayname = "星期四"; break;
            case 5:
                dayname = "星期五"; break;
            case 6:
                dayname = "星期六"; break;
        }
        return dayname;
    }
    Init = function () {

        var openId = GetUrlParam("openId");
     
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
        $("#btnNew").on("click", NewCourseEvent);
        if (openId != undefined && openId != "") {
            QueryUserCourse(openId);
        }

        layui.use('laypage', function () {
            glaypage = layui.laypage;
        });
        $("#btnUserCourse").on("click", QueryUserCourseLogEvent);
    }

    //查询用户用课情况
    QueryUserCourseLogEvent = function () {
        if (currentOpenId == undefined || currentOpenId == "" || currentOpenId == null) {
          //  ShowInfo("没有选择用户");
            return;
        }

        aq(QueryUserCourseLogUrl, { "openId": currentOpenId, "pageIndex": pageIndex, "pageSize": pageSize }, function (res) {
            var list = res.List;
            $(".UserCourseLogCourseTable tr:gt(0)").empty();
            $.each(list, function (i) {
                var html = "<tr class='OneCourseLog'>";
                html += "<td>" + list[i].CourseName + "</td>";
                html += "<td>" + list[i].CourseDate + "</td>";
                html += "<td>" + GetWeekDayName(list[i].Day) + "</td>";
                html += "<td>" + list[i].LessonTime + "</td>";
                html += "<td>" + list[i].CourseStatusName + "</td>";
                html += "<td>" + list[i].SignUser + "</td>";
                html += "</tr > ";
                $(".UserCourseLogCourseTable").append(html);
            });

            glaypage.render({
                elem: 'Pager',
                layout: ['prev', 'page', 'next', 'count', 'skip'],
                limit: pageSize,
                count: res.RecordTotal,
                curr: pageIndex,
                jump: function (obj, first) {

                    if (!first) {
                        pageIndex = obj.curr;
                        QueryUserList();

                    }
                    //else
                    //    pageIndex++;
                }
            });

        });

    }

    QueryUserCourse = function (openId) {
        currentOpenId = openId;

        aq(GetUserInfoUrl, { "openId": openId }, function (res) {
            var data = res.Entity;
            $(".MemberType").text(data.MemberTypeName);
            $(".RemainStd").text(data.RemainStd);
            $(".RemainSummer").text(data.RemainSummer);
            $(".RemainWinter").text(data.RemainWinter);
        });

        aq(QueryUserCourseUrl, { "openId": openId }, function (res) {
            $(".CourseTable tr:gt(0)").empty();
            var data = res.List;
            $.each(data, function (i) {
                NewCourse(data[i]);

            })
        })

        QueryUserCourseLogEvent();
    }

    NewCourseEvent = function () {
        NewCourse();
    }

    //创建新课程
    NewCourse = function (data) {
        var html = $("#hideData #TableData tr").clone();

        var d = new Date();
        var year = d.getFullYear();  //得到今年

        var selYear = html.find(".sel_Year");
        selYear.on("change", selYearEvent);

        var selCourseScheduleType = html.find(".sel_CourseScheduleType");
        selCourseScheduleType.on("change", selCourseScheduleTypeEvent);

        var selDay = html.find(".sel_Day");
        selDay.on("change", selDayEvent);

        var selLesson = html.find(".sel_Lesson");
        selLesson.on("change", selLessonEvent);

        var selCourse = html.find(".sel_Course");
     
        if (data != undefined && data != null) {
            html.attr("usercourseId", data.UserCourseId);
            selYear.val(data.Year);
            selCourseScheduleType.val(data.CourseScheduleType);
            selDay.val(data.Day);
            selLesson.val(data.Lesson);

            FillInSelCourse(selCourse, data.ScheduleList);

            if (data.ScheduleList.length>0)
            selCourse.val(data.LessonCode);
        }
        else {
            selYear.val(year);
        }

        var delBtn = html.find("#btnDel");
        delBtn.on("click", DelUserCourse);

        var saveBtn = html.find("#btnSave");
        saveBtn.on("click", SaveUserCourse);
        
        $(".CourseTable").append(html);
    }
    //选择年
    selYearEvent = function (e) {
        var root = $(e.currentTarget).closest(".OneCourse");
        root.find(".sel_CourseScheduleType").val("-1");
        root.find(".sel_Day").val("-1");
        root.find(".sel_Lesson").val("-1");
        root.find(".sel_Course").val("-1");
    }

    selCourseScheduleTypeEvent = function (e) {
        var root = $(e.currentTarget).closest(".OneCourse");
        root.find(".sel_Day").val("-1");
        root.find(".sel_Lesson").val("-1");
        root.find(".sel_Course").val("-1");

    };
    selDayEvent = function (e) {
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
    selLessonEvent = function (e) {
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
            
                var list = res.List;
                FillInSelCourse(selCourse, list);
        })
     

    }

    //填充课程下拉框
    FillInSelCourse = function (selCourse,list) {
        selCourse.empty();
        if (list.length == 0) {
            selCourse.append("<option value=-1>没有课程</option>");
        }
        else {
            $.each(list, function (i) {
                selCourse.append("<option value=" + list[i].LessonCode + ">" + list[i].CourseName + "</option>");
            })
        }
    }

    GetCourseFullName = function (root) {
        var name = "["+root.find(".sel_Year option:selected").text()+"]";
        name += "[" +root.find(".sel_CourseScheduleType option:selected").text() + "]";
        name += "[" +root.find(".sel_Day option:selected").text() + "]";
        name += "[" +root.find(".sel_Lesson option:selected").text() + "]";
        name += "[" +root.find(".sel_Course option:selected").text() + "]";
        return name;
    }
    //删除用户课程
    DelUserCourse = function (e) {
      
        var root = $(e.currentTarget).closest(".OneCourse");
        var lessonCode = root.find(".sel_Course").val();
        if (lessonCode != "-1") {
            var fullName = GetCourseFullName(root);
            ShowConfirm("您将删除:  " + fullName + "  ,是否继续？", null, null, function () {
                DoDelUserCourse(root,currentOpenId, lessonCode);
            })
        }
        else {
            DoDelUserCourse(root,currentOpenId, -1);
        }
    }

    DoDelUserCourse = function (root,openId,lessonCode) {

        if (lessonCode == -1) {
            root.remove();
        }
        else {
            aq(DeleteUserCourseUrl, {
                "openId": openId,
                "LessonCode": lessonCode
            }, function (res) {
                root.remove();
            });
        }
      
    }

    SaveUserCourse = function (e) {
        var root = $(e.currentTarget).closest(".OneCourse");
        if (root.find(".sel_CourseScheduleType").val() == -1) {
            ShowInfo("请选择课程类型"); return;
        }
        if (root.find(".sel_Day").val() ==  -1) {
            ShowInfo("请选择上课日"); return;
        }
        if (root.find(".sel_Lesson").val() == -1) {
            ShowInfo("请选择时间段"); return;
        }
        if (root.find(".sel_Course").val() == -1) {
            ShowInfo("请选择课程"); return;
        }

        var fullName = GetCourseFullName(root);
        ShowConfirm("您将为用户选择:  " + fullName + "  ,是否继续？", null, null, function () {
            DoSaveUserCourse(root,currentOpenId,
                root.find(".sel_Course").val(),
                root.attr("usercourseId"),
                root.find(".sel_CourseScheduleType").val());
        })
    }
    DoSaveUserCourse = function (root,openId, lessonCode, origUserCourseId, courseScheduleType) {
        aq(SaveUserCourseUrl, {
            "openId": openId,
            "lessonCode": lessonCode,
            "origUserCourseId": origUserCourseId,
            "courseScheduleType": courseScheduleType

        }, function (res) {
                var data = res.Entity;
                root.attr("usercourseId", data.Id);
                ShowInfo("保存成功", null, null, 1);
        });
    }


    Init();
})