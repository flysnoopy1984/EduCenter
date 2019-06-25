using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterSrv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduCenterWeb.Pages.AliPay
{
    [Route("api/[controller]")]
    [ApiController]
    public class AliPayController : ControllerBase
    {
        private BusinessSrv _BusinessSrv;
        public AliPayController(BusinessSrv businessSrv)
        {
            _BusinessSrv = businessSrv;
        }

        [HttpGet]
        public string Get()
        {

            return ""; 
        }

        [HttpPost]
        public string Post()
        {
            return "";
        }
    }
}