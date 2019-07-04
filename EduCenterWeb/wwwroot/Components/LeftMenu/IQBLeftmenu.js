$(function () {
    //0 关闭 1 代开
    var MenuStatus = 0;
   

    switchMenu = function () {
        //关闭
        if(MenuStatus ==1)
        {

          //  $(".sidenav").css("display", "none");
            $(".sidenav").transition({ x: '0px' });
        //    $(".MainContainer").transition({ x: '0px' });
          
        
            MenuStatus = 0;
        }
        else
        {

            //     $(".sidenav").css("display", "unset");
          //  alert( );
            $(".sidenav").transition({ x: $(".sidenav").css("width") });
         //   $(".sidenav b").css("opacity", "1");
         //   $(".sidenav b").transition({ x: '0px' });
         
            MenuStatus = 1;
        }
    };

    InitMenu = function (Id) {
   //     $(".sidenav").transition({ x: '-150px' });
        $(Id).on("click", switchMenu);


    }
    GoMenu = function (url) {
       // alert(url);
        window.location.href = url;
    }

    //使用时替换此处
    InitMenu(".LeftMenu");

  //  $("#btnLeftMainMenu").on("click", switchMenu);
});