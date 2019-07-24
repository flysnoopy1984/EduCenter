using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.BaseEnum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WX
{
    public class PayQRMoneyModel : EduBaseAppPageModel
    {
       
        public void OnGet()
        {
            var us = GetUserSession();

            if (us == null)
            {
                string amt = Request.Query["amt"];
                string ct = Request.Query["ct"]; //课时
                HttpContext.Response.Redirect($"/User/Login?handler=LoginTransfer2&toPage=/WX/PayQRMoney&amt={amt}&ct={ct}");
            }

        }

       
    }
}