$(function () {
    var saveUrl = "PlanCreator?handler=Save";
    var delUrl = "PlanCreator?handler=Delete";
    var getUrl = "PlanCreator?handler=Get";

    Init = function () {
        var myDate = new Date();
        var year = myDate.getFullYear();

        $("#planYear").val(year);
        laydate.render({
            elem: "#planYear",
            type: 'year'
        });

        $("#btnSave").on("click", Save);
    },


    DropData = function (event, ui) {

        var dropObj = $(ui.draggable);
        var root = $(this).find(".CellContainer");

   //     var gl = dropObj.attr("cgl");
        var cCode = dropObj.attr("cCode");
        var cType = dropObj.attr("cType");

        var gl = GetCellStyleByType(cType);

        var cellRow = $(".HideData .CellRow").clone();
           
        cellRow.addClass(gl);
        cellRow.attr("cCode", cCode);
        cellRow.attr("cType", cType);
        var cellText = cellRow.find(".cellText");
     
        cellText.text(dropObj.text());
        cellRow.show();
        root.append(cellRow);

    },

    GetCellStyleByType = function (cType) {
        var gl = "list-group-item-info";
        switch (cType) {

            case 2:
                gl = "list-group-item-warning"; break;
            case 1:
                gl = "list-group-item-danger"; break;
            case 3:
                gl = "list-group-item-success"; break;
        }
        return gl;

    }

    Save = function () {
      
        var year = $("#planYear").val();
        var dataList = new Array();
        $("#CourseGrid tr .CourseCell .CellContainer .CellRow").each(function () {

            var cellRow = $(this);
            var cCode = cellRow.attr("cCode");
            var cType = cellRow.attr("cType");
            var cellText = cellRow.find(".cellText");
            var selTec = cellRow.find(".selTec");
            cellRow.parent().siblings(":first")

           
            var tecCode = selTec.val();
            var day = cellRow.parent().attr("day");
            var courseSchedule = {
                "CourseCode": cCode,
                "CourseName": cellText.text(),
                "TecCode": tecCode,
                "Year": year,
                "Day": day,
                "StartTime": "",
                "EndTime": ""

            };
            dataList.push(courseSchedule);

        });

        callAjax(saveUrl, { "list": dataList }, HandlerSave, "保存完成");
       
        },

    HandlerSave = function () {

    }

    Init();

    InitCalendor(".CourseList li", "#CourseGrid .AcceptCell", DropData);
});