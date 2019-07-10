using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduCenterModel.WX.Media
{
    public class JOMedia_Item
    {
        public string media_id { get; set; }
        public string name { get; set; }

        public string update_time { get; set; }
        public string url { get; set; }

        public Content content { get; set; }


    }
}
