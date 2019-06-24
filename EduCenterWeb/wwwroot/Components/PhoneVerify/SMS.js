$(function () {
   

    var InitCount = 60;
    var countDown = InitCount;

    var PhoneCtrl = null;
    var CodeCtrl = null;
    var btnGetVC = null;
    var btnSubmitVC = null;
    var sucEvent = null;
    var befEvent = null;
    var endEvent = null;
    var RequireVerifyCodeUrl = "RegPhone?handler=RequireVerifyCode";
    var SubmitVerifyCodeUrl = "RegPhone?handler=SubmitVerifyCode";
  
    settime = function () {
        if (countDown == 0) {
            countDown = InitCount;
            btnGetVC.text('获取验证码');
            btnGetVC.attr("disabled", false);
            //var html = $('.o2o_modal').html();
            //popModel.setContent(html);
            return;
        } else {
            countDown--;

            btnGetVC.attr("disabled", true);
            btnGetVC.text("重新获取(" + countDown + ')s');
            //var html = $('.o2o_modal').html();
            //popModel.setContent(html);

        }
        setTimeout(function () {
            settime();
        }, 1000)
    }

    InitSMS = function (phoneCtrlId, codeCtrlId, btnGetVCId, btnSubmitVCId, _InitCount,
        _sucEvent, _befEvent, _endEvent, _reqUrl,_submitUrl) {

        PhoneCtrl = $("#" + phoneCtrlId);
        CodeCtrl = $("#" + codeCtrlId);
        btnGetVC = $("#" + btnGetVCId);
        btnSubmitVC = $("#" + btnSubmitVCId);
        btnSubmitVC.attr("disabled", true);
       
        
        if (_InitCount != undefined && _InitCount != 0)
        {
            InitCount = _InitCount;
            countDown = InitCount;
        }
           
        if (_sucEvent != undefined && _sucEvent != null)
            sucEvent = _sucEvent;
        if (_befEvent != undefined && _befEvent != null)
            befEvent = _befEvent;
        if (_endEvent != undefined && _endEvent != null)
            endEvent = _endEvent;

        if (_reqUrl != undefined && _reqUrl != null)
            RequireVerifyCodeUrl = _reqUrl;

        if (_submitUrl != undefined && _submitUrl != null)
            SubmitVerifyCodeUrl = _submitUrl;

        $(document).on("click", "#" + btnGetVCId, RequireVerifyCode);
        $(document).on("click", "#" + btnSubmitVCId, SubmitVerifyCode);
    }

    RequireVerifyCode = function (e) {

     
        var Phone = PhoneCtrl.val();
        if (Phone == '') {
            alert("手机号不能为空!");
            PhoneCtrl.focus();
            return;
        }
        if (!isPhoneNo(Phone))
        {
            alert("手机格式不正确!");
            PhoneCtrl.focus();
            return;
        }
        btnGetVC.attr("disabled", true);

       // var url = "/API/SMS/SentSMS_IQBPay_BuyerOrder";

        $.ajax({
            type: 'get',
            dataType: "json",
            data: { "mobilePhone": Phone, "IntervalSec": countDown},
            url: RequireVerifyCodeUrl,
            success: function (res) {
              
                if (res.IsSuccess) {
                    var result = res.Entity;
                    btnSubmitVC.attr("disabled", false);
                    if (result.SMSVerifyStatus == 2) {
                        settime();

                    }
                    else if (result.SMSVerifyStatus == 1) {

                        alert("请不要重复发送");
                        countDown = result.RemainSec;
                        settime();

                    }
                    else {
                        btnGetVC.attr("disabled", false);
                    }
                }
                else {
                    if (res.IntMsg == -1) {
                        ShowInfo(res.ErrorMsg, null, null, 1, function () {

                        })
                    }
                    else

                   
                }
                   
              

            },
            error: function (xhr, type) {
                btnGetVC.attr("disabled", false);
                alert("系统错误！");

            }
        });
    }

    SubmitVerifyCode = function(e){
        //   var Phone = $("#" + e.data.phoneCtrlId).text();
        var Phone = PhoneCtrl.val();
        if (Phone == '') {
            return;
        }
        var Code = CodeCtrl.val();

      //  var url = "/API/SMS/IQBPay_ConfirmVerifyCode";

        if (befEvent)
            befEvent();


        $.ajax({
            type: 'get',
            dataType: "json",
            data: { "mobilePhone": Phone, "Code": Code },
            url: SubmitVerifyCodeUrl,
            success: function (res) {

             
                if (res.IsSuccess) {
                    var data = res.Entity;
                    if (endEvent)
                        endEvent();
                    switch (data.SMSVerifyStatus) {
                        case -1:
                            alert("验证码未知错误，请联系管理员");
                            return data.SMSVerifyStatus;
                        case 5:
                            alert("验证码已失效，请重新获取！");
                            CodeCtrl.val("");
                            return data.SMSVerifyStatus;
                        case 3:
                            ////验证成功
                            if (sucEvent)
                                sucEvent();
                            else {
                                alert("验证通过");
                            }

                            return data.SMSVerifyStatus;
                        case 4:
                            alert("验证码不正确，请重新填写！");
                            CodeCtrl.select();
                            CodeCtrl.focus();
                            return data.SMSVerifyStatus;

                    }

                }
                else
                    alert(res.ErrorMsg);
            },
            error: function (xhr, type) {
                if (endEvent)
                    endEvent();
                alert("系统错误！");

            }
        });
    }

    ShowInfo = function (msg, title, style, closeSec, actionHandler) {
        if (title == undefined || title == null)
            title = "信息";
        if (style == undefined || style == null)
            style = 'orange';

        var jc = $.alert({
            title: title,
            content: msg,
            type: style,
            buttons: {
                ok: function () {
                    if (actionHandler)
                        actionHandler();
                },
            }

        });
        if (closeSec != undefined && closeSec > 0) {
            setTimeout(function () {
                if (!jc.isClosed()) {
                    jc.close();
                    if (actionHandler)
                        actionHandler();
                }

            }, 1000 * closeSec);
        }
    }

});




