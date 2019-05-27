using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Course
{
    public class PlanCreatorModel : EduBasePageModel
    {
        public List<ECourseTime> CourseTimes { get; set; }

        public List<ECourseInfo> CourseList { get; set; }

        private CourseSrv _CourseSrv;
        private TecSrv _TecSrv;

        public PlanCreatorModel(CourseSrv courseSrv, TecSrv tecSrv)
        {
            _CourseSrv = courseSrv;
            _TecSrv = tecSrv;
        }

        public void OnGet()
        {
            CourseTimes = StaticDataSrv.CourseTime.OrderBy(a => a.Lesson).ToList();
            
            CourseList = _CourseSrv.GetAllList();
        }

        public IActionResult OnPostSave(List<ECourseSchedule> list)
        {
            ResultNormal result = new ResultNormal();
            try
            {

            }
            catch(Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }
            return new JsonResult(result);
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