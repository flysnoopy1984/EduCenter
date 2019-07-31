using EduCenterCore.EduFramework;
using EduCenterModel.AliPay;
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

        private static EAliPayApplication _AliPayApplication;



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

            _AliPayApplication = srv.GetAliPayApplication();
            _CourseDateRange = srv.GetCourseDateRangeList();
          
        }

        public static EAliPayApplication GetAliPayApplication()
        {
            return _AliPayApplication;
        }

        public static List<ECourseDateRange> CourseDateRange
        {
            get
            {
                return _CourseDateRange;
            }
        }

        public static CourseScheduleType CurrentScheduleType
        {
            get
            {
                ECourseDateRange dr = CourseDateRange.Where(a => a.StartDate <= DateTime.Today &&
           a.EndDate >= DateTime.Today).FirstOrDefault();
                if (dr == null)
                    return CourseScheduleType.Standard;
                else
                    return dr.CourseScheduleType;
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
                    
                    _CourseTime.Add(1,new ECourseTime{Lesson = 1,TimeRange = "09:00-10:30",StartTime=9,EndTime=10.5});
                    _CourseTime.Add(2,new ECourseTime { Lesson = 2, TimeRange = "10:30-12:00",StartTime=10.3,EndTime=12 });
                    _CourseTime.Add(3,new ECourseTime { Lesson = 3, TimeRange = "13:00-14:30", StartTime = 13, EndTime = 14.5 });
                    _CourseTime.Add(4,new ECourseTime { Lesson = 4, TimeRange = "14:30-16:00", StartTime = 14.5, EndTime = 16 });
                    _CourseTime.Add(5,new ECourseTime { Lesson = 5, TimeRange = "16:30-18:00", StartTime = 16.5, EndTime = 18 });
                    _CourseTime.Add(6,new ECourseTime { Lesson = 6, TimeRange = "18:30-20:00", StartTime = 18.5, EndTime = 20 });
                 
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

                    _TrialTime.Add(1, new ECourseTime { Lesson = 1, TimeRange = "09:00-9:45", StartTime = 9, EndTime = 9.45 });
                    _TrialTime.Add(2, new ECourseTime { Lesson = 2, TimeRange = "10:00-10:45", StartTime = 10, EndTime = 10.45 });
                    _TrialTime.Add(3, new ECourseTime { Lesson = 3, TimeRange = "11:00-11:45", StartTime = 11, EndTime = 11.45 });
                    _TrialTime.Add(4, new ECourseTime { Lesson = 4, TimeRange = "12:00-12:45", StartTime = 12, EndTime = 12.45 });
                    _TrialTime.Add(5, new ECourseTime { Lesson = 5, TimeRange = "13:00-13:45", StartTime = 13, EndTime = 13.45 });
                    _TrialTime.Add(6, new ECourseTime { Lesson = 6, TimeRange = "14:00-14:45", StartTime = 14, EndTime = 14.45 });
                    _TrialTime.Add(7, new ECourseTime { Lesson = 7, TimeRange = "15:00-15:45", StartTime = 15, EndTime = 15.45 });
                    _TrialTime.Add(8, new ECourseTime { Lesson = 8, TimeRange = "16:00-16:45", StartTime = 16, EndTime = 16.45 });
                    _TrialTime.Add(9, new ECourseTime { Lesson = 9, TimeRange = "17:00-17:45", StartTime = 17, EndTime = 17.45 });
                    _TrialTime.Add(10, new ECourseTime { Lesson = 10, TimeRange = "18:00-18:45", StartTime = 18, EndTime = 18.45 });
                    _TrialTime.Add(11, new ECourseTime { Lesson = 11, TimeRange = "19:00-19:45", StartTime = 19, EndTime = 19.45 });
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

        public static CourseScheduleType GetSysCurrentCourseScheduleType(DateTime date)
        {
            ECourseDateRange dr = CourseDateRange.Where(a => a.StartDate <= date &&
         a.EndDate >= date).FirstOrDefault();
            if (dr != null)
                return dr.CourseScheduleType;
            else
                return CourseScheduleType.Standard;

        }
    }
}
