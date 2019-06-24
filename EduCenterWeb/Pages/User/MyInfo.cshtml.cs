using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.Session;
using EduCenterModel.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class MyInfoModel : EduBaseAppPageModel
    {
        public UserSession UserSession { get; set; }
        public void OnGet()
        {
            UserSession = GetUserSession();
          
               
        }
    }
}