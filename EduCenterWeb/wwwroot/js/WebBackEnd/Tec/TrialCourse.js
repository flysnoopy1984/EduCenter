$(function () {
    var QueryTrialLogUrl = "TrialCourse?handler=QueryTrialLog";
    var UpdateTrialLogStatusUrl = "TrialCourse?handler=ConfirmTrialStatus";
    var table;
    Init = function () {
        var sysDate = new Date();
    
        var fdateText = getDateStr(sysDate);
        var tdate = new Date(sysDate)
        tdate.setDate(tdate.getDate() + 1);
        var tdateText = getDateStr(tdate);

    
       laydate.render({
            elem: ".StartDateInput",
            eventElem: '#btn_StartDatePick',
            done: LayDataSelect,
           theme: 'molv',
           value: fdateText,
           isInitValue: false
       });

        laydate.render({
            elem: ".EndDateInput",
            eventElem: '#btn_EndDatePick',
            done: LayDataSelect,
            theme: 'molv',
            value: tdateText,
            isInitValue: false
        });
       
        var tecCode = $("#selTecCode").val();
        //var fDate = $(".StartDateInput").text();
        //var tDate = $(".EndDateInput").text();


        QueryTrialData(tecCode, fdateText, tdateText);


    }

   
    //getLayuiDateStr = function (date) {
    //    var year = date.year;
    //    var month = date.month;
    //    var monthName = month;
    //    if (month < 10)
    //        monthName = "0" + month;
    //    var day = date.date;
    //    return year + "-" + monthName + "-" + day;

    //}
    //getDateRangeStr = function (fdate, tdate) {
    //    if (fdate.month == tdate.month && fdate.date > tdate.date) {
    //        return getLayuiDateStr(tdate) + "-" + getLayuiDateStr(fdate);
    //    }
    //    else if (fdate.month > tdate.month) {
    //        return getLayuiDateStr(tdate) + "-" + getLayuiDateStr(fdate);
    //    }
    //    else
    //        return getLayuiDateStr(fdate) + "-" + getLayuiDateStr(tdate);
    //}

    LayDataSelect = function (value, fdate, edate) {
        var tecCode = $("#selTecCode").val();
         var fDate = $(".StartDateInput").text();
        var tDate = $(".EndDateInput").text();

     
        QueryTrialData(tecCode, fDate, tDate);
       // var dateRange = getDateRangeStr(fdate, edate);
       //// dateRangectrl.config.value = '2019-06-19 - 2019-06-20';
       // value = '2019-06-19 - 2019-06-20';
       // laydate.render(value);
       // $(".DateInput").text('2019-06-19 - 2019-06-20');
    }

    QueryTrialData = function (tecCode, fDate, tDate) {

        layui.use('table', function () {
             table = layui.table;
            //var tecCode = $("#selTecCode").val();
            //var fDate = $(".StartDateInput").text();
            //var tDate = $(".EndDateInput").text();
            //第一个实例
            table.render({
                elem: '#CourseList',
                id:"tableCourseList",
                height: 'full-350',
                limit:20,
                headers: {
                    "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val(),
                },
                url: QueryTrialLogUrl,
                method: 'post',
                where: { "fromDate": fDate, "toDate": tDate, "tecCode": tecCode },
                request: {
                    pageName: 'pageIndex',
                    limitName: 'pageSize'
                },
                response: {
                    msgName: 'ErrorMsg', //规定状态信息的字段名称，默认：msg
                    statusName: 'code',
                }, 
                parseData: function (res) { //res 即为原始返回的数据
                    var code = 0;
                    if (!res.IsSuccess)
                        code = 200;
                    else
                        code = 0;
                    return {
                        "code": code, //解析接口状态
                        "msg": res.ErrorMsg, //解析提示文本
                        "count": res.RecordTotal, //解析数据长度
                        "data": res.List //解析数据列表
                    }
                },
                page: true, //开启分页
                cols: [[ //表头
                   
                    { field: 'Id', title: 'ID', width: 80,hide:true, },
                    { field: 'UserName', title: '申请用户', width: 135, },
                    { field: 'CourseName', title: '课程名', width: 135, },
                    { field: 'TrialDateStr', title: '试听课日期', width: 135, },
                    { field: 'TrialTimeStr', title: '时间', width: 135, },
                    { field: 'TecName', title: '课程老师', width: 135, },
                    {
                        field: 'TrialLogStatusName',
                        title: '状态',
                        templet: function (d) {
                            if (d.TrialLogStatus != 10)
                                return '<div>' + d.TrialLogStatusName + '</div>';
                            if (d.TrialLogStatus == 11)
                                return '<div><div class="ConfirmStatus">' + d.TrialLogStatusName + '</div></div>';
                            else if (d.TrialLogStatus == 10)
                                return '<div><div class="ApplyStatus">' + d.TrialLogStatusName + '</div></div>';
                        }, width: 90
                    },
              
                    { field: 'ApplyDateTimeStr', title: '申请时间', width: 150, },
                    { fixed: 'right', width: 120, align: 'center', toolbar: '#TableToolBar' },
                   
                ]]
               
            });

            table.on('tool(AdminOption)', function (obj) {
                var data = obj.data; //获得当前行数据
                var layEvent = obj.event; //获得 lay-event 对应的值
                if (layEvent == "confirm") {
                  
                    UpdateLogStatus(data.Id);
                    //obj.update({
                    //    TrialLogStatus: 11,
                    //    TrialLogStatusName: "已安排",
                    //});
                }
            });
        });
    }

    UpdateLogStatus = function (Id) {
        callAjax_Query_NoBlock(UpdateTrialLogStatusUrl, {"Id":Id}, UpdateLogStatusCallBack)
    }

    UpdateLogStatusCallBack = function (res) {
        if (res.IsSuccess) {
            layer.alert('申请已确认', { icon: 1 });
            table.reload("tableCourseList");
        }
     
    }
 
    Init();
})