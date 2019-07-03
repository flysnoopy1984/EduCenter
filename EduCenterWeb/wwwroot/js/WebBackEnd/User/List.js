$(function () {
    var glaypage;
    var pageIndex = 1;
    var pageSize = 20;
    var QueryUserListUrl = "List?handler=QueryUserList";

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

            //$("#btnDate" + i).on('click', function (e) { //假设 test1 是一个按钮
            //    laydate.render({

            //        elem: "#btnDate" + i,
            //        show: true,
            //        closeStop: "#btnDate" + i,
            //        isInitValue: false,
            //        value: data.DeadLineStd
            //    });
            //});

           
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
        var openId = $(e.currentTarget).closest("tr").attr("openId");
        ShowInfo(openId);
    }

    Init();
});