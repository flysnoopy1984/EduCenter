﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Common
{
    public class EduException:Exception
    {
        public EduException(string message) : base(message) { }
        //
      
        public EduException(string message, Exception innerException):base(message, innerException) { }

        public EduException() { }
    }
}