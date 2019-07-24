using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.User.Result;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class MySignModel : EduBaseAppPageModel
    {
        public List<RUserCourseLog> UserCourseLogList { get; set; }
        private UserSrv _UserSrv;
        private BusinessSrv _BusinessSrv;
        public MySignModel(UserSrv userSrv, BusinessSrv businessSrv)
        {
            _UserSrv = userSrv;
            _BusinessSrv = businessSrv;
        }

       
        public void OnGet()
        {
            var us = base.GetUserSession();
            if (us == null)
                return;
            else
            {
                int totalPage;
                UserCourseLogList = _UserSrv.GetUserCourseLogList(us.OpenId, UserCourseLogStatus.SignIn,out totalPage,1, 10);
            }
        }

        public IActionResult OnPostInitPage()
        {
            ResultList<RUserSign> result = new ResultList<RUserSign>();
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    if(us.UserRole == UserRole.Visitor)
                    {
                        result.ErrorMsg = "您还没有购买课程，请先去购买吧";
                        result.IntMsg = -2;
                        return new JsonResult(result);

                    }
                    result.List =  _UserSrv.GetCurrentUserSign(us.OpenId, us.CurrentScheduleType);
                    

                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "请重新登陆！";
                }

            }
            catch (EduException eex)
            {
                result.ErrorMsg = eex.Message;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "页面加载失败,请联系工作人员";
                NLogHelper.ErrorTxt($"签到页面[OnPostInitPage]:{ex.Message}");
            }
            return new JsonResult(result);
        }

        public IActionResult OnPostSignCourse(string LessonCode)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    _BusinessSrv.UpdateCourseLogToSigned(us.OpenId, 
                        us.MemeberType, 
                        us.CurrentScheduleType, 
                        LessonCode,DateTime.Now);
               
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "请重新登陆！";
                }

            }
            catch (EduException eex)
            {
                if(eex.EduErrorMessage == EduErrorMessage.NoCourseTime)
                {
                    result.IntMsg = (long)EduErrorMessage.NoCourseTime;
                    result.ErrorMsg = BaseEnumSrv.EduErrorMessageName(eex.EduErrorMessage);
                }
                   
                result.ErrorMsg = eex.Message;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "签到失败,请联系工作人员";
                NLogHelper.ErrorTxt($"签到页面[OnPostSignCourse]:{ex.Message}");
            }
            return new JsonResult(result);
        }


    }
}