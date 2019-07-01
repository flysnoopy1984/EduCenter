;(function () {

    var LoginUrl = "/WebBackend/Login?handler=UserLogin";
	'use strict';

	// Placeholder 
	var placeholderFunction = function() {
		$('input, textarea').placeholder({ customClass: 'my-placeholder' });
	}
	
	// Placeholder 
	var contentWayPoint = function() {
		var i = 0;
		$('.animate-box').waypoint( function( direction ) {

			if( direction === 'down' && !$(this.element).hasClass('animated-fast') ) {
				
				i++;

				$(this.element).addClass('item-animate');
				setTimeout(function(){

					$('body .animate-box.item-animate').each(function(k){
						var el = $(this);
						setTimeout( function () {
							var effect = el.data('animate-effect');
							if ( effect === 'fadeIn') {
								el.addClass('fadeIn animated-fast');
							} else if ( effect === 'fadeInLeft') {
								el.addClass('fadeInLeft animated-fast');
							} else if ( effect === 'fadeInRight') {
								el.addClass('fadeInRight animated-fast');
							} else {
								el.addClass('fadeInUp animated-fast');
							}

							el.removeClass('item-animate');
						},  k * 200, 'easeInOutExpo' );
					});
					
				}, 100);
				
			}

		} , { offset: '85%' } );
	};
	// On load
    $(function () {

        Init()
        $("#btnSubmit").on("click", UserLogin);
     
    });

    GetSysDate = function () {
        var myDate = new Date;
        var year = myDate.getFullYear();
        var mon = myDate.getMonth() + 1;
        var day = myDate.getDate();

        return year + "/" + mon + "/" + day;

    }

    UserLogin = function() {
        var loginName = $("#username").val();
        var loginPwd = $("#password").val();

        if (loginName == "" || loginPwd=="")
        {
            ShowInfo("用户名或密码必须填写", null, null, 1);
            return;
        }
        var data = {
            "loginName": loginName, "loginPwd": loginPwd
        };
        callAjax_Query(LoginUrl,
            data,
            function (res) {
                if ($("#remember").is(":checked")) {
                    var obj = res.Entity;
                    var jsonObj = {
                        "loginName": loginName,
                        "loginPwd": loginPwd,
                        "UserRole": obj.UserRole,
                        "LoginDate": GetSysDate()
                    };
                    SetLocal_UserBackend(jsonObj);
                }
                else {
                    RemoveLocal_UserBackend();
                }
            
                window.location.href = "/WebBackend/Home";
            }
        );
    }

 
    CheckLocalStorage  = function(){
        var json = GetLocal_UserBackend();
        if (json)
        {
            $("#remember").attr('checked', 'checked');
            $("#username").val(json.loginName);

            $("#password").val(json.loginPwd);
        }
    }

    Init = function () {
        placeholderFunction();
        contentWayPoint();

        CheckLocalStorage();


    }



}());