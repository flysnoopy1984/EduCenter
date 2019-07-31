using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.WX.MessageTemplate
{
    /*IuCfy5jeoalgsuVUNniVPkZ5wBhLxKjg1caIrz0F9g8
     {{first.DATA}}
上课日期：{{keyword1.DATA}}
班级名称：{{keyword2.DATA}}
本次扣课时：{{keyword3.DATA}}
剩余总课时：{{keyword4.DATA}}
{{remark.DATA}}
         */
    public class UserSignTemplate : BaseTemplate<UserSignTemplate>
    {
        public UserSignTemplate GenerateData(string toUserOpenId,
           string SignUser,
           string CourseDateTime,
           string CourseName,
           double consumeTime,
           double remainStd,
           double remainSummer,
           double remainWinter

          )
        {
           
            string first = $"您的课程已经签到，请知晓";
            string remark = $"【标准课时:{remainStd}】 \r\n【暑假课时:{remainSummer}】 \r\n【寒假课时:{remainWinter}】\r\n签到人:{SignUser}\r\n点击消息，查看签到详情";
            string RemainTimeStr = $"{remainWinter+ remainSummer+ remainWinter} ";

            var data = new
            {
                first = new TemplateField() { value = first},
                keyword1 = new TemplateField() { value = CourseDateTime, color = "#068200" },
                keyword2 = new TemplateField() { value = CourseName },
                keyword3 = new TemplateField() { value = Convert.ToString(Math.Round(consumeTime, 2)), color = "#EB6B13" },
                keyword4 = new TemplateField() { value = RemainTimeStr,color= "#FFBA00" },
                remark = new TemplateField { value = remark, color = "#0974FF" },
            };
            string url = WebUrl + $"&openid={toUserOpenId}&toPage=/User/SignList";

            UserSignTemplate obj = base.InitObject(toUserOpenId, url, "IuCfy5jeoalgsuVUNniVPkZ5wBhLxKjg1caIrz0F9g8");
            obj.data = data;
            return obj;

        }

    }
}
