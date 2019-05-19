$(function () {
    var saveUrl = "Manager?handler=Save";

    Save = function () {
     //   SaveAlert();

        //$.ajax({
        //    type: 'post',
        //    url: saveUrl,
        //    data: {
        //        "Code": $("#vCode").val(),
        //        "TypeName":$("#vTypeName").val(),
        //    }, 
        //    success: function (res) {
        //    }
        //});

        var code = $("#vCode").val();
        var typeName = $("#vTypeName").val();
        $.ajax({
            type: "post",
            url: saveUrl,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: {
                "Code":code,
                "TypeName": typeName,
            }, 
            //contentType: "application/json",
            success: function (res) {
                if (res.IsSuccess) {
                    alert("OK");
                }
                else {
                    alert(res.ErrorMsg);
                }
            },
            error: function (xhr, type) {
                alert("系统错误！");
            }

        });

    }
});