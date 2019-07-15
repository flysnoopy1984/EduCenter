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
         
            Bitmap finImg = ImgHelper.CombineImageToCenter(bkImg, qrImg);
            finImg.Save(finalFileAddr);
            finImg.Dispose();
            qrImg.Dispose();
        }

        public static void AddLogoForQR(string logoUrl,Bitmap qrBitmap,string saveFilePath,int width=132,int height= 132)
        {
            var LogoImg = ImgHelper.GetImgFromUrl(logoUrl);
            LogoImg = ImgHelper.resizeImage(LogoImg, new Size(width, height));
           
            LogoImg = ImgHelper.AddImgBorder(new Bitmap(LogoImg), 8, Color.Wheat);
            LogoImg = ImgHelper.DrawTransparentRoundCornerImage(LogoImg, 20);
            Bitmap finImg = ImgHelper.CombineImageToCenter(qrBitmap, LogoImg);

            finImg = AddStringUnderQR(finImg, "您的朋友邀请您加入云艺书院");

            finImg.Save(saveFilePath);
            finImg.Dispose();
            LogoImg.Dispose();

        }

        //二维码下面+文字
        public static Bitmap AddStringUnderQR(Image qrImg,string text)
        {
            Bitmap bkImg = new Bitmap(qrImg.Width,qrImg.Height+50);
            bkImg = ImgHelper.CombineImageToTop(bkImg, qrImg);
            //添加文字
            using (Graphics g = Graphics.FromImage(bkImg))
            {
                string s = text;
                Font font = new Font("微软雅黑", 32,GraphicsUnit.Pixel);

                SolidBrush b = new SolidBrush(Color.Black);

                g.DrawString(s, font, b, new PointF(2, qrImg.Height+2));
            }

            return bkImg;
        }
    }
}
