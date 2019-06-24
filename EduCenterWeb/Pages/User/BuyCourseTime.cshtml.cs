using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.Course;
using EduCenterModel.Session;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class BuyCourseTimeModel : EduBaseAppPageModel
    {
        private CourseSrv _CourseSrv;
        public UserSession UserSession { get; set; }
        public List<ECoursePrice> PriceList { get; set; }
        public BuyCourseTimeModel(CourseSrv courseSrv)
        {
            _CourseSrv = courseSrv;
        }

        public void OnGet()
        {
            UserSession = this.GetUserSession();
            if(UserSession != null)
                PriceList = _CourseSrv.GetCoursePriceList();

        }
    }
}