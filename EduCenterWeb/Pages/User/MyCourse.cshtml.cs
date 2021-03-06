﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Pages.User;
using EduCenterModel.Session;
using EduCenterModel.User;
using EduCenterModel.User.Result;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class MyCourseModel : EduBaseAppPageModel
    {
        private UserSrv _UserSrv;

        public Dictionary<int, ECourseTime> CourseTime { get; set; }
      

        public List<RUserCourseLog> UserCourseLogList { get; set; }

        public RUserCourseLog NextCourse { get; set; }

        public RUserCourseLog CurrentCourse { get; set; }

        public Dictionary<int,string> UserCourseLogStatus { get; set; }

        public string CourseScheduleTypeName
        {
            get
            {
                var us = base.GetUserSession(false);
                if(us!=null)
                {
                    return us.CurrentScheduleTypeName;
                }
                return "";
            }
        }

        public MyCourseModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }
        public void OnGet()
        {
            CourseTime = StaticDataSrv.CourseTime;
            UserCourseLogStatus = BaseEnumSrv.UserCourseLogStatusList;

            var us = base.GetUserSession();
            if (us != null)
            {
                      
                UserCourseLogList = _UserSrv.GetUserCourseLogHistory(us.OpenId,10); 
            }
           
        }

        public IActionResult OnPostInitData()
        {
            ResultObject<PMyCourse> result = new ResultObject<PMyCourse>();
            try
            {
                var us = GetUserSession(false);
                if (us != null)
                {

                    CourseScheduleType courseScheduleType = _UserSrv.GetCurrentCourseScheduleType(us.OpenId,us.MemeberType);
                    var needRecharge = UserSession.NeedRecharge(us, courseScheduleType);
                    if (needRecharge < 0)
                    {
                        var csTypeName = BaseEnumSrv.GetCourseScheduleTypeName(courseScheduleType);
                        string errorMsg = $"您的{csTypeName}余额不足，请先去充值";
                        if (needRecharge == -2) errorMsg = $"您的余额不足，请先去充值";
                        result.ErrorMsg = errorMsg;
                        result.IntMsg = -2;
                        return new JsonResult(result);
                    }

                    //VIP用户课程(不管暑假寒假)
                    if(us.MemeberType == MemberType.VIP)
                        result.Entity.UserCourseList = _UserSrv.GetUserCourseAvaliable(us.OpenId);
                    else
                    {
                        //普通用户 先看是否有标准课，如果有标准课，全部放开。
                        if(us.UserAccount.RemainCourseTime>0)
                            result.Entity.UserCourseList = _UserSrv.GetUserCourseAvaliable(us.OpenId);
                        else
                            //如果无标准课，则显示暑假，寒假课
                            result.Entity.UserCourseList = _UserSrv.GetUserCourseAvaliable(us.OpenId,(int)courseScheduleType);
                    }
                    result.IntMsg = (int)courseScheduleType;
                    //获取用户最近课程
                    if (result.Entity.UserCourseList.Count > 0)
                    {
                        DateTime startDate = DateTime.Now;
                        if (us.CourseSkipToday)
                            startDate = startDate.AddDays(1);
                        result.Entity.UserShowCourse = _UserSrv.GetNextUserCourse(result.Entity.UserCourseList, startDate);

                    }
                
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "需要重新登陆";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "未能获取数据！请联系客服或稍后再试";
                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }

      
    }
}