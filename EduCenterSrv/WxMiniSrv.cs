using EduCenterModel.ArtShow;
using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduCenterModel.ArtShow.Out;
using EduCenterModel.BaseEnum;
using EduCenterModel.ArtShow.In;
using EduCenterSrv.Common;
using Microsoft.EntityFrameworkCore;
using EduCenterModel.User;
using EduCenterModel.User.Simple;
using EduCenterModel.News.Out;

namespace EduCenterSrv
{
    public class WxMiniSrv : BaseSrv
    {
        public WxMiniSrv(EduDbContext dbContext) : base(dbContext)
        {

        }

        public EArtInfo GetArtInfo(long artId)
        {
            return _dbContext.DbArtInfo.Where(a => a.Id == artId).FirstOrDefault();

        }

        public void AddArtInfo(EArtInfo art)
        {
            _dbContext.DbArtInfo.Add(art);
        }

        public void AddArtDetail(EArtDetail detail)
        {
            _dbContext.DbArtDetail.Add(detail);
        }
        public List<RArtDetail> QueryArtDetailList(long artId)
        {
            var sql = _dbContext.DbArtDetail.Select(a => new RArtDetail
            {
                ArtId = a.ArtId,
                FilePath = a.FilePath,
                Id = a.Id,
            }).Where(a => a.ArtId == artId);

            return sql.ToList();
        }
        public List<RArtInfo> QueryArtInfoList(string unionIdQuerier, 
            out int totalPage,
            string ownUnionId=null,
            ArtListOrderBy orderby = ArtListOrderBy.None,
            bool showall = false,
            int pageIndex=1,
            int pageSize=20)
        {
            var sql = from art in _dbContext.DbArtInfo.Where(a=>a.RecordStatus == RecordStatus.Normal)
                      join user in _dbContext.DBUserInfo on art.UnionId equals user.wx_unionid
                      join up in _dbContext.DbUserPraize.Where(a => a.PraizeTarget == PraizeTarget.ArtInfo && a.UnionId == unionIdQuerier)
                      on art.Id equals up.RefId into quer_up
                      from qup in quer_up.DefaultIfEmpty()
                      //orderby art.Praize descending, art.UploadDateTime descending
                      select new RArtInfo
                      {
                          CourseType = art.CourseType,
                          Title = art.Title,
                          Desc = art.Desc,
                          Id = art.Id,
                          Praize = art.Praize,
                          Comments = art.Comments,
                          CoverFilePath = art.CoverFilePath,
                          UploadDateTime = art.UploadDateTime,
                          UploadUser = art.UploadUser,
                          UnionId = art.UnionId,
                          UploaderHeaderUrl = user.wx_headimgurl,
                          ArtMediaType = art.ArtMediaType,
                          HasPriaize = qup != null,
                      };
          
               
            if (!string.IsNullOrEmpty(ownUnionId))
            {
                sql = sql.Where(a => a.UnionId == ownUnionId);
            }
            else
            {
                if(!showall)
                {
                    sql = sql.Where(a => a.UnionId == "cannotsee");
                }
            }
            switch(orderby)
            {
                case ArtListOrderBy.Praize:
                    sql = sql.OrderByDescending(a => a.Praize).ThenByDescending(a=>a.UploadDateTime); break;

                case ArtListOrderBy.UploadDateTime:
                    sql = sql.OrderByDescending(a => a.UploadDateTime).ThenByDescending(a=>a.Praize);break;
                default:
                    sql = sql.OrderByDescending(a => a.UploadDateTime); break;
            }
           
            totalPage = (sql.Count() / pageSize) + 1;

            sql = sql.Skip((pageIndex - 1) * pageSize).Take(pageSize);
      

            return sql.ToList();
        }

        public RArtInfo GetRArtInfo(long artId)
        {
            var sql = from art in _dbContext.DbArtInfo.Where(a => a.RecordStatus == RecordStatus.Normal)
                      join user in _dbContext.DBUserInfo on art.UnionId equals user.wx_unionid
                      where art.Id == artId
                      select new RArtInfo
                      {
                          CourseType = art.CourseType,
                          Title = art.Title,
                          Desc = art.Desc,
                          Id = art.Id,
                          Praize = art.Praize,
                          Comments = art.Comments,
                          CoverFilePath = art.CoverFilePath,
                          UploadDateTime = art.UploadDateTime,
                          UploadUser = art.UploadUser,
                          UnionId = art.UnionId,
                          UploaderHeaderUrl = user.wx_headimgurl,
                          ArtMediaType = art.ArtMediaType,
                          HasPriaize = false,
                      };
            return sql.FirstOrDefault();
        }

        public void DelArtInfo(long artId)
        {
            try
            {
                /*
                _dbContext.Database.BeginTransaction();
                string sql = $"delete from miniArtInfo where Id='{artId}'";
                _dbContext.Database.ExecuteSqlCommand(sql);

                sql = $"delete from miniArtDetail where artId='{artId}'";
                _dbContext.Database.ExecuteSqlCommand(sql);

                sql = $"delete from miniUserPraize where RefId='{artId}' and PraizeTarget=1";
                _dbContext.Database.ExecuteSqlCommand(sql);



                sql = $"delete from miniArtComment where artId='{artId}'";
                _dbContext.Database.ExecuteSqlCommand(sql);

                _dbContext.Database.CommitTransaction();
                */
                var sql = $"update miniArtInfo set recordstatus={(int)RecordStatus.Deleted} where Id='{artId}'";
                _dbContext.Database.ExecuteSqlCommand(sql);

            }
            catch(Exception ex)
            {
              //  _dbContext.Database.RollbackTransaction();

            }
           
        }
        public void AddComment(EArtComment comment)
        {
            _dbContext.DbArtComment.Add(comment);
        }

        public int DelComment(long comId)
        {
            string sql = $@"delete from miniartcomment where Id='{comId}'";
            return _dbContext.Database.ExecuteSqlCommand(sql);
        }

        public EArtComment GetArtComment(long comId)
        {
            return _dbContext.DbArtComment.Where(a => a.Id == comId).FirstOrDefault();
        }

        public List<RArtComment> QueryArtComment(long artId,string unionIdQuerier, out int totalpage, int pageIndex = 1, int pageSize = 20)
        {
            var sql = from com in _dbContext.DbArtComment
                      join ui in _dbContext.DBUserInfo on com.UnionId equals ui.wx_unionid
                      join up in _dbContext.DbUserPraize.Where(a => a.PraizeTarget == PraizeTarget.Comments && a.UnionId == unionIdQuerier)
                      on com.Id equals up.RefId into quer_up
                      from qup in quer_up.DefaultIfEmpty()
                      where com.ArtId == artId
                      orderby com.CreateDateTime descending,com.Praize descending
                      select new RArtComment
                      {
                          ArtId = artId,
                          CommentName = ui.Name,
                          Content = com.Content,
                          ClientDateTimeStr = DateSrv.DateTimeForClient(com.CreateDateTime),
                          CreateDateTime = com.CreateDateTime,
                          HeaderUrl = ui.wx_headimgurl,
                          Praize = com.Praize,
                          RefId = com.RefId,
                          UnionId = ui.wx_unionid,
                          Id = com.Id,
                          HasPriaize = qup != null,
                      };
            int c = sql.Count();
            totalpage = (sql.Count() / pageSize) + 1;
            sql = sql.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return sql.ToList();
        }

        /// <summary>
        /// 更新作品的赞，并同步用户赞表的数据
        /// </summary>
        /// <param name="artId"></param>
        /// <param name="unionId"></param>
        /// <param name="praize"></param>
        /// <returns></returns>
        public int UpdateArtPraize(long artId,string unionId,bool praize)
        {
            var art = GetArtInfo(artId);
            if (praize)
            {
               int c =  _dbContext.DbUserPraize.Where(a => a.UnionId == unionId &&
                a.PraizeTarget == PraizeTarget.ArtInfo &&
                a.RefId == artId).Count();
                if(c==0)
                {
                    art.Praize++;
                    EUserPraize up = new EUserPraize
                    {
                        PraizeDateTime = DateTime.Now,
                        PraizeTarget = PraizeTarget.ArtInfo,
                        RefId = artId,
                        UnionId = unionId,
                    };
                    _dbContext.DbUserPraize.Add(up);
                }
            }             
            else
            {
                art.Praize--;
                if (art.Praize < 0)
                    art.Praize = 0;
                var up = _dbContext.DbUserPraize.Where(a => a.UnionId == unionId &&
                a.PraizeTarget == PraizeTarget.ArtInfo &&
                a.RefId == artId).FirstOrDefault();

                if (up != null)
                    _dbContext.DbUserPraize.Remove(up);

            }
              

            return 0;
        }

        /// <summary>
        /// 更新评论的赞，并同步用户赞表的数据
        /// </summary>
        /// <param name="artId"></param>
        /// <param name="unionId"></param>
        /// <param name="praize"></param>
        /// <returns></returns>
        public int UpdateCommentPraize(long comId, string unionId, bool praize)
        {
            var comment = GetArtComment(comId);
            if (praize)
            {
                int c = _dbContext.DbUserPraize.Where(a => a.UnionId == unionId &&
                a.PraizeTarget == PraizeTarget.Comments &&
                a.RefId == comId).Count();
                if (c == 0)
                {
                    comment.Praize++;
                    EUserPraize up = new EUserPraize
                    {
                        PraizeDateTime = DateTime.Now,
                        PraizeTarget = PraizeTarget.Comments,
                        RefId = comId,
                        UnionId = unionId,
                    };
                    _dbContext.DbUserPraize.Add(up);
                }
            }
            else
            {
                comment.Praize--;
                if (comment.Praize < 0)
                    comment.Praize = 0;
                var up = _dbContext.DbUserPraize.Where(a => a.UnionId == unionId &&
                a.PraizeTarget == PraizeTarget.Comments &&
                a.RefId == comId).FirstOrDefault();

                if (up != null)
                    _dbContext.DbUserPraize.Remove(up);

            }
            return 0;
        }

        #region 
        public List<RNewsInfo> QueryNewsList(out int totalPage,int pageIndex=1,int pageSize =20, NewsPublishStatus publishStatus =  NewsPublishStatus.Published)
        {
            IQueryable<RNewsInfo> sql = _dbContext.DbNewsInfo.Select(a => new RNewsInfo
            {
                Auther = a.Auther,
                CoverImgUrl = a.CoverImgUrl,
                CreateDateTime = a.CreateDateTime,
                UpdateDateTime = a.UpdateDateTime,
                wxMediaID = a.wxMediaID,
                Id = a.Id,
                NewsSource = a.NewsSource,
                PageUrl = a.PageUrl,
                PublishStatus = a.PublishStatus,
                Title = a.Title,
            });
            if (publishStatus != NewsPublishStatus.All)
                sql = sql.Where(a => a.PublishStatus == publishStatus);

            totalPage = (sql.Count() / pageSize) + 1;

            sql = sql.OrderByDescending(a => a.CreateDateTime);

            

            sql = sql.Skip((pageIndex - 1) * pageSize).Take(pageSize);


            return sql.ToList();

        }
        #endregion




    }
}
