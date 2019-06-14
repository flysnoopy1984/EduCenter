using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.Course;
using EduCenterModel.Teacher;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Tec
{
    public class TrialCourseModel : PageModel
    {
        public List<ETecInfo> TecList { get; set; }
        public List<ECourseInfo> CourseList { get; set; }

        private TecSrv _TecSrv;
        private CourseSrv _CourseSrv;


        public TrialCourseModel(TecSrv tecSrv, CourseSrv courseSrv)
        {
            _TecSrv = tecSrv;
            _CourseSrv = courseSrv;
        }
        public void OnGet()
        {
            TecList = _TecSrv.GetAllStaffTec();
        }
    }
}