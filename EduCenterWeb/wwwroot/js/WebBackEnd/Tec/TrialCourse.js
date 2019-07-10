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

        $("#selTecCode").on("change", LayDataSelect);
       
        var tecCode = $("#selTecCode").val();
    
        QueryTrialData(tecCode, fdateText, tdateText);


    }

 
    LayDataSelect = function () {
        var tecCode = $("#selTecCode").val();
         var fDate = $(".StartDateInput").text();
        var tDate = $(".EndDateInput").text();

     
        QueryTrialData(tecCode, fDate, tDate);
      
    }

    QueryTrialData = function (tecCode, fDate, tDate) {

        layui.use('table', function () {
             table = layui.table;
   
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
                    { field: 'UserRealName', title: '用户', width: 135, },
                    { field: 'UserPhone', title: '联系方式', width: 135, },
                    { field: 'CourseName', title: '课程名', width: 135, },
                    { field: 'TrialDateStr', title: '试听课日期', width: 135, edit: 'text'},
                    { field: 'TrialTimeStr', title: '时间', width: 135, edit: 'select' },
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
                    { fixed: 'right', width: 140, align: 'center', toolbar: '#TableToolBar' },
                   
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
                else if (layEvent == "edit") {
                    EditTrialCourse(data.Id);
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

    //详细信息
    EditTrialCourse = function (Id) {
        //var obj = $(e.currentTarget);
    
        layer.open({
            type: 2,
            title: '试听课',
            shadeClose: false,
            shade: 0.8,
            area: ['500px', '80%'],
            content: 'NewTrialCourse?Id=' + Id //iframe的url
        });
    }

    CloseTrialCourse = function (openId, BabyName) {

        layer.closeAll("iframe");
        if (BabyName) {

            var tr = $("#UserListTable tr[openId=" + openId + "]");
            tr.find(".BabyName").text(BabyName);

        }
        //var date = $(".StartDateInput").text();
        //var tecCode = 
        //  window.location.href = "BabyInfo";
    }
 
    Init();
})