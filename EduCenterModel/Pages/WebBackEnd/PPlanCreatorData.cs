using EduCenterModel.Course;
using EduCenterModel.Teacher;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Pages.WebBackEnd
{
    public class PPlanCreatorData
    {
        public List<ETecSkill> TecSkillList { get; set; }

        public List<ECourseSchedule> ScheduleList { get; set; }
    }
}
