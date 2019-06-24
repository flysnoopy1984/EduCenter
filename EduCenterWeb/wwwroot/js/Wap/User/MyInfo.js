$(function () {
    Init = function () {
        $("#btn_EditPhone").on("click", function () {
            window.location.href = "/Independent/RegPhone?rurl=/User/MyInfo";
        });
        $("#AddBaby").on("click", function () {
            $("#SecBaby").slideDown();
        });

        $("#DelBaby").on("click", function () {
            $("#SecBaby").slideUp();

        });

    }
    Init();
})