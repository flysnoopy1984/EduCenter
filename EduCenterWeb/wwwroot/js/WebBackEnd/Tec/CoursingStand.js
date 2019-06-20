$(function () {
    var QueryTecCourseUrl = "CoursingStand?handler=QueryTecCourse";
    var TecCourseData = null;

    var sysDate = new Date();
    var year = sysDate.getFullYear();
    var month = sysDate.getMonth() + 1;
   
    var firstDate = new Date(year, month-1, 1);
    var dayofweek = firstDate.getDay();

    var nextDate = new Date(year, month, 1);
    nextDate.setDate(0);
    MaxDay = nextDate.getDate();
    var glaydate;
 
    Init = function () {
    
        $("#selDate").val(year + "-" + month);

        glaydate =laydate.render({
            elem: "#selDate",
            type: 'month',
            done: LayDataSelect
        });

        $("#selTecCode").on("change", TecSelectEvent);

        QueryCalendorData();

    };

    QueryCalendorData = function () {
        var tecCode = $("#selTecCode").val();
        var data = {
            "tecCode": tecCode,
            "year": year,
            "month": month,

        }
        var tecName = $("#selTecCode option[value=" + tecCode + "]").text();
        var info = month + "月 " + tecName + "老师";
        $(".CourseInfo").text(info);
        callAjax_Query(QueryTecCourseUrl, data, QueryTecCourseCallBack, "查询中");

    }

    AddCellTitle = function (obj, title) {
        var row = $("#HideData .cellTitle").clone();
        $(row).text(title);
        $(obj).append(row);
    }

    AddCellCourse = function (obj,c) {
        var row = $("#HideData .cellCourseRow").clone();
        var cst = "标";
        if (c.CourseScheduleType == 1)
            cst = "暑";
        else if (c.CourseScheduleType == 2)
            cst = "寒";
        var info = "[" + cst + "]" + c.CourseName + " | " + c.TimeRange + "(" + c.ApplyNum + ")";
        if (c.CoursingStatus == 1)
            row.addClass("text-danger");
        else if (c.CoursingStatus == 2)
            row.addClass("text-warning");

        $(row).text(info);
      
        $(obj).append(row);
        return row;
    }

    ConnectDate = function (day) {
        var mn = month;
        if (month < 10)
            mn = "0" + month;
        return year + "-" + mn + "-" + day;
    }

    GenCalendor = function () {
        var day = -1;
        var date = "";
        $("#tableCourseInfo tr:not(:first)").each(function (row) {
            var days = $(this).children();
            var container = null;

            days.each(function (d) {
                container = $(this).children(".CellContainer");
                container.empty();
               
                if (row == 0 && day == -1) {
                    if ((d + 1) == dayofweek) {
                        day = 1;
                        date = ConnectDate(day);
                        AddCellTitle(container, date)
                       
                    }
                }
                else if (day > 0) {
                    day += 1;
                    date = ConnectDate(day);
                    if (day <= MaxDay)
                        AddCellTitle(container, date)
                }
             //  var cell = $(this);

                if (day != -1) {
                    var needevent = false;
                    if (TecCourseData[date] != undefined) {
                        var list = TecCourseData[date];
                        $.each(list, function (i) {
                            var c = list[i];
                            var row = AddCellCourse(container,c);
                            
                            needevent = true;
                        });
                        if (needevent) {
                            container.on("click", { "date": date, "tecCode": list[0].TecCode}, ToCoursingDayEvent)
                        }
                        
                    }
                   
                }
            });

        });
    }

    ToCoursingDayEvent = function (e) {
        var date = e.data.date;
        var tecCode = e.data.tecCode;

        window.location.href = "CoursingDay?date=" + date + "&tecCode=" + tecCode;
    }

   
    QueryTecCourseCallBack = function (res) {

        TecCourseData = res.Entity;

        GenCalendor();
    //   

    }


    LayDataSelect = function (value,obj) {
        year = obj.year;
        month = obj.month;

        firstDate = new Date(year, month-1, 1);
        dayofweek = firstDate.getDay();
        nextDate = new Date(year, month, 1);
        nextDate.setDate(0);
        MaxDay = nextDate.getDate();


        QueryCalendorData();
    }

    TecSelectEvent = function () {

    
        year = glaydate.config.dateTime.year;
        month = glaydate.config.dateTime.month+1;

      

        firstDate = new Date(year, month - 1, 1);
        dayofweek = firstDate.getDay();
        nextDate = new Date(year, month, 1);
        nextDate.setDate(0);
        MaxDay = nextDate.getDate();

        QueryCalendorData();
    //     var a = glaydate;
    }

    Init();


});