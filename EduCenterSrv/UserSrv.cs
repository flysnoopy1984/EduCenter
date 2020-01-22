using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterCore.WX;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.User;
using EduCenterModel.User.In;
using EduCenterModel.User.Result;
using EduCenterModel.WX;
using EduCenterModel.WX.MessageTemplate;
using EduCenterSrv.Common;
using EduCenterSrv.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;



namespace EduCenterSrv
{
    public class UserSrv: BaseSrv
    {
      
        public UserSrv(EduDbContext dbContext) : base(dbContext)
        {
           
        }

        #region SQL
        public static string sql_UpdateUserRole(UserRole updateRole,string openId)
        {
            string sql = $"update UserInfo set UserRole = {(int)updateRole} where OpenId='{openId}'";
            return sql;
        }

        public static string sql_UpdateUserPhone(string openId,string phone)
        {
            string sql = $@"update UserInfo set Phone = '{phone}' where OpenId='{openId}'";
            return sql;
        }

        public static string sql_UpdateUserCourseLogStatus(string lessonCode, string openId, CourseScheduleType courseScheduleType, UserCourseLogStatus userCourseLogStatus)
        {
            string sql = $@"update UserCourseLog 
                            set UserCourseLogStatus = '{(int)userCourseLogStatus}'
                            where UserOpenId = '{openId}' and LessonCode = '{lessonCode}' and CourseScheduleType = '{(int)courseScheduleType}'";
            return sql;
        }

        public static string sql_DeleteAllUserChild(string openId)
        {
            string sql = $"delete from UserChild where UserOpenId ='{openId}'";
            return sql;
        }

        public static string sql_UpdateUserChild(string openId,string ChildName)
        {
            string sql = $@"update UserInfo 
                            set ChildName = '{ChildName}'
                            where OpenId = '{openId}'";
            return sql;
        }

       

        #endregion

        #region UserInfo

        private EUserInfo CreateNewUserFromWXUser(WXUserInfo wxUser)
        {
            EUserInfo user = new EUserInfo
            {
                OpenId = wxUser.openid,
            
                Name = wxUser.nickname,
                Sex = wxUser.sex,
                UserRole = UserRole.Visitor,
                CreatedDateTime = DateTime.Now,
                UpdatedDateTime = DateTime.Now,
            };
            _dbContext.DBUserInfo.Add(user);

            EUserAccount eUserAccount = CreateNewUserAccount(wxUser.openid);
             _dbContext.DBUserAccount.Add(eUserAccount);
            return user;
        }

       
        public void WXNewUserNotification(WXUserInfo wxUser, EUserInfo owner)
        {
            NewUserJoinWXTemplate wxMessage = new NewUserJoinWXTemplate();
            string ownName = owner == null ? "" : owner.Name;
            foreach(var receiverOpenId in GlobalSrv.GetNewUserReceiverList())
            {
             //   NLogHelper.InfoTxt($"WXNotify to {receiverOpenId}");
                wxMessage.data = wxMessage.GenerateData(receiverOpenId, wxUser.nickname, DateTime.Now, ownName);
                WXApi.SendTemplateMessage<NewUserJoinWXTemplate>(wxMessage);
            }
          
        }
        /// <summary>
        /// 添加或更新微信用户，微信相关字段总是更新
        /// </summary>
        /// <param name="wxUser"></param>
        public EUserInfo AddOrUpdateFromWXUser(WXUserInfo wxUser,EUserInfo owner = null,bool isSave = true,string fromChannel="")
        {

            EUserInfo user = _dbContext.DBUserInfo
                             .Where<EUserInfo>(u => u.OpenId == wxUser.openid)
                             .FirstOrDefault();
            if(user == null)
            {
               
                user = CreateNewUserFromWXUser(wxUser);
                //    NLogHelper.InfoTxt("WXNotification Start");
                //微信提醒
                if (!string.IsNullOrEmpty(fromChannel) && owner == null)
                    owner = new EUserInfo { Name = fromChannel };
                WXNewUserNotification(wxUser, owner);
            }
            user.wx_Name = wxUser.nickname;
            user.wx_city = wxUser.city;
            user.wx_country = wxUser.country;
            user.wx_headimgurl = wxUser.headimgurl;
            user.wx_unionid = wxUser.unionid;
            user.wx_province = wxUser.province;
            user.wx_unionid = wxUser.unionid;
            if(user.Id>0)
                _dbContext.DBUserInfo.Update(user);
        
            if(isSave)
            _dbContext.SaveChanges();

            return user;
        }

     
        public EUserInfo GetUserInfo(string openId)
        {
            return _dbContext.DBUserInfo.Where(a => a.OpenId == openId).FirstOrDefault();
        }


     
        public EUserInfo GetUserInfoByUninonId(string unionId)
        {
            return _dbContext.DBUserInfo.Where(a => a.wx_unionid == unionId).FirstOrDefault();
        }

        public bool ExistUnionId(string unionId)
        {
            return _dbContext.DBUserInfo.Where(a => a.wx_unionid == unionId).Count() > 0;
        }

        public List<EUserInfo> GetAllMemberList()
        {
            //  MemberType mt = 
            return _dbContext.DBUserInfo.Where(a => a.UserRole == UserRole.Member).ToList();
        }

        public bool IsExistUser(string openId)
        {
            return _dbContext.DBUserInfo.Where(a => a.OpenId == openId).Count()>0;
        }

        public RUserInfo_AdjustCourse GetUserInfo_ForAdjustCourse(string openId)
        {
            var sql = from ui in _dbContext.DBUserInfo
                      join ua in _dbContext.DBUserAccount on ui.OpenId equals ua.UserOpenId
                      where ui.OpenId == openId
                      select new RUserInfo_AdjustCourse
                      {
                          MemberTypeName = BaseEnumSrv.GetMemberTypeName(ui.MemberType),
                          openId = ui.OpenId,
                          RemainStd = ua.RemainCourseTime,
                          RemainSummer = ua.RemainSummerTime,
                          RemainWinter = ua.RemainWinterTime,
                          wxName = ui.Name,
                      };

                      return sql.FirstOrDefault();

        }

 
        #endregion

        #region UserCourese
        /// <summary>
        /// 根据用户，和类型获取可用的课程
        /// </summary>
        public List<RUserCourse> GetUserCourseAvaliable(string OpenId, int courseScheduleType=-1)
       {
        
            var time = StaticDataSrv.CourseTime;
            //按Day，Lesson排序获取用户所有课程
            var sql = from uc in _dbContext.DBUserCoures
                      join cs in _dbContext.DbCourseSchedule on uc.LessonCode equals cs.LessonCode
                      where uc.UserOpenId == OpenId 
                    //  && uc.CourseScheduleType == CourseScheduleType
                      orderby cs.Day, cs.Lesson
                      select new RUserCourse
                      {
                          Day = cs.Day,
                          Lesson = cs.Lesson,
                          LessonCode = cs.LessonCode,
                          CourseName = cs.CourseName,
                          StartTime = time[cs.Lesson].StartTime,
                          EndTime = time[cs.Lesson].EndTime,
                          UserOpenId = uc.UserOpenId,
                          Time = time[cs.Lesson].TimeRange,
                          CourseScheduleType = uc.CourseScheduleType,
                      };
            if (courseScheduleType != -1)
            {
                sql = sql.Where(a => a.CourseScheduleType ==(CourseScheduleType)courseScheduleType);
            }

            return sql.ToList();
 
       }

        public List<RUserCouser_WithCoureSchedule> GetUserAllCourse_WithSchedule(string OpenId)
        {

            var time = StaticDataSrv.CourseTime;
            //按Day，Lesson排序获取用户所有课程
            var sql = from uc in _dbContext.DBUserCoures
                      join cs in _dbContext.DbCourseSchedule on uc.LessonCode equals cs.LessonCode
                      where uc.UserOpenId == OpenId 
                      orderby cs.Day, cs.Lesson
                      select new RUserCouser_WithCoureSchedule
                      {
                          UserCourseId = uc.Id,
                          Year = cs.Year,
                          Day = cs.Day,
                          Lesson = cs.Lesson,
                          LessonCode = cs.LessonCode,
                          CourseName = cs.CourseName,
                          UserOpenId = uc.UserOpenId,
                          Time = time[cs.Lesson].TimeRange,
                          CourseScheduleType = uc.CourseScheduleType,
                      };
          
               List<RUserCouser_WithCoureSchedule> result = sql.ToList();


                return result;

        }


        public List<RUserCurrentCourse> GetUserCouseLogByLessonCode(string lessonCode,string date)
        {
          
            var efSql = from uc in _dbContext.DBUserCoures.Where(a => a.LessonCode == lessonCode)
                        //join ul in _dbContext.DBUserCourseLog.Where(a=>a.CourseDateTime == date) on uc.LessonCode equals ul.LessonCode into uc_ul
                        //from ucul in uc_ul.DefaultIfEmpty()
                        join ui in _dbContext.DBUserInfo on uc.UserOpenId equals ui.OpenId
                        select new RUserCurrentCourse
                        {
                            UserOpenId = ui.OpenId,
                            LessonCode = uc.LessonCode,
                       //     SignDateTime =ucul==null? "":ucul.UserSignDateTime.ToString("yyyy-MM-dd HH:mm"),
                       //     UserCourseLogStatus = ucul == null? UserCourseLogStatus.PreNext:ucul.UserCourseLogStatus,
                       //     UserCourseLogStatusName = ucul==null?"无":BaseEnumSrv.GetUserCourseLogStatusNameForTec(ucul.UserCourseLogStatus),
                            UserName = ui.Name,
                            MemberType = ui.MemberType,
                            ChildName = ui.ChildName,
                        };
            return efSql.ToList();
                                 
        }

        #region 用户选择课程时Check
        public bool CheckUserCanSelectCourse(string openId, CourseScheduleType courseScheduleType)
        {

           EUserAccount userAccount = GetUserAccount(openId);
           switch(courseScheduleType)
            {
                case CourseScheduleType.Group:
                case CourseScheduleType.Standard:
                    return userAccount.CanSelectCourse;
                case CourseScheduleType.Summer:
                case CourseScheduleType.Winter:
                    return userAccount.CanSelectSummerWinterCourse;
            }
            return false;
           // int n = _dbContext.DBUserCoures.Where(a => a.UserOpenId == OpenId &&
           //a.CourseScheduleType == CourseScheduleType &&
           //a.UserCourseStatus != UserCourseStatus.WaitingPay).Count();
           // return (n > 0);
        }
        public bool CheckUserHasThisCourse(string openId,string LessonCode)
        {
            int count = _dbContext.DBUserCoures.Where(a => a.UserOpenId == openId && a.LessonCode == LessonCode).Count();
            return (count > 0);
        }

        //检测是否要跳过今天课时
        public bool IsSkipTodayUserCourse(string openId)
        {
            int c =_dbContext.DBUserCoures.Where(a => a.UserOpenId == openId &&
         //   a.CreateDateTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd") &&
            a.CreateDateTime == DateTime.Today &&
            a.UseRightNow == false).Count();

            return c > 0;

        }
        #endregion

        public void AddUserCourse(List<EUserCourse> courseList)
        {
            _dbContext.DBUserCoures.AddRange(courseList);   
        }
        public void AddUserCourse(EUserCourse course)
        {
            _dbContext.DBUserCoures.Add(course);
        }

        public EUserCourse GetUserCouresByCode(string lessonCode)
        {
            return _dbContext.DBUserCoures.Where(a => a.LessonCode == lessonCode).FirstOrDefault();
        }
        public EUserCourse GetUserCouresById(long Id)
        {
            return _dbContext.DBUserCoures.Where(a => a.Id == Id).FirstOrDefault();
        }
        //标准版和假期版切换
        public int SwitchUserCourseScheduleType(string openId,string lessonCode,CourseScheduleType courseScheduleType)
        {
            string sql = $@"update UserCourse set coursescheduletype = {(int)courseScheduleType} 
                            where UserOpenId='{openId}' and LessonCode='{lessonCode}'";

           return _dbContext.Database.ExecuteSqlRaw(sql);
        }

        public void DeleteUserCourse(string openId,string LessonCode)
        {
            var uc = _dbContext.DBUserCoures.Where(a => a.UserOpenId == openId && a.LessonCode == LessonCode).FirstOrDefault();
            if(uc!=null)
            {
                _dbContext.DBUserCoures.Remove(uc);
                var log = _dbContext.DBUserCourseLog.Where(a => a.UserOpenId == openId && a.LessonCode == LessonCode && a.UserCourseLogStatus == UserCourseLogStatus.PreNext).FirstOrDefault();
                if(log!=null) _dbContext.DBUserCourseLog.Remove(log);
            }
        }

      
        public CourseScheduleType GetCurrentCourseScheduleType(string openId,MemberType memberType)
        {
          
            ECourseDateRange dr = StaticDataSrv.CourseDateRange.Where(a => a.StartDate <= DateTime.Today &&
            a.EndDate >= DateTime.Today).FirstOrDefault();

            if(dr!=null)
            {
                EUserAccount userAccount = GetUserAccount(openId);
                if(memberType == MemberType.Normal)
                {
                    if (dr.CourseScheduleType == CourseScheduleType.Summer &&
                userAccount.RemainSummerTime > 0)
                        return CourseScheduleType.Summer;

                    else if (dr.CourseScheduleType == CourseScheduleType.Winter &&
                       userAccount.RemainWinterTime > 0)
                        return CourseScheduleType.Winter;
                }
                else
                {
                    //不能选择，说明用户已经选择过假期课
                    //if(!userAccount.CanSelectSummerWinterCourse)
                    //{
                    //    if (dr.CourseScheduleType == CourseScheduleType.Summer)
                    //        return CourseScheduleType.Summer;
                    //    else if (dr.CourseScheduleType == CourseScheduleType.Winter)
                    //        return CourseScheduleType.Winter;
                    //}
                    return dr.CourseScheduleType;


                }
             
            }
            return CourseScheduleType.Standard;

        }

        //用户签到
        public List<RUserSign> GetCurrentUserSign(string openId, CourseScheduleType courseScheduleType)
        {
            List<RUserSign> result = new List<RUserSign>();

            string today = DateTime.Now.ToString("yyyy-MM-dd");
            //先检查今天是否有课
            var todayCourse = GetUserCourseByDate(openId, today, courseScheduleType);
            if(todayCourse.Count >0 )
            {
                foreach(var c in todayCourse)
                {
                    RUserSign rUserSign = new RUserSign
                    {
                        LessonCode = c.LessonCode,
                        CanSign = true,
                        CourseDate = today,
                        CourseName = c.CourseName,
                        CourseScheduleType = courseScheduleType,
                        CourseScheduleTypeName = BaseEnumSrv.GetCourseScheduleTypeName(courseScheduleType),
                        StartTime = c.CourseTime.Substring(0, 5),
                        UserCourseLogStatus = c.UserCourseLogStatus,
                        UserCourseLogStatusName = c.UserCourseLogStatusName,
                         
                    };
                    result.Add(rUserSign);
                }
                return result;
            }
            else
            {
                //从第二天开始寻找下节课
                RUserShowCourse rUserShowCourse = GetNextUserCourse(openId, courseScheduleType, DateTime.Today.AddDays(1));

                ////如果因为用户请假，任然允许签到
                if (rUserShowCourse.CourseSkipList.Count > 0)
                {
                    foreach (var skipcourse in rUserShowCourse.CourseSkipList)
                    {
                        if (skipcourse.CourseSkipReason == CourseSkipReason.UserLeave)
                        {
                            RUserSign userSign = new RUserSign()
                            {
                                LessonCode = skipcourse.LessonCode,
                                CanSign = false,
                                CourseName = skipcourse.CourseName,
                                CourseDate = skipcourse.Date,
                                CourseScheduleType = courseScheduleType,
                                CourseScheduleTypeName = BaseEnumSrv.GetCourseScheduleTypeName(courseScheduleType),
                                StartTime = skipcourse.StartTime,
                                UserCourseLogStatus = UserCourseLogStatus.Leave,

                            };
                            result.Add(userSign);
                            //if (DateTime.Parse(skipcourse.Date) == DateTime.Today)
                            //    result.CanSign = true;
                            break;
                        }
                    }
                }
                if (result.Count == 0)
                {
                    var time = StaticDataSrv.CourseTime;
                    var startTime = time[rUserShowCourse.NextLesson].TimeRange.Substring(0, 5);
                    RUserSign userSign = new RUserSign()
                    {
                        CanSign = false,
                      
                        CourseDate = rUserShowCourse.NextCourseDate,
                        CourseName = rUserShowCourse.NextCourseName,
                        CourseScheduleType = courseScheduleType,
                        CourseScheduleTypeName = BaseEnumSrv.GetCourseScheduleTypeName(courseScheduleType),
                        StartTime = startTime,
                        UserCourseLogStatus = UserCourseLogStatus.PreNext,
                    };
                    result.Add(userSign);
                    return result;
                }
            }
            return result;
        }
        public RUserShowCourse GetNextUserCourse(List<RUserCourse> list, DateTime startDate)
        { 
            return RecursionToGetAvaliableCourse(list, startDate, new RUserShowCourse(true));
        }

        public RUserShowCourse GetNextUserCourse(string openId,CourseScheduleType courseScheduleType,DateTime startDate)
        {
            var time = StaticDataSrv.CourseTime;
            //按Day，Lesson排序获取用户所有课程
            var sql = from uc in _dbContext.DBUserCoures
                      join cs in _dbContext.DbCourseSchedule on uc.LessonCode equals cs.LessonCode
                      where uc.UserOpenId == openId //&& uc.CourseScheduleType == courseScheduleType
                      orderby cs.Day, cs.Lesson
                      select new RUserCourse
                      {
                          Day = cs.Day,
                          Lesson = cs.Lesson,
                          LessonCode = cs.LessonCode,
                          CourseName = cs.CourseName,
                          Time = time[cs.Lesson].TimeRange,
                          StartTime = time[cs.Lesson].StartTime,
                          EndTime = time[cs.Lesson].EndTime,
                          UserOpenId = uc.UserOpenId,
                      };
            
            var list = sql.ToList();
            if(list.Count == 0)
            {
                throw new EduException("您还没有安排课程");
            }

            return RecursionToGetAvaliableCourse(list, startDate, new RUserShowCourse(true));
        }
   
        private RUserShowCourse RecursionToGetAvaliableCourse(List<RUserCourse> list,DateTime date, RUserShowCourse result,int level =0)
        {
            var dayofWeek = DateSrv.GetDayOfWeek(date);
            
            RUserCourse gotCourse = null;
            DateTime gotDate = date;
            //先看今天有没有课
            var todayCourses = list.Where(a => a.Day == dayofWeek).ToList();
            if (todayCourses.Count > 0)
            {
                var curHour = DateSrv.GetLessonHour(date);
                //先看当前时间有没有课
                gotCourse = todayCourses.Where(a => a.StartTime <= curHour && 
                                                    a.EndTime >= curHour &&
                                                    a.Day == dayofWeek).FirstOrDefault();

                //没有,则比较开始时间大于现在时间的
                if (gotCourse == null)
                    gotCourse = todayCourses.Where(a => a.StartTime >= curHour && a.Day == dayofWeek).FirstOrDefault();
                else
                {
                    if (level == 0) result.IsCurrent = true;
                }
                
            }
            if (gotCourse == null)
            {
                //找本周后几天的课
                gotCourse = list.Where(a => a.Day > dayofWeek).OrderBy(a => a.Day).ThenBy(a=>a.Lesson).FirstOrDefault();
                //如果没找到，则为下周第一节课
                if (gotCourse == null)
                {
                    gotCourse = list[0];
                    int diff = 7-(dayofWeek - gotCourse.Day);
                    gotDate = gotDate.AddDays(diff);
                }  
                else
                {
                    int diff = gotCourse.Day - dayofWeek;
                    gotDate = gotDate.AddDays(diff);
                }
            }
            //找到显示的课程,尝试寻找课程记录
            var uLog = _dbContext.DBUserCourseLog.Where(a => a.UserOpenId == list[0].UserOpenId &&
                       a.LessonCode == gotCourse.LessonCode &&
                       a.CourseDateTime == gotDate.ToString("yyyy-MM-dd")).FirstOrDefault();

            //查看是否国庆，老师，学生请假
            CourseSkipReason courseSkip = CheckCourseSkipReason(gotDate, uLog);
            
            if(courseSkip== CourseSkipReason.NoSkip)
            {
                if (uLog == null)
                {
                    result.UserCourseLogStatus = UserCourseLogStatus.PreNext;
            //      TimeSpan ts = gotDate.Date - DateTime.Today;
                  
                    result.CanLeave = true;
                    result.CanSign = true;
                }
                else
                {
                    result.UserCourseLogStatus = uLog.UserCourseLogStatus;
                    if (uLog.UserCourseLogStatus == UserCourseLogStatus.PreNext)
                    {
                        result.CanLeave = true;
                        result.CanSign = true;
                    }
                    if (uLog.UserCourseLogStatus == UserCourseLogStatus.Leave)
                    {
                        result.CanSign = true;
                    }    
                }
               
                result.UserCourseLogStatusName = BaseEnumSrv.GetUserCourseLogStatusName(result.UserCourseLogStatus);
                result.NextCourseDate = gotDate.ToString("yyyy-MM-dd");
                result.NextCourseName = gotCourse.CourseName;
                result.LessonCode = gotCourse.LessonCode;
                result.NextLesson = gotCourse.Lesson;
            }
            else
            {
                //处理没有获取的理由
                RUserSkipCourse rUserSkipCourse = new RUserSkipCourse
                {
                    LessonCode = gotCourse.LessonCode,
                    CourseName = gotCourse.CourseName,
                    Date = gotDate.ToString("yyyy-MM-dd"),
                    StartTime = gotCourse.Time.Substring(0,5),
                    CourseSkipReason = courseSkip,
                    CourseSkipReasonName = BaseEnumSrv.GetCourseSkipReasonName(courseSkip),
                };
                result.CourseSkipList.Add(rUserSkipCourse);
                //国假日
                if (courseSkip == CourseSkipReason.Holiday)
                {
                    var workDay = DateSrv.FindFirstWorkDayAfterHoliday(gotDate.AddDays(1));
                    result = RecursionToGetAvaliableCourse(list, workDay, result, level++);
                }
                else
                {
                    //老师或用户请假
                    //   result.CourseSkipList.Add(gotDate.ToString("yyyy-MM-dd"), courseSkip);
                    var time = StaticDataSrv.CourseTime[gotCourse.Lesson].EndTime+1;

                    date =DateTime.Parse(gotDate.ToString("yyyy-MM-dd")).AddHours(time);

                    result = RecursionToGetAvaliableCourse(list, date, result, level++);
                }
            }
          
            return result;
        }

        public List<RUserCourseLog> GetUserCourseByDate(string OpenId, string date, CourseScheduleType courseScheduleType)
        {
            var dayofweek = DateSrv.GetDayOfWeek(DateTime.Parse(date));

            List<RUserCourseLog> result = null;

            var times = StaticDataSrv.CourseTime;

            var efSql = from uc in _dbContext.DBUserCoures
                        join ul in _dbContext.DBUserCourseLog.Where(a => a.CourseDateTime == date) on uc.LessonCode equals ul.LessonCode into uc_ul
                        from ucul in uc_ul.DefaultIfEmpty()
                        join cs in _dbContext.DbCourseSchedule on uc.LessonCode equals cs.LessonCode
                        where uc.UserOpenId == OpenId &&
                              //uc.CourseScheduleType == courseScheduleType &&
                              cs.Day == dayofweek
                        //ucul.UserCourseLogStatus != UserCourseLogStatus.SignIn &&
                        //ucul.UserCourseLogStatus != UserCourseLogStatus.Absent

                        select new RUserCourseLog
                        {
                            CourseScheduleType = uc.CourseScheduleType,
                            CourseName = cs.CourseName,
                            Lesson = cs.Lesson,
                            Day = cs.Day,
                            CourseTime = times[cs.Lesson].TimeRange,
                            UserCourseLogStatus = ucul == null ? UserCourseLogStatus.PreNext : ucul.UserCourseLogStatus,
                            LessonCode = uc.LessonCode,
                            UserCourseLogStatusName = ucul == null ? BaseEnumSrv.UserCourseLogStatusList[(int)UserCourseLogStatus.PreNext] : BaseEnumSrv.UserCourseLogStatusList[(int)ucul.UserCourseLogStatus]
                        };

            result = efSql.ToList();

            return result;
        }

        /// <summary>
        /// 用户请假，老师请假，国假日
        /// </summary>
        /// <returns></returns>
        private CourseSkipReason CheckCourseSkipReason(DateTime courseDate,EUserCourseLog uLog)
        {
            if (DateSrv.IsHoliday(courseDate))
                return CourseSkipReason.Holiday;

          
            if (uLog == null)
                return CourseSkipReason.NoSkip;
            if (uLog.UserCourseLogStatus == UserCourseLogStatus.TecLeave)
                return CourseSkipReason.TecLeave;
            if (uLog.UserCourseLogStatus == UserCourseLogStatus.Leave)
                return CourseSkipReason.UserLeave;

            return CourseSkipReason.NoSkip;

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
        public List<RUserCourseLog> GetUserCourseLogHistory(string OpenId, int topnum =5)
        {

            var result = _dbContext.DBUserCourseLog.Join(_dbContext.DbCourseSchedule,
                 uc => uc.LessonCode, cs => cs.LessonCode, (uc, cs) => new RUserCourseLog
                 {
                     UserOpenId = uc.UserOpenId,
                     CourseScheduleType = uc.CourseScheduleType,
                     CourseScheduleTypeName = BaseEnumSrv.GetCourseScheduleTypeName(uc.CourseScheduleType),
                     CourseDateTime = uc.CourseDateTime,
                     CourseName = cs.CourseName,
                     LessonCode = uc.LessonCode,
                     CreatedDateTime = uc.CreatedDateTime,
                     UserCourseLogStatus = uc.UserCourseLogStatus,

                     UserCourseLogStatusName = BaseEnumSrv.UserCourseLogStatusList[(int)uc.UserCourseLogStatus],

                     Id = uc.Id,

                })
                .OrderByDescending(a=>a.CreatedDateTime)
                .Where(a => a.UserOpenId == OpenId && 
                       a.UserCourseLogStatus!= UserCourseLogStatus.PreNext).Take(topnum).ToList();
            return result;
        }

        public List<RUserCourseList> QueryUserCourseLogList(string openId, out int totalPage,string LessonCode=null, int pageIndex = 1, int pageSize = 20)
        {
            var time = StaticDataSrv.CourseTime;
            var sql = _dbContext.DBUserCourseLog.Join(_dbContext.DbCourseSchedule,
                uc => uc.LessonCode, cs => cs.LessonCode, (uc, cs) => new RUserCourseList
                {

                    CourseName = cs.CourseName,
                    CourseDate = uc.CourseDateTime,
                    Day = cs.Day,
                    LessonTime = time[cs.Lesson].TimeRange,
                    UserCourseLogStatus = uc.UserCourseLogStatus,
                    CourseStatusName = BaseEnumSrv.UserCourseLogStatusList[(int)uc.UserCourseLogStatus],
                    CreatedDateTime = uc.CreatedDateTime,
                    OpenId = uc.UserOpenId,
                    LessonCode = uc.LessonCode,
                    CourseScheduleTypeName = BaseEnumSrv.GetCourseScheduleTypeName(uc.CourseScheduleType),
                    SignUser = string.IsNullOrEmpty(uc.SignName)?"":uc.SignName,

                }).Where(a => a.OpenId == openId && a.UserCourseLogStatus != UserCourseLogStatus.PreNext);

            if (!string.IsNullOrEmpty(LessonCode))
                sql = sql.Where(a => a.LessonCode == LessonCode);

            int totalCount = sql.Count();
            totalPage = Convert.ToInt32(totalCount / pageSize)+1;

            var result = sql.OrderByDescending(a => a.CourseDate)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            
            return result;
        }

        public List<RUserCourseLog> GetUserCourseLogList(string OpenId,
                                                         UserCourseLogStatus userCourseLogStatus,
                                                         out int totalPage,
                                                         int pageIndex= 1,int pageSize =20)
        {
            totalPage = 1;

            var sql = _dbContext.DBUserCourseLog.Join(_dbContext.DbCourseSchedule,
                 uc => uc.LessonCode, cs => cs.LessonCode, (uc, cs) => new RUserCourseLog
                 {
                     UserOpenId = uc.UserOpenId,
                     CourseScheduleType = uc.CourseScheduleType,
                     CourseScheduleTypeName = BaseEnumSrv.GetCourseScheduleTypeName(uc.CourseScheduleType),
                     CourseDateTime = uc.CourseDateTime,
                     CourseName = cs.CourseName,
                     LessonCode = uc.LessonCode,
                     CreatedDateTime = uc.CreatedDateTime,
                     UserLeaveDateTime = uc.UserLeaveDateTime,
                     UserSignDateTime =uc.UserSignDateTime,
                     UserCourseLogStatus = uc.UserCourseLogStatus,
                    
                     UserCourseLogStatusName = BaseEnumSrv.UserCourseLogStatusList[(int)uc.UserCourseLogStatus],

                 });
            if (userCourseLogStatus == UserCourseLogStatus.Leave)
                sql = sql.OrderByDescending(a => a.UserLeaveDateTime);
            else if(userCourseLogStatus == UserCourseLogStatus.SignIn)
                sql = sql.OrderByDescending(a => a.UserSignDateTime);
            else
                sql = sql.OrderByDescending(a => a.CreatedDateTime);

            sql = sql.Where(a => a.UserOpenId == OpenId &&
                       a.UserCourseLogStatus == userCourseLogStatus);

            if(pageSize >10)
            {
                totalPage = Convert.ToInt32(sql.Count() / pageSize) + 1;
            }
            sql = sql.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return sql.ToList();
         
        }

        public List<RUserComsume> QueryUserCourseComsume(string OpenId, int pageIndex = 1, int pageSize = 10)
        {
            var time = StaticDataSrv.CourseTime;
            var sql = from log in _dbContext.DBUserCourseLog
                      join c in _dbContext.DbCourseSchedule on log.LessonCode equals c.LessonCode
                      where log.UserOpenId == OpenId && (int)log.UserCourseLogStatus >= 10
                      select new RUserComsume
                      {
                          CourseDate = log.CourseDateTime,
                          CourseTime = time[c.Lesson].TimeRange,
                          CourseName = c.CourseName,
                          CourseStatus = BaseEnumSrv.GetUserCourseLogStatusName(log.UserCourseLogStatus),
                          CourseSchudeuleType = BaseEnumSrv.GetCourseScheduleTypeName(c.CourseScheduleType),

                      };
            var result = sql.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return result;
        }

       

        public void UpdateUserCourseLogStatus(string lessonCode,string openId, CourseScheduleType courseScheduleType, UserCourseLogStatus userCourseLogStatus)
        {
            string sql = sql_UpdateUserCourseLogStatus(lessonCode, openId, courseScheduleType, userCourseLogStatus);
            _dbContext.Database.ExecuteSqlRaw(sql);
        }


        public RUserCourseLog AddNextCourseLog(string OpenId,bool isIncludeToday = true)
        {
            int dayofWeek = DateSrv.GetDayOfWeek(DateTime.Now);
            int curHour = DateTime.Now.Hour;

            var times = StaticDataSrv.CourseTime;

            RUserCourseLog result = null;
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
                            CourseName = cs.CourseName,
                            CourseScheduleType = uc.CourseScheduleType,
                            LessonCode = cs.LessonCode,
                            StartTime = times[cs.Lesson].StartTime,
                            EndTime = times[cs.Lesson].EndTime,
                            UserOpenId = uc.UserOpenId,
                            Time = times[cs.Lesson].TimeRange
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
            if (existLog == null)
            {
                _dbContext.DBUserCourseLog.Add(ucLog);
                _dbContext.SaveChanges();
            }
            else
                ucLog = existLog;

            //返回RUserCourseLog仅为了给其他函数用
            result = new RUserCourseLog
            {
                CourseName = nextCourse.CourseName,
                CourseTime = nextCourse.Time,
                UserOpenId = ucLog.UserOpenId,
                CourseDateTime = ucLog.CourseDateTime,
                CreatedDateTime = ucLog.CreatedDateTime,
                CourseScheduleType = ucLog.CourseScheduleType,
                LessonCode = ucLog.LessonCode,
                UserCourseLogStatus = ucLog.UserCourseLogStatus,
            };
               
      
            return result;
        }

        

        public void UpdateCourseLogToLeave(List<EUserCourseLog> logList,string openId)
        {
            foreach (var log in logList)
            {
                log.UserOpenId = openId;
                log.UserCourseLogStatus = UserCourseLogStatus.Leave;

                var data = _dbContext.DBUserCourseLog.Where(a => a.UserOpenId == log.UserOpenId &&
                                                     a.LessonCode == log.LessonCode &&
                                                     a.CourseDateTime == log.CourseDateTime &&
                                                     a.CourseScheduleType == log.CourseScheduleType).FirstOrDefault();
            
                if (data == null)
                {
                    log.CreatedDateTime = DateTime.Now;
                    log.UserLeaveDateTime = DateTime.Now;
                    _dbContext.DBUserCourseLog.Add(log);
                }
                else
                {
                    if (data.UserCourseLogStatus == UserCourseLogStatus.SignIn)
                        throw new EduException("您已签到,不能请假！");
                    if (data.UserCourseLogStatus == UserCourseLogStatus.Absent)
                        throw new EduException("您已缺席,不能请假！");
                    data.UserCourseLogStatus = UserCourseLogStatus.Leave;
                    data.UserLeaveDateTime = DateTime.Now;
                   
                }
                    
            }
            _dbContext.SaveChanges();
            
        }

        public void BatchCreateUserCourseLog(List<EUserCourseLog> list)
        {
            _dbContext.DBUserCourseLog.AddRange(list);
        }

       
        #endregion

        #region UserAccount

        public EUserAccount CreateNewUserAccount(string openId)
        {
            EUserAccount eUserAccount = new EUserAccount
            {
                DeadLine = DateTime.MinValue,
                SummerDeadLine = DateTime.MinValue,
                WinterDeadLine = DateTime.MinValue,

                RemainSummerTime = 0,
                RemainWinterTime = 0,
                RemainCourseTime = 0,
                CanSelectSummerWinterCourse = true,
                CanSelectCourse = true,

                BuyDate = DateTime.MinValue,
                SummerBuyDate = DateTime.MinValue,
                WinterBuyDate = DateTime.MinValue,

                UserOpenId = openId,
            };
            return eUserAccount;
        }
        public EUserAccount GetUserAccount(string openId)
        {
            EUserAccount result = _dbContext.DBUserAccount.Where(a => a.UserOpenId == openId).FirstOrDefault();
            if(result == null)
            {
                result = CreateNewUserAccount(openId);
                _dbContext.Add(result);
                _dbContext.SaveChanges();
            }
            return result;
        }

        

        public void UpdateCanSelectCourse(string openId,CourseScheduleType courseScheduleType,bool isCan)
        {
            var userAccount = GetUserAccount(openId);
            if (courseScheduleType == CourseScheduleType.Standard || courseScheduleType == CourseScheduleType.Group)
                userAccount.CanSelectCourse = isCan;
            else
                userAccount.CanSelectSummerWinterCourse = isCan;

        }

        public void UpdateAlipayAccount(string openId,string AliPayAccount)
        {
            var userAccount = GetUserAccount(openId);
            userAccount.AliPayAccount = AliPayAccount;
            _dbContext.SaveChanges();
        }

        
        #endregion

        #region UserChild

        public void AddNewSimpleChild(string openId,string childName)
        {
            var list = GetAllChild(openId);
            EUserChild child = new EUserChild()
            {
                UserOpenId = openId,
                Name = childName,
                No = list.Count+1,

            };
            //如果已经有2个孩子，删除一个
            if(list.Count>=2)
            {
                child.No = 2;
                var removeItem = list[list.Count - 1];
                _dbContext.DBUserChild.Remove(removeItem);
                list.Remove(removeItem);
            }
            _dbContext.DBUserChild.Add(child);

            //新孩子+到list中，计算ShowName
            list.Add(child);
            var sqlChild = GetSQL_ShowUserChildName(list);
            _dbContext.Database.ExecuteSqlRaw(sqlChild);
        }

        public string GetSQL_ShowUserChildName(List<EUserChild> list)
        {
            string childName = "";
            for (int i = 0; i < list.Count; i++)
            {
                childName += list[i].Name;
                if (i < list.Count - 1)
                    childName += "/";
            }
            var sqlChild = sql_UpdateUserChild(list[0].UserOpenId, childName);

            return sqlChild;
        }

        public void SaveChildList(List<EUserChild> list)
        {
         
            try
            {
                var sql = sql_DeleteAllUserChild(list[0].UserOpenId);
                //string childName = "";
                //for (int i=0;i<list.Count;i++)
                //{
                //    childName += list[i].Name ;
                //    if (i< list.Count-1)
                //        childName += "/";
                //}
                //var sqlChild = sql_UpdateUserChild(list[0].UserOpenId, childName);
                var sqlChild = GetSQL_ShowUserChildName(list);
                _dbContext.Database.BeginTransaction();

                _dbContext.Database.ExecuteSqlRaw(sql);
                _dbContext.DBUserChild.AddRange(list);

                _dbContext.Database.ExecuteSqlRaw(sqlChild);

                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();
            }
            catch
            {
                _dbContext.Database.RollbackTransaction();
            }
          
        }

        public List<EUserChild> GetAllChild(string openId)
        {
            return _dbContext.DBUserChild.Where(a => a.UserOpenId == openId).OrderBy(a=>a.No).ToList();
        }
        #endregion

        #region WebBackEnd

        public List<RUserList> QueryUserList(string userName,
            string babyName,
            int userRole,
            int memberType,
            string userOpenId,
            out int recordTotal, 
            int pageIndex=1,
            int pageSize =20)
        {

            var sql = from ui in _dbContext.DBUserInfo
                      join ua in _dbContext.DBUserAccount on ui.OpenId equals ua.UserOpenId
                      join sales in _dbContext.DBUserInfo on ui.SalesOpenId equals sales.OpenId into salesUser
                      from sui in salesUser.DefaultIfEmpty()                     
                      orderby ui.Id descending
                      select new RUserList
                      {
                          WxName = ui.Name,
                          BabyName = ui.ChildName,
                          RealName = ui.RealName,
                          userOpenId = ui.OpenId,
                          MemberType = ui.MemberType,
                          DeadLineStd = ua.DeadLine == DateTime.MinValue?DateTime.Parse("1900-01-01").ToString("yyyy-MM-dd") : ua.DeadLine.ToString("yyyy-MM-dd"),
                          DeadLineSummer = ua.SummerDeadLine.ToString("yyyy-MM-dd"),
                          DeadLineWinter = ua.WinterDeadLine.ToString("yyyy-MM-dd"),
                          RemainTimeStd = ua.RemainCourseTime,
                          RemainTimeSummer = ua.RemainSummerTime,
                          RemainTimeWinter = ua.RemainWinterTime,
                          UserRole = ui.UserRole,
                          UserRoleName = BaseEnumSrv.GetUserRoleName(ui.UserRole),
                          AllowChooseStd = ua.CanSelectCourse,
                          AllChooseWS = ua.CanSelectSummerWinterCourse,
                          VipPrice = ua.VIPPrice1,
                          WXJoinDateTime = ui.CreatedDateTime.ToString("yyyy-MM-dd"),
                          SalesName = sui == null ? "自助完成" : sui.RealName,
                          SalesOpenId = sui == null?"":sui.OpenId,
                          UserPhone = ui.Phone
                      };

            if(!string.IsNullOrEmpty(userOpenId))
            {
                sql = sql.Where(a => a.userOpenId == userOpenId);
            }
            else
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    sql = sql.Where(a => a.WxName.Contains(userName));
                }

                if (!string.IsNullOrEmpty(babyName))
                {
                    sql = sql.Where(a => a.BabyName.Contains(babyName));
                }
                if (userRole != -1)
                {
                    sql = sql.Where(a => a.UserRole == (UserRole)userRole);
                }
                if (memberType != -1)
                {
                    sql = sql.Where(a => a.MemberType == (MemberType)memberType);
                }
               
            }

            recordTotal = sql.Count();

            return sql.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

        }

        public List<RUserList> QueryUserList_FromStore(string userName,
          string babyName,
          int userRole,
          int memberType,
          string userOpenId,
          out int recordTotal,
          int pageIndex = 1,
          int pageSize = 20)
        {
            List<RUserList> result = new List<RUserList>();
            List<SqlParameter> spList = new List<SqlParameter>();
            spList.Add(new SqlParameter { Value = pageIndex-1, ParameterName = "@pageIndex" });
            spList.Add(new SqlParameter { Value = pageSize, ParameterName = "@pageSize" });
            spList.Add(new SqlParameter { Value = userName== null?"":userName, ParameterName = "@userName" });
            spList.Add(new SqlParameter { Value = babyName == null ? "" : babyName, ParameterName = "@babyName" });
            spList.Add(new SqlParameter { Value = userRole, ParameterName = "@userRole" });
            spList.Add(new SqlParameter { Value = memberType, ParameterName = "@memberType" });
            spList.Add(new SqlParameter { Value = userOpenId == null ? "" : userOpenId, ParameterName = "@userOpenId" });
            //存储过程Count先输出，其他数据再输出 2张表
     
            var arrayList =  SpExecForPage<RUserList>("Edu_QueryUserInfo", spList.ToArray());  
            recordTotal = (int)arrayList[0]["TotalCount"];
            for(int i=1;i< arrayList.Count;i++)
            {
                RUserList rUserList = new RUserList()
                {
                    WxName = Convert.ToString(arrayList[i]["Name"]),
                    BabyName = Convert.ToString(arrayList[i]["ChildName"]),
                    userOpenId = Convert.ToString(arrayList[i]["OpenId"]),
                    RealName = Convert.ToString(arrayList[i]["RealName"]),
                    MemberType = (MemberType)(arrayList[i]["MemberType"]),
                    DeadLineStd = Convert.ToDateTime(arrayList[i]["DeadLine"]).ToString("yyyy-MM-dd"),
                    DeadLineSummer = Convert.ToDateTime(arrayList[i]["SummerDeadLine"]).ToString("yyyy-MM-dd"),
                    DeadLineWinter = Convert.ToDateTime(arrayList[i]["WinterDeadLine"]).ToString("yyyy-MM-dd"),
                    RemainTimeStd = Convert.ToDouble(arrayList[i]["RemainCourseTime"]),
                    RemainTimeSummer = Convert.ToDouble(arrayList[i]["RemainSummerTime"]),
                    RemainTimeWinter = Convert.ToDouble(arrayList[i]["RemainWinterTime"]),
                    UserRole = (UserRole)(arrayList[i]["UserRole"]),
                    AllowChooseStd = Convert.ToBoolean(arrayList[i]["CanSelectCourse"]),
                    AllChooseWS = Convert.ToBoolean(arrayList[i]["CanSelectSummerWinterCourse"]),
                    VipPrice = Convert.ToDouble(arrayList[i]["VIPPrice1"]),
                    WXJoinDateTime = Convert.ToDateTime(arrayList[i]["CreatedDateTime"]).ToString("yyyy-MM-dd"),
                    SalesName = Convert.ToString(arrayList[i]["salesName"]),
                    SalesOpenId = Convert.ToString(arrayList[i]["SalesOpenId"]),
                    UserPhone = Convert.ToString(arrayList[i]["Phone"]),
                    HasTrial = !string.IsNullOrEmpty(Convert.ToString(arrayList[i]["HasTrial"])),
                    UserScore = arrayList[i]["UserScore"]==null?"0":Convert.ToString(arrayList[i]["UserScore"])

                };
                rUserList.UserRoleName = BaseEnumSrv.GetUserRoleName(rUserList.UserRole);
                result.Add(rUserList);
            }
            return result;
        }


        public bool UpdateUserData(InUserData userData)
        {
            var ui = _dbContext.DBUserInfo.Where(a => a.OpenId == userData.OpenId).FirstOrDefault();
            ui.MemberType = userData.MemberType;
            ui.RealName = userData.RealName;
            ui.UserRole = userData.UserRole;
            if (!string.IsNullOrEmpty(userData.Phone))
                ui.Phone = userData.Phone;

            if(ui.MemberType == MemberType.VIP && ui.UserRole == UserRole.Visitor)
            {
                ui.UserRole = UserRole.Member;
            }
            ui.SalesOpenId = userData.SalesOpenId;

            var ua = GetUserAccount(userData.OpenId);
            ua.VIPPrice1 = userData.VipPrice;
            ua.RemainCourseTime = userData.RemainTimeStd;
            ua.RemainSummerTime = userData.RemainTimeSummer;
            ua.RemainWinterTime = userData.RemainTimeWinter;
            ua.UserScore = userData.UserScore;
            if(userData.DeadLineStd != "1900-01-01")
                ua.DeadLine = DateTime.Parse(userData.DeadLineStd);
            

            _dbContext.SaveChanges();

            return true;
            //  ui.UserRole
        }

        public List<EUserInfo> GetSalesUserList()
        {
            return _dbContext.DBUserInfo.Where(a => a.UserRole == UserRole.Sales ||
            a.OpenId == "oh6cV1dh0hjoGEizCoKH1KU70UwQ" || 
            a.OpenId == "oh6cV1YaZFskTyZ3PXZ1g0VfSQjE").ToList();
        }

        #region UserNote
        public void AddUserNote(EUserNote eUserNote)
        {
            _dbContext.DBUserNote.Add(eUserNote);
        }
        public void DeleteUserNote(long Id)
        {
            string sql = $"delete from UserNote where Id={Id}";
            _dbContext.Database.ExecuteSqlRaw(sql);
      
        }

        public List<RUserNote> QueryUserNote(string userOpenId)
        {
            var linq = _dbContext.DBUserNote.Where(a => a.UserOpenId == userOpenId).Select(a => new RUserNote()
            {
                UserOpenId = a.UserOpenId,
                Content = a.Content,
                CreateBy = a.CreateBy,
                CreateDateTime = a.CreateDateTime,
                Id = a.Id,
                CanDelete = a.CreateDateTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd") ? true : false,
            });
            return linq.ToList();
          
        }
        #endregion
        #endregion


        #region API
        public RUserLogin ApiUserLoginByPhone(EUserLogin eUserLogin)
        {

            var userLogin = _dbContext.DbUserLogin.Where(a => a.LoginKey == eUserLogin.LoginKey.Trim() &&
            a.Pwd == eUserLogin.Pwd.Trim() &&
            a.AppSystem == eUserLogin.AppSystem
            ).FirstOrDefault();

            if (userLogin == null)
                return null;

            //从用户信息表获取数据
            var result = _dbContext.DBUserInfo.Where(a => a.Phone == eUserLogin.LoginKey).Select(a => new RUserLogin
            {
                Name = a.Name,
                UserRole = a.UserRole,
                HeaderUrl = string.IsNullOrEmpty(a.app_headerUrl) ? a.wx_headimgurl : a.app_headerUrl,
                WxOpenId = a.OpenId,
                Sex = a.Sex,
                Phone = a.Phone,

      
            }).FirstOrDefault();

            result.Token = EduCodeGenerator.UserLoginToken();
            result.EffectDate = DateTime.Now.AddHours(2);
            result.LoginKey = eUserLogin.LoginKey;

            //反写信息到用户登录表
            userLogin.Token = result.Token;
            userLogin.EffectDate = result.EffectDate;
            userLogin.WxOpenId = result.WxOpenId;
          
            _dbContext.SaveChanges();

            return result;

        }
        
        public int updateUserHeader(string userOpenId,string appHeaderUrl)
        {
            string sql = $"update UserInfo set app_headerUrl = '{appHeaderUrl}' where OpenId='{userOpenId}'";
            int result = _dbContext.Database.ExecuteSqlRaw(sql);
            return result;
          //  return sql;
        }
        #endregion 

    }
}
