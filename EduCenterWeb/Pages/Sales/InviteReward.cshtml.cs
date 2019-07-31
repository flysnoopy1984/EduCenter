using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduCenterModel.BaseEnum;
using EduCenterModel.Sales;
using EduCenterModel.Sales.Result;
using EduCenterSrv;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EduCenterWeb.Pages.Sales
{
    public class InviteRewardModel : EduBaseAppPageModel
    {
        private SalesSrv _SalesSrv;
        public RInviteLog InviteLog { get; set; }

        public List<EInviteRewardTrans> RewardTrans { get; set; }

        public EInviteRewardTrans TrialReward { get; set; }

        public EInviteRewardTrans PaidReward { get; set; }

        public InviteRewardModel(SalesSrv salesSrv)
        {
            _SalesSrv = salesSrv;
        }
        public void OnGet()
        {
            var vs = GetUserSession();
            if(vs!=null)
            {
                var Id = Request.Query["Id"];
                if(!string.IsNullOrEmpty("Id"))
                {
                    InviteLog = _SalesSrv.GetInviteLogById(Convert.ToInt64(Id));
                    _SalesSrv.GetRewardTransByInvitedId(InviteLog.Id);
                    RewardTrans = _SalesSrv.GetRewardTransByInvitedId(InviteLog.Id);
                    TrialReward = RewardTrans.Where(a => a.TransType == AmountTransType.Invited_TrialReward).FirstOrDefault();
                    PaidReward = RewardTrans.Where(a => a.TransType == AmountTransType.Invited_Paied).FirstOrDefault();

                }
                    
            }
        }

    }
}