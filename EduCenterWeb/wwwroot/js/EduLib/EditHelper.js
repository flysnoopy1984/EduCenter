$(function () {

    var _jc = null;

    ShowInfo = function (msg, title, style,closeSec,actionHandler) {
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

    ShowConfirm = function (msg, title, style, yesHandler, noHandler, notext, yestext) {
        if (title == undefined || title == null)
            title = "确认";
        if (style == undefined || style == null)
            style = 'orange';
        if (notext == undefined || notext == null)
            notext = '不了';
        if (yestext == undefined || yestext == null)
            yestext = '是的';
        $.confirm({
            title: title,
            content: msg,
            type: style,
            buttons: {
              
                no: {
                   
                    text: notext,
                    btnClass: 'btn-blue',
                    action: noHandler,
                },
                yes: {
                    text: yestext,
                    btnClass: 'btn-red',
                    action: yesHandler,
                },
            }
        });
    }

    ShowError = function (msg,title,style) {
        if (_jc != null && _jc != undefined)
            _jc.close();

        if (title == undefined)
            title = "错误";
        if (style == undefined)
            style = 'red';

        $.dialog({
            title: title,
            content: msg,
            buttons: {
                Done: {
                    text: "关闭",
                }
            },
            type: style,
        });
    };

    CloseBlock = function () {

        $.unblockUI()
    };

    ShowBlock = function (msg) {

        var html = '<div class="fa fa-spinner fa-pulse fa-3x fa-fw " style="color:#ffffff"></div>';
     //   if (msg == undefined) msg = "处理中...";
        //var html = '<div class="loader_box">';
        //html += '<div class="loader-05"></div><span id="BlockText">' + msg + '</span>';
        //html +='<div class="fa fa-spinner fa-pulse fa-3x fa-fw "></div>'
        //html += '</div>';
        $.blockUI({
            message: html,
            css: {
                //backgroundColor:'#ffffff00',
                border: 'none',
                backgroundColor:'',
                

            }
        });
    };


    SaveProcess = function (msg) {
        if (msg == undefined) msg = "处理中请稍等...";

        _jc = $.dialog({
            title: "消息",
            content: msg,
            buttons: {
                Done: {
                    text: "关闭",
                }
            },
            type: 'green',
        });
    };

    SaveDone = function (msg, IsError) {
        if (IsError == undefined)
            IsError = false;
        if (msg == undefined) {
            msg = "处理完成";
        }
        _jc.setContent(msg);
        if (!IsError) {
            setTimeout(function () {
                _jc.close();
            }, 800)
        }

    };

    callAjaxOrig = function (url, data, handler, msg, beforeEvt, doneEvt, errEvt) {

        if (beforeEvt) beforeEvt();
        $.ajax({
            type: "post",
            url: url,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: data,

            success: function (res) {

                if (res.IsSuccess) {
                    if (handler)
                        handler(res);
                    if (doneEvt)doneEvt(msg);
                }
                else {
                    if (errEvt) errEvt(res.ErrorMsg);
                }
            },
            error: function (xhr, type) {
                if (errEvt) errEvt("系统错误"); 
                
            }

        });
    }

    callAjax = function (url, data, handler, DoneMsg){

        callAjaxOrig(url, data, handler, DoneMsg, SaveProcess, SaveDone, ShowError);
      
    };

    callAjax_Query = function (url, data, handler, msg,AfterError) {
        //if (msg == undefined) msg = "查询中.."
        ShowBlock(msg);

        $.ajax({
            type: "post",
            url: url,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: data,

            success: function (res) {

                CloseBlock();
                if (res.IsSuccess) {
                    if (handler != null)
                        handler(res);

                }
                else {
                    
                    ShowError(res.ErrorMsg);
                    if (AfterError)
                        AfterError(res);

                }
            },
            error: function (xhr, type) {
                CloseBlock();
                ShowError("系统错误");
                if (AfterError) {
                    var eObj = new Object();
                    eObj.ErrorMsg = "系统错误";
                    AfterError(eObj);
                }
                   
            }

        });
    };

    callAjax_Query_NoBlock = function (url, data, handler,needErrorMsg,ErrorMsgHandler) {

        if (needErrorMsg == undefined) needErrorMsg = true;
      

        $.ajax({
            type: "post",
            url: url,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: data,
            success: function (res) {
                if (res.IsSuccess) {
                    if (handler != null)
                        handler(res);
                }
                else {
                    if (needErrorMsg) {
                        if (ErrorMsgHandler != undefined)
                            ErrorMsgHandler(res);
                        else
                            ShowError(res.ErrorMsg);
                    }
                  
                }
            },
            error: function (xhr, type) {
                if (needErrorMsg) {
                    if (ErrorMsgHandler != undefined) {
                        var eObj = new Object();
                        eObj.ErrorMsg = "系统错误";
                        ErrorMsgHandler(eObj);
                    }
                       
                    else
                        ShowError("系统错误");
                }
            }

        });
    };

    emptyFields = function (rootId) {
        $("#" + rootId).find("input[type='text']").val("");
    };

});

