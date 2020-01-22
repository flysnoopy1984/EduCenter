using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class DataDictionary
    {
        public int Id { get; set; }
        public string DicName { get; set; }
        public string Title { get; set; }
        public string DicValue { get; set; }
        public int? Order { get; set; }
        public int? Pid { get; set; }
        public bool IsSystem { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbUrl { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
