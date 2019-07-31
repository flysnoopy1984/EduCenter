$(function () {
    var SaveAccountUrl = "AlipayAccountSetting?handler=SaveAccount";
    var rurl = "";
    Init = function () {
        rurl = GetUrlParam("rurl");
        if (rurl == undefined)
            rurl = "/User/MyCourseTime";

        $("#btnSave").on("click", SaveAccount);
    };
    SaveAccount = function () {
        var AliPayAccount = $("#AliPayAccount").val();
        if (AliPayAccount == "") {
            ShowInfo("请输入支付宝账户");
            return;
        }

        aq(SaveAccountUrl, { "AliPayAccount": AliPayAccount }, function () {
            ShowInfo("设置成功", null, null,2,function () {
                Back();
            });
        }, function (res) {
                if (res.IntMsg == -1) {
                    window.location.href = "/User/Login";
                }
        })
    }
    Back = function () {
        window.location.href = rurl;
    }
    Init();
})