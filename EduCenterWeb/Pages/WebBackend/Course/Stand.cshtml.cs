using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.Course;
using EduCenterModel.Course.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Course
{
    public class PlanModel : PageModel
    {
        public List<string> CourseTimes { get; set; }

        public List <ECourseInfo> CourseList { get; set; }

        private CourseSrv _CourseSrv;
        private TecSrv _TecSrv;

        public PlanModel(CourseSrv courseSrv, TecSrv tecSrv)
        {
            _CourseSrv = courseSrv;
            _TecSrv = tecSrv;
        }

        public void OnGet()
        {
            CourseTimes = new List<string>
            {
               "9:00-10:30",
               "10:30-12:00",
               "13:00-14:30",
               "14:30-16:00",
               "16:30-18:00",
               "18:30-20:00",
            };

            CourseList = _CourseSrv.GetAllList();
        }

        //public IActionResult OnPostGet(string code)
        //{
        //    ResultObject<RTecAllInfo> result = new ResultObject<RTecAllInfo>();
        //    try
        //    {
        //        result.Entity = _TecSrv.GetAllInfo(code);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.ErrorMsg = ex.Message;
        //    }

        //    return new JsonResult(result);
        //}
    }
}