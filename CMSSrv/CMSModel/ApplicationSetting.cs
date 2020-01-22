using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class ApplicationSetting
    {
        public string SettingKey { get; set; }
        public string Value { get; set; }
        public string Title { get; set; }
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
