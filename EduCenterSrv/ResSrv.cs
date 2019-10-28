using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Res;
using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EduCenterSrv
{
    public class ResSrv: BaseSrv
    {
        public ResSrv(EduDbContext dbContext) : base(dbContext){ }

        public List<EAppBanner> GetBannerList()
        {
           return _dbContext.DbBanner.Where(a => a.RecordStatus == RecordStatus.Normal)
                .OrderBy(a=>a.Position).ToList();
        }

        /// <summary>
        /// App 首页Grid的Icons 
        /// </summary>
        /// <returns></returns>
        public List<EAppIcons> GetAppNavGridIcons()
        {
            return _dbContext.DbAppIcons.Where(a => a.RecordStatus == RecordStatus.Normal)
                .OrderBy(a => a.Position).ToList();
        }


    }
}
