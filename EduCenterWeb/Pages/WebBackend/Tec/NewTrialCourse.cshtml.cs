using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.WX;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Course.Result;
using EduCenterModel.User;
using EduCenterModel.WX.MessageTemplate;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Tec
{
    public class NewTrialCourseModel : EduBasePageModel
    {
        private UserSrv _UserSrv;
        private CourseSrv _CourseSrv;
        private TecSrv _TecSrv;
     
        public Dictionary<int, List<ECourseInfo>> CourseDic { get; set; }

        public Dictionary<int, ECourseTime> TrialTime { get; set; }

        public NewTrialCourseModel(UserSrv userSrv, CourseSrv courseSrv, TecSrv tecSrv)
        {
            _TecSrv = tecSrv;
            _UserSrv = userSrv;
            _CourseSrv = courseSrv;
        }
        public List<EUserInfo> SalesUserList { get; set; }

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
            SalesUserList = _UserSrv.GetSalesUserList();
            //var Id = Request.Query["Id"];
            //if (!string.IsNullOrEmpty(Id))
            //{
            //  //  UpdateTrialLog = _CourseSrv.GetTrialLogById(Convert.ToInt64(Id));
            //}
        }

        public IActionResult OnPostQueryTrialById(long Id)
        {
            ResultObject<RTrialLog> result = new ResultObject<RTrialLog>();
            try
            {
                result.Entity =  _CourseSrv.GetTrialLogById(Id);
               
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }

        public IActionResult OnPostUpdateTrial(RTrialLog updateTrial)
        {
            ResultNormal result = new ResultNormal();
            var times = StaticDataSrv.TrialTime;
            bool needWX = false ;
            try
            {
               if(updateTrial.Id <=0)
               {
                    result.ErrorMsg = "没有找到试听课，无法保存！";
               }
               else
               {
                    ETrialLog origTrial = _CourseSrv.GetTrialLog(updateTrial.Id);

                    origTrial.Lesson = updateTrial.Lesson;
                    origTrial.TrialDateTime = updateTrial.TrialDateTime;
                    origTrial.CourseCode = updateTrial.CourseCode;
                   
                    var cls = _CourseSrv.GetCourseInfoClass(origTrial.CourseCode);
                    origTrial.TecCode = cls.TecCode;
                    origTrial.TecName = cls.TecName;
                    origTrial.CourseType = cls.CourseType;
                    origTrial.CourseName = cls.CourseName;

                    var ui = _UserSrv.GetUserInfo(updateTrial.OpenId);
                  //  ui. = updateTrial.UserRealName;
                    ui.Phone = updateTrial.UserPhone;
                    ui.SalesOpenId = updateTrial.SalesOpenId;
                   
                    if(origTrial.TrialLogStatus == TrialLogStatus.UserApply)
                    {
                        needWX = true;
                        origTrial.TrialLogStatus = TrialLogStatus.TecConfirm;
                    }
                    _CourseSrv.SaveChanges();

                    //微信发送
                    if(needWX)
                    {
                        TecTrialRemindTemplate data = new TecTrialRemindTemplate();
                        RTrialLog rTrialLog = new RTrialLog();
                        rTrialLog.InitFromETrialLog(origTrial);
                        rTrialLog.SalesOpenId = updateTrial.SalesOpenId;
                        rTrialLog.SalesName = updateTrial.SalesName;
                        rTrialLog.TrialTimeStr = times[rTrialLog.Lesson].TimeRange;
                        rTrialLog.UserRealName = ui.ChildName;
                        var teacher = _TecSrv.Get(origTrial.TecCode);
                        if (teacher != null)
                        {
                           // teacher.UserOpenId = "oh6cV1QhPLj6XPesheYUQ4XtuGTs";
                            data = data.GenerateData(teacher.UserOpenId, rTrialLog);
                            result = WXApi.SendTemplateMessage<TecTrialRemindTemplate>(data);
                            result.IntMsg = 10;
                        }
                    }
                    

                }

            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }

    }
}