$(function () {
    var RedirectUrl;
    var wxPayUrl = "/api/wxpay/pay";
    var wxPaySuccessUrl = "/api/wxpay/PaySuccess"
   
    onBridgeReady = function (json) {

      
        WeixinJSBridge.invoke(
            'getBrandWCPayRequest',
            json,
            function (res) {
               
                if (res.err_code) {
                    ShowError("错误，请联系管理员");
                }
                else {
                    if (res.err_msg == "get_brand_wcpay_request:ok") {
                        callAjax_Query(wxPaySuccessUrl, { "OrderId":json.OrderNo})
                        window.location.href = "PayCourseSuccess";
                    }
                    if (res.err_msg == "get_brand_wcpay_request:cancel") {
                        ShowInfo("您已取消支付！");
                    }
                }

            }
        );
    }

    Init = function () {
        var rurl = GetUrlParam("rurl");
        if (rurl != undefined) {
            RedirectUrl = rurl;
         
        }

        $(".IconBack").on("click", function () {
            window.location.href = RedirectUrl;
        })

        $(".payItem").on("click", function (e) {
            var radio = $(e.currentTarget).find("input[type=radio]");
            radio.prop('checked', true);
        })

        $("#btn_Submit").on("click", Pay);
    }

    Pay = function () {
        var payway = $(".payselection .payItem input[type=radio]:checked").val();
        if (payway == "wx")
            DoWxPay();
        else {
            ShowInfo("即将开通！");
        }
    }

    DoWxPay = function () {

        var storageData = GetSessionBuyCourseTime();
        if (storageData == null || storageData == undefined) {
            ShowError("无法支付，请返回重新尝试！");
            return;
        }
        callAjax_Query_API(wxPayUrl, {
            "ItemDes": "课时费用",
            "PriceCode": storageData.priceCode,
            "PayAmount": storageData.payAmount,
            "VIPQty": storageData.VIPQty
        }, DoWxPayCallBack, function (res) {
            if (res.IntMsg == -1) {
                window.location.href = "Login";
            }
        });
       
    }
    DoWxPayCallBack = function (json) {

       
        onBridgeReady(json);
       
    }

    Init();
})