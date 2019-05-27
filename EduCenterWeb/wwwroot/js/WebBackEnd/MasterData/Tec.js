$(function () {
    var saveUrl = "Tec?handler=Save";
    var delUrl = "Tec?handler=Delete";
    var getUrl = "Tec?handler=Get";

    var saveSkillLevelUrl = "Tec?handler=SaveSkillLevel";
    var selCode = "";

    Init = function () {
        //laydate.render({
        //    elem: '#vCreatedDateTime',
        //});
        $(".RightDetail").hide();

        $("#btnSave").on("click", Save);
        $("#btnNew").on("click", NewTec);
    };

    SelectSkillLevel = function (obj) {
        var sk = $(obj)
        var SkillName = sk.text();
        sk.closest(".btn-group").find(".skillText").text(SkillName);

        var curType = sk.attr("curType");
        var skl = sk.attr("skl");
        var tsId = sk.attr("tsId");

        var data = {
            "TecCode": selCode,
            "CourseType": curType,
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

        if (selCode == "") {
            ShowError("没有选择任何老师！");
            return;
        }
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

    NewTec = function () {
       
        var html = '<div id="InviteTecQR" style="text-align:center;">'
            html += '<img src="/Files/QR/InviteTec/WXInvite.png" />'
        html += '</div>';

        $.confirm({
            title:"请让老师扫描",
            content: html,
            buttons: {
                close: {
                    btnClass: 'btn-warning',
                    text: "关闭",
                    action: function () {
                        window.location.reload();
                    },
                },
            },
          
            type: 'red',
        });
    }

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
            var CourseType = ts.CourseType;
            var SkillLevel = ts.SkillLevel;


            //和页面约定
            var levelList = $("#skCouse_" + CourseType).find("li a");
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