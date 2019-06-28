using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Pages.User;
using EduCenterModel.User;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class ApplyModel : EduBaseAppPageModel
    {
        private CourseSrv _CourseSrv;
        //private UserSrv _UserSrv;
        private BusinessSrv _BusinessSrv;
        public List<ECourseTime> CourseTimes { get; set; }

       
        public List<ECourseSchedule> CourseScheduleList;
        public ApplyModel(CourseSrv courseSrv,BusinessSrv businessSrv)
        {
            _CourseSrv = courseSrv;
     //       _UserSrv = userSrv;
            _BusinessSrv = businessSrv;
        }

        public List<ECourseSchedule> GetAvaliableCourseList(int day,int lesson)
        {
            try
            {
                if(CourseScheduleList!=null)
                {
                    return CourseScheduleList.Where(a => a.Day == day && a.Lesson == lesson).ToList();
                }
               
            }
            catch
            {

            }
            return new List<ECourseSchedule>();
        }
        public void OnGet()
        {
            var us = base.GetUserSession();
            if(us!=null)
            {
                if(us.CurrentScheduleType == CourseScheduleType.Summer || us.CurrentScheduleType == CourseScheduleType.Winter)
                {
                    HttpContext.Response.Redirect("/User/ApplyWinterSummer");
                    return;
                }
            
                CourseTimes = StaticDataSrv.CourseTime.Values.ToList();

                //获取所有课程信息，并整理Day,Lesson Hashtable
                CourseScheduleList = _CourseSrv.GetCourseScheduleByYearType(DateTime.Now.Year, CourseScheduleType.Standard);
            }
     
        }

        public IActionResult OnPostInitData()
        {
            ResultObject<PUserApply> result = new ResultObject<PUserApply>();
           // ResultObject<> result = new ResultObject<Dictionary<int, ECourseTime>>();
            try
            {
              //  result.Entity.CourseScheduleList = _CourseSrv.GetCourseScheduleByYearType(DateTime.Now.Year, CourseScheduleType.Standard);
                result.Entity.CourseTimeList = StaticDataSrv.CourseTime;
                result.Entity.CourseMaxApplyNum = StaticDataSrv.CourseMaxApplyNum;

            }
            catch (Exception ex)
            {
                result.ErrorMsg = "未能获取数据！请联系管理员或稍后再试";
                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }

      
        public IActionResult OnPostSubmit(List<string> lessonCodeList,int courseScheduleType)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                var us = base.GetUserSession(false);
                if(us!=null)
                {
                    List<EUserCourse> ucList = new List<EUserCourse>();
                    foreach(var lc in lessonCodeList)
                    {
                        EUserCourse uc = new EUserCourse
                        {
                            CourseScheduleType = (CourseScheduleType)courseScheduleType,
                            CreateDateTime = DateTime.Now,
                            LessonCode = lc,
                        
                            UserOpenId = us.OpenId
                        };
                        ucList.Add(uc);
                    }
                    _BusinessSrv.UserSelectNewCourses(us.OpenId, ucList);
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "请重新登陆";
                }
            }
            catch(EduException ex)
            {
                result.ErrorMsg = ex.Message;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "提交数据错误！请联系管理员或稍后再试";
                NLogHelper.ErrorTxt($"标准班课程选择[OnPostSubmit]:{ex.Message}");
            }
            return new JsonResult(result);
        }

    }
}