using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.User;
using EduCenterModel.User.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class MyLeaveModel : EduBaseAppPageModel
    {
        public List<RUserCourseLog> UserCourseLogList { get; set; }

        private UserSrv _UserSrv;

        public MyLeaveModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }
        public void OnGet()
        {
            var us = base.GetUserSession();
            if ( us== null)
                return;
            else
            {

                UserCourseLogList = _UserSrv.GetUserCourseLogList(us.OpenId, CourseScheduleType.Standard, UserCourseLogStatus.Leave);
            }
        }

        
        public IActionResult OnPostGetCourseByDate(string date)
        {
            ResultList<RUserCourseLog> result = new ResultList<RUserCourseLog>(); 
            try
            {
                date = DateTime.Parse(date).ToString("yyyy-MM-dd");
                var us = base.GetUserSession(false);
                if(us !=null)
                {
                    result.List = _UserSrv.GetUserCourseByDateForLeave(us.OpenId, date, CourseScheduleType.Standard);
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
                NLogHelper.ErrorTxt($"MyLeaveModel[OnPostGetCourseByDate]:{ex.Message}");
            }
            return new JsonResult(result);
        }
        

        public IActionResult OnPostCourseLeave(List<EUserCourseLog> list)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    //foreach(var c in list)
                    //{
                    //    c.UserCourseLogStatus = UserCourseLogStatus.Leave;
                    //}
                    _UserSrv.UpdateCourseLogToLeave(list, us.OpenId);
                }   
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "请重新登陆！";
                }
                   



            }
            catch (Exception ex)
            {
                result.ErrorMsg = "请假失败,请联系工作人员";
                NLogHelper.ErrorTxt($"MyLeaveModel[OnPostCourseLeave]:{ex.Message}");
            }
            return new JsonResult(result);
        }
       
    }
}