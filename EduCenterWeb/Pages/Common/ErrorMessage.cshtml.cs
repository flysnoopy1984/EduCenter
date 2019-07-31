using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.Common
{
    public class ErrorMessageModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public void OnGet()
        {
            ErrorMessage = Request.Query["msg"];
            if(string.IsNullOrEmpty(ErrorMessage))
            {
                ErrorMessage = "非法进入";
            }
        }
    }
}