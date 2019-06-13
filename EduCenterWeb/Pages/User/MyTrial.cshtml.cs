using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Course.Result;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class MyTrialModel : EduBaseAppPageModel
    {
        private CourseSrv _CourseSrv;

        public Dictionary<int,ECourseTime> TrialTime { get; set; }
        public Dictionary<int, List<ECourseInfo>> CourseDic { get; set; }
        public MyTrialModel(CourseSrv courseSrv)
        {
            _CourseSrv = courseSrv;
        }

       
       
        public void OnGet()
        {
            var list = _CourseSrv.GetAllList();
            var curct = -1;
            CourseDic = new Dictionary<int, List<ECourseInfo>>();
            foreach (var c in list)
            {
                int ct = (int)c.CourseType;
                if (curct != ct)
                {
                    curct = ct;
                    CourseDic.Add(ct,new List<ECourseInfo>());
                    CourseDic[ct].Add(c);
                }
                else
                    CourseDic[ct].Add(c);

            }

            TrialTime = StaticDataSrv.TrialTime;
        }
        public IActionResult OnPostGetAvaliableTrial(string date,string CourseCode)
        {
            ResultList<RTrialLog> result = new ResultList<RTrialLog>();
            try
            {
                date = DateTime.Parse(date).ToString("yyyy-MM-dd");
                var us = base.GetUserSession();
                if (us != null)
                {
                    
                }

            }
            catch (Exception ex)
            {
                result.ErrorMsg = "数据获取失败,请联系工作人员";
                NLogHelper.ErrorTxt($"MyLeaveModel[OnPostGetCourseByDate]:{ex.Message}");
            }
            return new JsonResult(result);
        }
        
    }
}