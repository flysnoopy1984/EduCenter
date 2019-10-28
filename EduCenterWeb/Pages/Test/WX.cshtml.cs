using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterCore.WX;
using EduCenterModel.BaseEnum;
using EduCenterModel.Course.Result;
using EduCenterModel.WX;
using EduCenterModel.WX.Media;
using EduCenterModel.WX.MessageTemplate;
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
        private CourseSrv _CourseSrv;
        public WXModel(TecSrv TecSrv, UserSrv userSrv, CourseSrv courseSrv)
        {
            _TecSrv = TecSrv;
            _UserSrv = userSrv;
            _CourseSrv = courseSrv;
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

        public void OnPostNewInvite()
        {
            Msg = "创建成功！";
            try
            {
                var trial = _CourseSrv.GetTrialLogById(172);
                UserTrialRemindTemplate wxMessage = new UserTrialRemindTemplate();
                wxMessage.data = wxMessage.GenerateData(trial.OpenId, trial);
                WXApi.SendTemplateMessage<UserTrialRemindTemplate>(wxMessage);

                //UserSignTemplate wxMessage = new UserSignTemplate();
                //wxMessage.data = wxMessage.GenerateData("oh6cV1QhPLj6XPesheYUQ4XtuGTs", "Jacky", DateTime.Now.ToString("yyyy-MM-dd"), "Test", 0, 12, 0, 33);
                //WXApi.SendTemplateMessage<UserSignTemplate>(wxMessage);

                //UserAccountChangeTemplate wxMessage = new UserAccountChangeTemplate();
                //wxMessage.data = wxMessage.GenerateData("oh6cV1QhPLj6XPesheYUQ4XtuGTs",
                //   "jacky",
                //    AmountTransType.Invited_TrialReward,
                //    DateTime.Now,
                //    10,
                //     GlobalSrv.GetRewardAmount(AmountTransType.Invited_TrialReward)
                //    );
                //WXApi.SendTemplateMessage<UserAccountChangeTemplate>(wxMessage);

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