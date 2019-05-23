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

namespace EduCenterWeb.Pages.Test
{
    public class DemoModel : EduBasePageModel
    {
        private EduDbContext _context;
      

        public string Msg = "";
        public string DbTest = "";
        public DemoModel(EduDbContext context)
        {
            _context = context;
            
          
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
    }
}