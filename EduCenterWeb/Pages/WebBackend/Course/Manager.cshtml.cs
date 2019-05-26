using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Course
{
    public class ManangerModel : EduBasePageModel
    {
        private CourseSrv _CourseSrv;
       
       
        public ManangerModel(CourseSrv courseSrv)
        {
            _CourseSrv = courseSrv;
        }

        public List<ECourseInfo> CourseList { get; set; }
        public List<SiKsV> CourseType { get; set; }

       public void OnGet()
       {
          //  CourseList = _CourseSrv.GetAllList();

            CourseType = _CourseSrv.GetCourseType();
        }

        public IActionResult OnPostGet(CourseType courseType)
        {
            ResultList<ECourseInfo> result = new ResultList<ECourseInfo>();
            try
            {
                result.List =  _CourseSrv.GetAllByType(courseType);
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }

        public  IActionResult OnPostSave(List<ECourseInfo> list)
        {

            ResultList<SlKiV> result = new ResultList<SlKiV>();
            try
            {

                result.List = new List<SlKiV>();
                if(list.Count>0)
                {
                    _CourseSrv.DelByType(list[0].CourseType);
                    foreach (var obj in list)
                    {

                        _CourseSrv.Add(obj, false);

                        result.List.Add(new SlKiV
                        {
                            Key = obj.Id,
                            Value = obj.Level
                        });
                    }
                    _CourseSrv.SaveChanges();
                }
              
            
              //  result.IntMsg = obj.Id;
            }
            catch(Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }
            
            return new JsonResult(result);
        }

        public IActionResult OnPostDelete(string delCode)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                _CourseSrv.Delete(delCode);
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }
    }
}