using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Course.Result;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class ApplyTrialModel : EduBaseAppPageModel
    {
        private CourseSrv _CourseSrv;

        public Dictionary<int, ECourseTime> TrialTime { get; set; }
        public Dictionary<int, List<ECourseInfo>> CourseDic { get; set; }

        public ApplyTrialModel(CourseSrv courseSrv)
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
                    CourseDic.Add(ct, new List<ECourseInfo>());
                    CourseDic[ct].Add(c);
                }
                else
                    CourseDic[ct].Add(c);

            }

            TrialTime = StaticDataSrv.TrialTime;
        }

        public IActionResult OnPostSubmitTrial(string courseCode,int Lesson,string date)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    if (_CourseSrv.VerifyUserTrial(us.OpenId, courseCode) == EduErrorMessage.ApplyTrial_OverSingleLimit)
                        result.ErrorMsg = "同类型课不能试听超过2次";
                    if(_CourseSrv.VerifyUserTrial(us.OpenId,courseCode, date) == EduErrorMessage.ApplyTrial_Exist)
                        result.ErrorMsg = "已经申请试听";    
                    else
                    {
                        var cls = _CourseSrv.GetCourseInfoClass(courseCode);
                        ETrialLog log = new ETrialLog
                        {
                            OpenId = us.OpenId,
                            UserName = us.UserName,
                            TecCode = cls.TecCode,
                            TecName = cls.TecName,
                            CourseCode = cls.CourseCode,
                            CourseName = cls.CourseName,
                            ApplyDateTime = DateTime.Now,
                            Lesson = Lesson,
                            TrialDateTime = DateTime.Parse(date),
                            TrialLogStatus = TrialLogStatus.UserApply
                        };
                        _CourseSrv.AddTrial(log);
                        _CourseSrv.SaveChanges();
                    }
                }
                else
                {
                    result.ErrorMsg = "需要您重新登录！";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "提交申请失败,请联系工作人员";
                NLogHelper.ErrorTxt($"ApplyTrialModel[OnPostSubmitTrial]:{ex.Message}");
            }
            return new JsonResult(result);
        }

      
    }
}