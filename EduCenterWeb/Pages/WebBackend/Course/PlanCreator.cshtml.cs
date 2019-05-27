using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Pages.WebBackEnd;
using EduCenterModel.Teacher;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Course
{
    public class PlanCreatorModel : EduBasePageModel
    {
        public List<ECourseTime> CourseTimes { get; set; }

        public List<ECourseInfo> CourseList { get; set; }

        public List<ETecSkill> TecSkillList { get; set; }

        private CourseSrv _CourseSrv;
        private TecSrv _TecSrv;

        public PlanCreatorModel(CourseSrv courseSrv, TecSrv tecSrv)
        {
            _CourseSrv = courseSrv;
            _TecSrv = tecSrv;
        }

        public void OnGet()
        {
            CourseTimes = StaticDataSrv.CourseTime.OrderBy(a => a.Lesson).ToList();
            
            CourseList = _CourseSrv.GetAllList();

            
            
        }

        public IActionResult OnPostSave(List<ECourseSchedule> list)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                if(list!=null &&list.Count>0)
                {
                    _CourseSrv.BeginTrans();
                    _CourseSrv.DeleteCourseSchduleByYear(list[0].Year);
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

        public IActionResult OnPostGet(int year,bool needSkill = true)
        {
            ResultObject<PPlanCreatorData> result = new ResultObject<PPlanCreatorData>();
            try
            {
                if (needSkill)
                    result.Entity.TecSkillList = _TecSrv.GetTecAvaliableSkill();
                result.Entity.ScheduleList = _CourseSrv.GetCourseScheduleByYear(year);
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }
    }
}