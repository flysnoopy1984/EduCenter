using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterSrv.Common
{
    public class DateSrv
    {
        public static int GetDayOfWeek(DateTime date)
        {
            int curDay = (int)date.DayOfWeek;
            if (curDay == 0)
                curDay = 7;
            return curDay;
        }
        public static DateTime GetNextCourseDate(int day)
        {
            DateTime courseDate = DateTime.Now;
            int curDay = GetDayOfWeek(courseDate);

            if (day> curDay)
                courseDate = courseDate.AddDays(day - curDay);
            else
            {
                int diff = 7 - (day - curDay);
                courseDate = courseDate.AddDays(diff);
            }
          
            //考虑是否节假日
            while (IsHoliday(courseDate))
            {
                courseDate = courseDate.AddDays(7);
            }
            //考虑是否节假日

            return courseDate;
        }
        //public static DateTime GetLastCourseDate(int day)
        //{
        //    DateTime result = DateTime.Now;
        //    int curDay = GetDayOfWeek(result);

        //    if (curDay > day)
        //        result = result.AddDays(day - curDay);
        //    else
        //    {
        //        int diff = 7-(day - curDay);
        //        result= result.AddDays(-diff);
        //    }
        //    //考虑是否节假日
        //    while(IsHoliday(result))
        //    {
        //        result = result.AddDays(-7);
        //    }
        //    //考虑老师是否请假

        //    return result;
        //}

        public static bool IsHoliday(DateTime date)
        {
            try
            {
                var holiday = StaticDataSrv.Holidays[date.Year][date.Month][date.Day];
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public static int GetSysDayOfWeek(DateTime date)
        {
            int day = (int)date.DayOfWeek;
            if (day == 0) day = 7;
            return day;
        }
    }
}
