$(function () {
    var genQRUrl = "PayQRSetting?handler=QRGen";
    Init = function () {
        $("#payQRGen").on("click", genQR);

        //var UserRole = $("#UserRole").val();
        //if (UserRole != 100 && UserRole != 20) {
        //    ShowInfo("没有权限，即将跳转...", null, null, 3, function () {
        //        window.location.href = "/User/Home";
        //    });
        //    return;
        //}
    }

    genQR = function () {
        var payAmount = $("#payAmount").val();
        if (payAmount == "") {
            ShowInfo("请填写付款金额", null, null, 2);
            return;
        }
        var vAmount = parseFloat(payAmount);
        if (vAmount == undefined || vAmount <= 0) {
            ShowInfo("付款金额必须大于零", null, null, 2)
            return;
        };

        var courseTime = $("#courseTime").val();
        if (!$("#cb_NeedCourseTime").is(':checked')) {
            if (courseTime <= 0 || courseTime == undefined) {
                ShowInfo("课时没有填写", null, null, 2)
                return;
            }
        }
        else
            courseTime = -1;
       
       

        aq(genQRUrl, {
            "payAmount": vAmount, "courseTime": courseTime}, function (res) {
            var url = res.SuccessMsg;
            $("#QRImg").attr("src", url);
        });
    }

    Init();
})