$(function () {
    Init = function () {

        laydate.render({
            elem: ".bTrialDate",
            eventElem: '#btn_DatePick',
            done: LayDataSelect,
            theme: 'molv',
        
            isInitValue: false
        });
    }
    LayDataSelect = function () {

    }

    Init();
})