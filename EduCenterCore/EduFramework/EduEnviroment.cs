
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterCore.EduFramework
{
    public static class EduEnviroment
    {
        public  static IWebHostEnvironment _Enviroment;
        public static void SetEnviroment(IWebHostEnvironment Enviroment)
        {
            _Enviroment = Enviroment;
        }
      
        public static string DicPath_QRInviteTec
        {
            get { return _Enviroment.WebRootPath + @"\Files\QR\InviteTec\"; }
        }

        public static string DicPath_QRInviteUser
        {
            get { return _Enviroment.WebRootPath + @"\Files\QR\InviteUser\"; }
        }

        public static string VirPath_QRInviteUser
        {
            get { return "/files/QR/InviteUser/"; }
        }

       

        public static string DicPath_QR
        {
            get { return _Enviroment.WebRootPath + @"\Files\QR\"; }
        }

        #region 支付码
        public static string VirPath_QRPay
        {
            get { return "/Files/QR/Pay/"; }
        }

        public static string DicPath_QRPay
        {
            get { return _Enviroment.WebRootPath + @"\Files\QR\Pay\"; }
        }
        #endregion

        public static string DicPath_StaticData
        {
            get { return _Enviroment.WebRootPath + @"\Files\StaticData\"; }
        }

        public static string Menus_JsonFilePath
        {
            get { return _Enviroment.WebRootPath + @"\Files\StaticData\menus.json"; }
        }

        public static string GetQRInviteUserFilePath(string fileName)
        {
            return DicPath_QRInviteUser + fileName;
        }

        public static string GetQRInviteTecFilePath(string fileName)
        {
            return DicPath_QRInviteTec + fileName;
        }
        public static string GetQRFilePath(string fileName)
        {
            return DicPath_QR + fileName;
        }

        #region 作品上传
        public static string VirPath_ArtRoot
        {
            get { return "/Files/Art/"; }
        }
        #endregion

        #region Tools 
        public static string DicPath_Tools_LessonQR
        {
            get { return _Enviroment.WebRootPath + @"\Files\QR\Tools\LessonQR\"; }
        }


        public static string VirPath_Tools_LessonQR
        {
            get { return "/Files/QR/Tools/LessonQR/"; }
        }

        #endregion 
    }
}
