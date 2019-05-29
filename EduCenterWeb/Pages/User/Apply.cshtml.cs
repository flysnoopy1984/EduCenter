using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.BaseEnum;
using EduCenterModel.Course;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class ApplyModel : EduBaseAppPageModel
    {
        private CourseSrv _CourseSrv;
        public List<ECourseTime> CourseTimes { get; set; }

        public List<ECourseSchedule> CourseScheduleList { get; set; }

        public ApplyModel(CourseSrv courseSrv)
        {
            _CourseSrv = courseSrv;
        }

        public void OnGet()
        {
            CourseTimes = StaticDataSrv.CourseTime.OrderBy(a => a.Lesson).ToList();

            CourseScheduleList = _CourseSrv.GetCourseScheduleByYearType(DateTime.Now.Year, CourseScheduleType.Standard);
        }

        

    }
}