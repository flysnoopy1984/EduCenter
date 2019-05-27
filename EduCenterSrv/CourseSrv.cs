﻿using EduCenterModel.BaseEnum;
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
        #endregion

        public List<SiKsV> GetCourseType()
        {
            BaseEnumSrv baseSrv = new BaseEnumSrv();
           return baseSrv.GetCourseType();
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
        #region CoureseSchedule

        public void DeleteCourseSchduleByYear(int year)
        {
            _dbContext.Database.ExecuteSqlCommand(sql_DeleteCourseSchedule(year));
        }

        public void AddRange(List<ECourseSchedule> list)
        {
            _dbContext.DbCourseSchedule.AddRange(list);
        }

        public List<ECourseSchedule> GetCourseScheduleByYear(int year)
        {
            return _dbContext.DbCourseSchedule.Where(a => a.Year == year).ToList();
        }

        #endregion


    }
}
