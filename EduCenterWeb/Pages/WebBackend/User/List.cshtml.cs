﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.User;
using EduCenterModel.User.In;
using EduCenterModel.User.Result;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.User
{
    public class ListModel : EduBasePageModel
    {
        private UserSrv _UserSrv;

        public List<EUserInfo> SalesUserList { get; set; }


        public ListModel(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }
        public void OnGet()
        {
            SalesUserList = _UserSrv.GetSalesUserList();
        }

        private List<SiKsV> _MemberTypeList;
        public List<SiKsV> MemberTypeList
        {
            get {
                if(_MemberTypeList == null)
                    _MemberTypeList = BaseEnumSrv.MemberTypeList;
                return _MemberTypeList;
            }
        }

        private List<SiKsV> _UserRoleList;
         public List<SiKsV> UserRoleList
        {
            get {
                if(_UserRoleList == null)
                    _UserRoleList = BaseEnumSrv.UserRoleList.Where(a => a.Key <= 20).ToList();
                return _UserRoleList;
            }
        }

        public IActionResult OnPostQueryUserList(string userName,
            string babyName,
            int userRole,
            int memberType,
            string userOpenId,
            int pageIndex,
            int pageSize)
        {
            ResultList<RUserList> result = new ResultList<RUserList>();
            try
            {
                int recordTotal;

                result.List = _UserSrv.QueryUserList_FromStore(userName,babyName,userRole, memberType, userOpenId,
                    out recordTotal, pageIndex,pageSize);

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
                if(userData.MemberType == MemberType.VIP && userData.VipPrice <=0)
                {
                    result.ErrorMsg = "VIP 价格没有设置";
                    return new JsonResult(result);
                }
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