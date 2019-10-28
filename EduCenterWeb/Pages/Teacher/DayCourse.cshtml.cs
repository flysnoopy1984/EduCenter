using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.WX;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Teacher.Result;
using EduCenterModel.User.Result;
using EduCenterModel.WX.MessageTemplate;
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
        private CourseSrv _CourseSrv;
        private BusinessSrv _BusinessSrv;
        public DayCourseModel(TecSrv tecSrv,UserSrv userSrv, BusinessSrv businessSrv, CourseSrv courseSrv)
        {
            _UserSrv = userSrv;
            _TecSrv = tecSrv;
            _CourseSrv = courseSrv;
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
                    NLogHelper.InfoTxt($"QueryTecDayCourse TecCode:{us.TecCode}");
                    result.List = _TecSrv.GetOneDayCourse(us.TecCode, date);
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "请重新登陆";
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
                var us = GetUserSession(false);
                if(us != null)
                {
                    DateTime signDate = DateTime.Parse(date);
                    var log = _BusinessSrv.UpdateCourseLogToSigned(openId, memberType, csType, lessonCode, signDate, us.OpenId);


                    //wx通知 --Begin
                    var course = _CourseSrv.GetCourseSchedule(log.LessonCode);
                    if(course == null)
                    {
                        result.ErrorMsg = "已签到，但未发送消息通知，请告知管理员！";
                        return new JsonResult(result);
                    }
                    var userAccount = _UserSrv.GetUserAccount(openId);

                    if (userAccount.ReduceTime == 0)
                        userAccount.ReduceTime = 2;
                    var time = StaticDataSrv.CourseTime; 
                   
                    UserSignTemplate wxMessage = new UserSignTemplate();
                    wxMessage.data = wxMessage.GenerateData(openId, log.SignName,
                        $"{log.CourseDateTime} | {time[course.Lesson].TimeRange}", 
                        course.CourseName,
                        userAccount.ReduceTime,
                        userAccount.RemainCourseTime,
                        userAccount.RemainSummerTime,
                        userAccount.RemainWinterTime);
                    WXApi.SendTemplateMessage<UserSignTemplate>(wxMessage);
                    //wx通知 --End

                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "请重新登陆";
                }

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