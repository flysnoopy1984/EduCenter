using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace EduCenterCore.Common.Helper
{
    public static class ImgHelper
    {
        public static Bitmap ImageWatermark(Bitmap MainImg, Image waterimg)
        {

            //添加水印
            using (Graphics g = Graphics.FromImage(MainImg))
            {
                //获取水印位置设置
                ArrayList loca = new ArrayList();
                int x = 0;
                int y = 0;
                x = MainImg.Width / 2 - waterimg.Width / 2;
                y = MainImg.Height / 2 - waterimg.Height / 2;
                loca.Add(x);
                loca.Add(y);


                g.DrawImage(waterimg, new Rectangle(int.Parse(loca[0].ToString()), int.Parse(loca[1].ToString()), waterimg.Width, waterimg.Height));
            }



            return MainImg;
        }
    }
}
