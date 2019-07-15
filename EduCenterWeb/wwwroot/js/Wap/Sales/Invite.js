$(function () {
    var genQRUrl = "Invite?handler=GenerateQR";

    Init = function () {

        $("#btnGenQR").on("click", GenQR);
    };

    GenQR = function () {

        aq(genQRUrl, {},
            function (res) {
                ShowInfo("二维码已生产", null, null, 2, function () {
                    window.location.reload();
                })   
            },
            function (res) {
                if (res.IntMsg == -1) {
                    window.location.href = "/User/Login";
                }
                else if (res.IntMsg == -2) {
                    window.location.href = "/Independent/RegPhone?rurl=/Sales/Invite";
                }
            },
            3)
    }
    ShowLogDetail = function () {
        ShowInfo("开发中，敬请期待！", null, null, 1);
    }

    Init();

})