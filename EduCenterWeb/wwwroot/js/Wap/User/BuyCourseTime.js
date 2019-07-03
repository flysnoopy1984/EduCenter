$(function () {
    var BuyCourseUrl = "BuyCourseTime?handler=BuyCourse"
    Init = function () {
        var phone = $("#hUserPhone");

     //   alert(window.history(-1).href);
        if (phone.val() == "" || phone.val() == null) {
            ShowInfo("请先绑定手机号，谢谢！", null, null, 5, function () {
                window.location.href = "/Independent/RegPhone?rurl=/User/BuyCourseTime";
            });
        }

        $("#btn_Submit").on("click", BuyCourse);
    }

    BuyCourse = function () {
        var priceCode = $(".Content").find("input[type=radio]:checked").val();
        if (priceCode == undefined) {
            ShowInfo("请选择套餐", null, null, 2);
            return;
        }
        else {
            callAjax_Query(BuyCourseUrl, { "priceCode": priceCode }, BuyCourseCallBack,"", function (res) {
                if (res.IntMsg == -1) {
                    window.location.href = "Login";
                }
                else if (res.IntMsg == -2)
                    window.location.href = "/Independent/RegPhone?rurl=/User/BuyCourseTime";
            })
        }
    }

    BuyCourseCallBack = function (res) {

        var priceItem = res.Entity;
        var priceCode = priceItem.PriceCode;
        var PayAmount = 0;
        var vipQty = $("#selCourseQty").val();
        if (vipQty == undefined || vipQty == 0) {
            PayAmount = priceItem.Price;
            vipQty = 0;
        }
            

        var storeageData = { "priceCode": priceCode, "VIPQty": vipQty, "payAmount": PayAmount};
        
        SetSessionBuyCourseTime(storeageData);

        window.location.href = "PaySelection?rurl=/User/BuyCourseTime";
      
    }



    Init();
});