using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterCore.WX;
using EduCenterModel.WX;
using EduCenterModel.WX.Media;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace EduCenterWeb.Pages.Test
{
    public class WXModel : PageModel
    {
        private TecSrv _TecSrv;
        private UserSrv _UserSrv;
        public WXModel(TecSrv TecSrv, UserSrv userSrv)
        {
            _TecSrv = TecSrv;
            _UserSrv = userSrv;
        }
        public string Msg;

        public JOMedia JOMedia { get; set; }

        public JOMedia PicMedia { get; set; }
        public void OnGet()
        {

        }

        private WXUserInfo GetWXUser()
        {
            string path = EduEnviroment._Enviroment.WebRootPath + @"\Files\Test\wxUser.json";
            FileInfo fi = new FileInfo(path);
            FileStream fs = fi.Open(FileMode.Open);
            WXUserInfo wxUser = null;
            using (StreamReader sr = new StreamReader(fs))
            {
                string json = sr.ReadToEnd();
                wxUser = JsonConvert.DeserializeObject<WXUserInfo>(json);
                fs.Close();
                fs.Dispose();
            }
            return wxUser;
        }

        public void OnPostInviteTec()
        {
            Msg = "创建成功！";
            try
            {

                WXUserInfo wxUser = GetWXUser();
                if(wxUser!=null)
                {
                    var user = _UserSrv.AddOrUpdateFromWXUser(wxUser);
                    _TecSrv.NewTecFromUser(user);
                }


            }
            catch (Exception ex)
            {
                Msg = ex.Message;
            }
        }

        public void OnPostGetNewsMeterial()
        {
            try
            {
                string access_token = WXApi.getAccessToken().access_token;
                string wxUrl = $"https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={access_token}";
                MaterialList_In paremeter = new MaterialList_In
                {
                    type = "news",
                    offset = 0,
                    count = 20,
                };
                var json = JsonConvert.SerializeObject(paremeter);
                 string data = HttpHelper.RequestUrlSendMsg(wxUrl, HttpHelper.HttpMethod.Post, json);

               
                JOMedia = JsonConvert.DeserializeObject<JOMedia>(data);

            }
            catch (Exception ex)
            {
                Msg = ex.Message;
            }
        }


        public void OnPostGetPicMeterial()
        {
            try
            {
                string access_token = WXApi.getAccessToken().access_token;
                string wxUrl = $"https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={access_token}";
                MaterialList_In paremeter = new MaterialList_In
                {
                    type = "image",
                    offset = 0,
                    count = 20,
                };
                var json = JsonConvert.SerializeObject(paremeter);
                string data = HttpHelper.RequestUrlSendMsg(wxUrl, HttpHelper.HttpMethod.Post, json);


                PicMedia = JsonConvert.DeserializeObject<JOMedia>(data);

            }
            catch (Exception ex)
            {
                Msg = ex.Message;
            }
        }
    }
}