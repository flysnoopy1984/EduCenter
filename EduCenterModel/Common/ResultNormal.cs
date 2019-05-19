using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Common
{
    public class ResultNormal
    {
        public ResultNormal()
        {
            IsSuccess = true;
            SuccessMsg = "成功";
        }

        public bool IsSuccess { get; set; }

        private string _ErrorMsg;
        public string ErrorMsg
        {
            get { return _ErrorMsg; }
            set
            {
                IsSuccess = false;
                _ErrorMsg = value;
                SuccessMsg = "";
            }
        }

        public string SuccessMsg { get; set; }


        public int IntMsg { get; set; }
    }
}
