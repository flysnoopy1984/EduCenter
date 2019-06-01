﻿$(function () {

    var _jc = null;

    ShowError = function (msg) {
        if (_jc != null && _jc != undefined)
            _jc.close();

        $.dialog({
            title: "错误",
            content: msg,
            buttons: {
                Done: {
                    text: "关闭",
                }
            },
            type: 'red',
        });
    };

    CloseBlock = function () {

        $.unblockUI()
    };

    ShowBlock = function (msg) {

        if (msg == undefined) msg = "处理中...";
        var html = '<div class="loader_box">';
        html += '<div class="loader-05"></div><span id="BlockText">' + msg + '</span>';
        html += '</div>';
        $.blockUI({
            message: html,
            css: {
                border: 'none',

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

    callAjax = function (url, data, handler, msg){

        callAjaxOrig(url, data, handler, msg, SaveProcess, SaveDone, ShowError);
      
    };

    callAjax_Query = function (url, data, handler, msg) {
        if (msg == undefined) msg = "查询中.."
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

                }
            },
            error: function (xhr, type) {
                CloseBlock();
                ShowError("系统错误");
            }

        });
    };

    emptyFields = function (rootId) {
        $("#" + rootId).find("input[type='text']").val("");
    };

});

