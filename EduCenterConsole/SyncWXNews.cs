using EduCenterModel.News;
using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EduCenterModel.WX;
using EduCenterCore.WX;
using Newtonsoft.Json;
using EduCenterCore.Common.Helper;
using EduCenterModel.WX.Media;
using System.Linq;
using EduCenterModel.BaseEnum;
using EduCenterSrv.Common;

namespace EduCenterConsole
{
    public class SyncWXNews
    {
        protected EduDbContext _dbContext;


        private int _NewsTotalItems=-1;
        private int _pageIndex = 0;

        private int _SyncCount;
        public int SyncCount {
            get { return _SyncCount; }
        }
        public SyncWXNews(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Run()
        {
            var JOMedia = QueryNews();
            while(JOMedia!=null)
            {
                foreach(var item in JOMedia.item)
                {
                    foreach (var news in item.content.news_item)
                    {
                        var c= _dbContext.DbNewsInfo.Where(a => a.wxMediaID == item.media_id && a.NewsSource == NewsSource.WX_YunYi_GZH).Count();
                        if(c==0)
                        {
                           
                            ENewsInfo eNews = new ENewsInfo()
                            {
                                NewsSource = NewsSource.WX_YunYi_GZH,
                                Auther = news.author,
                                CoverImgUrl = news.thumb_url,
                                PageUrl = news.url,
                                Title = news.title,
                                wxMediaID = item.media_id,
                                CreateDateTime = DateSrv.ConverTimeStamp(item.content.create_time),
                                UpdateDateTime = DateSrv.ConverTimeStamp(item.content.update_time),   
                            };
                            _dbContext.DbNewsInfo.Add(eNews);
                            _SyncCount++;
                        }
                        _dbContext.SaveChanges();

                    }
                }

                this._pageIndex++;
                JOMedia = QueryNews();   
            }
          
        }

        private void SyncData()
        {
        }


        private void Init()
        {
            _dbContext.Database.ExecuteSqlCommand("delete from miniNewsInfo");
        }

        private JOMedia QueryNews()
        {
            if (_pageIndex * 20 >= _NewsTotalItems && _NewsTotalItems!=-1) return null;

            string access_token = WXApi.getAccessToken().access_token;
            string wxUrl = $"https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={access_token}";
            MaterialList_In paremeter = new MaterialList_In
            {
                type = "news",
                offset = _pageIndex*20,
                count = 20,
            };
            var json = JsonConvert.SerializeObject(paremeter);
            string data = HttpHelper.RequestUrlSendMsg(wxUrl, HttpHelper.HttpMethod.Post, json);


            var JOMedia = JsonConvert.DeserializeObject<JOMedia>(data);

            _NewsTotalItems = Convert.ToInt32(JOMedia.total_count);
            return JOMedia;

        }

    }
}
