using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Pages.User;
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
        //private UserSrv _UserSrv;
        private BusinessSrv _BusinessSrv;

        public List<ECourseSchedule> CourseScheduleList;
        public ApplyWinterSummerModel(CourseSrv courseSrv, BusinessSrv businessSrv)
        {
            _CourseSrv = courseSrv;
            _BusinessSrv = businessSrv;
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

        public IActionResult OnPostSubmit(List<string> lessonCodeList)
        {
            ResultNormal result = new ResultNormal();
            CourseScheduleType courseScheduleType = StaticDataSrv.CurrentScheduleType;
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    List<EUserCourse> ucList = new List<EUserCourse>();
                    foreach (var lc in lessonCodeList)
                    {
                        EUserCourse uc = new EUserCourse
                        {
                            CourseScheduleType = courseScheduleType,
                            CreateDateTime = DateTime.Now,
                            LessonCode = lc,

                            UserOpenId = us.OpenId
                        };
                        ucList.Add(uc);
                    }
                    _BusinessSrv.UserSelectNewCourses(us.OpenId, ucList);
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