using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterCore.WX;
using EduCenterModel.ArtShow;
using EduCenterModel.ArtShow.In;
using EduCenterModel.ArtShow.Out;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.News.Out;
using EduCenterModel.User;
using EduCenterModel.WX;
using EduCenterModel.WX.Media;
using EduCenterModel.WX.Mini;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EduAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WXMiniController : BaseAPI
    {
        
        private UserSrv _UserSrv;
        private WxMiniSrv _WxMiniSrv;
        public WXMiniController(UserSrv userSrv, WxMiniSrv wxMiniSrv)
        {
            _UserSrv = userSrv;
            _WxMiniSrv = wxMiniSrv;
          
        }
        [HttpGet]
        public ResultList<SiKsV> GetCourseType()
        {
         
            ResultList<SiKsV> result = new ResultList<SiKsV>();
            try
            {
                result.List = BaseEnumSrv.CourseTypeList;
            }
            catch(Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }
            return result;
        }

        #region  Art
        private void VerifyData(ArtMediaType artMediaType)
        {
            string[] limitTypes = { ".jpg", ".bmp", ".png", ".jpeg", ".gif" };
            if (artMediaType == ArtMediaType.Pic)
            {
               
                foreach (var file in Request.Form.Files)
                {
                    if (file.Length > 1024 * 1024 * 5)
                    {
                        throw new EduException("文件不能大于5M");
                    }

                    var ext = Path.GetExtension(file.FileName).ToLower();
                    if (!limitTypes.Contains(ext))
                    {
                        throw new EduException($"不支持的文件扩展名{ext}");
                    }
                }
            }
            else
            {
                limitTypes = new string[]{".mp4",".mov",".avi",".3gp"};
                foreach (var file in Request.Form.Files)
                {
                  
                    var ext = Path.GetExtension(file.FileName).ToLower();
                    if (!limitTypes.Contains(ext))
                    {
                        throw new EduException($"不支持的文件扩展名{ext}");
                    }
                }
            }
          
        }

        /// <summary>
        /// 创建作品
        /// </summary>
        /// <param name="inParameter"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultNormal CreateArt(InArtInfo inParameter)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                if (string.IsNullOrEmpty(inParameter.unionId))
                {
                    result.ErrorMsg = "用户信息没有获取";
                    return result;
                }

                EUserInfo ui = _UserSrv.GetUserInfoByUninonId(inParameter.unionId);
                if(ui == null)
                {
                    result.ErrorMsg = "unionId无效，请尝试登陆课程后，返回后再进入小程序";
                    return result;
                }

                EArtInfo art = new EArtInfo()
                {
                    UnionId = inParameter.unionId,
                    UploadUser = ui.Name,
                    UploadDateTime = DateTime.Now,
                    CourseType = inParameter.courseType,
                    Title = inParameter.Title,
                    Desc = inParameter.Desc,
                    ArtMediaType = inParameter.ArtMediaType,
                    RecordStatus = RecordStatus.PreData,

                };
                _WxMiniSrv.AddArtInfo(art);
                _WxMiniSrv.SaveChanges();
                result.IntMsg = art.Id;
            }
            catch(Exception ex)
            {
                result.ErrorMsg = "创建作品失败!";
                NLogHelper.ErrorTxt("WxAPI - [CreateArt]:"+ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 上传图片，更新作品信息
        /// </summary>
        /// <param name="artId"></param>
        /// <param name="no"></param>
        /// <param name="isLast"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultNormal UploadArt(long artId, ArtMediaType ArtMediaType,int no,bool isLast=false)
        {
            ResultNormal result = new ResultNormal();
          //  List<EArtDetail> detailList = new List<EArtDetail>(); 
            try
            {
                if(artId <= 0)
                {
                    throw new EduException("活动编号没有获取！");
                }
                if(Request.Form !=null && Request.Form.Files.Count>0)
                {
                    VerifyData(ArtMediaType);

                    var file = Request.Form.Files[0];

                    var ext = Path.GetExtension(file.FileName).ToLower();
                    var fileName = $"art_{artId}_{no}_{DateTime.Now.ToString("hhmmss")}{ext}";

                    var date = DateTime.Now.ToString("yyyy_MM_dd");
                    DirectoryInfo di = new DirectoryInfo(EduConfig.UploadArtRoot + date);
                    if (!di.Exists)
                        di.Create();

                    var filepath = di.FullName + "\\" + fileName;
                    using (FileStream fs = new FileStream(filepath, FileMode.Create))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    EArtDetail detail = new EArtDetail
                    {
                        ArtId = artId,
                        FilePath = EduEnviroment.VirPath_ArtRoot + date + "/" + fileName,
                    };
                    _WxMiniSrv.AddArtDetail(detail);
                    if(isLast)
                    {
                        var art = _WxMiniSrv.GetArtInfo(artId);
                        art.RecordStatus = RecordStatus.Normal;
                        art.CoverFilePath = detail.FilePath;
                    }
                    _WxMiniSrv.SaveChanges();

                }
                else
                {
                    throw new EduException("没有上传的作品");
                }
              
            }
            catch(EduException eex)
            {
                result.ErrorMsg = eex.Message;
            }
            catch(Exception ex)
            {
                result.ErrorMsg = "上传失败";
                NLogHelper.ErrorTxt($"UploadArt:{ex.Message}");
                NLogHelper.ErrorTxt($"UploadArt:{ex.InnerException.Message}");
            }
            return result;

        }

       
        [HttpPost]
        public ResultNormal DelArt(long artId)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                _WxMiniSrv.DelArtInfo(artId);
            }
            catch(Exception ex)
            {
                NLogHelper.ErrorTxt($"DelArt:{ex.Message}");
               
                result.ErrorMsg = "删除作品失败,请联系客服！";
            }
            return result;
        }

        [HttpPost]
        public ResultList<RArtInfo> QueryArtInfo(InArtList inArtList)
        {
            ResultList<RArtInfo> result = new ResultList<RArtInfo>();
            try
            {
             //   NLogHelper.InfoTxt($"[调用QueryArtInfo]ownUnionId: {inArtList.ownUnionId}");
                int totalPage;
                result.List = _WxMiniSrv.QueryArtInfoList(inArtList.unionIdQuerier, out totalPage, 
                                                          inArtList.ownUnionId,
                                                          inArtList.orderby,
                                                          inArtList.showAll,
                                                          inArtList.pageIndex,
                                                          inArtList.pageSize);
                result.TotlaPage = totalPage;
            }
            catch(Exception ex)
            {
                result.ErrorMsg = "获取列表失败";
            } 
            return result;
        }

        [HttpPost]
        public ResultObject<RArtInfo> GetRArtInfo(long artId)
        {
            ResultObject<RArtInfo> result = new ResultObject<RArtInfo>();
            try
            {
                result.Entity = _WxMiniSrv.GetRArtInfo(artId);
            }
            catch (Exception ex)
            {
               
            }
            return result;
        }

        [HttpPost]
        public ResultList<RArtDetail> QueryArtDetail(long artId)
        {
        //    NLogHelper.InfoTxt($"Request ArtId{artId}");
            ResultList<RArtDetail> result = new ResultList<RArtDetail>();
            try
            {
                result.List = _WxMiniSrv.QueryArtDetailList(artId);
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "获取详情失败";
            }
            return result;
        }

        public ResultNormal DelComment(long comId,long artId)
        {
            ResultNormal result = new ResultNormal();
            try
            {
               int delnum =  _WxMiniSrv.DelComment(comId);
                if(delnum>0)
                {
                    var artInfo = _WxMiniSrv.GetArtInfo(artId);
                    artInfo.Comments--;
                    if (artInfo.Comments < 0)
                        artInfo.Comments = 0;
                    _WxMiniSrv.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "删除评论失败";
            }
            return result;
        }

        public ResultNormal SendComment(InArtComment indata)
        {
            NLogHelper.InfoTxt($"[SendComment]unionId: {indata.unionId}");
            ResultNormal result = new ResultNormal();
            try
            {
                var checkRes = WXApi.CheckContentSec(indata.Content);
                if(checkRes.errcode ==0)
                {
                    EArtComment artComment = new EArtComment
                    {
                        ArtId = indata.ArtId,
                        Content = indata.Content,
                        Praize = 0,
                        UnionId = indata.unionId,
                        CreateDateTime = DateTime.Now,
                        RefId = 0,
                    };
                    _WxMiniSrv.AddComment(artComment);
                    var artInfo = _WxMiniSrv.GetArtInfo(indata.ArtId);
                    artInfo.Comments++;

                    _WxMiniSrv.SaveChanges();
                }
                else
                {
                    if(checkRes.errcode == 87014)
                    {
                        result.ErrorMsg = "请注意您的言论，请勿发布包含色情、违法等有害信息";
                    }
                    else
                        result.ErrorMsg = checkRes.errMsg;
                }
                
            }
            catch(Exception ex)
            {
                result.ErrorMsg = "发布评论失败";
            }
            return result;
        }

        public ResultList<RArtComment> QueryComment(long artId,string unionIdQuerier,int pageIndex=1,int pageSize=20)
        {
            ResultList<RArtComment> result = new ResultList<RArtComment>();
            try
            {
                int totalPage;
                result.List = _WxMiniSrv.QueryArtComment(artId, unionIdQuerier,out totalPage, pageIndex, pageSize);
                result.TotlaPage = totalPage;
            }
            catch(Exception ex)
            {
                result.ErrorMsg = "获取评论列表失败！";
            }
            return result;
        }

        public ResultNormal SwitchPraize(InUserPraize inUserPraize)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                if(inUserPraize.PraizeTarget == PraizeTarget.ArtInfo)
                    _WxMiniSrv.UpdateArtPraize(inUserPraize.KeyId, inUserPraize.UnionId, inUserPraize.IsPraize);
                else if (inUserPraize.PraizeTarget == PraizeTarget.Comments)
                    _WxMiniSrv.UpdateCommentPraize(inUserPraize.KeyId, inUserPraize.UnionId, inUserPraize.IsPraize);

                _WxMiniSrv.SaveChanges();
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "点赞失败了！";
            }
            return result;
        }



        #endregion

        #region 素材
        [HttpPost] 
        public ResultList<RNewsInfo> QueryNewsList(int pageIndex=1,int pageSize=20)
        {
            ResultList<RNewsInfo> result = new ResultList<RNewsInfo>();
            try
            {
                int totalPage;
                result.List = _WxMiniSrv.QueryNewsList(out totalPage, pageIndex, pageSize);
                result.TotlaPage = totalPage;
            }
            catch(Exception ex)
            {
                result.ErrorMsg = "获取列表失败";
            }
            return result;
        }
        #endregion

        #region 微信公众号
        [HttpPost]
        public MiniCode2Session AuthUserOpenId()
        {
            string code = Request.Query["code"];
            MiniCode2Session result = null;
            if (!string.IsNullOrEmpty(code))
            {
                result =  WXApi.GetOpenIdForWxMini(code);
                result.HasExistInWX = _UserSrv.ExistUnionId(result.unionid);
             
                return result;
            }
            return  new MiniCode2Session();

        }

        
        [HttpPost]
        public ResultObject<JOMedia> GetNewsMeterialFromWX(int pageIndex = 0, int pageSize = 20)
        {
            ResultObject<JOMedia> result = new ResultObject<JOMedia>();
            try
            {
                string access_token = WXApi.getAccessToken().access_token;
                string wxUrl = $"https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={access_token}";
                MaterialList_In paremeter = new MaterialList_In
                {
                    type = "news",
                    offset = pageIndex*pageSize,
                    count = pageSize,
                };
                var json = JsonConvert.SerializeObject(paremeter);
                string data = HttpHelper.RequestUrlSendMsg(wxUrl, HttpHelper.HttpMethod.Post, json);


                result.Entity = JsonConvert.DeserializeObject<JOMedia>(data);

            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }
            return result;
        }
        #endregion

        #region 微信小程序
        public ResultObject<AccessToken> getMiniAccessToken()
        {
            ResultObject<AccessToken> result = new ResultObject<AccessToken>();
            try
            {
                result.Entity = WXApi.getAccessToken(EduConfig.WXAppId, EduConfig.WXSecret);
                result.Entity.expire_DateTime = DateTime.Now.AddSeconds(7200);
            }
            catch(Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }
            return result;
        }

     
        #endregion





    }


}