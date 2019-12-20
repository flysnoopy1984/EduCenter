using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.Common;
using EduCenterModel.Res;
using EduCenterSrv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EduAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ResController : BaseAPI
    {
        private IMemoryCache _MemoryCache;
        private ResSrv _resSrv;
        public ResController(IMemoryCache memoryCache,ResSrv resSrv)
        {
            _MemoryCache = memoryCache;
            _resSrv = resSrv;
        }
        public ResultList<EAppBanner> GetBannerList()
        {

            ResultList<EAppBanner> result = new ResultList<EAppBanner>();
            try
            {
                result.List = _MemoryCache.GetOrCreate<List<EAppBanner>>(Cache_Banner, entry=> { return _resSrv.GetBannerList(); });
                
            }
            catch(Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }
            return result;
        }

        
    }
}