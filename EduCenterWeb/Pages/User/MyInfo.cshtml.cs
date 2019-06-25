using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.Common;
using EduCenterModel.Session;
using EduCenterModel.User;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class MyInfoModel : EduBaseAppPageModel
    {

        public UserSession UserSession { get; set; }
        private UserSrv _UserSrv;
        public List<EUserChild> ChildList { get; set; }

        public MyInfoModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }

        public void OnGet()
        {
            UserSession = GetUserSession();           
        }

        public IActionResult OnPostSave(List<EUserChild> list)
        {
            ResultNormal result = new ResultNormal();
         
            try
            {
                var us = GetUserSession(false);
                if(us != null)
                {
                    foreach(var c in list)
                    {
                        c.UserOpenId = us.OpenId;
                    }
                    _UserSrv.SaveChild(list);
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "超时，请重新登陆！";
                }
               

            }
            catch (Exception ex)
            {
                result.ErrorMsg = "保存数据错误！请联系管理员或稍后再试";
                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }

        public IActionResult OnPostInitChildList()
        {
            ResultList<EUserChild> result = new ResultList<EUserChild>();
            var us = GetUserSession();
            try
            {
                result.List = _UserSrv.GetAllChild(us.OpenId);
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "查询数据异常！请联系管理员或稍后再试";
                NLogHelper.ErrorTxt(ex.Message);
            }
            return new JsonResult(result);
        }
       

    }
}