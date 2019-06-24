$(function () {
    var reqUrl = "RegPhone?handler=RequireVerifyCode";
    var submitUrl = "RegPhone?handler=SubmitVerifyCode";
    var RedirectUrl = "";



   

    SMSSuccess = function () {

        ShowInfo("手机绑定成功", null, null, 1, function () {
            window.location.href = RedirectUrl;

        });
    }


    Init = function () {

        var rurl = GetUrlParam("rurl");
        if (rurl != undefined) {
            RedirectUrl = rurl;
            //window.location.href = RedirectUrl;
        }

        $(".IconBack").on("click", function () {
            window.location.href = RedirectUrl;
        })

        InitSMS("phone_num", "code_num", "btn_GetVerifyCode", "btn_ConfirmVerifyCode", 45, SMSSuccess, null, null, reqUrl, submitUrl);
    }


  
    Init();

})