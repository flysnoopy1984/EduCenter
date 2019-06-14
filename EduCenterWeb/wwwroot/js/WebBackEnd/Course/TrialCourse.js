$(function () {
    Init = function () {
        var sysDate = new Date();
        var year = sysDate.getFullYear();
        var month = sysDate.getMonth() + 1;
        var monthName = month;
        if (month < 10)
            monthName = "0" + month;
        var day = sysDate.getDate();
        var fdate = year + "-" + monthName + "-" + day;

        var tDate = new Date(fdate + 1);
      
        $(".DateInput").text(fdate + " - " + tDate);

        laydate.render({
            elem: ".DateInput",
            eventElem: '#btn_DatePick',
            range: true,
            trigger: 'click',
            done: LayDataSelect,
            theme: 'molv',
        });
    }

    LayDataSelect = function (value, date) {
        //alert('你选择的日期是：' + value + '\n\n获得的对象是' + JSON.stringify(date));
    }
    Init();
})