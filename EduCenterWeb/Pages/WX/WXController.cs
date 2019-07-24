using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.WX;
using EduCenterModel.User;
using EduCenterModel.WX;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EduCenterWeb.Pages.WX
{ 
    //原始：http://wx.iqianba.cn/Home/Message
    [Route("api/[controller]")]
    public class WXController : Controller
    {
        private WXMessage _wxMessage;
        private string _EventKey;
        private string _ResultMsg;

        private TecSrv _TecSrv;
        private UserSrv _UserSrv;
        private BusinessSrv _BusinessSrv;

         public WXController(TecSrv TecSrv,UserSrv userSrv, BusinessSrv businessSrv)
        {
            _TecSrv = TecSrv;
            _UserSrv = userSrv;
            _BusinessSrv = businessSrv;
        }
        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            try
            {
              
                string echostr = Request.Query["echostr"].FirstOrDefault();

           //     NLogHelper.InfoTxt($"echoStr:{echostr}");
             
                return echostr;

            }
            catch(Exception ex)
            {
                NLogHelper.ErrorTxt($"[WX Home Get]:{ex.Message};");
            }
               
            return "";
        }

      

        // POST api/<controller>
        [HttpPost]
        public string Post()
        {
            try
            {
             
               var memoryStream = new MemoryStream();
               Request.Body.CopyTo(memoryStream);
               string strXml = System.Text.Encoding.Default.GetString(memoryStream.ToArray());
        //       NLogHelper.InfoTxt($"WXEntry Message:{strXml}");

                if (!string.IsNullOrEmpty(strXml))
                {
                    _ResultMsg = "";

                    _wxMessage = new WXMessage();
                    _wxMessage.LoadXml(strXml);

                    _EventKey = _wxMessage.EventKey;
                    if (!string.IsNullOrEmpty(_EventKey))
                    {
                        //邀请码微信消息规则
                        if (_EventKey.StartsWith("qrscene_"))
                            _EventKey = _EventKey.Substring(8);
                        //邀请码
                        if (_EventKey.StartsWith(WxConfig.QR_Invite))
                            InviteQRHandler();
                        else
                            //Click 菜单
                            HandlerStdMessage();
                    }    
                    else
                    {
                        HandlerStdMessage();
                    }
                   
                }
            
            }
            catch (Exception ex)
            {
                NLogHelper.ErrorTxt($"[WX Home Post]:{ex.Message};");
            }

            return _ResultMsg;
        }

        public void HandlerStdMessage()
        {
            switch (_wxMessage.Event)
            {
                case "view":
                //    _wxMessage.EventKey
                    break;
                case "click":
                    MenuClickHandler();
                    break;
                case "scan":
                    //如果是扫描登录
                    ScanHandler();
                    break;
                case "subscribe":
                    SubscribeHandler();
                    break;
                default:
                    _ResultMsg = _wxMessage.toText(WXReplyContent.DefaultMsessage());
                    break;
              
                      
            }
        }

        /// <summary>
        /// 邀请码进入
        /// </summary>
        private void InviteQRHandler()
        {
            try
            {
                //教师邀请
                if (_EventKey.StartsWith(WxConfig.QR_Invite_TecPre))
                {
                    var wxUser = WXApi.GetWXUserInfo(_wxMessage.FromUserName);
                    var user = _UserSrv.AddOrUpdateFromWXUser(wxUser);
                    _TecSrv.NewTecFromUser(user);
                    _ResultMsg = _wxMessage.toText(WXReplyContent.NewTec(user.Name));
                }
                else if (_EventKey.StartsWith(WxConfig.QR_Invite_User))
                {
                    var ownOpenId = _EventKey.Split("_")[2];
                    var user = _BusinessSrv.InvitedUserComing(_wxMessage.FromUserName, ownOpenId);
                 
                    _ResultMsg = _wxMessage.toText(WXReplyContent.NewUserAdd(user.Name));
                }
              
            }
            catch(Exception ex)
            {
                NLogHelper.ErrorTxt($"[InviteQRHandler]:{ex.Message}");
            } 
        }

      
        /// <summary>
        /// 扫码
        /// </summary>
        private void ScanHandler()
        {
           
            EUserInfo ui = _UserSrv.GetUserInfo(_wxMessage.FromUserName);
            if(ui!=null)
            {
                _ResultMsg = _wxMessage.toText(WXReplyContent.UserComing(ui.wx_Name));
            }
            else
            {
                _ResultMsg = _wxMessage.toText(WXReplyContent.NewUserLook(ui.wx_Name));
            }

        }

        /// <summary>
        /// 关注
        /// </summary>
        private void SubscribeHandler()
        {
            NLogHelper.InfoTxt("SubscribeHandler In");
            EUserInfo ui = _UserSrv.GetUserInfo(_wxMessage.FromUserName);
            if (ui== null)
            {
                var wxUser = WXApi.GetWXUserInfo(_wxMessage.FromUserName);
                ui = _UserSrv.AddOrUpdateFromWXUser(wxUser);

                _ResultMsg = _wxMessage.toText(WXReplyContent.NewUserAdd(ui.wx_Name));
            }
            else
                _ResultMsg = _wxMessage.toText(WXReplyContent.UserComing(ui.wx_Name));

        }

        /// <summary>
        /// 菜单点击
        /// </summary>
        private void MenuClickHandler()
        {
            string key = _wxMessage.EventKey;
            NLogHelper.InfoTxt("MenuClickHandler:" + key);
            switch (key)
            {
                case "intro_100":
                    IntroduceYunYi();
                    break;
                case "ApplyTrial":
                    menu_ApplyTrial();
                    break;
                case "SalesInvite":
                    menu_SalesInvite();
                    break;
            }
        }

        private void menu_ApplyTrial()
        {
            string openId = _wxMessage.FromUserName;
            HttpContext.Response.Redirect($"/User/Login?handler=LoginTransfer&openId={openId}&toPage=/User/ApplyTrial");
        }

        private void menu_SalesInvite()
        {
            string openId = _wxMessage.FromUserName;
            HttpContext.Response.Redirect($"/User/Login?handler=LoginTransfer&openId={openId}&toPage=/Sales/Invite");
        }

        private void IntroduceYunYi()
        {
            try
            {
                string url = @"http://mp.weixin.qq.com/s?__biz=MzU3NDk2NjE1MQ==&mid=100000013&idx=1&sn=1b9b19525f6a60329e1e4315f5c40ec5&chksm=7d2b11e54a5c98f3485c9a6ff1394b9397f90b06c72e41d919473cba592d8cb05e6832d3b233#rd";
                string picUrl = @"http://mmbiz.qpic.cn/mmbiz_jpg/Ct0QBXEFiaJkACbLKbpDy8ql5rBoDUHBS0SOic6bHQ0gUYagFibCdYykZeibHGG9LiaJ2JgWTtGOAx0mH8FZfKL3CyA/0?wx_fmt=jpeg";
                _ResultMsg = _wxMessage.toPicText(picUrl, url, "欢饮光临云艺国学教育","云艺国学教育");
               // NLogHelper.InfoTxt("Menu:" + _ResultMsg);
            }
            catch (Exception ex)
            {
                NLogHelper.ErrorTxt("Menu:" + ex.Message);
            }
           
        }






    }
}
