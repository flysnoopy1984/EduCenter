$(function () {

    var _jc = null;
    SaveAlert = function ()
    {
        _jc = $.dialog({
            title: "消息",
            content: "数据处理中请稍等...",
            buttons: {
                Done: {
                    text: "关闭",  
                }
            },
            type: 'red',
            onOpen: function () {
               
            }
        });
    }
    AfterSave = function () {
        _jc = $.dialog({
            title: "消息",
            content: "处理成功！",
            buttons: {
                Done: {
                    text: "关闭",
                }
            },
            type: 'red',
            onOpen: function () {

            }
        });
    }

});

