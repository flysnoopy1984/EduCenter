using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Course.Result;
using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EduCenterSrv.Common;

namespace EduCenterSrv
{
    public class CourseSrv: BaseSrvMasterData<ECourseInfo>
    {
      //  private EduDbContext _dbContext;
        public CourseSrv(EduDbContext dbContext):base(dbContext)
        {
            
        }

        #region SQL
        public static string sql_DeleteCourseByType(CourseType type)
        {
            string sql = $"delete from CourseInfo where CourseType={(int)type}";
            return sql;
        }
        public static string sql_DeleteCourseSchedule(int year)
        {
            string sql = $"delete from CourseSchedule where Year={(int)year}";
            return sql;
        }

        public static string sql_DeleteCourseClassByType(CourseType type)
        {
            string sql = $@"delete from CourseInfoClass  
                            from CourseInfo as ci
                            where ci.CourseType = {(int)type} and CourseInfoClass.CourseCode = ci.Code";
            return sql;
        }

        public static string sql_UpdateTrialLogStatus(long Id,TrialLogStatus status)
        {
            string sql = $@"update [TrialLog] 
                            set TrialLogStatus = {(int)status}
                            where Id={Id}";
            return sql;
        }
        #endregion

        public List<SiKsV> GetCourseType()
        {
            
           return BaseEnumSrv.CourseTypeList;
        }

        /// <summary>
        /// 主数据调用
        /// </summary>
        /// <returns></returns>
        public List<ECourseInfo> GetAllList()
        {
            
           return  _dbContext.Set<ECourseInfo>().OrderBy(a=>a.CourseType).ToList();
        
        }

        public List<ECourseInfo> GetAllByType(CourseType courseType)
        {
            return _dbContext.Set<ECourseInfo>().Where(a => a.CourseType == courseType).ToList();
        }

        public void DelByType(CourseType courseType)
        {
            _dbContext.Database.ExecuteSqlCommand(sql_DeleteCourseByType(courseType));

            _dbContext.Database.ExecuteSqlCommand(sql_DeleteCourseClassByType(courseType));
        }

        /// <summary>
        /// 老师技能列表
        /// </summary>
        /// <returns></returns>
        public List<SCourse> GetSimpleList()
        {
            return _dbContext.DBCourseInfo.Select(a => new SCourse
            {
                Code = a.Code,
                Name = a.Name,
                RecordStatus = a.RecordStatus,

            }).Where(a=>a.RecordStatus == RecordStatus.Normal).ToList();
        }

        public ECourseInfo Get(string pk)
        {
            return _dbContext.DBCourseInfo.Where<ECourseInfo>(a => a.Code == pk).FirstOrDefault();
        }


        public void Delete(string Code)
        {
            ECourseInfo delObj = new ECourseInfo
            {
                Code = Code,
            };

            this.Delete(delObj);
        }

        #region CourseClass
        public List<ECourseInfoClass> GetCourseClassList()
        {
            return _dbContext.DBCourseInfoClass.ToList();

        }

        public RCourseInfoClass GetCourseInfoClass(string courseCode,string className= "1班")
        {
            RCourseInfoClass result;
            var efSql = from cls in _dbContext.DBCourseInfoClass
                        join tec in _dbContext.DBTecInfo on cls.TecCode equals tec.Code
                        join ci in _dbContext.DBCourseInfo on  cls.CourseCode equals ci.Code
                        where cls.CourseCode == courseCode && cls.ClassName == className
                        select new RCourseInfoClass
                        {
                            ClassName = className,
                            CourseName = ci.Name,
                            CourseCode = cls.CourseCode,
                            TecName = tec.Name,
                            TecCode = tec.Code,
                            CourseType = ci.CourseType,
                        };
            result = efSql.FirstOrDefault();
            return result;

        }

       




        public void CreateOrUpdateClass(ECourseInfoClass cls,bool needSave=false)
        {
            var obj = _dbContext.DBCourseInfoClass.Where(a => a.Id == cls.Id).FirstOrDefault();
            if (obj == null)
                _dbContext.DBCourseInfoClass.Add(cls);
            else
            {
                obj.TecCode = cls.TecCode;
            }
            if (needSave)
                _dbContext.SaveChanges();
        }
        #endregion

        #region CoureseSchedule

        public void DeleteCourseSchduleByYear(int year)
        {
            _dbContext.Database.ExecuteSqlCommand(sql_DeleteCourseSchedule(year));
        }

        public void AddRange(List<ECourseSchedule> list)
        {
            _dbContext.DbCourseSchedule.AddRange(list);
        }

        public List<ECourseSchedule> GetCourseScheduleByYearType(int year, CourseScheduleType scheduleType)
        {
            return _dbContext.DbCourseSchedule.Where(a => a.Year == year && a.CourseScheduleType == scheduleType).ToList();
        }

        public ECourseSchedule GetCourseSchedule(string LessonCode)
        {
            return _dbContext.DbCourseSchedule.Where(a => a.LessonCode == LessonCode).FirstOrDefault();
        }


        // public DateTime GetLast

        #endregion

        #region CoursePrice
        public List<ECoursePrice> GetCoursePriceList(bool onlyAvaliable=true)
        {
           return _dbContext.DBCoursePrice.Where(a => a.RecordStatus == RecordStatus.Normal && a.EffectEndDate>DateTime.Now).ToList();
        }

        public ECoursePrice GetStandPrice()
        {
            return _dbContext.DBCoursePrice.Where(a => a.RecordStatus == RecordStatus.Normal && a.CourseScheduleType == CourseScheduleType.Standard).FirstOrDefault();
        }

        #endregion

        #region TrialLog
        public List<ETrialLog> QueryTrialLogList(string openId,string CourseCode=null, string date=null)
        {

            var sql = _dbContext.DBTrialLog.
                   Where(a=>a.OpenId == openId);
            if (CourseCode != null)
                sql = sql.Where(a => a.CourseCode == CourseCode);
            if (date != null)
                sql = sql.Where(a => a.TrialDateTime.ToString("yyyy-MM-dd") == date);
            sql = sql.OrderByDescending(a => a.ApplyDateTime);
            return sql.ToList();
        }

        public List<RTrialLog> QueryTrialLogList_BackEnd(string fromDate,string toDate,out int RecordTotal,string tecCode=null, int pageIndex = 0,int pageSize =20)
        {
            List<RTrialLog> result = null;
            var sql = _dbContext.DBTrialLog.
                  Where(a => a.TrialDateTime>= DateTime.Parse(fromDate) && a.TrialDateTime <=DateTime.Parse(toDate));
            if(!string.IsNullOrEmpty(tecCode))
            {
                sql = sql.Where(a => a.TecCode == tecCode);
            }
            RecordTotal = sql.Count();

            sql = sql.OrderByDescending(a => a.TrialDateTime);
            var times = StaticDataSrv.TrialTime;
            result = sql.Select(a => new RTrialLog
            {
                Id = a.Id,
                ApplyDateTime = a.ApplyDateTime,
                TrialDateTime = a.TrialDateTime,
                CourseCode = a.CourseCode,
                CourseName = a.CourseName,
                TecCode = a.TecCode,
                TecName = a.TecName,
                OpenId = a.OpenId,
                UserName = a.UserName,
                TrialLogStatus = a.TrialLogStatus,
                TrialLogStatusName = BaseEnumSrv.GetTrialLogStatusName(a.TrialLogStatus),
                TrialTimeStr = times[a.Lesson].TimeRange,


            }).Skip((pageIndex-1) * pageSize).Take(pageSize).ToList();
          
           
            return result;
        }

        public void  UpdateTrialStatus(ETrialLog log)
        {
            _dbContext.DBTrialLog.Update(log);
        }

        public void UpdateTrialStatus(long Id,TrialLogStatus status)
        {
            var sql = sql_UpdateTrialLogStatus(Id, status);
            _dbContext.Database.ExecuteSqlCommand(sql);
        }


        public void AddTrial(ETrialLog log)
        { 
            _dbContext.DBTrialLog.Add(log);
        }

        

        public EduErrorMessage VerifyUserTrial(string openId, int courseType = -1,string date =null,int lesson =-1)
        {
            if(courseType == -1)
            {
                int c = _dbContext.DBTrialLog.Where(a => a.OpenId == openId && (int)a.TrialLogStatus >= 10).Count();
                //获取所有类型，每个类型试听2次
                var bl = Enum.GetValues(typeof(CourseType)).Length;
                if (c >= bl * 2)
                    return EduErrorMessage.ApplyTrial_OverAllLimit;
            }
            else if(courseType>0)
            {
                int c = _dbContext.DBTrialLog.Where(a => a.OpenId == openId &&
                                            (int)a.TrialLogStatus >= 10 &&
                                               (int)a.CourseType == courseType).Count();
                if (c >= 2) return EduErrorMessage.ApplyTrial_OverSingleLimit;
            }
          
            if(date!=null)
            {
                 var list = _dbContext.DBTrialLog.
                 Where(a => a.TrialDateTime.ToString("yyyy-MM-dd") == date &&
                     a.Lesson == lesson &&
                     a.OpenId == openId).GroupBy(a=>a.Lesson).ToList();
               
                foreach ( var l in list)
                {
                    if(l.Count()>0)
                     return EduErrorMessage.ApplyTrial_SameTypeExist;
                }
               
            }
           
            return EduErrorMessage.NoError;
         
            
        }
        #endregion




    }
}
