$(function () {
    var saveUrl = "Plan?handler=Save";
    var delUrl = "Plan?handler=Delete";
    var getUrl = "Plan?handler=Get";
    var SkillLevel = null;

    Init = function () {
        var myDate = new Date();
        var year = myDate.getFullYear();

        $("#planYear").val(year);
        laydate.render({
            elem: "#planYear",
            type: 'year',
            min: -1,
           max:365*3,
            done: LayDataSelect
        });

        $("#btnSave").on("click", Save);
        $("#selScheduleType").on("change", QueryCourseSchdule);

        QueryCourseSchdule();
    };



    LayDataSelect = function (value, date) {

        var scheduleType = $("#selScheduleType").val();
        var data = {
            "year": value,
            "scheduleType": scheduleType
        };

        callAjax_Query(getUrl, data, HandlerGet);
    };

    QueryCourseSchdule = function () {

        var year = $("#planYear").val();
        var scheduleType = $("#selScheduleType").val();
        var data = {
            "year": year,
            "scheduleType": scheduleType
        };

        callAjax_Query(getUrl, data, HandlerGet);
    }

       


    DropData = function (event, ui) {

        var dropObj = $(ui.draggable);
        var root = $(this).find(".CellContainer");
       
        var cCode = dropObj.attr("cCode");
        var cType = dropObj.attr("cType");
        var cName = dropObj.text();

        var curNo = root.find(".CellRow[ccode=" + cCode + "]").length;

        CreateRow(root, cCode, cType, cName, curNo+1);

    };

    CreateRow = function (root, cCode, cType, cName,no) {

        var gl = GetCellStyleByType(cType);

        var cellRow = $(".HideData .CellRow").clone();

        cellRow.addClass(gl);
        cellRow.attr("cCode", cCode);
        cellRow.attr("no", no);
        cellRow.attr("cType", cType);
        var cellText = cellRow.find(".cellText");

        cellText.text(cName);
        cellRow.show();
        root.append(cellRow);
    }

    GetCellStyleByType = function (cType) {
        var gl = "list-group-item-info";
        switch (cType) {

            case "MS":
                gl = "list-group-item-warning"; break;
            case "SF":
                gl = "list-group-item-danger"; break;
            case "WQ":
                gl = "list-group-item-success"; break;
        }
        return gl;

    },

    Save = function () {

        var year = $("#planYear").val();
        var dataList = new Array();
        $("#CourseGrid tr .CourseCell .CellContainer .CellRow").each(function () {

            var cellRow = $(this);
            var cCode = cellRow.attr("cCode");
            var cType = cellRow.attr("cType");
            var lessonNo = cellRow.attr("no");
            var cellText = cellRow.find(".cellText");

            var day = cellRow.parent().attr("day");
            var lesson = cellRow.parent().attr("lesson");
            var lessonCode = year + "_" + day + "_" + lesson + "_" + cCode + "_" + lessonNo;

            var CourseScheduleType = $("#selScheduleType").val();


            var courseSchedule = {
                "CourseCode": cCode,
                "CourseName": cellText.text(),
               // "TecCode": tecCode,
                "Year": year,
                "Day": day,
                "CourseType": cType,
                "Lesson": lesson,
                "LessonCode": lessonCode,
                "LessonNo": lessonNo,
                "CourseScheduleType": CourseScheduleType,
                //"StartTime": starttime,
                //"EndTime": endtime

            };
            dataList.push(courseSchedule);

        });

        callAjax(saveUrl, { "list": dataList }, HandlerSave, "保存完成");

    },

    HandlerSave = function () {

    },

    HandlerGet = function (result) {
        var ScheduleList = result.List;
        var root;
        $("#CourseGrid tr .CourseCell .CellContainer").empty();

        $.each(ScheduleList, function (i) {
            var item = ScheduleList[i];
            var day = item.Day;
            var lesson = item.Lesson;
            var cCode = item.CourseCode;
            var cType = 0;
            var no = item.LessonNo;
            switch (item.CourseType) {
                case 1:
                    cType = "SF"; break;
                case 2:
                    cType = "MS"; break;
                case 3:
                    cType = "WQ"; break;
                    
            }
          // = item.CourseType;
            var cName = item.CourseName;
            root = $("#CourseGrid tr .CourseCell .CellContainer[day=" + day + "][lesson=" + lesson + "] ");

            CreateRow(root, cCode, cType, cName);


        });

    },

    Init();

    InitCalendor(".CourseList li", "#CourseGrid .AcceptCell", DropData);
});