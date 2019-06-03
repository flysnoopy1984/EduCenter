using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterCore.EduFramework
{
    public class EduCodeGenerator
    {
        /// <summary>
        /// 根据当前老师数，生产老师编号
        /// </summary>
        /// <param name="tecNum"></param>
        /// <returns></returns>
        public static string GetTecCode(int tecNum)
        {
            string code = tecNum.ToString();
            return code.PadLeft(5, '0');
        }

        public static string GetOrderOrder()
        {
            return "UserCourse_" + DateTime.Now.ToString("yyyyMMddhhmmss") + GetRnd(2, true, true, false, false, "");
        }

        public static string GetRnd(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;

            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }

            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }

            return s;
        }
    }
}
