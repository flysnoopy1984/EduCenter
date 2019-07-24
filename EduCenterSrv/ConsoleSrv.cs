using EduCenterSrv.DataBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduCenterSrv.Common;
using EduCenterModel.User.Result;
using EduCenterModel.BaseEnum;
using EduCenterModel.Job;

namespace EduCenterSrv
{
    public class ConsoleSrv: BaseSrv
    {

        public ConsoleSrv(EduDbContext dbContext) : base(dbContext)
        {

        }

        //晚上跑Job修复用户上课
        public void RunJob_FixUserCourse()
        {
            //Job第二天凌晨运行，所以-1;
            var signDate = DateTime.Now.AddDays(-1);
            signDate = DateTime.Parse("2019-07-22");

            UserSrv userSrv = new UserSrv(_dbContext);
            BusinessSrv businessSrv = new BusinessSrv(_dbContext);
           
            var day = DateSrv.GetDayOfWeek(signDate);
          
         
            var sql = from uc in _dbContext.DBUserCoures
                      join cs in _dbContext.DbCourseSchedule on uc.LessonCode equals cs.LessonCode
                      join ui in _dbContext.DBUserInfo on uc.UserOpenId equals ui.OpenId
                      where cs.Day == day
                      orderby uc.UserOpenId
                      select new FixUserCourse
                      {
                          UserOpenId = uc.UserOpenId,
                          LessonCode = uc.LessonCode,
                          MemberType = ui.MemberType,
                          UserName = ui.Name
                      };

            var userCourseList = sql.ToList();

            foreach(var uc in userCourseList)
            {
                if(!userSrv.IsSkipTodayUserCourse(uc.UserOpenId))
                {
                   
                    uc.CurrentCourseSchedule = userSrv.GetCurrentCourseScheduleType(uc.UserOpenId, uc.MemberType);
                   
                    var log = businessSrv.UpdateCourseLogToSigned(uc.UserOpenId, uc.MemberType, uc.CurrentCourseSchedule, uc.LessonCode, signDate,false);
                    log.IsFixedByAuto = true;
                    log.AutoFixedDatetime = DateTime.Now;
                    _dbContext.SaveChanges();
                }
            }
 
        }
    }
}
