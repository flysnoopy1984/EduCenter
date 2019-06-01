using EduCenterModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduCenterWeb.Pages
{
    public class EduBaseAppPageModel:PageModel
    {
        /* Session Begin */
        //public List<EUserShopingCard> UserPreBuyCourse
        //{
        //    get
        //    {
        //        string json = HttpContext.Session.GetString("UserCourseScheule");
        //        if (string.IsNullOrEmpty(json))
        //            return new List<EUserShopingCard>();
        //        else
        //            return JsonConvert.DeserializeObject<List<EUserShopingCard>>(json);

        //    }
        //    set
        //    {
        //        string json = JsonConvert.SerializeObject(value);
        //        HttpContext.Session.SetString("UserCourseScheule", json);
        //    }
        //}
        /* Session End */
    }
}
