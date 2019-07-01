$(function () {
    var urlLogin = "/user/Login?handler=UserLogin";
    Init = function () {
        $("#btnWxLogin").on("click", UserLogin);
        $("#btnLogin").on("click", UserLogin);
      
    }

    UserLogin = function () {

        callAjax_Query(urlLogin, {}, LoginCallBack, "登陆中");
       
    }

    LoginCallBack = function () {
        window.location.href = "/User/Home";
      //  window.history.
    } 

    Init();
});