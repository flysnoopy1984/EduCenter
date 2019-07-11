using EduCenterModel.Course;
using EduCenterModel.Course.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.WX.MessageTemplate
{
    //r71THWj2pi0o5FeWVBfLlkgkkgAXq7A2um59w4QYVuA
    /*
     {{first.DATA}}
    课程名称：{{keyword1.DATA}}
    预约时间：{{keyword2.DATA}}
    课程时长：{{keyword3.DATA}}
    老师姓名：{{keyword4.DATA}}
    {{remark.DATA}}
    */
    public class UserTrialRemindTemplate : BaseTemplate<UserTrialRemindTemplate>
    {
        public object data { get; set; }
        public UserTrialRemindTemplate GenerateData(string toUserOpenId,RTrialLog eTrialLog)
        {
            string first = $"尊敬的{eTrialLog.UserRealName},您预约的试听课请不要忘记参加.";
            string remark = string.Format("如需取消，点击进入此消息后操作");
            var data = new
            {
                first = new TemplateField() { value = first, color = "#EB6B13" },
                keyword1 = new TemplateField() { value = eTrialLog.CourseName },
                keyword2 = new TemplateField() { value = $"{ eTrialLog.TrialDateStr} | { eTrialLog.TrialTimeStr }" },
                keyword3 = new TemplateField() { value = "约45分钟" },
                keyword4 = new TemplateField() { value= eTrialLog.TecName},
                remark = new TemplateField { value = remark, color = "#007ACC" },
            };
          

            string url = WebUrl + $"&openid={toUserOpenId}&toPage=MyTrial";

            UserTrialRemindTemplate obj = base.InitObject(toUserOpenId, url, "r71THWj2pi0o5FeWVBfLlkgkkgAXq7A2um59w4QYVuA");
            obj.data = data;
            return obj;

        }
    }
}
