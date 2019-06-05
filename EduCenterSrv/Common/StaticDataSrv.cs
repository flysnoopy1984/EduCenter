using EduCenterCore.EduFramework;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterSrv.DataBase;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace EduCenterSrv.Common
{
    public static class StaticDataSrv
    {
        private static Dictionary<int, Dictionary<int, EHoliday>> _Holiday;
        private static List<ECourseTime> _CourseTime;

        public static void Init()
        {
            _CourseTime = CourseTime;
            _Holiday = Holidays;
        }
        public static List<ECourseTime> CourseTime
        {
            get
            {
                if (_CourseTime == null)
                {
                    _CourseTime = new List<ECourseTime>();
                    _CourseTime.Add(new ECourseTime{Lesson = 1,TimeRange = "9:00-10:30"});
                    _CourseTime.Add(new ECourseTime { Lesson = 2, TimeRange = "10:30-12:00" });
                    _CourseTime.Add(new ECourseTime { Lesson = 3, TimeRange = "13:00-14:30" });
                    _CourseTime.Add(new ECourseTime { Lesson = 4, TimeRange = "14:30-16:00" });
                    _CourseTime.Add(new ECourseTime { Lesson = 5, TimeRange = "16:30-18:00" });
                    _CourseTime.Add(new ECourseTime { Lesson = 6, TimeRange = "18:30-20:00" });
                 
                }
                   
                return _CourseTime;
            }
        }

        public static Dictionary<int, Dictionary<int, EHoliday>> Holidays
        {
            get
            {
                if (_Holiday == null)
                {
                    _Holiday = new Dictionary<int, Dictionary<int, EHoliday>>();
                    var FileName = $"{DateTime.Now.Year}Holiday.json";
                    string path = EduEnviroment.DicPath_StaticData + FileName;
                    FileInfo fi = new FileInfo(path);
                    FileStream fs = fi.Open(FileMode.Open);

                    try
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            string json = sr.ReadToEnd();
                            var list = JsonConvert.DeserializeObject<List<EHoliday>>(json).OrderBy(a => a.Month).ThenBy(a => a.Day);
                            foreach (var holiday in list)
                            {
                                try
                                {
                                    _Holiday[holiday.Month][holiday.Day] = holiday;
                                }
                                catch
                                {
                                    try
                                    {
                                        _Holiday[holiday.Month].Add(holiday.Day, holiday);
                                    }
                                    catch
                                    {
                                        _Holiday.Add(holiday.Month, new Dictionary<int, EHoliday>());
                                        _Holiday[holiday.Month] = new Dictionary<int, EHoliday>();
                                        _Holiday[holiday.Month].Add(holiday.Day, holiday);

                                    }
                                }
                                    
                            }
                        }
                    }
                    finally
                    {
                        fs.Close();
                        fs.Dispose();
                    }

                }

                return _Holiday;
            }
           
        }
    }
}
