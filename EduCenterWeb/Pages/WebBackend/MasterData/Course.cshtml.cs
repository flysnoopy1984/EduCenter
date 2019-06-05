using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Pages.WebBackEnd;
using EduCenterModel.Teacher.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.MasterData
{
    public class CourseModel : EduBasePageModel
    {
        private CourseSrv _CourseSrv;
        private TecSrv _TecSrv;

        public List<STec> TecList { get; set; }


        public CourseModel(CourseSrv courseSrv, TecSrv tecSrv)
        {
            _CourseSrv = courseSrv;
            _TecSrv = tecSrv;
    }

   //     public List<ECourseInfo> CourseList { get; set; }
        public List<SiKsV> CourseType { get; set; }

        public void OnGet()
        {
            //  CourseList = _CourseSrv.GetAllList();
            TecList = _TecSrv.GetSimpleList();
            CourseType = _CourseSrv.GetCourseType();


        }

        public IActionResult OnPostGet(CourseType courseType)
        {
            ResultObject<PMasterDataCourse> result = new ResultObject<PMasterDataCourse>();
          
            try
            {
                result.Entity.CourseList = _CourseSrv.GetAllByType(courseType);
                result.Entity.ClassList = _CourseSrv.GetCourseClassList();
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }

        public IActionResult OnPostSave(List<ECourseInfo> couseList,List<ECourseInfoClass> classList)
        {

            ResultList<SlKiV> result = new ResultList<SlKiV>();
            bool needSave = false;
            try
            {
                _CourseSrv.BeginTrans();
                result.List = new List<SlKiV>();
                if (couseList.Count > 0)
                {
                    _CourseSrv.DelByType(couseList[0].CourseType);
                    foreach (var obj in couseList)
                    {

                        _CourseSrv.Add(obj, false);

                        result.List.Add(new SlKiV
                        {
                            Key = obj.Id,
                            Value = obj.Level
                        });
                    }
                    needSave = true;
                }
                if(classList.Count>0)
                {
                    foreach(var cls in classList)
                    {
                        _CourseSrv.CreateOrUpdateClass(cls);
                    }
                }
                if(needSave)
                    _CourseSrv.SaveChanges();

                _CourseSrv.CommitTrans();
            }
            catch (Exception ex)
            {
                _CourseSrv.RollBackTrans();
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
