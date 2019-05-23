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
    }
}
