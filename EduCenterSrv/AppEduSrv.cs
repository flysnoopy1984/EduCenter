using EduCenterModel.AppEdu;
using EduCenterModel.BaseEnum;
using EduCenterModel.Res;
using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EduCenterSrv
{
    public class AppEduSrv: BaseSrv
    {
        public AppEduSrv(EduDbContext dbContext) : base(dbContext)
        {

        }


        public AppInitData InitData(ResSrv resSrv)
        {
            AppInitData initData = new AppInitData();
            initData.BannerList =  resSrv.GetBannerList();
      //      initData.AppIconsList = resSrv.GetAppNavGridIcons();

            initData.AppNavList = NavsList();
            return initData;

        }

        public List<EAppNavigation> NavsList()
        {
            return _dbContext.DbAppNavigation.Where(a => a.RecordStatus == RecordStatus.Normal)
                .OrderBy(a => a.Position).ToList();
        }


    }
}
