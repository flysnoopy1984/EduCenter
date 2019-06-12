$(function () {
    var carousel = null;
    ShowInfo = function (msg) {
        $.alert({
            title: "消息",
            content: msg,
            type:"red",

        });
    }
    Init = function () {
        var showMsg = GetUrlParam("showMsg");
        if (showMsg != undefined) {
            if (showMsg == 1)
            ShowInfo("不能重复选课！");
        }
        $("#btn_Leave").on("click", LeaveEvent);
        $("#btn_Sign").on("click", SignInEvent);
    };

    LeaveEvent = function () {
        var date = $("#btn_Leave").attr("date");
        window.location.href = "MyLeave?date=" + date;
    }
    SignInEvent = function () {
        var date = $("#btn_Leave").attr("date");
        window.location.href = "MySign?date="+date;
    }

    Init();
});