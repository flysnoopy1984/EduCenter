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
            return "有问题请留言，我们会尽快回复！您可点击<a href='http://mp.weixin.qq.com/s?__biz=MzU3NDk2NjE1MQ==&mid=100000028&idx=1&sn=4146ed60a3d7d93038b1499bb53926ee&chksm=7d2b11f44a5c98e278a2cf6604b6b703d30f7ad1a06a9a45507b3c912128df49723c9f35646c#'>用户手册查看书院使用说明</a>";
        }
        public static string NewTec(string Name)
        {
            string msg = $@"亲爱的{Name}老师，欢迎您加入{EduConfig.EduOrg}的大家庭。";
            //<a href='http://{EduConfig.AppMainSite}/Teacher/Join'>请点击填写些简单资料</a>"
            return msg;
        }

        public static string UserComing(string Name)
        {
            return $"亲爱的{Name}，欢迎回来！您可点击<a href='http://mp.weixin.qq.com/s?__biz=MzU3NDk2NjE1MQ==&mid=100000028&idx=1&sn=4146ed60a3d7d93038b1499bb53926ee&chksm=7d2b11f44a5c98e278a2cf6604b6b703d30f7ad1a06a9a45507b3c912128df49723c9f35646c#'>用户手册查看书院使用说明</a>";
        }
        public static string NewUserLook(string Name)
        {
            return $"您好,{Name},欢迎光临云艺国学教育！";
        }

        public static string NewUserAdd(string Name)
        {
            return $@"您好,{Name},欢迎加入云艺国学教育大家庭，愿我们能带给宝贝们不一样的学习体验！
您可点击<a href='http://mp.weixin.qq.com/s?__biz=MzU3NDk2NjE1MQ==&mid=100000028&idx=1&sn=4146ed60a3d7d93038b1499bb53926ee&chksm=7d2b11f44a5c98e278a2cf6604b6b703d30f7ad1a06a9a45507b3c912128df49723c9f35646c#'>用户手册查看书院使用说明</a>";
        }

    }
}
