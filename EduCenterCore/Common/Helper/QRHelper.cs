using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;

namespace EduCenterCore.Common.Helper
{
    public static class QRHelper
    {
        /// <summary>
        /// 为QR添加背景
        /// </summary>
       public static void AddBKForQR(string bkAddr,string QrAddr,string finalFileAddr)
        {
            Bitmap bkImg = new Bitmap(bkAddr);
            Image qrImg = Image.FromFile(QrAddr);
         
            Bitmap finImg = ImgHelper.ImageWatermark(bkImg, qrImg);
            finImg.Save(finalFileAddr);
            finImg.Dispose();
            qrImg.Dispose();

           
        }
    }
}
