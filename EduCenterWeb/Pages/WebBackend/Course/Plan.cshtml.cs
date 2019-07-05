using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Pages.WebBackEnd;
using EduCenterModel.Teacher;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Course
{
    public class PlanModel : EduBasePageModel
    {
        /// <summary>
        /// 课时9:00-10:30
        /// </summary>
        public List<ECourseTime> CourseTimes { get; set; }

       
        /// <summary>
        /// 课程列表 拖拽使用
        /// </summary>
        public List<ECourseInfo> CourseList { get; set; }
        
        public List<SiKsV> CourseScheduleType { get; set; } 

        private CourseSrv _CourseSrv;
        private TecSrv _TecSrv;

        public PlanModel(CourseSrv courseSrv, TecSrv tecSrv)
        {
            _CourseSrv = courseSrv;
            _TecSrv = tecSrv;
        }

        public void OnGet()
        {
            CourseTimes = StaticDataSrv.CourseTime.Values.ToList();
            
            CourseList = _CourseSrv.GetAllList();

            CourseScheduleType = BaseEnumSrv.CourseScheduleTypeList;
        }

        public IActionResult OnPostSave(List<ECourseSchedule> list)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                List<ECourseSchedule> newList = new List<ECourseSchedule>();
                foreach (ECourseSchedule es in list)
                {
                    es.LessonCode = $"{es.Year}_{es.Day}_{es.Lesson}_{es.CourseCode}_{es.LessonNo}_{es.CourseScheduleType}";
                    if(es.Id==0)
                    {
                        newList.Add(es);
                    }
                }

                if (newList.Count>0)
                {
                    _CourseSrv.AddRange(newList);
                  
                    _CourseSrv.SaveChanges();
                    
                }  
            }
            catch(Exception ex)
            {
                result.ErrorMsg = ex.Message;
              
            }
            return new JsonResult(result);
        }

        public IActionResult OnPostGet(int year,CourseScheduleType scheduleType)
        {
            ResultObject<PPlan> result = new ResultObject<PPlan>();
           
            try
            {
                ECourseDateRange dr = StaticDataSrv.CourseDateRange.Where(a => a.CourseScheduleType == scheduleType && a.Year == year).FirstOrDefault();
                result.Entity.PlanInfo = "";
                if (dr != null)
                    result.Entity.PlanInfo = $"{dr.Year} {dr.CourseDateRangeName}: {dr.StartDate.ToString("MM月dd日")} 到 {dr.EndDate.ToString("MM月dd日")}";
             
                result.Entity.CourseScheduleList = _CourseSrv.GetCourseScheduleByYearType(year,scheduleType);
          
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }

        public IActionResult OnPostDelete(long Id)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                var DelObj = _CourseSrv.GetCourseSchedule(Id);
                if(DelObj!=null)
                {
                    if (DelObj.ApplyNum > 0)
                    {
                        result.ErrorMsg = "已经有用户报名，不能删除此课程";
                    }
                    else
                    {
                        _CourseSrv.DeleteCourseSchdule(DelObj);
                        _CourseSrv.SaveChanges();

                    }
                        
                }
            }
            catch(Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }
            return new JsonResult(result);
        }

    }
}