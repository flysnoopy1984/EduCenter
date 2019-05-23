using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.WX;
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



         public WXController(TecSrv TecSrv,UserSrv userSrv)
        {
            _TecSrv = TecSrv;
            _UserSrv = userSrv;
        }
        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            try
            {
              
                string echostr = Request.Query["echostr"].FirstOrDefault();

                NLogHelper.InfoTxt($"echoStr:{echostr}");
             
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
               NLogHelper.InfoTxt($"strXml:{strXml}");

                if (!string.IsNullOrEmpty(strXml))
                {
                    _ResultMsg = "";

                    _wxMessage = new WXMessage();
                    _wxMessage.LoadXml(strXml);

                    _EventKey = _wxMessage.EventKey;
                    if (!string.IsNullOrEmpty(_EventKey))
                    {
                        //微信消息规则
                        if (_EventKey.StartsWith("qrscene_"))
                            _EventKey = _EventKey.Substring(8);
     
                        if(_EventKey.StartsWith(WxConfig.QR_Invite))
                            InviteQRHandler();
                    }    
                    else
                    {
                        switch (_wxMessage.Event)
                        {
                            case "view":
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
                                return _wxMessage.toText(WXReplyContent.DefaultMsessage());
                              //  return _wxMessage.toText(WXReplyContent.NewTec("宋"));

                        }
                    }
                   
                }
            
            }
            catch (Exception ex)
            {
                NLogHelper.ErrorTxt($"[WX Home Post]:{ex.Message};");
            }

            return _ResultMsg;
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
              
            }
            catch(Exception ex)
            {
                NLogHelper.ErrorTxt($"[InviteQRHandler]:{ex.Message}");
            } 
        }

        /// <summary>
        /// 菜单点击
        /// </summary>
        private void MenuClickHandler()
        {

        }

        /// <summary>
        /// 扫码
        /// </summary>
        private void ScanHandler()
        {
            
        }

        /// <summary>
        /// 关注
        /// </summary>
        private void SubscribeHandler()
        {

        }

     



    }
}
