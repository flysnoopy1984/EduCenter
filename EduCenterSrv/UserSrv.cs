using EduCenterCore.Common.Helper;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.User;
using EduCenterModel.User.Result;
using EduCenterModel.WX;
using EduCenterSrv.Common;
using EduCenterSrv.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduCenterSrv
{
    public class UserSrv
    {
        private EduDbContext _dbContext;
        public UserSrv(EduDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region SQL
        public static string sql_UpdateUserRole(UserRole updateRole,string openId)
        {
            string sql = $"update UserInfo set UserRole = {(int)updateRole} where OpenId='{openId}'";
            return sql;
        }
        #endregion

        /// <summary>
        /// 添加或更新微信用户，微信相关字段总是更新
        /// </summary>
        /// <param name="wxUser"></param>
        public EUserInfo AddOrUpdateFromWXUser(WXUserInfo wxUser)
        {

            EUserInfo user = _dbContext.DBUserInfo
                             .Where<EUserInfo>(u => u.OpenId == wxUser.openid)
                             .FirstOrDefault();
            if(user == null)
            {
                 user = new EUserInfo
                 {
                      OpenId = wxUser.openid,
                      ChildName = "",
                      Name = wxUser.nickname,
                      Sex = wxUser.sex,
                      UserRole = UserRole.Visitor,
                      CreatedDateTime = DateTime.Now,
                      UpdatedDateTime = DateTime.Now,
                 };
            }
            user.wx_Name = wxUser.nickname;
            user.wx_city = wxUser.city;
            user.wx_country = wxUser.country;
            user.wx_headimgurl = wxUser.headimgurl;
            user.wx_province = wxUser.province;
            if(user.Id>0)
                _dbContext.DBUserInfo.Update(user);
            else
                _dbContext.DBUserInfo.Add(user);

            _dbContext.SaveChanges();

            return user;
        }

        #region UserCourese
        /// <summary>
        /// 根据用户，和类型获取可用的课程
        /// </summary>
       public List<RUserCourse> GetUserCourseAvaliable(string OpenId, CourseScheduleType CourseScheduleType)
       {
            var times = StaticDataSrv.CourseTime;
          
           var result = _dbContext.DBUserCoures.Join(_dbContext.DbCourseSchedule, uc => uc.LessonCode, cs => cs.LessonCode, (uc, cs) => new RUserCourse
            {
               UserOpenId = uc.UserOpenId,
               CourseScheduleType = uc.CourseScheduleType,
               UserCourseStatus = uc.UserCourseStatus,
               Day = cs.Day,
               Lesson = cs.Lesson,
               Time = times[cs.Lesson].TimeRange,
               CourseName = cs.CourseName,

            }).Where(a => a.UserOpenId == OpenId &&
                    a.UserCourseStatus == UserCourseStatus.Avaliable &&
                    a.CourseScheduleType == CourseScheduleType)
              .OrderBy(a=>a.Day).ToList();

            return result;
       }

        public bool CheckHasUserCourse(string OpenId, CourseScheduleType CourseScheduleType)
        {
            int n =  _dbContext.DBUserCoures.Where(a => a.CourseScheduleType == CourseScheduleType && a.UserCourseStatus != UserCourseStatus.WaitingPay).Count();
            return (n > 0);
        }

        public RUserCourse GetCurrentUserCourse(string OpenId, CourseScheduleType CourseScheduleType)
        {
            int dayofWeek = DateSrv.GetDayOfWeek(DateTime.Now);
            int h = DateTime.Now.Hour;

            var times = StaticDataSrv.CourseTime;
            var result = _dbContext.DBUserCoures.Join(_dbContext.DbCourseSchedule, uc => uc.LessonCode, cs => cs.LessonCode, (uc, cs) => new RUserCourse
            {
                Day = cs.Day,
                CourseName = cs.CourseName,
                LessonCode = cs.LessonCode,
                Lesson = cs.Lesson,
                Time = times[cs.Lesson].TimeRange,
                UserOpenId = uc.UserOpenId,
                StartTime = times[cs.Lesson].StartTime,
                EndTime = times[cs.Lesson].EndTime,
                UserCourseStatus = uc.UserCourseStatus,
                CourseScheduleType = uc.CourseScheduleType
            })
            .Where(a => a.Day == dayofWeek &&
            a.CourseScheduleType == CourseScheduleType &&
            a.UserOpenId == OpenId &&
            a.UserCourseStatus == UserCourseStatus.Avaliable &&
            a.StartTime<=h &&
            a.EndTime>=h).FirstOrDefault();
            return result;
        }

        public RUserCourse GetNextUserCourse(string OpenId)
        {
            var result = _dbContext.DBUserCourseLog.Join(_dbContext.DbCourseSchedule,
            uc => uc.LessonCode, cs => cs.LessonCode, (uc, cs) => new RUserCourse
            {
                UserOpenId = uc.UserOpenId,
                CourseScheduleType = uc.CourseScheduleType,            
                CourseName = cs.CourseName,
                LessonCode = uc.LessonCode,
                
              

            }).Where(a => a.UserOpenId == OpenId).FirstOrDefault();
            return result;
        }

        public RUserCourse GetUserNextCourse(string OpenId, CourseScheduleType CourseScheduleType, List<RUserCourse> userCourses = null)
        {
            if (userCourses == null)
                userCourses = GetUserCourseAvaliable(OpenId, CourseScheduleType);
            RUserCourse result = null;

            if (userCourses.Count > 0)
            {
                result = userCourses[0];
                if(userCourses.Count>1)
                {
                    int curDay = DateSrv.GetDayOfWeek(DateTime.Now);
                    int minDiff = Math.Abs(result.Day - curDay);

                    List<RUserCourse> TempList = new List<RUserCourse>();
                    TempList.Add(result);

                    for (int i = 1; i < userCourses.Count; i++)
                    {
                        int diff = Math.Abs(userCourses[i].Day - curDay);
                        if (diff < minDiff)
                        {
                            minDiff = diff;
                            result = userCourses[i];
                            TempList.Clear();
                        }
                        else if(diff == minDiff)
                        {
                            TempList.Add(userCourses[i]);
                        }    
                    }
                    if(TempList.Count>1)
                    {
                        result = TempList.OrderBy(a => a.Lesson).FirstOrDefault();
                    }
                }
                result.NextCourseDate = DateSrv.GetNextCourseDate(result.Day);
              
            }

            return result;
         
        }

        public List<RUserCourse> GetAllUserCourseByLessonCode(string lessonCode)
        {
            string sql = @"select ui.Name,uc.LessonCode from UserCourse as uc
join UserInfo as ui on ui.OpenId = uc.UserOpenId
left join UserCourseLog as uclog on uclog.LessonCode = uc.LessonCode";
            return null;
        }

        public void AddUserCourse(List<EUserCourse> courseList)
        {
            _dbContext.DBUserCoures.AddRange(courseList);   
        }
        #endregion

        #region UserCoureseLog
        /// <summary>
        /// 排除Pre的记录
        /// </summary>
        /// <param name="OpenId"></param>
        /// <param name="CourseScheduleType"></param>
        /// <param name="topnum"></param>
        /// <returns></returns>
        public List<RUserCourseLog> GetUserCourseLogHistory(string OpenId, CourseScheduleType CourseScheduleType,int topnum =5)
        {
           var result =  _dbContext.DBUserCourseLog.Join(_dbContext.DbCourseSchedule,
                uc => uc.LessonCode, cs => cs.LessonCode, (uc, cs) => new RUserCourseLog
                {
                    UserOpenId = uc.UserOpenId,
                    CourseScheduleType = uc.CourseScheduleType,
                    CreatedDateTime = uc.CreatedDateTime,
                    CourseName = cs.CourseName,
                    LessonCode = uc.LessonCode,
                    UserCourseLogStatus = uc.UserCourseLogStatus

                }).Where(a => a.UserOpenId == OpenId && 
                        a.CourseScheduleType == CourseScheduleType &&
                        a.UserCourseLogStatus!= UserCourseLogStatus.PreNext).Take(topnum).ToList();
            return result;
        }

        public RUserCourseLog GetUserCourseLogPre(string OpenId, CourseScheduleType CourseScheduleType)
        {
            var times = StaticDataSrv.CourseTime;
            var result = _dbContext.DBUserCourseLog.Join(_dbContext.DbCourseSchedule,
                 uc => uc.LessonCode, cs => cs.LessonCode, (uc, cs) => new RUserCourseLog
                 {
                     UserOpenId = uc.UserOpenId,
                     CourseScheduleType = uc.CourseScheduleType,
                     UserCourseLogStatus = uc.UserCourseLogStatus,
                     CourseTime = times[cs.Lesson].TimeRange, 
                     CourseName = cs.CourseName,
                     LessonCode = uc.LessonCode,
                     CourseDateTime = uc.CourseDateTime

                 }).Where(a => a.UserOpenId == OpenId &&
                         a.CourseScheduleType == CourseScheduleType &&
                         a.UserCourseLogStatus == UserCourseLogStatus.PreNext).FirstOrDefault();
            return result;
        }


        public void AddNextCourseLog(string OpenId,bool isIncludeToday = true)
        {
            int dayofWeek = DateSrv.GetDayOfWeek(DateTime.Now);
            int curHour = DateTime.Now.Hour;

            var times = StaticDataSrv.CourseTime;

            EUserCourseLog ucLog = null;
            RUserCourse nextCourse = null;
            List<RUserCourse> courseList = null;
          
            courseList = _dbContext.DBUserCoures.Join(_dbContext.DbCourseSchedule,
                        uc => uc.LessonCode,
                        cs => cs.LessonCode,
                        (uc, cs) => new RUserCourse
                        {
                            Day = cs.Day,
                            Lesson = cs.Lesson,
                            CourseScheduleType = cs.CourseScheduleType,
                            LessonCode = cs.LessonCode,
                            StartTime = times[cs.Lesson].StartTime,
                            EndTime = times[cs.Lesson].EndTime,
                            UserOpenId = uc.UserOpenId,
                        })
                        .Where(a => a.UserOpenId == OpenId)
                        .OrderByDescending(a => a.Day)
                        .ThenBy(a => a.Lesson).ToList();

            //如果不是第一次报名则计算当天的。
            if (isIncludeToday)
                nextCourse = courseList.Where(a => a.Day == dayofWeek && a.StartTime > curHour).FirstOrDefault();
              
            if(nextCourse ==null)
            {
                foreach (var c in courseList)
                {
                    if (dayofWeek < c.Day)
                    {
                        nextCourse = c; break;
                    }
                }
            }
            if (nextCourse == null) nextCourse = courseList[courseList.Count - 1];         
            ucLog = new EUserCourseLog
            {
                UserOpenId = OpenId,
                CourseDateTime = DateSrv.GetNextCourseDate(nextCourse.Day).ToString("yyyy-MM-dd"),
                CreatedDateTime = DateTime.Now,
                CourseScheduleType = nextCourse.CourseScheduleType,
                LessonCode = nextCourse.LessonCode,
                UserCourseLogStatus = UserCourseLogStatus.PreNext
            };
            var existLog = _dbContext.DBUserCourseLog.Where(a => a.UserOpenId == OpenId &&
                                              a.LessonCode == ucLog.LessonCode &&
                                              a.CourseDateTime == ucLog.CourseDateTime).FirstOrDefault();
            if(existLog == null)
            {
                _dbContext.DBUserCourseLog.Add(ucLog);
                _dbContext.SaveChanges();
            }
        }
        #endregion



        //public List<EHoliday> GetHolidayJson()
        //{

        //   return  _dbContext.DBHoliday.ToList();


        //}



    }
}
