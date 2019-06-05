using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterCore.EduFramework
{
    public static class EduEnviroment
    {
        public  static IHostingEnvironment _Enviroment;
        public static void SetEnviroment(IHostingEnvironment Enviroment)
        {
            _Enviroment = Enviroment;
        }
        public static string DicPath_QRInviteTec
        {
            get { return _Enviroment.WebRootPath + @"\Files\QR\InviteTec\"; }
        }
        public static string DicPath_QR
        {
            get { return _Enviroment.WebRootPath + @"\Files\QR\"; }
        }
        public static string DicPath_StaticData
        {
            get { return _Enviroment.WebRootPath + @"\Files\StaticData\"; }
        }

        public static string GetQRInviteTecFilePath(string fileName)
        {
            return DicPath_QRInviteTec + fileName;
        }
        public static string GetQRFilePath(string fileName)
        {
            return DicPath_QR + fileName;
        }

    }
}
