$(function () {
    var saveUrl = "Manager?handler=Save";
    var delUrl = "Manager?handler=Delete";
    var getUrl = "Manager?handler=Get";
    var selCode = "";

    Init = function () {
        laydate.render({
            elem: '#vCreatedDateTime',
        });
    };

    SelectSkillLevel = function (obj) {
        var SkillName = $(obj).text();
        $(obj).closest(".btn-group").find(".skillText").text(SkillName);

    }

    Init();

});