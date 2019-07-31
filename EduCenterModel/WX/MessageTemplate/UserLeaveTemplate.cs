using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.WX.MessageTemplate
{
    /*
     h_kvGtdynZ-XoE1fVHz2zM60ae_yC_lx3RIRVDUU3Rc 学员请假通知

        {{first.DATA}}
学员名称：{{keyword1.DATA}}
请假时间：{{keyword2.DATA}}
请假课程：{{keyword3.DATA}}
{{remark.DATA}}
    */

    public class UserLeaveTemplate: BaseTemplate<UserLeaveTemplate>
    {
        public UserLeaveTemplate GenerateData(string toUserOpenId,
            string BabyName,
            DateTime LeaveDate,
            string CourseDesc,
            bool needDetail = false
          
           )
        {

            string first = $"有学生发起了请假申请,请知晓";

            string remark = "点击消息，查看您课程详情";
           


            var data = new
            {
                first = new TemplateField() { value = first, color = "#EB6B13" },
                keyword1 = new TemplateField() { value = BabyName },
                keyword2 = new TemplateField() { value = LeaveDate.ToString("yyyy-MM-dd"), color = "#81D842" },
                keyword3 = new TemplateField() { value = CourseDesc, color = "#81D842" },
                remark = new TemplateField { value = remark, color = "#FFC753" },
            };
            string url = "";
            if(needDetail)
            {
                url = WebUrl + $"&openid={toUserOpenId}&toPage=/Teacher/DayCourse?date={LeaveDate.ToString("yyyy-MM-dd")}";
            }
            UserLeaveTemplate obj = base.InitObject(toUserOpenId, url, "h_kvGtdynZ-XoE1fVHz2zM60ae_yC_lx3RIRVDUU3Rc");
            obj.data = data;
            return obj;

        }
    }
}
