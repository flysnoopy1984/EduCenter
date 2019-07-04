using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.Common;
using EduCenterModel.User.In;
using EduCenterModel.User.Result;
using EduCenterSrv;
using EduCenterSrv.Common;
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

        public List<SiKsV> MemberTypeList
        {
            get { return BaseEnumSrv.MemberTypeList; }
        }

        public IActionResult OnPostQueryUserList(string userName,int pageIndex,int pageSize)
        {
            ResultList<RUserList> result = new ResultList<RUserList>();
            try
            {
                int recordTotal;


                result.List = _UserSrv.QueryUserList(userName,out recordTotal, pageIndex,pageSize);
                result.RecordTotal = recordTotal;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }

        public IActionResult OnPostUpdateUser(InUserData userData)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                _UserSrv.UpdateUserData(userData);

            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }
    }
}