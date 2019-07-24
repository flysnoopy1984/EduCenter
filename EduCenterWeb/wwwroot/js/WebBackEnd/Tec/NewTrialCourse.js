$(function () {
    var QueryTrialUrl = "NewTrialCourse?handler=QueryTrialById";
    var UpdateTrialUrl = "NewTrialCourse?handler=UpdateTrial";
    Init = function () {

        laydate.render({
            elem: ".bTrialDate",
            eventElem: '#btn_DatePick',
            //done: LayDataSelect,
            theme: 'molv',
        
            isInitValue: false
        });
        var Id = GetUrlParam("Id");
        if (Id != undefined && Id != "")
            QueryTrialData(Id);

        $("#btn_Close").on("click", function () {
            parent.CloseTrialCourse();
        });

        $("#btn_Save").on("click", UpdateTrial);

    }


    QueryTrialData = function (Id) {

        callAjax_Query(QueryTrialUrl, { "Id": Id }, function (res) {

            var data = res.Entity;
            $(".Content .bUserName").val(data.UserRealName);
            $(".Content .bUserPhone").val(data.UserPhone);
            $(".Content #selCourseCode").val(data.CourseCode);
            $(".Content .bTrialDate").val(data.TrialDateStr);
            $(".Content #selSalesName").val(data.SalesOpenId);
            $(".Content #selCourseTime").val(data.Lesson);
          
            $(".Content .bStatus").text(data.TrialLogStatusName);
          
            $("#TrialId").val(Id);
            $("#UserOpenId").val(data.OpenId);
        })
    }

    UpdateTrial = function () {
        var data = new Object();
        
        data.UserRealName = $(".Content .bUserName").val();
        data.UserPhone = $(".Content .bUserPhone").val();
        data.CourseCode = $(".Content #selCourseCode").val();
        data.TrialDateTime = $(".Content .bTrialDate").val();
        data.SalesOpenId = $(".Content #selSalesName").val();
        data.SalesName = $(".Content #selSalesName option:selected").text();
        data.OpenId = $("#UserOpenId").val();
        data.Lesson = $(".Content #selCourseTime").val();
        data.Id = $("#TrialId").val();

        callAjax_Query(UpdateTrialUrl, { "updateTrial": data }, function (res) {
            var msg = "保存成功";
            if (res.IntMsg == 10)
                msg += ",并已通知老师";
            ShowInfo(msg, null, null, 1, function () {
                parent.CloseTrialCourse();
            });
        })
    }
    Init();
})