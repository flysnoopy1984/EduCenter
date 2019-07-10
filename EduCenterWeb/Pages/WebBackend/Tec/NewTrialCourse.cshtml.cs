using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.Course;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Tec
{
    public class NewTrialCourseModel : EduBasePageModel
    {
        private TecSrv _TecSrv;
        private CourseSrv _CourseSrv;
        public Dictionary<int, List<ECourseInfo>> CourseDic { get; set; }

        public Dictionary<int, ECourseTime> TrialTime { get; set; }

        public NewTrialCourseModel(TecSrv tecSrv, CourseSrv courseSrv)
        {
            _TecSrv = tecSrv;
            _CourseSrv = courseSrv;


        }

        public void OnGet()
        {
            TrialTime = StaticDataSrv.TrialTime;

            var list = _CourseSrv.GetAllList();
            var curct = -1;
            CourseDic = new Dictionary<int, List<ECourseInfo>>();
            foreach (var c in list)
            {
                int ct = (int)c.CourseType;
                if (curct != ct)
                {
                    curct = ct;
                    CourseDic.Add(ct, new List<ECourseInfo>());
                    CourseDic[ct].Add(c);
                }
                else
                    CourseDic[ct].Add(c);

            }

        }
    }
}