using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.Common;
using EduCenterModel.Teacher;
using EduCenterModel.Teacher.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Tec
{
    public class LeaveModel : EduBasePageModel
    {
        public List<STec> TecList { get; set; }
        private TecSrv _TecSrv;

        public LeaveModel(TecSrv tecSrv)
        {
            _TecSrv = tecSrv;
        }
        public void OnGet()
        {
            TecList = _TecSrv.GetSimpleList();
        }

        public IActionResult OnPostQueryTecLeave(string date, string tecCode, int pageIndex, int pageSize)
        {
            ResultList<Dictionary<string, List<RTecCourse>>> result = new ResultList<Dictionary<string, List<RTecCourse>>>();
            try
            {
                int recordTotal;
                var r = _TecSrv.QueryTecLeave(date, out recordTotal, tecCode, pageIndex, pageSize);
                
                foreach(var item in r)
                {
                    
                }
                result.RecordTotal = recordTotal;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }
    }
}