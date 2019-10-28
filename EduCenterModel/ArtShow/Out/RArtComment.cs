using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.ArtShow.Out
{
    public class RArtComment:EArtComment
    {
        public string ClientDateTimeStr { get; set; }

        public string HeaderUrl { get; set; }

        public string CommentName { get; set; }

        /// <summary>
        /// 查询人是否点过赞（需要前端传入当前查询人）
        /// </summary>
        public bool HasPriaize { get; set; }
    }
}
