using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.WX;
using EduCenterModel.User;
using EduCenterModel.WX;
using EduCenterSrv.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using Microsoft.EntityFrameworkCore;
using EduCenterSrv;
using EduCenterModel.Course;
using Newtonsoft.Json;
using EduCenterModel.BaseEnum;

namespace EduCenterWeb.Pages.Test
{
    public class DemoModel : EduBasePageModel
    {
        private OrderSrv  _orderSrv;
        private UserSrv _userSrv;
        private CourseSrv _courseSrv;
        private BusinessSrv _busSrv;
      

        public string Msg = "";
        public string DbTest = "";
        public DemoModel(OrderSrv orderSrv, UserSrv userSrv, CourseSrv courseSrv, BusinessSrv busSrv)
        {
            _orderSrv = orderSrv;
            _userSrv = userSrv;
            _courseSrv = courseSrv;
            _busSrv = busSrv;

    }
        public void OnGet()
        {
           
            //EUserInfo ui = new EUserInfo
            //{
            //    Name = "Test",
            //    OpenId = "xxxxxx",

            //};
            //_context.DBUserInfo.Add(ui);
            //_context.SaveChanges();
        }

        public void OnPostTestDb()
        {
            DbTest = "Done";
            DbTest =  EduCodeGenerator.GetTecCode(1);
           // _context.DBCourseInfo.Count(a=>a.RecordStatus=1)
        //    DbTest =  $"{_context.DBCourseInfo("select count(1) from CourseInfo")}";
        }

        public void OnPostCreateTecQR()
        {
            Msg = "创建成功！";

            string qrDownFilePath = EduEnviroment.GetQRInviteTecFilePath("WXInvite.png");
            string bkFilePath = EduEnviroment.GetQRFilePath("InviteBK.png");
            string finalFilePath = EduEnviroment.GetQRInviteTecFilePath("EduTecInvite.png");
            try
            {
                AccessToken accessToken = WXApi.getAccessToken();
                WXQRResult result = WXApi.getQR(WxConfig.QR_Invite_TecPre, accessToken.access_token);
                WXApi.DownLoadWXQR(result.ticket, qrDownFilePath);


                QRHelper.AddBKForQR(bkFilePath, qrDownFilePath, finalFilePath);
            }
            catch(Exception ex)
            {
                Msg = ex.Message;
            }
        }

        public void OnPostPayCourseTime()
        {
            Msg = "创建成功！";
            try
            {
                string openId = "o3nwE0qI_cOkirmh_qbGGG-5G6B0";

                ECoursePrice cp = _courseSrv.GetStandPrice();
                cp.Qty = 10;
                cp.Price = 2000;

                List<EUserCourse> eUserCourses = new List<EUserCourse>();

               
                eUserCourses.Add(new EUserCourse
                {
                    CourseScheduleType = cp.CourseScheduleType,
                    LessonCode = "2019_1_5_MS-1_1_Standard",
                    UserOpenId = openId,
                });
           
                eUserCourses.Add(new EUserCourse
                {
                    CourseScheduleType = cp.CourseScheduleType,
                    LessonCode = "2019_1_6_WQ-1_1_Standard",
                    UserOpenId = openId,
                });
            
                eUserCourses.Add(new EUserCourse
                {
                    CourseScheduleType = cp.CourseScheduleType,
                    LessonCode = "2019_2_5_SF-1_1_Standard",
                    UserOpenId = openId,
                });

                //var order = _busSrv.PayCourseOrder(openId, cp);

                //_busSrv.PayCourseSuccess(order.OrderId);

                //_busSrv.UserSelectNewCourses(openId, eUserCourses);
            }
            catch(Exception ex)
            {
                Msg = ex.Message;
            }
        }
        public void OnPostGetHolidayJson()
        {
           // var list = _userSrv.GetHolidayJson();
           //var json =  JsonConvert.SerializeObject(list);
           // Msg = json;
        }



    }
}