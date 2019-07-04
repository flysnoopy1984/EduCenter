$(function () {
    var urlLogin = "/user/Login?handler=UserLogin";
    Init = function () {
        $("#btnWxLogin").on("click", UserLogin);
        $("#btnLogin").on("click", UserLogin);
      
    }

    UserLogin = function () {

        callAjax_Query(urlLogin, {}, LoginCallBack, "");
       
    }

    LoginCallBack = function (res) {
        if (res.IntMsg == 10)
            window.location.href = "/Teacher/DayCourse";
        else

        window.location.href = "/User/Home";
      //  window.history.
    } 

    Init();
});