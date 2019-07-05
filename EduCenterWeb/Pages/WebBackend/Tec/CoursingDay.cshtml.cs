using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Teacher;
using EduCenterModel.Teacher.Result;
using EduCenterModel.User.Result;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Tec
{
    public class CoursingDayModel : EduBasePageModel
    {
        public List<ETecInfo> TecList { get; set; }
        private TecSrv _TecSrv;
        private UserSrv _UserSrv;
        public List<ECourseTime> CourseTimes { get; set; }
        public CoursingDayModel(TecSrv tecSrc, UserSrv uerSrv)
        {
            _TecSrv = tecSrc;
            _UserSrv = uerSrv;
            
        }
        public void OnGet()
        {
            TecList = _TecSrv.GetAllStaffTec();
            CourseTimes = StaticDataSrv.CourseTime.Values.ToList();
        }

        public IActionResult OnPostQueryOneDayCourse(string tecCode,DateTime date)
        {
            ResultList<RTecCourse> result = new ResultList<RTecCourse>();
            try
            {
               
                result.List = _TecSrv.GetOneDayCourse(tecCode, date);
                foreach (var c in result.List)
                {
                    c.CoursingStatusName = BaseEnumSrv.GetCoursingStatusName(c.CoursingStatus);
                }
            }
            catch(Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }
        
            return new JsonResult(result);
        }

        public IActionResult OnPostQueryUserCourse(string lessonCode,string date)
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