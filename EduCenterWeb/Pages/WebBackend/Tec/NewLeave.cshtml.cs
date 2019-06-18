using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.Common;
using EduCenterModel.Teacher;
using EduCenterModel.Teacher.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Tec
{
    public class NewLeaveModel : PageModel
    {
        public List<STec> TecList { get; set; }
        private TecSrv _TecSrv;
        public NewLeaveModel(TecSrv tecSrv)
        {
            _TecSrv = tecSrv;
        }
        public void OnGet()
        {
            TecList = _TecSrv.GetSimpleList();
        }

        public IActionResult OnPostQueryLessonList(string tecCode, string date)
        {
            ResultList<RTecLesson> result = new ResultList<RTecLesson>();
            try
            {
           
                result.List = _TecSrv.GetTecOneDayAllLesson(tecCode,date);
                
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }

        public IActionResult OnPostSubmitTecLeave(List<long> list,ETecLeave tecLeave)
        {
            ResultNormal result = new ResultNormal();
            try
            {

                _TecSrv.SubmitLeave(list,tecLeave);

            }
            catch (Exception ex)
            {
                result.ErrorMsg = "请假失败，请联系管理员";
                NLogHelper.ErrorTxt($"后台新增请假[OnPostSubmitTecLeave]:{ex.Message}");
            }

            return new JsonResult(result);
        }
    }
}