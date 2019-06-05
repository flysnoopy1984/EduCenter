$(function () {
    var InitUrl = "Apply?handler=InitData";
  
    var CourseScheduleData = null;
    var CourseTime = null;
    var selDay = null
    var selLesson = null;
    var selCode = null;

    Init = function () {
        var times = $("#GridWeek tr").length ;
        CourseScheduleData = new Object();
        for (var i = 1; i < 8; i++) {
            CourseScheduleData[i] = new Object();
            for (var j = 1; j < times; j++) {
                CourseScheduleData[i][j] = new Array();
            }
        }

        $("#btnConfirm").on("click", NextStep);
        callAjax_Query(InitUrl, {}, InitCallBack,"");
    }



    selectCourse = function (obj) {
        var cell = $(obj);
        selDay = cell.attr("day");
        selLesson = cell.attr("lesson");

        var csList = CourseScheduleData[selDay][selLesson];

        var selectedLi = $(".SelectedCourseItems .Item .SelectCourseItemContent ul li");

        //原来CheckBox都选，现在单选可以修改代码。
    //    var selectedCode = new Array();
        $.each(selectedLi, function () {
            var day = $(this).attr("day");
            var les = $(this).attr("lesson");
            if (day == selDay && les == selLesson) {
                selCode = $(this).closest(".Item").find(".SelectCourseItemTitle").attr("CourseCode");
                return false;
             
            }
        })
        var html = '<div class="CourseSelect">';
        html += CreateConfirmItemsHtml(-1, null);
        $.each(csList, function (i) {
            var item = csList[i];
            html += CreateConfirmItemsHtml(i, item);
        });
        html += '</div>';

        $.confirm({
            title: '选择课程',
            content: html,
            type: 'red',
            buttons: {
                cancel: {
                    text: "换一天",
                    btnClass: 'btn-info'
                },
                OK: {
                    text: '确认选择',
                    btnClass: 'btn-primary',
                    action: function () {
                        $(".CourseSelect").find("input[type=radio]:checked").each(function () {
                            var checkedNo = $(this).val();
                            var checkeditem = csList[checkedNo];
                            CreateSelectCourseInfo(checkeditem, checkedNo);

                        })
                    }
                }
            }
        });
    };


    //弹出框的课程信息
    CreateConfirmItemsHtml = function (i, item) {
        var id = "cb_" + i;
     //   var item = csList[i];
        var checked = "";
        var lbcss = "list-group-item-info";
        var name = "无";
        var CourseType = -1;

        if (i != -1) {
            if (item.CourseCode == selCode)
                checked = "checked";

            name = item.CourseName;
            CourseType = item.CourseType;
        }
        else
            checked = "checked";
      
        lbcss = GetItemStyleByType(CourseType, "label");
        var cbcss = "";
        cbcss = GetItemStyleByType(CourseType, "cb");


        var html = "";
       
        html += '<div class="radio ' + cbcss + '">';
        html += '<input type="radio" name="AvaliableCourse" value="' + i + '"  id="' + id + '" ' + checked + '>';
        html += '<label for="' + id + '" class="' + lbcss + '">' + name + '</label>';
        html += '</div>';
        return html;
    }


    //主页面的课程信息
    CreateSelectCourseInfo = function (data, selectNo) {

        if (selectNo == -1) {
            if (selCode != null && selCode != "") {
                DeleteSelectCourseInfo(selDay, selLesson, selCode);
            }
            return;
        };
        //Grid TD 信息
        var gridRoot = null;
        if (data.Day >= 1 && data.Day <= 5) gridRoot =$("#GridNormal");
        else gridRoot = $("#GridWeek"); 

        var td = gridRoot.find(".CellContainer[day=" + data.Day + "][lesson=" + data.Lesson + "]");
        td.empty();
        var gl = this.GetItemStyleByType(data.CourseType,"label");
        var tdHtml = $("#HideData .CellCourse").clone();
        tdHtml.addClass(gl);
        tdHtml.find(".cellName").text(data.CourseName);
        td.append(tdHtml);

        //底部信息
        var root = $(".SelectedCourseItems");
        var item = root.find(".Item .SelectCourseItemTitle[CourseCode=" + data.CourseCode + "]");
        var ul;
        if (item.length == 0) {
            var html = $("#HideData .Item").clone();
            html.find(".SelectCourseItemTitle").text(data.CourseName);
            html.find(".SelectCourseItemTitle").attr("CourseCode", data.CourseCode);
            ul = html.find(".SelectCourseItemContent ul");
            root.append(html);
        }
        else
            ul = item.next().find("ul");

        var li = ul.find("li[lesson=" + data.Lesson + "]");
        if (li.length == 0) {
            li = $("#HideData #liData").clone();

            var dayName = GetDayName(data.Day);
            li.attr("lesson", data.Lesson);
            li.attr("day", data.Day);
            li.attr("lcode", data.LessonCode);
            li.find(".SelectCourseTime").text(dayName + " | " + CourseTime[data.Lesson]);
            li.find(".btnCourseItemClose").on("click", { "courseschedule": data }, DeleteSelectCourseInfoEvent);
            ul.append(li);
        }
    };

    DeleteSelectCourseInfoEvent = function (e) {
        var courseschedule = e.data.courseschedule;
     
        DeleteSelectCourseInfo(courseschedule.Day, courseschedule.Lesson, courseschedule.CourseCode);
    }

    DeleteSelectCourseInfo = function (day, lesson, CourseCode) {

        //底部Info
        var course = $(".SelectedCourseItems .Item .SelectCourseItemTitle[CourseCode=" + CourseCode + "]");
        if (course.length > 0) {
            var ul = course.next().find("ul");
            var childnum = ul.children("li").length;
            var item = ul.find("li[day=" + day + "][lesson=" + lesson + "]");
            if (item.length) {
                item.remove();
                if (childnum == 1) {
                    course.parent().remove();
                }
            }   
        }

        //Grid Td 信息
        var gridRoot = null;
        if (day >= 1 && day <= 5) gridRoot = $("#GridNormal");
        else gridRoot = $("#GridWeek");

        var td = gridRoot.find(".CellContainer[day=" + day + "][lesson=" + lesson + "]");
        td.empty();
        var tdHtml = $("#HideData .noCourse").clone();
       
        td.append(tdHtml);

    }

    GetDayName = function (day) {
        switch (day) {
            case 1: return "周一";
            case 2: return "周二";
            case 3: return "周三";
            case 4: return "周四";
            case 5: return "周五";
            case 6: return "周六";
            case 7: return "周日";
        }
        return "";
    }

    //label cb
    GetItemStyleByType = function (cType, ctrl) {
       
        var gl = "list-group-item-default";
        var g2 = "radio-default";
        switch (cType) {

            case 2:
                gl = "list-group-item-warning";
                g2 = "radio-warning";
                 
                break;
            case 1:
                gl = "list-group-item-danger";
                g2 = "radio-danger";
                break;
            case 3:
                gl = "list-group-item-success";
                g2 = "radio-success";
                break;
        }
        if (ctrl == "label")
            return gl;
        else
            return g2;

    }

    NextStep = function () {

        var jsonObj = [];
       
        $(".SelectedCourseItems .Item").each(function () {
            var item = $(this);
            var courseCode = item.find(".SelectCourseItemTitle").attr("CourseCode");
            var list = item.find(".SelectCourseItemContent ul li");
            list.each(function () {
                var li = $(this);
                var day = li.attr("day");
                var lesson = li.attr("lesson");
                var lcode = li.attr("lcode");

                var itemObj = {
                    "courseCode": courseCode,
                    "day": day,
                    "lesson": lesson,
                    "lcode": lcode
                };
                jsonObj.push(itemObj);

            })
        });
        sessionStorage.clear();
        if (jsonObj.length >0) {
            SetSessonUserApplyCourse(jsonObj);

            window.location.href = "BuyCourseTime";
        }
        else {
       
            ShowError("请点击选择课程");
        }  
    }

    InitCallBack = function (result) {

        var list = result.Entity.CourseScheduleList;
        

        $.each(list, function (i) {
            var cs = list[i];
            CourseScheduleData[cs.Day][cs.Lesson].push(cs);
         
        });

        var times = result.Entity.CourseTimeList;
        CourseTime = new Object();
        $.each(times, function (i) {
            var t = times[i];
            CourseTime[t.Lesson] = t.TimeRange;
        });
        var userApplyList = GetSessonUserApplyCourse();
        if (userApplyList) {
            $.each(userApplyList, function (i) {
                var item = userApplyList[i];

                var csList = CourseScheduleData[item.day][item.lesson];
                $.each(csList, function (j) {
                    var course = csList[j];
                    if (course.CourseCode == item.courseCode) {
                        CreateSelectCourseInfo(course, j);
                        return false;
                    }
                });
            });
        }


       
       
    }

    Init();


   
});