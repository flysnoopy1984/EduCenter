using EduCenterModel.BaseEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Common
{
    public class EduException:Exception
    {
        public EduException(string message) : base(message) { }

        public EduException(string message, EduErrorMessage eduErrorMessage) : base(message) {
            EduErrorMessage = eduErrorMessage;
        }

        public EduException(string message, Exception innerException):base(message, innerException) { }

        public EduException() { }

        public EduErrorMessage EduErrorMessage { get; set; }
    }
}
