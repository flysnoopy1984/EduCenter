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
    };

    Init();
});