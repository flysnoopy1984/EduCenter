$(function () {
    Init = function () {
        $("#btn_Sign").on("click", SignUp)
    };

    SignUp = function () {
        $(".UnSignArea").hide();
        $(".SignedArea").css("display", "flex");
    }

    Init();
});