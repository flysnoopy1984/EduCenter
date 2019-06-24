$(function () {
    var reqUrl = "RegPhone?handler=RequireVerifyCode";
    var submitUrl = "RegPhone?handler=SubmitVerifyCode";
    var RedirectUrl = "";

    GetUrlParam = function (name, nd) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg);  //匹配目标参数
        if (r != null) {
            if (nd) {
                return unescape(decodeURI(r[2]));
            }
            return unescape(r[2]);
        }

        return null; //返回参数值
    }

   

    SMSSuccess = function () {

    }


    Init = function () {

        //var rurl = GetUrlParam("rurl");
        //if (rurl != undefined) {
        //    RedirectUrl = rurl;
        //    window.location.href = RedirectUrl;
        //}

        $(".IconBack").on("click", function () {
            window.history.go(-1);
        })
           
       
       
        InitSMS("phone_num", "code_num", "btn_GetVerifyCode", "btn_ConfirmVerifyCode", 45, SMSSuccess, null, null, reqUrl, submitUrl);
    }


  
    Init();

})