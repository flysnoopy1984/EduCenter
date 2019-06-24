using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class LoginModel : EduBaseAppPageModel
    {
        public void OnGet()
        {

        }

        public IActionResult OnPostUserLogin()
        {
            ResultNormal result = new ResultNormal();
            try
            {
                base.SetUserSesion("o3nwE0qI_cOkirmh_qbGGG-5G6B0","Jacky", "http://thirdwx.qlogo.cn/mmopen/hzVGicX27IG18yibKNnHfBojH4SpCPGNEvyOUZE8jxOw2ZnYcHzAkm72jugRaRc53jn1zZER32wE4SUib1aX3W7qwTZgolC4HWk/132","");
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "登陆失败";

                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }
    }
}