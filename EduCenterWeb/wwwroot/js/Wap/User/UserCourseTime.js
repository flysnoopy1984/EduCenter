$(function () {
    var QueryReChargeListUrl = "MyCourseTime?handler=QueryReChargeList";
    var QueryConsumeListUrl = "MyCourseTime?handler=QueryConsumeList";
    var ReChargeData = null;
    var ComsumeData = null;
    Init = function () {

        $("#btnToBuy").on("click", function() {
            window.location.href = "BuyCourseTime";
        });
        $("#ReChargeBar").on("click", SwitchQueryReChargeList);

        $("#ComsumeBar").on("click", SwitchQueryConsumeList);

    }

    SwitchQueryReChargeList =function(e){
        var btnI = $(e.currentTarget).children("i");

        if (btnI.hasClass("fa-caret-left")) {
            //Icon图标变换
            btnI.removeClass("fa-caret-left");
            btnI.addClass("fa-caret-down");

            if (ReChargeData == null) {
                callAjax_Query(QueryReChargeListUrl, { "maxLine": 10 }, SwitchQueryReChargeListCallBack, "", function (res) {
                    if (res.IntMsg == -1) {
                        window.location.href = "Login";
                    }
                });
            }
            else {
                $(".ReChargeList .DataList").slideDown();
            }
        }
        else {
            btnI.removeClass("fa-caret-down");
            btnI.addClass("fa-caret-left");
            $(".ReChargeList .DataList").slideUp();
        }
    }

    SwitchQueryReChargeListCallBack = function (res) {

        ReChargeData = res.List;
        if (ReChargeData.length == 0) {
            var item = $("#HideData .NoData").clone();
            $(".ReChargeList .DataList").append(item);
        }
        else {
            $.each(ReChargeData, function (i) {
                var item = $("#HideData .ReChargeOneRow").clone();
                item.find(".ItemName").text(ReChargeData[i].ItemName);
                item.find(".BuyDate").text(ReChargeData[i].CreateDateTime);
                item.find(".Amount").text(ReChargeData[i].Amount);
                item.append("<hr />");
                $(".ReChargeList .DataList").append(item);
            });
        }
        $(".DataList").slideDown();
    }

    //消耗
    SwitchQueryConsumeList = function (e) {
        var btnI = $(e.currentTarget).children("i");

        if (btnI.hasClass("fa-caret-left")) {
            //Icon图标变换
            btnI.removeClass("fa-caret-left");
            btnI.addClass("fa-caret-down");

            if (ComsumeData == null) {
                callAjax_Query(QueryConsumeListUrl, { "maxLine": 10 }, SwitchQueryConsumeListCallBack, "", function (res) {
                    if (res.IntMsg == -1) {
                        window.location.href = "Login";
                    }
                });
            }
            else {
                $(".ConsumeList .DataList").slideDown();
            }
        }
        else {
            btnI.removeClass("fa-caret-down");
            btnI.addClass("fa-caret-left");
            $(".ConsumeList .DataList").slideUp();
        }
    }
    SwitchQueryConsumeListCallBack = function(res){
        ComsumeData = res.List;
        if (ComsumeData.length == 0) {
            var item = $("#HideData .NoData").clone();
            $(".ConsumeList .DataList").append(item);
        }
        else {
            $.each(ComsumeData, function (i) {
                var item = $("#HideData .ComsumeOneRow").clone();
                item.find(".CourseName").text("【" + ComsumeData[i].CourseSchudeuleType + "】 | " + ComsumeData[i].CourseName);
                item.find(".CourseDate").text(ComsumeData[i].CourseDate + "(" + ComsumeData[i].CourseTime+")");
                item.find(".CourseStatus").text(ComsumeData[i].CourseStatus);
                item.append("<hr />");
                $(".ConsumeList .DataList").append(item);
            });
        }
        $(".DataList").slideDown();
    }

    Init();
})