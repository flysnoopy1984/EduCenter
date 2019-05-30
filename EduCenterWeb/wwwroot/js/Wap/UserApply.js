$(function () {
    var InitUrl = "Apply?handler=InitData";
    var CourseScheduleData = null;
    var CourseTime = null;
    var selDay = null
    var selLesson = null;
    Init = function () {

        var times = $("#GridWeek tr").length ;
        CourseScheduleData = new Object();
        for (var i = 1; i < 8; i++) {
            CourseScheduleData[i] = new Object();
            for (var j = 1; j < times; j++) {
                CourseScheduleData[i][j] = new Array();
            }
         
        }
        callAjax_Query(InitUrl, {}, InitCallBack,"");
    }



    selectCourse = function (obj) {
        var cell = $(obj);
        var day = cell.attr("day");
        var lesson = cell.attr("lesson");

        selDay = day;
        selLesson = lesson;

        var csList = CourseScheduleData[day][lesson];

        var selectedLi = $(".SelectedCourseItems .Item .SelectCourseItemContent ul li");
        //原来CheckBox都选，现在单选可以修改代码。
        var selectedCode = new Array();
        $.each(selectedLi, function () {
            var selDay = $(this).attr("day");
            var selLes = $(this).attr("lesson");
            if (day == selDay && lesson == selLes) {
                var selCode = $(this).closest(".Item").find(".SelectCourseItemTitle").attr("CourseCode");
                selectedCode.push(selCode);
                
            }
        })
        var html = '<div class="CourseSelect">';
        html += CreateConfirmItemsHtml(-1, null, selectedCode);
        $.each(csList, function (i) {
            var item = csList[i];
            html +=CreateConfirmItemsHtml(i, item, selectedCode);
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

    CreateConfirmItemsHtml = function (i, item, selectedCode) {
        var id = "cb_" + i;
     //   var item = csList[i];
        var checked = "";
        var lbcss = "list-group-item-info";
        var name = "无";
        var CourseType = -1;

        if (i != -1) {
            var arrIndex = $.inArray(item.CourseCode, selectedCode);
            if (arrIndex > -1)
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



    CreateSelectCourseInfo = function (data, selectNo) {

        if (selectNo == -1) {

        };

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
            ul = item.next().find(".SelectCourseItemContent ul");

        var li = ul.find("li[lesson=" + data.Lesson + "]");
        if (li.length == 0) {
            li = $("#HideData #liData").clone();
            var dayName = GetDayName(data.Day);
            li.attr("lesson", data.Lesson);
            li.attr("day", data.Day);
            li.find(".SelectCourseTime").text(dayName + " | " + CourseTime[data.Lesson]);
            ul.append(li);
        }
    };

    DeleteSelectCourseInfo = function (day,lesson,CourseCode) {

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
       
       
    }

    Init();

   
});