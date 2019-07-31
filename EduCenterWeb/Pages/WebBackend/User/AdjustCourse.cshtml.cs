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
        private BusinessSrv _BusinessSrv;
        public List<SiKsV> CourseScheduleTypeList { get; set; }

        public Dictionary<int, ECourseTime> TimeList { get; set; }


        public List<EUserInfo> MemberList { get; set; }
        public AdjustCourseModel(UserSrv userSrv, CourseSrv courseSrv, BusinessSrv businessSrv)
        {
            _UserSrv = userSrv;
            _CourseSrv = courseSrv;
            _BusinessSrv = businessSrv;
          
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
                result.List = _CourseSrv.GetCourseSchedule_ForSelection(year, day,lesson);

            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;

            }
            return new JsonResult(result);
        }

        public IActionResult OnPostGetUserInfo(string openId)
        {
            ResultObject<RUserInfo_AdjustCourse> result = new ResultObject<RUserInfo_AdjustCourse>();

            try
            {
                result.Entity = _UserSrv.GetUserInfo_ForAdjustCourse(openId);
           

            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;

            }
            return new JsonResult(result);
        }

        public IActionResult OnPostQueryUserCourse(string openId)
        {
            ResultList<RUserCouser_WithCoureSchedule> result = new ResultList<RUserCouser_WithCoureSchedule>();

            try
            {
                result.List = _UserSrv.GetUserAllCourse_WithSchedule(openId);
                foreach(var r in result.List)
                {
                    r.ScheduleList = _CourseSrv.GetCourseSchedule_ForSelection(r.Year, r.Day, r.Lesson);
                }

            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
              
            }
            return new JsonResult(result);
        }

        //删除用户课程
        public IActionResult OnPostDeleteUserCourse(string openId,string LessonCode)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                /* 
                 * 删除用户课程表
                 * 更新课程报名数
                 * 删除老师课程
                 */
                _BusinessSrv.DeleteUserCourse(openId, LessonCode);
            }
            catch(Exception  ex)
            {
                result.ErrorMsg = ex.Message;
            }
            return new JsonResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="LessonCode">选择后的用户课程</param>
        /// <param name="origUserCourseId">之前的用户课程Id</param>
        /// <param name="courseScheduleType"></param>
        /// <returns></returns>
        public IActionResult OnPostSaveUserCourse(string openId, string lessonCode, long origUserCourseId,CourseScheduleType courseScheduleType)
        {
            ResultObject<EUserCourse> result = new ResultObject<EUserCourse>();
            try
            {
             
                if(origUserCourseId == 0)
                {
                    _BusinessSrv.UserSelectNewCourses(openId, lessonCode, courseScheduleType,true,true);
                }
                else
                {
                    var origUserCoures = _UserSrv.GetUserCouresById(origUserCourseId);
                    //也许只是假期版和标准版切换
                    if(origUserCoures.LessonCode == lessonCode)
                        _UserSrv.SwitchUserCourseScheduleType(openId, lessonCode, courseScheduleType);
                    else
                    {
                        //删除老课程
                        //创建新课程
                        _BusinessSrv.AdjustUserCourse(openId, origUserCoures.LessonCode, lessonCode, courseScheduleType,true);
                    }                
                }

                result.Entity = _UserSrv.GetUserCouresByCode(lessonCode);


            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }
            return new JsonResult(result);
        }

        public IActionResult OnPostQueryUserCourseLog(string openId,int pageIndex =1,int pageSize=20)
        {
            ResultList<RUserCourseList> result = new ResultList<RUserCourseList>();

            try
            {
                int totalPage;
                result.List = _UserSrv.QueryUserCourseLogList(openId,out totalPage,null, pageIndex,pageSize);
                result.TotlaPage = totalPage;

            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;

            }
            return new JsonResult(result);
        }

    }
}