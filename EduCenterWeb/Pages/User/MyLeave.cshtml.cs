using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.WX;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.User;
using EduCenterModel.User.Result;
using EduCenterModel.WX.MessageTemplate;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class MyLeaveModel : EduBaseAppPageModel
    {
        public List<RUserCourseLog> UserCourseLogList { get; set; }

        private CourseSrv _CourseSrv;
        private UserSrv _UserSrv;
        private TecSrv _TecSrv;

        public MyLeaveModel(UserSrv userSrv, CourseSrv courseSrv, TecSrv tecSrv)
        {
            _CourseSrv = courseSrv;
            _UserSrv = userSrv;
            _TecSrv = tecSrv;
        }
        public void OnGet()
        {
            var us = base.GetUserSession();
            if ( us== null)
                return;
            else
            {
                int totalPage;
                UserCourseLogList = _UserSrv.GetUserCourseLogList(us.OpenId,UserCourseLogStatus.Leave,out totalPage, 1, 10);
            }
        }

        
        public IActionResult OnPostGetCourseByDate(string date)
        {
            ResultList<RUserCourseLog> result = new ResultList<RUserCourseLog>();
            var us = base.GetUserSession(false);
            try
            {
                date = DateTime.Parse(date).ToString("yyyy-MM-dd");
               
                if(us !=null)
                {
                    result.List = _UserSrv.GetUserCourseByDate(us.OpenId, date, us.CurrentScheduleType);
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "请重新登陆！";
                }
              
            }
            catch(Exception ex)
            {
                result.ErrorMsg = "数据获取失败,请联系工作人员";
                if(us!=null)
                    NLogHelper.ErrorTxt($"用户请假OpenId:{us.OpenId}");
                NLogHelper.ErrorTxt($"MyLeaveModel[OnPostGetCourseByDate]:{ex.Message}");
            }
            return new JsonResult(result);
        }
        

        public IActionResult OnPostCourseLeave(List<EUserCourseLog> list)
        {
            ResultNormal result = new ResultNormal();
            var us = base.GetUserSession(false);
            try
            {
               
                if (us != null)
                {
                  
                    _UserSrv.UpdateCourseLogToLeave(list, us.OpenId);


                    //wx通知
                    var time = StaticDataSrv.CourseTime;
                    UserLeaveTemplate wxMessage = new UserLeaveTemplate();
                    foreach (var c in list)
                    {
                        var ui = _UserSrv.GetUserInfo(us.OpenId);
                        var cs = _CourseSrv.GetCourseSchedule(c.LessonCode);
                        var desc = $"{cs.CourseName} | [{time[cs.Lesson].TimeRange}]";

                        var tec = _TecSrv.GetOpenIdByLessonCode(c.LessonCode, c.CourseDateTime);

                        if(!string.IsNullOrEmpty(ui.SalesOpenId))
                        {
                            wxMessage.data = wxMessage.GenerateData(ui.SalesOpenId, ui.ChildName, DateTime.Parse(c.CourseDateTime), desc);
                            WXApi.SendTemplateMessage<UserLeaveTemplate>(wxMessage);
                        }
                        if (tec!=null)
                        {
                         
                            wxMessage.data = wxMessage.GenerateData(tec.UserOpenId, ui.ChildName, DateTime.Parse(c.CourseDateTime), desc,true);
                            WXApi.SendTemplateMessage<UserLeaveTemplate>(wxMessage);
                        }
                    }   
                }   
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "请重新登陆！";
                }
                  
            }
            catch(EduException eex)
            {
                result.ErrorMsg = eex.Message;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "请假失败,请联系工作人员";
                if (us != null)
                    NLogHelper.ErrorTxt($"用户请假OpenId:{us.OpenId}");
                NLogHelper.ErrorTxt($"MyLeaveModel[OnPostCourseLeave]:{ex.Message}");
            }
            return new JsonResult(result);
        }
       
    }
}