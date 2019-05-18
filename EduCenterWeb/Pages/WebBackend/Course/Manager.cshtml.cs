﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.Course;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Course
{
    public class ManangerModel : PageModel
    {
        private CourseSrv _CourseSrv;
        public ManangerModel(CourseSrv courseSrv)
        {
            _CourseSrv = courseSrv;
        }
        public List<ECourseInfo> CourseList;
        public  void OnGet()
        {
            CourseList = _CourseSrv.GetAllList();
        }
    }
}