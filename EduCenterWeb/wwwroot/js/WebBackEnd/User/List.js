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
        callAjax_Query(QueryUserListUrl, {}, QueryUserListCallBack);
    }

    QueryUserListCallBack = function (res) {
        var data = res.List;
        var root = $("#UserListTable");
        $.each(data, function (i) {
            data[i]
        });
    }

    AddLabelTd = function (root,value) {

    }

    AddEditTd = function (root, value) {

    }
});