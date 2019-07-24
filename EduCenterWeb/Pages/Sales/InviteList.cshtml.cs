using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.Common;
using EduCenterModel.Sales.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.Sales
{
    public class InviteListModel : EduBaseAppPageModel
    {
        private SalesSrv _SalesSrv;

        public InviteListModel(SalesSrv salesSrv)
        {
            _SalesSrv = salesSrv;
        }
        public void OnGet()
        {

        }

        public IActionResult OnPostQueryList(int pageIndex, int pageSize)
        {
            ResultList<RInviteLog> result = new ResultList<RInviteLog>();
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                  
                    int totalPages;
                    result.List = _SalesSrv.QueryInviteLog(us.OpenId, out totalPages, 1, 5);
                    result.TotlaPage = totalPages;
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "需要您重新登录！";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "获取数据失败！";
                NLogHelper.ErrorTxt($"邀请列表[OnPostQueryList]:{ex.Message}");
            }
            return new JsonResult(result);
        }
    }
}