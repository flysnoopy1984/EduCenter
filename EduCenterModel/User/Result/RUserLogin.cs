using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.User.Result
{
    public class RUserLogin:EUserLogin
    {
        public string Name { get; set; }

        public string HeaderUrl { get; set; }

        public UserRole UserRole { get; set; }

        public string EffectDateStr
        {
            get { return EffectDate.ToString("yyyy-MM-dd hh:mm:ss"); }
        }   
    }
}
