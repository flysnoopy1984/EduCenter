using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Teacher;
using EduCenterModel.Teacher.Result;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Tec
{
    public class CoursingStandModel : EduBasePageModel
    {
        private TecSrv _TecSrv;
        private UserSrv _UserSrv;

        public List<ETecInfo> TecList { get; set; }
        public List<ECourseTime> CourseTimes { get; set; }

        public CoursingStandModel(TecSrv tecSrc, UserSrv userSrv)
        {
            _TecSrv = tecSrc;
            _UserSrv = userSrv;
        }
        public void OnGet()
        {
            TecList = _TecSrv.GetAllStaffTec();

            CourseTimes = StaticDataSrv.CourseTime.Values.ToList();
        }

   
        public IActionResult OnPostQueryTecCourse(string tecCode,int year,int month)
        {
            ResultObject<Dictionary<string, List<RTecCourse>>> result = new ResultObject<Dictionary<string, List<RTecCourse>>>();
            try
            {
                var list =  _TecSrv.GetTecCourse(tecCode, CourseScheduleType.Standard,year,month);
                foreach(var c in list)
                {
                    try
                    {
                        if (result.Entity[c.CourseDateTimeStr] == null)
                        {
                            result.Entity[c.CourseDateTimeStr] = new List<RTecCourse>();
                            result.Entity[c.CourseDateTimeStr].Add(c);
                        }
                        else
                            result.Entity[c.CourseDateTimeStr].Add(c);
                    }
                    catch
                    {
                        result.Entity.Add(c.CourseDateTimeStr, new List<RTecCourse>());
                        result.Entity[c.CourseDateTimeStr].Add(c);
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