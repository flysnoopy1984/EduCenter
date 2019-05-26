using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterSrv
{
    public class BaseEnumSrv
    {
        public List<SiKsV> GetSkillLevel()
        {
            List<SiKsV> r = new List<SiKsV>();
            foreach(SkillLevel sk in Enum.GetValues(typeof(SkillLevel)) )
            {
                string v = "";
                switch(sk)
                {
                    case SkillLevel.Main:
                        v = "主技能";
                        break;
                    case SkillLevel.Good:
                        v = "一般";
                        break;
                    case SkillLevel.None:
                        v = "无";
                        break;
                    case SkillLevel.Greate:
                        v = "熟练";
                        break;
      

                }
                r.Add(new SiKsV
                {
                    Key = (int)sk,
                    Value = v,
                });
            }
            return r;
        }

        public List<SiKsV> GetCourseType()
        {
            List<SiKsV> r = new List<SiKsV>();
            foreach (CourseType ct in Enum.GetValues(typeof(CourseType)))
            {
                string v = "";
                switch (ct)
                {
                    case CourseType.MS:
                        v = "美术";
                        break;
                    case CourseType.SF:
                        v = "书法";
                        break;
                    case CourseType.WQ:
                        v = "围棋";
                        break;
                    default:
                        v = "未分类";
                        break;


                }
                r.Add(new SiKsV
                {
                    Key = (int)ct,
                    Value = v,
                });
            }
            return r;
        }
    }
}
