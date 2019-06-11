$(function () {
    var userCourseUrl = "MyLeave?handler=GetCourseByDate";

    Init = function () {

        laydate.render({
            elem: '.DateInput',
            eventElem: '#btn_DatePick',
            trigger: 'click',
            done: function (value, date) {
             //   alert('你选择的日期是：' + value + '\n获得的对象是' + JSON.stringify(date));
                ShowLoading();

                callAjax_Query_NoBlock(userCourseUrl, { "date": value }, GetCourseByDateCallBack);
;            }
        });
 
    }

    GetCourseByDateCallBack = function () {

    }

    ShowLoading = function ()
    {
        $(".LoadingArea").css("display","flex");
        $(".LeaveList").hide();
        
    }
    LoadingDown = function () {
        $(".LoadingArea").hide();
        $(".LeaveList").show();
    }
    Init();
})