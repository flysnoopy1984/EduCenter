using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EduCenterModel
{
    public class ECMasterDataModel
    {
        public ECMasterDataModel()
        {
            RecordStatus = RecordStatus.Normal;
            CreatedDateTime = DateTime.MinValue;
            UpdatedDateTime = DateTime.Now;
        }
        public RecordStatus RecordStatus { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime UpdatedDateTime { get; set; }

        [NotMapped()]
        public string CreatedDateTimeStr
        {
            get {
                return CreatedDateTime.ToString("yyyy-MM-dd hh:mm:ss");
            }
        }

        [NotMapped()]
        public string UpdatedDateTimeStr
        {
            get
            {
                return UpdatedDateTime.ToString("yyyy-MM-dd hh:mm:ss");
            }
        }
    }
}
