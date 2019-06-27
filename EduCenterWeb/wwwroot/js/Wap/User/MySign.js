$(function () {
    var InitPageUrl = "MySign?handler=InitPage";
    Init = function () {

        callAjax_Query(InitPageUrl, {}, InitPageCallBack, "", function (res) {
            if (res.IntMsg == -1)
                window.location.href = "Login";
        });
       // $("#btn_Sign").on("click", SignUp)
       // $("#HideData ")
    };

    InitPageCallBack = function (res) {

    }


    SignUp = function () {
        $(".UnSignArea").hide();
        $(".SignedArea").css("display", "flex");
    }

    Init();
});