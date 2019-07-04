$(function () {
    Init = function () {
        $("#btnTrial").on("click", function () {
            window.location.href = "ApplyTrial";

        });

        $("#btnBuyTime").on("click", function () {
            window.location.href = "BuyCourseTime";

        })

        layui.use('carousel', function () {
            var carousel = layui.carousel;
            //建造实例
            carousel.render({
                elem: '#HomeBar'
                , width: '100%' //设置容器宽度
                , arrow: 'hover' //始终显示箭头
                //,anim: 'updown' //切换动画方式
            });
        });

        $(".MyArea").css("height", window.screen.height);
      
        InitBaiduMap();
    }

    InitBaiduMap = function () {
        var map = new BMap.Map("SchoolMap");
        var point = new BMap.Point(121.531031, 31.166735);
        map.centerAndZoom(point, 15);

        var myIcon = new BMap.Icon("/images/map_logo.png", new BMap.Size(120, 120), {


        });

        var marker = new BMap.Marker(point,
            {
                offset: new BMap.Size(20, -20),
                icon: myIcon,
                title: "我在这里"
            }
        );
        map.addOverlay(marker);
        map.panTo(point);

        //var text = "我在这里";

        //var label = new BMap.Label(text, {
        //    offset: new BMap.Size(15, -50)
        //});
        ////设置label(标注的样式)
        //label.setStyle({
        //    color: "black",

        //    fontWeight: "bold",
        //    fontSize: "12px",
        //    height: "30px",
        //    width:"100px",
        //    //lineHeight : "20px",
        //    fontFamily: "微软雅黑",
        //    maxWidth: "none",
        //    border: "none"
        //});
        //marker.setLabel(label);
    }

    Init();
})