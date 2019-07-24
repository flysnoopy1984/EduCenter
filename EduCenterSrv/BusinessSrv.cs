using System;

using EduCenterCore.Common.Helper;
using EduCenterCore.EduFramework;
using EduCenterCore.WX;
using EduCenterModel.BaseEnum;
using EduCenterModel.Common;
using EduCenterModel.Course;
using EduCenterModel.Order;
using EduCenterModel.Teacher;
using EduCenterModel.User;
using EduCenterModel.WX;
using EduCenterSrv.Common;
using EduCenterSrv.DataBase;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using EduCenterModel.Sales;

namespace EduCenterSrv
{
    public class BusinessSrv:BaseSrv
    {
        public BusinessSrv(EduDbContext dbContext) : base(dbContext)
        {

        }

        #region SQL
        ///// <summary>
        ///// 删除某一类型的用户课程（标准，夏季，冬季）
        ///// </summary>
        //public static string sql_DeleteAllUserCourseByType(string userOpenId,CoursePriceType coursePriceType)
        //{
        //    string sql = $"delete from UserCourse where UserOpenId='{userOpenId}' and CoursePriceType={(int)coursePriceType}";
        //    return sql;
        //}

   


        /// <summary>
        /// 更新这门课的总申请数
        /// </summary>
        public static string sql_UpdateCourseScheduleApplyNum(List<string> lessonCodeList)
        {
            string sqlCodes = "";
            for(int i=0;i<lessonCodeList.Count;i++)
            {
                sqlCodes += $"'{ lessonCodeList[i]}'";
                if((i+1)< lessonCodeList.Count)
                    sqlCodes += ",";
            }
            string sql = $"update CourseSchedule set ApplyNum+= 1 where LessonCode in ({sqlCodes})";
            return sql;
        }

        #endregion


        #region 提交购买课时订单
        
        public EOrder PayCourseOrder(string userOpenId, ECoursePrice coursePrice)
        {
            try
            {
               //  BeginTrans();

             
                //检查用户是否已经购买暑假寒假
                if(coursePrice.CourseScheduleType == CourseScheduleType.Summer ||
                    coursePrice.CourseScheduleType == CourseScheduleType.Winter)
                {
                    var sql = from o in _dbContext.DBOrder
                              join l in _dbContext.DBOrderLine on o.OrderId equals l.OrderId
                              where o.OrderStatus == OrderStatus.PaySuccess && o.CustOpenId == userOpenId
                              && l.ItemCode == coursePrice.PriceCode
                              select o.OrderId;

                    var item = sql.FirstOrDefault();
                    if(item != null)
                    {
                        var cstName = BaseEnumSrv.GetCourseScheduleTypeName(coursePrice.CourseScheduleType);
                        throw new EduException($"[{cstName}]只能购买一次，您已购买过");
                    }
                }

                //创建购买课时的订单
                var Order =  CreateBuyCourseOrder(userOpenId, coursePrice);

                _dbContext.SaveChanges();

              //  CommitTrans();

                return Order;

            }
            catch (Exception ex)
            {
             //  RollBackTrans();
              
                throw ex;
            }
          
        }


        private EOrder CreateBuyCourseOrder(string userOpenId, ECoursePrice coursePrice)
        {
            EOrder order = null;
            try
            {
                order = new EOrder
                {
                    CreateDateTime = DateTime.Now,
                    CustOpenId = userOpenId,
                    OrderStatus = OrderStatus.Created,
                    OrderType = OrderType.UserCourse,
                    OrderId = EduCodeGenerator.GetOrderOrder(),
                   
                };
                _dbContext.Add(order);

                EOrderLine line = new EOrderLine
                {
                    OrderId = order.OrderId,
                    ItemCode = coursePrice.PriceCode,
                    ItemName = $"课时购买{coursePrice.PriceCode}_{coursePrice.CourseScheduleType}",
                    Price = coursePrice.Price,
                    Qty = coursePrice.Qty,
                    Ext1 = (int)coursePrice.CourseScheduleType,
                };
                _dbContext.Add(line);

            }
            catch (Exception ex)
            {
              
                throw ex;
            }
            return order;

        }
        #endregion

        #region 购买课时成功
        public EUserAccount PayCourseSuccess(WXPaySuccess wXPaySuccess)
        {
            EUserAccount eUserAccount = null;
            try
            {
                string orderId = wXPaySuccess.OrderId;
            //    NLogHelper.InfoTxt($"PayCourseSuccess OrderId:{orderId}");
                BeginTrans();
                //跟新订单状态
                var order = _dbContext.DBOrder.Where(a => a.OrderId == orderId && a.OrderStatus == OrderStatus.Created).FirstOrDefault();
                if (order == null)
                    throw new Exception($"没有找到状态为[{ OrderStatus.Created}]的[{orderId}]订单,");
                order.OrderStatus = OrderStatus.PaySuccess;

                //获取订单行,更新课时
                var line = _dbContext.DBOrderLine.Where(a => a.OrderId == orderId).FirstOrDefault();
                eUserAccount = UpdateUserAccountByOrderLine(order.CustOpenId, line);

                var ui = _dbContext.DBUserInfo.Where(a => a.OpenId == order.CustOpenId).FirstOrDefault();
                if(ui.UserRole == UserRole.Visitor)
                {
                    ui.UserRole = UserRole.Member;
                }
                //如果是JSPay
                if(wXPaySuccess.IsJSPay)
                {
                    ui.MemberType = MemberType.VIP;
                    eUserAccount.VIPPrice1 = Math.Round(line.Price/line.Qty, 2);


                }
               

                _dbContext.SaveChanges();
                CommitTrans();
            }
            catch(Exception ex)
            {
                RollBackTrans();
                NLogHelper.ErrorTxt($"[PayCourseSuccess] {ex.Message}");
                throw ex;
            }
            return eUserAccount;
        }


       
      

      
        /// <summary>
        /// /获取订单行,更新课时
        /// </summary>
        /// <param name="userOpenId"></param>
        /// <param name="line"></param>
        private EUserAccount UpdateUserAccountByOrderLine(string userOpenId,EOrderLine line)
        {
       
            EUserAccount userAccount =  _dbContext.DBUserAccount.Where(a => a.UserOpenId == userOpenId).FirstOrDefault();
            if(userAccount == null)
            {
                UserSrv userSrv = new UserSrv(_dbContext);
                userAccount = userSrv.CreateNewUserAccount(userOpenId);
            }
            
            CourseScheduleType courseScheduleType = (CourseScheduleType)line.Ext1;
            ECourseDateRange dr = null;
            switch(courseScheduleType)
            {
                case CourseScheduleType.Standard:
                case CourseScheduleType.VIP:
                    userAccount.RemainCourseTime += line.Qty;
                    if (userAccount.BuyDate == DateTime.MinValue)
                        userAccount.BuyDate = DateTime.Now;
                    if (userAccount.DeadLine == DateTime.MinValue)
                        userAccount.DeadLine = DateTime.Now;
                    userAccount.DeadLine = userAccount.DeadLine.AddYears(1);
                
                    break;
                case CourseScheduleType.Summer:
                    userAccount.RemainSummerTime += line.Qty;
                    dr = StaticDataSrv.CourseDateRange.Where(a => a.CourseScheduleType == CourseScheduleType.Summer && a.Year == DateTime.Now.Year).FirstOrDefault();

                    userAccount.SummerDeadLine = dr.EndDate;

                    if (userAccount.SummerBuyDate == DateTime.MinValue)
                        userAccount.SummerBuyDate = DateTime.Now;

                    break;
                case CourseScheduleType.Winter:
                    userAccount.RemainWinterTime += line.Qty;
                    dr = StaticDataSrv.CourseDateRange.Where(a => a.CourseScheduleType == CourseScheduleType.Winter && a.Year == DateTime.Now.Year).FirstOrDefault();
                    userAccount.WinterDeadLine = dr.EndDate;
                    if (userAccount.WinterBuyDate == DateTime.MinValue)
                        userAccount.WinterBuyDate = DateTime.Now;
                    break;
                
            }
            return userAccount;

        }

        /// <summary>
        /// 新建课时交易
        /// </summary>
        /// <param name="line"></param>
        //private void AddCourseTimeTransByLine(EOrderLine line)
        //{
        //    EUserCourseTimeTrans trans = new EUserCourseTimeTrans
        //    {
        //        CourseScheduleType = (CourseScheduleType)line.Ext1,
        //        TransQty = line.Qty,
        //        UserOpenId = line.OrderId,
        //        CoursePriceCode = line.ItemCode,
        //        TransDateTime = DateTime.Now,
                
        //    };
        //    _dbContext.DBUserCourseTimeTrans.Add(trans);
        //}

        #endregion

        #region 选择课程

        /// <summary>
        /// 用户选择课程
        /// </summary>
        /// <param name="courseList"></param>
        public void UserSelectNewCourses(string openId,List<EUserCourse> courseList, CourseScheduleType courseScheduleType,bool useRightNow = false)
        {
            try
            {
                BeginTrans();
                if (courseList.Count > 0)
                {
                    UserSrv userSrv = new UserSrv(_dbContext);
                    TecSrv tecSrv = new TecSrv(_dbContext);
                  //  CourseScheduleType courseScheduleType = courseList[0].CourseScheduleType;
                    if (!userSrv.CheckUserCanSelectCourse(openId, courseScheduleType))
                        throw new EduException("无法选择，您已经选择过此类课程!，如果疑问，请联系客服");
                    else
                    {

                        foreach (var c in courseList)
                        {
                            if (userSrv.CheckUserHasThisCourse(openId, c.LessonCode))
                                continue;

                            c.UserOpenId = openId;

                            //更新课程总人数
                            var cs = _dbContext.DbCourseSchedule.Where(a => a.LessonCode == c.LessonCode).FirstOrDefault();
                            cs.ApplyNum++;

                            //获取课程对应的老师
                            var cls = _dbContext.DBCourseInfoClass.Where(s => s.CourseCode == cs.CourseCode).FirstOrDefault();
                            var tecCode = cls.TecCode;

                            //更新老师课程
                            tecSrv.UpdateTecCourse(tecCode, cs,DateTime.Now, useRightNow);

                            //添加用户课程
                            userSrv.AddUserCourse(c);

                        }
                        
                        userSrv.UpdateCanSelectCourse(openId, courseScheduleType, false);
                    }
                    _dbContext.SaveChanges();

                    userSrv.AddNextCourseLog(openId, false);

                    CommitTrans();

                  

                }
            }
            catch(Exception ex)
            {
                RollBackTrans();
              
                throw ex;
            }
           
            
        }


        #endregion

        #region 用户课时消耗
        public EUserCourseLog UpdateCourseLogToSigned(string openId,
            MemberType memberType, 
            CourseScheduleType courseScheduleType, 
            string lessonCode,
            DateTime signDate,
            bool needSave = true)
        {
            var date = signDate.ToString("yyyy-MM-dd");

            int result = UpdateUserCourseTimeOnce(openId, memberType, courseScheduleType);
            if(result == -1)
            {
                string courseScheduleTypeName = BaseEnumSrv.GetCourseScheduleTypeName(courseScheduleType);
                throw new EduException($"您的[{courseScheduleTypeName}]课时已用完，请先充值！",EduErrorMessage.NoCourseTime);
            }

            var log = _dbContext.DBUserCourseLog.Where(a => a.UserOpenId == openId &&
                                              a.LessonCode == lessonCode &&
                                              a.CourseDateTime == date &&
                                              a.CourseScheduleType == courseScheduleType).FirstOrDefault();
            if (log == null)
            {
                log = new EUserCourseLog
                {
                    CourseDateTime = date,
                    CourseScheduleType = courseScheduleType,
                    CreatedDateTime = DateTime.Now,
                    LessonCode = lessonCode,
                    UserCourseLogStatus = UserCourseLogStatus.SignIn,
                    UserSignDateTime = DateTime.Now,
                    UserOpenId = openId
                };
                _dbContext.DBUserCourseLog.Add(log);
            }
            else
            {
                log.UserCourseLogStatus = UserCourseLogStatus.SignIn;
                log.UserSignDateTime = DateTime.Now;
            }
            if(needSave)
             _dbContext.SaveChanges();
            return log;
        }

        //返回-1 用户余额不足
        public int UpdateUserCourseTimeOnce(string openId,MemberType memberType, CourseScheduleType courseScheduleType)
        {
            EUserAccount result = _dbContext.DBUserAccount.Where(a => a.UserOpenId == openId).FirstOrDefault();

            if(memberType == MemberType.Normal)
            {
                switch (courseScheduleType)
                {
                    case CourseScheduleType.Summer:
                        if (result.RemainSummerTime == 0) return -1;
                        else result.RemainSummerTime -= 2;
                        break;
                    case CourseScheduleType.Winter:
                        if (result.RemainWinterTime == 0) return -1;
                        else result.RemainWinterTime -= 2;
                        break;
                    default:
                        if (result.RemainCourseTime == 0) return -1;
                        else result.RemainCourseTime -= 2;
                        break;

                }
            }
            else
            {
                switch (courseScheduleType)
                {
                    case CourseScheduleType.Summer:
                        if (result.RemainSummerTime == 0)
                        {
                            if (result.RemainCourseTime == 0) return -1;
                            else result.RemainCourseTime -= 2;
                        }
                        else
                            result.RemainSummerTime -= 2;
                        break;
                    case CourseScheduleType.Winter:
                        if (result.RemainWinterTime == 0)
                        {
                            if (result.RemainCourseTime == 0) return -1;
                            else result.RemainCourseTime -= 2;
                        } 
                        else
                            result.RemainWinterTime -= 2;
                        break;
                    default:
                        if (result.RemainCourseTime == 0) return -1;
                        else result.RemainCourseTime -= 2;
                        break;

                }
            }
          
            return 0;

        }
        #endregion

        #region 用户
        /// <summary>
        /// 被邀请用户首次进入公众号，绑定关系
        /// wxUser 因为获取方式不同
        /// </summary>
        /// <param name="wxMessage"></param>
        /// <param name="ownOpenId"></param>
        /// <returns></returns>
        public EUserInfo InvitedUserComing(string InvitedOpenId,string ownOpenId, WXUserInfo wxUser = null)
        {
            UserSrv userSrv = new UserSrv(_dbContext);
            SalesSrv salesSrv = new SalesSrv(_dbContext);
            if (wxUser == null)
                wxUser = WXApi.GetWXUserInfo(InvitedOpenId);

            EUserInfo owner = null;
            //如果是老用户，不能绑定邀请
            if (!userSrv.IsExistUser(InvitedOpenId))
                owner = salesSrv.BindUser(ownOpenId, InvitedOpenId);

          
            EUserInfo user = userSrv.AddOrUpdateFromWXUser(wxUser, owner, false);

            _dbContext.SaveChanges();
            return user;

        }
        public bool UserRegisterByPhone(string openId, string phone, string babyName)
        {
            try
            {
                var updatePhoneSql = UserSrv.sql_UpdateUserPhone(openId, phone);
                _dbContext.Database.BeginTransaction();
                //更新用户手机号
                _dbContext.Database.ExecuteSqlCommand(updatePhoneSql);

                UserSrv userSrv = new UserSrv(_dbContext);
                //添加宝贝，同时更新用户表中宝贝名显示字段
                userSrv.AddNewSimpleChild(openId, babyName);
                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();
            }
            catch(Exception ex)
            {
                _dbContext.Database.RollbackTransaction();
                throw ex;
            }
            return true;
          

        }
        #endregion

      




    }
}
