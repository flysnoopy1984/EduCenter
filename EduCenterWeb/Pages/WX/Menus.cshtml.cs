using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterCore.WX;
using EduCenterModel.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WX
{
    public class MenusModel : PageModel
    {
        public void OnGet()
        {

        }

        public IActionResult OnPostCreateMenu()
        {
            ResultNormal result = new ResultNormal();
            try
            {
                string access_token = WXApi.getAccessToken().access_token;
                string posturl = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + access_token;
                string menuStr = "";// " 菜单结构";
                using (StreamReader sr = new StreamReader(EduEnviroment.Menus_JsonFilePath))
                {
                    menuStr = sr.ReadToEnd();
                }
                result.SuccessMsg = HttpHelper.RequestUrlSendMsg(posturl, HttpHelper.HttpMethod.Post, menuStr, "application/json");
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
              //  throw ex;
            
            }
            return new JsonResult(result);
        }

    }
}