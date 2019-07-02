$(function () {
    var RedirectUrl;
    var wxPayUrl = "";
   
    onBridgeReady = function (json) {

        var qrUserId = $("#qrUserId").val();
        var OrderNo = json.OrderNo;

        WeixinJSBridge.invoke(
            'getBrandWCPayRequest',
            json,
            function (res) {
                if (res.err_code) {
                    alert("错误，请联系管理员");
                }
                else {
                    if (res.err_msg == "get_brand_wcpay_request:ok") {
                        // alert("支付成功");
                        window.location.href = "/PP/PaySuccess?qrId=" + qrUserId + "&No=" + OrderNo;
                    }
                    if (res.err_msg == "get_brand_wcpay_request:cancel") {
                        ShowInfo("支付被取消");

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
        DoWxPay();
    }

    DoWxPay = function () {

        callAjax_Query(wxPayUrl, {}, DoWxPayCallBack);
       
    }
    DoWxPayCallBack = function () {

    }

    Init();
})