$(function () {
    var QueryTrialLogUrl = "TrialCourse?handler=QueryTrialLog";
    var QueryTecLeaveUrl = "Leave?handler=QueryTecLeave";
    var table;
    Init = function () {
        var date = new Date();
        var year = date.getFullYear();
        var month = date.getMonth() + 1;

        laydate.render({
            elem: ".StartDateInput",
            eventElem: '#btn_StartDatePick',
            done: LayDataSelect,
            type:'month',
            theme: 'molv',
            value: year + "-" + month,
            isInitValue: false
        });
        $("#btn_CreateLeave").on("click", CreateNewLeave);
    }

    QueryTecLeave = function (tecCode,date) {
        layui.use('table', function () {
            table = layui.table;
            //var tecCode = $("#selTecCode").val();
            //var fDate = $(".StartDateInput").text();
            //var tDate = $(".EndDateInput").text();
            //第一个实例
            table.render({
                elem: '#TableLeave',
                id: "tableLeaveList",
                height: 'full-350',
                limit: 20,
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
                    { field: 'TecCode', title: '教师编号', width: 135, },
                    { field: 'TecName', title: '教师名称', width: 135, },
                    { field: 'CourseName', title: '请假日期', width: 135, },
                    { field: 'TrialDateStr', title: '申请时间', width: 135, },
                    { field: 'ApplyDateTimeStr', title: '状态', width: 100, },
                ]]

            });

       
        });
    }

    LayDataSelect = function () {

    }

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
    Init();
})