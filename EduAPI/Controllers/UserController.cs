﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.AppFramework;
using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
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

        [HttpPost]
        public ResultNormal UploadHeader(string userOpenId)
        {
            ResultNormal result = new ResultNormal();
            //  List<EArtDetail> detailList = new List<EArtDetail>(); 
            try
            {
                if (string.IsNullOrEmpty(userOpenId))
                {
                    result.ErrorMsg = "没有获取用户编号";
                    return result;
                }
                if (Request.Form != null && Request.Form.Files.Count > 0)
                {
                   
                    var file = Request.Form.Files[0];

                    var ext = Path.GetExtension(file.FileName).ToLower();
                    var fileName = $"{userOpenId}{ext}";

                    //  var fileName = $"art_{artId}_{no}_{DateTime.Now.ToString("hhmmss")}{ext}";

                 //   var date = DateTime.Now.ToString("yyyy_MM_dd");
                    DirectoryInfo di = new DirectoryInfo(XYAppConfig.UserHeaderImagePath);
                    if (!di.Exists)
                        di.Create();

                    var filepath = di.FullName + "\\" + fileName;
                    using (FileStream fs = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    string headerUrl = XYAppConfig.ResSite + fileName;
                    _UserSrv.updateUserHeader(userOpenId, headerUrl);
                  
                    //EArtDetail detail = new EArtDetail
                    //{
                    //    ArtId = artId,
                    //    FilePath = EduEnviroment.VirPath_ArtRoot + date + "/" + fileName,
                    //};
                    //_WxMiniSrv.AddArtDetail(detail);
                    //if (isLast)
                    //{
                    //    var art = _WxMiniSrv.GetArtInfo(artId);
                    //    art.RecordStatus = RecordStatus.Normal;
                    //    art.CoverFilePath = detail.FilePath;
                    //}
                    //_WxMiniSrv.SaveChanges();

                }
                else
                {
                    throw new EduException("没有上传的头像");
                }

            }
            catch (EduException eex)
            {
                result.ErrorMsg = eex.Message;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "上传头像失败";
                NLogHelper.ErrorTxt($"UploadUserHeader:{ex.Message}");
                NLogHelper.ErrorTxt($"UploadUserHeader:{ex.InnerException.Message}");
            }
            return result;
        }
    }
}