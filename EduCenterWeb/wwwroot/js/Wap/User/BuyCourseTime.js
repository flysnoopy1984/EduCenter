$(function () {
  
    Init = function () {
        var phone = $("#hUserPhone");
    
        if (phone.val() == "" || phone.val() == null) {
            ShowInfo("请先绑定手机号，谢谢！", null, null, 2, function () {
                window.location.href = "/Independent/RegPhone";
            });
        }
      

    }



    Init();
});