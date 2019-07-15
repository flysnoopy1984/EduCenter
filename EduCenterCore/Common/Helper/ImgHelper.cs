using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace EduCenterCore.Common.Helper
{
    public static class ImgHelper
    {
        //将front图片贴在main图片中央
        public static Bitmap CombineImageToCenter(Bitmap MainImg, Image frontImg)
        {

            //添加水印
            using (Graphics g = Graphics.FromImage(MainImg))
            {
                //获取水印位置设置
                ArrayList loca = new ArrayList();
                int x = 0;
                int y = 0;
                x = MainImg.Width / 2 - frontImg.Width / 2;
                y = MainImg.Height / 2 - frontImg.Height / 2;
                loca.Add(x);
                loca.Add(y);


                g.DrawImage(frontImg, new Rectangle(int.Parse(loca[0].ToString()), int.Parse(loca[1].ToString()), frontImg.Width, frontImg.Height));
            }



            return MainImg;
        }

        public static Bitmap CombineImageToTop(Bitmap MainImg, Image frontImg)
        {

            //添加水印
            using (Graphics g = Graphics.FromImage(MainImg))
            {
                //获取水印位置设置
                ArrayList loca = new ArrayList();
                int x = 0;
                int y = 0;
                x = MainImg.Width / 2 - frontImg.Width / 2;
                y = 0;
                loca.Add(x);
                loca.Add(y);


                g.DrawImage(frontImg, new Rectangle(int.Parse(loca[0].ToString()), int.Parse(loca[1].ToString()), frontImg.Width, frontImg.Height));
            }



            return MainImg;
        }

        public static Image GetImgFromUrl(string httpurl)
        {
            try
            {
                System.Net.WebRequest webreq = System.Net.WebRequest.Create(httpurl);
                System.Net.WebResponse webres = webreq.GetResponse();
                System.IO.Stream stream = webres.GetResponseStream();
                Image result = Image.FromStream(stream);
                return result;

            }
            catch (Exception ex)
            {
               
                throw ex;
            }

        }

        public static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            //获取图片宽度
            int sourceWidth = imgToResize.Width;
            //获取图片高度
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //计算宽度的缩放比例
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //计算高度的缩放比例
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //期望的宽度
            int destWidth = (int)(sourceWidth * nPercent);
            //期望的高度
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //绘制图像
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }

       

        public static Image AddImgBorder(Bitmap resultImg, int BorderWidth, Color c)
        {
            try
            {

                float w = BorderWidth;

                using (Graphics g = Graphics.FromImage(resultImg))
                {
                    using (Brush brush = new SolidBrush(c))
                    {
                        using (Pen pen = new Pen(brush, w))
                        {
                            pen.DashStyle = DashStyle.Custom;
                            g.DrawRectangle(pen, new Rectangle(0, 0, Math.Abs(resultImg.Width), Math.Abs(resultImg.Height)));
                            g.Dispose();

                        }
                    }
                }
                return resultImg;
            }
            catch (Exception ex)
            {
              
                throw ex;
            }
        }

        #region 图片圆角处理
        public static System.Drawing.Image DrawTransparentRoundCornerImage(System.Drawing.Image image,int cornerRadius)
        {
            Bitmap bm = new Bitmap(image.Width, image.Height);
            Graphics g = Graphics.FromImage(bm);
            g.FillRectangle(Brushes.Transparent, new Rectangle(0, 0, image.Width, image.Height));

            using (System.Drawing.Drawing2D.GraphicsPath path = CreateRoundedRectanglePath(new Rectangle(0, 0, image.Width, image.Height), cornerRadius))
            {
                g.SetClip(path);
            }

            g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            g.Dispose();

            return bm;
        }

        private static System.Drawing.Drawing2D.GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            System.Drawing.Drawing2D.GraphicsPath roundedRect = new System.Drawing.Drawing2D.GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }
        #endregion
    }
}
