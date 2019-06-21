$(function () {
    //0 关闭 1 代开
    var MenuStatus = 0;
   

    switchMenu = function () {
        //关闭
        if(MenuStatus ==1)
        {

            $(".sidenav").css("display", "none");
        
         //   $(".o2o_content").transition({ x: '0px' });
          
        
            MenuStatus = 0;
        }
        else
        {

            $(".sidenav").css("display", "unset");
        
         //   $(".o2o_content").transition({ x: '150px' });
            $(".sidenav b").css("opacity", "1");
            $(".sidenav b").transition({ x: '0px' });
         
            MenuStatus = 1;
        }
    };

    InitMenu = function (id) {
        $(id).on("click", switchMenu);
    }
    GoMenu = function (url) {
        alert(url);
      //  window.location.href = url;
    }

  //  $("#btnLeftMainMenu").on("click", switchMenu);
});