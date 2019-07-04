$(function () {
    var InitUrl = "Apply?handler=InitData";
    var SubmitUrl = "Apply?handler=Submit";
  
  //  var CourseScheduleData = null;
    var CourseTime = null;
    var CourseMaxApplyNum = null;
    var selDay = null
    var selLesson = null;
    var selCode = null;
    
    Init = function () {
      
        //var msg = GetUrlParam("msg", true);
        //if (msg != undefined) {
        //    ShowInfo(msg, null, null, 2);
        //
        $("#btnConfirm").on("click", NextStep);
        $(".switchCourseScheduleType").on("click", function () {
            window.location.href = "ApplyWinterSummer";
        })

        callAjax_Query(InitUrl, {}, InitCallBack, "");

    }

    selectCourse = function (obj) {
        var cell = $(obj);
        selDay = cell.attr("day");
        selLesson = cell.attr("lesson");

        //生成单元格中的课程数据
        var csList = new Array();
        cell.find(".CellCourseData div").each(function () {
          
            var item = CreateDataObject(parseInt(selDay), parseInt(selLesson), $(this))
         
            csList.push(item);
        });

        //根据底部信息获取选择Code
        var selectedLi = $(".SelectedCourseItems .Item .SelectCourseItemContent ul li");
        //原来CheckBox都选，现在单选可以修改代码。
        $.each(selectedLi, function () {
            var day = $(this).attr("day");
            var les = $(this).attr("lesson");
            if (day == selDay && les == selLesson) {
                selCode = $(this).closest(".Item").find(".SelectCourseItemTitle").attr("CourseCode");
                return false;
             
            }
        })
        var html = '<div class="CourseSelect">';
       // html += CreateConfirmItemsHtml(-1, null);
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
                    text: "清空重选",
                    btnClass: 'btn-info',
                    action: function () {
                        DeleteSameTimeCourse(selDay, selLesson,true);
                        //var checkedNo = $(this).val();
                        //var checkeditem = csList[checkedNo];
                        //DeleteSelectCourseInfo(checkeditem.Day, checkeditem.Lesson, checkeditem.LessonCode);
                    }
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
        var disabled = "";
        var itemDes = "";
        //最大人数判断
        var maxNum = CourseMaxApplyNum[CourseType];
        if (maxNum == item.ApplyNum) {
            disabled = "disabled";
            itemDes = "(已满)";
        }
           
        html += '<div class="radio ' + cbcss + '">';
        html += '<input ' + disabled+' type="radio" name="AvaliableCourse" value="' + i + '"  id="' + id + '" ' + checked + '>';
        html += '<label for="' + id + '" class="' + lbcss + '">' + name + '  ' + itemDes+'</label>';
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
        td.find(".CellCourseData").hide();
        td.find(".CellCourse").remove();
    //    td.empty();
        var gl = this.GetItemStyleByType(data.CourseType,"label");
        var tdHtml = $("#HideData .CellCourse").clone();
        tdHtml.addClass(gl);
        tdHtml.find(".cellName").text(data.CourseName);
        td.append(tdHtml);

        //底部信息

        //删除已经选择的同时段课程
        DeleteSameTimeCourse(data.Day, data.Lesson,false);

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
            li.find(".SelectCourseTime").text(dayName + " | " + CourseTime[data.Lesson].TimeRange);
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
       
        RestoreCellData(day, lesson);

    }

    RestoreCellData = function (day,lesson) {
        var gridRoot = null;
        if (day >= 1 && day <= 5) gridRoot = $("#GridNormal");
        else gridRoot = $("#GridWeek");

        var td = gridRoot.find(".CellContainer[day=" + day + "][lesson=" + lesson + "]");
        td.find(".CellCourseData").show();
        td.find(".CellCourse").remove();
    }
    //根据Day Lesson找到已选择的课程
    DeleteSameTimeCourse = function (day, lesson,IsRestoreCell) {
        var course = $(".SelectCourseItemContent ul li[lesson=" + lesson + "][day=" + day + "]");
        if (course.length > 0) {
            if (course.parent().children().length == 1) {
                course.closest(".Item").remove();
            }
            else
                course.remove();
        }

        if (IsRestoreCell) {
            RestoreCellData(day, lesson);
        }

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

    DoNextStep = function (datalist) {

        ShowConfirm("课程时间只能选择一次，请谨慎选择！", null, "red", function () {

            callAjax_Query(SubmitUrl, {
                "lessonCodeList": datalist, "courseScheduleType": 0
            }, NextCallBack, "", NextError);

        }, undefined,"我再想想","我已确认");
      
    }
    NextStep = function () {

      
        var datalist = new Array();
        $(".SelectedCourseItems .Item").each(function () {
            var item = $(this);
            var list = item.find(".SelectCourseItemContent ul li");
            list.each(function () {
                var li = $(this);
           
                var lcode = li.attr("lcode");

                datalist.push(lcode);

            })
        });
  
        if (datalist.length > 0) {
            DoNextStep(datalist);
        }
        else {

            ShowError("请点击选择课程");
        }  
      
    }
    NextError = function (res) {
        if (res.IntMsg == -1) {
            window.location.href = "Login";
        }
    }
    NextCallBack = function (res) {
        ShowInfo("系统已安排您的课程", null, null, 2, function () {
            window.location.href = "MyCourse";
        })
    }

    InitCallBack = function (result) {
        /*
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
        */
        CourseTime = result.Entity.CourseTimeList;
        CourseMaxApplyNum = result.Entity.CourseMaxApplyNum;
      //  InitUserAction();

    
    }

    InitUserAction = function () {

        //var userApplyList = GetSessonUserApplyCourse();

      

        //if (userApplyList) {
        //    $.each(userApplyList, function (i) {

        //        var item = userApplyList[i];
        //        var gridRoot = null;
        //        if (item.day >= 1 && item.day <= 5) gridRoot = $("#GridNormal");
        //        else gridRoot = $("#GridWeek"); 

        //        var divList = gridRoot.find(".CellContainer[day=" + item.day + "][lesson=" + item.lesson + "] div[lCode=" + item.lcode+"]");
        //        $.each(divList, function (i) {
        //            var course = CreateDataObject(item.day, item.lesson, $(this));
        //            CreateSelectCourseInfo(course, i);
        //        });
                
           
        //        //$.each(csList, function (j) {
        //        //    var course = csList[j];
        //        //    if (course.CourseCode == item.courseCode) {
        //        //        CreateSelectCourseInfo(course, j);
        //        //        return false;
        //        //    }
        //        //});
        //    });
        //}
        
    }

    CreateDataObject =function(day,lesson,row)
    {
        var item = new Object();
        item.CourseCode = $(row).attr("CourseCode");
        item.CourseName = $(row).text();
        item.CourseType = parseInt($(row).attr("CourseType"));
        item.Day = parseInt(day);
        item.Lesson = parseInt(lesson);
        item.LessonCode = $(row).attr("lCode");
        item.ApplyNum = $(row).attr("CurUser");
        return item;
    }

    InitCourse = function () {

    }

    Init();


   
});