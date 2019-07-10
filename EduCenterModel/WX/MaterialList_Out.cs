using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.WX
{

    public class MaterialItem
    {
        public string media_id { get; set; }

        public MaterialItem_Content content { get; set; }
    }

    public class MaterialItem_Content
    {
        public MaterialItem_Content()
        {
            news_item = new List<news_item>();
        }
        public List<news_item> news_item { get; set; }
    }

    public class news_item
    {
        public string title { get; set; }
    }
    public class MaterialList_Out
    {
        public MaterialItem item { get; set; }
    }
}
