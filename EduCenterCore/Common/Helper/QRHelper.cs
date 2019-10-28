using EduCenterCore.EduFramework;
using QRCoder;
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

        //为QR添加Logo
        public static void AddLogoForQR(string logoUrl, Bitmap qrBitmap, string saveFilePath,List<string> text = null, int width = 132, int height = 132)
        {
            var LogoImg = ImgHelper.GetImgFromUrl(logoUrl);
            LogoImg = ImgHelper.resizeImage(LogoImg, new Size(width, height));
           
            LogoImg = ImgHelper.AddImgBorder(new Bitmap(LogoImg), 8, Color.Wheat);
            LogoImg = ImgHelper.DrawTransparentRoundCornerImage(LogoImg, 20);
            Bitmap finImg = ImgHelper.CombineImageToCenter(qrBitmap, LogoImg);

            if(text!=null)
                finImg = AddStringUnderQR(finImg, text);

            finImg.Save(saveFilePath);
            finImg.Dispose();
            LogoImg.Dispose();

        }

        //二维码下面+文字
        public static Bitmap AddStringUnderQR(Image qrImg,List<string> textList,int leftOffset=0)
        {
            if (textList == null) return new Bitmap(qrImg);

            Bitmap bkImg = new Bitmap(qrImg.Width,qrImg.Height+50* textList.Count);
            bkImg = ImgHelper.CombineImageToTop(bkImg, qrImg);
            //添加文字
            using (Graphics g = Graphics.FromImage(bkImg))
            {
                int height = qrImg.Height;
                foreach (string s in textList)
                {
                  
                    Font font = new Font("微软雅黑", 32, GraphicsUnit.Pixel);

                    SolidBrush b = new SolidBrush(Color.Black);

                    g.DrawString(s, font, b, new PointF(2 + leftOffset, height + 2));

                    height += font.Height + 2;
                }
             
            }

            return bkImg;
        }

        //自定义二维码生成
        public static void GenQR(string url,string savefilePath,List<string> desc = null)
        {
            QRCodeGenerator generator = new QRCodeGenerator();

            QRCodeData codeData = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M, true);

            QRCode qrcode = new QRCode(codeData);
        
            Bitmap qrImage = qrcode.GetGraphic(17, Color.Black, Color.White, true);
            if(desc!=null && desc.Count>0)
            {
                qrImage = AddStringUnderQR(qrImage, desc,20);
            }

            qrImage.Save(savefilePath);
            qrImage.Dispose();
           // string savePath = EduEnviroment.DicPath_QRPay+fil
           // MemoryStream ms = new MemoryStream();

            //qrImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            //byte[] bytes = ms.GetBuffer();

            //ms.Close();

            //  return File(bytes, "image/Png");
        }
    }
}
