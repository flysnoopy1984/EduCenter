using EduCenterCore.EduFramework;
using EduCenterModel.WX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EduCenterCore.WX
{
    public class WXReplyContent
    {
        public static string DefaultMsessage()
        {
            return "有问题请留言，我们会尽快回复！";
        }
        public static string NewTec(string Name)
        {
            string msg = $@"亲爱的{Name}老师，欢迎您加入{EduConfig.EduOrg}的大家庭。";
            //<a href='http://{EduConfig.AppMainSite}/Teacher/Join'>请点击填写些简单资料</a>"
            return msg;
        }

        public static string UserComing(string Name)
        {
            return $"亲爱的{Name}，欢迎回来！";
        }
        public static string NewUserLook(string Name)
        {
            return $"您好,{Name},欢迎光临云艺国学教育！";
        }

        public static string NewUserAdd(string Name)
        {
            return $"您好,{Name},欢迎加入云艺国学教育大家庭，愿我们能带给宝贝们不一样的学习体验！";
        }

    }
}
