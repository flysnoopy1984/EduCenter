$(function () {
   
    var QueryTecLeaveUrl = "Leave?handler=QueryTecLeave";
    var GetTecLeaveCourseUrl = "Leave?handler=GetTecLeaveCourse";

    var glaypage;
    var pageIndex = 1;
    var pageSize = 20;
  
    Init = function () {
        var date = new Date();
        var year = date.getFullYear();
        var month = date.getMonth() + 1;

        var dateText = year + "-" + month;

        var paramDate = GetUrlParam("date");
        var paramTecCode = GetUrlParam("tecCode");
        if (paramTecCode != undefined) {
            $("#selTecCode").val(paramTecCode);
        }
        if (paramDate != undefined) {
            dateText = paramDate;
        }


        laydate.render({
            elem: ".StartDateInput",
            eventElem: '#btn_StartDatePick',
            done: LayDaySelect,
            type:'month',
            theme: 'molv',
            value: dateText,
            isInitValue: false
        });
        $("#btn_CreateLeave").on("click", CreateNewLeave);

        $("#selTecCode").on("click", TecSelect);

        layui.use('laypage', function () {
            glaypage= layui.laypage;
        });
    

    
        ReSetQuery(dateText);
      //  QueryLeaveList($("#selTecCode").val(),year + "-" + month,1);
    }
    LayDaySelect = function (value) {
        ReSetQuery(value);
    }
    TecSelect = function () {
        var date = $(".StartDateInput").text();
        ReSetQuery(date);
    }
    ReSetQuery = function (date) {
        pageIndex = 1;
       
        QueryLeaveList(date);
    }

    StartQuery = function () {
        $(".DataMsg").hide();
        $(".TableData").show();
    }
    NoData = function () {
        $(".DataMsg").text("没有数据");
        $(".DataMsg").show();
        $(".TableData").hide();
    }

    QueryLeaveList = function (date) {
        StartQuery();
        var tecCode = $("#selTecCode").val();

        var data = {
            "date": date,
            "tecCode": tecCode,
            "pageIndex": pageIndex,
            "pageSize": pageSize,

        }
        callAjax_Query(QueryTecLeaveUrl, data,QueryLeaveListCallBack, "");
    }

    QueryLeaveListCallBack = function (res) {
        $("#TableLeave tr td").remove();
        var data = res.List;
        if (data.length > 0) {
            var root = $("#TableLeave");
            $.each(data, function (i) {
                var leaveType = "";
                if (data[i].LeaveType == 1)
                    leaveType = "全天请假";
                else
                    leaveType = "<a style='cursor:pointer; color:blue' onclick=ShowDetailLesson(this)>部分请假</a>";

                var html = "<tr class='trLeave'>";
                html += "<td class='tdTecCode'>" + data[i].TecCode + "</td>";
                html += "<td class='tdTecName'>" + data[i].TecName + "</td>";
                html += "<td class='tdDate'>" + data[i].LeaveDateStr + "</td>";

                html += "<td>" + leaveType + "</td>";
                html += "<td class='tdLessonDetail'></td>";
                html += "</tr>";
                root.append(html);
            });

            glaypage.render({
                elem: 'Pager',
                layout: ['prev', 'page', 'next', 'count', 'skip'],
                limit: pageSize,
                count: res.RecordTotal,
                jump: function (obj, first) {
                    if (!first) {
                        pageIndex = obj.curr;
                        QueryLeaveList();
                    }

                }
            });
        }
        else {
            NoData();
        }
     
    }

    ShowDetailLesson = function (obj) {

        var tr = $(obj).closest(".trLeave");
        var detailTd = tr.children(".tdLessonDetail");
        if ($(detailTd).text() == "") {
            var tecCode = tr.children(".tdTecCode").text();
            var date = tr.children(".tdDate").text();
            
            $(detailTd).append('<i class="fa fa-spinner fa-pulse fa-lg fa-fw"></i>稍等...');

            callAjax_Query_NoBlock(GetTecLeaveCourseUrl, { "tecCode": tecCode,"date":date}, function (res) {
                $(detailTd).empty();
                var data = res.List;
                var html = $("#HideData .DetailLessonList").clone();
                $.each(data, function (i) {
                    html.append("<div>" + data[i].CourseName + " | " + data[i].TimeRange+"</div>");
                });
                
                $(detailTd).append(html);
            });

        }
    }
    //GetTecLeaveCourseCallBack = function (res) {

    //}

    CreateNewLeave = function () {
        layer.open({
            type: 2,
            title: '创建教师请假',
            shadeClose: true,
            shade: 0.8,
            area: ['600px', '80%'],
            content: 'NewLeave' //iframe的url
        }); 
    }

    CloseNewLeave = function (tecCode,date) {

        layer.closeAll("iframe");
        //var date = $(".StartDateInput").text();
        //var tecCode = 
        window.location.href = "Leave?tecCode="+tecCode + "&date=" + date;
    }
    Init();
})