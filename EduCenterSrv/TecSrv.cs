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
        public static string sql_UpdateTecCourseLeaveBatch(List<long> list)
        {
            string ids="";
            for(int i=0;i<list.Count;i++)
            {
                ids += list[i].ToString();
                if ((i + 1) < list.Count)
                    ids += ",";
            }
            string sql = $"update TecCourse set CoursingStatus = 1,ApplyLeaveDateTime='{DateTime.Now}' where Id in ({ids})";
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
                Lesson = tc.Lesson,
                TimeRange = times[tc.Lesson].TimeRange,
            })
            .OrderBy(a => a.Lesson)
            .Where(a => a.CourseDateTime.Year == year &&
                    a.CourseDateTime.Month == month &&
                    a.TecCode == tecCode &&
                    a.CourseScheduleType == CourseScheduleType).ToList();

            return result;
        }

       

        public void UpdateTecCourse(string tecCode, ECourseSchedule courseSchedule)
        {
            var time = StaticDataSrv.CourseTime[courseSchedule.Lesson];

            int tcNum = _dbContext.DBTecCourse.Where(a => a.TecCode == tecCode && a.LessonCode == courseSchedule.LessonCode).Count();

            if (tcNum == 0)
            {
                DateTime startDate = DateTime.Now;
                int dayofWeek = DateSrv.GetSysDayOfWeek(startDate);
                if (courseSchedule.Day - dayofWeek > 0)
                    startDate = startDate.AddDays(courseSchedule.Day - dayofWeek);
                else
                    startDate = startDate.AddDays(7 - (dayofWeek - courseSchedule.Day));

                DateTime endDate = new DateTime(startDate.Year, 12, 31);
                while (startDate <= endDate)
                {
                    dayofWeek = DateSrv.GetSysDayOfWeek(startDate);
                    if (dayofWeek == courseSchedule.Day)
                    {
                        _dbContext.DBTecCourse.Add(new ETecCourse
                        {
                            CourseDateTime = startDate,
                            CourseScheduleType = courseSchedule.CourseScheduleType,
                            CoursingStatus = TecCoursingStatus.Normal,
                            LessonCode = courseSchedule.LessonCode,
                            Day = courseSchedule.Day,
                            CourseName = courseSchedule.CourseName,
                            TecCode = tecCode,
                            Lesson = courseSchedule.Lesson,
                            TimeStart = time.StartTime,
                            TimeEnd = time.EndTime,

                        });
                        startDate = startDate.AddDays(7);
                    }

                }

            }
        }

        #endregion

        #region TecLeave
    

        public List<RTecLesson> GetTecOneDayAllLesson(string tecCode, string date)
        {
            var time = StaticDataSrv.CourseTime;
            var result = _dbContext.DBTecCourse.Where(a => a.CourseDateTime.ToString("yyyy-MM-dd") == date && a.TecCode == tecCode)
                .Select(a => new RTecLesson()
                {
                    Id = a.Id,
                    TimeRange = time[a.Lesson].TimeRange,
                    LessonCode = a.LessonCode,
                    CourseName = a.CourseName,
                    Lesson = a.Lesson,
                    CoursingStatus = a.CoursingStatus,
                    CourseStatusName = BaseEnumSrv.GetCoursingStatusName(a.CoursingStatus),
                   
                }).OrderBy(a=>a.Lesson);

            return result.ToList();
        }

        public void SubmitLeave(List<long> list, ETecLeave tecLeave)
        {
            try
            {
                tecLeave.ApplyDateTime = DateTime.Now;

                _dbContext.Database.BeginTransaction();
                var sql = sql_UpdateTecCourseLeaveBatch(list);
                _dbContext.Database.ExecuteSqlCommand(sql);
                var dbleave = _dbContext.DBTecLeave.Where(a => a.TecCode == tecLeave.TecCode &&
                                            a.LeaveDate.ToString("yyyy-MM-dd") == tecLeave.LeaveDate.ToString("yyyy-MM-dd")).FirstOrDefault();
                if(dbleave == null)
                    _dbContext.DBTecLeave.Add(tecLeave);
                else
                {
                    dbleave.LeaveType = tecLeave.LeaveType;
                }
                _dbContext.SaveChanges();

                _dbContext.Database.CommitTransaction();
               
            }
            catch(Exception ex)
            {
                _dbContext.Database.RollbackTransaction();
                throw ex;
              
            }
         
        }

        public List<RTecLeave> QueryTecLeave(string tecCode,string leaveDate,out int total,int pageIndex=1,int pageSize=20)
        {
            leaveDate = DateTime.Parse(leaveDate).ToString("yyyy-MM");

            IQueryable<ETecLeave> sql = null;

            if (!string.IsNullOrEmpty(tecCode))
                sql = _dbContext.DBTecLeave.Where(a=>a.TecCode == tecCode);
            if (!string.IsNullOrEmpty(leaveDate))
            {
                if (sql != null)
                    sql = sql.Where(a => a.LeaveDate.ToString("yyyy-MM") == leaveDate);
                else
                    sql = _dbContext.DBTecLeave.Where(a => a.LeaveDate.ToString("yyyy-MM") == leaveDate);
            }
            if (sql == null)
            {
                total = _dbContext.DBTecLeave.Count();
                sql = _dbContext.DBTecLeave.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                
            }  
            else
            {
                total = sql.Count();
                sql = sql.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            var restult = sql.Select(a => new RTecLeave
            {
                ApplyDateTime = a.ApplyDateTime,
                LeaveDate = a.LeaveDate,
                LeaveType = a.LeaveType,
                Remark = a.Remark,
                TecCode = a.TecCode,
                TecName = a.TecName
            });
            return restult.ToList();
         

        }
       
        public List<RTecLesson> GetTecLeaveCourse(string tecCode,string date)
        {
            date = DateTime.Parse(date).ToString("yyyy-MM-dd");
            var sql = _dbContext.DBTecCourse.Where(a => a.CoursingStatus == TecCoursingStatus.ForLeave &&
            a.TecCode == tecCode &&
            a.CourseDateTime.ToString("yyyy-MM-dd") == date);

            var time = StaticDataSrv.CourseTime;
            var restult = sql.Select(a => new RTecLesson
            {
                TimeRange = time[a.Lesson].TimeRange,
                CourseName = a.CourseName,
                TecCode = tecCode,
            });
            return restult.ToList();
        }
        #endregion


    }
}
