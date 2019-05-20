$(function () {
    var saveUrl = "Manager?handler=Save";
    var delUrl = "Manager?handler=Delete";
    var getUrl = "Manager?handler=Get";
    var selCode = "";

    Init = function () {
        $("#btnNew").on("click", NewObj);
        $("#bntDelete").on("click", Delete);
        $("#btnSave").on("click", Save);
    };

   

    SelectCourse = function (Id, code) {

        var obj = $("#" + Id);

        selCode = code;

        $(".LeftList").children().removeClass("active");
        obj.addClass("active");

        $("#vCode").attr("readonly", true);

        callAjax_Query(getUrl, { "Code": code }, HandlerGet);
    };

    NewObj = function () {
        selCode = "";
        $(".LeftList").children().removeClass("active");
        emptyFields("FormFields");
        $("#vCode").attr("readonly", false);
    };

    Save = function () {

        var code = $("#vCode").val();
        var typeName = $("#vTypeName").val();

        var obj = $(".LeftList").find("a[Key='" + code + "']");
        var Id =  obj.attr("cId");

        var msg = "创建成功!";
        if (Id != undefined)
            msg = "更新成功！";

        callAjax(saveUrl, { "Id": Id, "Code": code, "TypeName": typeName }, HandlerSave, msg);
    };

    Delete = function () {
        if (selCode == "" || selCode == undefined) {
            $.alert("请先选择课类!");
            return;
        }
        callAjax(delUrl, { "delCode": selCode }, HandlerDelete, "删除成功!");
       
    };
    HandlerDelete = function (res) {

       
        emptyFields("FormFields");
        $(".LeftList").find("a[Key='" + selCode + "']").remove();
        selCode = "";

    };

    HandlerSave = function (res) {

        selCode = $("#vCode").val();
        var typeName = $("#vTypeName").val();

        var ctrlId = "cItem_" + selCode;
        var ItemName = selCode + "_" + typeName;
        var Id = res.IntMsg;

        $("#vCode").attr("readonly", true);

        if ($(".LeftList").find("a[Key='" + selCode + "']").length == 0) {

            $(".LeftList").children().removeClass("active");

            var html = '<a href="javascript:SelectCourse(\'' + ctrlId + '\',\'' + selCode + '\'); " id="' + ctrlId + '" Key="' + selCode + '" cId="' + Id + ' " class="list-group-item active">' + ItemName + '</a>';
            $(".LeftList").append(html);
        }
    };

    HandlerGet = function (res) {
        var obj = res.Entity;

        $("#vCode").val(obj.Code);
        $("#vTypeName").val(obj.TypeName);
    };

    Init();
});