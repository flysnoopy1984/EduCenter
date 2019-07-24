$(function () {
    var wxConfigUrl = "/api/WXJS/InitConfig";
    var wxPayUrl = "/api/wxpay/JSPay";
    var wxPaySuccessUrl = "/api/wxpay/PaySuccess"
    var amt;
    var ct;
    Init = function () {
        amt = GetUrlParam("amt");
        ct = GetUrlParam("ct");
        if (ct == undefined)
            ct = -1;
        if (amt == 0 || amt == undefined) {
            ShowInfo("出错了！无支付金额！");
            return;
        }
        if (ct == -1) {
            $(".courseTime").closest(".oneLine").hide();
        }
        else
            $(".courseTime").text(" [" + ct + "]节课");
        $(".payAmount").text(amt);

        callAjax_Query_API(wxConfigUrl + "?url=" + window.location.href, {}, function (res) {
            var jsList = new Array();
            jsList.push("chooseWXPay");

            wx.config({
                debug: false,
                appId: res.appId,
                timestamp: res.timestamp,
                nonceStr: res.nonceStr,
                signature: res.signature,
                jsApiList: jsList
            });
        },
        function (res) {
            if (res.IntMsg == -1) {
                window.location.href = "Login";
            }
        });
       

    };

    SurePay = function () {
        wxPayUrl += "?feeAmt=" + parseFloat(amt) + "&courseTime=" + ct;

        callAjax_Query_API(wxPayUrl, {}, function (json) {
            //  var orderId = json.EduOrderNo;
            wx.chooseWXPay({
                timestamp: json.timeStamp, // 支付签名时间戳，注意微信jssdk中的所有使用timestamp字段均为小写。但最新版的支付后台生成签名使用的timeStamp字段名需大写其中的S字符
                nonceStr: json.nonceStr, // 支付签名随机串，不长于 32 位
                package: json.package, // 统一支付接口返回的prepay_id参数值，提交格式如：prepay_id=\*\*\*）
                signType: json.signType, // 签名方式，默认为'SHA1'，使用新版支付需传入'MD5'
                paySign: json.paySign, // 支付签名
                success: function (payRes) {
                    callAjax_Query_API(wxPaySuccessUrl,
                        { "OrderId": json.EduOrderNo, "IsJSPay": true }, function () {

                            window.location.href = "/User/PayCourseSuccess";
                        })
                }
            });
        });
    }

    BackToHome = function () {
        window.location.href = "/User/Home";
    }
    Init();
});