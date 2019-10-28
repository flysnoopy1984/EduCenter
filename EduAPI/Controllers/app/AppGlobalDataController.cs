using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.AppEdu;
using EduCenterModel.Common;
using EduCenterSrv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EduAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppGlobalDataController : BaseAPI
    {
        private IMemoryCache _MemoryCache;
        private ResSrv _resSrv;
        private AppEduSrv _appEduSrv;
        public AppGlobalDataController(IMemoryCache memoryCache, 
            ResSrv resSrv,
            AppEduSrv appEduSrv)
        {
            _appEduSrv = appEduSrv;
            _MemoryCache = memoryCache;
            _resSrv = resSrv;
        }

        public ResultObject<AppInitData> GetInitData()
        {
            NLogHelper.InfoTxt("Invoke GetInitData");
            ResultObject<AppInitData> result = new ResultObject<AppInitData>();
            try
            {
                result.Entity = _MemoryCache.GetOrCreate(Cache_AppFrameworkData,e=> { return _appEduSrv.InitData(_resSrv); });
            }
            catch(Exception ex)
            {
                NLogHelper.ErrorTxt($"[AppEdu]GetInitData:{ex.Message}");
                result.ErrorMsg = ex.Message;
            }
            return result;
        }
    }
}