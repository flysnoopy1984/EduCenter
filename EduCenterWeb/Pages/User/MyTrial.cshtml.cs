using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Course.Result;
using EduCenterSrv;
using EduCenterSrv.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.User
{
    public class MyTrialModel : EduBaseAppPageModel
    {
        private CourseSrv _CourseSrv;
        public List<ETrialLog> TrialLogList { get; set; }

        public List<ETrialLog> CurrentTrialList { get; set; }
        public MyTrialModel(CourseSrv courseSrv)
        {
            _CourseSrv = courseSrv;
        }

       
       
        public void OnGet()
        {
            var us = base.GetUserSession();
            if (us != null)
            {
                TrialLogList = _CourseSrv.QueryTrialLogList(us.OpenId);
                if(TrialLogList !=null)
                {
                    CurrentTrialList = TrialLogList.Where(a=>a.TrialDateTime>= DateTime.Today).ToList();
                    if (CurrentTrialList.Count == 0) CurrentTrialList = null;

                    var checkList = TrialLogList.Where(a => a.TrialDateTime < DateTime.Today && 
                                                       (a.TrialLogStatus == TrialLogStatus.UserApply ||
                                                       a.TrialLogStatus == TrialLogStatus.TecConfirm)).ToList();
                    if (checkList.Count>0)
                    {
                        foreach (var log in checkList)
                        {
                            log.TrialLogStatus = EduCenterModel.BaseEnum.TrialLogStatus.UserNotCome;
                            _CourseSrv.UpdateTrialStatus(log);
                        }

                    }
                    
                }
            }
        }

        public IActionResult OnPostCancelTrial(long Id)
        {
            ResultNormal result = new ResultNormal();
            try
            {
                var us = base.GetUserSession(false);
                if (us != null)
                {
                    _CourseSrv.UpdateTrialStatus(Id, TrialLogStatus.Cancel);
                }
                else
                {
                    result.IntMsg = -1;
                    result.ErrorMsg = "需要您重新登录！";
                }
            }
            catch (Exception ex)
            {
                result.ErrorMsg = "提交申请失败,请联系工作人员";
                NLogHelper.ErrorTxt($"我的试听[OnPostCancelTrial]:{ex.Message}");
            }
            return new JsonResult(result);
        }





    }
}