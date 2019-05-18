using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel
{
    public class ECMasterDataModel
    {
        public ECMasterDataModel()
        {
            RecordStatus = RecordStatus.Normal;
            CreatedDateTime = DateTime.MinValue;
            UpdatedDateTime = DateTime.MinValue;
        }
        public RecordStatus RecordStatus { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime UpdatedDateTime { get; set; }
    }
}
