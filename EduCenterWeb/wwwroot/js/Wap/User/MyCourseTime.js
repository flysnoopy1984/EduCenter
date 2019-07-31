$(function () {
    var QueryReChargeListUrl = "MyCourseTime?handler=QueryReChargeList";
    var QueryConsumeListUrl = "MyCourseTime?handler=QueryConsumeList";
    var QueryAmountListUrl = "MyCourseTime?handler=QueryAmountList";

    var CheckUserAccountUrl = "MyCourseTime?handler=CheckUserAccount";
    var TransferAmountUrl = "MyCourseTime?handler=TransferAmount";
    var ReChargeData = null;
    var ComsumeData = null;

    Init = function () {

        $("#btnToBuy").on("click", function() {
            window.location.href = "BuyCourseTime";
        });
        $("#ReChargeBar").on("click", SwitchQueryReChargeList);

        $("#ComsumeBar").on("click", SwitchQueryConsumeList);

        $("#AmountBar").on("click", SwitchAmountList);

        $("#btnGetReword").on("click", GetReward);

    }
    //奖金Begin
    GetReward = function () {
        aq(CheckUserAccountUrl, {}, function (res) {
            var alipayAccount = res.Entity.AliPayAccount;
            if (alipayAccount.RemainRewards <= 0) {
                ShowInfo("没有可提取的金额");
                return;
            }

            ShowConfirm("是否将余额转入此支付宝账户【" + alipayAccount + "】?", null, null, TransAmountToUser,
                function () {
                    window.location.href = "/User/AlipayAccountSetting?rurl=/User/MyCourseTime";
                },"重新设置账户","我确定")
        }, function (res) {
                if (res.IntMsg == -1) {
                    window.location.href = "/User/Login";
                }
                else if (res.IntMsg == -2) {
                    window.location.href = "/User/AlipayAccountSetting?rurl=/User/MyCourseTime";
                }

            });
        //ShowInfo("暂时无法提现，如有提现需求请到店联系客服");
    }
    TransAmountToUser = function () {
        aq(TransferAmountUrl, {}, function (res) {
            ShowInfo("提取成功", null, null, 2, function () {

                window.location.reload();
            });
        });
    }

    SwitchAmountList = function (e) {
        var btnI = $(e.currentTarget).children("i");

        if (btnI.hasClass("fa-caret-left")) {
            //Icon图标变换
            btnI.removeClass("fa-caret-left");
            btnI.addClass("fa-caret-down");

            if (ReChargeData == null) {
                callAjax_Query(QueryAmountListUrl, { "maxLine": 10 }, SwitchAmountListCallBack, "", function (res) {
                    if (res.IntMsg == -1) {
                        window.location.href = "Login";
                    }
                });
            }
            else {
                $(".AmountList .DataList").slideDown();
            }
        }
        else {
            btnI.removeClass("fa-caret-down");
            btnI.addClass("fa-caret-left");
            $(".AmountList .DataList").slideUp();
        }
    }

    SwitchAmountListCallBack = function (res) {
        var data = res.List;
        if (data.length == 0) {
            var item = $("#HideData .NoData").clone();
            $(".AmountList .DataList").append(item);
        }
        else {
            $.each(data, function (i) {
                var item = $("#HideData .AmountOneRow").clone();
                item.find(".TransType").text(data[i].TransTypeName);
                var amountDesc = "";
                var transAmountObj = item.find(".TransAmount");
                if (data[i].transDirection > 0) {
                    transAmountObj.addClass("InAmount");
                    amountDesc = "+" + data[i].Amount;
                }
                else {
                    amountDesc = "-" + data[i].Amount;
                    transAmountObj.addClass("OutAmount");
                }
                   

                transAmountObj.text(amountDesc);
                item.find(".TransDate").text(data[i].TransDate);
                item.append("<hr />");
                $(".AmountList .DataList").append(item);
            });
        }
        $(".DataList").slideDown();
    }

    //奖金End
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