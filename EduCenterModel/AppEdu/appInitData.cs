using EduCenterModel.Res;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.AppEdu
{
    public class AppInitData
    {
        public List<EAppBanner> BannerList { get; set; }

        public List<EAppIcons> AppIconsList { get; set; }

        public List<EAppNavigation> AppNavList { get; set; }
    }
}
