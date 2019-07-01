using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterCore.EduFramework
{
    public static class EduConfigReader
    {
        private static IConfiguration _Configuration = null;

        public static void SetConfiguration(IConfiguration Configuration)
        {
            _Configuration = Configuration;
            InitEduConfig();
        }

        public static void InitEduConfig()
        {
            EduConfig.Version = _Configuration["EduConfig:Version"];
            EduConfig.EduOrg = _Configuration["EduConfig:EduOrg"];
            EduConfig.AppMainSite = _Configuration["EduConfig:AppMainSite"];
            EduConfig.IsTest = Convert.ToBoolean(_Configuration["EduConfig:IsTest"]);

            //EduConfig.WXAppId = _Configuration["EduConfig:WXAppId"];
            //EduConfig.WXSecret = _Configuration["EduConfig:WXSecret"];
        }
       
    }
}
