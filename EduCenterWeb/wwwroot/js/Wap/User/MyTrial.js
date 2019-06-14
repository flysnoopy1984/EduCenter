$(function () {
    var CancelTrialUrl = "MyTrial?handler=CancelTrial";
    Init = function () {
        $("#btn_Cancel").on("click", CancelTrial);
        $("#btn_ToApply").on("click", ToApply);
    }
    CancelTrial = function () {
        ShowConfirm("取消后，今天不能再申请试听，是否继续?",null,null,ConfirmYes);
    }

    ConfirmYes = function () {
        var Id = $(".Trial").attr("TrialId");

        callAjax_Query(CancelTrialUrl, {"Id":Id}, CancelTrialCallBack, "", CancelError);
    }

    CancelTrialCallBack = function () {
        ShowInfo("已为您取消申请！", null, null, 2, function () {
            window.location.reload()
        });     
    }

   
    CancelError = function (res) {
        if (res.IntMsg == -1) {
            window.location.href = "Login";
        }
    }

    ToApply = function () {

        window.location.href = "ApplyTrial";
        
    }


   
    Init();
   
})