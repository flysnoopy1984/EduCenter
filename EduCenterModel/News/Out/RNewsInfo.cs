using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.News.Out
{
    public class RNewsInfo:ENewsInfo
    {
        public string CreateTimeStr
        {
            get { return CreateDateTime.ToString("yyyy-MM-dd hh:mm"); }
        }
    }
}
