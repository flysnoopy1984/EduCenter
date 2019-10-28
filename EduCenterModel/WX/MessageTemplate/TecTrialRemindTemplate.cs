using EduCenterModel.Course;
using EduCenterModel.Course.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.WX.MessageTemplate
{
    /* TUbR0kbed9NM8Oh-yoHnnHI20NXb83URlR6rszXpqiM
     {{first.DATA}}
学员姓名：{{keyword1.DATA}}
课程名称：{{keyword2.DATA}}
上课日期：{{keyword3.DATA}}
上课时间：{{keyword4.DATA}}
上课教室：{{keyword5.DATA}}
{{remark.DATA}}
         */
    public class TecTrialRemindTemplate : BaseTemplate<TecTrialRemindTemplate>
    {

        public TecTrialRemindTemplate GenerateData(string toUserOpenId, RTrialLog eTrialLog)
        {
            string first = $"有用户预约了您的试听课.";
            string remark = $"接待人:{eTrialLog.SalesName}";
            var data = new
            {
                first = new TemplateField() { value = first, color = "#EB6B13" },
                keyword1 = new TemplateField() { value = eTrialLog.UserRealName, color = "#FFC753" },
                keyword2 = new TemplateField() { value = $"{ eTrialLog.CourseName} | { eTrialLog.TrialTimeStr }" },
                keyword3 = new TemplateField() { value = $"{ eTrialLog.TrialDateStr}",color = "#FFBA00" },
                keyword4 = new TemplateField() { value = $"{ eTrialLog.TrialTimeStr }",color = "#FFBA00" },
                keyword5 = new TemplateField() { value = $"高青路校区" },
                remark = new TemplateField { value = remark, color = "#007ACC" },
            };

            TecTrialRemindTemplate obj = base.InitObject(toUserOpenId, "", "TUbR0kbed9NM8Oh-yoHnnHI20NXb83URlR6rszXpqiM");
            obj.data = data;
            return obj;

        }
    }
}
