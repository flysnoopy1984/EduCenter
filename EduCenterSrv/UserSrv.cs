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

        public List<RUserCourseLog> GetUserCourseLog(string OpenId, CourseScheduleType CourseScheduleType,int topnum =5)
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

                }).Where(a => a.UserOpenId == OpenId && a.CourseScheduleType == CourseScheduleType).Take(topnum).ToList();
            return result;
        }

       



        //public List<EHoliday> GetHolidayJson()
        //{

        //   return  _dbContext.DBHoliday.ToList();


        //}
        #endregion


    }
}
