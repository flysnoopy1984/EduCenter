$(function () {
    var glaypage;
    var pageIndex = 1;
    var pageSize = 20;
    var QueryTecLeaveUrl = "List?handler=QueryUserList";

    Init = function () {
        layui.use('laypage', function () {
            glaypage = layui.laypage;
        });

        $("#btnQuery").on("click", QueryUserList);
    }
    QueryUserList = function () {

    }

    QueryUserListCallBack = function () {

    }
});