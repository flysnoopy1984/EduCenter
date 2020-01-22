using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class PageView
    {
        public int Id { get; set; }
        public string PageUrl { get; set; }
        public string PageTitle { get; set; }
        public string Ipaddress { get; set; }
        public string SessionId { get; set; }
        public string UserAgent { get; set; }
        public string Referer { get; set; }
        public string RefererName { get; set; }
        public string KeyWords { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
