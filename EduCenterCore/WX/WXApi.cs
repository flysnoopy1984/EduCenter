using EduCenterCore.Common.Helper;
using EduCenterModel.WX;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;

namespace EduCenterCore.WX
{
    public static class WXApi
    {
        /// <summary>
        /// 生成微信二维码
        /// </summary>
        /// <param name="scene_str"></param>
        /// <param name="access_token"></param>
        /// <param name="scene_id"></param>
        /// <param name="isTemp"></param>
        /// <returns></returns>
        public static WXQRResult getQR(string scene_str , string access_token, string scene_id="0",   bool isTemp = false)
        {
            WXQRResult resObj = null;
            try
            {

                //获取数据的地址（微信提供）
                String url = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + access_token;

                //发送给微信服务器的数据

                String jsonStr = "{\"action_name\": \"QR_LIMIT_SCENE\", \"action_info\":{\"scene\": {\"scene_id\":" + scene_id + "}}}";
                if (!string.IsNullOrEmpty(scene_str))
                {
                    if (isTemp)
                    {
                        jsonStr = "{\"expire_seconds\": \"180\",\"action_name\": \"QR_STR_SCENE\", \"action_info\":{\"scene\": {\"scene_str\":\"" + scene_str + "\"}}}";
                    }
                    else
                    {
                        jsonStr = "{\"action_name\": \"QR_LIMIT_STR_SCENE\", \"action_info\":{\"scene\": {\"scene_str\":\"" + scene_str + "\"}}}";
                    }
                }

              
                //post请求得到返回数据（这里是封装过的，就是普通的java post请求）
                String response = HttpHelper.RequestUrlSendMsg(url, HttpHelper.HttpMethod.Post, jsonStr);

                resObj = JsonConvert.DeserializeObject<WXQRResult>(response);

                /*
                 HTTP GET请求（请使用https协议）https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=TICKET
                提醒：TICKET记得进行UrlEncode
                 */
                resObj.ticket = Uri.EscapeDataString(resObj.ticket);
            }
            catch (Exception ex)
            {
                NLogHelper.ErrorTxt("[getQR]:"+ex.Message);
            }
            return resObj;
        }

        public static AccessToken getToken()
        {
            string tokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
            tokenUrl = string.Format(tokenUrl, WxConfig.APPID, WxConfig.APPSECRET);
            AccessToken token = HttpHelper.Get<AccessToken>(tokenUrl);

            return token;
        }

        /// <summary>
        /// 下载微信二维码
        /// </summary>
        /// <param name="qrTicket"></param>
        /// <param name="saveFullFilePath"></param>
        public static void DownLoadWXQR(string qrTicket, string saveFullFilePath)
        {
            string apiUrl = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=" + qrTicket;

            bool isExist = File.Exists(saveFullFilePath);
            if (isExist) return;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(apiUrl);
            req.Proxy = null;
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:11.0) Gecko/20100101 Firefox/11.0";
            req.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.8,zh-hk;q=0.6,ja;q=0.4,zh;q=0.2");
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            Image img = Image.FromStream(req.GetResponse().GetResponseStream());
            img.Save(saveFullFilePath);


        }
    }
}
