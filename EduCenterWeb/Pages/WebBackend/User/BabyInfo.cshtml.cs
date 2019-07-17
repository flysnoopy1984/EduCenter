using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.Common;
using EduCenterModel.User;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.User
{
    public class BabyInfoModel : EduBasePageModel
    {
        private UserSrv _UserSrv;
        public BabyInfoModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPostInitChildList(string openId)
        {
            ResultList<EUserChild> result = new ResultList<EUserChild>();
         
            try
            {
                result.List = _UserSrv.GetAllChild(openId);
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }

        public IActionResult OnPostSave(List<EUserChild> list)
        {
            ResultNormal result = new ResultNormal();

            try
            {
                _UserSrv.SaveChildList(list);

            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }
    }
}