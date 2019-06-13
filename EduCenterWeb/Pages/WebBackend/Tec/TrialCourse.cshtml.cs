using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.Course;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Tec
{
    public class TrialCourseModel : PageModel
    {
        public List<ECourseInfo> CourseList { get; set; }

        private UserSrv _UserSrv;
        private CourseSrv _CourseSrv;


        public TrialCourseModel(UserSrv userSrv, CourseSrv courseSrv)
        {
            _UserSrv = userSrv;
            _CourseSrv = courseSrv;
        }
        public void OnGet()
        {
            _CourseSrv.GetSimpleList();
        }
    }
}