using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.Common;
using EduCenterModel.Teacher.Result;
using EduCenterModel.User.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.Teacher
{
    public class DayCourseModel : EduBaseAppPageModel
    {
        private TecSrv _TecSrv;
        private UserSrv _UserSrv;
        public DayCourseModel(TecSrv tecSrv,UserSrv userSrv)
        {
            _UserSrv = userSrv;
            _TecSrv = tecSrv;
        }
        public void OnGet()
        {
            
        }

        public IActionResult OnPostQueryTecDayCourse(string date)
        {
            ResultList<RTecCourse> result = new ResultList<RTecCourse>();

            try
            {
                var us = GetUserSession(false);
                if(us != null)
                {
                    result.List = _TecSrv.GetOneDayCourse(us.TecCode, date);
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "未能获取数据！请联系管理员或稍后再试";
                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }

        public IActionResult OnPostQueryUserCourse(string lessonCode, string date)
        {
            ResultList<RUserCurrentCourse> result = new ResultList<RUserCurrentCourse>();
            try
            {
                result.List = _UserSrv.GetUserCouseLogByLessonCode(lessonCode, DateTime.Parse(date).ToString("yyyy-MM-dd"));
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }
    }
}