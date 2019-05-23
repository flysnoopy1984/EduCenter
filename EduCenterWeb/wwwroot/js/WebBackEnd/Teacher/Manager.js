$(function () {
    var saveUrl = "Manager?handler=Save";
    var delUrl = "Manager?handler=Delete";
    var getUrl = "Manager?handler=Get";

    var saveSkillLevelUrl = "Manager?handler=SaveSkillLevel";
    var selCode = "";

    Init = function () {
        //laydate.render({
        //    elem: '#vCreatedDateTime',
        //});
        $(".RightDetail").hide();

        $("#btnSave").on("click", Save);
    };

    SelectSkillLevel = function (obj) {
        var sk = $(obj)
        var SkillName = sk.text();
        sk.closest(".btn-group").find(".skillText").text(SkillName);

        var curCode = sk.attr("curCode");
        var skl = sk.attr("skl");
        var tsId = sk.attr("tsId");

        var data = {
            "TecCode": selCode,
            "CourseCode": curCode,
            "SkillLevel": skl,
            "Id": tsId,
        };
        callAjaxOrig(saveSkillLevelUrl, data, null, null, null, null, ShowError);

    };

    SelectTec = function (obj) {

        var tecObj = $(obj);
      
        selCode = tecObj.attr("Key");

        $(".LeftList").children().removeClass("active");
        tecObj.addClass("active");

        //部分Fields只读
        $("#vCode").attr("readonly", true);
        $("#vWXName").attr("readonly", true);

        callAjax_Query(getUrl, { "Code": selCode }, HandlerGet);
    };

   

    Save = function () {

        var phone = $("#vPhone").val();
        var Name = $("#vName").val();
        var tecInfo = {
            "Code": selCode,
            "Name": Name,
            "Phone": phone,
        };
        var data = {
            "TecInfo": tecInfo,
            "TecSkillList":null
        }
     
        callAjax(saveUrl, data, HandlerSave, "保存成功！");
    };

    HandlerSave = function () {

    };

    HandlerGet = function (res) {
        var tec = res.Entity.TecInfo;
        $(".RightDetail").show();
        $("#vCode").val(tec.Code);
        $("#vWXName").val(tec.WxName);
        $("#vPhone").val(tec.Phone);
        $("#vName").val(tec.Name);
        var joinDate = tec.CreatedDateTimeStr.split(" ")[0];
        $("#vCreatedDateTime").val(joinDate);

        $("input[name='sex'][value='" + tec.Sex + "']").attr("checked", true);

        var tecSkill = res.Entity.TecSkillList;
        $.each(tecSkill, function (i) {

            var ts = tecSkill[i];
            var tsId = ts.Id;
            var TecCode = ts.TecCode;
            var CourseCode = ts.CourseCode;
            var SkillLevel = ts.SkillLevel;


            //和页面约定
            var levelList = $("#skCouse_" + CourseCode).find("li a");
            $.each(levelList, function () {
                var itemLevel = $(this).attr("skl");
                if (itemLevel == SkillLevel) {
                    $(this).closest(".btn-group").find(".skillText").text($(this).text());
                }
                $(this).attr("tsId", tsId);
            });

        })

    };

    Init();

});