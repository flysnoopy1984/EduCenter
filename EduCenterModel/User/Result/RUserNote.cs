using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.User.Result
{
    public class RUserNote:EUserNote
    {
        public bool CanDelete { get; set; }

        public string CreatedDateTimeStr {
            get
            {
                return CreateDateTime.ToString("yyyy-MM-dd hh:mm");
            }
        }
    }
}
