using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.BaseEnum.API;
using EduCenterModel.Common;
using EduCenterModel.User;
using EduCenterModel.User.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseAPI
    {
        private UserSrv _UserSrv;
        public UserController(UserSrv userSrv)
        {
            _UserSrv = userSrv;
        }

        [HttpPost]
        public EUserInfo Test(string openId)
        {
            return _UserSrv.GetUserInfo(openId);
        }

        [HttpPost]
        public ResultObject<RUserLogin> Login(EUserLogin eUserLogin)
        {
            ResultObject<RUserLogin> result = new ResultObject<RUserLogin>();
            try
            {
                if (string.IsNullOrEmpty(eUserLogin.LoginKey))
                {
                    result.IntMsg = (int)ApiErrorCode.UserLogin_NoPhone;
                    result.ErrorMsg = "没有登陆信息";
                    return result;
                }
                if (string.IsNullOrEmpty(eUserLogin.Pwd))
                {
                    result.IntMsg = (int)ApiErrorCode.UserLogin_NoPwd;
                    result.ErrorMsg = "密码不能为空";
                    return result;
                }
                var loginUser = _UserSrv.ApiUserLoginByPhone(eUserLogin);
                if(loginUser == null)
                {
                    result.IntMsg = (int)ApiErrorCode.UserLogin_NoUser;
                    result.ErrorMsg = "登陆信息或密码错误，没有找到用户";
                    return result;
                }

                result.Entity = loginUser;
            }
            catch(EduException eex)
            {
                result.ErrorMsg = eex.Message;
            }
            return result;
        }


    }
}