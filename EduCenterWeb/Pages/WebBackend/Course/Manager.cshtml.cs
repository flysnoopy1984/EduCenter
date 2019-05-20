using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Course
{
    public class ManangerModel : PageModel
    {
        private CourseSrv _CourseSrv;
        public ManangerModel(CourseSrv courseSrv)
        {
            _CourseSrv = courseSrv;
        }
        public List<ECourseInfo> CourseList;
        public void OnGet()
        {
            CourseList = _CourseSrv.GetAllList();
        }

        public IActionResult OnPostGet(string code)
        {
            ResultObject<ECourseInfo> result = new ResultObject<ECourseInfo>();
            try
            {
                result.Entity =  _CourseSrv.Get(code);
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }

        public  IActionResult OnPostSave(ECourseInfo obj)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                if (obj.Id > 0)
                    _CourseSrv.Update(obj);
                else
                    _CourseSrv.Add(obj);
                result.IntMsg = obj.Id;
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