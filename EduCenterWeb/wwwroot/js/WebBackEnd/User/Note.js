$(function () {
    var SaveUserNoteUrl = "Note?handler=SaveNote";
    var DeleteUserNoteUrl = "Note?handler=DeleteNote";
    var QueryUserNoteUrl = "Note?handler=QueryUserNote";

    var userOpenId = "";

    Init = function () {
        $("#btnNewNote").on("click", NewNoteEvent);
        userOpenId = GetUrlParam("openId", true);
        if (userOpenId == undefined || userOpenId == "") {
            ShowInfo("没有获取用户OpenId"); return;
        }
        var babyName = GetUrlParam("babyName",true);
        var wxName = GetUrlParam("wxName",true);
        if (babyName != undefined)
            $(".BabyName").text(babyName);
        if (wxName != undefined)
            $(".UserWxName").text(wxName);

        QueryUserNote();
    }

    NewNoteEvent = function () {
        var content = $("#EditContent").val();
        if (content == "" || content == null) {
            ShowInfo("请填写备注内容!");
            return;
        }
        var root = $(".MainContainer");
        var html = $("#HideData .OneNote").clone();

      
        var data = {
            "UserOpenId": userOpenId,
            "CreateDateTime": GetNowTime(),
            "CreateBy": html.find(".CreatedBy").text(),
            "Content": content,
        };
        aq(SaveUserNoteUrl, data, function (res) {
            $("#EditContent").val("");
            html.find(".NoteContent").text(content);
            root.append(html);
        });
      
    }

    DelNote = function (obj) {
       
        ShowConfirm("备注将被删除，是否继续?", null, null, function () {

            var oneNote = $(obj).closest(".OneNote");
            var rId = oneNote.attr("rId");
            if (rId == undefined || rId == "")
                oneNote.remove();
            else {
                aq(DeleteUserNoteUrl, { "Id": rId }, function () {
                    oneNote.remove();
                })
            }
        });
       
    }

    QueryUserNote = function () {

        aq(QueryUserNoteUrl, { "userOpenId": userOpenId }, function (res) {

            var list = res.List;
            var root = $(".MainContainer");
            $.each(list, function (i) {
                var html = $("#HideData .OneNote").clone();
                html.attr("rId", list[i].Id);
                html.find(".NoteContent").text(list[i].Content);
                html.find(".CreatedDateTime").text(list[i].CreatedDateTimeStr);
                html.find(".CreatedBy").text(list[i].CreateBy);
                root.append(html);
            });
        });
    }

    getNow = function(s)
    {
        return s < 10 ? '0' + s : s;
    }

    GetNowTime = function () {
        var myDate = new Date();

        var year = myDate.getFullYear();        //获取当前年
        var month = myDate.getMonth() + 1;   //获取当前月
        var date = myDate.getDate();            //获取当前日


        var h = myDate.getHours();              //获取当前小时数(0-23)
        var m = myDate.getMinutes();          //获取当前分钟数(0-59)
        var s = myDate.getSeconds();

        var now = year + '-' + getNow(month) + "-" + getNow(date) + " " + getNow(h) + ':' + getNow(m);// + ":" + getNow(s)
        return now;
    }
    Init();
})