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
        public List<ETrialLog> GetTrialLogList(string openId,string date,string CourseCode)
        {
            return _dbContext.DBTrialLog.
                   Where(a => a.CourseCode == CourseCode &&
                         a.TrialDateTime.ToString("yyyy-MM-dd") == date &&
                         a.OpenId == openId
                         ).ToList();
        }
     
        public void AddTrial(ETrialLog log)
        { 
            _dbContext.DBTrialLog.Add(log);
        }

        public EduErrorMessage VerifyUserTrial(string openId, string courseCode = null,string date =null)
        {
            if(courseCode == null)
            {
                int c = _dbContext.DBTrialLog.Where(a => a.OpenId == openId && (int)a.TrialLogStatus >= 10).Count();
                //获取所有类型，每个类型试听2次
                var bl = Enum.GetValues(typeof(CourseType)).Length;
                if (c >= bl * 2) return EduErrorMessage.ApplyTrial_OverAllLimit;
            }
            else
            {
                if(date == null)
                {
                    int c = _dbContext.DBTrialLog.Where(a => a.OpenId == openId &&
                                             (int)a.TrialLogStatus >= 10 &&
                                                  a.CourseCode == courseCode).Count();
                    if (c >= 2) return EduErrorMessage.ApplyTrial_OverSingleLimit;
                }
                else
                {
                    int c = _dbContext.DBTrialLog.
                    Where(a => a.CourseCode == courseCode &&
                        a.TrialDateTime.ToString("yyyy-MM-dd") == date &&
                        a.OpenId == openId).Count();
                    if (c > 0) return EduErrorMessage.ApplyTrial_Exist;
                }
              
            }
            return EduErrorMessage.NoError;
         
            
        }
        #endregion




    }
}
