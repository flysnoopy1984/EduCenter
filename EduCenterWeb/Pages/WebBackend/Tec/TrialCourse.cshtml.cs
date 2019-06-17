using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Course.Result;
using EduCenterModel.Teacher;
using EduCenterModel.User.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Tec
{
    public class TrialCourseModel : EduBasePageModel
    {
        public List<ETecInfo> TecList { get; set; }
        public List<ECourseInfo> CourseList { get; set; }

        private TecSrv _TecSrv;
        private CourseSrv _CourseSrv;


        public TrialCourseModel(TecSrv tecSrv, CourseSrv courseSrv)
        {
            _TecSrv = tecSrv;
            _CourseSrv = courseSrv;
        }
        public void OnGet()
        {
            TecList = _TecSrv.GetAllStaffTec();
        }

        public IActionResult OnPostQueryTrialLog(string fromDate,string toDate,string tecCode,int pageIndex,int pageSize)
        {
            ResultList<RTrialLog> result = new ResultList<RTrialLog>();
            try
            {
                int recordTotal;
                result.List = _CourseSrv.QueryTrialLogList_BackEnd(fromDate, toDate,out recordTotal,tecCode,pageIndex,pageSize);
                result.RecordTotal = recordTotal;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }

        public IActionResult OnPostConfirmTrialStatus(long Id)
        {
            ResultNormal result = new ResultNormal();
            try
            {

                 _CourseSrv.UpdateTrialStatus(Id, TrialLogStatus.TecConfirm);
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }
    }
}