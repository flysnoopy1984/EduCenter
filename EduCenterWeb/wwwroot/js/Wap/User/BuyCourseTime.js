$(function () {
    var BuyCourseUrl = "BuyCourseTime?handler=BuyCourse"
    Init = function () {
        var phone = $("#hUserPhone");

        if (phone.val() == "" || phone.val() == null) {
            ShowInfo("请先绑定手机号，谢谢！", null, null, 2, function () {
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
            })
        }
    }

    BuyCourseCallBack = function (res) {
        //这里和后台做了个约定，如果IntMsg == 0，标准1，summer, 2,winter
        ShowInfo("购买成功,请选择课时", null, null, 2, function () {
            if (res.IntMsg == 0)
                window.location.href = "Apply";
            else
                window.location.href = "ApplyWinterSummer?type=" + res.IntMsg;
        })
      
    }



    Init();
});