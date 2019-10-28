using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterSrv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduAPI.Controllers.app
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppContentController : BaseAPI
    {
        private AppEduSrv _AppEduSrv;
        public AppContentController(AppEduSrv appEduSrv)
        {
            _AppEduSrv = appEduSrv;
        }
    }
}