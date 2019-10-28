using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.WX.MessageTemplate
{
    /*
      n7VI_vPJVcBhY79H5a7RpSBKUeehYt5-hU-d983qWLE

     {{first.DATA}}
会员昵称：{{keyword1.DATA}}
关注时间：{{keyword2.DATA}}
{{remark.DATA}}
       */
    public class NewUserJoinWXTemplate: BaseTemplate<NewUserJoinWXTemplate>
    {
       
        public NewUserJoinWXTemplate GenerateData(string toUserOpenId, 
            string userName,
            DateTime JoinDate,
            string InviteName = null //邀请人
           )
        {
        
            string first = $"有新的用户关注了我们的公众号";

            string remark = "";
            if(!string.IsNullOrEmpty(InviteName))
            {
                remark = $"邀请人：[{InviteName}]";
            }
            else
                remark = $"邀请人：无";

            var data = new
            {
                first = new TemplateField() { value = first, color = "#EB6B13" },
                keyword1 = new TemplateField() { value = userName },
                keyword2 = new TemplateField() { value = JoinDate.ToString("yyyy-MM-dd HH:mm:ss"), color = "#81D842" },
                remark = new TemplateField { value = remark, color = "#FFC753" },
            };
      
            NewUserJoinWXTemplate obj = base.InitObject(toUserOpenId, "", "n7VI_vPJVcBhY79H5a7RpSBKUeehYt5-hU-d983qWLE");
            obj.data = data;
            return obj;

        }
    }
}
