using System;
using System.Collections.Generic;
using System.Text;

namespace EduCenterSrv.Common
{
    public class DateSrv
    {
        public static string toNormalDate(DateTime date)
        {
            return date.ToString("yyyy-MM-dd hh:mm:ss");
        }
        public static int GetDayOfWeek(DateTime date)
        {
            int curDay = (int)date.DayOfWeek;
            if (curDay == 0)
                curDay = 7;
            return curDay;
        }

        public static double GetLessonHour(DateTime date)
        {
            return date.Hour + date.Minute/100;
        }
        public static DateTime GetNextCourseDate(int day)
        {
            DateTime courseDate = DateTime.Now;
            int curDay = GetDayOfWeek(courseDate);

            if (day> curDay)
                courseDate = courseDate.AddDays(day - curDay);
            else
            {
                int diff = 7 - (curDay- day );
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

        public static DateTime FindFirstWorkDayAfterHoliday(DateTime date)
        {
            while (IsHoliday(date))
            {
                date = date.AddDays(1);
            }
                return date;
        }

        public static int GetSysDayOfWeek(DateTime date)
        {
            int day = (int)date.DayOfWeek;
            if (day == 0) day = 7;
            return day;
        }

        /// <summary>
        /// 给前端查看的时间(和当前时间比，如果一分钟内，则显示刚刚，1天内的都显示24小时内)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string DateTimeForClient(DateTime date)
        {
            //dt2 - dt1的差额
            int sec = GetTimeDiff(date);


            if (sec < 60)
                return "刚刚";
            else if (sec > 60 && sec < 60 * 60)
                return $"{ sec / 60} 分钟前";
            else if (sec > 60 * 60 && sec < 60 * 60 * 24)
                return $"{sec / 3600} 小时前";

            return date.ToString("MM月dd日 hh:mm");
        }

        public static int GetTimeDiff(DateTime date)
        {
            TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts2 = new TimeSpan(date.Ticks);
            TimeSpan ts3 = ts1.Subtract(ts2).Duration();
            int sec = Convert.ToInt32(ts3.TotalSeconds);
            return sec;
        }

        public static DateTime ConverTimeStamp(long timeStamp)
        {
            DateTime dtStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1),TimeZoneInfo.Local);
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);

        }
    }
}
