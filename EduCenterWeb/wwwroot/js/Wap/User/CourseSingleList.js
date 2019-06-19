$(function () {
    var QueryListUrl = "CourseSingleList?handler=QueryList";
    var pageIndex = 1;
    var pageSize = 15;

    Init = function () {
        var LessonCode = GetUrlParam("LCode");
        //var CourseName = GetUrlParam("CName");
        if (LessonCode == undefined) {
            ShowInfo("NoCode，获取数据失败！", null, null, 1,function () {
                window.location.href = "Login";
                return;
            })
        }
      
        layui.use('flow', function () {
            var flow = layui.flow;

            //信息流
            flow.load({
                elem: '.dataList' //指定列表容器
                , done: function (page, next) { //到达临界点（默认滚动触发），触发下一页
                    pageIndex = page;
                    var html = "";
                 
                    callAjax_Query_NoBlock(QueryListUrl, { LessonCode,pageIndex, pageSize }, function (res) {
                        var data = res.List;

                        $.each(data, function (i) {
                            var d = data[i];
                            var item = $("#HideData .OneRecord").clone();
                        //    item.find(".courseName").text(d.CourseName);
                            item.find(".courseStatus").text(d.CourseStatusName);
                       //     item.find(".courseScheduleType").text(d.CourseScheduleTypeName);
                            item.find(".couseTime").text(d.CourseDate + " | " + d.LessonTime);

                            html += item.prop("outerHTML");
                            html += "<hr />";

                        });
                        next(html, page < res.TotlaPage);
                    }, true, QueryErrorHandler);


                }
            });

        });
        // QueryList();
    }

    QueryErrorHandler = function (res) {
        if (res.IntMsg == -1) {
            ShowInfo(res.ErrorMsg, null, "red", 1, function () {
                window.location.href = "Login";
            });

        }
    }

    Init();
});