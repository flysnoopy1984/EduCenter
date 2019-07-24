using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Pages.User;
using EduCenterModel.Session;
using EduCenterModel.User;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class ApplyWinterSummerModel : EduBaseAppPageModel
    {
        public List<ECourseTime> CourseTimes { get; set; }
        private CourseSrv _CourseSrv;

        public ECourseDateRange WSRange { get; set; }
        //private UserSrv _UserSrv;
        private BusinessSrv _BusinessSrv;

        public List<ECourseSchedule> CourseScheduleList;
        public ApplyWinterSummerModel(CourseSrv courseSrv, BusinessSrv businessSrv)
        {
            _CourseSrv = courseSrv;
            _BusinessSrv = businessSrv;
            WSRange = StaticDataSrv.CourseDateRange.Where(a => a.Year == DateTime.Now.Year
            && a.CourseScheduleType == StaticDataSrv.CurrentScheduleType).FirstOrDefault();
           // _UserSrv = userSrv;
        }

        public List<ECourseSchedule> GetAvaliableCourseList(int day, int lesson)
        {
            try
            {
                if (CourseScheduleList != null)
                {
                    return CourseScheduleList.Where(a => a.Day == day && a.Lesson == lesson).ToList();
                }
            }
            catch
            {

            }
            return new List<ECourseSchedule>();
        }

        public void OnGet()
        {
            var us = base.GetUserSession();
            if (us != null)
            {
                CourseTimes = StaticDataSrv.CourseTime.Values.ToList();
                // CourseScheduleList = _CourseSrv.GetSWCourseScheduleByYear(DateTime.Now.Year);
               
                 CourseScheduleList = _CourseSrv.GetCourseScheduleByYearType(DateTime.Now.Year, StaticDataSrv.CurrentScheduleType);

            }
        }

        public IActionResult OnPostInitData()
        {
            ResultObject<PUserApply> result = new ResultObject<PUserApply>();

            try
            {
                result.Entity.CourseTimeList = StaticDataSrv.CourseTime;
                result.Entity.CourseMaxApplyNum = StaticDataSrv.CourseMaxApplyNum;


            }
            catch (Exception ex)
            {
                result.ErrorMsg = "未能获取数据！请联系管理员或稍后再试";
                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }

        public IActionResult OnPostSubmit(List<string> lessonCodeList,bool useRightNow = false)
        {
            ResultNormal result = new ResultNormal();
            CourseScheduleType courseScheduleType = CourseScheduleType.Summer;
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    var needRecharge = UserSession.NeedRecharge(us, courseScheduleType);
                    if (needRecharge < 0)
                    {
                        var csTypeName = BaseEnumSrv.GetCourseScheduleTypeName(courseScheduleType);
                        string errorMsg = $"您的{csTypeName}余额不足，请先去充值";
                        if (needRecharge == -2) errorMsg = $"您的余额不足，请先去充值";
                        result.ErrorMsg = errorMsg;
                        result.IntMsg = -2;
                        return new JsonResult(result);
                    }

                    List<EUserCourse> ucList = new List<EUserCourse>();
                    foreach (var lc in lessonCodeList)
                    {
                        EUserCourse uc = new EUserCourse
                        {
                            CourseScheduleType = courseScheduleType,
                            CreateDateTime = DateTime.Now,
                            LessonCode = lc,
                            UseRightNow = useRightNow,
                            UserOpenId = us.OpenId
                        };
                        ucList.Add(uc);
                    }
                    _BusinessSrv.UserSelectNewCourses(us.OpenId, ucList, courseScheduleType, useRightNow);
                    //更新Session是否跳过当天
                    us.CourseSkipToday = useRightNow;
                   
                    us.CurrentScheduleType = StaticDataSrv.CurrentScheduleType;
                    SetUserSesion(us);
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "请重新登陆";
                }
            }
            catch (EduException ex)
            {
                result.ErrorMsg = ex.Message;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "提交数据错误！请联系管理员或稍后再试";
                NLogHelper.ErrorTxt($"标准班课程选择[OnPostSubmit]:{ex.Message}");
            }
            return new JsonResult(result);
        }
    }
}