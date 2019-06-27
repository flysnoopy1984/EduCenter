using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Session;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class BuyCourseTimeModel : EduBaseAppPageModel
    {
        private BusinessSrv _BusinessSrv;
        private CourseSrv _CourseSrv;
        public UserSession UserSession { get; set; }
        public List<ECoursePrice> PriceList { get; set; }

        public List<ECoursePrice> SummerPriceList { get; set; }

        public List<ECoursePrice> WinterPriceList { get; set; }
        public BuyCourseTimeModel(CourseSrv courseSrv, BusinessSrv businessSrv)
        {
            _CourseSrv = courseSrv;
            _BusinessSrv = businessSrv;
        }

        public void OnGet()
        {
            UserSession = this.GetUserSession();
            if(UserSession != null)
            {
                var list = _CourseSrv.GetCoursePriceList();
                PriceList =list.Where(a=>a.CourseScheduleType ==CourseScheduleType.Standard).ToList();
                SummerPriceList = list.Where(a => a.CourseScheduleType == CourseScheduleType.Summer).ToList();
                WinterPriceList = list.Where(a => a.CourseScheduleType == CourseScheduleType.Winter).ToList();
            }
               

        }

        public IActionResult OnPostBuyCourse(string priceCode)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                var us = GetUserSession(false);
                if(us !=null)
                {
                    ECoursePrice eCoursePrice =  _CourseSrv.GetCoursePrice(priceCode);
                    var order = _BusinessSrv.PayCourseOrder(us.OpenId, eCoursePrice);
                    _BusinessSrv.PayCourseSuccess(order.OrderId);
                    result.IntMsg = (int)eCoursePrice.CourseScheduleType;

                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "请先重新登录";
                }

            }
            catch (EduException eex)
            {
                result.ErrorMsg = eex.Message;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "购买失败，请联系客服！";
                NLogHelper.ErrorTxt($"购买课时[OnPostBuyCourse]:{ex.Message}");
             
            }
            return new JsonResult(result);
        }
    }
}