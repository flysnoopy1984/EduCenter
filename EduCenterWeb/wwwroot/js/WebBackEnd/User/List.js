$(function () {
    var glaypage;
    var pageIndex = 1;
    var pageSize = 20;
    var QueryUserListUrl = "List?handler=QueryUserList";
    var UpdateUserUrl = "List?handler=UpdateUser";

    Init = function () {
        layui.use('laypage', function () {
            glaypage = layui.laypage;
        });

        $("#btnQuery").on("click", QueryUserList);
    }
    QueryUserList = function () {
        var UserName = $("#UserName").val();
        var data = {
            "userName": UserName,
            "pageIndex": pageIndex,
            "pageSize": pageSize

        };
        callAjax_Query(QueryUserListUrl, data, QueryUserListCallBack);
    }

    QueryUserListCallBack = function (res) {
        var dataList = res.List;
        var header = $("#UserListTable #Header");

        $("#UserListTable tr td").remove();

        $.each(dataList, function (i) {
            var data = dataList[i];

            var tr = $("#HideData #DataTable tr").clone();
           
            tr.attr("openId", data.userOpenId);
            tr.find(".WxName").text(data.WxName);
            tr.find(".BabyName").text(data.BabyName);
            tr.find(".MemberType").val(data.MemberType);
            tr.find(".VipPrice").val(data.VipPrice);
            tr.find(".RemainTimeStd").val(data.RemainTimeStd);
            tr.find(".RemainTimeSummer").val(data.RemainTimeSummer);
            tr.find(".RemainTimeWinter").val(data.RemainTimeWinter);
            tr.find(".UserRole").text(data.UserRoleName);
            tr.find("#btn_Save").on("click", SaveUser);

            var DeadLineStd = tr.find(".DeadLineStd");
            DeadLineStd.attr("id", "showDate" + i);
            DeadLineStd.val(data.DeadLineStd);

            var dateBtn = DeadLineStd.next();
            dateBtn.attr("id", "btnDate" + i);

            header.after(tr);

            laydate.render({
                elem: "#showDate" + i,
                eventElem: "#btnDate" + i,
                min: 0,
                theme: 'molv',
                trigger: 'click',
              
            });
           
        });

        glaypage.render({
            elem: 'Pager',
            layout: ['prev', 'page', 'next', 'count', 'skip'],
            limit: pageSize,
            count: res.RecordTotal,
            jump: function (obj, first) {
                if (!first) {
                    pageIndex = obj.curr;
                    QueryUserList();
                }
            }
        });
    }

    SaveUser = function (e) {
        var obj = $(e.currentTarget);
        var tr = obj.closest("tr");

        var data = {
            "OpenId": tr.attr("openId"),
            "MemberType": tr.find(".MemberType").val(),
            "VipPrice": tr.find(".VipPrice").val(),
            "RemainTimeStd": tr.find(".RemainTimeStd").val(),
            "RemainTimeSummer": tr.find(".RemainTimeSummer").val(),
            "RemainTimeWinter": tr.find(".RemainTimeWinter").val(),
            "RemainTimeWinter": tr.find(".RemainTimeWinter").val(),
            "DeadLineStd": tr.find(".DeadLineStd").val()
        }

        callAjax_Query(UpdateUserUrl, data, SaveUserCallBack);
      //  ShowInfo(openId);

    }
    SaveUserCallBack = function (res) {

        ShowInfo("保存成功");

    }

    Init();
});