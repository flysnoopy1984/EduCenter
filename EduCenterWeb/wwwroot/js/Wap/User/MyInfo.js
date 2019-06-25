$(function () {
    var SaveUrl = "MyInfo?handler=Save";
    var GetUrl = "MyInfo?handler=InitChildList";
    Init = function () {
        $("#btn_EditPhone").on("click", function () {
            window.location.href = "/Independent/RegPhone?rurl=/User/MyInfo";
        });
        $("#AddBaby").on("click", function () {
            $("#SecBaby").slideDown();
        });

        $("#DelBaby").on("click", function () {
            $("#SecBaby").slideUp();

            $("#SecBaby").find("input[type=text]").val("");
        });

        $("#btnSave").on("click", SaveProfileInfo);

        laydate.render({
            elem: '#FirstBaby #BirthDay', //指定元素
            theme: '#393D49',
            isInitValue: false
        });

        laydate.render({
            elem: '#SecBaby #BirthDay', //指定元素
            theme: '#393D49',
            isInitValue: false
        });

        callAjax_Query(GetUrl, {}, GetCallBack)


    }

    GetCallBack = function (res) {
        var data = res.List;
        if (data.length > 0) {
            $("#FirstBaby #Name").val(data[0].Name);
            $("#FirstBaby #Sex").val(data[0].Sex);
            $("#FirstBaby #Age").val(data[0].Age);
            $("#FirstBaby #BirthDay").val(data[0].BirthDay);
        }
        if (data.length > 1) {
            $("#SecBaby #Name").val(data[1].Name);
            $("#SecBaby #Sex").val(data[1].Sex);
            $("#SecBaby #Age").val(data[1].Age);
            $("#SecBaby #BirthDay").val(data[1].BirthDay);
        }
    }

    SaveProfileInfo = function () {
        var list = new Array();
        var child = new Object();
        child.Name = $("#FirstBaby #Name").val();
        child.Sex = $("#FirstBaby #Sex").val();
        child.Age = $("#FirstBaby #Age").val();
        child.BirthDay = $("#FirstBaby #BirthDay").val();
        child.No = 1;
        if (child.Name == "") {
            ShowInfo("宝贝名字没有输入", "错误", "red", 1);
            return;
        }
        list.push(child);

        if (!$("#SecBaby").is(':hidden')) {
            var child = new Object();
            child.Name = $("#SecBaby #Name").val();
            child.Sex = $("#SecBaby #Sex").val();
            child.Age = $("#SecBaby #Age").val();
            child.BirthDay = $("#SecBaby #BirthDay").val();
            if (child.Name == "") {
                ShowInfo("二胎宝贝名字没有输入", "错误", "red", 1);
                return;
            }
            child.No = 2;
            list.push(child);
        }

        callAjax_Query(SaveUrl, { "list": list }, SaveCallBack, "", function(res) {
            if (res.IntMsg == -1)
                window.location.href = "Login";
        });

    }

    SaveCallBack = function (res) {
        ShowInfo("已保存", null, null, 1, function () {
         //   window.location.reload();
        });
    }

    Init();
})