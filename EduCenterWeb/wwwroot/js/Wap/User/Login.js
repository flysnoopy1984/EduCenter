﻿$(function () {
    var urlLogin = "/user/Login?handler=UserLogin";
    //微信菜单会传值过来
    var Transfer;
    Init = function () {
        $("#btnWxLogin").on("click", UserLogin);
        $("#btnLogin").on("click", UserLogin);


       // wx.miniProgram.postMessage({ data: 'miniLoginDone' });
        wx.miniProgram.navigateTo({ url: '../index/GetUnionId?login=done' })

    }

    UserLogin = function () {

        callAjax_Query(urlLogin, {}, LoginCallBack, "");
       
    }

    LoginCallBack = function (res) {
        if (res.IntMsg == 10)
            window.location.href = "/Teacher/DayCourse";
        else if (res.IntMsg == 1)
            window.location.href = "/User/MyCourse";
        else
            window.location.href = "/User/Home";
      //  window.history.
    } 

    Init();
});