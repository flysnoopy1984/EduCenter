﻿using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.QR;
using EduCenterModel.Order;
using EduCenterModel.Teacher;
using EduCenterModel.User;
using EduCenterSrv.SMS;
using Microsoft.EntityFrameworkCore;
using EduCenterModel.Sales;
using EduCenterModel.AliPay;
using EduCenterModel.ArtShow;
using EduCenterModel.News;
using EduCenterModel.Res;
using EduCenterModel.Tools;

namespace EduCenterSrv.DataBase
{
    public class EduDbContext: DbContext
    {

        public EduDbContext(DbContextOptions<EduDbContext> options) :base(options)
        {
            
        }
     
        public DbSet<EUserInfo> DBUserInfo { get; set; }

        public DbSet<EUserChild> DBUserChild { get; set; }

        public DbSet<EUserAccount> DBUserAccount { get; set; }

        public DbSet<EUserNote> DBUserNote { get; set; }

        public DbSet<EUserInfoBackEnd> DBUserInfoBackEnd { get; set; }

        public DbSet<ETecInfo> DBTecInfo { get; set; }

        public DbSet<ETecLeave> DBTecLeave { get; set; }

        public DbSet<ETecSkill> DBTecSkill { get; set; }

        public DbSet<ETecCourse> DBTecCourse { get; set; }

        public DbSet<ECourseInfo> DBCourseInfo { get; set; }

        public DbSet<ECourseInfoClass> DBCourseInfoClass { get; set; }

        public DbSet<ECourseSchedule> DbCourseSchedule { get; set; }

        public DbSet<ECoursePrice> DBCoursePrice { get; set; }

        public DbSet<EUserCourse> DBUserCoures { get; set; }

        public DbSet<EUserCourseLog> DBUserCourseLog { get; set; }

        public DbSet<EOrder> DBOrder { get; set; }

        public DbSet<EOrderLine> DBOrderLine { get; set; }

     
        public DbSet<ETrialLog> DBTrialLog { get; set; }

        public DbSet<EHoliday> DBHoliday { get; set; }

     
        public DbSet<ECourseDateRange> DBCourseDateRange { get; set; }

        public DbSet<EQRInvite> DBQRInvite { get; set; }

        public DbSet<EInviteLog> DBInviteLog { get; set; }

        public DbSet<EInviteRewardTrans> DBInviteRewardTrans { get; set; }

        //SMS Begin
        public DbSet<ESMSLog> DBSMSLog { get; set; }

        public DbSet<ESMSVerification> DBSMSVerification { get; set; }

        //SMS End

        public DbSet<EAliPayApplication> DBAliPayApplication { get; set; }

        public DbSet<EArtInfo> DbArtInfo { get; set; }

        public DbSet<EArtDetail> DbArtDetail { get; set; }

        public DbSet<EArtComment> DbArtComment { get; set; }

        public DbSet<EUserPraize> DbUserPraize { get; set; }

        public DbSet<ENewsInfo> DbNewsInfo { get; set; }

        public DbSet<EUserLogin> DbUserLogin { get; set; }

        #region Tools
        public DbSet<ELessonQR> DbLessonQR { get; set; }
        #endregion

        #region app
        public DbSet<EAppBanner> DbBanner { get; set; }
        public DbSet<EAppIcons> DbAppIcons { get; set; }

        public DbSet<EAppNavigation> DbAppNavigation { get; set; }
        #endregion

    }
}
