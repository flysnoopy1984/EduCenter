using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
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

        //public List<ECourseSchedule> CourseScheduleList { get; set; }

        public ApplyModel(CourseSrv courseSrv)
        {
            _CourseSrv = courseSrv;
        }

        public void OnGet()
        {
            CourseTimes = StaticDataSrv.CourseTime.OrderBy(a => a.Lesson).ToList();           
        }

        public IActionResult OnPostInitData()
        {
            ResultList<ECourseSchedule> result = new ResultList<ECourseSchedule>();
            try
            {
                result.List = _CourseSrv.GetCourseScheduleByYearType(DateTime.Now.Year, CourseScheduleType.Standard);

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