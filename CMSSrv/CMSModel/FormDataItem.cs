using System;
using System.Collections.Generic;

namespace CMSSrv.CMSModel
{
    public partial class FormDataItem
    {
        public int Id { get; set; }
        public int FormDataId { get; set; }
        public string FieldId { get; set; }
        public string FieldText { get; set; }
        public string FieldValue { get; set; }
        public string OptionValue { get; set; }
        public string CreateBy { get; set; }
        public string CreatebyName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateByName { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
