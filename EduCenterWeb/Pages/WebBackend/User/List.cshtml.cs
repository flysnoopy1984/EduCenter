using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.Common;
using EduCenterModel.User.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.User
{
    public class ListModel : EduBaseAppPageModel
    {
        private UserSrv _UserSrv;
        public ListModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPostQueryUserList(string userName,int pageIndex,int pageSize)
        {
            ResultList<RUserList> result = new ResultList<RUserList>();
            try
            {
                int totalPages;
             
                result.List = _UserSrv.QueryUserList(userName,out totalPages, pageIndex,pageSize);
                result.TotlaPage = totalPages;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }
    }
}