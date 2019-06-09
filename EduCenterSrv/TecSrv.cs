using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Teacher;
using EduCenterModel.Teacher.Result;
using EduCenterModel.User;
using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EduCenterCore.EduFramework;
using EduCenterModel.BaseEnum;
using EduCenterSrv.Common;

namespace EduCenterSrv
{
    public class TecSrv : BaseSrvMasterData<ETecInfo>
    {
      

        public TecSrv(EduDbContext dbContext):base(dbContext)
        {
           
        }

        #region SQL
        public static string sql_DeleteALLTecSkill(string tecCode)
        {
            string sql = $"delete from TecSkill where TecCode='{tecCode}'";
            return sql;
        }
        #endregion


       
        public List<STec> GetSimpleList()
        {

            return _dbContext.DBTecInfo.Select(a => new STec
            {
                Code = a.Code,
                Name = a.Name,
              

            }).ToList();
        }

        /// <summary>
        /// 获取所有在职老师
        /// </summary>
        /// <returns></returns>
        public List<ETecInfo> GetAllStaffTec()
        {
            return _dbContext.DBTecInfo.Where(a => a.RecordStatus == RecordStatus.Normal).ToList();
        }

        public RTecAllInfo GetTecAllInfo(string code)
        {
            RTecAllInfo result = new RTecAllInfo();
            result.TecInfo = Get(code);
            result.TecSkillList = GetTecSkillList(code);
       
            return result;
        }
        public ETecInfo Get(string code)
        {
            return _dbContext.DBTecInfo.Where<ETecInfo>(a => a.Code == code).FirstOrDefault();
        }

        /// <summary>
        /// 更新基本信息
        /// </summary>
        public void UpdatePartTecInfo(ETecInfo updatedTec,bool needSave =true)
        {
            _dbContext.DBTecInfo.Attach(updatedTec);
            _dbContext.Entry(updatedTec).Property(p => p.Name).IsModified = true;
            _dbContext.Entry(updatedTec).Property(p => p.Phone).IsModified = true;
            if (needSave)
                _dbContext.SaveChanges();
        }

       


        public void NewTecFromUser(EUserInfo user)
        {
            int count =  _dbContext.DBTecInfo.Count(t => t.UserOpenId == user.OpenId);
            if(count ==0)
            {
                int No = _dbContext.DBTecInfo.Count();
                No++;
                //教师信息
                ETecInfo tec = new ETecInfo
                {
                    Sex = user.Sex,
                    Code = EduCodeGenerator.GetTecCode(No),
                    Name = user.Name,
                    UserOpenId = user.OpenId,
                    WxName = user.wx_Name,
                    Phone = user.Phone,
                    CreatedDateTime = DateTime.Now,
                    UpdatedDateTime = DateTime.Now,
                };

                //教师技能
                _dbContext.Database.ExecuteSqlCommand(TecSrv.sql_DeleteALLTecSkill(tec.Code));

                CourseSrv courseSrv = new CourseSrv(this._dbContext);
                var courseList =  courseSrv.GetCourseType();
                foreach(var course in courseList)
                {
                    ETecSkill ts = new ETecSkill
                    {
                        CourseType = (CourseType)course.Key,
                        SkillLevel = SkillLevel.None,
                        TecCode = tec.Code,
                    };
                    
                   _dbContext.DBTecSkill.Add(ts);
                }

                _dbContext.DBTecInfo.Add(tec);
                _dbContext.Database.ExecuteSqlCommand(UserSrv.sql_UpdateUserRole(UserRole.Teacher, user.OpenId));
                _dbContext.SaveChanges();
               
            }
           
        }


        #region 技能
        public List<SiKsV> GetSkillLevelList()
        {
          
            return BaseEnumSrv.SkillLevelList;
        }

        public List<ETecSkill> GetTecSkillList(string tecCode = null)
        {
           
            if(tecCode!=null)
                return _dbContext.DBTecSkill.Where(s => s.TecCode == tecCode).ToList();
            else
                return _dbContext.DBTecSkill.ToList();
        }

        public List<ETecSkill> GetTecAvaliableSkill()
        {
            return _dbContext.DBTecSkill.Where(s => s.SkillLevel != SkillLevel.None).ToList();
        }

        public void UpdateTecSkillLevel(ETecSkill tecSkill,bool needSave = true)
        {
            _dbContext.DBTecSkill.Attach(tecSkill);
            _dbContext.Entry(tecSkill).Property(p => p.SkillLevel).IsModified = true;
            if (needSave)
                _dbContext.SaveChanges();
        }

        #endregion

        #region TecCourse

        public List<RTecCourse> GetOneDayCourse(string tecCode,DateTime date,CourseScheduleType CourseScheduleType)
        {
            var times = StaticDataSrv.CourseTime;
            var linq = _dbContext.DBTecCourse.Select(a => new RTecCourse
            {
                TimeRange = times[a.Lesson].TimeRange,
                CourseName = a.CourseName,
                TecCode = a.TecCode,
                CourseDateTime = a.CourseDateTime,
                CourseScheduleType = a.CourseScheduleType,
                Lesson  =a.Lesson,
                CoursingStatus = a.CoursingStatus,
                LessonCode = a.LessonCode
              

            })
            .Where(a => a.TecCode == tecCode &&
                        a.CourseDateTime.Date == date.Date &&
                        a.CourseScheduleType == CourseScheduleType);

            var result = linq.ToList();
               
         
            return result;
        }
      
        public List<RTecCourse> GetTecCourse(string tecCode, CourseScheduleType CourseScheduleType,int year,int month)
        {
            var times = StaticDataSrv.CourseTime;
            var result = _dbContext.DBTecCourse.Select(tc => new RTecCourse
            {
                Day = tc.Day,
                CourseName = tc.CourseName,
                CourseDateTime = tc.CourseDateTime,
                CoursingStatus = tc.CoursingStatus,
                CourseScheduleType = tc.CourseScheduleType,
                TecCode = tc.TecCode,
                TimeRange = times[tc.Lesson].TimeRange,
            })
            .Where(a => a.CourseDateTime.Year == year &&
                    a.CourseDateTime.Month == month &&
                    a.TecCode == tecCode &&
                    a.CourseScheduleType == CourseScheduleType).ToList();

            return result;
        }

        #endregion


    }
}
