using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterCore.Common.Helper;
using EduCenterCore.WX;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Course.Result;
using EduCenterModel.Teacher;
using EduCenterModel.User;
using EduCenterModel.User.Result;
using EduCenterModel.WX.MessageTemplate;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.WebBackend.Tec
{
    public class TrialCourseModel : EduBasePageModel
    {
        public List<ETecInfo> TecList { get; set; }
        public List<ECourseInfo> CourseList { get; set; }

        private TecSrv _TecSrv;
        private CourseSrv _CourseSrv;
        private SalesSrv _SalesSrv;
        private UserSrv _UserSrv;


        public TrialCourseModel(TecSrv tecSrv, CourseSrv courseSrv, SalesSrv salesSrv, UserSrv userSrv)
        {
            _TecSrv = tecSrv;
            _CourseSrv = courseSrv;
            _SalesSrv = salesSrv;
            _UserSrv = userSrv;
        }
        public void OnGet()
        {
            TecList = _TecSrv.GetAllStaffTec();
        }

        public IActionResult OnPostQueryTrialLog(string fromDate,string toDate,string tecCode,int pageIndex,int pageSize)
        {
            ResultList<RTrialLog> result = new ResultList<RTrialLog>();
            try
            {
                int recordTotal;
                result.List = _CourseSrv.QueryTrialLogList_BackEnd(fromDate, toDate,out recordTotal,tecCode,pageIndex,pageSize);
                result.RecordTotal = recordTotal;
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }

        public IActionResult OnPostConfirmTrialStatus(long Id)
        {
            ResultNormal result = new ResultNormal();
            try
            {

                 _CourseSrv.UpdateTrialStatus(Id, TrialLogStatus.TecConfirm);
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }

        //用户试听课微信提醒
        public IActionResult OnPostWxRemind(long Id)
        {
            ResultNormal result = new ResultNormal();
            try
            {

                var trial = _CourseSrv.GetTrialLogById(Id);

                //微信发送
                UserTrialRemindTemplate wxMessage = new UserTrialRemindTemplate();
                wxMessage.data = wxMessage.GenerateData(trial.OpenId, trial);
                WXApi.SendTemplateMessage<UserTrialRemindTemplate>(wxMessage);

                if (result.IsSuccess)
                {
                    _CourseSrv.AddTrialRemindCount(Id);
                }

            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }

        /// <summary>
        /// 发送奖励金
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult OnPostSendReward(long invitelogId,string invitedOpenId,string ownOpenId)
        {
            ResultNormal result = new ResultNormal();
            try
            {
               AmountTransType amountTransType = AmountTransType.Invited_TrialReward;
               EUserAccount ownAccount;
               bool needWx =  _SalesSrv.CreateRewardTrans(invitelogId, ownOpenId, amountTransType, out ownAccount);

                if(ownAccount !=null)
                {
                    var ui = _UserSrv.GetUserInfo(invitedOpenId);
                    NLogHelper.InfoTxt($"wxMessage:OpenId-{ownOpenId}");
                   //微信提醒
                   UserAccountChangeTemplate wxMessage = new UserAccountChangeTemplate();
                    wxMessage.data = wxMessage.GenerateData(ownOpenId,
                        ui.Name,
                        amountTransType,
                        DateTime.Now,
                        ownAccount.InviteRewards,
                        GlobalSrv.GetRewardAmount(amountTransType)
                        );
                    WXApi.SendTemplateMessage<UserAccountChangeTemplate>(wxMessage);
                }
            
            }
            catch (Exception ex)
            {
                result.ErrorMsg = ex.Message;
            }

            return new JsonResult(result);
        }
    }
}