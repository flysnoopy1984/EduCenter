using EduCenterModel.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Pages.User
{
    public class PUserCourseTime
    {
        public EUserAccount UserAccount { get; set; }

        public double TotalCourseLog { get; set; }

        public double TotalAbsence { get; set; }
    }
}
