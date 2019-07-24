$(function () {
    var QueryTrialLogUrl = "TrialCourse?handler=QueryTrialLog";
    var UpdateTrialLogStatusUrl = "TrialCourse?handler=ConfirmTrialStatus";
    var WxRemindUrl = "TrialCourse?handler=WxRemind";
    var SendRewardUrl = "TrialCourse?handler=SendReward";
    var table;
    Init = function () {
        var sysDate = new Date();
    
        var fdateText = getDateStr(sysDate);
        var tdate = new Date(sysDate)
        tdate.setDate(tdate.getDate() + 5);
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
                    {
                        field: 'UserRealName', title: '宝贝名称',
                        templet: function (d) {
                            return "<a class='FieldChildName' onclick=toUserInfo('" + d.OpenId + "')>" + d.UserRealName + "</a>";
                        },
                        width: 135,
                    },
                    { field: 'UserPhone', title: '联系方式', width: 135, },
                    { field: 'CourseName', title: '课程名', width: 90, },
                    { field: 'TrialDateStr', title: '试听课日期', width: 120,},
                    { field: 'TrialTimeStr', title: '时间', width: 120,},
                    { field: 'TecName', title: '课程老师', width: 90, },
                    { field: 'SalesName', title: '接待人', width: 90, },
                    { field: 'InviteOwnName', title: '邀请人', width: 90, },
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
              
                    { field: 'ApplyDateTimeStr', title: '申请时间', width: 140, },
                    { fixed: 'right', width:220, align: 'left', toolbar: '#TableToolBar' },
                   
                ]]
               
            });

            table.on('tool(AdminOption)', function (obj) {
                var data = obj.data; //获得当前行数据
                var layEvent = obj.event; //获得 lay-event 对应的值
                if (layEvent == "confirm") {
                  //  UpdateLogStatus(data.Id);
                }
                else if (layEvent == "edit") {
                    EditTrialCourse(data.Id);
                }
                else if (layEvent == "wxRemind") {
                    wxRemind(data.Id);
                }
                else if (layEvent == "sendReward") {
                    SendReward(data.InviteLogId, data.InviteOwnId, data.OpenId);
                }
            });
        });
    }

    SendReward = function (inviteLogId, ownOpenId,inviteOpenId) {
        var data = {
            "invitelogId": inviteLogId,
            "invitedOpenId": inviteOpenId,
            "ownOpenId": ownOpenId
        }
        callAjax_Query(SendRewardUrl, data, function () {
            layer.alert('奖励金已发送成功', { icon: 1 });
            table.reload("tableCourseList");
           // ShowInfo("", null, null, 1);
        })
    }

    toUserInfo = function (OpenId) {
        window.location.href = "/WebBackend/User/List?act=q&openId="+OpenId;
    }

    UpdateLogStatus = function (Id) {
        ShowInfo("请点击编辑选择接待人!");
      //  callAjax_Query_NoBlock(UpdateTrialLogStatusUrl, {"Id":Id}, UpdateLogStatusCallBack)
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

    CloseTrialCourse = function () {

        layer.closeAll("iframe");
        LayDataSelect();
    }

    //wxRemind
    wxRemind = function (Id) {
        callAjax_Query(WxRemindUrl, { "Id": Id }, function () {
            ShowInfo("提醒已发送成功", null, null, 1);
        })
       
    }
 
    Init();
})