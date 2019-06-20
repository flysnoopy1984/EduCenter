using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.MasterData
{
    public class GlobalConfigModel : EduBasePageModel
    {
        public Dictionary<int,List<ECourseDateRange>> DateRange;

        public void OnGet()
        {
            var list = StaticDataSrv.CourseDateRange.OrderBy(a=>a.Year).ThenBy(a=>a.CourseScheduleType);
            DateRange = new Dictionary<int, List<ECourseDateRange>>();
            foreach (var r in list)
            {
                try
                {
                    DateRange[r.Year].Add(r);
                }
                catch
                {
                    DateRange.Add(r.Year, new List<ECourseDateRange>());
                    DateRange[r.Year].Add(r);
                }
            }
        }
    }
}