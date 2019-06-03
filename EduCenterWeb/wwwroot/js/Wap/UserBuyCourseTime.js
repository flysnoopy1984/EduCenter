$(function () {
    var numStep = 10;

    Init = function () {

        var list = GetSessonUserApplyCourse();
        if (list) {
            var html = "";
            $.each(list, function (i) {
                html += list[i].lcode;
                html += "<br />";
            });
            $("#title").html(html);
        }

    }

    AddBuyNum = function () {
        var num = parseInt($("#buyNum").val());
        
        num += 10;
        if (num > 100) {
            ShowError("别买天多咯", "", "green");
            num = 100;
        }
           
        $("#buyNum").val(num);
    }
    DelBuyNum = function () {
        var num = parseInt($("#buyNum").val());

        num -= 10;
        if (num < 10) {
            ShowError("至少购买10课时","","red");
            num = 10;
        }
            
        $("#buyNum").val(num);
    }

    Init();
});