﻿using EduCenterModel.User.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Pages.User
{
    public class PMyCourse
    {
        public List<RUserCourse> UserCourseList { get; set; }

        public RUserShowCourse UserShowCourse { get; set; }
    }
}