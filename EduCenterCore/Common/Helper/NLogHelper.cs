
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterCore.Common.Helper
{
    public static class NLogHelper
    {
        private static Logger _FileLogger = LogManager.GetLogger("InfoNLog");
        private static Logger _FileErrorLogger = LogManager.GetLogger("ErrorNLog");

      

        public static void InfoTxt(string txt)
        {
            try
            {
                _FileLogger.Info(txt);
            }
            catch
            {

            }
        }

        public static void ErrorTxt(string txt)
        {
            try
            {

                _FileErrorLogger.Error(txt);
            }
            catch { 

            }
        }

       

    }
}
