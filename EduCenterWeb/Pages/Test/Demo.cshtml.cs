using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.User;
using EduCenterSrv.DataBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.Test
{
    public class DemoModel : EduBasePageModel
    {
        private EduDbContext _context;

        public string Msg = "";
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

        public void OnPostCreateTecQR()
        {
            Msg = "创建成功！";
        }
    }
}