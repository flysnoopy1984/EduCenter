using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.ArtShow.In
{
    public class InUserPraize
    {
        public string UnionId { get; set; }

        public long KeyId { get; set; }

        public bool IsPraize { get; set; }

        public PraizeTarget PraizeTarget { get; set; }


    }
}
