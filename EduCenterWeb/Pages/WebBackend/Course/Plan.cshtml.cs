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
                foreach (ECourseSchedule es in list)
                {
                    es.LessonCode = $"{es.Year}_{es.Day}_{es.Lesson}_{es.CourseCode}_{es.LessonNo}_{es.CourseScheduleType}";
                }

                if (list!=null &&list.Count>0)
                {
                    _CourseSrv.BeginTrans();
                    _CourseSrv.DeleteCourseSchduleByYear(list[0].Year);
                    _CourseSrv.SaveChanges();
                    _CourseSrv.AddRange(list);
                   
                    _CourseSrv.SaveChanges();
                    _CourseSrv.CommitTrans();
                }  
            }
            catch(Exception ex)
            {
                result.ErrorMsg = ex.Message;
                _CourseSrv.RollBackTrans();
            }
            return new JsonResult(result);
        }

        public IActionResult OnPostGet(int year,CourseScheduleType scheduleType)
        {
            ResultList<ECourseSchedule> result = new ResultList<ECourseSchedule>();
        
            try
            {
               
                result.List = _CourseSrv.GetCourseScheduleByYearType(year,scheduleType);
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }
    }
}