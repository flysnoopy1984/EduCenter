using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Pages.User;
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
        private UserSrv _UserSrv;

        public List<ECourseSchedule> CourseScheduleList;
        public ApplyWinterSummerModel(CourseSrv courseSrv, UserSrv userSrv)
        {
            _CourseSrv = courseSrv;
            _UserSrv = userSrv;
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

                CourseScheduleList = _CourseSrv.GetCourseScheduleByYearType(DateTime.Now.Year, CourseScheduleType.Summer);
            }
        }

        public IActionResult OnPostInitData()
        {
            ResultObject<PUserApply> result = new ResultObject<PUserApply>();
            // ResultObject<> result = new ResultObject<Dictionary<int, ECourseTime>>();
            try
            {
                //  result.Entity.CourseScheduleList = _CourseSrv.GetCourseScheduleByYearType(DateTime.Now.Year, CourseScheduleType.Standard);
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
    }
}