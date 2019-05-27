﻿$(function () {
    var saveUrl = "PlanCreator?handler=Save";
    var delUrl = "PlanCreator?handler=Delete";
    var getUrl = "PlanCreator?handler=Get";
    var SkillLevel = null;

    Init = function () {
        var myDate = new Date();
        var year = myDate.getFullYear();

        $("#planYear").val(year);
        laydate.render({
            elem: "#planYear",
            type: 'year'
        });

        $("#btnSave").on("click", Save);

        //callAjax_Query(getUrl, {}, HandlerGet);
    },


    DropData = function (event, ui) {

        var dropObj = $(ui.draggable);
        var root = $(this).find(".CellContainer");
       
        var cCode = dropObj.attr("cCode");
        var cType = dropObj.attr("cType");
        var cName = dropObj.text();

        CreateRow(root, cCode, cType, cName);

    };

    CreateRow = function (root, cCode, cType, cName) {

        var gl = GetCellStyleByType(cType);

        var cellRow = $(".HideData .CellRow").clone();

        cellRow.addClass(gl);
        cellRow.attr("cCode", cCode);
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
            var cellText = cellRow.find(".cellText");
            var selTec = cellRow.find(".selTec");
            //var time = cellRow.parent().parent().siblings(":first").text();
            //var starttime = time.split("-")[0];
            //var endtime = time.split("-")[1];

            var tecCode = selTec.val();
            var day = cellRow.parent().attr("day");
            var lesson = cellRow.parent().attr("lesson");

            var courseSchedule = {
                "CourseCode": cCode,
                "CourseName": cellText.text(),
                "TecCode": tecCode,
                "Year": year,
                "Day": day,
                "CourseType": cType,
                "Lesson":lesson,
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

    if (result.Entry.ScheduleList != null)
        SkillLevel = result.Entry.ScheduleList;

        var ScheduleList = result.Entry.ScheduleList;
   //    $.each()

    },

    Init();

    InitCalendor(".CourseList li", "#CourseGrid .AcceptCell", DropData);
});