using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Teacher.Result;
using EduCenterModel.User.Result;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.Teacher
{
    public class DayCourseModel : EduBaseAppPageModel
    {
        private TecSrv _TecSrv;
        private UserSrv _UserSrv;
        private BusinessSrv _BusinessSrv;
        public DayCourseModel(TecSrv tecSrv,UserSrv userSrv, BusinessSrv businessSrv)
        {
            _UserSrv = userSrv;
            _TecSrv = tecSrv;
            _BusinessSrv = businessSrv;
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

        public IActionResult OnPostSignForUser(string openId,string lessonCode,MemberType memberType,string date)
        {
            ResultNormal result = new ResultNormal();
            try
            {
               var csType =  _UserSrv.GetCurrentCourseScheduleType(openId, memberType);
                DateTime signDate = DateTime.Parse(date);
               _BusinessSrv.UpdateCourseLogToSigned(openId, memberType, csType, lessonCode, signDate, true);

                result.SuccessMsg = BaseEnumSrv.GetUserCourseLogStatusNameForTec(UserCourseLogStatus.SignIn);
            }
            catch(Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }
            return new JsonResult(result);
        }
    }
}