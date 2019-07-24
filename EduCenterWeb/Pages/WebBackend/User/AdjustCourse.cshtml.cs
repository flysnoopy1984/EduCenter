using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Course.Result;
using EduCenterModel.User;
using EduCenterModel.User.Result;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.User
{
    public class AdjustCourseModel : EduBasePageModel
    {
        private UserSrv _UserSrv;
        private CourseSrv _CourseSrv;

        public List<SiKsV> CourseScheduleTypeList { get; set; }

        public Dictionary<int, ECourseTime> TimeList { get; set; }


        public List<EUserInfo> MemberList { get; set; }
        public AdjustCourseModel(UserSrv userSrv, CourseSrv courseSrv)
        {
            _UserSrv = userSrv;
            _CourseSrv = courseSrv;
          
        }
        public void OnGet()
        {
            string userOpenId = Request.Query["openId"];
            if(!string.IsNullOrEmpty(userOpenId))
            {
                MemberList = _UserSrv.GetAllMemberList();
            }

            CourseScheduleTypeList = BaseEnumSrv.CourseScheduleTypeList;
            TimeList = StaticDataSrv.CourseTime;
        }

        public IActionResult OnPostGetCourseScheduleList(int year,int day,int lesson,CourseScheduleType courseScheduleType )
        {
            ResultList<SCourseSchedule> result = new ResultList<SCourseSchedule>();
            try
            {
                result.List = _CourseSrv.GetCourseSchedule_ForSelection(year, courseScheduleType, day,lesson);

            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;

            }
            return new JsonResult(result);
        }

        public IActionResult OnPostQueryUserCourse(string openId)
        {
            ResultList<RUserCourse> result = new ResultList<RUserCourse>();

            try
            {
                result.List = _UserSrv.GetUserAllCourse(openId);

            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
              
            }
            return new JsonResult(result);
        }
    }
}