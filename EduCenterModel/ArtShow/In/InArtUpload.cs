using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.ArtShow.In
{
    public class InArtUpload
    {
        public InArtUpload()
        {
            isLast = false;
        }

        public long artId { get; set; }

        public int no { get; set; }

        public ArtMediaType ArtMediaType { get; set; }

        public bool isLast { get; set; }
    }
}
