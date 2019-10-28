using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduAPI
{
    public class BaseAPI: ControllerBase
    {
        public const string Cache_Banner = "BannerList";
        public const string Cache_AppIcons = "AppIconsList";

        public const string Cache_AppFrameworkData = "AppFrameworkData";

        static MemoryCacheEntryOptions _MemoryCacheOptions;

        static MemoryCacheEntryOptions GetMemoryCacheEntryOptions()
        {
            if(_MemoryCacheOptions==null)
            {
                _MemoryCacheOptions = new MemoryCacheEntryOptions();
                _MemoryCacheOptions.AbsoluteExpiration = DateTime.Now.AddHours(2);
              
            }
            return _MemoryCacheOptions;
        }

    }
}
