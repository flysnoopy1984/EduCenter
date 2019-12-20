using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterCore.AppFramework
{
    public static class XYAppConfigReader
    {
        private static IConfiguration _Configuration = null;

        public static void SetConfiguration(IConfiguration Configuration)
        {
            _Configuration = Configuration;
            InitAppConfig();
        }

        public static void InitAppConfig()
        {
            XYAppConfig.UserHeaderImagePath = _Configuration["XYAppConfig:UserHeaderImagePath"];
            XYAppConfig.ResSite = _Configuration["XYAppConfig:ResSite"];


        }
    }
}
