$(function () {
    var InitUrl = "Apply?handler=InitData";
    var CourseScheduleData = null;

    Init = function () {

        var times = $("#GridWeek tr").length ;
        CourseScheduleData = new Object();
        for (var i = 1; i < 8; i++) {
            CourseScheduleData[i] = new Object();
            for (var j = 1; j < times; j++) {
                CourseScheduleData[i][j] = new Array();
            }
         
        }
        callAjax_Query(InitUrl, {}, InitCallBack,"");
    }



    selectCourse = function (obj) {
        var cell = $(obj);
        var day = cell.attr("day");
        var lesson = cell.attr("lesson");

        var csList = CourseScheduleData[day][lesson];

        var html = "";
        $.each(csList, function (i) {
            var id = "cb_" + i;
            var lbcss = "list-group-item-info";
            lbcss = GetItemStyleByType(csList[i].CourseType, "label");
            var cbcss = "";
            cbcss = GetItemStyleByType(csList[i].CourseType, "cb");

            html += '<div class="CourseSelect">';
            html += '<div class="checkbox ' + cbcss+'">';
            html += ' <input type="checkbox" id="'+id+'" class="form-control">';
            html += '<label for="' + id + '" class="' + lbcss+'">' + csList[i].CourseName + '</label>';
            html += '</div>';
        });
    
        html += '</div>';
        $.confirm({
            title: '选择课程',
            content: html,

            buttons: {
                cancel: {
                    text: "换一天",
                    btnClass: 'btn-info'
                },
                OK: {
                    text: '确认选择',
                    btnClass: 'btn-primary',
                    action:  ConfirmSelectCourse
                }
            }
           
        });
    }

    GetItemStyleByType = function (cType, ctrl) {
       
        var gl = "list-group-item-info";
        var g2 = "checkbox-info";
        switch (cType) {

            case 2:
                gl = "list-group-item-warning";
                g2 = "checkbox-warning";
                 
                break;
            case 1:
                gl = "list-group-item-danger";
                g2 = "checkbox-danger";
                break;
            case 3:
                gl = "list-group-item-success";
                g2 = "checkbox-success";
                break;
        }
        if (ctrl == "label")
            return gl;
        else
            return g2;

    }

    ConfirmSelectCourse = function () {
        alert("!1");
    }

    InitCallBack = function(result){
        var list = result.List;

        $.each(list, function (i) {

            var cs = list[i];
            CourseScheduleData[cs.Day][cs.Lesson].push(cs);
          //  cs.

        });
       
    }

    Init();

   
});