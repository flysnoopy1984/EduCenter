$(function () {
    var glaypage;
    var pageIndex = 1;
    var pageSize = 15;
    var QueryUserListUrl = "List?handler=QueryUserList";
    var UpdateUserUrl = "List?handler=UpdateUser";

    Init = function () {

        layui.use('laypage', function () {
            glaypage = layui.laypage;
        });

        $("#btnQuery").on("click", btnQuery);

        btnQuery();
    }
    btnQuery = function () {

        pageIndex = 1;
        QueryUserList();
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
        var root = null;

        $.each(dataList, function (i) {
            var data = dataList[i];

            var tr = $("#HideData #DataTable tr").clone();
           
            tr.attr("openId", data.userOpenId);
            tr.find(".WxName").text(data.WxName);
            tr.find(".RealName").val(data.RealName);
            if (data.BabyName)
                tr.find(".BabyName").text(data.BabyName);
            tr.find(".MemberType").val(data.MemberType);
            tr.find(".VipPrice").val(data.VipPrice);
            tr.find(".RemainTimeStd").val(data.RemainTimeStd);
            tr.find(".RemainTimeSummer").val(data.RemainTimeSummer);
            tr.find(".RemainTimeWinter").val(data.RemainTimeWinter);
            tr.find(".UserRole").val(data.UserRole);
            

            var DeadLineStd = tr.find(".DeadLineStd");
            DeadLineStd.attr("id", "showDate" + i);
            DeadLineStd.val(data.DeadLineStd);

            var dateBtn = DeadLineStd.next();
            dateBtn.attr("id", "btnDate" + i);

            tr.find("#btn_Save").on("click", SaveUser);
            tr.find("#btn_AdjustCourse").on("click", AdjustCourse);
            if (root == null) {
                header.after(tr);
                root = tr;
            }
            else {
                root.after(tr);
                root = tr;
            }
                
                

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
            curr: pageIndex,
            jump: function (obj, first) {
               
                if (!first) {
                    pageIndex = obj.curr;
                    QueryUserList();
                   
                }
                //else
                //    pageIndex++;
            }
        });
        
      
    }
    //课程调整
    AdjustCourse = function (e) {
        ShowInfo("开发中!");
    }
    //保存
    SaveUser = function (e) {
        var obj = $(e.currentTarget);
        var tr = obj.closest("tr");

        var data = {
            "OpenId": tr.attr("openId"),
            "MemberType": tr.find(".MemberType").val(),
            "UserRole": tr.find(".UserRole").val(),
            "VipPrice": tr.find(".VipPrice").val(),
            "RemainTimeStd": tr.find(".RemainTimeStd").val(),
            "RemainTimeSummer": tr.find(".RemainTimeSummer").val(),
            "RemainTimeWinter": tr.find(".RemainTimeWinter").val(),
            "RemainTimeWinter": tr.find(".RemainTimeWinter").val(),
            "DeadLineStd": tr.find(".DeadLineStd").val(),
            "RealName":tr.find(".RealName").val(),
        }

        callAjax_Query(UpdateUserUrl, data, SaveUserCallBack);
      //  ShowInfo(openId);

    }
    SaveUserCallBack = function (res) {

        ShowInfo("保存成功");

    }

    //宝贝信息
    CreateBabyInfoForm = function (obj) {
        //var obj = $(e.currentTarget);
        var tr = $(obj).closest("tr");

        layer.open({
            type: 2,
            title: '宝贝信息',
            shadeClose: false,
            shade: 0.8,
            area: ['600px', '80%'],
            content: 'BabyInfo?openId=' + tr.attr("openId") //iframe的url
        });
    }

    CloseBabyInfoForm = function (openId, BabyName) {

        layer.closeAll("iframe");
        if (BabyName) {
            
            var tr = $("#UserListTable tr[openId=" + openId + "]");
            tr.find(".BabyName").text(BabyName);

        }
        //var date = $(".StartDateInput").text();
        //var tecCode = 
      //  window.location.href = "BabyInfo";
    }

    Init();
});