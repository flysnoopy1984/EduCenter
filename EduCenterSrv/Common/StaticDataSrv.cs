using EduCenterCore.EduFramework;
using EduCenterModel.BaseEnum;
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
        private static Dictionary<int,Dictionary<int, Dictionary<int, EHoliday>>> _Holiday;
        private static Dictionary<int,ECourseTime> _CourseTime;
        private static Dictionary<int, ECourseTime> _TrialTime;
        private static Dictionary<int, int> _CourseMaxApplyNum;

        private static List<ECourseDateRange> _CourseDateRange;
     
        public static void Init()
        {
            _CourseTime = CourseTime;
          //  _Holiday = Holidays;
            _CourseMaxApplyNum = CourseMaxApplyNum;
        }

        /// <summary>
        /// 全局数据库数据初始化
        /// </summary>
        /// <param name="srv"></param>
        public static void InitDbData(EduDbContext db)
        {
            GlobalSrv srv = new GlobalSrv(db);

            _CourseDateRange = srv.GetCourseDateRangeList();
          
        }

    
        public static List<ECourseDateRange> CourseDateRange
        {
            get
            {
                return _CourseDateRange;
            }
        }
        public static Dictionary<int,int> CourseMaxApplyNum
        {
            get
            {
                if(_CourseMaxApplyNum == null)
                {
                    _CourseMaxApplyNum = new Dictionary<int, int>();
                    _CourseMaxApplyNum.Add((int)CourseType.MS, 10);
                    _CourseMaxApplyNum.Add((int)CourseType.SF, 10);
                    _CourseMaxApplyNum.Add((int)CourseType.WQ, 10);
                }
                return _CourseMaxApplyNum;
                    
            }
        }
        public static Dictionary<int, ECourseTime> CourseTime
        {
            get
            {
                if (_CourseTime == null)
                {
                    _CourseTime = new Dictionary<int, ECourseTime>();
                    
                    _CourseTime.Add(1,new ECourseTime{Lesson = 1,TimeRange = "09:00-10:30",StartTime=9,EndTime=10.3});
                    _CourseTime.Add(2,new ECourseTime { Lesson = 2, TimeRange = "10:30-12:00",StartTime=10.3,EndTime=12 });
                    _CourseTime.Add(3,new ECourseTime { Lesson = 3, TimeRange = "13:00-14:30", StartTime = 13, EndTime = 14.3 });
                    _CourseTime.Add(4,new ECourseTime { Lesson = 4, TimeRange = "14:30-16:00", StartTime = 14.3, EndTime = 16 });
                    _CourseTime.Add(5,new ECourseTime { Lesson = 5, TimeRange = "16:30-18:00", StartTime = 16.3, EndTime = 18 });
                    _CourseTime.Add(6,new ECourseTime { Lesson = 6, TimeRange = "18:30-20:00", StartTime = 18.3, EndTime = 20 });
                 
                }
                   
                return _CourseTime;
            }
        }

        public static Dictionary<int, ECourseTime> TrialTime
        {
            get
            {
                if (_TrialTime == null)
                {
                    _TrialTime = new Dictionary<int, ECourseTime>();

                    _TrialTime.Add(1, new ECourseTime { Lesson = 1, TimeRange = "09:00-10:00", StartTime = 9, EndTime = 10 });
                    _TrialTime.Add(2, new ECourseTime { Lesson = 2, TimeRange = "10:00-11:00", StartTime = 10, EndTime = 11 });
                    _TrialTime.Add(3, new ECourseTime { Lesson = 3, TimeRange = "11:00-12:00", StartTime = 11, EndTime = 12 });
                    _TrialTime.Add(4, new ECourseTime { Lesson = 4, TimeRange = "13:00-14:00", StartTime = 13, EndTime = 14 });
                    _TrialTime.Add(5, new ECourseTime { Lesson = 5, TimeRange = "14:00-15:00", StartTime = 14, EndTime = 15 });
                    _TrialTime.Add(6, new ECourseTime { Lesson = 6, TimeRange = "15:00-16:00", StartTime = 15, EndTime =16 });

                }

                return _TrialTime;
            }
        }



        public static Dictionary<int, Dictionary<int, Dictionary<int, EHoliday>>> Holidays
        {
            get
            {
                if (_Holiday == null)
                {
                    _Holiday = new Dictionary<int, Dictionary<int, Dictionary<int, EHoliday>>>();
                    _Holiday.Add(2019, new Dictionary<int, Dictionary<int, EHoliday>>());
                    _Holiday.Add(2020, new Dictionary<int, Dictionary<int, EHoliday>>());
                    _Holiday.Add(2021, new Dictionary<int, Dictionary<int, EHoliday>>());
                    _Holiday.Add(2022, new Dictionary<int, Dictionary<int, EHoliday>>());

                    var FileName = $"Holiday.json";
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
                                    _Holiday[holiday.Year][holiday.Month][holiday.Day] = holiday;
                                }
                                catch
                                {
                                    try
                                    {
                                        _Holiday[holiday.Year][holiday.Month].Add(holiday.Day, holiday);
                                    }
                                    catch
                                    {
                                        _Holiday[holiday.Year].Add(holiday.Month, new Dictionary<int, EHoliday>());
                                        _Holiday[holiday.Year][holiday.Month] = new Dictionary<int, EHoliday>();
                                        _Holiday[holiday.Year][holiday.Month].Add(holiday.Day, holiday);

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
