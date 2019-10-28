using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.ArtShow.Out
{
    public class RArtInfo: EArtInfo
    {
        public string UploadDateTimeStr
        {
            get { return UploadDateTime.ToString("yyyy-MM-dd hh:mm"); }
        }

        public string UploaderHeaderUrl { get; set; }

        /// <summary>
        /// 查询人是否点过赞（需要前端传入当前查询人）
        /// </summary>
        public bool HasPriaize { get; set; }
    }
}
