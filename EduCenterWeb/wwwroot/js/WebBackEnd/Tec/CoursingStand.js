$(function () {
    var QueryTecCourseUrl = "CoursingStand?handler=QueryTecCourse";
    var TecCourseData = null;

    var sysDate = new Date();
    var year = sysDate.getFullYear();
    var month = sysDate.getMonth() + 1;
    var firstDate = new Date(year, month-1, 1);
    var dayofweek = firstDate.getDay();
    firstDate.setDate(0);
    MaxDay = firstDate.getDate();

    Init = function () {
    
        $("#selDate").val(year + "-" + month);
        laydate.render({
            elem: "#selDate",
            type: 'month',
            done: LayDataSelect
        });
   

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

    AddCellCourse = function (obj,courseInfo) {
        var row = $("#HideData .cellCourseRow").clone();
        $(row).text(courseInfo);
        $(obj).append(row);
        return row;
    }

    GenCalendor = function () {
        var day = -1;
        var date = "";
        $("#tableCourseInfo tr:not(:first)").each(function (row) {
            var days = $(this).children();
            var container = null;

            days.each(function (d) {
                container = $(this).children(".CellContainer");
                
                date = year + "-" + month + "-" + day;
                if (row == 0 && day == -1) {
                    if ((d + 1) == dayofweek) {
                        day = 1;
                        
                        AddCellTitle(container, date)
                       
                    }
                }
                else if (day > 0) {
                    day += 1;
                    if (day <= MaxDay)
                        AddCellTitle(container, date)
                }
                var cell = $(this);

                if (day != -1) {
                    var needevent = false;
                    if (TecCourseData[date] != undefined) {
                        var list = TecCourseData[date];
                        $.each(list, function (i) {
                            var c = list[i];
                            var row = AddCellCourse(container, "[" + c.TimeRange + "]" + c.CourseName);
                            
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


    LayDataSelect = function () {

    }

    LoadCourseData = function () {
        var tecCode = $("#selTecCode").val();
        var date = $("#selDate").val();
        var year = date.split('-')[0];
        var month = date.split('-')[1];
    }

    Init();


});