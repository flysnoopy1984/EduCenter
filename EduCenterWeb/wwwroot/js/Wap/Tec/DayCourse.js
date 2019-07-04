$(function () {
    Init = function () {
        laydate.render({
            elem: '.DateInput',
            eventElem: '#btn_DatePick',

            trigger: 'click',
            theme: 'grid',
            done: function (value, date) {
                //   alert('你选择的日期是：' + value + '\n获得的对象是' + JSON.stringify(date));
                QueryTecDayCourse(value);
            }
        });
    }

    QueryTecDayCourse = function (date) {

    }

    Init();
  
})