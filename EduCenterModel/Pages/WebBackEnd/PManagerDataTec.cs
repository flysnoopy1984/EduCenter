﻿using EduCenterModel.Common;
using EduCenterModel.Course.Result;
using EduCenterModel.Teacher;
using EduCenterModel.Teacher.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Pages.WebBackEnd
{
    public class PManagerDataTec
    {
        public List<STec> TecList { get; set; }

        public List<SiKsV> SkillLevelList { get; set; }

        public List<SiKsV> CourseType { get; set; }

        //  public List<SCourse> CourseList { get; set; }
        // public List<>

        //   public List<ETecSkill> TecSkill { get; set; }

        //public List<ETecLeave> TecLeave { get; set; }
    }
}