using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public ManagerModel(TecSrv tecSrv,CourseSrv courseSrv)
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

        }
    }
}