using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.WX.MessageTemplate
{
    /*
     	tSw-YvTMPjTYieuDGdjBlH-ZOyZa9FxTW4SWt63Kup8
        {{first.DATA}}
变动时间：{{keyword1.DATA}}
变动金额：{{keyword2.DATA}}
帐户余额：{{keyword3.DATA}}
{{remark.DATA}}
         */
    public class UserAccountChangeTemplate: BaseTemplate<UserAccountChangeTemplate>
    {
      
        public UserAccountChangeTemplate GenerateData(string toUserOpenId, string InvitedName,
            AmountTransType amountTransType,
            DateTime transDate,
            double UserBalance,
            double Amount)
        {
           
            string desc = "";
            if(amountTransType == AmountTransType.Invited_TrialReward)
                desc = $"您邀请的用户[{InvitedName}],试听课成功参加.您获得奖励金额[{Amount}]";
            else if(amountTransType == AmountTransType.Invited_Paied)
                desc = $"您邀请的用户[{InvitedName}],已正式加入学院.您获得奖励金额[{Amount}]";

            string first = desc;
           
            string remark = $"请点击此条消息查看您的账户变动！也可通过[菜单]->[我的账户]查看";
            var data = new
            {
                first = new TemplateField() { value = first, color = "#EB6B13" },
                keyword1 = new TemplateField() { value = transDate.ToString("yyyy-MM-dd") },
                keyword2 = new TemplateField() { value = $"+【{Amount}】" , color = "#FFC753" },
                keyword3 = new TemplateField() { value =$"{UserBalance.ToString("0.00")} 元" },
              
                remark = new TemplateField { value = remark, color = "#007ACC" },
            };
            string url = WebUrl + $"&openid={toUserOpenId}&toPage=/User/MyCourseTime";

            UserAccountChangeTemplate obj = base.InitObject(toUserOpenId, url, "tSw-YvTMPjTYieuDGdjBlH-ZOyZa9FxTW4SWt63Kup8");
            obj.data = data;
            return obj;

        }
    }
}
