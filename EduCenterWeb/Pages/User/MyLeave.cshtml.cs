﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.User.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class MyLeaveModel : EduBaseAppPageModel
    {
        private UserSrv _UserSrv;

        public MyLeaveModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }
        public void OnGet()
        {
            if (base.GetUserSession() == null)
                return;
        }

        
        public IActionResult OnPostGetCourseByDate(string date)
        {
            ResultList<RUserCourseLog> result = new ResultList<RUserCourseLog>(); 
            try
            {
                date = DateTime.Parse(date).ToString("yyyy-MM-dd");
                var us = base.GetUserSession();
                if(us !=null)
                {
                    result.List = _UserSrv.GetUserCourseByDate(us.OpenId, date, CourseScheduleType.Standard);
                }
              
            }
            catch(Exception ex)
            {
                result.ErrorMsg = "数据获取失败";
                NLogHelper.ErrorTxt($"[OnPostGetCourseByDate]:{ex.Message}");
            }
            return new JsonResult(result);
        }
    }
}