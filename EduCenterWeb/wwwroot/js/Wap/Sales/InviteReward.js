$(function () {

    Init = function () {
        $("#btnBack").on("click", function () {
            window.history.go(-1);
        })
        $("#btnGetTrailReward").on("click", toMyAccount);
        $("#btnGetPaiedReward").on("click", toMyAccount);
    }
    toMyAccount = function () {
        window.location.href = "/User/MyCourseTime";
    }

    Init();

})