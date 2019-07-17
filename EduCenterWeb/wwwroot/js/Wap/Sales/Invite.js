$(function () {
    var genQRUrl = "Invite?handler=GenerateQR";
    var wxConfigUrl = "/api/WXJS/InitConfig";
    var localUrl = "http://edu.iqianba.cn/sales/Invite/";
   

    Init = function () {

        if ($(".QRImg").length > 0) {

            callAjax_Query_API(wxConfigUrl + "?url=" + window.location.href, {}, function (res) {
                var jsList = new Array();
                jsList.push("updateAppMessageShareData");
                jsList.push("updateTimelineShareData");
                jsList.push("onMenuShareTimeline");
                jsList.push("onMenuShareAppMessage");
                wx.config({
                    debug: false,
                    appId: res.appId,
                    timestamp: res.timestamp,
                    nonceStr: res.nonceStr,
                    signature: res.signature,
                    jsApiList: jsList

                });

                wx.ready(function () {   //需在用户可能点击分享按钮前就先调用
                    wx.updateAppMessageShareData({
                        title: '您的朋友邀请您加入云艺书院', // 分享标题
                        desc: '云艺书院欢迎您', // 分享描述
                        link: 'http://edu.iqianba.cn/User/Login?act=Invite&OwnOpenId=' + res.openId, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                        imgUrl: 'http://edu.iqianba.cn/images/logo_120.png', // 分享图标
                        success: function () {
                            // 设置成功
                        }
                    })
                });
                wx.ready(function () {      //需在用户可能点击分享按钮前就先调用
                    wx.updateTimelineShareData({
                        title: '加入云艺书院', // 分享标题
                        link: 'http://edu.iqianba.cn/User/Login?act=Invite&OwnOpenId='+res.openId, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                        imgUrl: 'http://edu.iqianba.cn/images/logo_120.png', // 分享图标
                        success: function () {
                            // 设置成功
                        }
                    })
                });
               
            },
            function (res) {
                if (res.IntMsg == -1) {
                    window.location.href = "/User/Login";
                }
            });
        }

        $("#btnGenQR").on("click", GenQR);
        //$("#btnShareToPerson").on("click",ShareToPerson);
        //$("#btnShareToGroup").on("click",ShareToGroup);
    };
   
   
    GenQR = function () {

        aq(genQRUrl, {},
            function (res) {
                ShowInfo("二维码已生产", null, null, 2, function () {
                    window.location.reload();
                })   
            },
            function (res) {
                if (res.IntMsg == -1) {
                    window.location.href = "/User/Login";
                }
                else if (res.IntMsg == -2) {
                    window.location.href = "/Independent/RegPhone?rurl=/Sales/Invite";
                }
            },
            3)
    }
    ShowLogDetail = function () {
        ShowInfo("开发中，敬请期待！", null, null, 1);
    }

    Init();

})