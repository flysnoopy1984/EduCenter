using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.WX;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Course.Result;
using EduCenterModel.Session;
using EduCenterModel.WX.MessageTemplate;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class ApplyTrialModel : EduBaseAppPageModel
    {
        private CourseSrv _CourseSrv;
        private TecSrv _TecSrv;

        public UserSession UserSession { get; set; }

        public Dictionary<int, ECourseTime> TrialTime { get; set; }
        public Dictionary<int, List<ECourseInfo>> CourseDic { get; set; }

        public ApplyTrialModel(CourseSrv courseSrv,TecSrv tecSrv)
        {
            _CourseSrv = courseSrv;
            _TecSrv = tecSrv;
        }
        public void OnGet()
        {
            UserSession = base.GetUserSession();

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
            var times = StaticDataSrv.TrialTime;
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    var cls = _CourseSrv.GetCourseInfoClass(courseCode);

                    var errorMsg = _CourseSrv.VerifyUserTrial(us.OpenId, (int)cls.CourseType,date, Lesson);
                    if (errorMsg == EduErrorMessage.ApplyTrial_OverSingleLimit)
                        result.ErrorMsg = "同类型课不能试听超过2次";
                    else if (errorMsg == EduErrorMessage.ApplyTrial_SameTypeExist)
                        result.ErrorMsg = "同时段已经有申请试听";
                    else
                    {
                        ETrialLog log = new ETrialLog
                        {
                            OpenId = us.OpenId,
                            //UserName = us.UserName,
                            TecCode = cls.TecCode,
                            TecName = cls.TecName,
                            CourseCode = cls.CourseCode,
                            CourseName = cls.CourseName,
                            CourseType = (int)cls.CourseType,
                            ApplyDateTime = DateTime.Now,
                            Lesson = Lesson,
                            TrialDateTime = DateTime.Parse(date),
                            TrialLogStatus = (int)TrialLogStatus.UserApply,

                        };
                        _CourseSrv.AddTrial(log);
                        _CourseSrv.SaveChanges();
                    }   
                    
                }
                else
                {
                    result.IntMsg = -1;
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