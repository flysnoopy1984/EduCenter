using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.Common;
using EduCenterModel.Pages.WebBackEnd;
using EduCenterModel.Teacher;
using EduCenterModel.Teacher.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Tec
{
    public class ManagerModel : EduBasePageModel
    {
        private TecSrv _TecSrv;
        private CourseSrv _CourseSrv;
        public PTecManagerData PData;

        public ManagerModel(TecSrv tecSrv, CourseSrv courseSrv)
        {
            _TecSrv = tecSrv;
            _CourseSrv = courseSrv;

        }
   
        public void OnGet()
        {
            PData = new PTecManagerData();
            PData.TecList = _TecSrv.GetSimpleList();
            PData.SkillLevelList = _TecSrv.GetSkillLevelList();
            PData.CourseList = _CourseSrv.GetSimpleList();
           // PData.TecSkill = _TecSrv.GetTecSkillList()
        }

        public IActionResult OnPostGet(string code)
        {
            ResultObject<RTecAllInfo> result = new ResultObject<RTecAllInfo>();
            try
            {
                result.Entity = _TecSrv.GetAllInfo(code);
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }

        public IActionResult OnPostSave(RTecAllInfo obj)
        {
            ResultNormal result = new ResultNormal();
            try
            {
              
                _TecSrv.UpdatePartTecInfo(obj.TecInfo, false);
                if(obj.TecSkillList!=null)
                {
                    foreach (var sk in obj.TecSkillList)
                    {
                        _TecSrv.UpdateTecSkillLevel(sk, false);
                    }
                }
               
                _TecSrv.SaveChanges();

            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }

        public IActionResult OnPostSaveSkillLevel(ETecSkill sk)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                _TecSrv.UpdateTecSkillLevel(sk);    
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }
    }
}