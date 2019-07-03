using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class PayCourseSuccessModel : EduBaseAppPageModel
    {
        public void OnGet()
        {
            var us = GetUserSession();

        }


    }
}