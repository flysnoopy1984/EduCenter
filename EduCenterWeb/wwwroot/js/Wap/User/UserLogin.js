$(function () {
    var urlLogin = "/user/Login?handler=UserLogin";
    Init = function () {
        $(".wxicon").on("click", UserLogin);
    }

    UserLogin = function () {

        callAjax_Query(urlLogin, {}, LoginCallBack, "登陆中");
       
    }

    LoginCallBack = function () {
        window.location.href = "/User/MyCourse";
      //  window.history.
    } 

    Init();
});