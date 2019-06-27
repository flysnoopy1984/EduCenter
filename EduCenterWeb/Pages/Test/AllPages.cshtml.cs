using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.Test
{
    public class AllPagesModel : EduBaseAppPageModel
    {
        public void OnGet()
        {
            base.SetUserSesion("o3nwE0qI_cOkirmh_qbGGG-5G6B0","Jacky", 
                "http://thirdwx.qlogo.cn/mmopen/hzVGicX27IG18yibKNnHfBojH4SpCPGNEvyOUZE8jxOw2ZnYcHzAkm72jugRaRc53jn1zZER32wE4SUib1aX3W7qwTZgolC4HWk/132","",
                EduCenterModel.BaseEnum.CourseScheduleType.Standard);
        }
    }
}