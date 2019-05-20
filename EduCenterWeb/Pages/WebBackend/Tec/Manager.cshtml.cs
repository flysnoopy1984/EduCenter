using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


        public ManagerModel(TecSrv tecSrv)
        {
            _TecSrv = tecSrv;
        }

        public List<STec> TecList;

        public void OnGet()
        {
            TecList = _TecSrv.GetSimpleList();
        }
    }
}