using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.ArtShow.In
{
    public class InArtInfo
    {
       
        public string unionId { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public CourseType courseType { get; set; }
        public int coverIndex { get; set; }

        public ArtMediaType ArtMediaType { get; set; }
    }
}
