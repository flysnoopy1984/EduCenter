using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterModel.Common
{
    public class ResultObject<T> where T : class
    {
        public ResultObject()
        {
            IsSuccess = true;
            SuccessMsg = "成功";
        }

        public T Entity { get; set; }

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
