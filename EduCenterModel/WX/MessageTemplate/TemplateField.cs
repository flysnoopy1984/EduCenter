﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduCenterModel.WX.MessageTemplate
{
    public class TemplateField
    {
        public TemplateField()
        {
            color = "#000000";
        }
        public string value { get; set; }
        public string color { get; set; }
    }
}