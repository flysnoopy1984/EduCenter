using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.Test
{
    public class AllPagesModel : EduBaseAppPageModel
    {
        public void OnGet()
        {
            base.SetUserSesion("o3nwE0qI_cOkirmh_qbGGG-5G6B0","Jacky");
        }
    }
}