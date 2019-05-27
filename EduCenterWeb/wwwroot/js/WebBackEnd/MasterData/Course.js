$(function () {
    var saveUrl = "Course?handler=Save";
    var delUrl = "Course?handler=Delete";
    var getUrl = "Course?handler=Get";
    var selType = 0;

    Init = function () {
        //$("#btnNew").on("click", NewObj);
        //$("#bntDelete").on("click", Delete);
        $("#btnSave").on("click", Save);
        $("#btn_AddLevel").on("click", NewLevel);
    };

   

    SelectCourse = function (obj) {

        selType = $(obj).attr("cType");

   
        $(".LeftList").children().removeClass("active");
        $(obj).addClass("active");

      //  $("#selCourseType").val(selType);
        //$("#vCode").attr("readonly", true);

        callAjax_Query(getUrl, { "courseType": selType }, HandlerGet);
    };

    //NewObj = function () {
    //    //selCode = "";
    //    //$(".LeftList").children().removeClass("active");
    //    //emptyFields("FormFields");
    //    //$("#vCode").attr("readonly", false);
    //};

    Save = function () {
      
        var courseType = selType;
        if (courseType == 0) {
            ShowError("请选择课类型");
            return;
        }
        var code;
        var name;
        var level;
        var Id;
        var msg = "保存成功!";
    

        var data = new Array();
        $(".FormFields").children().each(function () {
            Id = $(this).attr("cId");
            code = $(this).find("#vCode").val();
            name = $(this).find("#vName").val();
            level = $(this).find(".selLevel").val();
            var obj = {
                "Id":Id,"Code": code, "Name": name, "CourseType": courseType, "Level": level
            };
         
            data.push(obj);
        });

        callAjax(saveUrl, {"list":data} , HandlerSave, msg);
    };

    Delete = function () {
        if (selCode == "" || selCode == undefined) {
            $.alert("请先选择课类!");
            return;
        }
        callAjax(delUrl, { "delCode": selCode }, HandlerDelete, "删除成功!");
       
    };

    //Level start
    NewLevel = function () {
        var last = $("#FormFields .FieldRow:last");
        if (last.length == 0) {
            last = $(".HideData .FieldRow");
        }
        var level = last.clone();
        level.show();
        $("#FormFields").append(level);
    }
    DelLevel = function (obj) {
       
      
        $(obj).closest(".FieldRow").remove();
    }
    //Level end

    HandlerDelete = function (res) {

       
        emptyFields("FormFields");
        $(".LeftList").find("a[Key='" + selCode + "']").remove();
        selCode = "";

    };

    HandlerSave = function (res) {
        var list = res.List;
        $(".FormFields").children().each(function () {
            var row = $(this);
            var level = row.find(".selLevel").val();

            $.each(list, function (i) {
                var obj = list[i];
                if (obj.Value == level) {
                    row.attr("cId", obj.Key);
                    return false;
                }

            });
        });
     


        //selCode = $("#vCode").val();
        //var typeName = $("#vName").val();

        //var ctrlId = "cItem_" + selCode;
        //var ItemName = selCode + "_" + typeName;
        //var Id = res.IntMsg;
   

        //$("#vCode").attr("readonly", true);

        //if ($(".LeftList").find("a[Key='" + selCode + "']").length == 0) {

        //    $(".LeftList").children().removeClass("active");

        //    var html = '<a href="javascript:SelectCourse(\'' + ctrlId + '\',\'' + selCode + '\'); " id="' + ctrlId + '" Key="' + selCode + '" cId="' + Id + ' " class="list-group-item active">' + ItemName + '</a>';
        //    $(".LeftList").append(html);
        //}
    };

    HandlerGet = function (res) {

       
        var list = res.List;

        var html = $(".HideData .FieldRow");
        var root = $(".FormFields");

        
        root.empty();

        $.each(list, function (i) {
            var obj = list[i];

            var row = html.clone();
            row.attr("cId", obj.Id);
            row.find("#vCode").val(obj.Code);
            row.find("#vName").val(obj.Name);
            row.find(".selLevel").val(obj.Level);
            row.show();

            root.append(row);
        });
   

    };

    Init();
});