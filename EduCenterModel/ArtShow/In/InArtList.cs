using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.ArtShow.In
{
    public class InArtList
    {
        public string unionIdQuerier { get; set; }
        public string ownUnionId { get; set; }

        public int pageIndex { get; set; }

        public bool showAll { get; set; }

        public int pageSize { get; set; }

     
        public ArtListOrderBy orderby { get; set; }

        public InArtList()
        {
            ownUnionId = null;
            showAll = false;
            pageIndex = 1;
            pageSize = 20;
        }
    }
}
