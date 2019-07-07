$(function () {
    var SaveUrl = "BabyInfo?handler=Save";
    var InitUrl = "BabyInfo?handler=InitChildList"

    var babyCount = 0;
    var openId = "";
    Init = function () {
     

        $("#AddBaby").on("click", AddBaby);
        
        openId = GetUrlParam("openId");
        if (openId == undefined) {
            ShowInfo("没有获取OpenId,请联系开发", null, null, 2, function () {
                parent.CloseBabyInfoForm();
            })
        }

        $("#btn_Save").on("click", SaveData);

        InitBabyData();
    };

 

    AddBaby = function () {
        if (babyCount == 3) {
            ShowInfo("最多添加3个宝贝");
            return;
        }
        var html = $("#HideData .OneBaby").clone();

        babyCount++;
        var BabyNo = html.find(".BabyNo");
        BabyNo.text("第" + babyCount + "个宝贝");
        BabyNo.attr("No", babyCount);

        var BirthDay = html.find(".BabyBirthday");
        BirthDay.attr("id", "BabyBirthday" + babyCount);

        var delBtn = html.find(".DelButton");
        if (babyCount >= 2) {
            delBtn.css("display", "flex");
            delBtn.on("click", DelBaby);
        }

    
        $(".AllBaby").append(html);

        laydate.render({
            elem: "#BabyBirthday" + babyCount,
            theme: 'molv',
            isInitValue: false,
            //   done: LayDaySelect,
        });
        return html;
       
    }
    DelBaby = function (e) {
        var obj = e.currentTarget;
        babyCount--;
        $(obj).closest(".OneBaby").remove();

        $(".AllBaby .OneBaby").eq(1).find(".BabyNo").text("第" + babyCount + "个宝贝");
    }

    SaveData = function () {
        var list = new Array();
        var isError = false;
        var babyName="";

        $(".AllBaby .OneBaby").each(function (i, item) {

            var obj = $(item);
            var data = Object();

            data.No = obj.find(".BabyNo").attr("No");
            data.UserOpenId = openId;
            data.Name = obj.find(".bName").val();
            data.Sex = obj.find(".bSex").val();
            data.Age = obj.find(".bAge").val();
            data.BirthDay = obj.find(".BabyBirthday").val();

            if (data.Name == null || data.Name == "") {
                ShowError("宝贝姓名没有填写！");
                isError = true;
                return false;
            }
            babyName += data.Name+"/";
            
            list.push(data);

        });

        if (list.length > 0 && isError == false)
          

            callAjax_Query(SaveUrl, { "list": list }, function () {
                babyName = babyName.substring(0, babyName.length - 1);
                ShowInfo("保存成功", null, null, 1, function () {
                    parent.CloseBabyInfoForm(openId,babyName);
                });
            })
    }

    InitBabyData = function () {

        callAjax_Query(InitUrl, { "openId": openId }, function (res) {
            var list = res.List;
            $.each(list, function (i) {

                var html=  AddBaby();
                //赋值
                var data = list[i];
                if (data) {
                    var BabyNo = html.find(".BabyNo");
                    var BirthDay = html.find(".BabyBirthday");
                    BabyNo.attr("No", data.No);
                    BabyNo.text("第" + data.No + "个宝贝");

                    html.find(".bName").val(data.Name);
                    html.find(".bSex").val(data.Sex);
                    html.find(".bAge").val(data.Age);
                    html.find(".BabyBirthday").val(data.BirthDay);
                    BirthDay.attr("id", "BabyBirthday" + data.No);
                }
            });
        })
    }

    Init();
})