
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

        private static Logger _ConsoleInfoLogger = LogManager.GetLogger("ConsoleInfoNLog");
        private static Logger _ConsoleErrorLogger = LogManager.GetLogger("ConsoleErrorNLog");


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

        public static void ConsoleInfo(string txt)
        {
            try
            {
                _ConsoleInfoLogger.Info(txt);
            }
            catch
            {

            }
        }

        public static void ConsoleError(string txt)
        {
            try
            {

                _ConsoleErrorLogger.Error(txt);
            }
            catch
            {

            }
        }



    }
}
