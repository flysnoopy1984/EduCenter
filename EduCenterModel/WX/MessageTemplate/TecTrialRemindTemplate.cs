using EduCenterModel.Course;
using EduCenterModel.Course.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.WX.MessageTemplate
{
    /* xp8syqSpqXSkZ4kYuCmAFHPQ-jw5gykbqmZl4Eg4kos
     {{first.DATA}}
试听课程：{{keyword1.DATA}}
试听时间：{{keyword2.DATA}}
试听地址：{{keyword3.DATA}}
注意事项：{{keyword4.DATA}}
{{remark.DATA}}
         */
    public class TecTrialRemindTemplate: BaseTemplate<TecTrialRemindTemplate>
    {
      
        public TecTrialRemindTemplate GenerateData(string toUserOpenId, RTrialLog eTrialLog)
        {
            string first = $"有用户预约了您的试听课.";
            string remark = $"接待人:{eTrialLog.SalesName}";
            var data = new
            {
                first = new TemplateField() { value = first, color = "#EB6B13" },
                keyword1 = new TemplateField() { value = eTrialLog.CourseName, color= "#FFC753" },
                keyword2 = new TemplateField() { value = $"{ eTrialLog.TrialDateStr} | { eTrialLog.TrialTimeStr }" },
                keyword3 = new TemplateField() { value = "高青路校区" },
                keyword4 = new TemplateField() { value = $"登记的宝贝名:{eTrialLog.UserRealName},如有问题请联系接待人",color= "#17A05D" },
                remark = new TemplateField { value = remark, color = "#007ACC" },
            };

            TecTrialRemindTemplate obj = base.InitObject(toUserOpenId, "", "xp8syqSpqXSkZ4kYuCmAFHPQ-jw5gykbqmZl4Eg4kos");
            obj.data = data;
            return obj;

        }
    }
}
