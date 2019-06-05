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




    }
}
