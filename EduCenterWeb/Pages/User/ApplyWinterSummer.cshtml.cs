using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.BaseEnum;
using EduCenterModel.Course;
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
    }
}